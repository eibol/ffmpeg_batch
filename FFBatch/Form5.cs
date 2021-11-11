using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public Image pic_http = null;
        public String stream_n = String.Empty;
        public String lv1_item = String.Empty;
        public String dur_lv1 = String.Empty;
        public int Id = 0;
        public int current_fr = 0;

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

        private void Form5_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                dg_streams.BackgroundColor = Color.Gray;
                dg_streams.RowsDefaultCellStyle.BackColor = Color.Gray;
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                dg_streams.BackgroundColor = SystemColors.InactiveBorder;
                dg_streams.RowsDefaultCellStyle.BackColor = Color.White;
            }

            btn_close.Text = Properties.Strings.close_win;
            this.Text = FFBatch.Properties.Strings.multi_streams;
            dg_streams.Columns[2].HeaderText = FFBatch.Properties.Strings.str_ouput;
            this.Enabled = false;

            if (!lv1_item.ToLower().Contains("http")) pic_frame.Image = null;

            dg_streams.BackgroundColor = this.BackColor;
            dg_streams.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.RowHeadersVisible = false;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_streams.Columns[1].ReadOnly = true;
            dg_streams.Columns[2].ReadOnly = true;
            dg_streams.Rows.Clear();
            String filepath, name = "";


            if (!lv1_item.ToLower().Contains("http"))
            {
                filepath = lv1_item.Substring(0, lv1_item.LastIndexOf("\\"));
                name = lv1_item.Substring(filepath.Length + 1, lv1_item.Length - filepath.Length - 1);                
            }
            else
            {
                name = lv1_item;                
            }
            
            new System.Threading.Thread(() =>
            {
                this.InvokeEx(f => txt_file.Text = name);

                ff_str.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                ff_str.StartInfo.Arguments = " -i " + '\u0022' + lv1_item + '\u0022';
                ff_str.StartInfo.RedirectStandardOutput = true;
                ff_str.StartInfo.RedirectStandardError = true;
                ff_str.StartInfo.UseShellExecute = false;
                ff_str.StartInfo.CreateNoWindow = true;
                ff_str.EnableRaisingEvents = true;
                ff_str.Start();

                Form11 frm_prog = new Form11();
                frm_prog.Refresh();
                frm_prog.label1.Text = FFBatch.Properties.Strings.obt_strs;
                frm_prog.label1.Refresh();

                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;
                    frm_prog.ShowDialog();
                    frm_prog.Refresh();

                }).Start();
                frm_prog.procId = ff_str.Id;
                String stream = "";
                String sub_str = "";
                int f_streams = -1;
                Boolean has_stream = false;
                int img = 0;
                while (!ff_str.StandardError.EndOfStream)
                {
                    stream = ff_str.StandardError.ReadLine();

                    if (stream.Contains("Stream #0:"))
                    {
                        has_stream = true;
                        f_streams = f_streams + 1;

                        if (stream.Contains("Video")) img = 0;
                        if (stream.Contains("Audio")) img = 1;
                        if (stream.Contains("Subtitle")) img = 2;

                        if (stream.Substring(stream.IndexOf("#0:") + 4, 1) == "(")
                        {
                            if (stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(und)" || stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(unk)")
                            {

                                stream_n = stream_n + 1;
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 11);
                                this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 11), (stream.Length - sub_str.Length))));

                            }
                            else
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                                this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length))));

                            }
                        }
                        else
                        {
                            if (stream.Contains("Video"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                String to_add = stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length));
                                to_add = Regex.Replace(to_add, @"\([^()]*\)", string.Empty);
                                RegexOptions options = RegexOptions.None;
                                Regex regex = new Regex("[ ]{2,}", options);
                                to_add = regex.Replace(to_add, " ");

                                this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[0], "#0:" + f_streams.ToString(), to_add));
                            }
                            if (stream.Contains("Audio"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                String to_add = stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length));
                                to_add = Regex.Replace(to_add, @"\([^()]*\)", string.Empty);
                                RegexOptions options = RegexOptions.None;
                                Regex regex = new Regex("[ ]{2,}", options);
                                to_add = regex.Replace(to_add, " ");
                                this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[1], "#0:" + f_streams.ToString(), to_add));
                            }
                            if (stream.Contains("Subtitle"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[2], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length))));
                            }
                        }
                    }

                }
                this.InvokeEx(f => this.Enabled = true);
                ff_str.WaitForExit(10000);
                this.InvokeEx(f => this.Enabled = true);

                try
                {
                    frm_prog.Invoke(new MethodInvoker(delegate
                    {
                        frm_prog.Dispose();
                    }));
                }
                catch { }

                this.InvokeEx(f => this.Enabled = true);
                if (frm_prog.abort_validate == true)
                {
                    this.InvokeEx(f => this.Close());
                    return;
                }

                if (has_stream == false)
                {
                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[3], "0", FFBatch.Properties.Strings.no_str_f));
                }

                this.InvokeEx(f => dg_streams.ClearSelection());
                this.InvokeEx(f => dg_streams.CurrentCell = null);
                
                try
                {
                    frm_prog.Invoke(new MethodInvoker(delegate
                    {
                        frm_prog.Dispose();
                    }));
                }
                catch { }

                //Attempt to extract frame as image

           
                    DateTime time2;
                    //Double seconds = 0;
                    if (DateTime.TryParse(dur_lv1, out time2))
                    {
                    if (lv1_item.ToLower().Contains("http")) return;
                    TimeSpan t1 = TimeSpan.FromSeconds((current_fr));
                    String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t1.Hours,
                        t1.Minutes,
                        t1.Seconds);

                    this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);                  
                }                    

            }).Start();
            if (lv1_item.ToLower().Contains("http"))
            {
                pic_frame.Image = pic_http;                
                return;
            }
        }

        private void ct1_Click(object sender, EventArgs e)
        {
            if (dg_streams.SelectedCells.Count > 0)
            {
                Clipboard.SetText(dg_streams.SelectedCells[0].Value.ToString());
            }
        }

        private void menu_grid_Opening(object sender, CancelEventArgs e)
        {
            if (dg_streams.SelectedCells.Count > 1) e.Cancel = true;
            if (dg_streams.SelectedCells.Count == 0) e.Cancel = true;
            if (dg_streams.SelectedCells[0].ColumnIndex == 0) e.Cancel = true;

        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            RefreshResources(this, resources);
        }
        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void btn_10_Click(object sender, EventArgs e)
        {          
                try
                {
                    if (current_fr <= 4) current_fr = 5;
                    else if (current_fr == 5) current_fr = 10;
                    else current_fr = current_fr + 10;

                    DateTime time0 = new DateTime();
                    Double seconds1 = 0;
                    if (DateTime.TryParse(dur_lv1, out time0))
                    {
                        seconds1 = TimeSpan.Parse(dur_lv1).TotalSeconds;
                    }

                    Double t_to = (double)current_fr;
                    if (t_to > seconds1)
                    {
                        t_to = seconds1;
                        current_fr = (int)seconds1;
                    }
                    TimeSpan t1 = TimeSpan.FromSeconds((t_to));
                    String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t1.Hours,
                        t1.Minutes,
                        t1.Seconds);
                    this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                }
                catch
                {
                    
                }
        }

        private void btn_minus10_Click(object sender, EventArgs e)
        {
              try
                {
                    current_fr = current_fr - 10;
                    if (current_fr < 0) current_fr = 0;
                    Double t_to = (double)current_fr;
                    TimeSpan t1 = TimeSpan.FromSeconds((t_to));
                    String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t1.Hours,
                        t1.Minutes,
                        t1.Seconds);
                    this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);

                } catch {  }
        }

        private void btn_fr_start_Click(object sender, EventArgs e)
        {          
             lbl_fr_time.Text = "00:00:00";
        }

        private void btn_fr_end_Click(object sender, EventArgs e)
        {            
                    DateTime time0 = new DateTime();
                    
                    if (DateTime.TryParse(dur_lv1, out time0))
                    {
                        lbl_fr_time.Text = dur_lv1;
                    }
                    else lbl_fr_time.Text = "00:00:00";               
        }

        private void lbl_fr_time_TextChanged(object sender, EventArgs e)
        {
            if (lbl_fr_time.Text.Contains(".") || lbl_fr_time.Text.Contains(",")) return;
            if (lv1_item.ToLower().Contains("http")) return;
            
                DateTime time;
            if (!DateTime.TryParse(lbl_fr_time.Text, out time))
            {                
                return;
            }
            else
            {
                Double seconds = TimeSpan.Parse(lbl_fr_time.Text).TotalSeconds;
                Double seconds2 = TimeSpan.Parse(dur_lv1).TotalSeconds;
                if (seconds > seconds2)
                {
                    lbl_fr_time.Text = dur_lv1;
                }                
                get_frame();
            }           
        }

        private void get_frame()
        {
            //Attempt to extract frame as image         

            try
            {
                DateTime time2;
                Double seconds = 0;
                if (!DateTime.TryParse(lbl_fr_time.Text, out time2))
                {
                    return;
                }
                seconds = TimeSpan.Parse(lbl_fr_time.Text).TotalSeconds;

                Decimal time_frame = Math.Round((decimal)seconds, 0);
                current_fr = (int)time_frame;

                Process proc_img = new System.Diagnostics.Process();
                String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

                String file_img = Path.GetFullPath(lv1_item);
                String fullPath_img = file_img;
                String AppParam_img = "";

                String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
                String extracted_img = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_img) + "_480_" + time_frame.ToString() + "." + "jpg";


                if (File.Exists(extracted_img))
                {
                    this.InvokeEx(f => this.Width = 1116);
                    Image img_tmp1;
                    using (var bmpTemp = new Bitmap(extracted_img))
                    {
                        img_tmp1 = new Bitmap(bmpTemp);
                        pic_frame.Image = img_tmp1;
                        return;
                    }                
                }

                AppParam_img = " -ss " + time_frame + " -i " + '\u0022' + file_img + '\u0022' + " -qscale:v 2" + " -vf scale=480:-1" + " -f image2 -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_img) + "_480_" + time_frame.ToString() + "." + "jpg" + '\u0022';

                proc_img.StartInfo.RedirectStandardOutput = false;
                proc_img.StartInfo.RedirectStandardError = false;
                proc_img.StartInfo.UseShellExecute = true;
                proc_img.StartInfo.CreateNoWindow = false;
                proc_img.EnableRaisingEvents = false;
                proc_img.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                proc_img.StartInfo.FileName = ffm_img;
                proc_img.StartInfo.Arguments = AppParam_img;

                proc_img.Start();
                proc_img.WaitForExit(2000);

                this.InvokeEx(f => this.Width = 1116);
                Image img_tmp;
                using (var bmpTemp = new Bitmap(extracted_img))
                {
                    img_tmp = new Bitmap(bmpTemp);
                    pic_frame.Image = img_tmp;
                }
                    //End extract frame as image
                }
            catch { }
        }

        private void btn_min1_Click(object sender, EventArgs e)
        {
            try
            {
                current_fr = current_fr - 1;
                if (current_fr < 0) current_fr = 0;
                Double t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromSeconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    t1.Hours,
                    t1.Minutes,
                    t1.Seconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
            }
            catch { }
        }

        private void btn_plus1_Click(object sender, EventArgs e)
        {
            try
            {   
                current_fr = current_fr + 1;

                DateTime time0 = new DateTime();
                Double seconds1 = 0;
                if (DateTime.TryParse(dur_lv1, out time0))
                {
                    seconds1 = TimeSpan.Parse(dur_lv1).TotalSeconds;
                }

                Double t_to = (double)current_fr;
                if (t_to > seconds1)
                {
                    t_to = seconds1;
                    current_fr = (int)seconds1;
                }
                TimeSpan t1 = TimeSpan.FromSeconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                    t1.Hours,
                    t1.Minutes,
                    t1.Seconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
            }
            catch
            {

            }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            pic_frame.Image = null;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
            
    }

}
