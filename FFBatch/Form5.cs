using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

        private Boolean working = false;
        public Boolean refresh = false;
        Double elapsed = 0;
        private Boolean cancel_keyfr = false;
        public String trim_start = "00:00:00.000";
        public String trim_end = "0";
        public Boolean trim_st = false;
        public Boolean trim_e = false;
        private int total_time = 0;
        public Boolean already_scan = false;
        private Decimal frs = 0;
        private SortedList<String, String> pos = new SortedList<String, String>();
        private List<String> file_kf = new List<String>();
        private List<String> file_imgs = new List<String>();
        private Boolean save_ok = false;
        private Boolean copy_img = false;
        public Image pic_http = null;
        public String stream_n = String.Empty;
        public String lv1_item = String.Empty;
        public String dur_lv1 = String.Empty;
        public int Id = 0;
        public int current_fr = 0;
        Process proc_img_0 = new System.Diagnostics.Process();

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
            cancel_keyfr = false;
            lbl_prog.Text = Properties.Strings2.get_prev;
            btn_cancel.Text = Properties.Strings.cancel;
            btn_close.Text = Properties.Strings.close_win;
            this.Text = FFBatch.Properties.Strings.multi_streams;
            panel1.Parent = groupBox1;
            panel1.BackColor = Color.Transparent;
            ct1.Text = Properties.Strings.copy;
            ct_save.Text = Properties.Strings2.save_img;
            ct_copy.Text = Properties.Strings.copy;

            frs = get_frate();

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

            dg_streams.Columns[2].HeaderText = FFBatch.Properties.Strings.str_ouput;
            this.Enabled = false;

            if (!lv1_item.ToLower().Substring(0, 4).Contains("http")) pic_frame.Image = null;

            dg_streams.BackgroundColor = this.BackColor;
            dg_streams.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.RowHeadersVisible = false;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_streams.Columns[1].ReadOnly = true;
            dg_streams.Columns[2].ReadOnly = true;
            dg_streams.Rows.Clear();
            String filepath, name = "";

            if (!lv1_item.ToLower().Substring(0, 4).Contains("http"))
            {
                filepath = lv1_item.Substring(0, lv1_item.LastIndexOf("\\"));
                name = lv1_item.Substring(filepath.Length + 1, lv1_item.Length - filepath.Length - 1);
            }
            else
            {
                name = lv1_item;
            }
            txt_file.Text = name + " - " + dur_lv1;
            img_prog.Width = 0;
            lbl_fr_time.Text = "00:00:00.000";

            new System.Threading.Thread(() =>
            {

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
                    this.Invoke(new MethodInvoker(delegate
                    {
                        frm_prog.ShowDialog(this);
                        frm_prog.Refresh();
                    }));
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
            }).Start();

            if (lv1_item.ToLower().Substring(0, 4).Contains("http"))
            {
                pic_frame.Image = pic_http;
                return;
            }

            String ext_a0 = Path.GetExtension(lv1_item);
            if (ext_a0 == ".jpg" || ext_a0 == ".jpeg" || ext_a0 == ".png" || ext_a0 == ".gif" || ext_a0 == ".tif" || ext_a0 == ".bmp" || ext_a0 == ".ico")
            {
                try { pic_frame.Load(lv1_item); } catch { }
                panel1.Visible = false;
                return;
            }

            DateTime time2;

            if (DateTime.TryParse(dur_lv1, out time2))
            {
                if (lv1_item.ToLower().Substring(0, 4).Contains("http")) return;
                trim_end = dur_lv1;
                TimeSpan t1 = TimeSpan.FromMilliseconds((double)current_fr);
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);

                lbl_fr_time.Text = tx_1;
                get_frame();

                trackB.Minimum = 0;
                timer1.Enabled = true;
                BG_Keyframes.RunWorkerAsync();
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
                if (current_fr <= 4000) current_fr = 5000;
                else if (current_fr == 5000) current_fr = 10000;
                else current_fr = current_fr + 10000;
                if (current_fr % 1000 != 0) current_fr = Convert.ToInt32(Math.Ceiling((double)(current_fr / 1000) * 1000));

                DateTime time0 = new DateTime();
                Double seconds1 = 0;
                if (DateTime.TryParse(dur_lv1, out time0))
                {
                    seconds1 = TimeSpan.Parse(dur_lv1).TotalMilliseconds;
                }

                Double t_to = (double)current_fr;
                if (t_to > seconds1)
                {
                    t_to = seconds1;
                    current_fr = (int)seconds1;
                }
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                get_frame();
            }
            catch
            {
            }
        }

        private void btn_minus10_Click(object sender, EventArgs e)
        {
            try
            {
                current_fr = current_fr - 10000;
                if (current_fr < 0) current_fr = 0;
                if (current_fr % 1000 != 0) current_fr = Convert.ToInt32(Math.Floor((double)(current_fr / 1000) * 1000));
                Double t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                get_frame();
            }

            catch { }
        }

        private void btn_fr_start_Click(object sender, EventArgs e)
        {
            lbl_fr_time.Text = "00:00:00.000";
            current_fr = 0;
            get_frame();
            trackB.Value = trackB.Minimum;
        }

        private void btn_fr_end_Click(object sender, EventArgs e)
        {
            DateTime time0 = new DateTime();

            if (DateTime.TryParse(dur_lv1, out time0))
            {
                lbl_fr_time.Text = dur_lv1;
                current_fr = (int)TimeSpan.Parse(dur_lv1).TotalMilliseconds;
            }
            trackB.Value = trackB.Maximum;
            get_frame();

        }

        private void lbl_fr_time_TextChanged(object sender, EventArgs e)
        {
            Double seconds = 0;
            Double seconds2 = 0;
            if (lv1_item.ToLower().Substring(0, 4).Contains("http")) return;

            DateTime time2;
            if (DateTime.TryParse(lbl_fr_time.Text, out time2))
            {
                seconds = TimeSpan.Parse(lbl_fr_time.Text).TotalMilliseconds;
                seconds2 = TimeSpan.Parse(dur_lv1).TotalMilliseconds;
                current_fr = (int)seconds;

                if (seconds > seconds2)
                {
                    lbl_fr_time.Text = dur_lv1;
                }

                img_prog.Width = (int)(seconds * 480 / seconds2);
            }
        }
        private void get_frame()
        {
            //Attempt to extract frame as image

            DateTime time2;
            Double seconds = 0;
            if (!DateTime.TryParse(lbl_fr_time.Text, out time2))
            {
                return;
            }

            Double t_to = (double)current_fr;

            TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
            String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);
            String time_frame = tx_1;
            String repl_frm = time_frame.Replace(",", "").Replace(".", "").Replace(":", "");

            Process proc_img1 = new System.Diagnostics.Process();
            String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

            String file_img = Path.GetFullPath(lv1_item);
            String fullPath_img = file_img;
            String AppParam_img = "";

            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");
            String new_out = Path.GetFileNameWithoutExtension(file_img);
            String target_img = Path.GetTempPath() + "FFBatch_Test" + "\\" + new_out + "_480_" + rel_path + "_" + current_fr + "." + "jpg";

            if (File.Exists(target_img))
            {
                Image img_tmp1;
                using (var bmpTemp = new Bitmap(target_img))
                {
                    img_tmp1 = new Bitmap(bmpTemp);
                    pic_frame.Image = img_tmp1;
                    return;
                }
            }
            Boolean is_audio = is_audio_f();
            String ss = " -ss " + time_frame;
            if (is_audio == true) ss = "";
            AppParam_img = ss + " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 0" + " -vf scale=480:-1" + " -f image2 -y " + '\u0022' + target_img + '\u0022';

            proc_img1.StartInfo.RedirectStandardOutput = false;
            proc_img1.StartInfo.RedirectStandardError = false;
            proc_img1.StartInfo.UseShellExecute = true;
            proc_img1.StartInfo.CreateNoWindow = false;
            proc_img1.EnableRaisingEvents = false;
            proc_img1.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            proc_img1.StartInfo.FileName = ffm_img;
            proc_img1.StartInfo.Arguments = AppParam_img;
            proc_img1.Start();
            proc_img1.WaitForExit(5000);

            if (File.Exists(target_img))
            {
                Image img_tmp;
                using (var bmpTemp = new Bitmap(target_img))
                {
                    img_tmp = new Bitmap(bmpTemp);
                    pic_frame.Image = img_tmp;
                }
            }
            //End extract frame as image
        }

        private void btn_min1_Click(object sender, EventArgs e)
        {
            try
            {
                current_fr = current_fr - 100;
                if (current_fr < 0) current_fr = 0;

                Double t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                             (int)t1.TotalHours,
                             t1.Minutes,
                             t1.Seconds,
                             t1.Milliseconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                get_frame();
            }
            catch { }
        }

        private void btn_plus1_Click(object sender, EventArgs e)
        {
            try
            {
                current_fr = current_fr + 100;

                DateTime time0 = new DateTime();
                Double miliseconds1 = 0;
                if (DateTime.TryParse(dur_lv1, out time0))
                {
                    miliseconds1 = TimeSpan.Parse(dur_lv1).TotalMilliseconds;
                }

                Double t_to = (double)current_fr;
                if (t_to > miliseconds1)
                {
                    t_to = miliseconds1;
                    current_fr = (int)miliseconds1;
                }

                t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                             (int)t1.TotalHours,
                             t1.Minutes,
                             t1.Seconds,
                             t1.Milliseconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                get_frame();
            }
            catch { }
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working == true)
            {
                btn_cancel.PerformClick();
                working = false;
                e.Cancel = true;
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private Boolean is_audio_f()
        {
            String[] audios = new string[] { "alac", "flac", "aac", "ac3", "mp3", "oga", "mka", "opus", "wav", "ape", "m4a", "eac3", "wma", "dsd", "wave", "aiff" };
            Boolean is_audio = false;
            foreach (String str in audios)
            {
                if (lv1_item.ToLower().Substring(lv1_item.Length - 3, 3).Contains(str) || lv1_item.ToLower().Substring(lv1_item.Length - 4, 4).Contains(str))
                {
                    is_audio = true;
                    break;
                }
            }
            if (is_audio == true) return true;
            else return false;
        }

        private void save_copy_img()
        {
            Boolean is_audio = is_audio_f();

            if (txt_file.Text.ToLower().Substring(0, 4).Contains("http") || is_audio == true)
            {
                if (copy_img == false)
                {
                    save_img.Filter = Properties.Strings2.imgs + " PNG|*.png|" + Properties.Strings2.imgs + " JPEG |*.jpg";
                    save_img.ShowDialog();
                    if (save_ok == false) return;
                    save_ok = false;
                    try
                    {
                        if (Path.GetExtension(save_img.FileName) == ".png") pic_frame.Image.Save(save_img.FileName, ImageFormat.Png);
                        else pic_frame.Image.Save(save_img.FileName, ImageFormat.Jpeg);
                        MessageBox.Show(Properties.Strings2.saved_img);
                    }
                    catch
                    {
                        MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

                    var random = new Random();
                    var randomString = new string(Enumerable.Repeat(chars, 6)
                                                            .Select(s => s[random.Next(s.Length)]).ToArray());

                    try
                    {
                        String temp = Path.GetTempPath() + "FFBatch_Test" + "\\" + randomString + ".jpg";
                        pic_frame.Image.Save(temp, ImageFormat.Jpeg);
                        Image img;
                        using (var bmpTemp = new Bitmap(temp))
                        {
                            img = new Bitmap(bmpTemp);
                        }
                        Clipboard.SetImage(img);
                        MessageBox.Show(Properties.Strings2.img_copied);
                    }
                    catch { MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                copy_img = false;
                return;
            }

            save_ok = false;
            DateTime time2;
            Double seconds = 0;
            if (!DateTime.TryParse(lbl_fr_time.Text, out time2))
            {
                return;
            }
            seconds = TimeSpan.Parse(lbl_fr_time.Text).TotalMilliseconds;

            TimeSpan t = TimeSpan.FromMilliseconds(seconds);
            String dur = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t.TotalHours,
                         t.Minutes,
                         t.Seconds,
                         t.Milliseconds);
            String time_frame = dur;

            Process proc_img = new System.Diagnostics.Process();
            String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

            String file_img = Path.GetFullPath(lv1_item);
            String fullPath_img = file_img;
            String AppParam_img = "";

            String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");
            String target_img = Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img) + "_480_" + rel_path + "_" + current_fr + "." + "jpg";

            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");

            if (copy_img == false)
            {
                save_img.Filter = Properties.Strings2.imgs + " PNG|*.png|" + Properties.Strings2.imgs + " JPEG |*.jpg";
                String img_Ext = "png";
                if (Path.GetExtension(save_img.FileName) == ".jpg") img_Ext = "jpg";
                save_img.ShowDialog();
                if (save_ok == false) return;
                save_ok = false;
                target_img = save_img.FileName;
            }

            AppParam_img = " -ss " + time_frame + " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 0" + " -f image2 -y " + '\u0022' + target_img + '\u0022';

            proc_img.StartInfo.RedirectStandardOutput = false;
            proc_img.StartInfo.RedirectStandardError = false;
            proc_img.StartInfo.UseShellExecute = true;
            proc_img.StartInfo.CreateNoWindow = false;
            proc_img.EnableRaisingEvents = false;
            proc_img.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            proc_img.StartInfo.FileName = ffm_img;
            proc_img.StartInfo.Arguments = AppParam_img;
            try
            {
                proc_img.Start();
                proc_img.WaitForExit();
                if (proc_img.ExitCode == 1 || proc_img.ExitCode == 0)
                {
                    if (copy_img == true)
                    {
                        Bitmap imageToAdd = new Bitmap(target_img);
                        Clipboard.SetImage(imageToAdd);
                        MessageBox.Show(Properties.Strings2.img_copied);
                    }
                    else MessageBox.Show(Properties.Strings2.saved_img);

                    copy_img = false;
                }
                else
                {
                    MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void get_fullscr()
        {
            DateTime time2;
            Double seconds = 0;
            if (!DateTime.TryParse(lbl_fr_time.Text, out time2))
            {
                return;
            }
            seconds = TimeSpan.Parse(lbl_fr_time.Text).TotalMilliseconds;

            TimeSpan t = TimeSpan.FromMilliseconds(seconds);
            String dur = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t.TotalHours,
                         t.Minutes,
                         t.Seconds,
                         t.Milliseconds);
            String time_frame = dur;

            Process proc_img = new System.Diagnostics.Process();
            String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

            String file_img = Path.GetFullPath(lv1_item);
            String fullPath_img = file_img;
            String AppParam_img = "";

            String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");
            String target_img = Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img) + "_full_" + rel_path + "_" + current_fr + "." + "jpg";

            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            Boolean is_audio = is_audio_f();
            if (is_audio == false) AppParam_img = " -ss " + time_frame + " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 0" + " -f image2 -y " + '\u0022' + target_img + '\u0022';
            else AppParam_img = " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 0" + " -f image2 -y " + '\u0022' + target_img + '\u0022';

            proc_img.StartInfo.RedirectStandardOutput = false;
            proc_img.StartInfo.RedirectStandardError = false;
            proc_img.StartInfo.UseShellExecute = true;
            proc_img.StartInfo.CreateNoWindow = false;
            proc_img.EnableRaisingEvents = false;
            proc_img.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            proc_img.StartInfo.FileName = ffm_img;
            proc_img.StartInfo.Arguments = AppParam_img;
            try
            {
                proc_img.Start();
                proc_img.WaitForExit();
                if (proc_img.ExitCode == 1 || proc_img.ExitCode == 0)
                {
                        Bitmap imageToAdd = new Bitmap(target_img);
                        Clipboard.SetImage(imageToAdd);
                }
                else
                {
                    MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ct_save_Click(object sender, EventArgs e)
        {
            copy_img = false;
            save_copy_img();
        }

        private void ct_copy_Click(object sender, EventArgs e)
        {
            copy_img = true;
            save_copy_img();
        }

        private void save_img_FileOk(object sender, CancelEventArgs e)
        {
            save_ok = true;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            ct_save.PerformClick();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            ct_copy.PerformClick();
        }

        private void trackB_ValueChanged(object sender, EventArgs e)
        {
            if (cancel_keyfr == false)
            {
                lbl_fr_time.Text = pos.Keys[trackB.Value];
                try { pic_frame.Load(pos.Values[trackB.Value]); } catch { }
                current_fr = (int)TimeSpan.Parse(lbl_fr_time.Text).TotalMilliseconds;
            }
            if (trackB.Value == trackB.Maximum)
            {
                current_fr = (int)TimeSpan.Parse(dur_lv1).TotalMilliseconds;
                Double t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                             (int)t1.TotalHours,
                             t1.Minutes,
                             t1.Seconds,
                             t1.Milliseconds);
                this.InvokeEx(f => f.lbl_fr_time.Text = tx_1);
                get_frame();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Task t = Task.Run(() =>
            {
                int pre_prog =  pg1.Value;

                String pattern = "*.jpg*";
                String path = Path.Combine(Path.GetTempPath(), "FFBatch_test") + "\\" + Path.GetFileNameWithoutExtension(lv1_item);
                var dirInfo = new DirectoryInfo(path);
                if (dirInfo.GetFiles(pattern).Count() == 0) return;
                var file = (from f in dirInfo.GetFiles(pattern) orderby f.LastWriteTime descending select f).First();
                String time_pos = get_time_pos(file.FullName);

                Double prog = 0;
                DateTime time2;
                total_time++;

                if (DateTime.TryParse(dur_lv1, out time2))
                {
                    prog = TimeSpan.Parse(time_pos).TotalSeconds;
                }

                try { pic_frame.Load(file.FullName); } catch { }

                this.Invoke(new MethodInvoker(delegate
                {
                    lbl_fr_time.Text = time_pos;
                }));

                pg1.Invoke(new MethodInvoker(delegate
                {
                if (prog <= pg1.Maximum)
                {
                    pg1.Value = (int)prog;
                        Double speed = pg1.Value - pre_prog;
                        Double remain = pg1.Maximum - pg1.Value;
                        if (total_time > 0 && total_time < 6) elapsed = Math.Round(remain / speed, 0);
                        else elapsed = elapsed - 1;
                        if (total_time > 5)
                        {
                            pg1.Text = elapsed.ToString() + " " + Properties.Strings.seconds;
                            if (elapsed <= 0) pg1.Text = Properties.Strings.about_finish;
                        }
                    }
                else pg1.Value = pg1.Maximum;
                if (pg1.Value == pg1.Maximum) btn_cancel.PerformClick();
                }));

            });
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel_keyfr = true;
            try
            {
                proc_img_0.Kill();
            }
            catch { }
            panel1.Visible = false;
            timer1.Enabled = false;
            btn_refresh.Enabled = true;
        }

        private Decimal get_frate()
        {
            Decimal frames = 0;
            String ff_frames = String.Empty;
            Process get_frames = new Process();
            get_frames.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "MediaInfo.exe");
            String ffprobe_frames = " " + '\u0022' + "--Inform=Video;%FrameRate%" + '\u0022';
            get_frames.StartInfo.Arguments = ffprobe_frames + " " + '\u0022' + lv1_item + '\u0022';
            get_frames.StartInfo.RedirectStandardOutput = true;
            get_frames.StartInfo.RedirectStandardError = true;
            get_frames.StartInfo.UseShellExecute = false;
            get_frames.StartInfo.CreateNoWindow = true;
            get_frames.EnableRaisingEvents = true;
            get_frames.Start();

            ff_frames = get_frames.StandardOutput.ReadLine();
            get_frames.WaitForExit();
            String fade_string = String.Empty;
            if (get_frames.ExitCode == 0)
            {
                if (ff_frames != null)
                {
                    try
                    {
                        frames = decimal.Parse(ff_frames) / 1000;
                        fade_string = frames.ToString().Replace(",", ".");
                    }
                    catch
                    {
                        frames = 0;
                    }

                }
                else
                {
                    frames = 0;
                }
            }
            return frames;
        }

        private String get_time_pos(String str)
        {
            int str_l = str.Length;
            int str_end = str.LastIndexOf("_");
            int ext_end = str.LastIndexOf(".");
            String final_str = str.Substring(str_end + 1, str_l - str_end - 1 - (str_l - ext_end));
            int outi = 0;

            if (int.TryParse(final_str, out outi))
            {
                outi = int.Parse(final_str);

                Decimal position = 0;
                String tx_1 = "";

                if (outi > 0) position = Math.Round(outi * (1 / frs), 3);
                if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".") position = position / 1000;

                TimeSpan t1 = TimeSpan.FromSeconds((double)position);
                tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds);
                return tx_1;
            }
            return "";
        }

        private void sort_frames2()
        {
            pos.Clear();
            file_kf.Clear();

            String destino0 = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String vstats_name = Regex.Replace("vstats_" + Path.GetFileNameWithoutExtension(lv1_item), @"[^\u0000-\u007F]+", string.Empty);
            String target_log = destino0 + "\\" + vstats_name + ".log";
            String it = "";
            Double test = 0;

            if (File.Exists(target_log))
            {
                foreach (String kf in File.ReadLines(target_log))
                {
                    try
                    {
                     int s = kf.IndexOf("time= ");
                     int e = kf.IndexOf(" br=");
                     it = kf.Substring(s + 6, e - s - 6);
                    }
                    catch { it = ""; }

                    test = 0;

                    if (Double.TryParse(it, out test))
                    {
                        if (CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ".") test = Double.Parse(it) * 1000;
                        else test = Double.Parse(it);
                        file_kf.Add(test.ToString());
                    }
                    else file_kf.Add("00:00:00.000");
                }
            }

            String file_img = Path.GetFullPath(lv1_item);
            String path = Path.Combine(Path.GetTempPath(), "FFBatch_test") + "\\" + Path.GetFileNameWithoutExtension(file_img);
            String fullPath_img = file_img;
            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");

            if (Directory.Exists(path))
            {
                foreach (string str in Directory.GetFiles(path))
                {
                    file_imgs.Add(str);
                }

                DirectoryInfo di = new DirectoryInfo(path);
                FileSystemInfo[] files = di.GetFileSystemInfos();
                var file_imgs2 = files.OrderBy(f => f.CreationTime).ToList();


                for (int i = 0; i < file_kf.Count; i++)
                {
                    try
                    {
                        Double time = Convert.ToDouble(file_kf[i]);
                        TimeSpan t1 = TimeSpan.FromMilliseconds(time);
                        String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                                         (int)t1.TotalHours,
                                         t1.Minutes,
                                         t1.Seconds,
                                         t1.Milliseconds);
                        pos.Add(tx_1, path + "\\" + file_imgs2[i].ToString());
                    }
                    catch { }
                }
                pos.Add(dur_lv1, null);
            }

            else timer1.Stop();
            this.InvokeEx(f => f.trackB.Maximum = pos.Count - 1);
            this.InvokeEx(f => f.trackB.Value = trackB.Minimum);
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            refresh = true;
            this.Close();
        }

        private void trim_left_Click(object sender, EventArgs e)
        {
            trim_st = true;
            trim_start = lbl_fr_time.Text;
            if (TimeSpan.Parse(trim_start).TotalMilliseconds >= TimeSpan.Parse(trim_end).TotalMilliseconds)
            {
                MessageBox.Show(Properties.Strings.total_time + " " + Properties.Strings.error);
                trim_start = "00:00:00.000";
                trim_st = false;
            }
            else
            {
                Double total_trim = TimeSpan.Parse(trim_end).TotalMilliseconds - TimeSpan.Parse(trim_start).TotalMilliseconds;
                TimeSpan t1 = TimeSpan.FromMilliseconds(total_trim);
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);
                lbl_start.Text = "[[  " + trim_start;
                lbl_end.Text = trim_end + " ]]";
                lbl_lapse.Text = "[[  " + tx_1 + "  ]]";
            }
        }

        private void trim_right_Click(object sender, EventArgs e)
        {
            trim_e = true;
            trim_end = lbl_fr_time.Text;
            if (TimeSpan.Parse(trim_start).TotalMilliseconds >= TimeSpan.Parse(trim_end).TotalMilliseconds)
            {
                MessageBox.Show(Properties.Strings.total_time + " " + Properties.Strings.error);
                trim_end = dur_lv1;
                trim_e = false;
            }

            else
            {
                Double total_trim = TimeSpan.Parse(trim_end).TotalMilliseconds - TimeSpan.Parse(trim_start).TotalMilliseconds;
                TimeSpan t1 = TimeSpan.FromMilliseconds(total_trim);
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                         (int)t1.TotalHours,
                         t1.Minutes,
                         t1.Seconds,
                         t1.Milliseconds);

                lbl_start.Text = "[[  " + trim_start;
                lbl_end.Text = trim_end + "  ]]";
                lbl_lapse.Text = "[[  " +  tx_1 + "  ]]";
            }
        }

        private void BG_Keyframes_DoWork(object sender, DoWorkEventArgs e)
        {
            elapsed = 0;

                DateTime time2;
                Double seconds = 0;
                if (!DateTime.TryParse(lbl_fr_time.Text, out time2)) return;
                working = true;
            try
            {

                this.Invoke(new MethodInvoker(delegate
                {
                    pg1.Maximum = (int)TimeSpan.Parse(dur_lv1).TotalSeconds;
                    btn_refresh.Enabled = false;
                }));


                String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

                String file_img = Path.GetFullPath(lv1_item);
                String fullPath_img = file_img;
                String AppParam_img = "";

                String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
                String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");
                String out1 = Path.GetFileNameWithoutExtension(file_img);
                if (out1.Length > 12) out1 = out1.Substring(0, 8);
                String target_img = Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img) + "\\" + out1 + "_480_";
                if (!Directory.Exists(Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img)))
                    try
                    {
                        Directory.CreateDirectory(Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img));
                    }
                    catch { return; }

                String vstats_name = Regex.Replace("vstats_" + Path.GetFileNameWithoutExtension(lv1_item), @"[^\u0000-\u007F]+", string.Empty);
                String target_log = destino + "\\" + vstats_name + ".log";


                AppParam_img = "-skip_frame nokey" + " -i " + "" + '\u0022' + file_img + '\u0022' + " -vsync 0 " + " -vf scale=480:-1 -frame_pts 1 -q:v 5 -y " + '\u0022' + target_img + "_" + "%d" + ".jpg" + '\u0022' + " -vstats -vstats_file " + '\u0022' + target_log + '\u0022';

                proc_img_0.StartInfo.RedirectStandardOutput = false;
                proc_img_0.StartInfo.RedirectStandardError = false;
                proc_img_0.StartInfo.CreateNoWindow = true;
                proc_img_0.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc_img_0.StartInfo.FileName = ffm_img;
                proc_img_0.StartInfo.Arguments = AppParam_img;

                if (already_scan == false)
                {
                    proc_img_0.Start();
                    proc_img_0.WaitForExit();

                    if (proc_img_0.ExitCode == 0)
                    {
                        already_scan = true;
                    }
                    else
                    {
                        already_scan = false;
                        if (cancel_keyfr == true)
                        {
                            MessageBox.Show(Properties.Strings2.kf_no_a, Properties.Strings.cancelled);
                            cancel_keyfr = false;
                        }
                        else MessageBox.Show(Properties.Strings.error + ". " + Properties.Strings2.kf_no_a, Properties.Strings.error);
                    }
                }
            } catch { }

        }

        private void BG_Keyframes_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
                working = false;
                pg1.Value = pg1.Maximum;
                timer1.Enabled = false;
                panel1.Visible = false;
                btn_close.Enabled = true;
                trackB.Enabled = true;
                btn_kplus1.Enabled = true;
                btn_k_m1.Enabled = true;
                trackB.Value = trackB.Minimum;

            sort_frames2();
            try
            {
                lbl_fr_time.Text = pos.Keys[0];
                trackB.Value = 0;
                pic_frame.Load(pos.Values[0]);
            } catch { }
        }

        private void btn_kplus1_Click(object sender, EventArgs e)
        {
            if (trackB.Value < trackB.Maximum) trackB.Value++;
        }

        private void btn_k_m1_Click(object sender, EventArgs e)
        {
            if (trackB.Value > trackB.Minimum) trackB.Value--;
        }

        private void pic_frame_DoubleClick(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            get_fullscr();
            Form6 frm6 = new Form6();
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            try
            {
                frm6.Top = 0; frm6.Left = 0;
                frm6.Width = resolution.Width;
                frm6.Height = resolution.Height;
                frm6.pic1.Image = Clipboard.GetImage();
                frm6.pic1.SizeMode = PictureBoxSizeMode.Zoom;
                int width = 0;
                int height = 0;
                if (resolution.Width >= frm6.pic1.Image.Width)
                {
                    width = (resolution.Width - frm6.pic1.Image.Width) / 2;
                    frm6.pic1.SizeMode = PictureBoxSizeMode.Normal;
                }
                if (resolution.Height >= frm6.pic1.Image.Height)
                {
                    height = (resolution.Height - frm6.pic1.Image.Height) / 2;
                    frm6.pic1.SizeMode = PictureBoxSizeMode.Normal;
                }
                frm6.pic1.Top = height; frm6.pic1.Left = width;
                frm6.pic1.Width = resolution.Width;
                frm6.pic1.Height = resolution.Height;
                this.Cursor = Cursors.Arrow;
                frm6.ShowDialog();
            }
            catch { }
        }
    }
}