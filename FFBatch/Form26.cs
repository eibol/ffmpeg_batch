using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form26 : Form
    {
        public Form26()
        {
            InitializeComponent();
        }

        public int top = 0;
        public int left = 0;
        public Boolean no_pop = false;
        public String out_file = "";
        public String dur_file = "";
        public Image pic_img = null;
        public String tot_bit = "";
        public String out_size = "";
        private Boolean is_portable = false;
        private String port_path = Application.StartupPath + "\\" + "settings" + "\\";
        CultureInfo ci = System.Threading.Thread.CurrentThread.CurrentUICulture;        

        private void Form26_Load(object sender, EventArgs e)
        {            
            if (Owner != null) Location = new Point(Owner.Location.X + Owner.Width / 2 - Width / 2,
                    Owner.Location.Y + Owner.Height / 2 - Height / 2);

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

            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;

            String no_out_pop = "";
            if (is_portable == false)
            {
                no_out_pop = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_pop_up.ini";
            }
            else
            {
                no_out_pop = port_path + "ff_pop_up_portable.ini";
            }
            if (File.Exists(no_out_pop))
            {
                chk_no_popp.Checked = true;
                no_pop = true;
            }
            else
            {
                chk_no_popp.Checked = false;
                no_pop = false;
            }

            this.Text = Properties.Strings2.out_file;
                btn_close.Text = Properties.Strings.close_win;
                btn_browse.Text = Properties.Strings2.browse;
                chk_no_popp.Text = Properties.Strings2.not_display;
                btn_play.Text = Properties.Strings.Play_file;
                int current_fr = 4000;
                Process proc_img = new System.Diagnostics.Process();
                
                l_v.Text = Properties.Strings2.video + ":";
                l_s.Text = Properties.Strings.resolution + ":";
                l_a.Text = Properties.Strings2.audio + ":";
                l_tb.Text = Properties.Strings2.bitrate + ":";
                l_size.Text = Properties.Strings.size + ":";
                l_dur.Text = Properties.Strings.duration + ":";

            String sep_th = ci.NumberFormat.CurrencyGroupSeparator;

            //Attempt to extract frame as image
                String dur_lv1 = "00:00:00.000";
                String lv1_item = out_file;
                pic_frame.Image = pic_img;            

                if (!File.Exists(lv1_item)) return;
                lbl_name.Text = Path.GetFileName(out_file);
                lbl_succ.Text = Properties.Strings.success;
                //pic_success.Left = lbl_succ.Left + lbl_succ.Width + 5;
                if (tot_bit != null && tot_bit.Length > 3) lbl_gb_th.Text = tot_bit.Substring(tot_bit.LastIndexOf(":") + 2, tot_bit.Length - tot_bit.LastIndexOf(":") - 2);
                lbl_size.Text = out_size.Substring(out_size.LastIndexOf(":") + 2, out_size.Length - out_size.LastIndexOf(":") - 2);

                if (!Directory.Exists(Path.Combine(Path.GetTempPath(), "FFBatch_Test"))) Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "FFBatch_Test"));

                DateTime time2;
            if (DateTime.TryParse(dur_file, out time2))
            {
                Double t_to1 = TimeSpan.Parse(dur_file).TotalSeconds;
                TimeSpan t11 = TimeSpan.FromSeconds((t_to1));
                String dur = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                             (int)t11.TotalHours,
                             t11.Minutes,
                             t11.Seconds);
                lbl_dur.Text = dur;
                current_fr = (int)TimeSpan.Parse(dur_file).TotalMilliseconds / 2;                
            }
            else current_fr = 0;    
                if (current_fr < 1) current_fr = 1;

                String time_frame = "";
                Double t_to = (double)current_fr;
                TimeSpan t1 = TimeSpan.FromMilliseconds((t_to));
                String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                             (int)t1.TotalHours,
                             t1.Minutes,
                             t1.Seconds,
                             t1.Milliseconds);
                time_frame = tx_1;
                String repl_frm = time_frame.Replace(",", "").Replace(".", "").Replace(":", "");

                String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");                

                String file_img = Path.GetFullPath(lv1_item);
                String fullPath_img = file_img;
                String AppParam_img = "";
                String config_info = Path.Combine(Path.GetTempPath(), "FFBatch_Test") + "\\" + "mediaconfig.txt";
                String rel_path = Path.GetDirectoryName(file_img).Replace(":", "_").Replace("\\", "_") + Path.GetExtension(file_img).Replace(".", "_");
                String target_img = Path.GetTempPath() + "FFBatch_Test" + "\\" + Path.GetFileNameWithoutExtension(file_img) + "_480_" + rel_path + "_" + repl_frm + "." + "jpg";                

                String m_info_path = System.IO.Path.Combine(Application.StartupPath, "MediaInfo.exe");

                Task t = Task.Run(() =>
                {
                    List<String> lines_ouput = new List<String>();

                    Process get_frames = new Process();

                    this.Invoke(new MethodInvoker(delegate
                    {
                        try
                        {
                            String[] config_lines = new String[3];
                            config_lines[0] = "Video;%Format%" + "\\n" + "%Width%" + "\\n" + "%Height%" + "\\n" + "%DisplayAspectRatio/String%" + "\\n" + "%BitRate/String%" + "\\n";
                            config_lines[1] = "Audio;%Format%" + "\\n" + "%BitRate/String%" + "\\n";
                            config_lines[2] = "General;%OverallBitRate/String%" + "\\n";

                            File.WriteAllLines(config_info, config_lines);
                            get_frames.StartInfo.FileName = m_info_path;

                            String ffprobe_frames = " " + "--Inform=file://" + config_info;
                            get_frames.StartInfo.Arguments = ffprobe_frames + " " + '\u0022' + out_file + '\u0022';
                            get_frames.StartInfo.RedirectStandardOutput = true;
                            get_frames.StartInfo.UseShellExecute = false;
                            get_frames.StartInfo.CreateNoWindow = true;
                            get_frames.EnableRaisingEvents = true;

                            get_frames.Start();

                            while (!get_frames.StandardOutput.EndOfStream)
                            {
                                lines_ouput.Add(get_frames.StandardOutput.ReadLine());
                            }

                            get_frames.WaitForExit();
                            
                            if (get_frames.ExitCode == 0)
                            {
                                if (lines_ouput[1].ToLower().Contains("visual")) lines_ouput[1] = lines_ouput[1].Replace("Visual", "");
                                try {  lbl_v_th.Text = lines_ouput[1].Replace("Video", "").Replace("AVC", "h264").ToLower() + " " + "(" + lines_ouput[5] + ")";                         
                                    
                                } catch {  }

                                try { lbl_s_th.Text = lines_ouput[2] + "x" + lines_ouput[3] + " (" + lines_ouput[4] + ")";  } catch { }

                                try { lbl_a_th.Text = lines_ouput[6].ToLower() + " (" + lines_ouput[7] + ")"; } 
                                catch {lbl_a_th.Text = lines_ouput[1].ToLower() + " (" + lines_ouput[2].ToLower() + ")";

                                    lbl_s_th.Text = "-";
                                    lbl_v_th.Text = "-";                                
                                }                            
                                
                                String ext_a0 = Path.GetExtension(file_img);

                                if (ext_a0 == ".flac" || ext_a0 == ".mp3" || ext_a0 == ".oga" || ext_a0 == ".ape" || ext_a0 == ".wav" || ext_a0 == ".aac" || ext_a0 == ".ac3" || ext_a0 == ".mpa" || ext_a0 == ".mka" || ext_a0 == ".wave" || ext_a0 == ".mp1" || ext_a0 == ".dsd" || ext_a0 == ".wma" || ext_a0 == ".amr" || ext_a0 == ".opus")
                                {
                                    lbl_a_th.Text = lines_ouput[1].ToLower() + " (" + lines_ouput[2].ToLower() + ")";                                   
                                }
                                
                                if (lbl_v_th.Text.Contains("()")) lbl_v_th.Text = lbl_v_th.Text.Replace("()", "(vbr)");
                                if (lbl_a_th.Text.Contains("()")) lbl_a_th.Text = lbl_a_th.Text.Replace("()", "(vbr)");
                                lbl_v_th.Text = Regex.Replace(lbl_v_th.Text, @"(?<=\d)\p{Zs}(?=\d)", sep_th);
                                lbl_a_th.Text = Regex.Replace(lbl_a_th.Text, @"(?<=\d)\p{Zs}(?=\d)", sep_th);
                            }
                            else
                            {                                
                                lbl_v_th.Text = "-";
                                lbl_a_th.Text = "-";                                
                            }                           

                        }
                        catch
                        {                            
                            lbl_v_th.Text = "-";
                            lbl_a_th.Text = "-";
                        }                        

                    }));

                    //End Get Info                   

                    AppParam_img = " -ss " + time_frame + " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 0" + " -vf scale=480:-1" + " -f image2 -y " + '\u0022' + target_img + '\u0022';

                    String ext_a = Path.GetExtension(file_img);
                    if (ext_a == ".flac" || ext_a == ".mp3") AppParam_img = " -i " + "" + '\u0022' + file_img + '\u0022' + " -f image2 -y " + '\u0022' + target_img + '\u0022';

                    proc_img.StartInfo.RedirectStandardOutput = false;
                    proc_img.StartInfo.RedirectStandardError = false;
                    proc_img.StartInfo.UseShellExecute = false;
                    proc_img.StartInfo.CreateNoWindow = true;
                    proc_img.EnableRaisingEvents = false;
                    proc_img.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    proc_img.StartInfo.FileName = ffm_img;
                    proc_img.StartInfo.Arguments = AppParam_img;

                    pic_frame.Image = pic_img;

                    proc_img.Start();
                    proc_img.WaitForExit(5000);

                    if (File.Exists(target_img))
                    {
                        Image img_tmp;
                        using (var bmpTemp = new Bitmap(target_img))
                        {
                            img_tmp = new Bitmap(bmpTemp);
                            try
                            {
                                pic_frame.Invoke(new MethodInvoker(delegate
                                {
                                    try
                                    {
                                        pic_frame.Image = img_tmp;
                                        panel_thumb.Refresh();
                                    }
                                    catch
                                    {
                                        pic_frame.Image = pic_img;
                                        panel_thumb.Refresh();
                                    }
                                }));
                            } catch { }
                        }
                    }
                    else
                    {
                        String ext_a0 = Path.GetExtension(file_img);

                        if (ext_a0 == ".flac" || ext_a0 == ".mp3" || ext_a0 == ".oga" || ext_a0 == ".ape" || ext_a0 == ".wav" || ext_a0 == ".aac" || ext_a0 == ".ac3" || ext_a0 == ".mpa" || ext_a0 == ".mka" || ext_a0 == ".wave" || ext_a0 == ".mp1" || ext_a0 == ".dsd" || ext_a0 == ".wma" || ext_a0 == ".amr" || ext_a0 == ".opus" || ext_a0 == ".eac3" || ext_a0 == ".alac" || ext_a0 == ".m4a")
                        {
                            pic_frame.Invoke(new MethodInvoker(delegate
                            {
                                try
                                {
                                    //if (lbl_a_th.Text != ext_a0.Replace(".", "")) lbl_v_th.Text = ext_a0.Replace(".", "");
                                    //else lbl_v_th.Text = "Audio";                                    
                                    PictureBox pic2 = new PictureBox();
                                    pic2.Image = pic_frame.ErrorImage;
                                    pic_frame.Image = pic2.Image;
                                }
                                catch
                                {
                                    pic_frame.Invoke(new MethodInvoker(delegate
                                    {
                                        pic_frame.Image = pic_img;
                                    }));
                                }
                            }));
                        }

                        if (ext_a0 == ".jpg" || ext_a0 == ".jpeg" || ext_a0 == ".png" || ext_a0 == ".gif" || ext_a0 == ".tif" || ext_a0 == ".bmp" || ext_a0 == ".ico" || ext_a0 == ".bmp")
                        {
                            pic_frame.Invoke(new MethodInvoker(delegate
                            {
                                try
                                {
                                    PictureBox pic2 = new PictureBox();
                                    pic2.Image = new Bitmap(file_img);
                                    pic_frame.Image = pic2.Image;
                                }
                                catch
                                {
                                    pic_frame.Invoke(new MethodInvoker(delegate
                                    {
                                        pic_frame.Image = pic_img;
                                    }));
                                }
                            }));

                            proc_img.StartInfo.Arguments = "--Inform=Image;%Width%x%Height%" + " " + '\u0022' + out_file + '\u0022';
                            proc_img.StartInfo.FileName = m_info_path;

                            proc_img.StartInfo.RedirectStandardOutput = true;
                            proc_img.StartInfo.RedirectStandardError = true;
                            proc_img.StartInfo.UseShellExecute = false;
                            proc_img.StartInfo.CreateNoWindow = true;
                            proc_img.EnableRaisingEvents = false;
                            proc_img.StartInfo.CreateNoWindow = true;

                            get_frames.Start();
                            String outp = "-";
                            while (!get_frames.StandardOutput.EndOfStream)
                            {
                                outp = get_frames.StandardOutput.ReadLine();
                            }
                            get_frames.WaitForExit();
                            pic_frame.Invoke(new MethodInvoker(delegate
                            {
                                lbl_v_th.Text = "Image";
                                lbl_s_th.Text = outp;
                                lbl_a_th.Text = ext_a0.Replace(".", "");
                            }));
                        }
                    }
                });
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void btn_browse_Click(object sender, EventArgs e)
        {            
            if (Directory.Exists(Path.GetDirectoryName(out_file)) && Directory.GetFiles(Path.GetDirectoryName(out_file)).Length != 0)
            {                
                Process open_processed = new Process();
                open_processed.StartInfo.FileName = "explorer.exe";
                open_processed.StartInfo.Arguments = '\u0022' + Path.GetDirectoryName(out_file) + '\u0022';
                open_processed.Start();
            }
        }

        private void chk_no_popp_CheckedChanged(object sender, EventArgs e)
        {
            String no_out_pop = String.Empty;
            if (is_portable == false)
            {
                no_out_pop = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_pop_up.ini";
            }
            else
            {
                no_out_pop = port_path + "ff_pop_up_portable.ini";
            }
            if (chk_no_popp.CheckState == CheckState.Checked)
            {
                no_pop = true;
                File.WriteAllText(no_out_pop, String.Empty);                
            }
            else
            {
                no_pop = false;
                if (File.Exists(no_out_pop))
                {
                    try
                    {
                        File.Delete(no_out_pop);
                    }
                    catch { }
                }
            }
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            Process.Start(out_file);
        }
    }
}
