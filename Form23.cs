using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Globalization;

namespace FFBatch
{
    public partial class Form23 : Form
    {
        public Form23()
        {
            InitializeComponent();
        }
                
        Boolean cancel_queue = false;
        Boolean working = false;
        Boolean aborted_url = false;
        private Process process_glob = new Process();
        Boolean no_save_logs = false;
        Boolean is_portable = false;
        Boolean play_on_end = false;
        public Boolean open_complete = false;
        String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        int total_videos = 0;
        //ProgressBarWithText Pg1 = new ProgressBarWithText();
        int progress = 0;
        String play_file_path = "";
        System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer();
        Boolean killed = false;
        Boolean show_est = false;
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
                MessageBox.Show(FFBatch.Properties.Strings2.yt_url_blank);
                return;
            }
            if (txt_channel.Text.Length < 8)
            {
                MessageBox.Show(FFBatch.Properties.Strings2.yt_url_sh);
                return;
            }
            
            lbl_d_v.TextAlign = ContentAlignment.MiddleCenter;
            lbl_d_v.Width = 452;
            
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

                if (Directory.GetFiles(destino).Length > 0)
                {
                    DialogResult a = MessageBox.Show(FFBatch.Properties.Strings2.dest_not_empty, FFBatch.Properties.Strings.warning, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (a == DialogResult.No)
                    {
                        working = false;
                        this.InvokeEx(f => f.lbl_d_v.Text = "");
                        this.InvokeEx(f => timer1.Stop());
                        Enable_Controls();
                        return;
                    }
                }
                
                        String AppParam = txt_parameters.Text;                        
                        process_glob.StartInfo.FileName = ffm;                        
                        AppParam = pre_params + " " + txt_parameters.Text + " " + " -o " + '\u0022' + destino + "\\" + "%(title)s.%(ext)s" + '\u0022' + " " + txt_channel.Text;
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

                this.InvokeEx(f => f.lbl_d_v.Text = FFBatch.Properties.Strings2.init_wait);
                this.InvokeEx(f => timer1.Start());

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
                    Double interval = 0;
                    Decimal est_bitrate = 0;
                    Decimal est_size = 0;
                    Double sec_prog = 0;
                //this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.Normal));
                String pl_title = "";

                while (!process_glob.StandardOutput.EndOfStream)
                        {
                            err_txt = process_glob.StandardOutput.ReadLine();
                            list_lines.Add(err_txt);

                    if (err_txt.Contains("[download] Downloading playlist: ")) pl_title = err_txt.Replace("[download] Downloading playlist: ", "");
                    
                    if (err_txt.Contains("[youtube:tab] Downloading page "))
                    {
                        lbl_d_v.Invoke(new MethodInvoker(delegate
                        {
                            if (pl_title.Length == 0 ) lbl_d_v.Text = FFBatch.Properties.Strings2.brow_page + " " +  err_txt.Replace("[youtube:tab] Downloading page ", "");
                            else lbl_d_v.Text = FFBatch.Properties.Strings2.browsing + " "  + "\u0022" + pl_title + "\u0022" + " Page " + err_txt.Replace("[youtube:tab] Downloading page ", "");
                        }));
                    }

                        if (err_txt.Contains("[youtube:tab] playlist ") && err_txt.Contains("Downloading ") && err_txt.Contains("videos"))
                    {
                        int ind1 = err_txt.LastIndexOf("Downloading ");
                        int ind2 = err_txt.Length;
                        String n_vs = err_txt.Substring(ind1, ind2 - ind1).Replace("Downloading ", "").Replace("videos", "").Trim(); ;
                        lbl_d_v.Invoke(new MethodInvoker(delegate
                        {                            
                            lbl_d_v.Text = FFBatch.Properties.Strings2.total + ": "  + n_vs.Replace("Videos: Downloading ","").Replace("videos","").Trim();
                            //lbl_d_v.Visible = true;
                            //lbl_d_v.Text = "Starting downloads...";
                            total_videos = Convert.ToInt32(n_vs.Replace("Videos: Downloading ", "").Replace("videos", "").Trim());
                            Pg1.Maximum = total_videos;                            
                            this.InvokeEx(f => f.Pg1.Text = "0 of " + total_videos.ToString());
                            
                        }));
                    }
                    if (err_txt.Contains("[download] Destination: "))
                    {                        
                        lbl_d_v.Invoke(new MethodInvoker(delegate
                        {                            
                            lbl_d_v.Text =  Path.GetFileName(err_txt.Replace("Downloading video: ", ""));                         
                        }));
                    }
                    if (err_txt.Contains("[download] Downloading video ") && err_txt.Contains(total_videos.ToString()))
                    {
                        String prog = "";
                        Pg1.Invoke(new MethodInvoker(delegate
                        {
                            try
                            {
                                prog = err_txt.Replace("[download] ", "");
                                prog = prog.Replace("Downloading video ", "");
                                prog = prog.Replace(" of " + total_videos.ToString(), "");
                                Pg1.Value = Convert.ToInt32(prog);
                                Pg1.Text = err_txt.Replace("[download] Downloading video ", "");
                                this.InvokeEx(f => TaskbarProgress.SetValue(this.Handle, Pg1.Value, Pg1.Maximum));
                            }
                            catch
                            {
                                lbl_d_v.Text = FFBatch.Properties.Strings2.outf_count + " "  + Directory.GetFiles(destino).Length.ToString();
                                lbl_d_v.Refresh();
                                total_videos = 0;
                            }
                        }));                        
                    }
                    if (err_txt.Contains("%"))
                    {
                        try
                        {
                            this.InvokeEx(f => f.lbl_d_v.TextAlign = ContentAlignment.MiddleLeft);
                            this.InvokeEx(f => f.lbl_d_v.Width = 349);
                            Double prog_y = Double.Parse(err_txt.Substring(err_txt.IndexOf("%") - 4, 4));
                            prog_y = prog_y / 10;
                            int progress = Convert.ToInt32(prog_y);
                            this.InvokeEx(f => f.pg2.Value = progress);
                            this.InvokeEx(f => f.pg2.Text = progress.ToString() + "%");
                            
                        }
                        catch { }
                    }
                    if (err_txt.Contains("ETA") && show_est == true)
                    {
                        try
                        {
                            int ind1 = err_txt.LastIndexOf("ETA");
                            String unit = "";
                            String est_time = err_txt.Substring(ind1, err_txt.Length - ind1).Replace("ETA", "").Trim();
                            if (err_txt.ToLower().Contains("mib/s")) unit = " MB/s";
                            if (err_txt.ToLower().Contains("kib/s")) unit = " KB/s";
                            this.InvokeEx(f => f.lbl_down_time.Text = err_txt.Substring(err_txt.IndexOf("at ") + 3, 5) + " " + unit + " - " + est_time);
                        }
                        catch { }
                    }
                    if (total_videos == 0 || total_videos == 1)
                    {
                        try
                        {
                            this.InvokeEx(f => f.Pg1.Maximum = pg2.Maximum);
                            this.InvokeEx(f => f.Pg1.Value = pg2.Value);
                            this.InvokeEx(f => f.Pg1.Text = pg2.Text);
                            this.InvokeEx(f => TaskbarProgress.SetValue(this.Handle, Pg1.Value, Pg1.Maximum));
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
                    this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress));
                    this.InvokeEx(f => timer1.Stop());
                                    
                    this.InvokeEx(f => f.lbl_d_v.Text = "");
                    this.InvokeEx(f => f.lbl_down_time.Text = "");

                list_lines.Add("");
                    list_lines.Add("---------End of download log---------");
                    list_lines.Add("");
                    if (process_glob.ExitCode == 0)
                    {
                    
                    this.InvokeEx(f => f.pg2.Text = "100%");
                    this.InvokeEx(f => f.Pg1.Text = "100%");
                    this.InvokeEx(f => f.pg2.Value = pg2.Maximum);
                    this.InvokeEx(f => f.Pg1.Value = Pg1.Maximum);

                    if (total_videos > 0 && Directory.GetFiles(destino).Length == total_videos)
                    {
                        MessageBox.Show(Properties.Strings2.down_com + " " + Environment.NewLine + Environment.NewLine + total_videos.ToString() + " " + Properties.Strings2.vids_dest, Properties.Strings2.down_com, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                       if (Directory.GetFiles(destino).Length < total_videos) MessageBox.Show(Properties.Strings2.down_com + " " + Environment.NewLine + Environment.NewLine + Directory.GetFiles(destino).Length.ToString() + " " + Properties.Strings.of1 +  " " + total_videos.ToString() + " " + Properties.Strings2.vids_dest + Environment.NewLine + Environment.NewLine + Properties.Strings2.vids_unav, Properties.Strings2.down_com, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }                   
                }
                    else
                    {
                    this.InvokeEx(f => f.Pg1.Refresh());
                    this.InvokeEx(f => f.pg2.Refresh());

                    String to_remove = "please report this issue on https://yt-dl.org/bug . Make sure you are using the latest version; type  youtube-dl -U  to update. Be sure to call youtube-dl with the --verbose flag and include its complete output.";
                    String remove2 = "Type youtube-dl --help to see a list of all options.";
                    if (aborted_url == false &&  killed == false) MessageBox.Show(Properties.Strings.error2 + Environment.NewLine + Environment.NewLine + error_out.Replace(to_remove, "").Replace(remove2,"Check youtube-dl parameters and url."),FFBatch.Properties.Strings.error,MessageBoxButtons.OK,MessageBoxIcon.Error);
                    if (killed == true) MessageBox.Show(Properties.Strings2.aborted2, Properties.Strings.aborted, MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        this.InvokeEx(f => MessageBox.Show(FFBatch.Properties.Strings2.aborted2, FFBatch.Properties.Strings.aborted, MessageBoxButtons.OK, MessageBoxIcon.Error));                    
                        this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress));
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
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
            //Read play sound

            refresh_lang();            
            this.Text = FFBatch.Properties.Strings2.quick_yt1;
            txt_get_url.Text = FFBatch.Properties.Strings2.quick_yt2 + Environment.NewLine + FFBatch.Properties.Strings2.quick_yt3 + Environment.NewLine + FFBatch.Properties.Strings2.quick_yt4 + " " + "https://www.youtube.com/user/[channel_name]/videos";
            
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

        }

        private void abort_dl()
        {
            if (working == false) return;

                    StreamWriter write_q2 = process_glob.StandardInput;
                    write_q2.Write("q");
                    Process[] localByName = Process.GetProcessesByName("yt-dlp");
                    foreach (Process p in localByName)
                    {
                        if (p.Id == process_glob.Id)
                        {
                            try { p.Kill(); }
                            catch { }
                        }
                    }                

                aborted_url = true;                
                cancel_queue = true;                
            }

        private void btn_abort_all_Click(object sender, EventArgs e)
        {
            abort_dl();
        }

        private void btn_browse_path_m3u_Click(object sender, EventArgs e)
        {
            if (fd1.ShowDialog() == DialogResult.OK) txt_path_main.Text = fd1.SelectedPath;
        }

        private void lbl_d_v_TextChanged(object sender, EventArgs e)
        {
            if (lbl_d_v.Text.Length > 69)
            {
                lbl_d_v.Text = lbl_d_v.Text.Substring(0, 66) + "...";
            }
        }

        private void btn_clear_list_Click(object sender, EventArgs e)
        {
            lbl_d_v.Text = "";
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
                MessageBox.Show(FFBatch.Properties.Strings2.down_prog);
                return;
            }
            else this.Close();
        }

        private void Form23_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working == true)
            {
                DialogResult a =  MessageBox.Show(FFBatch.Properties.Strings2.down_p_ab, FFBatch.Properties.Strings2.down_prog2,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
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
    }
}
