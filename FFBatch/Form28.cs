using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        Boolean failed = false;

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
            this.Text = Properties.Strings2.crop1;

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
            if (TimeSpan.Parse(dur).TotalSeconds > 10) txt_seek.Text = "00:00:10";
            else txt_seek.Text = "00:00:01";

            get_size();
            if (failed == false) original_frame();
            //if (failed == false) autodetect();
            if (failed == false) get_prop();
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
                    MessageBox.Show(Properties.Strings2.err_size);
                    this.Close();
                    return;
                }
                txt_size1.Text = ff_frames;
                if (ff_frames.Length == 0)
                {
                    failed = true;
                    MessageBox.Show(Properties.Strings2.err_size);
                    this.Close();
                    return;
                }
                n_w.Maximum = Convert.ToDecimal(ff_frames.Substring(0, ff_frames.IndexOf("x")));
                n_w.Value = 0;
                n_h.Maximum = Convert.ToDecimal(ff_frames.Substring(ff_frames.IndexOf("x") + 1, ff_frames.Length - ff_frames.IndexOf("x") - 1));
                n_h.Value = 0;
                n_X.Maximum = n_w.Maximum;
                n_Y.Maximum = n_h.Maximum;
            }
        }

        private void test_cropping()
        {
            this.Cursor = Cursors.WaitCursor;
            test_crop.Enabled = false;
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileName(item));
            String param = " -i " + '\u0022' + item + '\u0022' + " -ss " + txt_seek.Text + " -t " + n_secs.Value.ToString() + " -c:v libx264 -crf 25 -preset veryfast -c:a copy" + " -vf crop=" + (n_w.Maximum - n_w.Value -n_X.Value).ToString() + ":" + (n_h.Maximum -  n_h.Value - n_Y.Value).ToString() + ":" + n_X.Value.ToString() + ":" + n_Y.Value.ToString() + " -y " + '\u0022' + temp_f + '\u0022'  + " -hide_banner";
            String crop = "";
            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe");
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
            test_crop.Enabled = true;
            this.Cursor = Cursors.Arrow;
            this.Enabled = true;
            if (proc.ExitCode != 0)
            {
                MessageBox.Show(Properties.Strings2.err_crop);                
                return;
            }            
            
            if (proc.ExitCode == 0) Process.Start(temp_f);            
        }

        private void autodetect()
        {
            Process proc = new Process();
            String param = "-ss " + txt_seek.Text + " -i " + '\u0022' + item + '\u0022' + " -t 1 -vf cropdetect -f null -" + " -hide_banner";
            String crop = "";
            Boolean ok = false;
            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe");
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
            this.Enabled = false;
            test_cropping();
            this.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Enabled = false;            
            autodetect();            
            this.Enabled = true;
        }

        private void btn_frame_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            original_frame();
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileNameWithoutExtension(item) + ".jpg");            
            String param = "-ss " + txt_seek.Text + " -i " + '\u0022' + item + '\u0022' + " -frames:v 1 -y " + '\u0022' + temp_f + '\u0022' + " -hide_banner";

            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe");
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
         
            proc.WaitForExit();

            Image img;
            using (var bmpTemp = new Bitmap(temp_f))
            {
                img = new Bitmap(bmpTemp);
                pic1.Image = img;
            }
            try
            {
                File.Delete(temp_f);
            }

            catch { }
            int x, y, w, h = 0;
            x = (int)n_X.Value;
            y = (int)n_Y.Value;
            w = (int)(n_w.Maximum - n_w.Value);
            h = (int)(n_h.Maximum - n_h.Value);
            if (w == 0 || h == 0) return;
            Rectangle rect = new Rectangle(x, y, w, h);
            Bitmap bmp = new Bitmap(img);
            pic1.Image = cropAtRect(bmp, rect, x, y);
            this.Cursor = Cursors.Arrow;
        }

        private void original_frame()
        {
            Process proc = new Process();
            String temp_f = Path.Combine(Path.GetTempPath() + "FFBatch_test", Path.GetFileNameWithoutExtension(item) + ".jpg");
            String param = "-ss " + txt_seek.Text + " -i " + '\u0022' + item + '\u0022' + "  -frames:v 1 -y " + '\u0022' + temp_f + '\u0022'  + " -hide_banner";

            List<string> list_lines = new List<string>();

            proc.StartInfo.FileName = Path.Combine(Application.StartupPath, "ffmpeg.exe");
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
            Bitmap nb = new Bitmap(r.Width - x1, r.Height - y1);
            using (Graphics g = Graphics.FromImage(nb))
            {
                g.DrawImage(b, -r.X, -r.Y);
                return nb;
            }
        }

        private void btn_use_Click(object sender, EventArgs e)
        {
            if (n_w.Value == 0 && n_h.Value == 0 && n_X.Value == 0 && n_Y.Value == 0) vf_crop = String.Empty;
            else
            {
                vf_crop = " -vf crop=" + (n_w.Maximum - n_w.Value - n_X.Value).ToString() + ":" + (n_h.Maximum - n_h.Value - n_Y.Value).ToString() + ":" + n_X.Value.ToString() + ":" + n_Y.Value.ToString();
                Clipboard.SetText(vf_crop);
            }
            this.Close();
        }

        private void txt_seek_Leave(object sender, EventArgs e)
        {
            DateTime time2;
            if (!DateTime.TryParse(txt_seek.Text, out time2))
            {
                MessageBox.Show(FFBatch.Properties.Strings.time_bad, FFBatch.Properties.Strings.pre_input2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (TimeSpan.Parse(dur).TotalSeconds > 10) txt_seek.Text = "00:00:10";
                else txt_seek.Text = "00:00:01";                
                return;
            }
        }

        private void txt_seek_DoubleClick(object sender, EventArgs e)
        {
            if (TimeSpan.Parse(dur).TotalSeconds > 10) txt_seek.Text = "00:00:10";
            else txt_seek.Text = "00:00:01";
        }        

        private void crop_pic()
        {            
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
            Decimal r = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 6);
            Decimal r2 = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 3);
            Decimal r3 = Math.Round((n_w.Maximum - n_w.Value - n_X.Value) / (n_h.Maximum - n_h.Value - n_Y.Value), 2);

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
            lbl_prop.Text = (n_w.Maximum - n_w.Value - n_X.Value).ToString() + "x" + (n_h.Maximum - n_h.Value - n_Y.Value).ToString() + " / " + lbl_prop.Text;
        }

        private void txt_seek_TextChanged(object sender, EventArgs e)
        {
            DateTime time2;
            if (txt_seek.Text.Substring(txt_seek.Text.Length - 1, 1) == ".") return;
            if (DateTime.TryParse(txt_seek.Text, out time2))
            {
                if (TimeSpan.Parse(dur).TotalSeconds > TimeSpan.Parse(txt_seek.Text).TotalSeconds)
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
            n_w.Value = 0;
            n_h.Value = 0;
            n_X.Value = 0;
            n_Y.Value = 0;
        }

        private void Form28_Resize(object sender, EventArgs e)
        {            
            if (this.Width < 869) this.Width = 869;
            if (this.Height < 457) this.Height = 457;
            lbl_f.Width = this.Width - 57;
            groupBox1.Width = this.Width - (869 - 811);
            pic_0.Width = this.Width - (this.Width - 390) + (this.Width - 869) / 2;
            pic1.Width = this.Width - (this.Width - 390) + (this.Width - 869) / 2; 
            pic_0.Height = this.Height - (457 - 219);
            pic1.Height = this.Height - (457 - 219);
            pic1.Left = pic_0.Width + 52;
            lbl_or.Left = pic_0.Left + (pic_0.Width / 2) - (lbl_or.Width / 2);
            lbl_crop.Left = pic1.Left + (pic1.Width  / 2) - (lbl_crop.Width / 2);
            btn_use.Left = this.Width - btn_use.Width - 35;
            btn_cancel.Left = this.Width - btn_cancel.Width - btn_use.Width - 40;
            test_crop.Left = (this.Width / 2) - (test_crop.Width / 2) - 8;
            txt_seek.Left = test_crop.Left - txt_seek.Width - 8;
            n_secs.Left = test_crop.Left + test_crop.Width + 8;
            lbl_sec.Left = n_secs.Left + n_secs.Width + 3;
            lbl_prop.Left = (this.Width / 2) - (lbl_prop.Width / 2) - 3;

            btn_frame.Top = this.Height - (457 - 375);
            test_crop.Top = this.Height - (457 - 375);
            btn_cancel.Top = this.Height - (457 - 375);
            btn_use.Top = this.Height - (457 - 375);
            txt_seek.Top = this.Height - (457 - 381);
            n_secs.Top = this.Height - (457 - 381);
            lbl_sec.Top = this.Height - (457 - 384);


        }
    }
}
