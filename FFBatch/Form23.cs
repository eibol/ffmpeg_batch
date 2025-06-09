﻿using FFBatch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }

        private int v_count = 0;
        private Boolean cancel_queue = false;
        private Boolean working = false;
        private Boolean aborted_url = false;
        private Process process_glob = new Process();
        private Boolean no_save_logs = false;
        private Boolean is_portable = false;
        private Boolean play_on_end = false;
        public Boolean open_complete = false;
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        private int total_videos = 0;
        private String play_file_path = "";
        private System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
        private Boolean killed = false;
        private Boolean show_est = false;
        public String pre_params = "";

        private void play_end()
        {
            try
            {
                Task t = Task.Run(() =>
                {
                    soundPlayer.SoundLocation = play_file_path;
                    soundPlayer.Play();
                    Thread.Sleep(8000);
                    soundPlayer.Stop();
                });
            }
            catch { }
        }

        private void Disable_Controls()
        {
            foreach (Control ct in groupBox2.Controls) ct.Enabled = false;
            btn_abort_all.Enabled = true;
        }

        private void Enable_Controls()
        {
            foreach (Control ct in groupBox2.Controls) this.InvokeEx(f => ct.Enabled = true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_path_main.Text.Length == 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings.out_blank);
                return;
            }

            if (txt_channel.Text.Length == 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings.yt_url_blank);
                return;
            }
            if (txt_channel.Text.Length < 8)
            {
                MessageBox.Show(FFBatch.Properties.Strings.yt_url_sh);
                return;
            }

            lbl_d_v.TextAlign = ContentAlignment.MiddleCenter;
            lbl_d_v.Width = 452;            
            btn_abort_all.Text = Strings.ResourceManager.GetString("abort_down", new CultureInfo(Properties.Settings.Default.app_lang)); ;
            List<string> list_lines = new List<string>();
            List<string> list_err = new List<string>();
            total_videos = 0;
            aborted_url = false;
            lbl_d_v.Text = "";
            Pg1.Value = 0;
            pg2.Value = 0;
            working = true;            
            killed = false;
            Pg1.Value = 0;
            Pg1.Text = "0%";
            pg2.Value = 0;
            pg2.Text = "0%";
            Pg1.Refresh();
            pg2.Refresh();
            lbl_d_v.Text = "";
            lbl_down_time.Text = "";
            Boolean sel_format = false;
            String set_format = "";
            if (cb_format.SelectedIndex != -1)
            {
                sel_format = true;
                set_format = cb_format.SelectedItem.ToString();
            }
            
            Disable_Controls();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                System.Threading.Thread.Sleep(50); //Allow kill process to send cancel_queue

                String file = txt_channel.Text;
                String ffm = System.IO.Path.Combine(Application.StartupPath, "yt-dlp.exe");
                String destino = txt_path_main.Text;

                if (!Directory.Exists(destino))
                {
                    try
                    {
                        Directory.CreateDirectory(destino);
                    }
                    catch (System.Exception excpt)
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.write_error2 + " " + excpt.Message, FFBatch.Properties.Strings.write_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        this.InvokeEx(f => f.lbl_d_v.Text = "");
                        return;
                    }
                }                

                process_glob.StartInfo.FileName = ffm;
                String AppParam = pre_params + " " + txt_parameters.Text + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " --ffmpeg-location " + '\u0022' + Properties.Settings.Default.ffm_path + '\u0022' + " " + txt_channel.Text;
                if (sel_format == true) AppParam = AppParam + " --merge-output-format " +  set_format;
                process_glob.StartInfo.Arguments = AppParam;

                if (set_format == "m4a") process_glob.StartInfo.Arguments = "-f bestaudio[ext=m4a] -x " + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " --ffmpeg-location " + '\u0022' + Properties.Settings.Default.ffm_path + '\u0022' + " " + txt_channel.Text;
                if (set_format == "mp3") process_glob.StartInfo.Arguments = "-f bestaudio[ext=m4a] -x --audio-format mp3 --audio-quality 0 " + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " --ffmpeg-location " + '\u0022' + Properties.Settings.Default.ffm_path + '\u0022' + " " + txt_channel.Text;
                if (set_format == "flac") process_glob.StartInfo.Arguments = " -f bestaudio -x --audio-format flac --audio-quality 5 " + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " --ffmpeg-location " + '\u0022' + Properties.Settings.Default.ffm_path + '\u0022' + " " + txt_channel.Text;
                if (set_format == "opus") process_glob.StartInfo.Arguments = "-f bestaudio -x --audio-format opus --audio-quality 0 " + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " --ffmpeg-location " + '\u0022' + Properties.Settings.Default.ffm_path + '\u0022' + " " + txt_channel.Text;
                process_glob.StartInfo.Arguments = "--windows-filenames   " + process_glob.StartInfo.Arguments + " -P " + destino;

                if (!File.Exists(System.IO.Path.Combine(Application.StartupPath, "yt-dlp.exe")))
                {
                    cancel_queue = true;
                    working = false;
                    timer_tasks.Stop();
                    Enable_Controls();
                    this.Invoke(new MethodInvoker(delegate
                    {
                        TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                        timer1.Stop();
                        lbl_down_time.Text = "";
                    }));                    
                    MessageBox.Show(FFBatch.Properties.Strings.yt_not, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Invoke(new MethodInvoker(delegate
                {
                    lbl_d_v.Text = FFBatch.Properties.Strings.init_wait;
                    timer1.Start();
                }));
               

                process_glob.StartInfo.RedirectStandardOutput = true;
                process_glob.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.RedirectStandardError = true;
                process_glob.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.EnableRaisingEvents = true;
                process_glob.Start();

                String err_txt = "";
                String error_out = "";
                String tr_speed = "";
                int n_vid = 0;
                Double interval = 0;
                Decimal est_bitrate = 0;
                Decimal est_size = 0;
                Double sec_prog = 0;
                String n_vs = "";
                //this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Normal));
                String pl_title = "";

                while (!process_glob.StandardOutput.EndOfStream)
                {
                    err_txt = process_glob.StandardOutput.ReadLine();
                    list_lines.Add(err_txt);

                    if (err_txt.Contains("[download] Downloading playlist: ")) 
                        pl_title = err_txt.Replace("[download] Downloading playlist: ", "");

                    if (err_txt.Contains("[youtube:tab] Downloading page "))
                    {
                        lbl_d_v.Invoke(new MethodInvoker(delegate
                        {
                            if (pl_title.Length == 0) lbl_d_v.Text = FFBatch.Properties.Strings.brow_page + " " + err_txt.Replace("[youtube:tab] Downloading page ", "");
                            else lbl_d_v.Text = FFBatch.Properties.Strings.browsing + " " + "\u0022" + pl_title + "\u0022" + " Page " + err_txt.Replace("[youtube:tab] Downloading page ", "");
                        }));
                    }

                    if (err_txt.Contains("[download] Downloading item ") && err_txt.Contains(" of "))
                    {
                        n_vid++;
                        int start_c = err_txt.LastIndexOf("[download] Downloading item ") + 28;
                        int end_c = err_txt.Length - start_c;
                        n_vs = err_txt.Substring(start_c, end_c).Replace("[download] Downloading item ", "").Replace("of ", "");
                        n_vs = n_vs.Substring(n_vs.LastIndexOf(" "), n_vs.Length - n_vs.LastIndexOf(" "));
                        total_videos = Convert.ToInt32(n_vs);
                        Pg1.Invoke(new MethodInvoker(delegate
                        {
                            Pg1.Maximum = total_videos;
                            Pg1.Value = n_vid;
                            Pg1.Text = n_vid +  " of " + total_videos.ToString();
                        }));
                    }
                    
                    if (err_txt.Contains("[download] Destination: "))
                    {
                        lbl_d_v.Invoke(new MethodInvoker(delegate
                        {
                            lbl_d_v.Text = Path.GetFileName(err_txt.Replace("Downloading video: ", ""));
                        }));
                    }
            
                    if (err_txt.Contains("%"))
                    {
                        try
                        {
                            Double prog_y = Double.Parse(err_txt.Substring(err_txt.IndexOf("%") - 4, 4));
                            prog_y = prog_y / 10;
                            int progress = Convert.ToInt32(prog_y);
                            this.Invoke(new MethodInvoker(delegate
                            {
                                lbl_d_v.TextAlign = ContentAlignment.MiddleLeft;
                                lbl_d_v.Width = 349;                               
                                pg2.Value = progress;
                                pg2.Text = progress.ToString() + "%";
                            }));
                            
                        }
                        catch { }
                    }
                    if (err_txt.Contains("ETA") && show_est == true)
                    {
                        try
                        {
                            int ind1 = err_txt.LastIndexOf("ETA");                            
                            String est_time = err_txt.Substring(ind1, err_txt.Length - ind1).Replace("ETA", "").Trim();
                            
                            String tr_r = "";
                            if (err_txt.ToLower().Contains("mib/s"))
                            {
                                int ind_tr = err_txt.ToLower().IndexOf("at ") + 3;
                                int ind_tr_e = err_txt.ToLower().IndexOf("mib/s");
                                int leng = ind_tr_e - ind_tr;
                                tr_r = err_txt.Substring(ind_tr, leng);
                                tr_speed = tr_r.Trim(' ') + " MB/s";
                            }
                            if (err_txt.ToLower().Contains("kib/s"))
                            {
                                int ind_tr = err_txt.ToLower().IndexOf("at ") + 3;
                                int ind_tr_e = err_txt.ToLower().IndexOf("kib/s");
                                int leng = ind_tr_e - ind_tr;
                                tr_r = err_txt.Substring(ind_tr, leng);
                                tr_speed = tr_r.Trim(' ') + " KB/s";
                            }

                            this.InvokeEx(f => f.lbl_down_time.Text = tr_speed + " - " + est_time);
                        }
                        catch { }
                    }
                    if (total_videos == 0 || total_videos == 1)
                    {
                        try
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                               Pg1.Maximum = pg2.Maximum;
                               Pg1.Value = pg2.Value;
                               Pg1.Text = pg2.Text;
                               TaskbarProgress.SetValue(this.Handle, Pg1.Value, Pg1.Maximum);
                            }));
                            
                        }
                        catch { }
                    }
                }

                while (!process_glob.StandardError.EndOfStream)
                {
                    error_out = process_glob.StandardError.ReadLine();
                }
                process_glob.WaitForExit();
                process_glob.StartInfo.Arguments = String.Empty;
                
                foreach (String str in list_lines)
                {
                    if (str.ToLower().Contains("has already been downloaded"))
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            MessageBox.Show(Strings.already_down + Environment.NewLine + Environment.NewLine + Strings.check_log, Strings.warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);                        
                        }));
                        break;
                    }
                }
                
                this.Invoke(new MethodInvoker(delegate
                {
                    TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                    timer1.Stop();
                    lbl_d_v.Text = "";
                    lbl_down_time.Text = "";
                }));

                list_lines.Add("");
                list_lines.Add("---------End of download log---------");
                list_lines.Add("");
                if (process_glob.ExitCode == 0)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                       pg2.Text = "100%";
                       Pg1.Text = "100%";
                       pg2.Value = pg2.Maximum;
                       Pg1.Value = Pg1.Maximum;
                    }));
             

                    if (total_videos > 0 && Directory.GetFiles(destino).Length == total_videos)
                    {
                        MessageBox.Show(Properties.Strings.down_com + " " + Environment.NewLine + Environment.NewLine + total_videos.ToString() + " " + Properties.Strings.vids_dest, Properties.Strings.down_com, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (Directory.GetFiles(destino).Length < total_videos) MessageBox.Show(Properties.Strings.down_com + " " + Environment.NewLine + Environment.NewLine + Directory.GetFiles(destino).Length.ToString() + " " + Properties.Strings.of1 + " " + total_videos.ToString() + " " + Properties.Strings.vids_dest + Environment.NewLine + Environment.NewLine + Properties.Strings.vids_unav, Properties.Strings.down_com, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        Pg1.Refresh();
                        pg2.Refresh();
                    }));                    

                    String to_remove = "please report this issue on https://yt-dl.org/bug . Make sure you are using the latest version; type  youtube-dl -U  to update. Be sure to call youtube-dl with the --verbose flag and include its complete output.";
                    String remove2 = "Type youtube-dl --help to see a list of all options.";
                    if (aborted_url == false && killed == false) MessageBox.Show(Properties.Strings.error2 + Environment.NewLine + Environment.NewLine + error_out.Replace(to_remove, "").Replace(remove2, "Check youtube-dl parameters and url."), FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //if (killed == true) MessageBox.Show(Properties.Strings.aborted2, Properties.Strings.aborted, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress));

                working = false;

                //Save log
                if (no_save_logs == false)
                {
                    string[] array_err = list_lines.ToArray();

                    String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";
                    if (is_portable == true) path = port_path + "ff_batch_portable.log";

                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                    SaveFile.WriteLine("Download YouTube log sesion: " + System.DateTime.Now);
                    SaveFile.WriteLine("-----------------------");
                    foreach (String item in array_err)
                    {
                        SaveFile.WriteLine(item);
                    }

                    SaveFile.Close();

                    File.AppendAllText(path, "-----------------------");
                    File.AppendAllText(path, Environment.NewLine + "END OF LOG FILE");
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);

                    var bytes = fileInfo.Length;

                    var kilobytes = (double)bytes / 1024;
                    var megabytes = kilobytes / 1024;
                    var gigabytes = megabytes / 1024;

                    //Format size view
                    String size = "";
                    String separator = ".";

                    if (bytes > 1000000000)
                    {
                        if (gigabytes.ToString().Contains("."))
                        {
                            separator = ".";
                        }
                        else
                        {
                            separator = ",";
                        }

                        String gigas = gigabytes.ToString();
                        if (gigas.Length >= 5)
                        {
                            gigas = gigas.Substring(0, gigas.LastIndexOf(separator) + 3);
                            size = (gigas + " GB");
                        }
                        else
                        {
                            size = (gigas + " GB");
                        }
                    }

                    if (bytes >= 1048576 && bytes <= 1000000000)
                    {
                        if (megabytes.ToString().Contains("."))
                        {
                            separator = ".";
                        }
                        else
                        {
                            separator = ",";
                        }
                        String megas = megabytes.ToString();
                        if (megas.Length > 5)
                        {
                            megas = megas.Substring(0, megas.LastIndexOf(separator));
                            size = (megas + " MB");
                        }
                        else
                        {
                            size = (megas + " MB");
                        }
                    }

                    if (bytes >= 1024 && bytes < 1048576)

                    {
                        if (kilobytes.ToString().Contains("."))
                        {
                            separator = ".";
                        }
                        else
                        {
                            separator = ",";
                        }

                        String kbs = kilobytes.ToString();
                        if (kbs.Length >= 5)
                        {
                            kbs = kbs.Substring(0, kbs.LastIndexOf(separator));
                            size = (kbs + " KB");
                        }
                        else
                        {
                            size = (kbs + " KB");
                        }
                    }
                    if (bytes > -1 && bytes < 1024)
                    {
                        String bits = bytes.ToString();
                        size = (bits + " Bytes");
                    }

                    //End Format size view
                    File.AppendAllText(path, Environment.NewLine + "LOG SIZE: " + size);

                    //End save log
                }

                if (aborted_url == false && process_glob.ExitCode == 0)
                {
                    if (Form.ActiveForm == null)
                    {
                        notifyIcon1.BalloonTipText = FFBatch.Properties.Strings.url_comp;
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon1.BalloonTipTitle = FFBatch.Properties.Strings.url_comp2;
                        notifyIcon1.ShowBalloonTip(0);
                        if (play_on_end == true && process_glob.ExitCode == 0 && aborted_url == false) play_end();
                    }

                    if (open_complete == true)
                    {
                        if (Directory.GetFiles(destino).Length != 0)
                        {
                            Process open_processed = new Process();
                            open_processed.StartInfo.FileName = "explorer.exe";
                            open_processed.StartInfo.Arguments = '\u0022' + destino + '\u0022';
                            open_processed.Start();
                        }
                        else
                        {
                            if (Directory.Exists(destino))
                            {
                                System.IO.Directory.Delete(destino);
                            }
                        }
                    }
                }

                if (aborted_url == true)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.aborted2, FFBatch.Properties.Strings.aborted, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress);
                        this.Cursor = Cursors.Arrow;
                    }));
                   
                }
                
                Enable_Controls();
            }).Start();
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form23));
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

        private void Form23_Load(object sender, EventArgs e)
        {
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

            working = false;            
            Pg1.Text = "0%";
            pg2.Text = "0%";
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true; else is_portable = false;
            
            refresh_lang();
            btn_abort_all.Text = Strings.ResourceManager.GetString("close_win", new CultureInfo(Properties.Settings.Default.app_lang));
            this.Text = FFBatch.Properties.Strings.quick_yt1;
            txt_get_url.Text = FFBatch.Properties.Strings.quick_yt2 + Environment.NewLine + FFBatch.Properties.Strings.quick_yt3 + Environment.NewLine + FFBatch.Properties.Strings.quick_yt4 + " " + "https://www.youtube.com/user/[channel_name]/videos";

            String ff_play_sound = String.Empty;
            if (is_portable == false)
            {
                ff_play_sound = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_play.ini";
            }
            else
            {
                ff_play_sound = port_path + "ff_play_portable.ini";
            }

            if (!File.Exists(ff_play_sound))
            {
                play_on_end = false;
            }
            else
            {
                String read_play = File.ReadAllText(ff_play_sound);
                if (read_play.Length != 0)
                {
                    play_on_end = true;
                    play_file_path = read_play;
                }
                else
                {
                    play_on_end = false;
                }
            }

            if (FFBatch.Properties.Settings.Default.app_lang == "zh-Hans") this.Height = this.Height + 20;
            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
            cb_format.Items.RemoveAt(cb_format.FindString("m3u8"));

        }

        private void abort_dl()
        {
            if (working == false) return;
            if (Send_CTRLC(process_glob) == false)
            {
                Process[] localByName = Process.GetProcessesByName("yt-dlp");
                foreach (Process p in localByName)
                {
                    if (p.Id == process_glob.Id)
                    {
                        try { p.Kill(); }
                        catch { }
                    }
                }
            }

            aborted_url = true;
            cancel_queue = true;
        }

        public Boolean Send_CTRLC(Process p)
        {
            if (AttachConsole((uint)p.Id))
            {
                SetConsoleCtrlHandler(null, true);
                try
                {
                    if (!GenerateConsoleCtrlEvent(CTRL_C_EVENT, 0))
                        return false;
                    p.WaitForExit(10000);
                }
                finally
                {
                    FreeConsole();
                    SetConsoleCtrlHandler(null, false);
                }
                return true;
            }
            return false;
        }


        private void btn_abort_all_Click(object sender, EventArgs e)
        {
            if (working == true)
            {
                btn_abort_all.Text = Strings.ResourceManager.GetString("aborting", new CultureInfo(Properties.Settings.Default.app_lang));
                abort_dl();
                btn_abort_all.Text = Strings.ResourceManager.GetString("close_win", new CultureInfo(Properties.Settings.Default.app_lang));
            }
            else this.Close();

        }

        private void btn_browse_path_m3u_Click(object sender, EventArgs e)
        {
            if (fd1.ShowDialog() == DialogResult.OK) txt_path_main.Text = fd1.SelectedPath;
        }

        private void lbl_d_v_TextChanged(object sender, EventArgs e)
        {
            if (lbl_d_v.Text.Length > 109)
            {
                lbl_d_v.Text = lbl_d_v.Text.Substring(0, 106) + "...";
            }
        }

        private void btn_clear_list_Click(object sender, EventArgs e)
        {
            cb_codec.SelectedIndex = -1;
            cb_res.SelectedIndex = -1;
            lbl_d_v.Text = "";
            lbl_vcount.Text = "";
            txt_parameters.Text = "";
            lbl_d_v.TextAlign = ContentAlignment.MiddleLeft;
            lbl_down_time.Text = "";
            txt_channel.Text = "";
            Pg1.Value = 0;
            Pg1.Text = "0%";
            pg2.Value = 0;
            pg2.Text = "0%";
            Pg1.Refresh();
            pg2.Refresh();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (working == true)
            {
                MessageBox.Show(FFBatch.Properties.Strings.down_prog);
                return;
            }
            else this.Close();
        }

        private void Form23_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working == true)
            {
                DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.down_p_ab, FFBatch.Properties.Strings.down_prog2, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a == DialogResult.No || a == DialogResult.Cancel) e.Cancel = true;
                else
                {
                    Process[] localByName = Process.GetProcessesByName("yt-dlp");
                    Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                    try
                    {
                        foreach (Process p in localByName) p.Kill();
                        foreach (Process p in localByName2) p.Kill();
                        killed = true;
                        e.Cancel = true;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            show_est = !show_est;
        }

        internal const int CTRL_C_EVENT = 0;

        [DllImport("kernel32.dll")]
        internal static extern bool GenerateConsoleCtrlEvent(uint dwCtrlEvent, uint dwProcessGroupId);

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool AttachConsole(uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        private static extern bool SetConsoleCtrlHandler(ConsoleCtrlDelegate HandlerRoutine, bool Add);

        // Delegate type to be used as the Handler Routine for SCCH
        private delegate Boolean ConsoleCtrlDelegate(uint CtrlType);

        private void get_v_count()
        {
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                System.Threading.Thread.Sleep(50); //Allow kill process to send cancel_queue

                String file = txt_channel.Text;
                String ffm = System.IO.Path.Combine(Application.StartupPath, "yt-dlp.exe");
                String destino = txt_path_main.Text;
                String err_txt = "";

                if (!Directory.Exists(destino))
                {
                    try
                    {
                        Directory.CreateDirectory(destino);
                    }
                    catch (System.Exception excpt)
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.write_error2 + " " + excpt.Message, FFBatch.Properties.Strings.write_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        this.InvokeEx(f => f.lbl_d_v.Text = "");
                        return;
                    }
                }

                this.Invoke(new MethodInvoker(delegate
                {
                    lbl_vcount.Text = Properties.Strings.init1;
                    button1.Enabled = false;
                    txt_channel.Enabled = false;
                    btn_clear_list.Enabled = false;
                }));

                String AppParam = txt_parameters.Text;
                process_glob.StartInfo.FileName = ffm;
                AppParam = "--flat-play --get-id " + txt_channel.Text;
                process_glob.StartInfo.Arguments = AppParam;

                if (!File.Exists(System.IO.Path.Combine(Application.StartupPath, "yt-dlp.exe")))
                {
                    cancel_queue = true;
                    working = false;
                    timer_tasks.Stop();
                    Enable_Controls();
                    this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress));
                    this.InvokeEx(f => timer1.Stop());
                    this.InvokeEx(f => f.lbl_down_time.Text = "");
                    MessageBox.Show(FFBatch.Properties.Strings.yt_not, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                                
                this.InvokeEx(f => timer1.Start());

                List<string> list_lines = new List<string>();

                process_glob.StartInfo.RedirectStandardOutput = true;
                process_glob.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.RedirectStandardError = true;
                process_glob.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.EnableRaisingEvents = true;
                process_glob.Start();

                while (!process_glob.StandardOutput.EndOfStream)
                {
                    err_txt = process_glob.StandardOutput.ReadLine();
                    list_lines.Add(err_txt);
                }
                this.Invoke(new MethodInvoker(delegate
                {
                    lbl_vcount.Text = "Videos" + ": " + list_lines.Count;
                    Pg1.Text = "0 " + Properties.Strings.of1 +  " " + list_lines.Count;
                    Pg1.Refresh();
                    button1.Enabled = true;
                    btn_clear_list.Enabled = true;
                    txt_channel.Enabled = true;
                }));
                
                v_count = list_lines.Count();


            }).Start();
        }
            

        private void txt_channel_TextChanged(object sender, EventArgs e)
        {
            if (txt_channel.Text.Contains("youtube") || txt_channel.Text.Contains("youtu.be"))
            {
                // Code order: -S res,codec:av1,codec:h264,codec:vp9
                get_v_count();
            }
        }

        private void cb_codec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_codec.SelectedIndex == -1) return;
            txt_parameters.Text = "";
            String param = "-S +codec:" + cb_codec.SelectedItem.ToString();
            
            if (cb_res.SelectedIndex != -1)
            {
                param = "-S res:" + cb_res.SelectedItem.ToString().Replace("p", "")  + ",+codec:" + cb_codec.SelectedItem.ToString();
            }
            txt_parameters.Text = param;
        }

        private void cb_res_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_res.SelectedIndex == -1) return;
            txt_parameters.Text = "";
            String param = "-S res:" + cb_res.SelectedItem.ToString().Replace("p", "");

            if (cb_codec.SelectedIndex != -1)
            {
                param = "-S res:" + cb_res.SelectedItem.ToString().Replace("p", "") + ",+codec:" + cb_codec.SelectedItem.ToString();
            }
            txt_parameters.Text = param;
        }
    }
}
//--merge-output-format mkv