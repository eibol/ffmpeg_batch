using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static System.Net.Mime.MediaTypeNames;

namespace FFBatch
{
    public partial class Form28 : Form
    {
        public Form28()
        {
            InitializeComponent();
        }

        public String item = "";
        public String dur = "";
        public String vf_crop = "";
        public String aspect_r = "";
        String selected_crop = "";
        Boolean failed = false;
        Boolean reset = false;

        public void UpdateColorDark(Control myControl)
        {
            myControl.BackColor = Color.FromArgb(255, 64, 64, 64);
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDark(subC);
            }
        }

        public void UpdateColorDefault(Control myControl)
        {
            myControl.BackColor = SystemColors.InactiveBorder;
            myControl.ForeColor = Control.DefaultForeColor;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDefault(subC);
            }
        }

        private void Form28_Load(object sender, EventArgs e)
        {            
            this.Text = Properties.Strings.crop1;
            var dateStr = "00:00:10";
            var dateTime = DateTime.ParseExact(dateStr, "HH:mm:ss", null, System.Globalization.DateTimeStyles.None);

            time_pre_subs.Format = DateTimePickerFormat.Custom;
            time_pre_subs.CustomFormat = "HH:mm:ss";              
            
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);             
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;                
            }

            if (!Directory.Exists(Path.GetTempPath() + "FFBatch_test"))
            {
                Directory.CreateDirectory(Path.GetTempPath() + "FFBatch_test");
            }
            
            lbl_f.Text = Path.GetFileName(item);
            if (get_dur_secs(dur) > 10)
            {
                time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 10);
            }
            else
            {
                dur = get_file_dur(item);
                if (get_dur_secs(dur) > 10)
                {
                    time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 10);
                }
                else
                {
                    time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
                }
            }            

            get_size();
            if (failed == false) original_frame();            
            if (failed == false) get_prop();

            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
            String[] crops = new string[] { "1:1", "4:3", "5:3", "16:9", "1920x1080", "1440x1080", "1366x768", "1280x720", "1024x768", "1024x576", "800x600", "800x480", "720x576", "720x480", "640x480", "640x360", "320x240" };
            foreach (String v_crops in crops) cb_crop.Items.Add(v_crops);
            String[] dar = new string[] { "1:1", "2:3", "4:3", "5:3", "16:9", "1.85", "2.35", "2.40" };
            foreach (String dars in dar) cb_dar.Items.Add(dars);

            time_pre_subs.Select();
            SendKeys.Send("{RIGHT}"); SendKeys.Send("{RIGHT}");            
        }

        Double get_dur_secs(String item)
        {
            TimeSpan t = new TimeSpan();
            if (TimeSpan.TryParse(item, out t)) { return t.TotalSeconds; }
            else return 0;
        }
        private String get_file_dur(String path)
        {
            Process tmp = new Process();
            tmp.StartInfo.FileName = Path.Combine(Application.StartupPath, "MediaInfo.exe");
            String ffprobe_frames = " " + '\u0022' + "--Inform=General;%Duration/String3%" + '\u0022';
            tmp.StartInfo.Arguments = ffprobe_frames + " " + '\u0022' + path + '\u0022';

            tmp.StartInfo.RedirectStandardOutput = true;
            tmp.StartInfo.UseShellExecute = false;
            tmp.StartInfo.CreateNoWindow = true;
            tmp.EnableRaisingEvents = true;
            tmp.Start();

            String duracion = tmp.StandardOutput.ReadToEnd();
            tmp.WaitForExit();

            if (duracion == null) return "00:00:00";
            else
            {
                TimeSpan time;
                if (TimeSpan.TryParse(duracion, out time))
                {
                    return duracion;
                }
                else return "00:00:00";
            }
        }

        private void get_size()
        {
            Boolean has_streams = false;
            Boolean has_video = false;
            Boolean to_remove = false;
            Boolean match = false;

            String ff_frames = String.Empty;
            Process get_frames = new Process();
            get_frames.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "Mediainfo.exe");
            String ffprobe_frames = "--Inform=Video;%Width%" + "x" + "%Height%";
            get_frames.StartInfo.Arguments = ffprobe_frames + " " + '\u0022' + item + '\u0022';

            get_frames.StartInfo.RedirectStandardOutput = true;
            get_frames.StartInfo.RedirectStandardError = true;
            get_frames.StartInfo.UseShellExecute = false;
            get_frames.StartInfo.CreateNoWindow = true;
            get_frames.EnableRaisingEvents = true;
            get_frames.Start();

            ff_frames = get_frames.StandardOutput.ReadLine();
            get_frames.WaitForExit();

            if (get_frames.ExitCode == 0)
            {
                if (ff_frames != null)
                {
                    has_streams = true;
                    if (ff_frames.ToLower().Contains("x"))
                    {
                        has_video = true;
                    }
                    else
                    {
                        has_video = false;
                    }
                }
                else
                {
                    failed = true;
                    ff_frames = String.Empty;                    
                    MessageBox.Show(Properties.Strings.err_size);
                    this.Close();
                    return;
                }
                txt_size1.Text = ff_frames;
                if (ff_frames.Length == 0)
                {
                    failed = true;
                    MessageBox.Show(Properties.Strings.err_size);
                    this.Close();
                    return;
                }
                n_w.Maximum = Convert.ToDecimal(ff_frames.Substring(0, ff_frames.IndexOf("x")));
                n_w.Value = 0;                
                n_h.Maximum = Convert.ToDecimal(ff_frames.Substring(ff_frames.IndexOf("x") + 1, ff_frames.Length - ff_frames.IndexOf("x") - 1));
                n_h.Value = 0;
                n_X.Maximum = n_w.Maximum;
                n_Y.Maximum = n_h.Maximum;
                n_crop_res_X.Maximum = n_w.Maximum;
                n_crop_res_Y.Maximum = n_h.Maximum;
            }
        }

        private void test_cropping()
        {
            this.Cursor = Cursors.WaitCursor;
            test_crop.Enabled = false;
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileName(item));
            String param = " -ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -t " + n_secs.Value.ToString() + " -c:v libx264 -crf 25 -preset ultrafast -c:a copy" + " -vf crop=" + (n_w.Maximum - n_w.Value -n_X.Value).ToString() + ":" + (n_h.Maximum -  n_h.Value - n_Y.Value).ToString() + ":" + n_X.Value.ToString() + ":" + n_Y.Value.ToString() + " " + aspect_r + " -y " + '\u0022' + temp_f + '\u0022'  + " -hide_banner";
            if (cb_crop.SelectedIndex != -1)
            {
                String coord_crp = String.Empty;
                if (chk_center.Checked == false) coord_crp = ":" + n_crop_res_X.Value.ToString() + ":" + n_crop_res_Y.Value.ToString();
                param = " -ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -t " + n_secs.Value.ToString() + " -c:v libx264 -crf 25 -preset ultrafast -c:a copy" + " -vf crop=" + selected_crop + coord_crp + " " + aspect_r + " -y " + '\u0022' + temp_f + '\u0022' + " -hide_banner";
            }
            String crop = "";
            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");            
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(item);
            proc.StartInfo.Arguments = param;
            proc.Start();
            while (!proc.StandardError.EndOfStream)
            {                
                list_lines.Add(proc.StandardError.ReadLine());
            }

            proc.WaitForExit(8000);
            test_crop.Enabled = true;
            this.Cursor = Cursors.Arrow;
            this.Enabled = true;
            if (proc.ExitCode != 0)
            {
                MessageBox.Show(Properties.Strings.err_crop);                
                return;
            }            
            
            if (proc.ExitCode == 0) Process.Start(temp_f);            
        }

        private void autodetect()
        {
            Process proc = new Process();
            String param = "-ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -t 1 -vf cropdetect -f null -" + " -hide_banner";
            String crop = "";
            Boolean ok = false;
            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(item);
            proc.StartInfo.Arguments = param;
            proc.Start();

            while (!proc.StandardError.EndOfStream)
            {
                list_lines.Add(proc.StandardError.ReadLine());
            }

            proc.WaitForExit(10000);            
            if (proc.ExitCode == 0)
            {
                foreach (String line in list_lines)
                {
                    if (line.Contains("cropdetect") && line.Contains("crop="))
                    {
                        try
                        {
                            int index = line.LastIndexOf("crop=") + 5;
                            crop = line.Substring(index, line.Length - index);
                            String[] split = crop.Split(':');
                            n_w.Value = n_w.Maximum - Convert.ToInt32(split[0]);
                            n_h.Value = n_h.Maximum - Convert.ToInt32(split[1]);
                            n_X.Value = Convert.ToInt32(split[2]);
                            n_Y.Value = Convert.ToInt32(split[3]);
                            ok = true;
                        }
                        catch
                        {
                            n_w.Value = 0;
                            n_h.Value = 0;
                            n_X.Value = 0;
                            n_Y.Value = 0;
                        }
                    }
                }
                if (ok == true) btn_frame.PerformClick();
            }

        }

        private void test_crop_Click(object sender, EventArgs e)
        {
            if (reset == true)
            {
                reset = false;
                return;
            }
            this.Enabled = false;
            test_cropping();
            this.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            cb_crop.SelectedIndex = -1;
            cb_dar.SelectedIndex = -1;
            cb_dar.Text = String.Empty;
            autodetect();            
            this.Enabled = true;
        }

        private void btn_frame_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            original_frame();
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileNameWithoutExtension(item) + ".jpg");            
            String param = "-ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -vframes 1 -f image2 -qscale:v 3 -y " + '\u0022' + temp_f + '\u0022' + " -hide_banner";

            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            proc.StartInfo.RedirectStandardError = true;            
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(item);
            proc.StartInfo.Arguments = param;
            proc.Start();

            while (!proc.StandardError.EndOfStream)
            {
                list_lines.Add(proc.StandardError.ReadLine());
            }
         
            proc.WaitForExit();
                  
            try
            {
                File.Delete(temp_f);
            }

            catch { }

            if (cb_crop.SelectedIndex == -1) crop_pic();
            //{
            //    Image img;
            //    using (var bmpTemp = new Bitmap(temp_f))
            //    {
            //        img = new Bitmap(bmpTemp);
            //        pic1.Image = img;
            //        bmpTemp.Dispose();
            //    }

            //    int x, y, w, h = 0;
            //    x = (int)n_X.Value;
            //    y = (int)n_Y.Value;
            //    w = (int)(n_w.Maximum - n_w.Value);
            //    h = (int)(n_h.Maximum - n_h.Value);
            //    if (w == 0 || h == 0) return;
            //    Rectangle rect = new Rectangle(x, y, w, h);
            //    Bitmap bmp = new Bitmap(img);
            //    pic1.Image = cropAtRect(bmp, rect, x, y);
            //}
            //else crop_pic();
            this.Cursor = Cursors.Arrow;
           change_crop();
           get_frame_prev();
        }

        private void original_frame()
        {
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileNameWithoutExtension(item) + ".jpg");
            String param = "-ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -vframes 1 -f image2 -qscale:v 3 -y " + '\u0022' + temp_f + '\u0022'  + " -hide_banner";

            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.EnableRaisingEvents = true;
            proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(item);
            proc.StartInfo.Arguments = param;
            proc.Start();

            while (!proc.StandardError.EndOfStream)
            {
                list_lines.Add(proc.StandardError.ReadLine());                
            }

            //foreach (String line in list_lines) MessageBox.Show(line);

            proc.WaitForExit();
            if (proc.ExitCode !=0)
            {
                failed = true;
                return;
            }

            Image img;
            using (var bmpTemp = new Bitmap(temp_f))
            {
                img = new Bitmap(bmpTemp);
                pic_0.Image = img;
                pic1.Image = pic_0.Image;
            }
            try
            {
                File.Delete(temp_f);
            }

            catch { }
        }


        public static Bitmap cropAtRect(Bitmap b, Rectangle r, Int32 x1, Int32 y1)
        {
            try
            {
                Bitmap nb = new Bitmap(r.Width - x1, r.Height - y1);
                using (Graphics g = Graphics.FromImage(nb))
                {
                    g.DrawImage(b, -r.X, -r.Y);
                    return nb;
                }
            } catch
            {
                Bitmap nb = new Bitmap(r.Width, r.Height);
                using (Graphics g = Graphics.FromImage(nb))
                {
                    g.DrawImage(b, r.X, r.Y);
                    return nb;
                }
            }
        }

        private void btn_use_Click(object sender, EventArgs e)
        {            
            if (n_w.Value == 0 && n_h.Value == 0 && n_X.Value == 0 && n_Y.Value == 0 && cb_crop.SelectedIndex == -1)
            { 
                vf_crop = String.Empty;
            }
            else if (cb_crop.SelectedIndex == -1)
            {
                vf_crop = " -vf crop=" + (n_w.Maximum - n_w.Value - n_X.Value).ToString() + ":" + (n_h.Maximum - n_h.Value - n_Y.Value).ToString() + ":" + n_X.Value.ToString() + ":" + n_Y.Value.ToString();
                Clipboard.SetText(vf_crop);
            }
            else 
            {
                String coord_crp = String.Empty;
                if (chk_center.Checked == false) coord_crp = ":" + n_crop_res_X.Value.ToString() + ":" + n_crop_res_Y.Value.ToString();
                vf_crop = " -vf crop=" + selected_crop + coord_crp;
                Clipboard.SetText(vf_crop);
            }
            
            this.Close();
        }

        private void txt_seek_Leave(object sender, EventArgs e)
        {
            DateTime time2;
            if (!DateTime.TryParse(time_pre_subs.Value.TimeOfDay.ToString(), out time2))
            {
                MessageBox.Show(FFBatch.Properties.Strings.time_bad, FFBatch.Properties.Strings.pre_input2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (TimeSpan.Parse(dur).TotalSeconds > 10) time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 10);
                else time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1);
                return;
            }
        }

        private void txt_seek_DoubleClick(object sender, EventArgs e)
        {
            if (TimeSpan.Parse(dur).TotalSeconds > 10) time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 10);
            else time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 1); ;
        }        

        private void crop_pic()
        {
            cb_crop.SelectedIndex = -1;
            cb_dar.SelectedIndex = -1;            
            int x, y, w, h = 0;
            x = Convert.ToInt32(n_X.Value);
            y = Convert.ToInt32(n_Y.Value);
            w = Convert.ToInt32(n_w.Maximum - n_w.Value);
            h = Convert.ToInt32(n_h.Maximum - n_h.Value);
            if (w == 0 || h == 0) return;
            Rectangle rect = new Rectangle(x, y, w, h);
            if (pic_0.Image != null)
            {
                Bitmap bmp = new Bitmap(pic_0.Image);
                pic1.Image = cropAtRect(bmp, rect, x, y);
                get_prop();
            }
        }
        private void get_prop()
        {
            Decimal r = 0;
            Decimal r2 = 0;
            Decimal r3 = 0;
            try
            {
                r = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 6);
                r2 = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 3);
                r3 = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 2);
            }
            catch { return; }

            if (cb_crop.SelectedIndex != -1)
            {
                String new_size = cb_crop.SelectedItem.ToString();
                int new_w = 0;
                int new_h = 0;
                Decimal rel1 = 1;
                Decimal rel2 = 1;
                Decimal test = 0;
                Decimal rel = 1;
                if (new_size.Contains(":"))
                {
                    if (new_size.Contains(":"))
                    {
                        if (Decimal.TryParse(new_size.Substring(0, new_size.LastIndexOf(":")), out test))
                            rel1 = test;
                        else return;
                        if (Decimal.TryParse(new_size.Substring(new_size.LastIndexOf(":") + 1, cb_crop.Text.Length - cb_crop.Text.LastIndexOf(":") - 1), out test))
                            rel2 = test;
                        else return;
                    }
                    rel = rel1 / rel2;
                    r = Math.Round(rel, 6);
                    r2 = Math.Round(rel, 3);
                    r3 = Math.Round(rel, 2);
                }
                if (new_size.Contains("x"))
                {
                    if (Decimal.TryParse(new_size.Substring(0, new_size.LastIndexOf("x")), out test))
                        rel1 = test;
                    else return;
                    if (Decimal.TryParse(new_size.Substring(new_size.LastIndexOf("x") + 1, new_size.Length - new_size.LastIndexOf("x") - 1), out test))
                        rel2 = test;
                    else return;
                }
                rel = rel1 / rel2;
                r = Math.Round(rel, 6);
                r2 = Math.Round(rel, 3);
                r3 = Math.Round(rel, 2);            
            }        

            Decimal pan = (decimal)1.777778;
            Decimal box = (decimal)1.333333;
            Decimal box2 = (decimal)1.250000;
            Decimal box3 = (decimal)1.500000;
            Decimal box4 = (decimal)1.666667;
            Decimal pan_cine = (decimal)2.400000;
            Decimal pan_pan = (decimal)2.350;
            Decimal pan_us = (decimal)1.850;
            Decimal wide = (decimal)2.000;           

            if (r == pan) lbl_prop.Text = "16:9";
            else if (r == pan_cine) lbl_prop.Text = "2:40:1";
            else if (r == box) lbl_prop.Text = "4:3";
            else if (r == box2) lbl_prop.Text = "5:4";
            else if (r == box3) lbl_prop.Text = "3:2";
            else if (r == box4) lbl_prop.Text = "5:3";
            else if (r2 == pan_pan) lbl_prop.Text = "2:35:1";
            else if (r2 == pan_us) lbl_prop.Text = "1:85:1";
            else if (r2 == wide) lbl_prop.Text = "2:1";
            else lbl_prop.Text = r3.ToString();
            if (cb_crop.SelectedIndex == -1)
            {
                lbl_prop.Text = (n_w.Maximum - n_w.Value - n_X.Value).ToString() + "x" + (n_h.Maximum - n_h.Value - n_Y.Value).ToString() + " / " + lbl_prop.Text;
            }
            else
            {
                lbl_prop.Text = cb_crop.SelectedItem.ToString() + " / " + lbl_prop.Text;
            }
        }

        private void txt_seek_TextChanged(object sender, EventArgs e)
        {
            DateTime time2;            
            if (DateTime.TryParse(time_pre_subs.Value.TimeOfDay.ToString(), out time2))
            {
                if (TimeSpan.Parse(dur).TotalSeconds > TimeSpan.Parse(time_pre_subs.Value.TimeOfDay.ToString()).TotalSeconds)
                {
                    Thread.Sleep(250);
                    btn_frame.PerformClick();
                }                
            }
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            vf_crop = String.Empty; 
            this.Close();
        }

        private void n_w_ValueChanged(object sender, EventArgs e)
        {   
            crop_pic();
        }

        private void n_h_ValueChanged(object sender, EventArgs e)
        {   
            crop_pic();
        }

        private void n_X_ValueChanged(object sender, EventArgs e)
        {   
            crop_pic();
        }

        private void n_Y_ValueChanged(object sender, EventArgs e)
        {   
            crop_pic();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            reset = true;
            n_w.Value = 0;
            n_h.Value = 0;
            n_X.Value = 0;
            n_Y.Value = 0;
            n_crop_res_X.Value = 0;
            n_crop_res_Y.Value = 0;
            chk_center.Checked = true;
            cb_crop.SelectedIndex = -1;
            cb_dar.SelectedIndex = -1;
            cb_dar.Text = String.Empty;
            pic1.Image = pic_0.Image;
            lbl_crop.Text = Properties.Strings.cropped;
        }

        private void Form28_Resize(object sender, EventArgs e)
        {            
            if (this.Width < 869) this.Width = 869;
            if (this.Height < 457) this.Height = 457;
            lbl_f.Width = this.Width - 57;
            groupBox1.Width = this.Width - (869 - 811);
            pic_0.Width = this.Width - (this.Width - 390) + (this.Width - 869) / 2;
            pic1.Width = this.Width - (this.Width - 390) + (this.Width - 869) / 2; 
            pic_0.Height = this.Height - (457 - 139);
            pic1.Height = this.Height - (457 - 139);
            pic1.Left = pic_0.Width + 52;
            lbl_or.Left = pic_0.Left + (pic_0.Width / 2) - (lbl_or.Width / 2);
            lbl_crop.Left = pic1.Left + (pic1.Width  / 2) - (lbl_crop.Width / 2);
            btn_use.Left = this.Width - btn_use.Width - 35;
            btn_cancel.Left = this.Width - btn_cancel.Width - btn_use.Width - 40;
            test_crop.Left = (this.Width / 2) - (test_crop.Width / 2) - 8;
            time_pre_subs.Left = test_crop.Left - time_pre_subs.Width - 28;            
            n_secs.Left = test_crop.Left + test_crop.Width + 8;
            lbl_sec.Left = n_secs.Left + n_secs.Width + 3;
            lbl_prop.Left = (this.Width / 2) - (lbl_prop.Width / 2) - 3;

            btn_frame.Top = this.Height - (457 - 365);
            test_crop.Top = this.Height - (457 - 365);
            btn_cancel.Top = this.Height - (457 - 365);
            btn_use.Top = this.Height - (457 - 365);
            time_pre_subs.Top = this.Height - (457 - 375);
            n_secs.Top = this.Height - (457 - 375);
            lbl_sec.Top = this.Height - (457 - 374);            
        }
        private void change_crop()
        {
            selected_crop = String.Empty;
            if (cb_crop.SelectedIndex == 0) selected_crop = "in_h";
            if (cb_crop.SelectedIndex == 1) selected_crop = "ih/3*4:ih";
            if (cb_crop.SelectedIndex == 2) selected_crop = "ih/3*5:ih";
            if (cb_crop.SelectedIndex == 3) selected_crop = "ih/9*16:ih";            
            if (cb_crop.SelectedIndex == 4) selected_crop = "1920:1080";
            if (cb_crop.SelectedIndex == 5) selected_crop = "1440:1080";
            if (cb_crop.SelectedIndex == 6) selected_crop = "1366:768";
            if (cb_crop.SelectedIndex == 7) selected_crop = "1280:720";
            if (cb_crop.SelectedIndex == 8) selected_crop = "1024:768";
            if (cb_crop.SelectedIndex == 9) selected_crop = "1024:576";
            if (cb_crop.SelectedIndex == 10) selected_crop = "800:600";
            if (cb_crop.SelectedIndex == 11) selected_crop = "800:480";
            if (cb_crop.SelectedIndex == 12) selected_crop = "720:576";
            if (cb_crop.SelectedIndex == 13) selected_crop = "720:480";
            if (cb_crop.SelectedIndex == 14) selected_crop = "640:480";
            if (cb_crop.SelectedIndex == 15) selected_crop = "640:360";
            if (cb_crop.SelectedIndex == 16) selected_crop = "320:240";

            if (cb_crop.SelectedIndex == -1)
            {
                n_h.Enabled = true;
                n_w.Enabled = true;
                n_X.Enabled = true;
                n_Y.Enabled = true;
                chk_center.Enabled = false;
            }
            else
            {
                n_h.Enabled = false;
                n_w.Enabled = false;
                n_X.Enabled = false;
                n_Y.Enabled = false;
                chk_center.Enabled = true;
            }            
        }

        private void get_frame_prev()
        {
            this.Cursor = Cursors.WaitCursor;
            test_crop.Enabled = false;
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileNameWithoutExtension(item) + ".jpg");
            String coord_crp = String.Empty;
            if (chk_center.Checked == false) coord_crp = ":" + n_crop_res_X.Value.ToString() + ":" + n_crop_res_Y.Value.ToString();
            String param = " -ss " + time_pre_subs.Value.TimeOfDay.ToString() + " -i " + '\u0022' + item + '\u0022' + " -frames:v 1 -vf crop=" + selected_crop + coord_crp + " -y " + '\u0022' + temp_f + '\u0022' + " -hide_banner";

            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            //proc.StartInfo.RedirectStandardError = true;
            //proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.EnableRaisingEvents = true;
            //proc.StartInfo.WorkingDirectory = Path.GetDirectoryName(item);
            proc.StartInfo.Arguments = param;
            proc.Start();
            proc.WaitForExit();
            test_crop.Enabled = true;
            this.Cursor = Cursors.Arrow;
            this.Enabled = true;
            if (proc.ExitCode != 0)
            {
                MessageBox.Show(Properties.Strings.err_crop);
                return;
            }

            if (proc.ExitCode == 0)
            {
                Image img;
                using (var bmpTemp = new Bitmap(temp_f))
                {
                    img = new Bitmap(bmpTemp);
                    pic1.Image = img;
                    bmpTemp.Dispose();
                }
            }
        }

        private void cb_crop_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_dar.SelectedIndex = -1;
            lbl_crop.Text = Properties.Strings.cropped;
            change_crop();
            get_frame_prev();
            get_prop();
        }

        private void cb_dar_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cb_dar.Text.Length > 0) aspect_r = "-aspect " + cb_dar.Text;
            else aspect_r = String.Empty;

            if (pic_0.Image != null) pic1.Image = pic_0.Image;
            Decimal test = 0;
            Decimal rel1 = 1;
            Decimal rel2 = 1;
            Decimal rel = 1;

            if (cb_dar.Text.Contains(":"))
            {
                if (Decimal.TryParse(cb_dar.Text.Substring(0, cb_dar.Text.LastIndexOf(":")), out test))
                    rel1 = test;
                else return;
                if (Decimal.TryParse(cb_dar.Text.Substring(cb_dar.Text.LastIndexOf(":") + 1, cb_dar.Text.Length - cb_dar.Text.LastIndexOf(":") - 1), out test))
                    rel2 = test;
                else return;
            }
            
            if (cb_dar.Text.Contains("."))
            {
                String dar_trans = cb_dar.Text.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                if (Decimal.TryParse(dar_trans, out test)) rel = test;
                else return;
            }
            else rel = rel1 / rel2;

            Decimal pre_rel = (Decimal)pic1.Image.Width / (Decimal)pic1.Image.Height;            
                int new_width = 0;            
            new_width = (int)(pic1.Image.Width * rel / pre_rel);
            Bitmap img_resize = new Bitmap(pic1.Image, new Size(new_width, pic1.Image.Height));            
            pic1.Image = img_resize;
            pic1.SizeMode = PictureBoxSizeMode.Zoom;
            lbl_crop.Text = Properties.Strings.cropped + " " + "(" + Properties.Strings.use_prev + ")";
            
        }

        private void cb_dar_TextUpdate(object sender, EventArgs e)
        {
            if (cb_dar.Text.Length > 1) aspect_r = "-aspect " + cb_dar.Text;
            else aspect_r = String.Empty;
        }

        private void chk_center_CheckedChanged(object sender, EventArgs e)
        {            
            if (chk_center.Checked)
            {
                n_crop_res_X.Enabled = false;
                n_crop_res_Y.Enabled = false;
            }
            else
            {
                n_crop_res_X.Enabled = true;
                n_crop_res_Y.Enabled = true;
            }
            change_crop();
            get_frame_prev();
        }

        private void n_crop_res_X_ValueChanged(object sender, EventArgs e)
        {
            cb_dar.SelectedIndex = -1;
            lbl_crop.Text = Properties.Strings.cropped;
            change_crop();
            get_frame_prev();
        }

        private void n_crop_res_Y_ValueChanged(object sender, EventArgs e)
        {
            cb_dar.SelectedIndex = -1;
            lbl_crop.Text = Properties.Strings.cropped;
            change_crop();
            get_frame_prev();
        }
        private void n_crop_res_X_KeyUp(object sender, KeyEventArgs e)
        {
            cb_dar.SelectedIndex = -1;
            lbl_crop.Text = Properties.Strings.cropped;            
            change_crop();
            get_frame_prev();
        }

        private void n_crop_res_Y_KeyUp(object sender, KeyEventArgs e)
        {
            cb_dar.SelectedIndex = -1;
            lbl_crop.Text = Properties.Strings.cropped;
            change_crop();
            get_frame_prev();
        }

        private void n_w_KeyUp(object sender, KeyEventArgs e)
        {
            crop_pic();
        }

        private void n_h_KeyUp(object sender, KeyEventArgs e)
        {
            crop_pic();
        }

        private void n_X_KeyUp(object sender, KeyEventArgs e)
        {
            crop_pic();
        }

        private void n_Y_KeyUp(object sender, KeyEventArgs e)
        {
            crop_pic();
        }

        private void time_pre_subs_ValueChanged(object sender, EventArgs e)
        {
            if (time_pre_subs.Value.TimeOfDay.TotalSeconds > get_dur_secs(dur))
            {
                MessageBox.Show(Properties.Strings.time_err + ", " + Properties.Strings.exc_dur);
                time_pre_subs.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            }
            btn_frame.PerformClick();
        }
    }
}