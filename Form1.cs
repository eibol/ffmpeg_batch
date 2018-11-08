using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Shell32;
using System.Runtime.InteropServices;
using System.Net;
//using System.Collections.Concurrent;


namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        private static SemaphoreSlim semaphore;
        // A padding interval to make the output more orderly.
        private static int padding;

        private ListViewColumnSorter lvwColumnSorter;
        private ListViewColumnSorter lvwColumnSorter3;

        //
        Process process2 = new Process();
        Boolean cancelados_paralelos = false;
        Boolean cancel_queue = false;
        Boolean working = false;
        int tiempo_apaga = 120;
        Double durat_n = 0;
        String validate_duration;
        Boolean valid_prog = false;
        String def_mux_video_enc = "copy";
        String def_mux_audio_enc = "copy";
        String def_mux_subs_enc = "copy";
        String def_lang_und_tracks = "und";
        Boolean Extract_img = false;
        Boolean select_mp4 = false;
        int time_n_tasks = 0;
        Boolean total_time = false;
        Boolean recording_scr = false;
        Boolean hard_sub = false;
        String add_suffix = "";
        int capture_handle;
        Boolean Enable_txt_hard_Subs = false;
        long tot_size = 0;
        Boolean add_subfs = false;
        Boolean empty_text = false;
        String n_th_suffix = String.Empty;
        String n_th_source_ext = String.Empty;
        int pending_dur = 0;
        Boolean canceled_add = false;
        Boolean dur_ok = false;
        Boolean canceled_file_adding = false;
        ListView list_pending_dur = new ListView();
        ListView list_adding = new ListView();
        ListView list_global_proc = new ListView();
        List<string> files_to_add = new List<string>();
        Boolean change_tab_1 = false;
        Boolean change_tab_2 = false;
        Boolean list_not_empty = false;
        Process process_glob = new Process();
        Process probe_urls = new Process();
        Label was_started = new Label();
        Boolean avoid_overwriting = false;
        Boolean stop_validating_url = false;
        Boolean skip_current_url = false;
        Boolean m3u_running = false;
        Boolean multi_running = false;
        Dictionary<string, Process> procs = new Dictionary<string, Process>();
        int rows_running = 0;
        Boolean aborted_url = false;
        int capture_count = 0;
        Boolean tried_ok = false;
        String textbox_params = String.Empty;
        Boolean aborted = false;
        Double total_multi_duration = 0;
        Double start_total_time = 0;
        ComboBox mem_prio = new ComboBox();

        public Form1()
        {
            InitializeComponent();

            // Create an instance of a ListView column sorter and assign it // to the ListView control.
            lvwColumnSorter = new ListViewColumnSorter();
            this.listView1.ListViewItemSorter = lvwColumnSorter;
            lvwColumnSorter3 = new ListViewColumnSorter();
            this.listView3.ListViewItemSorter = lvwColumnSorter3;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Audio and video |*.mp4; *.mkv; *.mp3; *.wav; *.flac; *.avi; *.mts; *.flv; *.alac; *.aac; *.mpg; *.mp2; *.mpe; *.ogv; *.webm; *.aiff; *.vob; *.wma; *.wmv; *.mov; *.mka; *.m2ts; *.ac3; *.ogg|All files(*.*) | *.*";

            if (tabControl1.SelectedIndex == 3)
            {
                btn_add_urls.PerformClick();
                return;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                openFileDialog1.Filter = "Audio and video |*.mp4; *.mkv; *.mp3; *.wav; *.flac; *.avi; *.mts; *.flv; *.alac; *.aac; *.mpg; *.mp2; *.mpe; *.ogv; *.webm; *.aiff; *.vob; *.wma; *.wmv; *.mov; *.mka; *.srt; *.m2ts; *.idx; *.ac3; *.jpg; *.png; *.gif; *.psd; *.tiff; *.ass; *.ogg|All files(*.*) | *.*";
            }
            if (tabControl1.SelectedIndex == 2)
            {
                openFileDialog1.Filter = "Audio and video | *.mp4; *.mkv; *.avi; *.mts; *.flv; *.mpg; *.mp2; *.mpe; *.ogv; *.webm; *.aiff; *.vob; *.wmv; *.mov; *.mka; *.m2ts; *.ogg| All files(*.*) | *.*";
            }
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            change_tab_1 = false;
            change_tab_2 = false;

            if (tabControl1.SelectedIndex == 1)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_1 = true;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_2 = true;
            }

            string[] file_drop = (string[])openFileDialog1.FileNames;

            List<string> files2 = new List<string>();

            int num_drop = 0;
            foreach (String dropped in file_drop)
            {

                if (File.Exists(dropped))
                {
                    files2.Add(dropped);
                }

                num_drop = files2.Count();


                DriveInfo driveInfo = new DriveInfo(Path.GetDirectoryName(files2[0]));
                if (num_drop >= 1000 && driveInfo.DriveType == DriveType.Network)
                {

                    var a = MessageBox.Show("Adding " + num_drop + " files from a network drive can take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        return;
                    }

                }
                if (num_drop >= 2800 && driveInfo.DriveType != DriveType.Network)
                {

                    var a = MessageBox.Show("Adding " + num_drop + " files can take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        return;
                    }
                }
            }


            files_to_add = files2;
            canceled_file_adding = false;
            btn_cancel_add.Enabled = true;
            btn_cancel_add.Visible = true;
            btn_cancel_add.Refresh();
            BG_Files.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            was_started.Text = button2.Text;
            cancel_queue = false;
            notifyIcon1.Visible = true;
            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String is_overw = textBox3.Text + "\\" + Path.GetFileNameWithoutExtension(listView1.Items[0].Text) + "." + textBox2.Text;

            if (is_overw == listView1.Items[0].Text && chk_suffix.Checked == false)
            {
                MessageBox.Show("Overwriting is not supported. Change destination directory or enable " + '\u0022' + "Rename output" + '\u0022' + " checkbox.", "Overwriting not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            DateTime time2;
            if (!DateTime.TryParse(ss_time_input.Text, out time2))
            {
                MessageBox.Show("Pre-input seeking format is incorrect. Change it or reset it by double-clicking on it", "Pre-input seeking format error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ss_time_input.Text != "0:00:00")
            {
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "0:00:00" && item.SubItems[2].Text != "00:00:00" && item.SubItems[2].Text != "Pending")
                    {
                        if (TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds <= TimeSpan.Parse(ss_time_input.Text).TotalSeconds)
                        {
                            MessageBox.Show("Pre-input seeking exceeds duration of file: " + '\u0022' + Path.GetFileName(item.Text) + '\u0022', "Pre-input seeking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

            }
            DateTime time;
            if (!DateTime.TryParse(ss_time_input.Text, out time))
            {
                MessageBox.Show("Pre-input seeking format is incorrect. Change it or reset it by double-clicking on it", "Pre-input seeking format error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text == "")
            {
                var a = MessageBox.Show("Format field is empty. Source file extension will be used. Continue?", "Format field blank", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel) return;

            }

            avoid_overw();

            if (avoid_overwriting == true && textBox3.Text != "..\\FFBatch" && checkBox1.CheckState != CheckState.Checked)
            {
                avoid_overwriting = false;
                MessageBox.Show("Multiple folders in input files and a single output folder may lead to file overwriting. Please enable " + '\u0022' + "Recreate source path" + '\u0022' + " to avoid opossible overwritings, or double click on the output path textbox to set it to the default relative path", "Different input folders but single output folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Validated list, start processing
            txt_remain.Text = "Time remaining:";

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();
            }

            if (tried_ok == false)
            {
                BG_Try_preset.RunWorkerAsync();
                return;
            }
            tried_ok = false;

            //Remove test file/folder

            String file_prueba = "";
            String sel_test = listView1.SelectedItems[0].Text;
            file_prueba = sel_test;
            String destino = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

            if (File.Exists(borrar))
            {
                File.Delete(borrar);
            }

            if (Directory.Exists(destino) == true)
            {
                if (Directory.GetFiles(destino).Length == 0)
                {
                    System.IO.Directory.Delete(destino);
                }
            }

            //END Remove test file/folder

            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;

            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView1.Items)
            {
                list_proc.Items.Add((ListViewItem)item.Clone());
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";
            }

            Pg1.Maximum = list_proc.Items.Count;
            listView1.SelectedIndices.Clear();

            Double total_duration = 0;
            Double total_prog = 0;
            int i_dur = 0;

            //Get total duration of files

            foreach (ListViewItem item in listView1.Items)
            {

                if (listView1.Items[i_dur].SubItems[2].Text != "N/A" && listView1.Items[i_dur].SubItems[2].Text != "0:00:00" && listView1.Items[i_dur].SubItems[2].Text != "00:00:00" && listView1.Items[i_dur].SubItems[2].Text != "Pending")
                {
                    total_duration = total_duration + TimeSpan.Parse(listView1.Items[i_dur].SubItems[2].Text).TotalSeconds - TimeSpan.Parse(ss_time_input.Text).TotalSeconds;
                }

                else
                {
                    total_duration = total_duration + 0;
                }
                i_dur = i_dur + 1;
            }

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";
            String remain_time = "0";
            //End get total duration of files

            List<string> list_lines = new List<string>();
            process_glob.StartInfo.Arguments = String.Empty;

            time_n_tasks = 0;
            timer_tasks.Start();


            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                for (int list_index = 0; list_index < listView1.Items.Count; list_index++)
                {

                    System.Threading.Thread.Sleep(50); //Allow kill process to send cancel_queue

                    String file = list_proc.Items[list_index].Text;

                    if (cancel_queue == true)
                    {
                        working = false;

                        this.InvokeEx(f => f.button2.Enabled = true);

                        Enable_Controls();

                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    //int prog = (Pg1.Value * 100 / list_proc.Items.Count);
                    //this.InvokeEx(f => f.textBox5.Text = (prog.ToString() + "%"));
                    //this.InvokeEx(f => f.textBox5.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = file;

                    //Begin Shifting
                    String shifting = "";
                    if (chk_shift.Checked == true)
                    {
                        if (Num_Shift.Value >= 0)
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 0:v -map 1:a ";
                        }
                        else
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 1:v -map 0:a ";
                        }
                    }

                    //End Shifting

                    String change_vol = "";
                    if (chk_vol.Checked == true)
                    {

                        change_vol = "-filter:a " + '\u0022' + "volume=" + vol_ch.Value.ToString() + "dB " + '\u0022' + " ";
                    }


                    //End Change Volume

                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino = file.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            String pre_dest = Path.GetDirectoryName(file);
                            destino = Path.Combine(textBox3.Text, pre_dest.Substring(3, pre_dest.Length - 3));
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }
                    }

                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    add_suffix = "";

                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                    }

                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(file);
                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }

                    textbox_params = textBox1.Text;
                    String file2 = file;
                    if (textbox_params.Contains("%1"))
                    {
                        file2 = file2.Replace("\\", "\\\\\\\\");
                        file2 = file2.Replace(":", "\\\\" + ":");
                        textbox_params = textbox_params.Replace("%1", file2);
                    }

                    String AppParam = pre_input_var + " " + pre_ss + " -i " + "" + '\u0022' + file + '\u0022' + " " + shifting + " " + " -y " + textbox_params + " " + change_vol + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + add_suffix + ext_output1 + '\u0022';

                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    process_glob.StartInfo.FileName = ffm;
                    process_glob.StartInfo.Arguments = AppParam;

                    valid_prog = false;
                    this.InvokeEx(f => f.listView1.Items[list_index].SubItems[4].Text = "Processing");
                    this.InvokeEx(f => f.textBox7.Text = "0%");
                    this.InvokeEx(f => f.textBox7.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());


                    process_glob.StartInfo.RedirectStandardOutput = true;
                    process_glob.StartInfo.RedirectStandardInput = true;
                    process_glob.StartInfo.RedirectStandardError = true;
                    process_glob.StartInfo.UseShellExecute = false;
                    process_glob.StartInfo.CreateNoWindow = true;
                    process_glob.EnableRaisingEvents = true;
                    process_glob.Start();

                    if (mem_prio.SelectedIndex != 2)
                    {
                        System.Threading.Thread.Sleep(50);
                        Change_mem_prio();
                    }

                    this.InvokeEx(f => validate_duration = listView1.Items[list_index].SubItems[2].Text);
                    if (validate_duration != "N/A" && validate_duration != "0:00:00" && validate_duration != "00:00:00" && validate_duration != "Pending")
                    {
                        valid_prog = true;
                    }
                    String err_txt = "";
                    Double interval = 0;

                    while (!process_glob.StandardError.EndOfStream)
                    {
                        err_txt = process_glob.StandardError.ReadLine();
                        list_lines.Add(err_txt);

                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog == true)
                            {

                                this.InvokeEx(f => durat_n = TimeSpan.Parse(listView1.Items[list_index].SubItems[2].Text).TotalSeconds - TimeSpan.Parse(ss_time_input.Text).TotalSeconds);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;


                                Double percent = (sec_prog * 100 / durat_n);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                Double percent_tot = (total_prog * 100 / total_duration);
                                int percent_tot_2 = Convert.ToInt32(percent_tot);

                                if (percent_tot_2 <= 100)
                                {
                                    this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                    this.InvokeEx(f => f.Pg1.Refresh());
                                    this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                    this.InvokeEx(f => f.textBox5.Refresh());
                                }
                                if (percent2 <= 100)
                                {
                                    this.InvokeEx(f => f.pg_current.Value = percent2);
                                    this.InvokeEx(f => f.pg_current.Refresh());
                                    this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                                    this.InvokeEx(f => f.textBox4.Refresh());

                                }

                                if (cancel_queue == false)
                                {
                                    //Estimated remaining time

                                    remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                                    remain_time = remain_time.Replace("x", String.Empty);
                                    Double timing1 = 0;

                                    if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                    {
                                        timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                                    }
                                    else
                                    {
                                        timing1 = Math.Round(Double.Parse(remain_time), 2);
                                    }

                                    Decimal timing = (decimal)timing1;
                                    Decimal total_dur_dec = Convert.ToDecimal(total_duration);
                                    Decimal total_prog_dec = Convert.ToDecimal(total_prog);
                                    Decimal remain_secs = 0;
                                    if (timing > 0)
                                    {
                                        remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                                    }

                                    if (remain_secs > 60)
                                    {
                                        remain_secs = remain_secs + 60;
                                    }

                                    String remain_from_secs = "";

                                    TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                                    remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                                       t.Hours,
                                      t.Minutes);

                                    if (remain_secs >= 43200)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Math.Round(remain_secs / 3600).ToString() + " hours");
                                    }

                                    if (remain_secs >= 3600 && remain_secs < 43200)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                                    }

                                    if (remain_secs < 3600 && remain_secs >= 600)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                                    }
                                    if (remain_secs < 600 && remain_secs >= 120)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                                    }

                                    if (remain_secs <= 59 && remain_secs != 0)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                                    }
                                    if (remain_secs == 0)
                                    {
                                        this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + "About to finish");
                                    }

                                }
                                //End remaining time
                            }
                        }
                        //Read output, get progress
                        this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                        this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                    }
                    process_glob.WaitForExit();

                    process_glob.StartInfo.Arguments = String.Empty;

                    this.InvokeEx(f => f.pg_current.Value = 100);
                    this.InvokeEx(f => f.textBox4.Text = "100%");
                    list_lines.Add("");
                    list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    list_lines.Add("");


                    if (process_glob.ExitCode == 0)
                    {
                        this.InvokeEx(f => f.listView1.Items[list_index].SubItems[4].Text = "Success");
                        this.InvokeEx(f => f.listView1.Items[list_index].BackColor = Control.DefaultBackColor);
                        this.InvokeEx(f => f.listView1.Items[list_index].EnsureVisible());

                    }
                    else
                    {
                        this.InvokeEx(f => f.listView1.Items[list_index].SubItems[4].Text = "Error");
                        this.InvokeEx(f => f.listView1.Items[list_index].BackColor = Color.PaleGoldenrod);

                    }

                    //prog = (Pg1.Value * 100 / list_proc.Items.Count);


                    if (list_index == list_proc.Items.Count - 1)
                    {

                        this.InvokeEx(f => f.Pg1.Value = 100);
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        working = false;
                        //Save log
                        string[] array_err = list_lines.ToArray();
                        String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                        SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                        SaveFile.WriteLine("-------------------------------");
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

                        //Automatic shutdown check
                        if (chkshut.Checked && cancel_queue == false)
                        {

                            Disable_Controls();
                            this.InvokeEx(f => f.chkshut.Enabled = false);
                            this.InvokeEx(f => f.btn_pause.Enabled = false);
                            this.InvokeEx(f => f.Timer_apaga.Start());
                            this.InvokeEx(f => f.TopMost = true);
                            this.InvokeEx(f => f.TB1.Enabled = true);
                            this.InvokeEx(f => f.TB1.Visible = true);
                            this.InvokeEx(f => f.button10.Enabled = true);
                            this.InvokeEx(f => f.button10.Visible = true);
                            this.InvokeEx(f => f.button20.Enabled = false);
                            this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                            notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                            //String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                            //foreach (string file_s in Directory.GetFiles(destino_test))
                            //{
                            //File.Delete(file_s);
                            //}
                            //System.IO.Directory.Delete(destino_test);
                            return;

                        }
                        //End shutdown check
                        else
                        {
                            if (cancel_queue == false)
                            {
                                if (Form.ActiveForm == null)
                                {
                                    notifyIcon1.BalloonTipText = "Queue processing completed";
                                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                    notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                    notifyIcon1.ShowBalloonTip(0);
                                }

                                if (checkBox3.Checked)
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
                            else
                            {
                                this.InvokeEx(f => f.textBox5.Text = "100%");
                                this.InvokeEx(f => MessageBox.Show("Queue processing aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));

                            }
                        }

                    }
                }

                this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                Enable_Controls();

                //String borrar = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                //foreach (string file in Directory.GetFiles(destino_test))
                //{
                //File.Delete(file);
                //}
                //System.IO.Directory.Delete(destino_test);

            }).Start();

        }

        private void process_Exited(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
            //Begin exit code 

            //End exit code

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            n_threads.Value = Environment.ProcessorCount;
            n_threads.Minimum = 2;
            n_threads.Maximum = Environment.ProcessorCount;
            pictureBox1.BackColor = Color.Transparent;
                        
            mem_prio.Items.AddRange(combo_prio.Items.Cast<Object>().ToArray());
            btn_n_urls.Image = button17.Image;
            combo_prio.Text = "Priority";
            
            //Load priority

            String f_prio = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_priority.ini";
            if (File.Exists(f_prio))
            {
                String saved_prio = File.ReadAllText(f_prio);
                if (saved_prio != String.Empty)
                {
                     combo_prio.SelectedIndex = Convert.ToInt16(saved_prio);
                }
            }
                else
                 {
                     combo_prio.SelectedIndex = 2;
                 }

            //End load priority
            
            //Only one instance running
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {

                this.Hide();
                MessageBox.Show("Program is already running", "Program already running", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit();
                return;
            }

            combo_ext_m3u.SelectedIndex = 0;
            
            chk_subfolders.FlatAppearance.CheckedBackColor = Color.FromArgb(255, 225, 235, 251);
            combo_ext.SelectedIndex = 0;
            Combo_def_sub_mux.SelectedIndex = 0;
            Combo_ext_sub_mux.SelectedIndex = 0;
            notifyIcon1.Visible = false;
            Create_Tooltips();
            listView1.LabelWrap = true;

            String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
            if (!File.Exists(ffm))
            {
                MessageBox.Show("ffmpeg.exe was not found in application path.", "ffmpeg.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            String ffm2 = System.IO.Path.Combine(Application.StartupPath, "mediainfo.exe");
            if (!File.Exists(ffm2))
            {
                MessageBox.Show("mediainfo.exe was not found in application path.", "mediainfo.exe not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            //Read configuration

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            comboBox1.Items.Add("Default parameters");
            comboBox1.SelectedIndex = 0;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Default saved parameters");
            String path_s = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_path.ini";
            if (File.Exists(path_s))
            { 
            String saved_path = File.ReadAllText(path_s);
                if (saved_path != String.Empty && Directory.Exists(saved_path) == true)
                {
                    textBox3.Text = saved_path;
                    textBox3.BackColor = textBox1.BackColor;
                }
                else
                {
                    File.Delete(path_s);
                }

            }
            //Check ffbatch.ini if not found
            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";

            int linea = 0;
            comboBox1.SelectedIndex = comboBox1.FindString(("Default saved parameters"));

            foreach (string line in File.ReadLines(path))
            {
                linea = linea + 1;

                if (line == "yes")

                {
                    checkBox3.CheckState = CheckState.Checked;
                }

                if (line == "no")
                {
                    checkBox3.CheckState = CheckState.Unchecked;
                }

                if (line == "Vs")
                {
                    chk_suffix.CheckState = CheckState.Checked;
                }
                if (line == "Vn")
                {
                    chk_suffix.CheckState = CheckState.Unchecked;
                }

                if (line == "grid_yes")
                {
                    listView1.GridLines = true;
                    listView2.GridLines = true;
                    listView3.GridLines = true;
                }
                if (line == "grid_no")
                {
                    listView1.GridLines = false;
                    listView2.GridLines = false;
                    listView3.GridLines = false;
                }

                if (line == "keep_yes")
                {
                    checkBox1.CheckState = CheckState.Checked;
                }
                if (line == "keep_no")
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                }

                if (line == "subf_yes")
                {
                    chk_subfolders.CheckState = CheckState.Checked;
                }
                if (line == "subf_no")
                {
                    chk_subfolders.CheckState = CheckState.Unchecked;
                }


                if (linea == 1)
                {
                    textBox1.Text = line;
                }

                if (linea == 2)
                {
                    textBox2.Text = line;
                }

                if (line.Contains("PR: "))
                {
                    comboBox1.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));
                }

            }

            //End read configuration

            String[] arguments = Environment.GetCommandLineArgs();
            String[] file_drop = arguments.Skip(1).ToArray();

            //Sendto parameters
            if (file_drop.Count() != 0)
            {

                change_tab_1 = false;
                change_tab_2 = false;

                List<string> files2 = new List<string>();

                int num_drop = 0;

                foreach (String dropped in file_drop)
                {

                    if (File.Exists(dropped))
                    {
                        files2.Add(dropped);
                        num_drop = files2.Count();
                    }

                    else
                    {
                        if (Directory.Exists(dropped))
                        {
                            if (add_subfs == false)
                            {

                                foreach (String file in Directory.GetFiles(dropped))
                                {
                                    if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden))
                                    {
                                        files2.Add(file);
                                        num_drop = num_drop + 1;
                                    }
                                }
                            }
                            else
                            {
                                try
                                {
                                    foreach (string f in Directory.GetFiles(dropped, "*.*", SearchOption.AllDirectories))
                                    {
                                        if (!File.GetAttributes(f).HasFlag(FileAttributes.Hidden))
                                        {
                                            files2.Add(f);
                                            num_drop = num_drop + 1;
                                        }
                                    }
                                }
                                catch (System.Exception excpt)
                                {
                                    var a = MessageBox.Show("Error: " + excpt.Message, "Access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }

                            }
                        }
                    }
                }


                if (num_drop >= 5000)
                {
                    var a = MessageBox.Show("Adding " + num_drop + " files could take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        return;
                    }
                }

                files_to_add = files2;
                canceled_file_adding = false;
                btn_cancel_add.Enabled = true;
                btn_cancel_add.Visible = true;
                btn_cancel_add.Refresh();
                BG_Files.RunWorkerAsync();

                //End Sendto files
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

            DialogResult a = MessageBox.Show("Do you wish to overwrite default saved parameters?", "Confirm operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                String[] lines = System.IO.File.ReadAllLines(path);

                for (int i = 0; i < lines.Count() - 1; i++)
                {
                    if (i == 0)
                    {
                        lines[i] = textBox1.Text;

                    }
                    if (i == 1)
                    {
                        lines[i] = textBox2.Text;

                    }
                    if (i == 2)
                    {
                        if (checkBox3.Checked)
                        {
                            lines[i] = ("yes");
                        }
                        else
                        {
                            lines[i] = ("no");
                        }

                    }
                    if (i == 3)
                    {
                        if (chk_suffix.Checked)
                        {
                            lines[i] = ("Vs");
                        }
                        else
                        {
                            lines[i] = ("Vn");
                        }
                    }

                    if (i == 4)
                    {
                        if (listView1.GridLines == true)
                        {
                            lines[i] = ("grid_yes");
                        }
                        else
                        {
                            lines[i] = ("grid_no");
                        }
                    }
                    if (i == 5)
                    {
                        if (checkBox1.Checked)
                        {
                            lines[i] = ("keep_yes");
                        }
                        else
                        {
                            lines[i] = ("keep_no");
                        }
                    }

                    if (i == 6)
                    {
                        if (chk_subfolders.Checked)
                        {
                            lines[i] = ("subf_yes");
                        }
                        else
                        {
                            lines[i] = ("subf_no");
                        }
                    }
                }

                System.IO.File.WriteAllLines(path, lines);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ss_time_input.Text = "0:00:00.000";
            txt_pre_input.Text = "";
            txt_pre_input.BackColor = Control.DefaultBackColor;
            txt_suffix.Text = "_FFB";

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Default saved parameters");
            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            int linea = 0;

            comboBox1.SelectedIndex = comboBox1.FindString(("Default saved parameters"));

            foreach (string line in File.ReadLines(path))
            {
                linea = linea + 1;

                if (line == "yes")

                {
                    checkBox3.CheckState = CheckState.Checked;
                }

                if (line == "no")
                {
                    checkBox3.CheckState = CheckState.Unchecked;
                }

                if (line == "Vs")
                {
                    chk_suffix.CheckState = CheckState.Checked;
                }

                if (line == "Vn")
                {
                    chk_suffix.CheckState = CheckState.Unchecked;
                }

                if (line == "grid_yes")
                {
                    listView1.GridLines = true;
                    listView2.GridLines = true;
                    listView3.GridLines = true;
                }
                if (line == "grid_no")
                {
                    listView1.GridLines = false;
                    listView2.GridLines = false;
                    listView3.GridLines = false;
                }

                if (line == "keep_yes")
                {
                    checkBox1.CheckState = CheckState.Checked;
                }
                if (line == "keep_no")
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                }

                if (line == "subf_yes")

                {
                    chk_subfolders.CheckState = CheckState.Checked;
                    chk_subfolders.Refresh();
                }

                if (line == "subf_no")
                {
                    chk_subfolders.CheckState = CheckState.Unchecked;
                    chk_subfolders.Refresh();
                }

                if (linea == 1)
                {
                    textBox1.Text = line;
                }

                if (linea == 2)
                {
                    textBox2.Text = line;
                }

                if (line.Contains("PR: "))
                {
                    comboBox1.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Control.DefaultBackColor;
            }
            else
            {
                textBox2.BackColor = Color.LightYellow;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lbl_size.Text = "";
            txt_remain.Text = "Time remaining:";
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            dg1.Rows.Clear();
            listBox4.Items.Clear();
            listBox4.Items.Add("");
            listBox4.Items.Add("");
            listBox4.Items.Add("                                                                                           FFmpeg standard output");
            listBox4.Items.Add("");
            textBox7.Visible = false;
            ss_time_input.Text = "0:00:00.000";
            txt_pre_input.Text = "";
            txt_pre_input.BackColor = Control.DefaultBackColor;
            txt_folder_subs.Text = String.Empty;
            txt_folder_subs.BackColor = Control.DefaultBackColor;
            label9.Text = "";
            label12.Text = "";
            Pg1.Value = 0;
            pg_current.Value = 0;
            textBox4.Text = "0%";
            textBox5.Text = "0%";
            list_tracks.Items.Clear();
            combo_item_lang_2.SelectedIndex = -1;
            Combo_sub_lang_mux.SelectedIndex = -1;
            lbl_tr_n.Text = "Tracks: 0";
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 0)
            {

                txt_pre_input.BackColor = Control.DefaultBackColor;

                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch")))
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
                }

                if (!File.Exists(path))
                {

                    File.WriteAllText(path, "-c copy" + "\n" + "mp4" + "\n" + "yes" + "\n" + "Vs" + "\n" + "grid_yes" + "\n" + "keep_no" + "\n" + "subf_no" + "\n" + "PR: Video: MP4 Stream copy & -c copy % mp4" + "\n" + "PR: Video: Convert audio track to AAC HQ 2 channels & -c:v copy -c:a aac -cutoff 20K -b:a 256K -ac 2 % mkv" + "\n" + "PR: Video: Convert to H264 HQ + Source Audio & -c:v libx264 -crf 20 -c:a copy % mkv" + "\n" + "PR: Video: Resize 1280x720 H264-AAC & -c:v libx264 -crf 23 -vf scale=1280:720 -c:a aac -b:a 128K % mp4" + "\n" + "PR: Audio: Convert to MP3 VBR HQ 2 ch embedded cover & -c:v copy -c:a libmp3lame -qscale:a 0 -ac 2  % mp3" + "\n" + "PR: Audio: Convert to FLAC 16/44,1KHz 2 channels & -vn -c:a flac -ar 44100 -sample_fmt s16 -ac 2 % flac" + "\n" + "PR: Record screen at 24 fps using default gdigrab options to MKV & -r 24 % mkv" + "\n" + "PR: Video: Remove subtitles to MP4 & -map 0 -c copy -sn % mp4");
                }
                int linea = 0;
                foreach (string line in File.ReadLines(path))
                {

                    linea = linea + 1;

                    if (linea == 1)
                    {
                        textBox1.Text = line;
                    }

                    if (linea == 2)
                    {
                        textBox2.Text = line;
                    }

                    if (line == "yes")

                    {
                        checkBox3.CheckState = CheckState.Checked;
                    }

                    if (line == "no")
                    {
                        checkBox3.CheckState = CheckState.Unchecked;
                    }



                }

            }
            else
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";

                foreach (string line in File.ReadLines(path))
                {
                    if (line.Contains(comboBox1.SelectedItem.ToString()))
                    {
                        int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");

                        textBox2.Text = line.Substring(line.LastIndexOf("%") + 2);
                        textBox1.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                    }
                }
            }

        }

        private void ctm1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {


        }

        private void cti1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                String fullPath = listView1.SelectedItems[0].Text;
                String destino = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
                if (Directory.Exists(destino))
                {
                    Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = "explorer.exe";
                    process.StartInfo.Arguments = destino;
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                else
                {
                    MessageBox.Show("Path was not found", "Folder not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cti2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                String fullPath = listView1.SelectedItems[0].Text;
                if (File.Exists(fullPath))
                {
                    System.Diagnostics.Process.Start(fullPath);
                }
                else
                {
                    MessageBox.Show("File was not found", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ctm1_Opening(object sender, CancelEventArgs e)
        {
            cti6.Visible = false;
            if (working == true)
            {
                if (multi_running == false)
                {
                    e.Cancel = true;
                }
                else
                {
                    cti1.Enabled = false;
                    cti2.Enabled = false;
                    cti3.Enabled = false;
                    cti4.Enabled = false;
                    cti5.Enabled = false;
                    ctdel.Enabled = false;
                    ctm_add_files.Enabled = false;
                    ctm_add_folder.Enabled = false;

                    if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].SubItems[4].Text != "Aborted" && listView1.SelectedItems[0].SubItems[4].Text != "Queued" && listView1.SelectedItems[0].SubItems[4].Text != "Aborting")
                    {
                        cti6.Visible = true;
                        cti6.Text = "Stop processing " + "\u0022" + Path.GetFileName(listView1.SelectedItems[0].Text) + "\u0022";
                    }
                }
                return;
            }


            if (listView1.SelectedItems.Count > 0)
            {

                cti1.Enabled = true;
                cti2.Enabled = true;

                String fullPath = listView1.SelectedItems[0].Text;
                String destino = "";
                if (textBox3.Text == "..\\FFBatch")
                {
                    destino = fullPath.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                }
                else
                {
                    destino = textBox3.Text;

                }

                if (Directory.Exists(destino))
                {
                    cti3.Enabled = true;
                }

                else
                {
                    cti3.Enabled = false;
                }

                cti4.Enabled = true;
                cti5.Enabled = true;
                ctdel.Enabled = true;

                ctm_add_files.Visible = false;
                ctm_add_folder.Visible = false;



                toolStripSeparator2.Visible = false;

            }

            if (listView1.Items.Count == 0)
            {
                e.Cancel = false;
                ctm_add_files.Visible = true;
                ctm_add_folder.Visible = true;

                toolStripSeparator2.Visible = true;
                cti1.Enabled = false;
                cti2.Enabled = false;
                cti3.Enabled = false;
                cti4.Enabled = false;
                cti5.Enabled = false;
                ctdel.Enabled = false;
                toolStripSeparator2.Visible = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Double weight = 0;
            Double dg_multi_prog = 0;
            start_total_time = start_total_time + 1;

            foreach (ListViewItem item_p in listView1.Items)
            {
                weight = TimeSpan.Parse(item_p.SubItems[2].Text).TotalSeconds / total_multi_duration;
                if (item_p.SubItems[4].Text.Contains("%") == true && cancelados_paralelos == false)
                {
                    dg_multi_prog = dg_multi_prog + (Convert.ToDouble(item_p.SubItems[4].Text.Replace("%", "")) * weight) * listView1.Items.Count;

                }

                if (item_p.SubItems[4].Text == "Success" || item_p.SubItems[4].Text == "Failed" || item_p.SubItems[4].Text == "Aborted" || item_p.SubItems[4].Text == "Aborting")
                {
                    dg_multi_prog = dg_multi_prog + (100 * weight * listView1.Items.Count);
                }
            }

            if (time_n_tasks > 1)
            {
                Pg1.Value = Convert.ToInt32(dg_multi_prog);
                textBox5.Text = (Pg1.Value / list_global_proc.Items.Count).ToString() + "%";
                textBox5.Refresh();
            }
            else
            {
                Pg1.Value = 0;
                textBox5.Text = "0%";
                textBox5.Refresh();
            }

            //MessageBox.Show("First: " + multi_interval.ToString() + " Last: " + (Pg1.Value / list_global_proc.Items.Count));
            //interval = (Pg1.Value / list_global_proc.Items.Count) - multi_interval;

            
            if (Pg1.Value / listView1.Items.Count > 0 && start_total_time > 4)
            {
                               

                Double remain_secs = time_n_tasks * 100 / (Pg1.Value / listView1.Items.Count) - start_total_time;
                String remain_string = String.Empty;
                
                TimeSpan t = TimeSpan.FromSeconds(remain_secs);
                remain_string = string.Format("{0:D2}h:{1:D2}",
                t.Hours,
                t.Minutes);

                if (remain_secs >= 43200)
                {
                    txt_remain.Text = "Time remaining: " + Math.Round(remain_secs / 3600).ToString() + " hours";
                    
                }

                if (remain_secs >= 3600 && remain_secs < 43200)
                {
                    txt_remain.Text = "Time remaining: " + remain_secs + " min";
                    
                }

                if (remain_secs < 3600 && remain_secs >= 600)
                {
                    txt_remain.Text = "Time remaining: " + remain_string.Substring(remain_string.LastIndexOf(":") + 1, 2) + " minutes";
                    
                }
                if (remain_secs < 600 && remain_secs >= 120)
                {
                    txt_remain.Text = "Time remaining: " + remain_string.Substring(remain_string.LastIndexOf(":") + 2, 1) + " minutes";
                    
                }

                if (remain_secs < 120 && remain_secs > 59)
                {
                    txt_remain.Text = "Time remaining: " + "About 1 minute";

                }

                if (remain_secs <= 59)
                {
                    txt_remain.Text = "Time remaining: < 1 minute";
                    
                }
                if (remain_secs <= 0)
                {
                    txt_remain.Text = "Time remaining: Almost done";
                    
                }
                txt_remain.Refresh();
                this.InvokeEx(f => f.txt_remain.Refresh());
            }
            else
            {
                txt_remain.Text = "Time remaining: Calculating...";
                txt_remain.Refresh();
            }            
        }

        private void cti3_Click(object sender, EventArgs e)
        {
            String fullPath = listView1.SelectedItems[0].Text;
            String destino = "";
            if (textBox3.Text == "..\\FFBatch")
            {
                destino = fullPath.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
            }
            else
            {
                destino = textBox3.Text;

            }

            if (Directory.Exists(destino))
            {

                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "explorer.exe";
                process.StartInfo.Arguments = destino;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
            }
            else
            {
                MessageBox.Show("Folder was not found", "Folder not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cti4_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {

                String file1 = System.IO.Path.Combine(Application.StartupPath + "\\", "mediainfo.exe");
                String fullPath = "\u0022" + listView1.SelectedItems[0].Text + "\u0022";
                String testPath = listView1.SelectedItems[0].Text;


                if (!File.Exists(testPath))
                {

                    MessageBox.Show("File was not found", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = file1;
                process.StartInfo.Arguments = fullPath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                String salida = "";

                Form frmInfo = new Form();
                frmInfo.Name = "Multimedia information";
                frmInfo.Icon = this.Icon;
                frmInfo.Height = 724;
                frmInfo.Width = 496;
                frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
                frmInfo.MaximizeBox = false;
                frmInfo.MinimizeBox = false;
                frmInfo.BackColor = this.BackColor;

                var fuente_list = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Regular);

                ListView LB1 = new ListView();
                LB1.Parent = frmInfo;
                LB1.Left = 14;
                LB1.Top = 56;
                LB1.Height = 591;
                LB1.Width = 447;
                //LB1.Font = fuente_list;
                LB1.View = View.Details;
                LB1.FullRowSelect = true;
                LB1.GridLines = true;
                LB1.Columns.Add("", 130);
                LB1.Columns.Add("", 295);
                LB1.HeaderStyle = ColumnHeaderStyle.None;
                LB1.Refresh();

                TextBox titulo = new TextBox();
                titulo.Parent = frmInfo;
                titulo.Top = 6;
                titulo.Left = 14;
                titulo.Width = 448;
                titulo.TabIndex = 0;
                var fuente = new System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold);
                titulo.BackColor = this.BackColor;

                titulo.Font = fuente;
                titulo.BorderStyle = BorderStyle.Fixed3D;
                titulo.TextAlign = HorizontalAlignment.Center;
                titulo.ReadOnly = true;

                titulo.Text = "MULTIMEDIA INFORMATION";

                Button boton_ok = new Button();
                boton_ok.Parent = frmInfo;
                boton_ok.Left = 73;
                boton_ok.Top = 650;
                boton_ok.Width = 330;
                boton_ok.Height = 27;
                boton_ok.Text = "Close window";
                boton_ok.Click += new EventHandler(boton_ok_Click);

                Button btn_next = new Button();
                btn_next.Parent = frmInfo;
                btn_next.Left = 404;
                btn_next.Top = 650;
                btn_next.Width = 59;
                btn_next.Height = 27;
                btn_next.Text = "Next  -->";
                btn_next.Click += new EventHandler(btn_next_Click);

                Button btn_prev = new Button();
                btn_prev.Parent = frmInfo;
                btn_prev.Left = 14;
                btn_prev.Top = 650;
                btn_prev.Width = 59;
                btn_prev.Height = 27;
                btn_prev.Text = "<-- Prev ";
                btn_prev.Click += new EventHandler(btn_prev_Click);


                String fichero = Path.GetFileName(listView1.SelectedItems[0].Text);
                TextBox titulo2 = new TextBox();
                titulo2.Parent = frmInfo;
                titulo2.Top = 33;
                titulo2.Left = 14;
                titulo2.Width = 440;
                titulo2.BackColor = this.BackColor;

                var fuente2 = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Regular);
                titulo2.Font = fuente2;
                titulo2.BorderStyle = BorderStyle.None;
                titulo2.TextAlign = HorizontalAlignment.Center;
                titulo2.ReadOnly = true;

                titulo2.Text = (fichero);
                int indx = 0;
                var font_item = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                do
                {
                    salida = process.StandardOutput.ReadLine();

                    if (salida.Contains("Format  ") || salida.Contains("Bit depth") || salida.Contains("General") || salida.Contains("Bit rate") || salida.Contains("bit rate") || salida.Contains("Duration") || salida == "" || salida == "Video" || salida.Contains("Audio") || salida.Contains("Frame rate  ") || salida.Contains("Width") || salida.Contains("Height") || salida.Contains("Color space") || salida.Contains("Chroma subsampling") || salida.Contains("Channel(s)") || salida.Contains("Channel positions") || salida.Contains("Maximum bit rate") || salida.Contains("Sampling rate ") || salida.Contains("File size") || salida.Contains("Format profile") || salida.Contains("Display aspect ratio") || salida.Contains("Stream size") || salida.Contains("Text") || salida.Contains("Language"))

                    {

                        int derecha = 0;

                        if (!salida.Contains("  : "))
                        {
                            LB1.Items.Add(salida.ToUpper());
                            LB1.Items[indx].Font = font_item;
                            LB1.Items[indx].ForeColor = Color.DarkBlue;
                            if (salida != String.Empty)
                            {
                                LB1.Items[indx].SubItems[0].BackColor = Color.FromArgb(255, 220, 238, 255);
                            }

                            indx = indx + 1;


                        }

                        else

                        {
                            if (!salida.Contains("SPF"))
                            {
                                LB1.Items.Add(salida.Substring(0, salida.LastIndexOf("  : ")).Replace("  ", ""));
                                derecha = salida.Length - (salida.LastIndexOf("  :"));
                                LB1.Items[indx].SubItems.Add(salida.Substring(salida.LastIndexOf("  :") + 4, derecha - 4).Replace("kb", "Kb"));
                                indx = indx + 1;

                            }
                        }
                    }


                }

                while (!process.StandardOutput.EndOfStream);


                for (int x = 0; x < 2; x++)
                {
                    LB1.Items.RemoveAt(LB1.Items.Count - 1);
                }

                int duraciones = 0;
                String elemento = "";
                for (int i = 0; i < LB1.Items.Count; i++)
                {

                    elemento = LB1.Items[i].Text;

                    if (elemento.Contains("Duration"))
                    {

                        duraciones = duraciones + 1;

                        if (duraciones > 1)
                        {
                            LB1.Items.RemoveAt(i);
                        }
                    }
                }

                foreach (ListViewItem item in LB1.Items)
                {
                    if (item.Text == String.Empty)
                    {
                        item.Remove();
                    }
                }

                frmInfo.StartPosition = FormStartPosition.CenterParent;
                frmInfo.ShowDialog();

            }

            else

            {
                MessageBox.Show("No item was selected", "No item selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.Description = "Adding subfolders not enabled";
            if (add_subfs == true)
            {
                folderBrowserDialog1.Description = "Adding subfolders enabled";
            }

            folderBrowserDialog1.ShowNewFolderButton = false;

            change_tab_1 = false;
            change_tab_2 = false;

            if (listView1.Items.Count == 0)
            {
                list_not_empty = false;
            }
            else
            {
                list_not_empty = true;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                change_tab_1 = true;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                change_tab_2 = true;
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {

                List<string> files2 = new List<string>();


                foreach (string file in Directory.GetFiles(folderBrowserDialog1.SelectedPath))
                {
                    if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden))
                    {
                        files2.Add(file);
                    }
                }

                int num_drop = files2.Count();

                if (add_subfs == true)
                {
                    string[] dirs = Directory.GetDirectories(folderBrowserDialog1.SelectedPath);

                    foreach (string ds in dirs)
                    {
                        try
                        {
                            foreach (string f in Directory.GetFiles(ds, "*.*", SearchOption.AllDirectories))
                            {
                                if (!File.GetAttributes(f).HasFlag(FileAttributes.Hidden))
                                {
                                    files2.Add(f);
                                    num_drop = num_drop + 1;
                                }
                            }
                        }
                        catch (System.Exception excpt)
                        {
                            var a = MessageBox.Show("Error: " + excpt.Message + " Continue?", "Access error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            if (a == DialogResult.Cancel) return;
                        }
                    }

                }

                if (num_drop == 0)
                {
                    var a = MessageBox.Show("Folder is empty", "Folder empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DriveInfo driveInfo = new DriveInfo(folderBrowserDialog1.SelectedPath);
                if (num_drop >= 1000 && driveInfo.DriveType == DriveType.Network)
                {

                    var a = MessageBox.Show("Adding " + num_drop + " files from a network drive can take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        return;
                    }

                }
                if (num_drop >= 2800 && driveInfo.DriveType != DriveType.Network)
                {

                    var a = MessageBox.Show("Adding " + num_drop + " files can take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                files_to_add = files2;
                canceled_file_adding = false;
                btn_cancel_add.Enabled = true;
                btn_cancel_add.Visible = true;
                btn_cancel_add.Refresh();

                BG_Files.RunWorkerAsync();

            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void ctdel_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.BeginUpdate();
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    listView1.Items.Remove(item);

                    foreach (ListViewItem item2 in listView3.Items)
                    {
                        if (item2.Text == item.Text)
                        {
                            listView3.Items.RemoveAt(item2.Index);
                        }
                    }
                }
                listView1.EndUpdate();
                label9.Text = "Items: " + listView1.Items.Count;

                calc_total_dur();
                calc_list_size();
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start("https://sourceforge.net/projects/ffmpeg-batch/");
        }


        private void ctm_add_files_Click(object sender, EventArgs e)
        {
            this.button1.PerformClick();
        }

        private void ctm_add_folder_Click(object sender, EventArgs e)
        {
            this.button6.PerformClick();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {

                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {

            }
            if (comboBox1.Text == "Default saved parameters")
            {
                MessageBox.Show("Select a preset different from default, or write a new preset description", "Select a different preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            String path_check = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";


            foreach (string line in File.ReadLines(path_check))
            {


                if (line.LastIndexOf("&") >= 0)
                {

                    if (line.Substring(4, line.LastIndexOf("&") - 5) == comboBox1.Text)
                    {
                        MessageBox.Show("Preset already exists. Change description before saving as a new one.", "Add nuew preset", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }


            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            string createText = "PR: " + comboBox1.Text + " & " + textBox1.Text + " % " + textBox2.Text;
            File.AppendAllText(path, Environment.NewLine + createText);


            comboBox1.Items.Clear();
            comboBox1.Items.Add("Default saved parameters");
            String path2 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            int linea = 0;

            foreach (string line in File.ReadLines(path2))
            {
                linea = linea + 1;

                if (line == "yes")

                {
                    checkBox3.CheckState = CheckState.Checked;
                }

                if (line == "no")
                {
                    checkBox3.CheckState = CheckState.Unchecked;
                }

                if (linea == 1)
                {
                    textBox1.Text = line;
                }

                if (linea == 2)
                {
                    textBox2.Text = line;
                }

                if (line.Contains("PR: "))
                {
                    comboBox1.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));

                }
            }


            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            MessageBox.Show("The new preset has been saved.", "Preset saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("Default preset can not be removed", "Preset can't be removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            var a = MessageBox.Show("Do you wish to remove the current preset?", "Confirm action", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {

                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                File.Create(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch2.txt").Dispose();
                String path2 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch2.txt";

                int ind = 0;
                foreach (string line in File.ReadLines(path))
                {
                    ind = ind + 1;
                    String linea_sin = String.Empty;

                    if (!line.Contains("PR: "))
                    {
                        linea_sin = line + Environment.NewLine;
                        File.AppendAllText(path2, linea_sin);
                    }

                    if (line.LastIndexOf("&") >= 0)
                    {

                        if (line.Substring(4, line.LastIndexOf("&") - 5) != comboBox1.SelectedItem.ToString())
                        {

                            if (ind <= File.ReadLines(path).Count() - 1)
                            {
                                linea_sin = line + Environment.NewLine;
                                File.AppendAllText(path2, linea_sin);
                            }
                            else
                            {
                                File.AppendAllText(path2, line);
                            }

                        }
                    }

                }
                File.Delete(path);
                File.Copy(path2, path);
                File.Delete(path2);

                String path1 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                comboBox1.Items.Clear();
                comboBox1.Items.Add("Default saved parameters");
                int linea = 0;

                foreach (string line in File.ReadLines(path1))
                {
                    linea = linea + 1;

                    if (line == "yes")

                    {
                        checkBox3.CheckState = CheckState.Checked;
                    }

                    if (line == "no")
                    {
                        checkBox3.CheckState = CheckState.Unchecked;
                    }

                    if (linea == 1)
                    {
                        textBox1.Text = line;
                    }

                    if (linea == 2)
                    {
                        textBox2.Text = line;
                    }

                    if (line.Contains("PR: "))
                    {
                        comboBox1.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));

                    }

                }
                comboBox1.SelectedIndex = 0;


            }

        }


        private void boton_ok_Click(object sender, System.EventArgs e)
        {

            Form.ActiveForm.Close();

        }


        private void btn_next_Click(object sender, System.EventArgs e)
        {

            Form.ActiveForm.Close();
            if (listView1.SelectedItems[0].Index < listView1.Items.Count - 1)
            {
                listView1.Items[listView1.SelectedIndices[0] + 1].Selected = true;
                listView1.Items[listView1.SelectedIndices[0]].Selected = false;
                listView1.Select();
                cti4.PerformClick();
            }

        }

        private void btn_prev_Click(object sender, System.EventArgs e)
        {

            Form.ActiveForm.Close();
            if (listView1.SelectedItems[0].Index > 0)
            {
                int selected_item = listView1.SelectedIndices[0];
                //listView1.Items[listView1.SelectedIndices[0] - 2].Selected = true;
                listView1.Items[selected_item].Selected = false;
                listView1.Items[selected_item - 1].Selected = true;
                listView1.Select();
                cti4.PerformClick();
            }

        }

        private void boton_ok_wave_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void boton_kill_Click(object sender, System.EventArgs e)
        {
            cancelados_paralelos = true;
            cancel_queue = true;


            Form.ActiveForm.Close();

            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
                p.Kill();
            System.Threading.Thread.Sleep(250);
            Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
            foreach (Process p2 in localByName2)
                p2.Kill();

        }
        private void boton_ok_ff_Click(object sender, System.EventArgs e)
        {

            Form.ActiveForm.Close();

        }

        private void LB1_o_Click(object sender, System.EventArgs e)
        {

            //Form.ActiveForm.Close();

        }


        private void Timer_apaga_Tick(object sender, EventArgs e)
        {
            tiempo_apaga = tiempo_apaga - 1;

            if (tiempo_apaga % 2 == 0)

            {
                TB1.ForeColor = Color.Black;
                TB2.ForeColor = Color.Black;
            }
            else
            {
                TB1.ForeColor = Color.DarkRed;
                TB2.ForeColor = Color.DarkRed;


            }

            TB1.Text = "COMPUTER WILL POWER OFF IN " + (tiempo_apaga / 2).ToString() + " seconds";
            TB2.Text = "SHUTTING DOWN IN " + (tiempo_apaga / 2).ToString() + " seconds";

            if (tiempo_apaga == 0)
            {
                Process apagar = new System.Diagnostics.Process();
                apagar.StartInfo.FileName = "shutdown.exe";
                apagar.StartInfo.Arguments = "-s -t 60";
                apagar.StartInfo.CreateNoWindow = true;
                apagar.Start();
                //Application.Exit();
                TB1.Text = "COMPUTER SHUTDOWN LAUNCHED";
                Timer_apaga.Stop();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            Enable_Controls();
            chkshut.Enabled = true;
            btn_pause.Enabled = true;
            textBox1.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            checkBox3.Enabled = true;

            Timer_apaga.Stop();
            TB1.Visible = false;
            TB1.Text = "AUTOMATIC SHUTDOWN ENABLED";
            TB2.Visible = false;
            TB2.Text = "AUTOMATIC SHUTDOWN ENABLED";
            button10.Visible = false;
            
            chkshut.CheckState = CheckState.Unchecked;
            notifyIcon1.Visible = false;
            txt_remain.Text = "Time remaining";

            if (tiempo_apaga == 0)
            {
                Process no_apagar = new System.Diagnostics.Process();
                no_apagar.StartInfo.FileName = "shutdown.exe";
                no_apagar.StartInfo.Arguments = "-a";
                no_apagar.StartInfo.UseShellExecute = false;
                no_apagar.StartInfo.CreateNoWindow = true;
                no_apagar.Start();
            }
            tiempo_apaga = 120;
        }

        void button11_Click(object sender, EventArgs e)
        {

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Add at least one file to try.", "Empty list", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }
            BG_Try_button.RunWorkerAsync();
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {

            if (comboBox1.Text == "")
            {

                return;
            }

            if (comboBox1.SelectedIndex == 0)
            {

                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                int linea = 0;
                foreach (string line in File.ReadLines(path))
                {

                    linea = linea + 1;

                    if (linea == 1)
                    {
                        textBox1.Text = line;
                    }

                    if (linea == 2)
                    {
                        textBox2.Text = line;
                    }

                    if (line == "yes")

                    {
                        checkBox3.CheckState = CheckState.Checked;
                    }

                    if (line == "no")
                    {
                        checkBox3.CheckState = CheckState.Unchecked;
                    }



                }


            }

            else
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";

                foreach (string line in File.ReadLines(path))
                {

                    if (line.Contains(comboBox1.Text))
                    {
                        int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");

                        textBox2.Text = line.Substring(line.LastIndexOf("%") + 2);
                        textBox1.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            String path = "cmd.exe";
            String param = "/k " + "ffmpeg.exe -version";
            Process ff_ext = new Process();
            ff_ext.StartInfo.FileName = path;
            ff_ext.StartInfo.Arguments = param;
            ff_ext.Start();
            ff_ext.WaitForExit();

        }

        private void cancelarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("hola");
        }


        private void chkshut_CheckedChanged(object sender, EventArgs e)
        {
            if (button10.Visible == true)
            {
                return;
            }

            if (chkshut.Checked == false)
            {
                TB1.Visible = false;
                TB2.Visible = false;
                chkshut.ImageIndex = 0;
                groupBox5.Focus();
            }
            else
            {
                TB1.Visible = true;
                if (tabControl1.SelectedIndex == 1)
                {
                    TB2.Visible = true;
                }
                else
                {
                    TB2.Visible = false;
                }
                chkshut.ImageIndex = 1;            }

        }


        private void button15_Click_1(object sender, EventArgs e)
        {
            was_started.Text = button15.Text;
            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (listView1.Items.Count < 2)
            {
                MessageBox.Show("Add at least two files to concatenate", "Add files to queue list", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Format field can not be empty, please add a file format extension", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chk_shift.Checked == true)
            {
                MessageBox.Show("Shifting audio is not available for concatenation", "Shifting audio not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkshut.Checked)
            {
                TB1.Visible = true;
                TB1.Text = "Automatic shutdown disabled for concatenating.";
            }

            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            Disable_Controls();
            txt_remain.Text = "Time remaining:";
            time_n_tasks = 0;
            timer_tasks.Start();
            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            textBox5.Text = "0%";
            textBox5.Visible = true;
            notifyIcon1.Visible = true;

            working = true;

            String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");

            Pg1.Value = 0;

            String primero_lista = listView1.Items[0].Text;

            String destino = "";
            if (textBox3.Text == "..\\FFBatch")
            {
                destino = primero_lista.Substring(0, primero_lista.LastIndexOf('\\')) + "\\" + "FFBatch";
            }
            else
            {
                destino = textBox3.Text;
            }

            if (!Directory.Exists(destino))
            {
                try
                {
                    Directory.CreateDirectory(destino);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    Enable_Controls();
                    working = false;
                    return;
                }

            }

            var lista_concat = new String[listView1.Items.Count];
            int i = 0;
            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView1.Items)
            {
                list_proc.Items.Add(item.Text);
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";

            }

            listView1.SelectedIndices.Clear();
            textBox7.Text = "Task processing: 0%";
            Double total_duration = 0;
            int i_dur = 0;

            //Get total duration of files
            foreach (ListViewItem item in listView1.Items)
            {


                if (listView1.Items[i_dur].SubItems[2].Text != "N/A" && listView1.Items[i_dur].SubItems[2].Text != "0:00:00" && listView1.Items[i_dur].SubItems[2].Text != "00:00:00" && listView1.Items[i_dur].SubItems[2].Text != "Pending")
                {

                    total_duration = total_duration + TimeSpan.Parse(listView1.Items[i_dur].SubItems[2].Text).TotalSeconds;
                }

                else
                {
                    total_duration = total_duration + 0;
                }

                i_dur = i_dur + 1;
            }
            //End get total duration of files

            Pg1.Maximum = 100;
            foreach (ListViewItem item in listView1.Items)
            {
                item.SubItems[4].Text = "Processing";
            }

            //End total duration

            List<string> list_lines = new List<string>();
            process_glob.StartInfo.Arguments = String.Empty;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                String remain_time = "";

                foreach (ListViewItem file in list_proc.Items)
                {
                    //Aborted requested
                    if (cancel_queue == true)
                    {
                        working = false;

                        this.InvokeEx(f => f.Pg1.Value = 0);
                        this.InvokeEx(f => f.pg_current.Value = 0);
                        Enable_Controls();
                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (file.Text.Contains(" "))
                    {
                        lista_concat[i] = "file " + "'" + file.Text + "'";
                        i = i + 1;
                    }
                    else
                    {
                        lista_concat[i] = "file " + "'" + file.Text.Replace("\\", "\\\\") + "'";
                        i = i + 1;
                    }
                }

                String path = System.IO.Path.Combine(Path.GetTempPath(), "concat.txt");
                File.WriteAllLines(path, lista_concat);


                //Change Volume
                String change_vol = "";
                if (chk_vol.Checked == true)
                {
                    change_vol = "-filter:a " + '\u0022' + "volume=" + vol_ch.Value.ToString() + "dB " + '\u0022' + " ";
                }
                //End change volume

                String AppParam = " -f concat -safe 0 -i " + '\u0022' + path + '\u0022' + " " + textBox1.Text + " " + " -y " + change_vol + '\u0022' + destino + "\\" + "Concatenated" + "." + textBox2.Text + '\u0022';
                process_glob.StartInfo.FileName = ffm;
                process_glob.StartInfo.Arguments = AppParam;
                process_glob.StartInfo.RedirectStandardOutput = true;
                process_glob.StartInfo.RedirectStandardError = true;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.EnableRaisingEvents = true;

                valid_prog = false;

                this.InvokeEx(f => f.pg_current.Value = 0);

                process_glob.Start();
                if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                String err_txt = "";

                while (!process_glob.StandardError.EndOfStream)
                {
                    err_txt = process_glob.StandardError.ReadLine();
                    list_lines.Add(err_txt);


                    if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                    {
                        int start_time_index = err_txt.IndexOf("time=") + 5;
                        Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                        Double percent = (sec_prog * 100 / total_duration);
                        int percent2 = Convert.ToInt32(percent);

                        if (percent2 <= 100)
                        {
                            this.InvokeEx(f => f.textBox5.Text = (percent2).ToString() + "%");
                            this.InvokeEx(f => f.textBox5.Refresh());
                            this.InvokeEx(f => f.Pg1.Value = percent2);
                            this.InvokeEx(f => f.Pg1.Refresh());
                        }
                        //Estimated remaining time

                        remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                        remain_time = remain_time.Replace("x", String.Empty);
                        Double timing1 = 0;

                        if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                        }
                        else
                        {
                            timing1 = Math.Round(Double.Parse(remain_time), 2);
                        }
                        Decimal timing = (decimal)timing1;
                        Decimal total_dur_dec = Convert.ToDecimal(total_duration);
                        Decimal total_prog_dec = Convert.ToDecimal(sec_prog);
                        Decimal remain_secs = 0;
                        if (timing > 0)
                        {
                            remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                        }

                        if (remain_secs > 60)
                        {
                            remain_secs = remain_secs + 60;
                        }
                        String remain_from_secs = "";

                        TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                        remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                           t.Hours,
                          t.Minutes);

                        if (remain_secs >= 3600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                        }

                        if (remain_secs < 3600 && remain_secs >= 600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                        }
                        if (remain_secs < 600 && remain_secs >= 120)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                        }

                        if (remain_secs <= 59)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                        }

                        //End remaining time   
                    }


                    //Read output, get progress
                    this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                    this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                }
                process_glob.WaitForExit();
                process_glob.StartInfo.Arguments = String.Empty;
                list_lines.Add("");


                if (process_glob.ExitCode == 0)
                {
                    foreach (ListViewItem item in list_proc.Items)
                    {
                        this.InvokeEx(f => f.listView1.Items[item.Index].SubItems[4].Text = "Success");
                    }
                    working = false;
                    Enable_Controls();

                    //Save log
                    string[] array_err = list_lines.ToArray();
                    String path_l = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                    System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path_l);
                    SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                    SaveFile.WriteLine("-------------------------------");
                    foreach (String item in array_err)
                    {
                        SaveFile.WriteLine(item);
                    }
                    SaveFile.Close();

                    File.AppendAllText(path_l, "-----------------------");
                    File.AppendAllText(path_l, Environment.NewLine + "END OF LOG FILE");
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(path_l);

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
                    File.AppendAllText(path_l, Environment.NewLine + "LOG SIZE: " + size);

                    //End save log

                    if (Form.ActiveForm == null)
                    {
                        notifyIcon1.BalloonTipText = "Queue concatenation completed";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                        notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                        notifyIcon1.ShowBalloonTip(0);
                    }

                    if (checkBox3.Checked)
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
                else
                {
                    working = false;
                    Enable_Controls();
                    if (Directory.GetFiles(destino).Length == 0)
                    {
                        if (Directory.Exists(destino))
                        {
                            System.IO.Directory.Delete(destino);
                        }
                    }
                    if (cancel_queue == true)
                    {
                        foreach (ListViewItem item in list_proc.Items)
                        {
                            this.InvokeEx(f => f.listView1.Items[item.Index].SubItems[4].Text = "Aborted");
                        }
                        MessageBox.Show("Concatenation aborted by user", "Concatenation Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        foreach (ListViewItem item in list_proc.Items)
                        {
                            this.InvokeEx(f => f.listView1.Items[item.Index].SubItems[4].Text = "Error");
                        }
                        MessageBox.Show("Concatenation failed: " + listBox4.Items[listBox4.Items.Count - 1].ToString(), "Concatenation failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }

                if (File.Exists(System.IO.Path.Combine(Application.StartupPath, "concat.txt")))
                {
                    File.Delete(System.IO.Path.Combine(Application.StartupPath, "concat.txt"));
                }

                Enable_Controls();

            }).Start();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
                p.Kill();
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            change_tab_1 = false;
            change_tab_2 = false;

            string[] file_drop = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> files2 = new List<string>();

            int num_drop = 0;

            foreach (String dropped in file_drop)
            {

                if (File.Exists(dropped))
                {
                    files2.Add(dropped);
                    num_drop = files2.Count();
                }

                else
                {
                    if (Directory.Exists(dropped))
                    {
                        if (add_subfs == false)
                        {

                            foreach (String file in Directory.GetFiles(dropped))
                            {
                                if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden))
                                {
                                    files2.Add(file);
                                    num_drop = num_drop + 1;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (string f in Directory.GetFiles(dropped, "*.*", SearchOption.AllDirectories))
                                {
                                    if (!File.GetAttributes(f).HasFlag(FileAttributes.Hidden))
                                    {
                                        files2.Add(f);
                                        num_drop = num_drop + 1;
                                    }
                                }
                            }
                            catch (System.Exception excpt)
                            {
                                var a = MessageBox.Show("Error: " + excpt.Message, "Access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                    }
                }
            }


            if (num_drop >= 5000)
            {
                var a = MessageBox.Show("Adding " + num_drop + " files could take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel)
                {
                    return;
                }
            }

            files_to_add = files2;
            canceled_file_adding = false;
            btn_cancel_add.Enabled = true;
            btn_cancel_add.Visible = true;
            btn_cancel_add.Refresh();
            BG_Files.RunWorkerAsync();

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (working == true)
            {
                if (multi_running == false)
                {
                    listView1.SelectedIndices.Clear();
                }

            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;

            //   if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;

        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ctdel.PerformClick();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            listView1.Sorting = SortOrder.Ascending;

        }

        private void button17_Click_1(object sender, EventArgs e)
        {
            foreach (ListViewItem n_a in listView1.Items)
            {
                if (n_a.BackColor == Color.LightGoldenrodYellow)
                {
                    n_a.Remove();
                }
            }
            label9.Text = "Items: " + listView1.Items.Count;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            cti4.PerformClick();

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0 || e.Column == 1 || e.Column == 2)
            {

                if (e.Column == lvwColumnSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (lvwColumnSorter.Order == SortOrder.Ascending)
                    {
                        lvwColumnSorter.Order = SortOrder.Descending;
                    }
                    else
                    {
                        lvwColumnSorter.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    lvwColumnSorter.SortColumn = e.Column;
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }

                // Perform the sort with these new sort options.
                this.listView1.Sort();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            listView1.GridLines = !listView1.GridLines;
            listView2.GridLines = !listView2.GridLines;
            listView3.GridLines = !listView3.GridLines;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView2.View = View.Details;
            listView3.View = View.Details;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (working == false) return;

            button20.Enabled = false;
            button20.Text = "Aborting queue";


            if (recording_scr == true)
            {
                Enable_Controls();
                working = false;
                recording_scr = false;

                StreamWriter write_q = process_glob.StandardInput;
                write_q.Write("q");
                return;
            }

            if (multi_running == true)
            {
                working = false;
                multi_running = false;
                aborted = true;
                cancelados_paralelos = true;

                foreach (ListViewItem item in listView1.Items)
                {
                    if (item.SubItems[4].Text != "Success" && item.SubItems[4].Text != "Ready" && item.SubItems[4].Text != "Queued")
                    {
                        item.SubItems[4].Text = "Aborting";
                    }
                }

                foreach (Process proc in procs.Values)
                {
                    cancelados_paralelos = true;
                    if (proc.StartInfo.Arguments != String.Empty)

                    {
                        StreamWriter write_q = proc.StandardInput;
                        write_q.Write("q");
                    }
                }
                return;
            }

            cancel_queue = true;
            cancelados_paralelos = true;

            if (process_glob.StartInfo.Arguments != String.Empty)
            {
                process_glob.Kill();
            }

            else
            {
                System.Threading.Thread.Sleep(250);
                Process[] localByName = Process.GetProcessesByName("ffmpeg");
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(500);

                Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process p2 in localByName2)
                {
                    p2.Kill();
                }
            }
            cancel_queue = true;
            cancelados_paralelos = true;

            if (process_glob.StartInfo.Arguments != String.Empty)
            {
                StreamWriter write_q = process_glob.StandardInput;
                write_q.Write("q");
                return;
                //process_glob.Kill();
            }

            else
            {
                System.Threading.Thread.Sleep(250);
                Process[] localByName = Process.GetProcessesByName("ffmpeg");
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(500);

                Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process p2 in localByName2)
                {
                    p2.Kill();
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                listView1.BeginUpdate();
                foreach (ListViewItem n_a in listView1.Items)
                {
                    if (n_a.BackColor == Color.LightGoldenrodYellow)
                    {
                        n_a.Remove();
                    }
                }

                label9.Text = "Items: " + listView1.Items.Count;
                calc_total_dur();
                calc_list_size();
                listView1.EndUpdate();
            }

            if (tabControl1.SelectedIndex == 3)
            {

                if (dg1.RowCount == 0) return;

                dg1.EndEdit();
                dg1.ClearSelection();

                for (int i = 1; i < dg1.RowCount; i++)

                {

                    if (dg1.Rows[i].Cells[4].Value.ToString() == "Error")

                    {

                        dg1.Rows.RemoveAt(i);
                        i--;
                    }

                }

                if (dg1.Rows[0].Cells[0] != null && dg1.Rows[0].Cells[4].Value.ToString() == "Error")
                {
                    dg1.Rows.RemoveAt(0);
                    return;
                }
                dg1.Refresh();
            }
        }

        private void button17_Click_2(object sender, EventArgs e)
        {            
            was_started.Text = button17.Text;
            if (listView1.Items.Count == 1)
            {
                //   button2.PerformClick();
                //   return;
            }
            cancelados_paralelos = false;

            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ss_time_input.Text != "0:00:00")
            {
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "0:00:00" && item.SubItems[2].Text != "00:00:00" && item.SubItems[2].Text != "Pending")
                    {
                        if (TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds <= TimeSpan.Parse(ss_time_input.Text).TotalSeconds)
                        {
                            MessageBox.Show("Pre-input seeking exceeds duration of file: " + '\u0022' + Path.GetFileName(item.Text) + '\u0022', "Pre-input seeking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

            }

            empty_text = false;
            if (textBox2.Text == "")
            {
                empty_text = true;
                var a = MessageBox.Show("Format field is empty. Source file extension will be used. Continue?", "Format field blank", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel) return;

            }

            avoid_overw();

            if (avoid_overwriting == true && textBox3.Text != "..\\FFBatch" && checkBox1.CheckState != CheckState.Checked)
            {
                avoid_overwriting = false;
                MessageBox.Show("Multiple folders in input files and a single output folder may lead to file overwriting. Please enable " + '\u0022' + "Recreate source path" + '\u0022' + " to avoid opossible overwritings, or double click on the output path textbox to set it to the default relative path", "Different input folders but single output folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (textBox1.Text.Contains("libx264") || textBox1.Text.Contains("libx265") || textBox1.Text.Contains("jpeg2000") || textBox1.Text.Contains("libtheora") || textBox1.Text.Contains("libxvid") || textBox1.Text.Contains("mpeg2") || textBox1.Text.Contains("webp") || textBox1.Text.Contains("mpeg4") || textBox1.Text.Contains("libvpx"))
            {
                var a = MessageBox.Show("Video encoding tasks are already multi-thread, thus sequential single file processing is recommended. Continue anyway?", "Confirm video multi-file processing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.No)
                    return;
            }

            //Validated list, start processing
            txt_remain.Text = "";
            txt_remain.Refresh();
            total_time = true;
            n_th_suffix = String.Empty;
            n_th_source_ext = String.Empty;

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }
            //Try preset

            if (tried_ok == false)
            {
                BG_Try_preset.RunWorkerAsync();
                return;
            }
            tried_ok = false;

            //Remove test file/folder
            String file_prueba = "";
            String sel_test = listView1.SelectedItems[0].Text;
            file_prueba = sel_test;
            String destino = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

            if (File.Exists(borrar))
            {
                File.Delete(borrar);
            }

            if (Directory.Exists(destino) == true)
            {
                if (Directory.GetFiles(destino).Length == 0)
                {
                    System.IO.Directory.Delete(destino);
                }
            }

            //END Remove test file/folder

            //END try preset

            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            //if (textBox1.Text.Contains("libx264") || textBox1.Text.Contains("libx265") || textBox1.Text.Contains("jpeg2000") || textBox1.Text.Contains("libtheora") || textBox1.Text.Contains("libxvid") || textBox1.Text.Contains("mpeg2") || textBox1.Text.Contains("webp") || textBox1.Text.Contains("mpeg4") || textBox1.Text.Contains("libvpx"))
            //{
            ////if (a == DialogResult.No)
            //  return;
            //}

            cancel_queue = false;
            cancelados_paralelos = false;
            textBox4.Visible = false;

            working = true;
            listBox4.Items.Clear();
            groupBox5.Focus();
            Disable_Controls();

            //Total duration

            total_multi_duration = 0;
            foreach (ListViewItem item in listView1.Items)
            {

                DateTime time2;
                if (DateTime.TryParse(item.SubItems[2].Text, out time2))

                {
                    total_multi_duration = total_multi_duration + TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds;
                }
            }
            //End total duration

            //String remain_time = "0";
            start_total_time = 0;

            //interval = 0;
            procs.Clear();

            for (int ii = 0; ii < listView1.Items.Count; ii++)
            {
                procs.Add("proc_urls_" + ii.ToString(), new Process());
            }

            rows_running = 0;

            //Beginning multi-thread
            notifyIcon1.Visible = true;
            list_global_proc.Clear();
            foreach (ListViewItem item in listView1.Items)
            {
                list_global_proc.Items.Add((ListViewItem)item.Clone());
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";
            }

            Pg1.Value = 0;
            Pg1.Maximum = list_global_proc.Items.Count * 100;
            textBox5.Text = "";

            //listView1.SelectedIndices.Clear();
            textBox5.Visible = true;
            time_n_tasks = 0;
            timer_tasks.Start();
            timer1.Start();
            textBox7.Text = "Running tasks";
            textBox7.Visible = true;
            
            List<string> list_items = new List<string>();
            foreach (ListViewItem item in list_global_proc.Items)
            {
                list_items.Add(item.Index.ToString());
            }
                        
            multi_running = true;
            //remains_multis.Clear();
            //for (int i = 0; i < listView1.Items.Count; i++)
            //{
            //  remains_multis.Add((decimal)i);
            //                remains_multis[i] = 0;
            //          }

            ParallelOptions par_op = new ParallelOptions();

            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();
            
            par_op.CancellationToken = cts.Token;
            
            aborted = false;

            Parallel.For(0, list_global_proc.Items.Count, par_op, (file_int) => {
                
                var t = Task.Run(() =>
                {                                    
                    if (cancelados_paralelos == true)
                    {
                        cts.Cancel();
                        rows_running = 0;

                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();
                        if (aborted == true)
                        {
                            aborted = false;
                            MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        cancelados_paralelos = false;
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        cts.Dispose();
                        return;
                    }

                    List<string> list_lines = new List<string>();
                    String item_capture_time = String.Empty;
                    Double total_prog = 0;
                    Double row_duration = 0;
                    Boolean valid_prog2 = false;
                    TimeSpan time2;

                    if (TimeSpan.TryParse(list_global_proc.Items[file_int].SubItems[2].Text, out time2))
                    {
                        row_duration = TimeSpan.Parse(list_global_proc.Items[file_int].SubItems[2].Text).TotalSeconds;
                        valid_prog2 = true;
                    }

                    else
                    {
                        row_duration = 0;
                        valid_prog2 = false;
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = list_global_proc.Items[file_int].Text;
                    //String destino = file.Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";

                    //Begin Shifting
                    String shifting = "";
                    if (chk_shift.Checked == true)
                    {
                        if (Num_Shift.Value >= 0)
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " -map 0:v -map 1:a ";
                        }
                        else
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " -map 1:v -map 0:a ";
                        }
                    }

                    //End Shifting

                    //Change Volume
                    String change_vol = "";
                    if (chk_vol.Checked == true)
                    {
                        change_vol = "-filter:a " + '\u0022' + "volume=" + vol_ch.Value.ToString() + "dB " + '\u0022' + " ";
                    }
                    //End change volume


                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino = list_global_proc.Items[file_int].Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            String pre_dest = Path.GetDirectoryName(list_global_proc.Items[file_int].Text);
                            destino = Path.Combine(textBox3.Text, pre_dest.Substring(3, pre_dest.Length - 3));
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }
                    }

                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    add_suffix = "";
                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                        n_th_suffix = add_suffix;
                    }

                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(list_global_proc.Items[file_int].Text);

                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }
                    n_th_source_ext = ext_output1;

                    textbox_params = textBox1.Text;
                    String file2 = list_global_proc.Items[file_int].Text;
                    if (textbox_params.Contains("%1"))
                    {
                        file2 = file2.Replace("\\", "\\\\\\\\");
                        file2 = file2.Replace(":", "\\\\" + ":");
                        textbox_params = textbox_params.Replace("%1", file2);
                    }
                    String AppParam = pre_input_var + " " + pre_ss + " -i " + "" + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " " + shifting + " " + " -y " + textbox_params + " " + change_vol + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(list_global_proc.Items[file_int].Text) + add_suffix + ext_output1 + '\u0022';

                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());
                    
                    var tmp = procs["proc_urls_" + file_int.ToString()];
                    tmp.StartInfo.FileName = ffm;
                    tmp.StartInfo.Arguments = AppParam;
                    tmp.StartInfo.RedirectStandardInput = true;
                    tmp.StartInfo.RedirectStandardOutput = true;
                    tmp.StartInfo.RedirectStandardError = true;
                    tmp.StartInfo.UseShellExecute = false;
                    tmp.StartInfo.CreateNoWindow = true;
                    tmp.EnableRaisingEvents = true;

                    if (cts.IsCancellationRequested == false)
                    {
                        tmp.Start();
                        this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Processing");
                        if (mem_prio.SelectedIndex != 2)
                        {
                            System.Threading.Thread.Sleep(50);
                            Change_mem_prio();
                        }
                    }
                    else
                    {
                        timer1.Stop();
                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();
                        cancelados_paralelos = false;
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        return;
                    }

                    rows_running = rows_running + 1;
                    this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Processing");
                    //this.InvokeEx(f => f.listView1.Items[file_int].EnsureVisible());

                    String err_txt = "";
                    Double interval = 0;
                    Double durat_n2 = 0;
                    

                    //REVIEW
                    while (!tmp.StandardError.EndOfStream)
                    {
                        err_txt = tmp.StandardError.ReadLine();
                        //list_lines.Add(err_txt);

                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog2 == true)
                            {
                                this.InvokeEx(f => f.txt_remain.Refresh());
                                this.InvokeEx(f => durat_n2 = row_duration);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n2);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                //Double percent_tot = (total_prog * 100 / total_duration);
                                //int percent_tot_2 = Convert.ToInt32(percent_tot);

                                //{
                                //this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                //this.InvokeEx(f => f.Pg1.Refresh());
                                //this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                //this.InvokeEx(f => f.textBox5.Refresh());
                                //}

                                if (percent2 <= 100)
                                {
                                    //this.InvokeEx(f => f.pg_current.Value = percent2);
                                    //this.InvokeEx(f => f.pg_current.Refresh());

                                    this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = percent2.ToString() + "%");
                                    list_global_proc.Items[file_int].SubItems[4].Text = percent2.ToString() + "%";
                                    
                                }
                            }
                            
                        }

                    } // while

                    tmp.WaitForExit();
                    
                    tmp.StartInfo.Arguments = String.Empty;

                    //this.InvokeEx(f => f.pg_current.Value = 100);
                    //this.InvokeEx(f => f.textBox4.Text = "100%");
                    //list_lines.Add("");
                    //list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    //list_lines.Add("");

                    if (tmp.ExitCode == 0)
                    {
                        if (cancelados_paralelos == false && cts.IsCancellationRequested == false)
                        {
                            if (aborted_url == false)
                            {
                                rows_running = rows_running - 1;
                                this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Success");

                                this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.White);
                            }
                            else
                            {
                                rows_running = rows_running - 1;
                                this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Aborted");

                                this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);
                                aborted_url = false;
                            }
                        }

                        else
                        {
                            rows_running = rows_running - 1;
                            this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Aborted");
                            list_global_proc.Items[file_int].SubItems[4].Text = "Aborted";
                            this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);
                            if (cancelados_paralelos == false)
                            {
                                aborted_url = false;
                            }
                        }

                    }

                    else
                    {
                        rows_running = rows_running - 1;

                        this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Failed");
                        list_global_proc.Items[file_int].SubItems[4].Text = "Failed";
                        this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);

                    }
                    file_int++;

                    if (rows_running == 0)
                    {
                        timer1.Stop();
                        timer_tasks.Stop();
                        this.InvokeEx(f => f.Pg1.Value = Pg1.Maximum);
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();

                        if (cancelados_paralelos == false && cts.IsCancellationRequested == false)
                        {
                            //Automatic shutdown check
                            if (chkshut.Checked)
                            {

                                Disable_Controls();
                                this.InvokeEx(f => f.chkshut.Enabled = false);
                                this.InvokeEx(f => f.btn_pause.Enabled = false);
                                this.InvokeEx(f => f.Timer_apaga.Start());

                                this.InvokeEx(f => this.TopMost = true);
                                this.InvokeEx(f => f.TB1.Enabled = true);
                                this.InvokeEx(f => f.TB1.Visible = true);

                                this.InvokeEx(f => f.button10.Visible = true);
                                this.InvokeEx(f => f.button10.Enabled = true);
                                this.InvokeEx(f => f.button20.Enabled = false);
                                this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                                notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                                notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                notifyIcon1.ShowBalloonTip(0);
                                return;
                            }
                            //End shutdown check

                            notifyIcon1.BalloonTipText = "All FFmpeg processes completed";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);

                            if (checkBox3.Checked == true)
                            {
                                String primero_lista = list_global_proc.Items[0].Text;

                                String destino2 = "";
                                if (textBox3.Text == "..\\FFBatch")
                                {
                                    destino2 = destino2 = primero_lista.Substring(0, primero_lista.LastIndexOf('\\')) + "\\" + "FFBatch";
                                }
                                else
                                {
                                    destino2 = textBox3.Text;
                                }

                                if (Directory.GetFiles(destino2).Length == 0)
                                {

                                    if (Directory.Exists(destino2))
                                    {
                                        System.IO.Directory.Delete(destino2);
                                    }
                                }
                            }

                            return;
                        }
                        else
                        {
                            if (aborted == true)
                            {
                                aborted = false;
                                MessageBox.Show("Queue processing aborted by user", "FFmpeg processing aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            cancelados_paralelos = false;
                            this.InvokeEx(f => f.textBox7.Text = "FFmpeg output");
                            return;
                        }

                    }
                });
            });
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            String path_log = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";
            if (File.Exists(path_log))
            {
                System.Diagnostics.Process.Start(path_log);
            }
            else
            {
                MessageBox.Show("No log file was found", "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Disable_Controls()
        {
            foreach (Control p in this.Controls)
            {
                if (p.Name != panel1.Name && p.Name != tabControl1.Name && p.Name != groupBox15.Name && p.Name != groupBox10.Name && p.Name != groupBox5.Name && p.Name != groupBox_m3u.Name && p.Name != groupBox9.Name && p.Name != groupBox1.Name)
                {
                    this.InvokeEx(f => p.Enabled = false);
                }
                this.InvokeEx(f => f.btn_pause.Enabled = true);
            }

            foreach (Control ct in groupBox_m3u.Controls)
            {
                this.InvokeEx(f => ct.Enabled = false);
            }

            this.InvokeEx(f => f.btn_abort_urls.Enabled = true);
            this.InvokeEx(f => f.btn_stop_m3u8.Enabled = true);


            foreach (Control ct in panel1.Controls)
            {
                if (ct.Name != button20.Name)
                {
                    this.InvokeEx(f => ct.Enabled = false);
                }
            }

            foreach (Control ct_g in groupBox15.Controls)
            {
                if (ct_g.Name != btn_abort_sub_mux.Name)
                {
                    this.InvokeEx(f => ct_g.Enabled = false);
                }
            }

            foreach (Control ct in groupBox9.Controls)
            {
                this.InvokeEx(f => ct.Enabled = false);
            }
            foreach (Control ct in groupBox1.Controls)
            {
                this.InvokeEx(f => ct.Enabled = false);
            }

            this.InvokeEx(f => f.TB1.Enabled = true);
            this.InvokeEx(f => f.chkshut.Enabled = true);
            this.InvokeEx(f => f.btn_pause.Enabled = true);
            this.InvokeEx(f => f.listView2.Enabled = false);
            this.InvokeEx(f => f.list_tracks.Enabled = false);
            this.InvokeEx(f => f.btn_mux.Enabled = false);
            this.InvokeEx(f => f.btn_mux_cancel.Enabled = true);
            this.InvokeEx(f => f.listView1.Enabled = true);
            this.InvokeEx(f => f.label10.Enabled = true);
            this.InvokeEx(f => f.txt_remain.Enabled = true);
            this.InvokeEx(f => f.label11.Enabled = true);
            this.InvokeEx(f => f.textBox4.Enabled = true);
            this.InvokeEx(f => f.textBox5.Enabled = true);
            this.InvokeEx(f => f.textBox7.Enabled = true);
            this.InvokeEx(f => f.Pg1.Enabled = true);
            this.InvokeEx(f => f.pg_current.Enabled = true);
            this.InvokeEx(f => f.listBox4.Enabled = true);
            this.InvokeEx(f => f.textBox7.Visible = false);
            total_time = false;
            this.InvokeEx(f => f.LB_Wait.Enabled = true);
            this.InvokeEx(f => f.pg_adding.Enabled = true);
            this.InvokeEx(f => f.txt_adding_p.Enabled = true);

            this.InvokeEx(f => f.combo_prio.Enabled = true);
            this.InvokeEx(f => f.lbl_combo_prio.Enabled = true);
            this.InvokeEx(f => f.lbl_combo_prio.Visible = true);
            this.InvokeEx(f => f.combo_prio.Visible = true);
            this.InvokeEx(f => f.btn_save_prio.Visible = true);
            this.InvokeEx(f => f.btn_mux_cancel.Enabled = true);

        }

        private void Enable_Controls()
        {
            this.InvokeEx(f => f.button20.Text = "Abort queue");
            foreach (Control p in this.Controls)
            {
                this.InvokeEx(f => p.Enabled = true);
            }

            foreach (Control ct in panel1.Controls)
            {
                this.InvokeEx(f => ct.Enabled = true);
            }

            foreach (Control ct_g in groupBox15.Controls)
            {
                this.InvokeEx(f => ct_g.Enabled = true);
            }

            foreach (Control ct_g in groupBox1.Controls)
            {
                this.InvokeEx(f => ct_g.Enabled = true);
            }

            foreach (Control ct in groupBox_m3u.Controls)
            {
                this.InvokeEx(f => ct.Enabled = true);
            }

            foreach (Control ct in groupBox9.Controls)
            {
                this.InvokeEx(f => ct.Enabled = true);
            }

            this.InvokeEx(f => f.chk_vol.Checked = false);
            this.InvokeEx(f => f.vol_ch.Enabled = false);
            this.InvokeEx(f => f.chk_shift.Checked = false);
            this.InvokeEx(f => f.Num_Shift.Enabled = false);
            this.InvokeEx(f => f.listView2.Enabled = true);
            this.InvokeEx(f => f.list_tracks.Enabled = true);
            this.InvokeEx(f => f.btn_mux.Enabled = true);
            this.InvokeEx(f => f.txt_remain.Text = "");
            this.InvokeEx(f => f.combo_prio.Visible = false);
            this.InvokeEx(f => f.btn_save_prio.Visible = false);


            if (Enable_txt_hard_Subs == true)
            {
                this.InvokeEx(f => f.txt_hard_subs.Enabled = true);
            }
            else
            {
                this.InvokeEx(f => f.txt_hard_subs.Enabled = false);
            }

            timer_tasks.Stop();
            TimeSpan t = TimeSpan.FromSeconds(time_n_tasks);
            String tx_elapsed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
            this.InvokeEx(f => f.txt_remain.Text = "Total time:  " + tx_elapsed);

            this.InvokeEx(f => f.pg_adding.Visible = false);

            if (chk_m3u_params.CheckState == CheckState.Checked)
            {
                this.InvokeEx(f => f.txt_m3u_params.Enabled = true);
            }
            else
            {
                this.InvokeEx(f => f.txt_m3u_params.Enabled = false);
            }

            if (chk_output_server.CheckState == CheckState.Checked)
            {
                this.InvokeEx(f => f.txt_output_server.Enabled = true);
            }
            else
            {
                this.InvokeEx(f => f.txt_output_server.Enabled = false);
            }

        }

        private void Create_Tooltips()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 750;
            toolTip1.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.listView1, "Drag and drop files or folders here");

            ToolTip toolTip2 = new ToolTip();
            toolTip2.AutoPopDelay = 5000;
            toolTip2.InitialDelay = 750;
            toolTip2.ReshowDelay = 500;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.button8, "Remove current preset");

            ToolTip toolTip3 = new ToolTip();
            toolTip3.AutoPopDelay = 5000;
            toolTip3.InitialDelay = 750;
            toolTip3.ReshowDelay = 500;
            toolTip3.ShowAlways = true;
            toolTip3.SetToolTip(this.button11, "Try ffmpeg parameters with selected file");

            ToolTip toolTip4 = new ToolTip();
            toolTip4.AutoPopDelay = 5000;
            toolTip4.InitialDelay = 750;
            toolTip4.ReshowDelay = 500;
            toolTip4.ShowAlways = true;
            toolTip4.SetToolTip(this.button12, "Remove items with type/duration warning");

            ToolTip toolTip5 = new ToolTip();
            toolTip5.AutoPopDelay = 5000;
            toolTip5.InitialDelay = 750;
            toolTip5.ReshowDelay = 500;
            toolTip5.ShowAlways = true;
            toolTip5.SetToolTip(this.chkshut, "Automatic shutdown on completion");

            ToolTip toolTip6 = new ToolTip();
            toolTip6.AutoPopDelay = 5000;
            toolTip6.InitialDelay = 500;
            toolTip6.ReshowDelay = 500;
            toolTip6.ShowAlways = true;
            toolTip6.SetToolTip(this.textBox3, "Double-click to reset default output folder");

            ToolTip toolTip6_2 = new ToolTip();
            toolTip6_2.AutoPopDelay = 5000;
            toolTip6_2.InitialDelay = 500;
            toolTip6_2.ReshowDelay = 500;
            toolTip6_2.ShowAlways = true;
            toolTip6_2.SetToolTip(this.btn_reset_path, "Click to reset default output folder");

            ToolTip toolTip8 = new ToolTip();
            toolTip8.AutoPopDelay = 5000;
            toolTip8.InitialDelay = 750;
            toolTip8.ReshowDelay = 500;
            toolTip8.ShowAlways = true;
            toolTip8.SetToolTip(this.button2, "Start sequential processing");

            ToolTip toolTip9 = new ToolTip();
            toolTip9.AutoPopDelay = 5000;
            toolTip9.InitialDelay = 750;
            toolTip9.ReshowDelay = 500;
            toolTip9.ShowAlways = true;
            toolTip9.SetToolTip(this.button17, "Start multiple file processing using maximum threads");

            ToolTip toolTip10 = new ToolTip();
            toolTip10.AutoPopDelay = 5000;
            toolTip10.InitialDelay = 750;
            toolTip10.ReshowDelay = 500;
            toolTip10.ShowAlways = true;
            toolTip10.SetToolTip(this.button15, "Concatenate files in queue using selected preset");

            ToolTip toolTip12 = new ToolTip();
            toolTip12.AutoPopDelay = 5000;
            toolTip12.InitialDelay = 750;
            toolTip12.ReshowDelay = 500;
            toolTip12.ShowAlways = true;
            toolTip12.SetToolTip(this.checkBox3, "Open output folder on completion");

            ToolTip toolTip13 = new ToolTip();
            toolTip13.AutoPopDelay = 5000;
            toolTip13.InitialDelay = 750;
            toolTip13.ReshowDelay = 500;
            toolTip13.ShowAlways = true;
            toolTip13.SetToolTip(this.button22, "Show audio waveform");

            ToolTip toolTip14 = new ToolTip();
            toolTip14.AutoPopDelay = 1500;
            toolTip14.InitialDelay = 750;
            toolTip14.ReshowDelay = 500;
            toolTip14.ShowAlways = true;
            toolTip14.SetToolTip(this.pictureBox1, "Visit FFmpeg Batch website");

            ToolTip toolTip15 = new ToolTip();
            toolTip15.AutoPopDelay = 1500;
            toolTip15.InitialDelay = 750;
            toolTip15.ReshowDelay = 500;
            toolTip15.ShowAlways = true;
            toolTip15.SetToolTip(this.label13, "Double-click to set default output folder");

            ToolTip toolTip16 = new ToolTip();
            toolTip16.AutoPopDelay = 1500;
            toolTip16.InitialDelay = 750;
            toolTip16.ReshowDelay = 500;
            toolTip16.ShowAlways = true;
            toolTip16.SetToolTip(this.button23, "Clear track list");

            ToolTip toolTip17 = new ToolTip();
            toolTip17.AutoPopDelay = 1500;
            toolTip17.InitialDelay = 750;
            toolTip17.ReshowDelay = 500;
            toolTip17.ShowAlways = true;
            toolTip17.SetToolTip(this.txt_track_param, "FFmpeg parameters for selected track");

            ToolTip toolTip18 = new ToolTip();
            toolTip18.AutoPopDelay = 1500;
            toolTip18.InitialDelay = 750;
            toolTip18.ReshowDelay = 500;
            toolTip18.ShowAlways = true;
            toolTip18.SetToolTip(this.btn_mux, "Multiplex streams");

            ToolTip toolTip19 = new ToolTip();
            toolTip19.AutoPopDelay = 1500;
            toolTip19.InitialDelay = 750;
            toolTip19.ReshowDelay = 500;
            toolTip19.ShowAlways = true;
            toolTip19.SetToolTip(this.btn_mux_cancel, "Abort multiplexing");

            ToolTip toolTip20 = new ToolTip();
            toolTip20.AutoPopDelay = 1500;
            toolTip20.InitialDelay = 750;
            toolTip20.ReshowDelay = 500;
            toolTip20.ShowAlways = true;
            toolTip20.SetToolTip(this.btn_set_track_param, "Restore default parameters");

            ToolTip toolTip21 = new ToolTip();
            toolTip21.AutoPopDelay = 1500;
            toolTip21.InitialDelay = 750;
            toolTip21.ReshowDelay = 500;
            toolTip21.ShowAlways = true;
            toolTip21.SetToolTip(this.btn_update, "Check for updates");

            ToolTip toolTip22 = new ToolTip();
            toolTip22.AutoPopDelay = 1500;
            toolTip22.InitialDelay = 750;
            toolTip22.ReshowDelay = 500;
            toolTip22.ShowAlways = true;
            toolTip22.SetToolTip(this.txt_track_param, "Do not include -c:v / -c:a / -c:s");

            ToolTip toolTip23 = new ToolTip();
            toolTip23.AutoPopDelay = 1500;
            toolTip23.InitialDelay = 750;
            toolTip23.ReshowDelay = 500;
            toolTip23.ShowAlways = true;
            toolTip23.SetToolTip(this.lbl_mux_par, "Do not include -c:v / -c:a / -c:s");

            ToolTip toolTip24 = new ToolTip();
            toolTip24.AutoPopDelay = 1500;
            toolTip24.InitialDelay = 750;
            toolTip24.ReshowDelay = 500;
            toolTip24.ShowAlways = true;
            toolTip24.SetToolTip(this.button9, "Save current preset");

            ToolTip toolTip25 = new ToolTip();
            toolTip25.AutoPopDelay = 1500;
            toolTip25.InitialDelay = 750;
            toolTip25.ReshowDelay = 500;
            toolTip25.ShowAlways = true;
            toolTip25.SetToolTip(this.btn_add_tracks, "Add selected file to tracks list");

            ToolTip toolTip26 = new ToolTip();
            toolTip26.AutoPopDelay = 3500;
            toolTip26.InitialDelay = 750;
            toolTip26.ReshowDelay = 500;
            toolTip26.ShowAlways = true;
            toolTip26.SetToolTip(this.btn_capture, "1-Click record screen using gdigrab");

            ToolTip toolTip27 = new ToolTip();
            toolTip27.AutoPopDelay = 3500;
            toolTip27.InitialDelay = 750;
            toolTip27.ReshowDelay = 500;
            toolTip27.ShowAlways = true;
            toolTip27.SetToolTip(this.chk_suffix, "Add this text to the end of output file name");

            ToolTip toolTip28 = new ToolTip();
            toolTip28.AutoPopDelay = 3500;
            toolTip28.InitialDelay = 750;
            toolTip28.ReshowDelay = 500;
            toolTip28.ShowAlways = true;
            toolTip28.SetToolTip(this.button27, "Backup configuration file (ff_batch.ini)");

            ToolTip toolTip29 = new ToolTip();
            toolTip29.AutoPopDelay = 3500;
            toolTip29.InitialDelay = 750;
            toolTip29.ReshowDelay = 500;
            toolTip29.ShowAlways = true;
            toolTip29.SetToolTip(this.button28, "Restore configuration file");

            ToolTip toolTip30 = new ToolTip();
            toolTip30.AutoPopDelay = 3500;
            toolTip30.InitialDelay = 750;
            toolTip30.ReshowDelay = 500;
            toolTip30.ShowAlways = true;
            toolTip29.SetToolTip(this.chk_subfolders, "Add files in path subfolders");

            ToolTip toolTip31 = new ToolTip();
            toolTip31.AutoPopDelay = 3500;
            toolTip31.InitialDelay = 750;
            toolTip31.ReshowDelay = 500;
            toolTip31.ShowAlways = true;
            toolTip31.SetToolTip(this.button6, "Select path");

            ToolTip toolTip32 = new ToolTip();
            toolTip32.AutoPopDelay = 3500;
            toolTip32.InitialDelay = 750;
            toolTip32.ReshowDelay = 500;
            toolTip32.ShowAlways = true;
            toolTip32.SetToolTip(this.btn_refresh, "Refresh list");

            ToolTip toolTip33 = new ToolTip();
            toolTip33.AutoPopDelay = 3500;
            toolTip33.InitialDelay = 750;
            toolTip33.ReshowDelay = 500;
            toolTip33.ShowAlways = true;
            toolTip33.SetToolTip(this.label2, "Output file extension. Leave blank to use source file extension");

            ToolTip toolTip34 = new ToolTip();
            toolTip34.AutoPopDelay = 3500;
            toolTip34.InitialDelay = 750;
            toolTip34.ReshowDelay = 500;
            toolTip34.ShowAlways = true;
            toolTip34.SetToolTip(this.textBox2, "Output file extension. Leave blank to use source file extension");

            ToolTip toolTip35 = new ToolTip();
            toolTip35.AutoPopDelay = 3500;
            toolTip35.InitialDelay = 750;
            toolTip35.ReshowDelay = 500;
            toolTip35.ShowAlways = true;
            toolTip35.SetToolTip(this.btn_start_m3u, "Start sequential capture (one by one)");

            ToolTip toolTip36 = new ToolTip();
            toolTip36.AutoPopDelay = 3500;
            toolTip36.InitialDelay = 750;
            toolTip36.ReshowDelay = 500;
            toolTip36.ShowAlways = true;
            toolTip36.SetToolTip(this.btn_n_urls, "Start capturing all urls");

            ToolTip toolTip37 = new ToolTip();
            toolTip37.AutoPopDelay = 3500;
            toolTip37.InitialDelay = 750;
            toolTip37.ReshowDelay = 500;
            toolTip37.ShowAlways = true;
            toolTip37.SetToolTip(this.btn_validate_url, "Check URLs are ready for capture");

            ToolTip toolTip38 = new ToolTip();
            toolTip38.AutoPopDelay = 3500;
            toolTip38.InitialDelay = 750;
            toolTip38.ReshowDelay = 500;
            toolTip38.ShowAlways = true;
            toolTip38.SetToolTip(this.btn_url_info, "Get url multimedia information");

            ToolTip toolTip39 = new ToolTip();
            toolTip39.AutoPopDelay = 3500;
            toolTip39.InitialDelay = 750;
            toolTip39.ReshowDelay = 500;
            toolTip39.ShowAlways = true;
            toolTip39.SetToolTip(this.btn_multi_m, "Start limited multi-file processing using threads below");

            ToolTip toolTip40 = new ToolTip();
            toolTip40.AutoPopDelay = 3500;
            toolTip40.InitialDelay = 750;
            toolTip40.ReshowDelay = 500;
            toolTip40.ShowAlways = true;
            toolTip40.SetToolTip(this.btn_save_path, "Save this path as default");

            ToolTip toolTip41 = new ToolTip();
            toolTip41.AutoPopDelay = 3500;
            toolTip41.InitialDelay = 750;
            toolTip41.ReshowDelay = 500;
            toolTip41.ShowAlways = true;
            toolTip41.SetToolTip(this.btn_save_prio, "Save this priority as default");

            ToolTip toolTip42 = new ToolTip();
            toolTip42.AutoPopDelay = 3500;
            toolTip42.InitialDelay = 750;
            toolTip42.ReshowDelay = 500;
            toolTip42.ShowAlways = true;
            toolTip42.SetToolTip(this.btn_pause, "Pause encoding");
        }

        private void button21_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                textBox3.BackColor = textBox1.BackColor;
                textBox9.Text = folderBrowserDialog1.SelectedPath;
                textBox9.BackColor = textBox1.BackColor;
            }
        }

        private void textBox3_DoubleClick(object sender, EventArgs e)
        {
            textBox3.Text = "..\\FFBatch";
            textBox3.BackColor = Control.DefaultBackColor;
            textBox9.Text = "..\\FFBatch";
            textBox9.BackColor = Control.DefaultBackColor;
        }

        private void chk_vol_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_vol.Checked == true)
            {
                vol_ch.Enabled = true;
            }
            else
            {
                vol_ch.Enabled = false;
            }
        }

        private void chk_shift_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_shift.Checked == true)
            {
                Num_Shift.Enabled = true;
            }
            else
            {
                Num_Shift.Enabled = false;
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {

            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //Validated list, start processing

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }

            if (!File.Exists(listView1.SelectedItems[0].Text))
            {
                MessageBox.Show("File was not found: " + listView1.SelectedItems[0].Text, "Selected file not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Try preset
            String sel_test = listView1.SelectedItems[0].Text;
            this.Cursor = Cursors.WaitCursor;

            String file_prueba = "";

            file_prueba = sel_test;
            String fichero = Path.GetFileName(file_prueba);

            String destino_test = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_wave";
            if (!Directory.Exists(destino_test))
            {
                Directory.CreateDirectory(destino_test);
            }

            Form frmAWait_Wave = new Form();
            frmAWait_Wave.Name = "Processing audio, please wait";
            frmAWait_Wave.Text = "Processing audio, please wait...";
            frmAWait_Wave.Height = 100;
            frmAWait_Wave.Width = 340;
            frmAWait_Wave.MinimizeBox = false;
            frmAWait_Wave.MaximizeBox = false;
            frmAWait_Wave.Icon = this.Icon;
            frmAWait_Wave.ControlBox = false;

            frmAWait_Wave.BackColor = this.BackColor;
            Button boton_cancel_wav = new Button();
            boton_cancel_wav.Parent = frmAWait_Wave;
            boton_cancel_wav.Left = 40;
            boton_cancel_wav.Top = 20;
            boton_cancel_wav.Width = 250;
            boton_cancel_wav.Height = 27;
            boton_cancel_wav.Text = "Cancel waveform creation";
            boton_cancel_wav.BackColor = frmAWait_Wave.BackColor;
            boton_cancel_wav.TextAlign = ContentAlignment.MiddleCenter;
            boton_cancel_wav.Click += new EventHandler(boton_cancel_wav_Click);
            frmAWait_Wave.StartPosition = FormStartPosition.CenterScreen;
            frmAWait_Wave.Show();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                Process consola_pre = new Process();
                consola_pre.StartInfo.FileName = "ffmpeg.exe";
                consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + " -y " + "-filter_complex " + '\u0022' + "showwavespic=colors=#0000A0:s=800x180" + '\u0022' + " -frames:v 1" + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + "png" + '\u0022';
                consola_pre.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                consola_pre.Start();
                consola_pre.WaitForExit();
                this.InvokeEx(f => frmAWait_Wave.Close());

                if (consola_pre.ExitCode != 0)
                {
                    this.InvokeEx(f => f.Cursor = Cursors.Arrow);


                    MessageBox.Show("Waveform creation aborted", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.InvokeEx(f => f.Cursor = Cursors.Arrow);

                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    return;
                }
                else
                {

                    this.InvokeEx(f => f.Cursor = Cursors.Arrow);
                    //Form wave image
                    Form frmAddWave = new Form();
                    frmAddWave.Name = "File audio waveform";
                    frmAddWave.Text = "Audio waveform";
                    frmAddWave.Icon = this.Icon;
                    frmAddWave.Height = 280;
                    frmAddWave.Width = 810;
                    frmAddWave.FormBorderStyle = FormBorderStyle.Fixed3D;
                    frmAddWave.MaximizeBox = false;
                    frmAddWave.MinimizeBox = false;
                    frmAddWave.BackColor = this.BackColor;

                    TextBox txt_titulo = new TextBox();
                    txt_titulo.Parent = frmAddWave;
                    txt_titulo.Top = 5;
                    txt_titulo.Left = 5;
                    txt_titulo.Width = 800;
                    txt_titulo.TextAlign = HorizontalAlignment.Center;
                    txt_titulo.BorderStyle = BorderStyle.None;
                    this.InvokeEx(f => txt_titulo.Text = listView1.SelectedItems[0].Text);
                    txt_titulo.BackColor = Control.DefaultBackColor;
                    txt_titulo.Enabled = false;
                    txt_titulo.BackColor = this.BackColor;

                    PictureBox pic_wave = new PictureBox();
                    pic_wave.SizeMode = PictureBoxSizeMode.Zoom;
                    pic_wave.Parent = frmAddWave;
                    pic_wave.Left = 0;
                    pic_wave.Top = 20;
                    pic_wave.BackColor = this.BackColor;
                    String pic_file = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + "png";
                    Image img;
                    using (var bmpTemp = new Bitmap(pic_file))
                    {
                        img = new Bitmap(bmpTemp);
                    }
                    pic_wave.Image = img;
                    if (File.Exists(pic_file))
                    {

                        File.Delete(pic_file);
                    }
                    if (Directory.Exists(destino_test))
                    {
                        int num = Directory.EnumerateFiles(destino_test, "*.*").Count();
                        if (num == 0)
                        {
                            Directory.Delete(destino_test);
                        }
                    }

                    pic_wave.Width = 800;
                    pic_wave.Height = 180;
                    pic_wave.BorderStyle = BorderStyle.FixedSingle;

                    Button boton_ok_wave = new Button();
                    boton_ok_wave.Parent = frmAddWave;
                    boton_ok_wave.Left = 330;
                    boton_ok_wave.Top = 205;
                    boton_ok_wave.Width = 120;
                    boton_ok_wave.Height = 27;
                    boton_ok_wave.Text = "Close window";
                    boton_ok_wave.BackColor = this.BackColor;
                    boton_ok_wave.Click += new EventHandler(boton_ok_wave_Click);

                    frmAddWave.StartPosition = FormStartPosition.CenterScreen;
                    frmAddWave.ShowDialog();
                    frmAddWave.Disposed += new EventHandler(frmAddWave_Disposed);
                }

            }).Start();
            //END try preset
        }

        private void boton_cancel_wav_Click(object sender, EventArgs e)
        {
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
                p.Kill();
            System.Threading.Thread.Sleep(500);
            Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
            foreach (Process p2 in localByName2)
                p2.Kill();

            //throw new NotImplementedException();
        }

        private void frmAddWave_Disposed(object sender, EventArgs e)
        {

            //throw new NotImplementedException();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //clean file blank lines

            String path3 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            if (File.Exists(path3))
            {

                String path_temp = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_temp.txt";

                int i2 = 0;

                foreach (string line in File.ReadLines(path3))
                {
                    i2 = i2 + 1;

                    if (line != String.Empty)
                    {

                        if (i2 < File.ReadAllLines(path3).Count())
                        {
                            File.AppendAllText(path_temp, line + Environment.NewLine);

                        }
                        else
                        {
                            File.AppendAllText(path_temp, line);

                        }
                    }
                }
                File.Delete(path3);
                File.Copy(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_temp.txt", path3);
                File.Delete(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_temp.txt");
            }

            int num = 0;
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            num = localByName.Length;
            if (num > 0)
            {
                var a = MessageBox.Show("FFmpeg processes are still running. If you quit now they will be terminated. Are you sure", "Confirm forced exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (a == DialogResult.Yes)
                {
                    Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                    foreach (Process p in localByName2)
                        p.Kill();
                    System.Threading.Thread.Sleep(500);
                    Process[] localByName3 = Process.GetProcessesByName("ffmpeg");
                    foreach (Process p in localByName3)
                        p.Kill();
                    System.Threading.Thread.Sleep(250);
                    Environment.Exit(0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            if (notifyIcon1 != null)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
            this.BringToFront();
            this.Focus();
        }

        private void label13_DoubleClick(object sender, EventArgs e)
        {
            textBox3.Text = "..\\FFBatch";
            textBox3.BackColor = Control.DefaultBackColor;
        }

        private void label13_Click(object sender, EventArgs e)
        {
            textBox3.Text = "..\\FFBatch";
            textBox3.BackColor = Control.DefaultBackColor;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                lbl_size.Visible = true;
                label12.Visible = true;
                label9.Visible = true;

                groupBox_m3u.Visible = false;
                list_tracks.Visible = false;
                checkBox3.Visible = true;
                chkshut.Visible = true;
                TB2.Visible = false;
                combo_ext.Visible = false;
                label19.Visible = false;
                btn_set_mux_def.Visible = false;
                lbl_tr_n.Visible = false;

                groupBox15.Visible = false;
                label17.Visible = false;
                combo_item_lang_2.Visible = false;

                button12.Visible = true;
                button18.Visible = true;
                label13.Visible = true;
                textBox3.Visible = true;
                button21.Visible = true;
                chkshut.Visible = true;
                btn_pause.Visible = true;
                btn_mux.Visible = false;
                btn_extract.Visible = false;
                txt_track_format.Visible = false;
                btn_mux_cancel.Visible = false;
                btn_del_track.Visible = false;
                btn_default_track.Visible = false;
                button23.Visible = false;
                pic_encode_param.Visible = false;
                lbl_mux_par.Visible = false;
                txt_track_param.Visible = false;
                btn_set_track_param.Visible = false;
                groupBox1.Visible = true;
                panel1.Visible = true;
                list_tracks.Visible = false;


                return;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                if (chkshut.Checked == false)
                {
                    TB2.Visible = false;
                }
                else
                {
                    TB2.Visible = true;
                }
                    lbl_size.Visible = true;
                lbl_tr_n.Visible = true;
                label12.Visible = true;
                label9.Visible = true;

                groupBox_m3u.Visible = false;
                list_tracks.Visible = true;
                chkshut.Visible = true;
                btn_pause.Visible = true;

                checkBox3.Visible = false;
                btn_set_mux_def.Visible = true;
                btn_set_track_param.Visible = true;

                combo_ext.Visible = true;
                label19.Visible = true;
                groupBox15.Visible = false;
                label17.Visible = true;
                combo_item_lang_2.Visible = true;

                label13.Visible = false;
                textBox3.Visible = false;
                button21.Visible = false;
                
                btn_mux.Visible = true;
                btn_extract.Visible = true;
                txt_track_format.Visible = true;
                btn_del_track.Visible = true;
                btn_default_track.Visible = true;
                btn_mux_cancel.Visible = true;
                btn_mux.Refresh();
                button23.Visible = true;
                pic_encode_param.Visible = true;
                lbl_mux_par.Visible = true;
                txt_track_param.Visible = true;
                btn_set_track_param.Visible = true;
                groupBox1.Visible = false;
                panel1.Visible = false;
                list_tracks.Visible = true;
                list_tracks.Refresh();


                int list_int = 0;
                if (listView1.Items.Count == listView2.Items.Count)
                {

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (listView2.Items[list_int].Text == item.Text)
                        {
                            list_int = list_int + 1;

                        }
                    }


                    if (list_int == listView1.Items.Count)
                    {
                        return;
                    }
                }
                else
                {
                    if (listView1.Items.Count > 200)
                    {
                        var a = MessageBox.Show("Obtaining streams for " + listView1.Items.Count + " files can take a long time. Continue?", "Too many files to be parsed", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (a == DialogResult.Cancel)
                        {
                            tabControl1.SelectedIndex = 0;

                            return;

                        }
                        listView2.Clear();
                        add_to_tab_2();
                    }
                    else
                    {
                        listView2.Clear();
                        add_to_tab_2();
                        return;
                    }
                }

                listView2.Clear();
                add_to_tab_2();
            }

            if (tabControl1.SelectedIndex == 2)
            {
                lbl_tr_n.Visible = false;
                lbl_size.Visible = true;
                label12.Visible = true;
                label9.Visible = true;

                groupBox_m3u.Visible = false;
                list_tracks.Visible = false;
                groupBox15.Visible = true;
                chkshut.Visible = false;
                btn_pause.Visible = false;
                btn_set_mux_def.Visible = false;
                btn_extract.Visible = false;
                txt_track_format.Visible = false;
                btn_del_track.Visible = false;
                btn_default_track.Visible = false;
                btn_set_track_param.Visible = false;

                groupBox15.Visible = true;

                chkshut.Checked = false;
                chkshut.Visible = false;
                TB1.Visible = false;
                txt_hard_subs.Text = textBox1.Text;

                int list_int = 0;

                if (listView1.Items.Count == listView3.Items.Count)
                {

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (listView3.Items[list_int].Text == item.Text)
                        {
                            list_int = list_int + 1;

                        }
                    }


                    if (list_int == listView1.Items.Count)
                    {
                        return;
                    }
                }
                else
                {
                    if (listView1.Items.Count > 1000)
                    {
                        var a = MessageBox.Show("Adding " + listView1.Items.Count + " files can take some time. Continue?", "Many files to be parsed", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (a == DialogResult.Cancel)
                        {
                            tabControl1.SelectedIndex = 0;

                            return;

                        }
                        listView3.Items.Clear();
                        add_to_tab_3();
                    }
                    else
                    {
                        listView3.Items.Clear();
                        add_to_tab_3();
                        return;
                    }
                }

                listView3.Items.Clear();
                add_to_tab_3();
            }

            if (tabControl1.SelectedIndex == 3)
            {
                lbl_size.Visible = false;
                lbl_tr_n.Visible = false;
                label12.Visible = false;
                label9.Visible = false;
                groupBox15.Visible = false;
                list_tracks.Visible = false;
                groupBox_m3u.Visible = true;
                chkshut.Visible = false;
                btn_pause.Visible = false;
            }
        }

        private void add_to_tab_3()

        {

            listView3.SmallImageList = listView1.SmallImageList;
            foreach (ListViewItem item in listView1.Items)
            {

                this.Cursor = Cursors.WaitCursor;
                ListViewItem elemento = new ListViewItem(item.Text, 1);
                //Begin get file icon
                Icon iconForFile = SystemIcons.WinLogo;

                // Check to see if the image collection contains an image
                // for this extension, using the extension as a key.
                if (!imageList2.Images.ContainsKey(System.IO.Path.GetExtension(item.Text)))
                {
                    // If not, add the image to the image list.
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(item.Text);
                    imageList3.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                    imageList2.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                }

                elemento.ImageKey = System.IO.Path.GetExtension(item.Text);

                listView3.Items.Add(elemento);

            }

            foreach (ListViewItem item in listView3.Items)
            {
                String is_srt = item.Text.Substring(item.Text.LastIndexOf("."));

                if (is_srt == ".srt")
                {
                    listView3.Items.RemoveAt(item.Index);
                }

                String is_Vobsub = item.Text.Substring(item.Text.LastIndexOf("."));

                if (is_Vobsub == ".idx" || is_Vobsub == ".sub")
                {
                    listView3.Items.RemoveAt(item.Index);
                }

                String is_ass = item.Text.Substring(item.Text.LastIndexOf("."));

                if (is_srt == ".ass")
                {
                    listView3.Items.RemoveAt(item.Index);
                }

                String subs_path = String.Empty;
                String Sub_File_SRT = String.Empty;
                String Sub_File_Vobsub = String.Empty;
                String Sub_File_ASS = String.Empty;

                if (txt_folder_subs.Text == String.Empty)
                {
                    Sub_File_SRT = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".srt";
                    Sub_File_Vobsub = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".idx";
                    Sub_File_ASS = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".ass";
                }
                else
                {
                    String path_sub = txt_folder_subs.Text;
                    Path.GetFileName(item.Text);
                    Sub_File_SRT = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".srt";
                    Sub_File_Vobsub = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".idx";
                    Sub_File_ASS = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".ass";
                }

                if (item.SubItems[0].Text.Contains("No subtitle for this file") == false)
                {


                    if (File.Exists(Sub_File_SRT))
                    {
                        item.SubItems.Add("SRT subtitle available");
                        item.SubItems.Add("und");

                        var a = TextFileEncodingDetector.DetectTextFileEncoding(Sub_File_SRT);
                        if (a != null)
                        {
                            String txt_enc = a.ToString().Replace("System.Text.", "");
                            item.SubItems.Add(txt_enc.Replace("Encoding", ""));
                        }
                        else
                        {
                            item.SubItems.Add("Unknown");
                        }
                        item.SubItems.Add("Yes");
                        item.SubItems.Add("Ready");
                    }

                    if (File.Exists(Sub_File_Vobsub))
                    {
                        item.SubItems.Add("VobSub IDX subtitle available");
                        item.SubItems.Add("und");

                        item.SubItems.Add("-");
                        item.SubItems.Add("Yes");
                        item.SubItems.Add("Ready");
                    }
                    if (File.Exists(Sub_File_ASS))
                    {
                        item.SubItems.Add("ASS subtitle available");
                        item.SubItems.Add("und");

                        var a = TextFileEncodingDetector.DetectTextFileEncoding(Sub_File_ASS);
                        if (a != null)
                        {
                            String txt_enc = a.ToString().Replace("System.Text.", "");
                            item.SubItems.Add(txt_enc.Replace("Encoding", ""));
                        }
                        else
                        {
                            item.SubItems.Add("Unknown");
                        }
                        item.SubItems.Add("Yes");
                        item.SubItems.Add("Ready");

                    }
                }

                if (!File.Exists(Sub_File_Vobsub) && !File.Exists(Sub_File_SRT) && !File.Exists(Sub_File_ASS))
                {
                    item.SubItems.Add("No subtitle for this file, double-click to locate");
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                    item.SubItems.Add("-");
                    item.SubItems.Add("No Sub");
                }

            }
            Combo_sub_lang_mux.Text = "";
            this.Cursor = Cursors.Arrow;


        }

        private void ctm2_Opening_1(object sender, CancelEventArgs e)
        {
            if (listView2.SelectedItems.Count == 0)
            {
                e.Cancel = true;
                ct2_a.Enabled = false;
                ct2_v.Enabled = false;
                ct2_s.Enabled = false;
            }
            else
            {
                ct2_a.Enabled = true;
                ct2_v.Enabled = true;
                ct2_s.Enabled = true;
                combo_def_und_lang.Text = "Set language for undefined tracks";
            }

        }

        private void ct2_v_Click(object sender, EventArgs e)
        {
            Boolean has_video = false;
            Boolean stream_found = false;
            foreach (ListViewItem track_item in list_tracks.Items)
            {
                if (track_item.SubItems[2].Text.Contains("Video"))
                {

                    has_video = true;
                    MessageBox.Show("List already contains a video track", "Video track already present", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            foreach (ListViewItem item in listView2.SelectedItems)
            {

                for (int i = 1; i < item.SubItems.Count; i++)
                {

                    if (item.SubItems[i].Text.Contains("Video"))
                    {
                        stream_found = true;

                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 0);
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());

                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_video == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }

                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_video_enc);

                    }
                    tracks_background();
                }

                foreach (ListViewItem track_item in list_tracks.Items)
                {
                    if (track_item.SubItems[2].Text.Contains("Video"))
                    {
                        list_tracks.Items.RemoveAt(track_item.Index);
                        list_tracks.Items.Insert(0, track_item);

                    }
                }

                if (stream_found == false)
                {
                    MessageBox.Show("File does not contain video streams", "No video found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ct2_a_Click(object sender, EventArgs e)
        {
            Boolean has_audio = false;
            foreach (ListViewItem track_item in list_tracks.Items)
            {
                if (track_item.SubItems[2].Text.Contains("Audio"))
                {
                    has_audio = true;
                }
            }

            foreach (ListViewItem item in listView2.SelectedItems)
            {
                Boolean stream_found = false;
                for (int i = 1; i < item.SubItems.Count; i++)
                {

                    if (item.SubItems[i].Text.Contains("Audio"))
                    {
                        stream_found = true;

                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 1);
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());

                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_audio == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }

                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_audio_enc);
                    }

                }

                if (stream_found == false)
                {
                    MessageBox.Show("File does not contain audio streams", "No audio found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            tracks_background();
        }

        private void ct2_s_Click(object sender, EventArgs e)
        {
            if (notifyIcon1 != null)
            {
                notifyIcon1.Dispose();
            }

            Boolean has_subs = false;
            foreach (ListViewItem track_item in list_tracks.Items)
            {
                if (track_item.SubItems[2].Text.Contains("Subtitle"))
                {
                    has_subs = true;
                }
            }

            foreach (ListViewItem item in listView2.SelectedItems)
            {
                Boolean stream_found = false;
                for (int i = 1; i < item.SubItems.Count; i++)
                {

                    if (item.SubItems[i].Text.Contains("Subtitle"))
                    {
                        stream_found = true;
                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 2);
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());

                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_subs == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }
                        if (item.SubItems[i].Text.Contains("dvd_subtitle") || (item.SubItems[i].Text.Contains("hdmv_pgs_subtitle")))


                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("copy");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_subs_enc);
                        }

                    }

                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Subtitle"))
                        {
                            list_tracks.Items.RemoveAt(track_item.Index);
                            list_tracks.Items.Insert(list_tracks.Items.Count, track_item);

                        }
                    }
                }

                if (stream_found == false)
                {
                    MessageBox.Show("No subtitles stream found", "No subtitles found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            tracks_background();
        }

        private void btn_mux_Click(object sender, EventArgs e)
        {
            cancel_queue = false;

            foreach (ListViewItem file in list_tracks.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the track list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (list_tracks.Items.Count == 0)
            {
                MessageBox.Show("Tracks list is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            Boolean video_track_in = false;
            foreach (ListViewItem tracks_item in list_tracks.Items)
            {
                if (tracks_item.SubItems[2].Text.Contains("Video"))
                {
                    video_track_in = true;
                }
            }

            if (video_track_in == false)
            {
                MessageBox.Show("A video track is required for muxing", "No video track found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            String is_overw = textBox3.Text + "\\" + Path.GetFileNameWithoutExtension(list_tracks.Items[0].Text) + "." + combo_ext.SelectedItem.ToString();

            if (is_overw == list_tracks.Items[0].Text)
            {
                MessageBox.Show("Overwriting is not supported. Change destination directory on main screen or double-click on the textbox to reset to default", "Overwriting not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DateTime time2;
            if (!DateTime.TryParse(ss_time_input.Text, out time2))
            {
                MessageBox.Show("Pre-input seeking selected in Batch processing tab is incorrect. Change it or reset it by double-clicking on it", "Pre-input seeking format error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validated list, start processing
            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            Disable_Controls();
            txt_remain.Text = "Time remaining:";
            time_n_tasks = 0;
            timer_tasks.Start();

            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;


            //Copy list of tracks for thread processing
            ListView list_proc = new ListView();
            foreach (ListViewItem item in list_tracks.Items)
            {
                list_proc.Items.Add((ListViewItem)item.Clone());

            }
            //End of copy list of tracks for thread processing

            Pg1.Maximum = list_proc.Items.Count;

            Double total_duration = 0;
            Double total_prog = 0;


            //Get specific track list video duration
            //Duration
            Boolean has_audio_for_image = false;
            Process probe = new Process();
            probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");

            if (list_proc.Items[0].BackColor == Color.LightYellow)
            {
                foreach (ListViewItem item1 in list_proc.Items)
                {

                    if (item1.SubItems[2].Text.Contains("Audio"))
                    {

                        has_audio_for_image = true;
                    }
                }
                if (has_audio_for_image == false)
                {
                    MessageBox.Show("No audio found in the track list to mux with image", "Error muxing with image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    working = false;
                    Enable_Controls();
                    return;
                }
                else
                {
                    probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + list_proc.Items[1].Text + '\u0022';
                }

            }

            else
            {
                probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + list_proc.Items[0].Text + '\u0022';

            }

            probe.StartInfo.RedirectStandardOutput = true;
            probe.StartInfo.UseShellExecute = false;
            probe.StartInfo.CreateNoWindow = true;
            probe.EnableRaisingEvents = true;
            probe.Start();
            
            String duracion = probe.StandardOutput.ReadLine();

            probe.WaitForExit();

            if (duracion != null)
            {

                if (duracion.Length >= 7)
                {
                    //total_duration = Convert.ToDouble(duracion.Substring(0, 7));
                    durat_n = TimeSpan.Parse(duracion).TotalSeconds;
                    total_duration = TimeSpan.Parse(duracion).TotalSeconds;
                }
            }
            else
            {
                total_duration = 0;
            }

            //End duration

            //End 

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";

            List<string> list_lines = new List<string>();
            String mux_ext = combo_ext.Text;

            // process_glob.StartInfo.Arguments = String.Empty;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                String remain_time = "0";


                this.InvokeEx(f => f.pg_current.Value = 0);
                this.InvokeEx(f => f.pg_current.Refresh());

                String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");

                String file = list_proc.Items[0].Text;
                String fullPath = file;
                String destino = "";


                if (textBox3.Text == "..\\FFBatch")
                {
                    destino = file.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                }

                else
                {
                    destino = textBox3.Text;
                }

                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                }

                //Create joint inputs variable
                String inputs = String.Empty;
                foreach (ListViewItem input_item in list_proc.Items)
                {

                    if (input_item.SubItems[2].Text.Contains("Subtitle") && !input_item.SubItems[2].Text.Contains("hdmv_pgs") && !input_item.SubItems[2].Text.Contains("dvd_subtitle"))
                    {
                        inputs = inputs + " -sub_charenc UTF-8" + " -i " + '\u0022' + input_item.Text + '\u0022';
                    }

                    else
                    {
                        if (input_item.BackColor != Color.LightYellow)
                        {
                            inputs = inputs + " -i " + '\u0022' + input_item.Text + '\u0022';
                        }
                        else
                        {
                            String ext_image = Path.GetExtension(list_proc.Items[0].Text);

                            //Attempt to extract frame as image
                            Process proc_img = new System.Diagnostics.Process();
                            String ffm_img = Path.Combine(Application.StartupPath, "ffmpeg.exe");

                            String file_img = Path.GetFullPath(list_proc.Items[0].Text);
                            String fullPath_img = file_img;
                            String AppParam_img = "";

                            if (ext_image != ".jpg" && ext_image != ".jpeg" && ext_image != ".png" && ext_image != ".gif" && ext_image != ".bmp" && ext_image != ".tiff" && ext_image != ".psd")
                            {
                                AppParam_img = " -ss " + ss_time_input.Text + " -i " + "" + '\u0022' + file_img + '\u0022' + " -vframes 1 -f image2" + " -qscale:v 2" + " -vf scale=1280:-2" + " -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_img) + "." + "jpg" + '\u0022';
                            }
                            else
                            {
                                AppParam_img = " -i " + "" + '\u0022' + file_img + '\u0022' + " -qscale:v 2" + " -vf scale=1280:-2" + " -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_img) + "." + "jpg" + '\u0022';
                            }
                            proc_img.StartInfo.RedirectStandardOutput = false;
                            proc_img.StartInfo.RedirectStandardError = false;
                            proc_img.StartInfo.UseShellExecute = true;
                            proc_img.StartInfo.CreateNoWindow = false;
                            proc_img.EnableRaisingEvents = false;
                            proc_img.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                            proc_img.StartInfo.FileName = ffm_img;
                            proc_img.StartInfo.Arguments = AppParam_img;

                            proc_img.Start();
                            proc_img.WaitForExit();
                            if (proc_img.ExitCode == 0)
                            {
                                String extracted_img = "" + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_img) + ".jpg" + '\u0022' + "";
                                inputs = inputs + " -i " + extracted_img;
                            }
                            else
                            {

                                MessageBox.Show("Error extracting image from video track.", "Error using as image for audio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Enable_Controls();
                                working = false;
                                return;
                            }
                            //End extract frame as image

                        }

                    }
                }
                //End create joint inputs variable


                //Create mapping inputs variable
                String input_map = String.Empty;
                //Add video track, it must be first
                if (list_proc.Items[0].BackColor == Color.LightYellow)
                {
                    //Video track is an still image


                    //Attempt to extract first frame from video track as image

                    input_map = input_map + " -map 0:0" + " -c:v libx264 -r 1 -crf 29 -preset veryfast -x264-params keyint=1 -pix_fmt yuv420p";

                }
                else
                {
                    input_map = input_map + " -map 0:" + list_proc.Items[0].SubItems[1].Text + " -c:v " + list_proc.Items[0].SubItems[5].Text + " -metadata:s:v:0 language=" + list_proc.Items[0].SubItems[3].Text;
                }

                int int_auds = 0;
                int i_subs = 0;

                for (int i = 1; i < list_tracks.Items.Count; i++)

                {

                    //Audio tracks
                    if (list_proc.Items[i].SubItems[2].Text.Contains("Audio"))
                    {
                        String is_default = String.Empty;
                        if (list_proc.Items[i].SubItems[4].Text == "Yes")
                        {
                            is_default = "-disposition:a:" + (int_auds).ToString() + " default";
                        }
                        else
                        {
                            is_default = "-disposition:a:" + (int_auds).ToString() + " 0";
                        }
                        String track_codec = " -c:a copy ";
                        if (list_proc.Items[i].SubItems[5].Text != "copy")
                        {
                            track_codec = " -c:a:" + int_auds + " " + list_proc.Items[i].SubItems[5].Text;
                        }
                        input_map = input_map + " -map " + (i).ToString() + ":" + list_proc.Items[i].SubItems[1].Text + track_codec + " -metadata:s:a:" + (int_auds).ToString() + " language=" + list_proc.Items[i].SubItems[3].Text + " " + is_default;
                        int_auds = int_auds + 1;
                    }

                }

                for (int i = 1; i < list_tracks.Items.Count; i++)

                {
                    //Subtitle tracks
                    if (list_proc.Items[i].SubItems[2].Text.Contains("Subtitle"))
                    {
                        String is_default = String.Empty;
                        if (list_proc.Items[i].SubItems[4].Text == "Yes")
                        {
                            is_default = "-disposition:s:" + (i_subs).ToString() + " default";
                        }
                        else
                        {
                            is_default = "-disposition:s:" + (i_subs).ToString() + " 0";
                        }


                        input_map = input_map + " -map " + (i).ToString() + ":" + list_proc.Items[i].SubItems[1].Text + " -c:s " + list_proc.Items[i].SubItems[5].Text + " -metadata:s:s:" + (i_subs).ToString() + " language=" + list_proc.Items[i].SubItems[3].Text + " " + is_default;

                        i_subs = i_subs + 1;
                    }
                }

                String AppParam = inputs + input_map + " -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + "." + mux_ext + '\u0022';

                if (list_proc.Items[0].BackColor == Color.LightYellow)
                {
                    AppParam = " -loop 1 -r 6 " + inputs + input_map + " -shortest " + "-y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + "." + mux_ext + '\u0022';
                }

                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                }

                process_glob.StartInfo.FileName = ffm;
                process_glob.StartInfo.Arguments = AppParam;
                valid_prog = false;

                this.InvokeEx(f => f.textBox7.Text = "0%");
                this.InvokeEx(f => f.textBox7.Refresh());
                this.InvokeEx(f => f.pg_current.Value = 0);
                this.InvokeEx(f => f.pg_current.Refresh());


                process_glob.StartInfo.RedirectStandardOutput = true;
                process_glob.StartInfo.RedirectStandardError = true;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.EnableRaisingEvents = true;
                process_glob.Start();
                if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                valid_prog = true;

                String err_txt = "";
                Double interval = 0;

                while (!process_glob.StandardError.EndOfStream)
                {
                    err_txt = process_glob.StandardError.ReadLine();
                    list_lines.Add(err_txt);


                    if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                    {

                        //this.InvokeEx(f => durat_n = TimeSpan.Parse(listView1.Items[0].SubItems[2].Text).TotalSeconds);
                        total_prog = durat_n;
                        int start_time_index = err_txt.IndexOf("time=") + 5;
                        Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                        Double percent = (sec_prog * 100 / durat_n);

                        total_prog = total_prog + (sec_prog - interval);
                        interval = sec_prog;
                        int percent2 = Convert.ToInt32(percent);


                        if (percent2 <= 100)
                        {
                            this.InvokeEx(f => f.pg_current.Value = percent2);
                            this.InvokeEx(f => f.pg_current.Refresh());
                            this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                            this.InvokeEx(f => f.textBox4.Refresh());
                            this.InvokeEx(f => f.Pg1.Value = percent2);
                            this.InvokeEx(f => f.Pg1.Refresh());
                            this.InvokeEx(f => f.textBox5.Text = (percent2).ToString() + "%");
                            this.InvokeEx(f => f.textBox5.Refresh());

                        }

                        //Estimated remaining time

                        remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                        remain_time = remain_time.Replace("x", String.Empty);
                        Double timing1 = 0;

                        if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                        }
                        else
                        {
                            timing1 = Math.Round(Double.Parse(remain_time), 2);
                        }
                        Decimal timing = (decimal)timing1;
                        Decimal total_dur_dec = Convert.ToDecimal(interval);
                        Decimal total_prog_dec = Convert.ToDecimal(total_prog);

                        Decimal remain_secs = 0;
                        if (timing > 0)
                        {
                            remain_secs = (decimal)(total_prog_dec - total_dur_dec) / timing;
                        }

                        if (remain_secs > 60)
                        {
                            remain_secs = remain_secs + 60;
                        }
                        String remain_from_secs = "";

                        TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                        remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                           t.Hours,
                          t.Minutes);

                        if (remain_secs >= 3600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                        }

                        if (remain_secs < 3600 && remain_secs >= 600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                        }
                        if (remain_secs < 600 && remain_secs >= 120)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                        }

                        if (remain_secs <= 59)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                        }

                        //End remaining time    
                    }

                    //Read output, get progress
                    this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                    this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                }
                process_glob.WaitForExit();
                process_glob.StartInfo.Arguments = String.Empty;

                this.InvokeEx(f => f.pg_current.Value = 100);
                this.InvokeEx(f => f.textBox4.Text = "100%");
                list_lines.Add("");
                list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                list_lines.Add("");

                this.InvokeEx(f => f.Pg1.Value = 100);
                this.InvokeEx(f => f.textBox5.Text = "100%");
                working = false;
                //Save log
                string[] array_err = list_lines.ToArray();
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                SaveFile.WriteLine("-------------------------------");
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
                Enable_Controls();
                
                if (process_glob.ExitCode == 0 && cancel_queue == false)
                {
                    if (chkshut.Checked)
                    {
                        Disable_Controls();
                        groupBox2.Enabled = true;
                        foreach (Control ct in groupBox2.Controls)
                        {
                            this.InvokeEx(f => ct.Enabled = false);
                        }

                        this.InvokeEx(f => f.TB2.Enabled = true);
                        this.InvokeEx(f => f.chkshut.Enabled = false);
                        this.InvokeEx(f => f.btn_pause.Enabled = false);
                        this.InvokeEx(f => f.Timer_apaga.Start());
                        this.InvokeEx(f => f.TopMost = true);
                        this.InvokeEx(f => f.TB1.Enabled = true);
                        this.InvokeEx(f => f.TB1.Visible = true);
                        this.InvokeEx(f => f.button10.Enabled = true);
                        this.InvokeEx(f => f.button10.Visible = true);
                        this.InvokeEx(f => f.button20.Enabled = false);
                        this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                        this.InvokeEx(f => f.TB2.Text = "Shutting shutdown in 60 seconds");
                        notifyIcon1.Visible = true;
                        notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                        notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                        notifyIcon1.ShowBalloonTip(0);
                        //String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                        //foreach (string file_s in Directory.GetFiles(destino_test))
                        //{
                        //File.Delete(file_s);
                        //}
                        //System.IO.Directory.Delete(destino_test);
                        return;
                    }

                    //End shutdown check
                    else
                    {
                        if (Form.ActiveForm == null)
                        {
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipText = "Multiplexing completed";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                        }

                        if (checkBox3.Checked)
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
                }
                else
                {
                    if (process_glob.ExitCode ==0)
                    { 
                    this.InvokeEx(f => f.textBox5.Text = "100%");
                    this.InvokeEx(f => MessageBox.Show("Multiplexing aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                    else
                    {
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        this.InvokeEx(f => MessageBox.Show("Multiplexing failed: " + listBox4.Items[listBox4.Items.Count - 1].ToString(), "Error multiplexing", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                }
                
            }).Start();
        }

        private void ct2_all_Click(object sender, EventArgs e)
        {
            
            foreach (ListViewItem item in listView2.SelectedItems)
            {
                
                if (item.SubItems[1].Text == "No usable streams found")
                {
                    MessageBox.Show("File does not contain usable streams", "No streams found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                for (int i = 1; i < item.SubItems.Count; i++)
                {   
                    //Video
                    Boolean has_video = false;
                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Video"))
                        {
                            has_video = true;

                        }
                    }

                    if (item.SubItems[i].Text.Contains("Video") && has_video == false)
                    {
                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 0);

                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());

                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_video == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_video_enc);
                    }

                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Video"))
                        {
                            list_tracks.Items.RemoveAt(track_item.Index);
                            list_tracks.Items.Insert(0, track_item);

                        }
                    }

                    // Audio
                    Boolean has_audio = false;
                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Audio"))
                        {
                            has_audio = true;
                        }
                    }


                    if (item.SubItems[i].Text.Contains("Audio"))
                    {

                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 1);
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());

                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_audio == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_audio_enc);
                    }

                    //Subtitles

                    Boolean has_subs = false;
                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Subtitle"))
                        {
                            has_subs = true;
                        }
                    }

                    if (item.SubItems[i].Text.Contains("Subtitle"))
                    {
                        list_tracks.Items.Add(listView2.SelectedItems[0].Text, 2);
                        list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add((i - 1).ToString());
                        
                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(7, listView2.SelectedItems[0].SubItems[i].Text.Length - 7));
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text);
                        }


                        if (listView2.SelectedItems[0].SubItems[i].Text.Substring(0,1) == "(")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(1, 3));
                        }
                        
                        else if (listView2.SelectedItems[0].SubItems[i].Text.Substring(3, 1) == ")")
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(listView2.SelectedItems[0].SubItems[i].Text.Substring(0, 3));
                        }
                            
                         else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_lang_und_tracks);
                        }


                        if (has_subs == false)
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("Yes");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("No");
                        }
                        if (item.SubItems[i].Text.Contains("dvd_subtitle") || item.SubItems[i].Text.Contains("hdmv_pgs_subtitle"))


                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add("copy");
                        }
                        else
                        {
                            list_tracks.Items[list_tracks.Items.Count - 1].SubItems.Add(def_mux_subs_enc);
                        }
                    }

                    foreach (ListViewItem track_item in list_tracks.Items)
                    {
                        if (track_item.SubItems[2].Text.Contains("Subtitle"))
                        {
                            list_tracks.Items.RemoveAt(track_item.Index);
                            list_tracks.Items.Insert(list_tracks.Items.Count, track_item);

                        }
                    }
                }                
            }
            
            tracks_background();
         }

        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            ct2_all.PerformClick();
        }

        private void list_tracks_KeyUp(object sender, KeyEventArgs e)
        {
            Boolean has_default = false;
            if (e.KeyCode == Keys.Delete)
            {
                foreach (ListViewItem elemento in list_tracks.SelectedItems)
                {
                    list_tracks.Items.Remove(elemento);
                    pic_encode_param.Image = null;
                }
            }
            lbl_tr_n.Text = "Tracks: " + list_tracks.Items.Count.ToString();
            //Review audio track defaults

            foreach (ListViewItem audio_item in list_tracks.Items)
            {
                if (audio_item.SubItems[2].Text.Contains("Audio"))
                {
                    if (audio_item.SubItems[4].Text == "Yes")
                    {
                        has_default = true;
                    }
                }
            }
            if (has_default == false)
            {
                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Audio"))
                    {
                        audio_item.SubItems[4].Text = "Yes";
                        return;
                    }
                }
            }
            //End review audio track defaults
            
        }

        private void ctm3_Opening(object sender, CancelEventArgs e)
        {
            foreach (ToolStripItem ct in ctm3.Items)
            {
                ct.Visible = true;
            }

            if (list_tracks.Items.Count == 0 || list_tracks.SelectedIndices.Count == 0)
            {
                ct3_combo_language.Text = "Item language";
                e.Cancel = true;                
            }

            if (list_tracks.SelectedIndices.Count > 1)
            {
                foreach (ToolStripItem ct in ctm3.Items)
                {
                    ct.Visible = false;
                    ct3_del.Visible = true;
                }
            }
        }
        
        private void ct3_combo_language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in list_tracks.SelectedItems)
                {
                    item.SubItems[3].Text = ct3_combo_language.SelectedItem.ToString().Substring(ct3_combo_language.SelectedItem.ToString().Length - 4, 3);
                }
                ctm3.Close();
            }
        }

        private void ct3_default_Click(object sender, EventArgs e)
        {

            if (list_tracks.SelectedIndices.Count == 0 || list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Video"))
            {
                return;
            }


            if (list_tracks.SelectedItems[0].SubItems[4].Text == "Yes")
            {
                list_tracks.SelectedItems[0].SubItems[4].Text = "No";
            }
            else
            {
                list_tracks.SelectedItems[0].SubItems[4].Text = "Yes";
            }


            //Review audio defaults
            int default_items = 0;

            for (int i = 0; i < list_tracks.Items.Count; i++)
            {
                if (list_tracks.Items[i].SubItems[2].Text.Contains("Audio"))
                {
                    if (list_tracks.Items[i].SubItems[4].Text == "Yes")
                    {
                        default_items = default_items + 1;
                    }
                }
            }

            if (default_items > 1)
            {
                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Audio"))
                    {
                        if (audio_item.Text != list_tracks.SelectedItems[0].Text)
                        {
                            audio_item.SubItems[4].Text = "No";
                        }

                    }
                }
            }

            if (default_items == 0)
            {
                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Audio"))
                    {
                        if (audio_item.Text != list_tracks.SelectedItems[0].Text)
                        {
                            audio_item.SubItems[4].Text = "Yes";
                            return;
                        }
                    }
                }
            }


            //End review audio track defaults

            //Begin subtitle defaults

            //Review audio defaults
            int default_subs = 0;
            for (int i = 0; i < list_tracks.Items.Count; i++)
            {
                if (list_tracks.Items[i].SubItems[2].Text.Contains("Subtitle"))
                {
                    if (list_tracks.Items[i].SubItems[4].Text == "Yes")
                    {
                        default_subs = default_subs + 1;
                    }
                }
            }

            if (default_subs > 1)
            {
                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Subtitle"))
                    {
                        if (audio_item != list_tracks.SelectedItems[0])
                        {
                            audio_item.SubItems[4].Text = "No";
                        }

                    }
                }
            }
            //End subtitle defaults
        }


        private void ct3_del_Click(object sender, EventArgs e)
        {
            Boolean has_default = false;

            foreach (ListViewItem elemento in list_tracks.SelectedItems)
            {
                list_tracks.Items.Remove(elemento);
                pic_encode_param.Image = null;
            }
            lbl_tr_n.Text = "Tracks: " + list_tracks.Items.Count.ToString();
            //Review audio track defaults

            foreach (ListViewItem audio_item in list_tracks.Items)
            {
                if (audio_item.SubItems[2].Text.Contains("Audio"))
                {
                    if (audio_item.SubItems[4].Text == "Yes")
                    {
                        has_default = true;
                    }
                }
            }

            if (has_default == false)
            {
                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Audio"))
                    {
                        audio_item.SubItems[4].Text = "Yes";
                        return;
                    }
                }
            }
            //End review audio track defaults
            
        }

        private void button23_Click(object sender, EventArgs e)
        {
            list_tracks.Items.Clear();
            txt_track_param.Text = String.Empty;
            pic_encode_param.Image = null;
            combo_item_lang_2.SelectedIndex = -1;
            txt_track_format.Text = String.Empty;
            lbl_tr_n.Text = "Tracks: 0";
        }

        private void list_tracks_DoubleClick(object sender, EventArgs e)
        {
            ct3_default.PerformClick();
        }


        private void ctm3_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {

        }

        private void ct3_combo_language_Click(object sender, EventArgs e)
        {

        }

        private void list_tracks_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (list_tracks.SelectedIndices.Count > 0)
            {
                txt_track_param.Text = list_tracks.SelectedItems[0].SubItems[5].Text;
                combo_item_lang_2.Text = list_tracks.SelectedItems[0].SubItems[3].Text;

                //Track extension
                string ext_track = list_tracks.SelectedItems[0].SubItems[2].Text.ToLower();

                if (ext_track.Contains("h264")) txt_track_format.Text = "m4v";

                else if (ext_track.Contains("webm")) txt_track_format.Text = "webm";
                else if (ext_track.Contains("rawvideo")) txt_track_format.Text = "avi";
                else if (ext_track.Contains("divx")) txt_track_format.Text = "avi";
                else if (ext_track.Contains("xvid")) txt_track_format.Text = "avi";
                else if (ext_track.Contains("mov")) txt_track_format.Text = "mov";
                else if (ext_track.Contains("aac")) txt_track_format.Text = "aac";
                else if (ext_track.Contains("ac3")) txt_track_format.Text = "ac3";
                else if (ext_track.Contains("pcm")) txt_track_format.Text = "wav";
                else if (ext_track.Contains("flac")) txt_track_format.Text = "flac";
                else if (ext_track.Contains("truehd")) txt_track_format.Text = "thd";
                else if (ext_track.Contains("dts")) txt_track_format.Text = "dts";
                else if (ext_track.Contains("vp7")) txt_track_format.Text = "webm";
                else if (ext_track.Contains("vp8")) txt_track_format.Text = "webm";
                else if (ext_track.Contains("vp9")) txt_track_format.Text = "webm";
                else if (ext_track.Contains("wma")) txt_track_format.Text = "wma";
                else if (ext_track.Contains("wvc1")) txt_track_format.Text = "wmv";
                else if (ext_track.Contains("vorbis")) txt_track_format.Text = "ogg";
                else if (ext_track.Contains("theora")) txt_track_format.Text = "ogv";
                else if (ext_track.Contains("subrip")) txt_track_format.Text = "srt";
                else txt_track_format.Text = String.Empty;


                //End track extension

                if (list_tracks.SelectedItems[0].SubItems[5].Text == "Image for audio")
                {
                    pic_encode_param.Image = img_streams.Images[4];
                }
                else
                {
                    pic_encode_param.Image = img_streams.Images[list_tracks.SelectedItems[0].ImageIndex];
                }

            }
            else
            {
                txt_track_param.Text = String.Empty;
                combo_item_lang_2.Text = String.Empty;
                pic_encode_param.Image = null;
                txt_track_format.Text = String.Empty;
            }

            if (list_tracks.SelectedIndices.Count > 1) txt_track_format.Text = String.Empty;

        }

        private void btn_set_track_param_Click(object sender, EventArgs e)
        {
            ct3_encode_default.PerformClick();
        }

        private void txt_track_param_DoubleClick(object sender, EventArgs e)
        {

            if (list_tracks.SelectedIndices.Count > 0)
            {

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Subtitle"))

                {
                    list_tracks.SelectedItems[0].SubItems[5].Text = "copy";
                }

                else
                {
                    list_tracks.SelectedItems[0].SubItems[5].Text = "copy";
                }
            }
        }


        private void ct3_encode_default_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count > 0)
            {

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Subtitle"))

                {
                    if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("dvd_subtitle") || (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("hdmv_pgs_subtitle")))

                    {
                        list_tracks.SelectedItems[0].SubItems[5].Text = "copy";
                    }
                    else
                    {
                        def_mux_subs_enc = "copy";
                        list_tracks.SelectedItems[0].SubItems[5].Text = "copy"; ;
                    }

                }

                else
                {
                    list_tracks.SelectedItems[0].SubItems[5].Text = "copy";
                    if (list_tracks.SelectedItems[0].BackColor == Color.LightYellow)
                    {
                        list_tracks.SelectedItems[0].BackColor = Color.White;
                    }

                    def_mux_audio_enc = "copy";
                    def_mux_video_enc = "copy";
                }
                txt_track_param.Text = "copy";
            }
        }

        private void btn_mux_cancel_Click(object sender, EventArgs e)
        {
            if (working == false) return;
            cancel_queue = true;
            working = false;

            if (process_glob.StartInfo.Arguments != String.Empty)
            {
                StreamWriter write_q = process_glob.StandardInput;
                write_q.Write("q");
                return;

            }

            else
            {
                System.Threading.Thread.Sleep(250);
                Process[] localByName = Process.GetProcessesByName("ffmpeg");
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(500);

                Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process p2 in localByName2)
                {
                    p2.Kill();
                }
            }
        }

        private void txt_track_param_TextChanged(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count > 0)
            {

                if (txt_track_param.Text != String.Empty && list_tracks.SelectedItems[0].SubItems[5].Text != "Image for audio")
                {
                    list_tracks.SelectedItems[0].SubItems[5].Text = txt_track_param.Text;
                }
            }

            if (txt_track_param.Text.Contains("-c:") || txt_track_param.Text.Contains("acodec") || txt_track_param.Text.Contains("acodec") || txt_track_param.Text.Contains("vcodec"))
            {
                MessageBox.Show("Codec type arguments like " + '\u0022' + "-c:v" + '\u0022' + ", " + '\u0022' + "-c:a" + '\u0022' + ",-acodec or -vcodec are not required", "Parameter unncessary", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void combo_item_lang_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_tracks.Items.Count > 0)
            {
                foreach (ListViewItem item in list_tracks.Items)
                {
                    item.SubItems[3].Text = combo_item_lang_2.SelectedItem.ToString().Substring(combo_item_lang_2.SelectedItem.ToString().Length - 4, 3);
                }
            }
        }        

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;

        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {

            change_tab_1 = false;
            change_tab_2 = false;

            if (tabControl1.SelectedIndex == 1)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_1 = true;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_2 = true;
            }

            tabControl1.SelectedIndex = 0;

            string[] file_drop = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> files2 = new List<string>();

            int num_drop = 0;

            foreach (String dropped in file_drop)
            {

                if (File.Exists(dropped))
                {
                    files2.Add(dropped);
                    num_drop = files2.Count();
                }

                else
                {
                    if (Directory.Exists(dropped))
                    {
                        if (add_subfs == false)
                        {

                            foreach (String file in Directory.GetFiles(dropped))
                            {
                                if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden))
                                {
                                    files2.Add(file);
                                    num_drop = num_drop + 1;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (string f in Directory.GetFiles(dropped, "*.*", SearchOption.AllDirectories))
                                {
                                    if (!File.GetAttributes(f).HasFlag(FileAttributes.Hidden))
                                    {
                                        files2.Add(f);
                                        num_drop = num_drop + 1;
                                    }
                                }
                            }
                            catch (System.Exception excpt)
                            {
                                var a = MessageBox.Show("Error: " + excpt.Message, "Access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                    }
                }
            }


            if (num_drop >= 5000)
            {
                var a = MessageBox.Show("Adding " + num_drop + " files could take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel)
                {
                    return;
                }
            }

            files_to_add = files2;
            canceled_file_adding = false;
            btn_cancel_add.Enabled = true;
            btn_cancel_add.Visible = true;
            btn_cancel_add.Refresh();
            BG_Files.RunWorkerAsync();

        }

        private void listView3_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {

                if (e.Column == lvwColumnSorter3.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (lvwColumnSorter3.Order == SortOrder.Ascending)
                    {
                        lvwColumnSorter3.Order = SortOrder.Descending;
                    }
                    else
                    {
                        lvwColumnSorter3.Order = SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    lvwColumnSorter3.SortColumn = e.Column;
                    lvwColumnSorter3.Order = SortOrder.Ascending;
                }

                // Perform the sort with these new sort options.
                this.listView3.Sort();
            }
        }

        private void Combo_sub_lang_mux_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[1].Text.Contains("No subtitle for this file") == false)
                {
                    item.SubItems[2].Text = Combo_sub_lang_mux.SelectedItem.ToString().Substring(Combo_sub_lang_mux.SelectedItem.ToString().LastIndexOf("(") + 1, 3);
                }
            }
        }

        private void Combo_def_sub_mux_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[1].Text.Contains("No subtitle for this file") == false)
                {
                    item.SubItems[4].Text = Combo_def_sub_mux.SelectedItem.ToString();
                }
            }
        }

        private void ct3_default_enc_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count > 0)
            {
                if (list_tracks.SelectedItems[0].SubItems[5].Text.Contains("Image for audio"))
                {
                    return;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Subtitle"))

                {
                    def_mux_subs_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Audio"))

                {
                    def_mux_audio_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Video"))

                {
                    def_mux_video_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

            }
        }

        private void combo_def_und_lang_Click(object sender, EventArgs e)
        {

        }

        private void combo_def_und_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            def_lang_und_tracks = combo_def_und_lang.SelectedItem.ToString().Substring(combo_def_und_lang.SelectedItem.ToString().Length - 4, 3);

            ctm2.Close();
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = false;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txt_folder_subs.Text = folderBrowserDialog1.SelectedPath;
                txt_folder_subs.BackColor = textBox1.BackColor;
                listView3.Items.Clear();
                tabControl1.SelectedIndex = 0;
                tabControl1.SelectedIndex = 2;
            }


        }

        private void ctm4_Opening(object sender, CancelEventArgs e)
        {
            if (listView3.SelectedIndices.Count == 0)
            {
                e.Cancel = true;
                return;
            }
            if (listView3.SelectedItems[0].SubItems[1].Text == "No subtitle for this file, double-click to locate")
            {
                Combo_single_subs_lang.Enabled = false;
                ct4_conv.Enabled = false;
            }
            else
            {
                Combo_single_subs_lang.Enabled = true;
                ct4_conv.Enabled = true;
            }

        }

        private void ct4_browse_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk_1(object sender, CancelEventArgs e)
        {
            listView3.SelectedItems[0].SubItems[1].Text = openFileDialog2.FileName;
            listView3.SelectedItems[0].SubItems[2].Text = "und";

            var a = TextFileEncodingDetector.DetectTextFileEncoding(openFileDialog2.FileName);
            if (a != null)
            {
                String txt_enc = a.ToString().Replace("System.Text.", "");
                listView3.SelectedItems[0].SubItems[3].Text = txt_enc.Replace("Encoding", "");
            }
            else
            {
                listView3.SelectedItems[0].SubItems[3].Text = "Unknown";
            }

            listView3.SelectedItems[0].SubItems[4].Text = "Yes";
            listView3.SelectedItems[0].SubItems[5].Text = "Ready";
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (working == true)
            {
                listView3.SelectedIndices.Clear();
                return;
            }

            if (listView3.SelectedIndices.Count == 0)
            {
                Combo_sub_lang_mux.Text = "";
                Combo_def_sub_mux.Text = "";
            }
        }

        private void Combo_single_subs_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {
                foreach (ListViewItem item in listView3.SelectedItems)
                {
                    item.SubItems[2].Text = Combo_single_subs_lang.SelectedItem.ToString().Substring(Combo_single_subs_lang.SelectedItem.ToString().Length - 4, 3);
                }
                ctm4.Close();
            }
        }

        private void btn_abort_sub_mux_Click(object sender, EventArgs e)
        {
            if (working == false) return;
            cancel_queue = true;
            working = false;
            if (process_glob.StartInfo.Arguments != String.Empty)
            {
                StreamWriter write_q = process_glob.StandardInput;
                write_q.Write("q");
                return;

            }

            else
            {
                System.Threading.Thread.Sleep(250);
                Process[] localByName = Process.GetProcessesByName("ffmpeg");
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(500);

                Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process p2 in localByName2)
                {
                    p2.Kill();
                }
            }

        }

        private void btn_sub_mux_Click(object sender, EventArgs e)
        {
            cancel_queue = false;
            notifyIcon1.Visible = true;
            foreach (ListViewItem file in listView3.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[1].Text == "No subtitle for this file, double-click to locate")
                {
                    MessageBox.Show("Some items have no subtitle available", "Missing subtitles on some files", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (listView3.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "List empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            String is_overw = textBox8.Text + "\\" + Path.GetFileNameWithoutExtension(listView3.Items[0].Text) + "." + Combo_ext_sub_mux.SelectedItem.ToString();
            if (is_overw == listView3.Items[0].Text && chk_suffix.Checked == false)
            {
                MessageBox.Show("Overwriting is not supported. Change destination directory or enable " + '\u0022' + "Rename output" + '\u0022' + " checkbox.", "Overwriting not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Combo_ext_sub_mux.SelectedIndex == 1)
            {
                foreach (ListViewItem item in listView3.Items)
                {
                    if (item.SubItems[1].Text.ToLower().Contains("idx"))
                    {
                        MessageBox.Show("VobSub subtitles are not supported in mp4 container", "Container not supported", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }

            //Validate subs encoding
            Boolean non_eng = false;
            Boolean non_utf = false;

            foreach (ListViewItem item in listView3.Items)
            {
                if (item.SubItems[2].Text != "eng")
                {
                    non_eng = true;

                }
                if (item.SubItems[3].Text.Contains("UTF") == false && item.SubItems[3].Text.Contains("Unicode") == false)
                {
                    non_utf = true;
                }
            }

            if (non_utf == true && (Combo_sub_lang_mux.SelectedIndex != 0 || non_eng == true) && chk_burn.CheckState == CheckState.Unchecked)
            {
                var a = MessageBox.Show("Non-UTF subtitles found on the list. Non-english languages may show incorrect characters. Continue?", "Subtitles encoding warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (a == DialogResult.Cancel)
                {
                    return;
                }
            }

            if (chk_burn.CheckState == CheckState.Checked)
            {
                foreach (ListViewItem item in listView3.Items)
                {
                    if (item.SubItems[1].Text.Contains("[") || item.SubItems[1].Text.Contains("]") || ((item.Text.Contains("[") || item.Text.Contains("]")) && item.SubItems[1].Text.Contains("available")))
                    {
                        MessageBox.Show("Some subtitle file names contain conflicting characters with subtitles filter. Please ensure your subtitle file names do not contain characters like [ ]", "Subtitle name warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

            }

            //Validated list, start processing

            txt_remain.Text = "Time remaining:";
            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();

            time_n_tasks = 0;
            timer_tasks.Start();

            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;

            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView3.Items)
            {
                list_proc.Items.Add((ListViewItem)item.Clone());
            }

            Pg1.Maximum = list_proc.Items.Count;
            listView3.SelectedIndices.Clear();

            Double total_duration = 0;
            Double total_prog = 0;

            //Get total duration of files
            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            total_duration = Convert.ToInt32(label12.Text.Substring(0, 2)) * 3600;
            total_duration = total_duration + Convert.ToInt32(label12.Text.Substring(4, 2)) * 60;
            total_duration = total_duration + Convert.ToInt32(label12.Text.Substring(8, 2));

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";
            //End get total duration of files

            List<string> list_lines = new List<string>();
            String sub_mux_ext = Combo_ext_sub_mux.SelectedItem.ToString();
            String path_sub = txt_folder_subs.Text;

            String prev_subs_mp4 = String.Empty;
            Boolean mkv_selected = true;

            if (Combo_ext_sub_mux.SelectedIndex == 1)
            {
                prev_subs_mp4 = "-c:s mov_text ";
                mkv_selected = false;
            }
            else
            {
                prev_subs_mp4 = "-c:s copy ";
            }

            if (hard_sub == true)
            {
                prev_subs_mp4 = String.Empty;
            }

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                String remain_time = "0";

                //int list_index = 0;
                //foreach (ListViewItem file in list_proc.Items)
                for (int list_index = 0; list_index < list_proc.Items.Count; list_index++)
                {

                    String file = list_proc.Items[list_index].Text;
                    // Get specific tack list video duration 
                    Process probe = new Process();
                    probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                    probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + file + '\u0022';
                    probe.StartInfo.RedirectStandardOutput = true;
                    probe.StartInfo.UseShellExecute = false;
                    probe.StartInfo.CreateNoWindow = true;
                    probe.EnableRaisingEvents = true;
                    probe.Start();
                    
                    String duracion = probe.StandardOutput.ReadLine();

                    probe.WaitForExit();

                    if (duracion != null)
                    {

                        if (duracion.Length >= 7)
                        {
                            durat_n = TimeSpan.Parse(duracion).TotalSeconds;
                        }
                    }
                    else
                    {
                        durat_n = 0;
                    }

                    //End duration

                    //Get number of source file subtitle tracks
                    Process ff_str = new Process();
                    ff_str.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    ff_str.StartInfo.Arguments = " -i " + '\u0022' + file + '\u0022';
                    ff_str.StartInfo.RedirectStandardOutput = true;
                    ff_str.StartInfo.RedirectStandardError = true;
                    ff_str.StartInfo.UseShellExecute = false;
                    ff_str.StartInfo.CreateNoWindow = true;
                    ff_str.EnableRaisingEvents = true;
                    ff_str.Start();

                    String stream = "";
                    int source_subs = 0;
                    String is_default = String.Empty;

                    while (!ff_str.StandardError.EndOfStream)
                    {
                        stream = ff_str.StandardError.ReadLine();

                        if (stream.Contains("Stream #0:"))
                        {
                            if (stream.Contains("Subtitle"))
                            {
                                source_subs = source_subs + 1;
                                if (stream.Contains("mov_text") && mkv_selected == true)
                                {
                                    MessageBox.Show("Source file contains mov_text subtitles not supported by mkv container. Switch to mp4 container or remove mov_text subtitles from source files.", "MP4 subtitles not supported in MKV container", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    Enable_Controls();
                                    return;
                                }
                                else
                                {
                                    prev_subs_mp4 = "-c:s copy ";
                                }

                                if (list_proc.Items[list_index].SubItems[4].Text == "Yes")
                                {
                                    is_default = is_default + " -disposition:s:" + (source_subs + 1).ToString() + " 0";

                                }
                            }
                        }
                    }

                    //End get number of subtitle tracks

                    if (cancel_queue == true)
                    {
                        working = false;

                        //this.InvokeEx(f => f.Pg1.Value = 0);
                        //this.InvokeEx(f => f.pg_current.Value = 0);
                        this.InvokeEx(f => f.button2.Enabled = true);
                        Enable_Controls();
                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = file;
                    String destino = "";

                    if (textBox8.Text == "..\\FFBatch")
                    {
                        destino = file.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        destino = textBox8.Text;
                    }
                    String link_sub = list_proc.Items[list_index].SubItems[1].Text;

                    if (txt_folder_subs.Text == String.Empty)
                    {
                        if (list_proc.Items[list_index].SubItems[1].Text == "SRT subtitle available")
                        {
                            link_sub = file.Substring(0, file.LastIndexOf(".")) + ".srt";
                        }
                        if (list_proc.Items[list_index].SubItems[1].Text == "VobSub IDX subtitle available")
                        {
                            link_sub = file.Substring(0, file.LastIndexOf(".")) + ".idx";
                        }

                        if (list_proc.Items[list_index].SubItems[1].Text == "ASS subtitle available")
                        {
                            link_sub = file.Substring(0, file.LastIndexOf(".")) + ".ass";
                        }
                    }
                    else
                    {
                        if (list_proc.Items[list_index].SubItems[1].Text == "SRT subtitle available")
                        {
                            link_sub = path_sub + "\\" + Path.GetFileNameWithoutExtension(file) + ".srt";
                        }
                        if (list_proc.Items[list_index].SubItems[1].Text == "VobSub IDX subtitle available")
                        {
                            link_sub = path_sub + "\\" + Path.GetFileNameWithoutExtension(file) + ".idx";
                        }
                        if (list_proc.Items[list_index].SubItems[1].Text == "ASS subtitle available")
                        {
                            link_sub = path_sub + "\\" + Path.GetFileNameWithoutExtension(file) + ".ass";
                        }
                    }
                    if (list_proc.Items[list_index].SubItems[1].Text == "No subtitle for this file, double-click to locate")
                    {
                        link_sub = String.Empty;
                    }


                    if (list_proc.Items[list_index].SubItems[4].Text == "Yes")
                    {
                        is_default = is_default + " -disposition:s" + (source_subs + 1).ToString() + " default";
                    }

                    String sub_enc = "-c:s copy";

                    if (sub_mux_ext == "mp4")
                    {
                        sub_enc = "-c:s mov_text";
                    }
                    if (mkv_selected == true)
                    {
                        sub_enc = prev_subs_mp4;
                    }
                    add_suffix = "";
                    if (chk_suffix.Checked == true)
                    {
                        add_suffix = "_FFB";
                    }

                    String AppParam = "";
                    if (hard_sub == true)
                    {
                        link_sub = link_sub.Replace("\\", "\\\\\\\\");
                        link_sub = link_sub.Replace(":", "\\\\" + ":");

                        AppParam = " -i " + "" + '\u0022' + file + '\u0022' + " -vf subtitles=" + '\u0022' + link_sub + '\u0022' + " " + txt_hard_subs.Text + " -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + add_suffix + "." + sub_mux_ext + '\u0022';

                    }
                    else
                    {
                        AppParam = " -i " + "" + '\u0022' + file + '\u0022' + " -sub_charenc UTF-8 " + "-i " + '\u0022' + link_sub + '\u0022' + " -map 0 -c:v copy -c:a copy " + prev_subs_mp4 + "-map 1:0 " + sub_enc + " " + "-metadata:s:s:" + source_subs + " language=" + list_proc.Items[list_index].SubItems[2].Text + " " + is_default + " -y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + add_suffix + "." + sub_mux_ext + '\u0022';

                    }

                    if (!Directory.Exists(destino))
                    {
                        try
                        {
                            Directory.CreateDirectory(destino);
                        }
                        catch (System.Exception excpt)
                        {
                            MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = Cursors.Arrow;
                            Enable_Controls();
                            return;
                        }
                    }

                    process_glob.StartInfo.FileName = ffm;
                    process_glob.StartInfo.Arguments = AppParam;

                    valid_prog = false;
                    this.InvokeEx(f => f.listView3.Items[list_index].SubItems[5].Text = "Processing");
                    this.InvokeEx(f => f.textBox7.Text = "0%");
                    this.InvokeEx(f => f.textBox7.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    process_glob.StartInfo.RedirectStandardOutput = true;
                    process_glob.StartInfo.RedirectStandardError = true;
                    process_glob.StartInfo.RedirectStandardInput = true;
                    process_glob.StartInfo.UseShellExecute = false;
                    process_glob.StartInfo.CreateNoWindow = true;
                    process_glob.EnableRaisingEvents = true;

                    process_glob.Start();
                    if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                    valid_prog = true;

                    String err_txt = "";
                    Double interval = 0;

                    while (!process_glob.StandardError.EndOfStream)
                    {
                        err_txt = process_glob.StandardError.ReadLine();
                        list_lines.Add(err_txt);


                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog == true)
                            {
                                //this.InvokeEx(f => durat_n = TimeSpan.Parse(listView1.Items[list_index].SubItems[2].Text).TotalSeconds);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                Double percent_tot = (total_prog * 100 / total_duration);
                                int percent_tot_2 = Convert.ToInt32(percent_tot);

                                if (percent_tot_2 <= 100)
                                {
                                    this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                    this.InvokeEx(f => f.Pg1.Refresh());
                                    this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                    this.InvokeEx(f => f.textBox5.Refresh());
                                }
                                if (percent2 <= 100)
                                {
                                    this.InvokeEx(f => f.pg_current.Value = percent2);
                                    this.InvokeEx(f => f.pg_current.Refresh());
                                    this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                                    this.InvokeEx(f => f.textBox4.Refresh());

                                }
                                //Estimated remaining time

                                remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                                remain_time = remain_time.Replace("x", String.Empty);
                                Double timing1 = 0;

                                if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                                }
                                else
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time), 2);
                                }
                                Decimal timing = (decimal)timing1;
                                Decimal total_dur_dec = Convert.ToDecimal(total_duration);
                                Decimal total_prog_dec = Convert.ToDecimal(total_prog);

                                Decimal remain_secs = 0;
                                if (timing > 0)
                                {
                                    remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                                }

                                if (remain_secs > 60)
                                {
                                    remain_secs = remain_secs + 60;
                                }
                                String remain_from_secs = "";

                                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                                remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                                   t.Hours,
                                  t.Minutes);

                                if (remain_secs >= 3600)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                                }

                                if (remain_secs < 3600 && remain_secs >= 600)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                                }
                                if (remain_secs < 600 && remain_secs >= 120)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                                }

                                if (remain_secs <= 59)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                                }

                                //End remaining time
                            }
                        }
                        //Read output, get progress
                        this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                        this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                    }
                    process_glob.WaitForExit();
                    process_glob.StartInfo.Arguments = String.Empty;
                    this.InvokeEx(f => f.pg_current.Value = 100);
                    this.InvokeEx(f => f.textBox4.Text = "100%");
                    list_lines.Add("");
                    list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    list_lines.Add("");


                    //this.InvokeEx(f => f.Pg1.Value = Pg1.Value + 1);
                    //this.InvokeEx(f => f.Pg1.Refresh());

                    Boolean single_error = false;

                    if (process_glob.ExitCode == 0)
                    {
                        this.InvokeEx(f => f.listView3.Items[list_index].SubItems[5].Text = "Success");
                        this.InvokeEx(f => f.listView3.Items[list_index].EnsureVisible());
                        this.InvokeEx(f => f.listView3.Items[list_index].BackColor = Color.White);

                    }
                    else
                    {
                        if (list_proc.Items[list_index].SubItems[1].Text == "No subtitle for this file, double-click to locate")
                        {
                            this.InvokeEx(f => f.listView3.Items[list_index].SubItems[5].Text = "No Sub");
                        }
                        else
                        {
                            this.InvokeEx(f => f.listView3.Items[list_index].SubItems[5].Text = "Error");
                        }

                        this.InvokeEx(f => f.listView3.Items[list_index].BackColor = Color.PaleGoldenrod);
                        
                        if (listView3.Items.Count == 1)
                        {
                            single_error = true;
                            MessageBox.Show("Error muxing subtitles: " + Environment.NewLine + listBox4.Items[listBox4.Items.Count - 1].ToString(), "Error multiplexing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    //prog = (Pg1.Value * 100 / list_proc.Items.Count);


                    if (list_index == list_proc.Items.Count - 1)
                    {

                        this.InvokeEx(f => f.Pg1.Value = 100);
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        working = false;
                        //Save log
                        string[] array_err = list_lines.ToArray();
                        String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                        SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                        SaveFile.WriteLine("-------------------------------");
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


                        //End shutdown check
                        if (cancel_queue == false)
                        {
                            if (Form.ActiveForm == null)
                            {
                                notifyIcon1.BalloonTipText = "Queue processing completed";
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                notifyIcon1.ShowBalloonTip(0);
                            }
                            if (checkBox3.Checked)
                            {
                                if (Directory.GetFiles(destino).Length != 0 && single_error == false)
                                {
                                    Process open_processed = new Process();
                                    open_processed.StartInfo.FileName = "explorer.exe";
                                    open_processed.StartInfo.Arguments = '\u0022' + destino + '\u0022';
                                    open_processed.Start();
                                }
                                else
                                {
                                    if (Directory.Exists(destino) && Directory.GetFiles(destino).Length == 0)
                                    {
                                        System.IO.Directory.Delete(destino);
                                    }

                                }
                            }
                        }
                        else
                        {
                            textBox5.Text = "100%";
                            this.InvokeEx(f => MessageBox.Show("Queue processing aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));

                        }


                    }
                }
                Enable_Controls();

                if (sub_mux_ext == "mp4")
                {
                    this.InvokeEx(f => f.Combo_def_sub_mux.Enabled = false);
                }
                else
                {
                    this.InvokeEx(f => f.Combo_def_sub_mux.Enabled = true);
                }

            }).Start();

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = folderBrowserDialog1.SelectedPath;
                textBox8.BackColor = textBox1.BackColor;
            }
        }

        private void textBox8_DoubleClick(object sender, EventArgs e)
        {
            textBox8.Text = "..\\FFBatch";
            textBox8.BackColor = Control.DefaultBackColor;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            button5.PerformClick();
        }

        private void Combo_ext_sub_mux_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_ext_sub_mux.SelectedIndex == 1)
            {
                Combo_def_sub_mux.Enabled = false;
                Combo_def_sub_mux.SelectedIndex = 1;
                foreach (ListViewItem item in listView3.Items)
                {
                    if (item.SubItems[5].Text != "No Sub")
                    {
                        item.SubItems[4].Text = "No";
                    }
                }

            }
            else
            {
                Combo_def_sub_mux.Enabled = true;
                Combo_def_sub_mux.SelectedIndex = 0;
                foreach (ListViewItem item in listView3.Items)
                {
                    if (item.SubItems[5].Text != "No Sub")
                    {
                        item.SubItems[4].Text = "Yes";
                    }
                }

            }
        }

        private void combo_ext_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_ext.SelectedIndex == 1)
            {
                if (list_tracks.Items.Count != 0 && list_tracks.Items[0].BackColor == Color.LightYellow)
                {
                    return;
                }
                else
                {
                    if (select_mp4 == false)
                    {
                        MessageBox.Show("Some features may not work in mp4 format. Subtitle text files only supported encoding is mov_text", "MP4 container limitations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        select_mp4 = true;
                    }
                }
            }
        }

        private void listView3_DragDrop(object sender, DragEventArgs e)
        {
            change_tab_1 = false;
            change_tab_2 = false;

            if (tabControl1.SelectedIndex == 1)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_1 = true;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                tabControl1.SelectedIndex = 0;
                change_tab_2 = true;
            }

            tabControl1.SelectedIndex = 0;

            string[] file_drop = (string[])e.Data.GetData(DataFormats.FileDrop);

            List<string> files2 = new List<string>();

            int num_drop = 0;

            foreach (String dropped in file_drop)
            {

                if (File.Exists(dropped))
                {
                    files2.Add(dropped);
                    num_drop = files2.Count();
                }

                else
                {
                    if (Directory.Exists(dropped))
                    {
                        if (add_subfs == false)
                        {

                            foreach (String file in Directory.GetFiles(dropped))
                            {
                                if (!File.GetAttributes(file).HasFlag(FileAttributes.Hidden))
                                {
                                    files2.Add(file);
                                    num_drop = num_drop + 1;
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                foreach (string f in Directory.GetFiles(dropped, "*.*", SearchOption.AllDirectories))
                                {
                                    if (!File.GetAttributes(f).HasFlag(FileAttributes.Hidden))
                                    {
                                        files2.Add(f);
                                        num_drop = num_drop + 1;
                                    }
                                }
                            }
                            catch (System.Exception excpt)
                            {
                                var a = MessageBox.Show("Error: " + excpt.Message, "Access error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                        }
                    }
                }
            }


            if (num_drop >= 5000)
            {
                var a = MessageBox.Show("Adding " + num_drop + " files could take some time. Continue?", "Adding many files", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel)
                {
                    return;
                }
            }

            files_to_add = files2;
            canceled_file_adding = false;
            btn_cancel_add.Enabled = true;
            btn_cancel_add.Visible = true;
            btn_cancel_add.Refresh();
            BG_Files.RunWorkerAsync();
        }

        private void listView3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {
                if (listView3.SelectedItems[0].SubItems[1].Text == "No subtitle for this file, double-click to locate")
                {
                    ct4_browse.PerformClick();
                }
            }
        }

        private void ct4_del_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count > 0)
            {

                foreach (ListViewItem item in listView3.SelectedItems)
                {
                    listView3.Items.RemoveAt(item.Index);
                    foreach (ListViewItem item2 in listView1.Items)
                    {
                        if (item2.Text == item.Text)
                        {
                            listView1.Items.RemoveAt(item2.Index);
                        }
                    }
                    foreach (ListViewItem item3 in listView2.Items)
                    {
                        if (item3.Text == item.Text)
                        {
                            listView2.Items.RemoveAt(item3.Index);
                        }
                    }

                }
                label9.Text = "Items: " + listView1.Items.Count;


                Double new_dur = 0;
                Double current_dur = 0;

                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (listView1.Items[i].SubItems[2].Text != "N/A" && listView1.Items[i].SubItems[2].Text != "Pending")
                    {
                        current_dur = TimeSpan.Parse(listView1.Items[i].SubItems[2].Text).TotalSeconds;
                        new_dur = new_dur + current_dur;
                    }
                }
                TimeSpan t = TimeSpan.FromSeconds(new_dur);
                String dur = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                             (int)t.TotalHours,
                             t.Minutes,
                             t.Seconds,
                             t.Milliseconds);
                label12.Text = dur.Substring(0, 11);
                calc_list_size();
            }
        }

        private void listView3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                ct4_del.PerformClick();
            }
        }

        private void listView3_Click(object sender, EventArgs e)
        {
            if (working == true)
            {
                listView3.SelectedIndices.Clear();
                return;
            }
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (working == true)
            {
                if (multi_running == false)
                {
                    listView1.SelectedIndices.Clear();
                    return;
                }
            }
        }

        private void ct3_image_aud_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Video"))
            {
                list_tracks.SelectedItems[0].BackColor = Color.LightYellow;
                list_tracks.SelectedItems[0].SubItems[5].Text = "Image for audio";

                String ext_image = Path.GetExtension(list_tracks.SelectedItems[0].Text);

                if (ext_image != ".jpg" && ext_image != ".jpeg" && ext_image != ".png" && ext_image != ".gif" && ext_image != ".bmp" && ext_image != ".tiff" && ext_image != ".psd")
                {
                    if (Extract_img == false)
                    {
                        if (ss_time_input.Text == "0:00:00")
                        {
                            MessageBox.Show("The first frame will be used as base image. You can select a different time on main screen pre-input seeking.", "Image for audio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        Extract_img = true;
                    }

                }
                combo_ext.SelectedIndex = 1;
                pic_encode_param.Image = img_streams.Images[4];
            }
            else
            {
                MessageBox.Show("Selected track does not contain a usable image.", "Image for audio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
            this.BringToFront();
            this.Focus();
        }

        private void txt_pre_input_Click(object sender, EventArgs e)
        {
            txt_pre_input.BackColor = Color.LightYellow;
        }

        private void txt_pre_input_TextChanged(object sender, EventArgs e)
        {
            if (txt_pre_input.Text == "")
            {
                txt_pre_input.BackColor = Control.DefaultBackColor;
            }
            else
            {
                txt_pre_input.BackColor = Color.LightYellow;
            }
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.BackColor = Color.LightYellow;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count > 0)
            {
                if (list_tracks.SelectedItems[0].SubItems[5].Text.Contains("Image for audio"))
                {
                    return;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Subtitle"))

                {
                    def_mux_subs_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Audio"))

                {
                    def_mux_audio_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

                if (list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Video"))

                {
                    def_mux_video_enc = list_tracks.SelectedItems[0].SubItems[5].Text;
                }

            }
        }


        private void txt_pre_input_Leave(object sender, EventArgs e)
        {
            if (txt_pre_input.Text == "")
            {
                txt_pre_input.BackColor = Control.DefaultBackColor;
            }
            else
            {
                txt_pre_input.BackColor = Color.LightYellow;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            was_started.Text = button14.Text;
            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_ini.Text == "0:00:00.000" && txt_fin.Text == "0:00:00.000")
            {
                MessageBox.Show("Trim can not be zero", "Trim is zero", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_ini.BackColor == Color.Orange || txt_fin.BackColor == Color.Orange)
            {
                MessageBox.Show("Wrong time format. Check time fields", "Wrong time format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listView1.Items.Count > 1)
            {
                var a = MessageBox.Show("Do you wish to apply trimming to all files in queue list?", "Multiple trimmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.No)
                {
                    return;
                }

                avoid_overw();

                if (avoid_overwriting == true && textBox3.Text != "..\\FFBatch" && checkBox1.CheckState != CheckState.Checked)
                {
                    avoid_overwriting = false;
                    MessageBox.Show("Multiple folders in input files and a single output folder may lead to file overwriting. Please enable " + '\u0022' + "Recreate source path" + '\u0022' + " to avoid opossible overwritings, or double click on the output path textbox to set it to the default relative path", "Different input folders but single output folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }

            if (textBox2.Text == "")
            {
                var a = MessageBox.Show("Format field is empty. Source file extension will be used. Continue?", "Format field blank", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel) return;

            }

            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            listBox4.Items.Clear();
            Disable_Controls();
            txt_remain.Text = "Time remaining:";

            String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");

            Pg1.Value = 0;
            Pg1.Maximum = listView1.Items.Count;


            foreach (ListViewItem file in listView1.Items)
            {

                if (TimeSpan.Parse(file.SubItems[2].Text).TotalSeconds < (TimeSpan.Parse(txt_fin.Text).TotalSeconds - TimeSpan.Parse(txt_ini.Text).TotalSeconds))
                {
                    MessageBox.Show("Trimming exceeds file duration", "Trimming outside boundaries", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (Control p in groupBox1.Controls)
                    {
                        p.Enabled = true;
                    }
                    
                    Enable_Controls();
                    return;
                }

                if (TimeSpan.Parse(file.SubItems[2].Text).TotalSeconds == (TimeSpan.Parse(txt_fin.Text).TotalSeconds - TimeSpan.Parse(txt_ini.Text).TotalSeconds))
                {
                    MessageBox.Show("Trimming lenght is 0", "Trimming is 0", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (Control p in groupBox1.Controls)
                    {
                        p.Enabled = true;
                    }
                    

                    Enable_Controls();
                    return;
                }

                if (TimeSpan.Parse(file.SubItems[2].Text).TotalSeconds < TimeSpan.Parse(txt_fin.Text).TotalSeconds)
                {
                    MessageBox.Show("Finish time exceeds file duration", "Trimming exceeds duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (Control p in groupBox1.Controls)
                    {
                        p.Enabled = true;
                    }
                    

                    Enable_Controls();
                    return;
                }

                if (TimeSpan.Parse(file.SubItems[2].Text).TotalSeconds < TimeSpan.Parse(txt_ini.Text).TotalSeconds)
                {
                    MessageBox.Show("Start time exceeds file duration", "Trimming exceeds duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (Control p in groupBox1.Controls)
                    {
                        p.Enabled = true;
                    }
                    
                    Enable_Controls();
                    return;
                }

            }

            //Validated list, start processing
            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }
            //Try preset
            String sel_test = listView1.SelectedItems[0].Text;
            this.Cursor = Cursors.WaitCursor;

            String file_prueba = "";

            file_prueba = sel_test;
            String fichero = Path.GetFileName(file_prueba);

            String destino_test = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            if (!Directory.Exists(destino_test))
            {
                try
                {
                    Directory.CreateDirectory(destino_test);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    return;
                }
            }


            Process consola_pre = new Process();
            consola_pre.StartInfo.FileName = "ffmpeg.exe";
            consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " " + "-ss 0 -t 1 " + " -y " + textBox1.Text + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text + '\u0022';
            consola_pre.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            consola_pre.Start();
            if (mem_prio.SelectedIndex != 2) Change_mem_prio();
            consola_pre.WaitForExit();


            if (consola_pre.ExitCode != 0)
            {
                Enable_Controls();
                this.Cursor = Cursors.Arrow;
                MessageBox.Show("FFmpeg command failed on first file. Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }

                return;
            }
            else
            {

                this.Cursor = Cursors.Arrow;

            }

            //END try preset

            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;
            notifyIcon1.Visible = true;

            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView1.Items)
            {
                list_proc.Items.Add(item.Text);
                item.BackColor = Color.White;

                item.SubItems[4].Text = "Queued";

            }

            Pg1.Maximum = list_proc.Items.Count;
            listView1.SelectedIndices.Clear();

            Double dura2 = 0;
            time_n_tasks = 0;
            timer_tasks.Start();

            List<string> list_lines = new List<string>();
            //End new pre-thread code
            process_glob.StartInfo.Arguments = String.Empty;
            //Begin new thread code for trimming
            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                String remain_time = "";

                //foreach (ListViewItem file in list_proc.Items)
                for (int list_index = 0; list_index < listView1.Items.Count; list_index++)
                {
                    String file = list_proc.Items[list_index].Text;
                    if (cancel_queue == true)
                    {
                        working = false;

                        this.InvokeEx(f => f.Pg1.Value = 0);
                        this.InvokeEx(f => f.pg_current.Value = 0);
                        this.InvokeEx(f => f.button2.Enabled = true);
                        Enable_Controls();
                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    int prog = (Pg1.Value * 100 / list_proc.Items.Count);
                    this.InvokeEx(f => f.textBox5.Text = (prog.ToString() + "%"));
                    this.InvokeEx(f => f.textBox5.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());


                    this.InvokeEx(f => dura2 = TimeSpan.Parse(listView1.Items[list_index].SubItems[2].Text).TotalSeconds);
                    //Decimal current_dur = Convert.ToDecimal(TimeSpan.Parse(txt_fin.Text).TotalSeconds);

                    String ffm2 = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = file;

                    //Begin Shifting
                    String shifting = "";
                    if (chk_shift.Checked == true)
                    {
                        if (Num_Shift.Value >= 0)
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 0:v -map 1:a ";
                        }
                        else
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 1:v -map 0:a ";
                        }
                    }

                    //End Shifting

                    //Change Volume
                    String change_vol = "";
                    if (chk_vol.Checked == true)
                    {
                        change_vol = "-filter:a " + '\u0022' + "volume=" + vol_ch.Value.ToString() + "dB " + '\u0022' + " ";
                    }
                    //End change volume


                    String destino = "";
                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino = file.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            String pre_dest = Path.GetDirectoryName(file);
                            destino = Path.Combine(textBox3.Text, pre_dest.Substring(3, pre_dest.Length - 3));
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }
                    }


                    add_suffix = "";
                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                    }

                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(file);
                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }

                    String AppParam = " -i " + "" + '\u0022' + file + '\u0022' + shifting + " " + " -ss " + txt_ini.Text + " -to " + txt_fin.Text + " -y " + textBox1.Text + " " + change_vol + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + add_suffix + ext_output1 + '\u0022';

                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }
                    process_glob.StartInfo.FileName = ffm2;
                    process_glob.StartInfo.Arguments = AppParam;
                    process_glob.StartInfo.RedirectStandardOutput = true;
                    process_glob.StartInfo.RedirectStandardInput = true;
                    process_glob.StartInfo.RedirectStandardError = true;
                    process_glob.StartInfo.UseShellExecute = false;
                    process_glob.StartInfo.CreateNoWindow = true;
                    process_glob.EnableRaisingEvents = true;

                    valid_prog = false;
                    this.InvokeEx(f => f.listView1.Items[Pg1.Value].SubItems[4].Text = "Processing");
                    this.InvokeEx(f => f.textBox7.Text = "0%");
                    this.InvokeEx(f => f.textBox7.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    process_glob.Start();

                    this.InvokeEx(f => validate_duration = listView1.Items[list_index].SubItems[2].Text);
                    if (validate_duration != "N/A" && validate_duration != "0:00:00" && validate_duration != "00:00:00" && validate_duration != "Pending")
                    {
                        valid_prog = true;
                    }
                    String err_txt = "";

                    while (!process_glob.StandardError.EndOfStream)
                    {
                        err_txt = process_glob.StandardError.ReadLine();
                        list_lines.Add(err_txt);


                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog == true)
                            {
                                this.InvokeEx(f => durat_n = TimeSpan.Parse(listView1.Items[list_index].SubItems[2].Text).TotalSeconds);
                                durat_n = durat_n - Convert.ToDouble(TimeSpan.Parse(txt_fin.Text).TotalSeconds - Convert.ToDouble(TimeSpan.Parse(txt_ini.Text).TotalSeconds));
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n);
                                int percent2 = Convert.ToInt32(percent);
                                if (percent2 <= 100)
                                {
                                    this.InvokeEx(f => f.pg_current.Value = percent2);
                                    this.InvokeEx(f => f.pg_current.Refresh());
                                    this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                                    this.InvokeEx(f => f.textBox4.Refresh());
                                }
                                //Estimated remaining time

                                remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                                remain_time = remain_time.Replace("x", String.Empty);
                                Double timing1 = 0;

                                if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                                }
                                else
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time), 2);
                                }
                                Decimal timing = (decimal)timing1;
                                Decimal total_dur_dec = Convert.ToDecimal(durat_n);
                                Decimal total_prog_dec = Convert.ToDecimal(sec_prog);
                                Decimal remain_secs = 0;
                                if (timing > 0)
                                {
                                    remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                                }

                                if (remain_secs > 60)
                                {
                                    remain_secs = remain_secs + 60;
                                }
                                String remain_from_secs = "";

                                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                                remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                                   t.Hours,
                                  t.Minutes);

                                if (remain_secs >= 3600)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                                }

                                if (remain_secs < 3600 && remain_secs >= 600)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                                }
                                if (remain_secs < 600 && remain_secs >= 120)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                                }

                                if (remain_secs <= 59)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                                }
                                if (remain_secs <= 0)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "About to finish");
                                }

                                //End remaining time
                            }

                        }
                        //Read output, get progress
                        this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                        this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                    }
                    process_glob.WaitForExit();

                    this.InvokeEx(f => f.pg_current.Value = 100);
                    this.InvokeEx(f => f.textBox4.Text = "100%");
                    list_lines.Add("");
                    list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    list_lines.Add("");

                    this.InvokeEx(f => f.Pg1.Value = Pg1.Value + 1);
                    this.InvokeEx(f => f.Pg1.Refresh());

                    process_glob.StartInfo.Arguments = String.Empty;

                    if (process_glob.ExitCode == 0)
                    {
                        this.InvokeEx(f => f.listView1.Items[Pg1.Value - 1].SubItems[4].Text = "Success");
                        this.InvokeEx(f => f.listView1.Items[Pg1.Value - 1].EnsureVisible());

                    }
                    else
                    {
                        this.InvokeEx(f => f.listView1.Items[Pg1.Value - 1].SubItems[4].Text = "Error");
                        this.InvokeEx(f => f.listView1.Items[Pg1.Value - 1].BackColor = Color.PaleGoldenrod);

                    }

                    prog = (Pg1.Value * 100 / list_proc.Items.Count);

                    this.InvokeEx(f => f.textBox5.Text = (prog.ToString() + "%"));

                    if (Pg1.Value == Pg1.Maximum)
                    {
                        working = false;
                        //Save log
                        string[] array_err = list_lines.ToArray();
                        String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                        SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                        SaveFile.WriteLine("-------------------------------");
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

                        //Automatic shutdown check
                        if (chkshut.Checked && cancel_queue == false)
                        {

                            Disable_Controls();
                            this.InvokeEx(f => f.chkshut.Enabled = false);
                            this.InvokeEx(f => f.btn_pause.Enabled = false);
                            this.InvokeEx(f => f.TopMost = true);

                            this.InvokeEx(f => f.Timer_apaga.Start());
                            this.InvokeEx(f => f.TB1.Enabled = true);
                            this.InvokeEx(f => f.TB1.Visible = true);
                            this.InvokeEx(f => f.button10.Enabled = true);
                            this.InvokeEx(f => f.button10.Visible = true);
                            this.InvokeEx(f => f.button20.Enabled = false);
                            this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                            notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                            String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                            foreach (string file_s in Directory.GetFiles(destino_test))
                            {
                                File.Delete(file_s);
                            }
                            System.IO.Directory.Delete(destino_test);
                            return;
                        }
                        //End shutdown check
                        else
                        {
                            if (cancel_queue == false)
                            {

                                if (Form.ActiveForm == null)
                                {
                                    notifyIcon1.BalloonTipText = "Queue trimming completed";
                                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                    notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                    notifyIcon1.ShowBalloonTip(1000);
                                }
                                if (checkBox3.Checked)
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
                            else
                            {
                                this.InvokeEx(f => MessageBox.Show("Queue processing aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            }
                        }

                    }
                }
                Enable_Controls();

                String borrar = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                foreach (string file in Directory.GetFiles(destino_test))
                {
                    File.Delete(file);
                }
                System.IO.Directory.Delete(destino_test);
            }).Start();
        }

        private void timer_tasks_Tick(object sender, EventArgs e)
        {
            time_n_tasks = time_n_tasks + 1;
            if (total_time == true)
            {
                TimeSpan t = TimeSpan.FromSeconds(time_n_tasks);
                String tx_elapsed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);
                txt_remain.Text = "Time elapsed: " + tx_elapsed;
            }
        }

        private void ss_time_input_TextChanged(object sender, EventArgs e)
        {
            if (ss_time_input.Text == "0:00:00.000")
            {
                ss_time_input.BackColor = groupBox1.BackColor;
                return;

            }

            DateTime time;
            if (!DateTime.TryParse(ss_time_input.Text, out time))
            {
                ss_time_input.BackColor = Color.Orange;
            }

            else
            {
                ss_time_input.BackColor = Color.White;
            }

            if (ss_time_input.Text.Length > 12)
            {
                MessageBox.Show("Wrong format. Please use a 00:00:00.000 time format", "Time format error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void ss_time_input_Leave(object sender, EventArgs e)
        {
            if (ss_time_input.Text == "0:00:00.000")
            {
                ss_time_input.BackColor = Control.DefaultBackColor;
                return;

            }

            DateTime time2;
            if (!DateTime.TryParse(ss_time_input.Text, out time2))
            {
                ss_time_input.BackColor = Color.Orange;
            }
            else
            {
                ss_time_input.BackColor = Color.White;
            }
        }

        private void ss_time_input_DoubleClick(object sender, EventArgs e)
        {
            ss_time_input.Text = "0:00:00.000";
        }

        private void btn_add_tracks_Click(object sender, EventArgs e)
        {

            if (listView2.Items.Count == 1)
            {
                listView2.Items[0].Selected = true;

            }
            ct2_all.PerformClick();

            tracks_background();

            if (listView2.Items.Count > 1 && listView2.SelectedIndices.Count == 0)
            {
                MessageBox.Show("No file selected to add available streams", "No file selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void calc_list_size()

        {
            tot_size = 0;
            foreach (ListViewItem file in listView1.Items)
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(file.Text);
                tot_size = tot_size + fileInfo.Length;
            }

            var bytes = tot_size;
            var kilobytes = (double)bytes / 1024;
            var megabytes = kilobytes / 1024;
            var gigabytes = megabytes / 1024;

            //Format size view

            //Get separator
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
                    this.InvokeEx(f => f.lbl_size.Text = gigas + " GB");

                }
                else
                {
                    this.InvokeEx(f => f.lbl_size.Text = gigas + " GB");
                }

            }

            if (bytes >= 1048576 && bytes < 1000000000)
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
                    this.InvokeEx(f => f.lbl_size.Text = megas + " MB");

                }
                else
                {
                    this.InvokeEx(f => f.lbl_size.Text = megas + " MB");
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
                    this.InvokeEx(f => f.lbl_size.Text = kbs + " KB");

                }
                else
                {
                    this.InvokeEx(f => f.lbl_size.Text = kbs + " KB");
                }

            }
            if (bytes > -1 && bytes < 1024)
            {
                String bits = bytes.ToString();
                this.InvokeEx(f => f.lbl_size.Text = bits + " Bytes");
            }

            //End Format size view

        }

        private void calc_list_dur()

        {
            //Show total duration
            Double Total_dur = 0;

            foreach (ListViewItem item in listView1.Items)
                if (item.SubItems[2].Text != "0:00:00" && item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "Pending" && item.SubItems[2].Text != "00:00:00")
                {
                    Total_dur = Total_dur + TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds;
                }


            TimeSpan t = TimeSpan.FromSeconds(Total_dur);
            String dur = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                     (int)t.TotalHours,
                     t.Minutes,
                     t.Seconds,
                     t.Milliseconds);
            label12.Text = dur.Substring(0, 11);

            //End show total duration
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void btn_capture_Click(object sender, EventArgs e)
        {
            cancel_queue = false;
            notifyIcon1.Visible = true;

            //Try preset

            this.Cursor = Cursors.WaitCursor;

            String destino_test = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "FFBatch_Test");
            if (!Directory.Exists(destino_test))
            {
                Directory.CreateDirectory(destino_test);
            }
            Process consola_pre = new Process();
            consola_pre.StartInfo.FileName = "ffmpeg.exe";
            consola_pre.StartInfo.Arguments = " -f gdigrab " + txt_pre_input.Text + " -i desktop" + " -t 00:00:00.250 " + " -y " + textBox1.Text + " " + '\u0022' + destino_test + "\\" + "Rec_Screen_01" + "." + textBox2.Text + '\u0022';
            consola_pre.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            consola_pre.Start();
            consola_pre.WaitForExit();


            if (consola_pre.ExitCode != 0)
            {
                this.Cursor = Cursors.Arrow;


                MessageBox.Show("FFmpeg capture command failed. Try with a screen capture preset and check pre-input and input parameters", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;
                System.IO.DirectoryInfo di = new DirectoryInfo(destino_test);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
                return;
            }
            else
            {

                this.Cursor = Cursors.Arrow;

            }

            System.IO.DirectoryInfo di2 = new DirectoryInfo(destino_test);

            foreach (FileInfo file in di2.GetFiles())
            {
                file.Delete();
            }

            if (Directory.GetFiles(destino_test).Length == 0)
            {
                System.IO.Directory.Delete(destino_test);
            }

            //END try preset

            //Recording countdown

            listBox4.Items.Clear();
            listBox4.Items.Add("Screen capture will start in 3 seconds");
            listBox4.Refresh();
            System.Threading.Thread.Sleep(1000);
            listBox4.Items.Clear();
            listBox4.Items.Add("Screen capture will start in 2 seconds");
            listBox4.Refresh();
            System.Threading.Thread.Sleep(1000);
            listBox4.Items.Clear();
            listBox4.Items.Add("Screen capture will start in 1 second");
            listBox4.Refresh();
            System.Threading.Thread.Sleep(1000);
            listBox4.Items.Clear();
            listBox4.Items.Add("Screen recording started");
            listBox4.Refresh();
            timer_tasks.Start();
            //End recording countdown

            //Validated list, start processing
            recording_scr = true;
            txt_remain.Text = "Time remaining:";

            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;

            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView1.Items)
            {
                list_proc.Items.Add(item.Text);
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";

            }

            Pg1.Maximum = list_proc.Items.Count;
            listView1.SelectedIndices.Clear();


            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */
                //Process process = new System.Diagnostics.Process();

                String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");

                String destino = "";

                if (textBox3.Text == "..\\FFBatch")
                {
                    destino = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "FFBatch");

                }
                else
                {
                    destino = textBox3.Text;
                }

                String pre_input_var = "";
                if (txt_pre_input.Text != "")
                {
                    pre_input_var = txt_pre_input.Text;
                }

                String AppParam = " -f gdigrab " + pre_input_var + " -i desktop" + " -y " + textBox1.Text + " " + '\u0022' + destino + "\\" + "Rec_Screen_" + capture_count.ToString() + "." + textBox2.Text + '\u0022';
                capture_count = capture_count + 1;
                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                }

                process_glob.StartInfo.FileName = ffm;
                process_glob.StartInfo.Arguments = AppParam;

                valid_prog = false;

                this.InvokeEx(f => f.textBox7.Text = "0%");
                this.InvokeEx(f => f.textBox7.Refresh());
                this.InvokeEx(f => f.pg_current.Value = 0);
                this.InvokeEx(f => f.pg_current.Refresh());

                process_glob.StartInfo.RedirectStandardOutput = false;
                process_glob.StartInfo.RedirectStandardError = false;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;


                process_glob.Start();
                capture_handle = process_glob.Id;
                //proc_main_handle = process.MainWindowHandle;
                process_glob.WaitForExit();

                recording_scr = false;
                Enable_Controls();
                working = false;


                if (process_glob.ExitCode == 1)
                {
                    MessageBox.Show("Invalid parameters for screen capturing.", "Invalid capturing paremeters", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (process_glob.ExitCode != -1073741510 && process_glob.ExitCode != 1 && process_glob.ExitCode != 0)
                {

                    MessageBox.Show("Screen capturing terminated unexpectedly. File may be truncated.", "Capturing terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.InvokeEx(f => f.listBox4.Items.Clear());
                this.InvokeEx(f => f.listBox4.Items.Add("Recording finished"));
                this.InvokeEx(f => f.listBox4.Refresh());
                //this.InvokeEx(f => f.listBox4.Items.Add(process.ExitCode.ToString()));
                timer_tasks.Stop();
                TimeSpan t = TimeSpan.FromSeconds(time_n_tasks);
                String tx_elapsed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);
                this.InvokeEx(f => f.txt_remain.Text = "Total time:  " + tx_elapsed);

                if (checkBox3.Checked)
                {

                    if (Directory.GetFiles(destino).Length != 0 && process_glob.ExitCode == 0)
                    {
                        Process open_processed = new Process();
                        open_processed.StartInfo.FileName = "explorer.exe";
                        open_processed.StartInfo.Arguments = '\u0022' + destino + '\u0022';
                        open_processed.Start();
                    }

                }

            }).Start();
        }

        private void chk_burn_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_burn.CheckState == CheckState.Checked)
            {
                hard_sub = true;
                txt_hard_subs.Enabled = true;
                Enable_txt_hard_Subs = true;
                Combo_def_sub_mux.Enabled = false;
                Combo_sub_lang_mux.Enabled = false;
            }
            else
            {
                hard_sub = false;
                txt_hard_subs.Enabled = false;
                Enable_txt_hard_Subs = false;
                Combo_def_sub_mux.Enabled = true;
                Combo_sub_lang_mux.Enabled = true;
            }
        }

        private void txt_fin_TextChanged(object sender, EventArgs e)
        {
            DateTime time;
            if (!DateTime.TryParse(txt_fin.Text, out time))
            {
                txt_fin.BackColor = Color.Orange;
            }

            else
            {
                txt_fin.BackColor = Color.White;
            }

            if (txt_fin.Text.Length > 12)
            {
                MessageBox.Show("Wrong format. Please use a 00:00:00.000 time format", "Time format error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_fin_DoubleClick_1(object sender, EventArgs e)
        {

            if (listView1.SelectedIndices.Count > 0)
            {
                txt_fin.Text = listView1.SelectedItems[0].SubItems[2].Text;
            }
            else
            {
                txt_fin.Text = "0:00:00.000";
            }
        }

        private void txt_ini_TextChanged(object sender, EventArgs e)
        {

            DateTime time;
            if (!DateTime.TryParse(txt_ini.Text, out time))
            {
                txt_ini.BackColor = Color.Orange;
            }

            else
            {
                txt_ini.BackColor = Color.White;
            }

            if (txt_ini.Text.Length > 12)
            {
                MessageBox.Show("Wrong format. Please use a 00:00:00.000 time format", "Time format error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txt_ini_Leave(object sender, EventArgs e)
        {

            if (txt_ini.Text == "0:00:00.000")
            {
                txt_ini.BackColor = groupBox1.BackColor;
                return;

            }

            DateTime time2;
            if (!DateTime.TryParse(txt_ini.Text, out time2))
            {
                txt_ini.BackColor = Color.Orange;
            }
            else
            {
                txt_ini.BackColor = Color.White;
            }
        }

        private void txt_ini_DoubleClick(object sender, EventArgs e)
        {
            txt_ini.Text = "0:00:00.000";
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            var a = MessageBox.Show("All users created presets will be deleted. Do you want to reset to factory presets and settings?", "Confirm reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (a == DialogResult.No)
            {
                return;
            }

            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            

            button4.PerformClick();
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            folderBrowserDialog1.Description = "Select path to save a copy of configuration file (ff_batch.ini)";


            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                folderBrowserDialog1.Description = "";
                return;
            }

            folderBrowserDialog1.Description = "";

            if (File.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini"))
            {
                File.Copy(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini", folderBrowserDialog1.SelectedPath + "\\" + "ff_batch.ini", true);
                MessageBox.Show("Configuration was saved successfully", "Configuration restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("An error ocurred saving configuration file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button28_Click(object sender, EventArgs e)
        {
            openFileDialog3.Filter = "Configuration file|ff_batch.ini";

            if (openFileDialog3.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            File.Copy(openFileDialog3.FileName, System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini", true);
            button4.PerformClick();
            MessageBox.Show("Configuration was restored successfully", "Configuration restored", MessageBoxButtons.OK, MessageBoxIcon.Information);



            //else
            //{
            //    MessageBox.Show("An error ocurred saving configuration file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            // }
        }

        private void txt_fin_Leave(object sender, EventArgs e)
        {
            if (txt_fin.Text == "0:00:00.000")
            {
                txt_fin.BackColor = groupBox1.BackColor;
                return;

            }

            DateTime time2;
            if (!DateTime.TryParse(txt_fin.Text, out time2))
            {
                txt_fin.BackColor = Color.Orange;
            }
            else
            {
                txt_fin.BackColor = Color.White;
            }
        }

        private void cti5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                String fullPath = listView1.SelectedItems[0].Text;
                if (File.Exists(fullPath))
                {
                    if (listView1.SelectedItems[0].SubItems[2].Text != "Pending" && listView1.SelectedItems[0].SubItems[2].Text != "N/A")
                    {
                        txt_fin.Text = listView1.SelectedItems[0].SubItems[2].Text;
                        txt_fin.BackColor = Color.LightYellow;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("File was not found", "File missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void ct4_conv_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems[0].SubItems[1].Text == "VobSub IDX subtitle available")
            {
                MessageBox.Show("VobSub format conversion is not required", "Conversion not required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            String dest;
            String sub_ext;
            String source_sub;

            if (listView3.SelectedItems[0].SubItems[1].Text == "SRT subtitle available")
            {
                sub_ext = listView3.SelectedItems[0].SubItems[1].Text.Substring(0, 3).ToLower();
                source_sub = Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].Text), Path.GetFileNameWithoutExtension(listView3.SelectedItems[0].Text) + "." + sub_ext);
                dest = Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].Text), Path.GetFileNameWithoutExtension(listView3.SelectedItems[0].Text) + "_utf8." + sub_ext);
            }
            else if (listView3.SelectedItems[0].SubItems[1].Text == "ASS subtitle available")
            {
                sub_ext = listView3.SelectedItems[0].SubItems[1].Text.Substring(0, 3).ToLower();
                source_sub = Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].Text), Path.GetFileNameWithoutExtension(listView3.SelectedItems[0].Text) + "." + sub_ext);
                dest = Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].Text), Path.GetFileNameWithoutExtension(listView3.SelectedItems[0].Text) + "_utf8." + sub_ext);
            }
            else
            {
                sub_ext = listView3.SelectedItems[0].SubItems[1].Text;
                source_sub = listView3.SelectedItems[0].SubItems[1].Text;
                dest = Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].SubItems[1].Text), Path.GetFileNameWithoutExtension(listView3.SelectedItems[0].SubItems[1].Text) + "_utf8" + Path.GetExtension(listView3.SelectedItems[0].SubItems[1].Text));

            }

            String f1 = File.ReadAllText(source_sub, Encoding.Default);

            File.WriteAllText(Path.Combine(Path.GetDirectoryName(listView3.SelectedItems[0].Text), dest), f1, Encoding.UTF8);
            listView3.SelectedItems[0].SubItems[1].Text = dest;

            var a = TextFileEncodingDetector.DetectTextFileEncoding(dest);
            if (a != null)
            {
                String txt_enc = a.ToString().Replace("System.Text.", "");
                listView3.SelectedItems[0].SubItems[3].Text = txt_enc.Replace("Encoding", "");
            }
            else
            {
                listView3.SelectedItems[0].SubItems[3].Text = "Unknown";
            }

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 3)
            {
                btn_validate_url.PerformClick();
                return;
            }

            if (listView1.Items.Count == 0)
            {
                return;
            }

            Refresh_lists();
            calc_list_size();
            calc_list_dur();
            label9.Text = "Items: " + listView1.Items.Count;

            if (tabControl1.SelectedIndex == 0)
            {
                checkBox3.Visible = true;
                combo_ext.Visible = false;
                label19.Visible = false;
                btn_set_mux_def.Visible = false;

                groupBox15.Visible = false;
                label17.Visible = false;
                combo_item_lang_2.Visible = false;

                button12.Visible = true;

                button18.Visible = true;

                label13.Visible = true;
                textBox3.Visible = true;
                button21.Visible = true;
                chkshut.Visible = true;
                btn_mux.Visible = false;
                btn_mux_cancel.Visible = false;
                button23.Visible = false;
                pic_encode_param.Visible = false;
                lbl_mux_par.Visible = false;
                txt_track_param.Visible = false;
                btn_set_track_param.Visible = false;
                groupBox1.Visible = true;
                panel1.Visible = true;
                list_tracks.Visible = false;

                return;
            }
            if (tabControl1.SelectedIndex == 1)
            {
                add_to_tab_2();
            }

            if (tabControl1.SelectedIndex == 2)
            {

                btn_set_mux_def.Visible = false;
                btn_set_track_param.Visible = false;

                groupBox15.Visible = true;

                chkshut.Checked = false;
                chkshut.Visible = false;
                TB1.Visible = false;
                txt_hard_subs.Text = textBox1.Text;
                //Check listview1 vs listview2
                int list_int = 0;
                if (listView1.Items.Count == listView3.Items.Count)
                {

                    foreach (ListViewItem item in listView1.Items)
                    {
                        if (listView3.Items[list_int].Text == item.Text)
                        {
                            list_int = list_int + 1;

                        }
                    }
                }
                if (list_int == listView1.Items.Count)
                {
                    return;
                }

                else
                {
                    listView3.Items.Clear();
                }

                listView3.SmallImageList = listView1.SmallImageList;
                foreach (ListViewItem item in listView1.Items)
                {

                    this.Cursor = Cursors.WaitCursor;
                    ListViewItem elemento = new ListViewItem(item.Text, 1);
                    //Begin get file icon
                    Icon iconForFile = SystemIcons.WinLogo;

                    // Check to see if the image collection contains an image
                    // for this extension, using the extension as a key.
                    if (!imageList2.Images.ContainsKey(System.IO.Path.GetExtension(item.Text)))
                    {
                        // If not, add the image to the image list.
                        iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(item.Text);
                        imageList3.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                        imageList2.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                    }

                    elemento.ImageKey = System.IO.Path.GetExtension(item.Text);
                    //End get file icon

                    listView3.Items.Add(elemento);

                }

                foreach (ListViewItem item in listView3.Items)
                {
                    String is_srt = item.Text.Substring(item.Text.LastIndexOf("."));

                    if (is_srt == ".srt")
                    {
                        listView3.Items.RemoveAt(item.Index);
                    }

                    String is_Vobsub = item.Text.Substring(item.Text.LastIndexOf("."));

                    if (is_Vobsub == ".idx" || is_Vobsub == ".sub")
                    {
                        listView3.Items.RemoveAt(item.Index);
                    }

                    String is_ass = item.Text.Substring(item.Text.LastIndexOf("."));

                    if (is_srt == ".ass")
                    {
                        listView3.Items.RemoveAt(item.Index);
                    }

                    String subs_path = String.Empty;
                    String Sub_File_SRT = String.Empty;
                    String Sub_File_Vobsub = String.Empty;
                    String Sub_File_ASS = String.Empty;

                    if (txt_folder_subs.Text == String.Empty)
                    {
                        Sub_File_SRT = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".srt";
                        Sub_File_Vobsub = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".idx";
                        Sub_File_ASS = item.Text.Substring(0, item.Text.LastIndexOf(".")) + ".ass";
                    }
                    else
                    {
                        String path_sub = txt_folder_subs.Text;
                        Path.GetFileName(item.Text);
                        Sub_File_SRT = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".srt";
                        Sub_File_Vobsub = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".idx";
                        Sub_File_ASS = path_sub + "\\" + Path.GetFileName(item.Text).Substring(0, Path.GetFileName(item.Text).LastIndexOf(".")) + ".ass";
                    }

                    if (item.SubItems[0].Text.Contains("No subtitle for this file") == false)
                    {


                        if (File.Exists(Sub_File_SRT))
                        {
                            item.SubItems.Add("SRT subtitle available");
                            item.SubItems.Add("und");

                            var a = TextFileEncodingDetector.DetectTextFileEncoding(Sub_File_SRT);
                            if (a != null)
                            {
                                String txt_enc = a.ToString().Replace("System.Text.", "");
                                item.SubItems.Add(txt_enc.Replace("Encoding", ""));
                            }
                            else
                            {
                                item.SubItems.Add("Unknown");
                            }
                            item.SubItems.Add("Yes");
                            item.SubItems.Add("Ready");
                        }

                        if (File.Exists(Sub_File_Vobsub))
                        {
                            item.SubItems.Add("VobSub IDX subtitle available");
                            item.SubItems.Add("und");

                            item.SubItems.Add("-");
                            item.SubItems.Add("Yes");
                            item.SubItems.Add("Ready");
                        }
                        if (File.Exists(Sub_File_ASS))
                        {
                            item.SubItems.Add("ASS subtitle available");
                            item.SubItems.Add("und");

                            var a = TextFileEncodingDetector.DetectTextFileEncoding(Sub_File_ASS);
                            if (a != null)
                            {
                                String txt_enc = a.ToString().Replace("System.Text.", "");
                                item.SubItems.Add(txt_enc.Replace("Encoding", ""));
                            }
                            else
                            {
                                item.SubItems.Add("Unknown");
                            }
                            item.SubItems.Add("Yes");
                            item.SubItems.Add("Ready");

                        }
                    }

                    if (!File.Exists(Sub_File_Vobsub) && !File.Exists(Sub_File_SRT) && !File.Exists(Sub_File_ASS))
                    {
                        item.SubItems.Add("No subtitle for this file, double-click to locate");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("-");
                        item.SubItems.Add("No Sub");
                    }

                }
                Combo_sub_lang_mux.Text = "";
                this.Cursor = Cursors.Arrow;
            }

            if (tabControl1.SelectedIndex == 3)
            {
                btn_validate_url.PerformClick();
            }
        }

        private void Refresh_lists()
        {
            if (tabControl1.SelectedIndex == 0)
            {
                listView1.BeginUpdate();
                foreach (ListViewItem item in listView1.Items)
                {
                    if (!File.Exists(item.Text))
                    {
                        item.Remove();
                    }
                }
                listView1.EndUpdate();
                return;
            }

            if (tabControl1.SelectedIndex == 1)
            {
                listView2.BeginUpdate();
                foreach (ListViewItem item in listView2.Items)
                {
                    if (!File.Exists(item.Text))
                    {
                        item.Remove();
                    }
                }
                listView2.EndUpdate();
                return;
            }
            if (tabControl1.SelectedIndex == 2)
            {
                listView3.BeginUpdate();
                foreach (ListViewItem item in listView3.Items)
                {
                    if (!File.Exists(item.Text))
                    {
                        item.Remove();
                    }
                }
                listView3.EndUpdate();
                return;
            }
        }

        private void chk_subfolders_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_subfolders.Checked == true)
            {
                add_subfs = true;
            }
            else
            {
                add_subfs = false;
            }

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.BackColor = Control.DefaultBackColor;
            }
            else
            {
                textBox2.BackColor = Color.LightYellow;
            }
        }

        private void chk_suffix_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_suffix.CheckState == CheckState.Checked)
            {
                txt_suffix.Enabled = true;
            }
            else
            {
                txt_suffix.Enabled = false;
            }
        }

        private void txt_suffix_TextChanged(object sender, EventArgs e)
        {
            if (txt_suffix.Text.Contains("/") || txt_suffix.Text.Contains(":") || txt_suffix.Text.Contains("*") || txt_suffix.Text.Contains("?") || txt_suffix.Text.Contains("¿") || txt_suffix.Text.Contains('\u0022') || txt_suffix.Text.Contains("<") || txt_suffix.Text.Contains(">") || txt_suffix.Text.Contains("|") || txt_suffix.Text.Contains("\\"))
            {
                MessageBox.Show("Invalid characters detected (\\/:*?'\u0022'<>|", "Invalid characters", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txt_suffix.Text = txt_suffix.Text.Replace("/", "");
                txt_suffix.Text = txt_suffix.Text.Replace(":", "");
                txt_suffix.Text = txt_suffix.Text.Replace("*", "");
                txt_suffix.Text = txt_suffix.Text.Replace("?", "");
                txt_suffix.Text = txt_suffix.Text.Replace("¿", "");
                txt_suffix.Text = txt_suffix.Text.Replace("\u0022", "");
                txt_suffix.Text = txt_suffix.Text.Replace("<", "");
                txt_suffix.Text = txt_suffix.Text.Replace(">", "");
                txt_suffix.Text = txt_suffix.Text.Replace("|", "");
                txt_suffix.Text = txt_suffix.Text.Replace("\\", "");
            }

        }

        private void txt_suffix_Leave(object sender, EventArgs e)
        {
            if (txt_suffix.Text == String.Empty)
            {
                txt_suffix.Text = "_FFB";
            }
        }

        private void btn_cancel_add_Click(object sender, EventArgs e)
        {
            canceled_add = true;
            canceled_file_adding = true;
            btn_cancel_add.Visible = false;
            dur_ok = false;
            groupBox5.Focus();
        }

        private void BG_Dur_DoWork(object sender, DoWorkEventArgs e)
        {
            dur_ok = false;
            canceled_add = false;
            this.InvokeEx(f => f.listView1.BeginUpdate());
            this.InvokeEx(f => f.Disable_Controls());
            this.InvokeEx(f => f.button20.Enabled = false);
            this.InvokeEx(f => f.groupBox5.Focus());
            this.InvokeEx(f => f.btn_cancel_add.Enabled = true);
            this.InvokeEx(f => f.btn_cancel_add.Visible = true);
            this.InvokeEx(f => f.btn_cancel_add.Refresh());
            this.InvokeEx(f => f.LB_Wait.Visible = true);
            this.InvokeEx(f => f.LB_Wait.Text = "Parsing unknown files");
            this.InvokeEx(f => f.LB_Wait.Refresh());
            this.InvokeEx(f => f.pg_adding.Visible = true);
            this.InvokeEx(f => f.pg_adding.Value = 0);
            this.InvokeEx(f => f.txt_adding_p.Visible = true);
            this.InvokeEx(f => f.txt_adding_p.Refresh());

            this.InvokeEx(f => f.pg_adding.Maximum = list_pending_dur.Items.Count);

            Process probe = new Process();

            for (int i = 0; i < list_pending_dur.Items.Count; i++)
            {

                this.InvokeEx(f => f.pg_adding.Value = pg_adding.Value + 1);
                this.InvokeEx(f => f.txt_adding_p.Text = (pg_adding.Value * 100 / list_pending_dur.Items.Count + "%"));
                this.InvokeEx(f => f.txt_adding_p.Refresh());

                if (canceled_add == false)
                {
                    if (list_pending_dur.Items[i].SubItems[2].Text == "Pending")
                    {

                        probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                        probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + list_pending_dur.Items[i].Text + '\u0022';
                        probe.StartInfo.RedirectStandardOutput = true;
                        probe.StartInfo.UseShellExecute = false;
                        probe.StartInfo.CreateNoWindow = true;
                        probe.EnableRaisingEvents = true;
                        probe.Start();

                        String duracion = probe.StandardOutput.ReadLine();
                        probe.WaitForExit();

                        if (duracion != null)
                        {
                            if (duracion.Length >= 7)
                            {
                                this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = duracion.Substring(0, 7));

                                if (duracion.Substring(0, 7) == "0:00:00")
                                {
                                    this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                                }
                            }

                            else
                            {
                                this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = "N/A");
                                this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                            }
                        }
                        else
                        {
                            this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = "N/A");
                            this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                        }

                    }
                }
            }

            if (canceled_add == false)
            {
                dur_ok = true;
            }
            else
            {
                dur_ok = false;
            }

            this.InvokeEx(f => f.label9.Text = "Items: " + listView1.Items.Count);
            this.InvokeEx(f => f.LB_Wait.Text = "");
            this.InvokeEx(f => f.txt_adding_p.Visible = false);
            this.InvokeEx(f => f.listView1.EndUpdate());
            this.InvokeEx(f => f.Enable_Controls());
            this.InvokeEx(f => f.chkshut.Enabled = true);
            this.InvokeEx(f => f.btn_pause.Enabled = true);
            this.InvokeEx(f => f.btn_cancel_add.Visible = false);
            this.InvokeEx(f => f.txt_adding_p.Text = "");
            this.InvokeEx(f => f.txt_adding_p.Visible = false);
            this.InvokeEx(f => f.label9.Visible = true);
            this.InvokeEx(f => f.label12.Visible = true);
            this.InvokeEx(f => f.lbl_size.Visible = true);
            this.InvokeEx(f => f.pg_adding.Visible = false);
            this.InvokeEx(f => f.LB_Wait.Visible = false);
            this.InvokeEx(f => f.tabControl1.Enabled = true);

        }

        private void BG_Dur_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (canceled_add == true)
            {
                MessageBox.Show("File list duration parsing needs to be complete", "Files parsing incomplete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (tabControl1.SelectedIndex == 0)
                {

                    if (was_started.Text == button2.Text)
                    {
                        button2.PerformClick();
                    }
                    if (was_started.Text == button17.Text)
                    {
                        button17.PerformClick();
                    }
                    if (was_started.Text == button15.Text)
                    {
                        button15.PerformClick();
                    }
                    if (was_started.Text == button14.Text)
                    {
                        button14.PerformClick();
                    }

                }
                if (tabControl1.SelectedIndex == 1)
                {
                    btn_mux.PerformClick();
                }
                if (tabControl1.SelectedIndex == 2)
                {
                    btn_sub_mux.PerformClick();
                }
            }
        }


        private void BG_Files_DoWork(object sender, DoWorkEventArgs e)
        {
            //Adding files/folders thread

            this.InvokeEx(f => f.listBox4.Items.Clear());
            this.InvokeEx(f => f.listView1.SmallImageList = imageList2);
            this.InvokeEx(f => f.listView1.LargeImageList = imageList3);

            Disable_Controls();
            this.InvokeEx(f => f.groupBox5.Focus());
            this.InvokeEx(f => f.button20.Enabled = false);
            this.InvokeEx(f => f.btn_abort_sub_mux.Enabled = false);
            this.InvokeEx(f => f.btn_mux_cancel.Enabled = false);
            this.InvokeEx(f => f.chkshut.Enabled = false);
            this.InvokeEx(f => f.btn_pause.Enabled = false);
            this.InvokeEx(f => f.tabControl1.Enabled = false);
            this.InvokeEx(f => f.LB_Wait.Visible = true);
            this.InvokeEx(f => f.btn_cancel_add.Enabled = true);


            Type ts = Type.GetTypeFromProgID("Shell.Application");
            dynamic shell = Activator.CreateInstance(ts);
            //Shell shell = new Shell();

            //Es carpeta

            int i = 0;
            pending_dur = 0;

            this.InvokeEx(f => f.pg_adding.Value = 0);
            this.InvokeEx(f => f.pg_adding.Maximum = files_to_add.Count);
            this.InvokeEx(f => f.pg_adding.Visible = true);
            this.InvokeEx(f => f.txt_adding_p.Visible = true);
            this.InvokeEx(f => f.txt_adding_p.Refresh());
            this.InvokeEx(f => f.label9.Visible = false);
            this.InvokeEx(f => f.label9.Refresh());
            this.InvokeEx(f => f.label12.Visible = false);
            this.InvokeEx(f => f.label12.Refresh());
            this.InvokeEx(f => f.lbl_size.Visible = false);
            this.InvokeEx(f => f.lbl_size.Refresh());

            this.InvokeEx(f => f.LB_Wait.Text = "Adding selected " + files_to_add.Count + " files");
            this.InvokeEx(f => f.LB_Wait.Refresh());


            this.InvokeEx(f => f.listView1.BeginUpdate());

            ListViewItem[] itemsToAdd = new ListViewItem[files_to_add.Count];

            for (int n = 0; n < files_to_add.Count; n++)
            {
                if (canceled_file_adding == false)
                {
                    i = i + 1;

                    this.InvokeEx(f => f.pg_adding.Value = i);
                    this.InvokeEx(f => f.txt_adding_p.Text = (i * 100 / files_to_add.Count).ToString() + "%");

                    //ListViewItem elemento = new ListViewItem(file2, 1);

                    Icon iconForFile = SystemIcons.WinLogo;

                    if (!imageList2.Images.ContainsKey(System.IO.Path.GetExtension(files_to_add[n])))
                    {
                        iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(files_to_add[n]);
                        this.InvokeEx(f => f.imageList3.Images.Add(System.IO.Path.GetExtension(files_to_add[n]), iconForFile));
                        this.InvokeEx(f => f.imageList2.Images.Add(System.IO.Path.GetExtension(files_to_add[n]), iconForFile));
                    }

                    itemsToAdd[n] = new ListViewItem(files_to_add[n]);
                    itemsToAdd[n].ImageKey = System.IO.Path.GetExtension(files_to_add[n]);

                    //Testing

                    Folder rFolder = shell.NameSpace(Path.GetDirectoryName(files_to_add[n]));
                    FolderItem rFiles = rFolder.ParseName(System.IO.Path.GetFileName(files_to_add[n]));
                    String videostype = rFolder.GetDetailsOf(rFiles, 2).Trim();
                    String videosLength = rFolder.GetDetailsOf(rFiles, 27).Trim();
                    String videoSize = rFolder.GetDetailsOf(rFiles, 1).Trim();
                    itemsToAdd[n].SubItems.Add(videostype);
                    Boolean no_av = false;
                    DateTime time;

                    if (Path.GetExtension(files_to_add[n]).ToLower() == ".jpg" || Path.GetExtension(files_to_add[n]).ToLower() == ".files_to_add[n]" || Path.GetExtension(files_to_add[n]).ToLower() == ".gif" || Path.GetExtension(files_to_add[n]).ToLower() == ".bmp" || Path.GetExtension(files_to_add[n]).ToLower() == ".tif" || Path.GetExtension(files_to_add[n]).ToLower() == ".psd" || Path.GetExtension(files_to_add[n]).ToLower() == ".txt" || Path.GetExtension(files_to_add[n]).ToLower() == ".ini" || Path.GetExtension(files_to_add[n]).ToLower() == ".zip" || Path.GetExtension(files_to_add[n]).ToLower() == ".htm" || Path.GetExtension(files_to_add[n]).ToLower() == ".html" || Path.GetExtension(files_to_add[n]).ToLower() == ".rar" || Path.GetExtension(files_to_add[n]).ToLower() == ".doc" || Path.GetExtension(files_to_add[n]).ToLower() == ".docx" || Path.GetExtension(files_to_add[n]).ToLower() == ".xlsx")
                    {
                        itemsToAdd[n].SubItems.Add("00:00:00");
                        itemsToAdd[n].BackColor = Color.LightGoldenrodYellow;
                        no_av = true;
                    }

                    else if ((!DateTime.TryParse(videosLength, out time) && no_av == false))
                    {
                        itemsToAdd[n].SubItems.Add("Pending");
                        pending_dur = pending_dur + 1;
                    }

                    else
                    {
                        itemsToAdd[n].SubItems.Add(videosLength);
                    }

                    //End testing

                    itemsToAdd[n].SubItems.Add(videoSize);
                    itemsToAdd[n].SubItems.Add("Queued");

                    //this.InvokeEx(f => f.listView1.Items.Add(elemento));
                }

            }

            if (canceled_file_adding == true)
            {
                this.InvokeEx(f => f.pg_adding.Value = 0);
                this.InvokeEx(f => f.pg_adding.Maximum = i);
                this.InvokeEx(f => f.LB_Wait.Text = "Canceling, please wait...");
                ListViewItem[] itemsToAdd2 = new ListViewItem[i];
                for (int n2 = 0; n2 < i; n2++)
                {
                    this.InvokeEx(f => f.pg_adding.Value = n2);
                    itemsToAdd2[n2] = itemsToAdd[n2];

                }
                this.InvokeEx(f => f.listView1.Items.AddRange(itemsToAdd2));
                //this.InvokeEx(f => f.listView1.Items.Clear());
            }
            else
            {
                this.InvokeEx(f => f.listView1.Items.AddRange(itemsToAdd.ToArray()));
            }

        }

        private void BG_Files_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            calc_list_size();
            calc_total_dur();


            //Remove duplicates
            LB_Wait.Text = "Removing duplicates";
            LB_Wait.Refresh();
            pg_adding.Value = 0;

            var tags = new HashSet<string>();
            var duplicates = new List<ListViewItem>();

            txt_adding_p.Text = "";

            foreach (ListViewItem item in listView1.Items)
            {
                // HashSet.Add() returns false if it already contains the key.
                if (!tags.Add(item.Text))
                    duplicates.Add(item);

            }
            if (duplicates.Count != 0)
            {
                MessageBox.Show("Duplicated files were added and they will be removed", "Duplicated files found", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            pg_adding.Maximum = duplicates.Count();

            foreach (ListViewItem item in duplicates)
            {
                item.Remove();
                pg_adding.Value = pg_adding.Value + 1;
                txt_adding_p.Text = (pg_adding.Value * 100 / duplicates.Count).ToString() + "%";
                txt_adding_p.Refresh();
            }
            //End remove duplicates 

            calc_total_dur();
            calc_list_size();

            groupBox5.Focus();
            btn_cancel_add.Enabled = true;
            btn_cancel_add.Visible = true;
            btn_cancel_add.Refresh();

            if (canceled_file_adding == false)
            {
                canceled_add = false;
                if (change_tab_1 == true)
                {
                    change_tab_1 = false;
                    tabControl1.SelectedIndex = 0;
                    tabControl1.SelectedIndex = 1;
                }
            }

            else
            {
                canceled_add = true;

                if (change_tab_1 == true)
                {
                    change_tab_1 = false;
                    tabControl1.SelectedIndex = 0;
                }
            }

            list_global_proc.Items.Clear();
            foreach (ListViewItem item in listView1.Items)
            {
                list_global_proc.Items.Add((ListViewItem)item.Clone());
            }


            BG_P_Dur.RunWorkerAsync();            
            listView1.EndUpdate();
        }

        private void change_tab()
        {

            if (change_tab_1 == true)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.SelectedIndex = 1;

            }
            if (change_tab_2 == true)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.SelectedIndex = 2;
            }
        }

        private void calc_total_dur()
        {
            Double Total_dur = 0;
            foreach (ListViewItem item in listView1.Items)

                if (item.SubItems[2].Text != "0:00:00" && item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "00:00:00" && item.SubItems[2].Text != "Pending")
                {

                    try {
                        Total_dur = Total_dur + TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds;
                    }
                    catch (System.Exception)
                    {

                        item.SubItems[2].Text = "N/A";
                        item.BackColor = Color.LightGoldenrodYellow;
                    }
                }

            TimeSpan t = TimeSpan.FromSeconds(Total_dur);
            String dur = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                     (int)t.TotalHours,
                     t.Minutes,
                     t.Seconds,
                     t.Milliseconds);
            label12.Text = dur.Substring(0, 11);
        }

        private void BG_P_Dur_DoWork(object sender, DoWorkEventArgs e)
        {

            if (list_not_empty == true)
            {
                pending_dur = 0;
                foreach (ListViewItem item in list_global_proc.Items)
                    if (item.SubItems[2].Text == "Pending")
                    {
                        pending_dur = pending_dur + 1;
                    }
            }

            this.InvokeEx(f => f.pg_adding.Value = 0);
            this.InvokeEx(f => f.pg_adding.Maximum = pending_dur);
            this.InvokeEx(f => f.LB_Wait.Text = "Parsing " + pending_dur + " unknown files");
            this.InvokeEx(f => f.LB_Wait.Refresh());

            this.InvokeEx(f => f.pg_adding.Maximum = pending_dur);
            Process probe = new Process();
            this.InvokeEx(f => f.listView1.BeginUpdate());

            for (int i = 0; i < list_global_proc.Items.Count; i++)
            {
                if (canceled_add == false)
                {
                    if (list_global_proc.Items[i].SubItems[2].Text == "Pending")
                    {

                        this.InvokeEx(f => f.pg_adding.Value = pg_adding.Value + 1);
                        this.InvokeEx(f => f.txt_adding_p.Text = (pg_adding.Value * 100 / pending_dur + "%"));
                        this.InvokeEx(f => f.txt_adding_p.Refresh());

                        probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                        probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + list_global_proc.Items[i].Text + '\u0022';
                        probe.StartInfo.RedirectStandardOutput = true;
                        probe.StartInfo.UseShellExecute = false;
                        probe.StartInfo.CreateNoWindow = true;
                        probe.EnableRaisingEvents = true;
                        probe.Start();

                        String duracion = probe.StandardOutput.ReadLine();
                        probe.WaitForExit();

                        if (duracion != null)
                        {
                            if (duracion.Length >= 7)
                            {
                                this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = duracion.Substring(0, 7));

                                if (duracion.Substring(0, 7) == "0:00:00")
                                {
                                    this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                                }
                            }

                            else
                            {
                                this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = "N/A");
                                this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                            }
                        }
                        else
                        {
                            this.InvokeEx(f => f.listView1.Items[i].SubItems[2].Text = "N/A");
                            this.InvokeEx(f => f.listView1.Items[i].BackColor = Color.LightGoldenrodYellow);
                        }

                    }
                }

            }
            if (canceled_add == false)
            {
                dur_ok = true;
            }

            else
            {
                dur_ok = false;
            }

            this.InvokeEx(f => f.label9.Text = "Items: " + list_global_proc.Items.Count);
            this.InvokeEx(f => f.LB_Wait.Text = "");
            this.InvokeEx(f => f.txt_adding_p.Visible = false);

            this.InvokeEx(f => f.label12.Refresh());

            this.InvokeEx(f => f.listView1.EndUpdate());
            this.InvokeEx(f => f.Enable_Controls());
            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: ");
            this.InvokeEx(f => f.chkshut.Enabled = true);
            this.InvokeEx(f => f.btn_pause.Enabled = true);
            this.InvokeEx(f => f.btn_cancel_add.Visible = false);
            this.InvokeEx(f => f.txt_adding_p.Text = "");
            this.InvokeEx(f => f.txt_adding_p.Visible = false);
            this.InvokeEx(f => f.label9.Visible = true);
            this.InvokeEx(f => f.label12.Visible = true);
            this.InvokeEx(f => f.lbl_size.Visible = true);
            this.InvokeEx(f => f.pg_adding.Visible = false);
            this.InvokeEx(f => f.LB_Wait.Visible = false);
            this.InvokeEx(f => f.btn_mux_cancel.Enabled = true);
            this.InvokeEx(f => f.tabControl1.Enabled = true);
            this.InvokeEx(f => f.listBox4.Items.Clear());
            this.InvokeEx(f => f.listBox4.Items.Add(""));
            this.InvokeEx(f => f.listBox4.Items.Add(""));
            this.InvokeEx(f => f.listBox4.Items.Add("                                                                                      FFmpeg standard output"));
            this.InvokeEx(f => f.listView1.EndUpdate());

        }

        private void BG_P_Dur_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            calc_total_dur();
            this.InvokeEx(f => f.listBox4.Items.Clear());
            this.InvokeEx(f => f.listBox4.Items.Add(""));
            this.InvokeEx(f => f.listBox4.Items.Add(""));
            listBox4.Items.Add("                                                                                           FFmpeg standard output");


            if (change_tab_1 == true)
            {

                if (listView1.Items.Count > 200)
                {
                    var a = MessageBox.Show("Obtaining streams for " + listView1.Items.Count + " files can take a long time. Continue?", "Many files to be parsed", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (a == DialogResult.Cancel)
                    {
                        tabControl1.SelectedIndex = 0;
                        return;

                    }

                }
                add_to_tab_2();
            }

            if (change_tab_2 == true)
            {
                tabControl1.SelectedIndex = 0;
                tabControl1.SelectedIndex = 2;
            }
        }

        private void add_to_tab_2()
        {

            listView2.Clear();
            listView2.BeginUpdate();
            listView2.Columns.Add("      File", 450);
            listView2.SmallImageList = listView1.SmallImageList;
            foreach (ListViewItem item in listView1.Items)
            {

                this.Cursor = Cursors.WaitCursor;
                ListViewItem elemento = new ListViewItem(item.Text, 1);
                //Begin get file icon
                Icon iconForFile = SystemIcons.WinLogo;

                // Check to see if the image collection contains an image
                // for this extension, using the extension as a key.
                if (!imageList2.Images.ContainsKey(System.IO.Path.GetExtension(item.Text)))
                {
                    // If not, add the image to the image list.
                    iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(item.Text);
                    imageList3.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                    imageList2.Images.Add(System.IO.Path.GetExtension(item.Text), iconForFile);
                }

                elemento.ImageKey = System.IO.Path.GetExtension(item.Text);
                //End get file icon

                listView2.Items.Add(elemento);
            }

            foreach (ListViewItem item in listView2.Items)
            {
                Process ff_str = new Process();
                ff_str.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                ff_str.StartInfo.Arguments = " -i " + '\u0022' + item.Text + '\u0022';
                ff_str.StartInfo.RedirectStandardOutput = true;
                ff_str.StartInfo.RedirectStandardError = true;
                ff_str.StartInfo.UseShellExecute = false;
                ff_str.StartInfo.CreateNoWindow = true;
                ff_str.EnableRaisingEvents = true;
                ff_str.Start();
                String stream = "";
                String sub_str = "";
                int c = 0;
                Boolean has_stream = false;
                while (!ff_str.StandardError.EndOfStream)
                {
                    stream = ff_str.StandardError.ReadLine();

                    if (stream.Contains("Stream #0:"))
                    {
                        has_stream = true;
                        c = c + 1;
                        if (listView2.Columns.Count <= c)
                        {
                            listView2.Columns.Add("Stream " + c, 150);
                        }

                        if (stream.Substring(stream.IndexOf("#0:") + 4, 1) == "(")
                        {
                            if (stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(und)" || stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(unk)")
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 11);
                                item.SubItems.Add(stream.Substring((stream.LastIndexOf("#0:") + 11), (stream.Length - sub_str.Length)));
                            }
                            else
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                                item.SubItems.Add(stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length)));
                            }
                        }
                        else
                        {
                            if (stream.Contains("Video"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                item.SubItems.Add(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)));

                            }
                            if (stream.Contains("Audio"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                item.SubItems.Add(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)));
                            }
                            if (stream.Contains("Subtitle"))
                            {
                                sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                item.SubItems.Add(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)));
                            }

                        }

                    }

                }

                ff_str.WaitForExit();
                if (has_stream == false)
                {
                    item.SubItems.Add("No usable streams found");
                }
                
            }
            listView2.EndUpdate();
            this.Cursor = Cursors.Arrow;

            if (listView2.Items.Count == 1)            
                {
                    listView2.Items[0].Selected = true;
                    ct2_all.PerformClick();
                    tracks_background();
                }            
        }

        private void avoid_overw()
        {
            avoid_overwriting = false;
            if (listView1.Items.Count < 2) return;
            int i = 0;
            foreach (ListViewItem item in listView1.Items)
            {

                if (Path.GetDirectoryName(item.Text) != Path.GetDirectoryName(listView1.Items[0].Text))
                {
                    avoid_overwriting = true;
                    return;
                }
                i = i + 1;
            }
        }

        private void dg1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dg1.RowCount == 1 && dg1.Rows[0].Cells[0].Value == null)
            {
                dg1.Rows.Clear();
                return;
            }

            if (dg1.Rows[0].Cells[0].Value == null)
            {
                dg1.Rows.RemoveAt(0);
                return;
            }

            if (dg1.CurrentCell.ColumnIndex == 0)
            {

                dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Validating URL";

                if (dg1.CurrentCell.Value == null)
                {
                    ct_del_m3u.PerformClick();
                }

                if (dg1.CurrentCell.Value == null)
                {
                    dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "";
                    dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                    return;
                }

                if (dg1.CurrentCell.Value.ToString().Contains("http") == false)
                {
                    dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Error";
                    dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    return;
                }

                dg1.Refresh();

                Process probe = new Process();
                probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 -timeout 10000000" + " -i " + '\u0022' + dg1.Rows[dg1.CurrentCell.RowIndex].Cells[0].Value.ToString() + '\u0022';

                probe.StartInfo.RedirectStandardOutput = true;
                probe.StartInfo.UseShellExecute = false;
                probe.StartInfo.CreateNoWindow = true;
                probe.EnableRaisingEvents = true;

                probe.Start();

                String duracion = probe.StandardOutput.ReadLine();

                probe.WaitForExit();

                if (duracion == null)
                {
                    dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Error";
                    dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    return;
                }

                if (duracion != null)
                {
                    if (duracion.Length >= 7)
                    {
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[1].Value = duracion.Substring(0, 7);
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[2].Value = duracion.Substring(0, 7);
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Ready";
                        dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;

                        if (duracion.Substring(0, 7) == "0:00:00")
                        {
                            dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                            dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Ready";
                        }
                    }

                    if (duracion == "N/A")
                    {
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[1].Value = "N/A";
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[2].Value = "\u221E";
                        dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                        dg1.Rows[dg1.CurrentCell.RowIndex].Cells[4].Value = "Ready";
                    }
                }

            }

            if (dg1.CurrentCell.ColumnIndex == 2)
            {

                if (dg1.CurrentCell.Value == null)
                {
                    return;
                }

                DateTime time1;
                if (!DateTime.TryParse(dg1.CurrentCell.Value.ToString(), out time1))
                {

                    MessageBox.Show("Invalid time format", "Invalid time", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    dg1.Rows[dg1.CurrentCell.RowIndex].Cells[2].Value = "00:00:00";

                }
                else
                {
                    if (TimeSpan.Parse(dg1.CurrentCell.Value.ToString()).TotalSeconds > 0 && dg1.CurrentCell.Value.ToString() != "00:00:00")
                    {
                        dg1.Rows[dg1.CurrentCell.RowIndex].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                    }

                }
            }

        }


        private void button19_Click_1(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
        }

        private void open_file_m3u_FileOk(object sender, CancelEventArgs e)
        {

            dg1.Rows.Clear();
            stop_validating_url = false;

            String[] lines = File.ReadAllLines(open_file_m3u.FileName);

            for (int i = 0; i < lines.Count(); i++)
            {

                //MessageBox.Show(lines[i]);

                String out_file = String.Empty;

                if (lines[i].ToString().Length > 4 && lines[i].Substring(0, 4).ToLower() == "http")
                {

                    if (i > 0)

                    {

                        if (lines[i - 1].ToString().Substring(0, 7) == "#EXTINF")
                        {
                            out_file = lines[i - 1].Substring(lines[i - 1].LastIndexOf(",") + 1, lines[i - 1].Length - lines[i - 1].LastIndexOf(",") - 1);


                            if (out_file.Contains("/") || out_file.Contains(":") || out_file.Contains("*") || out_file.Contains("?") || out_file.Contains("¿") || out_file.Contains('\u0022') || out_file.Contains("<") || out_file.Contains(">") || out_file.Contains("|") || out_file.Contains("\\"))
                            {

                                out_file = out_file.Replace("/", "");
                                out_file = out_file.Replace(":", "");
                                out_file = out_file.Replace("*", "");
                                out_file = out_file.Replace("?", "");
                                out_file = out_file.Replace("¿", "");
                                out_file = out_file.Replace("\u0022", "");
                                out_file = out_file.Replace("<", "");
                                out_file = out_file.Replace(">", "");
                                out_file = out_file.Replace("|", "");
                                out_file = out_file.Replace("\\", "");
                            }

                            dg1.Rows.Add(lines[i], "", "", out_file, "");
                        }

                        else
                        {
                            out_file = Path.GetFileNameWithoutExtension(lines[i]);
                            dg1.Rows.Add(lines[i], "", "", out_file, "");
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                ctm_m3u.Enabled = false;
            }
            foreach (Control ct in groupBox_m3u.Controls)
            {
                ct.Enabled = false;
            }
            btn_cancel_validate.Enabled = true;

            BG_Validate_URLs.RunWorkerAsync();
        }


        private void button31_Click(object sender, EventArgs e)
        {
            open_file_m3u.ShowDialog();
        }


        private void Validate_added_row()
        {
            {

                dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Validating URL";

                if (dg1.Rows[dg1.RowCount - 1].Cells[0].Value == null)
                {
                    dg1.Rows[dg1.RowCount - 1].Cells[4].Value = String.Empty;
                    dg1.Rows[dg1.RowCount - 1].Cells[3].Value = String.Empty;
                    dg1.Rows[dg1.RowCount - 1].Cells[2].Value = String.Empty;
                    dg1.Rows[dg1.RowCount - 1].Cells[1].Value = String.Empty;
                    dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                    return;
                }


                if (dg1.Rows[dg1.RowCount - 1].Cells[0].Value.ToString().Contains("http") == false)
                {
                    dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Error";
                    dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    return;
                }

                dg1.Refresh();

                Process probe = new Process();
                probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 -timeout 10000000" + " -i " + '\u0022' + dg1.Rows[dg1.RowCount - 1].Cells[0].Value.ToString() + '\u0022';
                probe.StartInfo.RedirectStandardOutput = true;
                probe.StartInfo.UseShellExecute = false;
                probe.StartInfo.CreateNoWindow = true;
                probe.EnableRaisingEvents = true;

                probe.Start();

                String duracion = probe.StandardOutput.ReadLine();

                probe.WaitForExit(10000);

                if (duracion == null)
                {
                    dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Error";
                    dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow;
                    return;
                }

                if (duracion != null)
                {
                    if (duracion.Length >= 7)
                    {
                        dg1.Rows[dg1.RowCount - 1].Cells[1].Value = duracion.Substring(0, 7);
                        dg1.Rows[dg1.RowCount - 1].Cells[2].Value = duracion.Substring(0, 7);
                        dg1.Rows[dg1.RowCount - 1].Cells[3].Value = Path.GetFileNameWithoutExtension(dg1.Rows[dg1.RowCount - 1].Cells[0].Value.ToString()) + "_FF" + (dg1.RowCount).ToString();
                        dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Ready";
                        dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;

                        if (duracion.Substring(0, 7) == "0:00:00")
                        {
                            dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                            dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Ready";
                        }
                    }

                    if (duracion == "N/A")
                    {
                        dg1.Rows[dg1.RowCount - 1].Cells[1].Value = "N/A";
                        dg1.Rows[dg1.RowCount - 1].Cells[2].Value = "\u221E";
                        dg1.Rows[dg1.RowCount - 1].Cells[3].Value = Path.GetFileNameWithoutExtension(dg1.Rows[dg1.RowCount - 1].Cells[0].Value.ToString()) + "_FF" + (dg1.RowCount - 1).ToString();
                        dg1.Rows[dg1.RowCount - 1].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor;
                        dg1.Rows[dg1.RowCount - 1].Cells[4].Value = "Ready";
                    }
                }

            }
        }


        private void BG_Validate_URLs_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow row in dg1.Rows)
            {
                this.InvokeEx(f => f.ctm_m3u.Enabled = false);
            }
            foreach (Control ct in groupBox_m3u.Controls)
            {
                this.InvokeEx(f => ct.Enabled = false);
            }

            this.InvokeEx(f => f.btn_cancel_validate.Enabled = true);


            for (int i = 0; i < dg1.Rows.Count; i++)
            {

                if (dg1.Rows[i].Cells[0].Value == null)
                {
                    continue;
                }

                if (stop_validating_url == true)
                {
                    break;
                }

                if (dg1.Rows[i].Cells[4].Value.ToString() == "Ready" || dg1.Rows[i].Cells[4].Value.ToString() == "Processing" || dg1.Rows[i].Cells[4].Value.ToString() == "Success") continue;


                this.InvokeEx(f => f.dg1.Rows[i].Cells[4].Value = "Validating URL");
                this.InvokeEx(f => f.dg1.Refresh());

                probe_urls.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
                probe_urls.StartInfo.Arguments = "-i " + dg1.Rows[i].Cells[0].Value.ToString() + " -timeout 9000000";
                probe_urls.StartInfo.RedirectStandardOutput = true;
                probe_urls.StartInfo.RedirectStandardError = true;
                probe_urls.StartInfo.UseShellExecute = false;
                probe_urls.StartInfo.CreateNoWindow = true;
                probe_urls.EnableRaisingEvents = true;

                String duracion = String.Empty;
                String std_out = String.Empty;
                probe_urls.Start();

                while (!probe_urls.StandardError.EndOfStream)
                {
                    std_out = probe_urls.StandardError.ReadLine();

                    if (std_out.Contains("Duration: N/A"))
                    {
                        duracion = "N/A";

                    }
                    if (std_out.Contains("Duration: "))
                    {
                        TimeSpan time = new TimeSpan();
                        if (TimeSpan.TryParse(std_out.Substring(12, 7), out time))
                        {
                            duracion = std_out.Substring(12, 7);

                        }
                    }
                }
                probe_urls.WaitForExit();

                probe_urls.StartInfo.Arguments = String.Empty;

                if (duracion == null || duracion == string.Empty)
                {
                    this.InvokeEx(f => f.dg1.Rows[i].Cells[4].Value = "Error");
                    this.InvokeEx(f => f.dg1.Rows[i].DefaultCellStyle.BackColor = Color.LightGoldenrodYellow);
                    //return;
                }

                if (duracion != null)
                {
                    if (duracion.Length >= 7)
                    {
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[1].Value = duracion.Substring(0, 7));
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[2].Value = duracion.Substring(0, 7));
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[3].Value = dg1.Rows[i].Cells[3].Value.ToString() + "_FF" + (i + 1).ToString());
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[4].Value = "Ready");
                        this.InvokeEx(f => f.dg1.Rows[i].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor);

                        if (duracion.Substring(0, 7) == "0:00:00")
                        {
                            this.InvokeEx(f => f.dg1.Rows[i].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor);
                            this.InvokeEx(f => f.dg1.Rows[i].Cells[4].Value = "Ready");
                        }
                    }

                    if (duracion == "N/A")
                    {
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[1].Value = "N/A");
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[2].Value = "\u221E");
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[3].Value = dg1.Rows[i].Cells[3].Value.ToString() + "_FF" + (i + 1).ToString());
                        this.InvokeEx(f => f.dg1.Rows[i].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor);
                        this.InvokeEx(f => f.dg1.Rows[i].Cells[4].Value = "Ready");
                    }
                }
            }
        }

        private void chk_m3u_params_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_m3u_params.CheckState == CheckState.Checked)
            {

                txt_m3u_params.Enabled = true;

            }
            else
            {
                txt_m3u_params.Enabled = false;
            }
        }

        private void button16_Click_1(object sender, EventArgs e)
        {

            if (folderBrowser_m3u.ShowDialog() == DialogResult.OK)
            {
                txt_path_m3u.Text = folderBrowser_m3u.SelectedPath;
                txt_path_m3u.BackColor = Color.White;
            }

        }

        private void ct_validate_url_Click(object sender, EventArgs e)
        {

            dg1.EndEdit();
            dg1.ClearSelection();

            for (int i = 1; i < dg1.RowCount; i++)

            {

                if (dg1.Rows[i].Cells[4].Value.ToString() == "Error")

                {

                    dg1.Rows.RemoveAt(i);
                    i--;
                }

            }

            if (dg1.Rows[0].Cells[0] != null && dg1.Rows[0].Cells[4].Value.ToString() == "Error")
            {
                dg1.Rows.RemoveAt(0);
                return;
            }
            dg1.Refresh();
        }

        private void ct_del_m3u_Click(object sender, EventArgs e)
        {

            dg1.EndEdit();
            dg1.ClearSelection();

            for (int i = 1; i < dg1.RowCount; i++)

            {
                if (dg1.RowCount == 1 && dg1.Rows[0].Cells[0].Value == null)
                {
                    dg1.Rows.Clear();
                    return;

                }

                if (dg1.Rows[i].Cells[0].Value == null)

                {
                    dg1.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        private void ct_paste_m3u_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != String.Empty && Clipboard.GetText().ToLower().Contains("http"))
            {
                dg1.Rows.Add(Clipboard.GetText(), "", "", "", "");
                foreach (DataGridViewRow row in dg1.Rows)
                {
                    row.ReadOnly = true;
                    ctm_m3u.Enabled = false;
                }
                foreach (Control ct in groupBox_m3u.Controls)
                {
                    ct.Enabled = false;
                }
                Validate_added_row();
                foreach (DataGridViewRow row in dg1.Rows)
                {
                    row.ReadOnly = false;
                    ctm_m3u.Enabled = true;
                }
                foreach (Control ct in groupBox_m3u.Controls)
                {
                    ct.Enabled = true;
                }

                if (chk_m3u_params.CheckState == CheckState.Checked)
                {
                    txt_m3u_params.Enabled = true;
                }
                else
                {
                    txt_m3u_params.Enabled = false;
                }

                if (chk_output_server.CheckState == CheckState.Checked)
                {
                    txt_output_server.Enabled = true;
                }
                else
                {
                    txt_output_server.Enabled = false;
                }
            }
        }

        private void BG_Validate_URLs_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dg1.ReadOnly = false;

            foreach (Control ct in groupBox_m3u.Controls)
            {
                ct.Enabled = true;
            }

            if (chk_m3u_params.CheckState == CheckState.Checked)
            {
                txt_m3u_params.Enabled = true;
            }
            else
            {
                txt_m3u_params.Enabled = false;
            }

            if (chk_output_server.CheckState == CheckState.Checked)
            {
                txt_output_server.Enabled = true;
            }
            else
            {
                txt_output_server.Enabled = false;
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = false;
                ctm_m3u.Enabled = true;
            }
            if (was_started.Text == btn_start_m3u.Text)
            {
                btn_start_m3u.PerformClick();
            }

        }

        private void btn_start_m3u_Click(object sender, EventArgs e)
        {
            cancel_queue = false;
            notifyIcon1.Visible = true;
            foreach (DataGridViewRow row in dg1.Rows)
            {

                if (row.Cells[4].Value.ToString() == "Error")
                {
                    MessageBox.Show("There are errors on the list", "Errors found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (dg1.RowCount == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txt_m3u_params.Text == String.Empty)
            {
                MessageBox.Show("Capture parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_path_m3u.Text == "No path selected" && chk_output_server.CheckState == CheckState.Unchecked)
            {
                MessageBox.Show("Please select an output folder", "Ouput folder not configured", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String output_server = String.Empty;

            if (chk_output_server.CheckState == CheckState.Checked && txt_output_server.Text == String.Empty)
            {
                MessageBox.Show("Output to streaming is enabled but field is empty", "Output url is blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                output_server = txt_output_server.Text;
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[4].Value.ToString() == "Ready" && row.Cells[1].Value.ToString() != "N/A")
                {

                    if (TimeSpan.Parse(row.Cells[2].Value.ToString()).TotalSeconds > TimeSpan.Parse(row.Cells[1].Value.ToString()).TotalSeconds)
                    {
                        MessageBox.Show("Capture time for item " + row.Cells[3].Value.ToString() + " exceeds source duration", "Incorrect capture duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            foreach (DataGridViewRow row in dg1.Rows)
            {

                if (row.Cells[3].Value.ToString().Contains("/") || row.Cells[3].Value.ToString().Contains(":") || row.Cells[3].Value.ToString().Contains("*") || row.Cells[3].Value.ToString().Contains("?") || row.Cells[3].Value.ToString().Contains("¿") || row.Cells[3].Value.ToString().Contains('\u0022') || row.Cells[3].Value.ToString().Contains("<") || row.Cells[3].Value.ToString().Contains(">") || row.Cells[3].Value.ToString().Contains("|") || row.Cells[3].Value.ToString().Contains("\\"))
                {

                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("/", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace(":", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("*", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("?", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("¿", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("\u0022", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("<", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace(">", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("|", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("\\", "");
                }
            }

            //Pending validation
            Boolean pre_validate = false;


            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[4].Value.ToString() == String.Empty)
                {
                    pre_validate = true;
                }
            }
            if (pre_validate == true)
            {
                var a = MessageBox.Show("You need to validate URLs before processing. Click OK to validate, or cancel to abort", "Validation pending", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.OK)
                {
                    was_started.Text = btn_start_m3u.Text;
                    btn_validate_url.PerformClick();
                    return;
                }
                else
                {
                    return;
                }

            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[2].Value.ToString() == "\u221E")
                {
                    MessageBox.Show("Processing will not continue after items with undefined duration (infinite). Start all URLs instead", "items with undefinde duration in sequential mode", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            //Try preset

            this.Cursor = Cursors.WaitCursor;

            String file_prueba = dg1.Rows[0].Cells[0].Value.ToString();
            String file_output = dg1.Rows[0].Cells[4].Value.ToString() + "." + combo_ext_m3u.SelectedItem.ToString();
            String fichero = Path.GetFileName(file_prueba);

            String destino_test = Path.Combine(txt_path_m3u.Text, "FFBatch_test");
            if (!Directory.Exists(destino_test))
            {
                try
                {
                    Directory.CreateDirectory(destino_test);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show("Error writing test file: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    return;
                }
            }
            Process consola_pre = new Process();

            consola_pre.StartInfo.FileName = "ffmpeg.exe";
            consola_pre.StartInfo.Arguments = " -i " + file_prueba + " -t 1 " + txt_m3u_params.Text + " -y " + '\u0022' + destino_test + "\\" + file_output + '\u0022';

            consola_pre.StartInfo.CreateNoWindow = true;
            consola_pre.StartInfo.RedirectStandardError = true;
            consola_pre.StartInfo.RedirectStandardOutput = true;
            consola_pre.StartInfo.UseShellExecute = false;

            consola_pre.Start();

            String err_txt_1 = "";


            while (!consola_pre.StandardError.EndOfStream)
            {
                err_txt_1 = consola_pre.StandardError.ReadLine();
                this.InvokeEx(f => f.listBox4.Items.Add(err_txt_1));
                this.InvokeEx(f => f.listBox4.Refresh());

            }

            consola_pre.WaitForExit(8000);

            consola_pre.StartInfo.Arguments = String.Empty;

            if (consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                MessageBox.Show("Timeout trying to process url", "Url timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (consola_pre.ExitCode != 0)
            {
                this.Cursor = Cursors.Arrow;

                listBox4.TopIndex = listBox4.Items.Count - 1;
                listBox4.Refresh();
                MessageBox.Show("FFmpeg command failed on first item. Check FFmpeg output for more information. You may need to set different capture parameters.", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;
                if (File.Exists(Path.Combine(destino_test, file_output)))
                {

                    File.Delete(Path.Combine(destino_test, file_output));
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }

                return;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                if (File.Exists(Path.Combine(destino_test, file_output)))
                {
                    File.Delete(Path.Combine(destino_test, file_output));
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
            }

            //END try preset

            //Start pre-processing
            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;


            //Disable Datagrid edition
            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                ctm_m3u.Enabled = false;
                row.DefaultCellStyle.BackColor = Color.White;
            }


            DataGridView list_proc = new DataGridView();
            foreach (DataGridViewColumn col in dg1.Columns)
            {
                list_proc.Columns.Add((DataGridViewColumn)col.Clone());
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                list_proc.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString());

            }

            //Get total duration of files

            Pg1.Maximum = dg1.RowCount;

            Double total_duration = 0;
            Double total_prog = 0;
            int i_dur = 0;

            Double row_duration = 0;


            foreach (DataGridViewRow row in dg1.Rows)
            {

                DateTime time2;
                if (DateTime.TryParse(row.Cells[2].Value.ToString(), out time2))

                {
                    total_duration = total_duration + TimeSpan.Parse(row.Cells[2].Value.ToString()).TotalSeconds;

                }
                else
                {
                    //total_duration = total_duration + 0;
                }

                i_dur = i_dur + 1;
            }

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";
            String remain_time = "0";
            //End get total duration of files

            List<string> list_lines = new List<string>();
            process_glob.StartInfo.Arguments = String.Empty;

            time_n_tasks = 0;
            timer_tasks.Start();
            String m3u_params = txt_m3u_params.Text;
            String m3u_output_ext = combo_ext_m3u.SelectedItem.ToString();
            m3u_running = true;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;

                Disable_Controls();

                for (int list_index = 0; list_index < dg1.RowCount; list_index++)
                {
                    System.Threading.Thread.Sleep(50); //Allow kill process to send cancel_queue

                    String file = list_proc.Rows[list_index].Cells[0].Value.ToString();


                    //cancel queue REVIEW
                    if (cancel_queue == true)
                    {
                        working = false;
                        m3u_running = false;
                        Enable_Controls();

                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }
                    //End cancel queue   

                    //Skip current
                    if (skip_current_url == true)
                    {
                        skip_current_url = false;

                        continue;
                    }

                    //End skip current

                    String url_capture_time = String.Empty;
                    DateTime time2;
                    if (DateTime.TryParse(list_proc.Rows[list_index].Cells[2].Value.ToString(), out time2))

                    {
                        if (TimeSpan.Parse(list_proc.Rows[list_index].Cells[2].Value.ToString()).TotalSeconds > 0)
                        {
                            row_duration = TimeSpan.Parse(list_proc.Rows[list_index].Cells[2].Value.ToString()).TotalSeconds;
                            url_capture_time = " -t " + row_duration.ToString();
                        }
                        else
                        {
                            url_capture_time = String.Empty;
                        }
                    }


                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = file;
                    String destino = txt_path_m3u.Text;


                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    add_suffix = "";

                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                    }

                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(file);
                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }

                    String AppParam = String.Empty;

                    if (chk_output_server.CheckState == CheckState.Checked)
                    {
                        AppParam = pre_input_var + " " + pre_ss + " -i " + "" + file + " -y " + m3u_params + url_capture_time + " " + output_server;
                    }
                    else
                    {
                        AppParam = pre_input_var + " " + pre_ss + " -i " + "" + file + " -y " + m3u_params + url_capture_time + " " + '\u0022' + Path.Combine(destino, list_proc.Rows[list_index].Cells[3].Value.ToString() + "." + m3u_output_ext);
                    }


                    if (!Directory.Exists(destino))
                    {
                        try
                        {
                            Directory.CreateDirectory(destino);
                        }
                        catch (System.Exception excpt)
                        {
                            MessageBox.Show("Error writing output folder: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = Cursors.Arrow;
                            return;
                        }
                    }

                    process_glob.StartInfo.FileName = ffm;
                    process_glob.StartInfo.Arguments = AppParam;


                    this.InvokeEx(f => f.dg1.Rows[list_index].Cells[4].Value = "Processing");
                    this.InvokeEx(f => f.textBox7.Text = "0%");
                    this.InvokeEx(f => f.textBox7.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());


                    process_glob.StartInfo.RedirectStandardOutput = true;
                    process_glob.StartInfo.RedirectStandardError = true;
                    process_glob.StartInfo.RedirectStandardInput = true;
                    process_glob.StartInfo.UseShellExecute = false;
                    process_glob.StartInfo.CreateNoWindow = true;
                    process_glob.EnableRaisingEvents = true;

                    process_glob.Start();
                    if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                    this.InvokeEx(f => validate_duration = dg1.Rows[list_index].Cells[2].Value.ToString());

                    DateTime time3;
                    if (DateTime.TryParse(list_proc.Rows[list_index].Cells[2].Value.ToString(), out time3))
                    {
                        if (TimeSpan.Parse(list_proc.Rows[list_index].Cells[2].Value.ToString()).TotalSeconds != 0)
                        {
                            valid_prog = true;
                        }
                    }
                    else
                    {
                        valid_prog = false;
                    }

                    String err_txt = "";
                    Double interval = 0;

                    //REVIEW
                    while (!process_glob.StandardError.EndOfStream)
                    {
                        err_txt = process_glob.StandardError.ReadLine();
                        list_lines.Add(err_txt);

                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog == true)
                            {

                                this.InvokeEx(f => durat_n = row_duration);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                Double percent_tot = (total_prog * 100 / total_duration);
                                int percent_tot_2 = Convert.ToInt32(percent_tot);

                                if (percent_tot_2 <= 100)
                                {
                                    this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                    this.InvokeEx(f => f.Pg1.Refresh());
                                    this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                    this.InvokeEx(f => f.textBox5.Refresh());
                                }
                                if (percent2 <= 100)
                                {
                                    this.InvokeEx(f => f.pg_current.Value = percent2);
                                    this.InvokeEx(f => f.pg_current.Refresh());
                                    this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                                    this.InvokeEx(f => f.textBox4.Refresh());

                                }

                                //Estimated remaining time

                                remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                                remain_time = remain_time.Replace("x", String.Empty);
                                Double timing1 = 0;

                                if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                                }
                                else
                                {
                                    timing1 = Math.Round(Double.Parse(remain_time), 2);
                                }

                                Decimal timing = (decimal)timing1;
                                Decimal total_dur_dec = Convert.ToDecimal(total_duration);
                                Decimal total_prog_dec = Convert.ToDecimal(total_prog);
                                Decimal remain_secs = 0;
                                if (timing > 0)
                                {
                                    remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                                }

                                if (remain_secs > 60)
                                {
                                    remain_secs = remain_secs + 60;
                                }

                                String remain_from_secs = "";

                                TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                                remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                                   t.Hours,
                                  t.Minutes);

                                if (remain_secs >= 43200)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Math.Round(remain_secs / 3600).ToString() + " hours");
                                }

                                if (remain_secs >= 3600 && remain_secs < 43200)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                                }

                                if (remain_secs < 3600 && remain_secs >= 600)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                                }
                                if (remain_secs < 600 && remain_secs >= 120)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                                }

                                if (remain_secs <= 59)
                                {
                                    this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                                }


                                //End remaining time
                            }
                        }
                        //Read output, get progress
                        this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                        this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                    }
                    process_glob.WaitForExit();

                    process_glob.StartInfo.Arguments = String.Empty;

                    this.InvokeEx(f => f.pg_current.Value = 100);
                    this.InvokeEx(f => f.textBox4.Text = "100%");
                    list_lines.Add("");
                    list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    list_lines.Add("");


                    if (process_glob.ExitCode == 0)
                    {
                        this.InvokeEx(f => f.dg1.Rows[list_index].Cells[4].Value = "Success");
                        this.InvokeEx(f => f.dg1.Rows[list_index].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor);

                    }
                    else
                    {
                        this.InvokeEx(f => f.dg1.Rows[list_index].Cells[4].Value = "Failed");
                        this.InvokeEx(f => f.dg1.Rows[list_index].DefaultCellStyle.BackColor = Color.PaleGoldenrod);

                    }


                    if (list_index + 1 == list_proc.RowCount - 1)
                    {

                        this.InvokeEx(f => f.Pg1.Value = 100);
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        working = false;
                        m3u_running = false;
                        //Save log
                        string[] array_err = list_lines.ToArray();
                        String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                        System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                        SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                        SaveFile.WriteLine("-------------------------------");
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

                        //Automatic shutdown check
                        if (chkshut.Checked && cancel_queue == false)
                        {

                            Disable_Controls();

                            this.InvokeEx(f => f.chkshut.Enabled = false);
                            this.InvokeEx(f => f.btn_pause.Enabled = false);
                            this.InvokeEx(f => f.Timer_apaga.Start());

                            this.InvokeEx(f => f.TopMost = true);
                            this.InvokeEx(f => f.TB1.Enabled = true);
                            this.InvokeEx(f => f.TB1.Visible = true);
                            this.InvokeEx(f => f.button10.Enabled = true);
                            this.InvokeEx(f => f.button10.Visible = true);
                            this.InvokeEx(f => f.button20.Enabled = false);
                            this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                            notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                            String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                        }
                        //End shutdown check
                        else
                        {
                            if (cancel_queue == false)
                            {
                                if (Form.ActiveForm == null)
                                {
                                    notifyIcon1.BalloonTipText = "Queue processing completed";
                                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                                    notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                    notifyIcon1.ShowBalloonTip(0);
                                }

                                if (checkBox3.Checked)
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
                            else
                            {
                                this.InvokeEx(f => f.textBox5.Text = "100%");
                                this.InvokeEx(f => MessageBox.Show("Queue processing aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));

                            }
                        }

                    }
                }

                Enable_Controls();

                String borrar = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;



            }).Start();
        }


        private void dg1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {


                foreach (DataGridViewCell cell in dg1.SelectedCells)
                {
                    try
                    {
                        if (dg1.Rows[cell.RowIndex].Cells[0].Selected == true)
                        {

                            foreach (DataGridViewCell cell2 in dg1.Rows[cell.RowIndex].Cells)
                            {
                                cell2.Selected = false;
                            }

                            dg1.Rows.RemoveAt(cell.RowIndex);

                        }
                    }

                    catch (System.Exception)
                    {

                    }

                }

            }
        }


        private void btn_validate_url_Click_1(object sender, EventArgs e)
        {
            stop_validating_url = false;

            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                ctm_m3u.Enabled = false;
            }

            foreach (Control ct in groupBox_m3u.Controls)
            {
                ct.Enabled = false;
            }
            btn_cancel_validate.Enabled = true;

            BG_Validate_URLs.RunWorkerAsync();
        }

        private void combo_ext_m3u_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_ext_m3u.SelectedIndex == 0)
            {
                txt_m3u_params.Text = "-bsf:a aac_adtstoasc -c copy";
            }
            if (combo_ext_m3u.SelectedIndex == 1)
            {
                txt_m3u_params.Text = "-fflags +genpts";
            }


        }

        private void Pre_validate_urls()
        {
            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                ctm_m3u.Enabled = false;
            }

            foreach (Control ct in groupBox_m3u.Controls)
            {
                ct.Enabled = false;
            }
            btn_stop_m3u8.Enabled = true;
            btn_abort_urls.Enabled = true;
            BG_Validate_URLs.RunWorkerAsync();

        }

        private void btn_abort_urls_Click(object sender, EventArgs e)
        {

            if (m3u_running == true)
            {
                working = false;
                m3u_running = false;
                cancelados_paralelos = true;

                foreach (DataGridViewRow row in dg1.Rows)
                {

                    if (row.Cells[4].Value.ToString() != "Success" && row.Cells[4].Value.ToString() != "Ready")
                    {
                        row.Cells[4].Value = "Aborting";
                    }

                }

                foreach (Process proc in procs.Values)
                {
                    aborted_url = true;
                    StreamWriter write_q = proc.StandardInput;
                    write_q.Write("q");
                }
                return;
            }

            cancel_queue = true;
            cancelados_paralelos = true;

            if (process_glob.StartInfo.Arguments != String.Empty)
            {
                process_glob.Kill();
            }

            else
            {
                System.Threading.Thread.Sleep(250);
                Process[] localByName = Process.GetProcessesByName("ffmpeg");
                foreach (Process p in localByName)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(500);

                Process[] localByName2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process p2 in localByName2)
                {
                    p2.Kill();
                }
            }
        }

        private void btn_stop_m3u8_Click(object sender, EventArgs e)
        {
            if (working == true)
            {
                StreamWriter write_q = process_glob.StandardInput;
                write_q.Write("q");
            }
        }

        private void btn_cancel_validate_Click(object sender, EventArgs e)
        {
            stop_validating_url = true;
            System.Threading.Thread.Sleep(250);
            Process[] localByName = Process.GetProcessesByName("ffprobe");
            foreach (Process p in localByName)
            {
                p.Kill();
            }
            System.Threading.Thread.Sleep(500);

            Process[] localByName2 = Process.GetProcessesByName("ffprobe");
            foreach (Process p2 in localByName2)
            {
                p2.Kill();
            }

        }

        private void chk_output_server_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_output_server.CheckState == CheckState.Checked)
            {

                txt_output_server.Enabled = true;
                txt_path_m3u.Enabled = false;
                btn_browse_path_m3u.Enabled = false;

            }
            else
            {
                txt_output_server.Enabled = false;
                txt_path_m3u.Enabled = true;
                btn_browse_path_m3u.Enabled = true;
            }
        }

        private void ctm_m3u_Opening(object sender, CancelEventArgs e)
        {
            if (Clipboard.ContainsText() == false || Clipboard.GetText().Contains("http") == false)
            {
                ct_paste_m3u.Enabled = false;
            }
            else
            {
                ct_paste_m3u.Enabled = true;
            }

            if (dg1.RowCount == 0)
            {
                ct_del_m3u.Enabled = false;
                ct_validate_url.Enabled = false;

            }
            else
            {
                ct_del_m3u.Enabled = true;
                ct_validate_url.Enabled = true;

            }
            if (dg1.SelectedCells.Count > 0)
            {
                ct_play_vlc.Enabled = true;
                ct_show_urls.Enabled = true;
            }
            else
            {
                ct_show_urls.Enabled = false;
                ct_play_vlc.Enabled = false;
            }
            if (m3u_running == true)
            {
                ct_paste_m3u.Enabled = false;
                ct_del_m3u.Enabled = false;
                ct_validate_url.Enabled = false;
                ct_show_urls.Enabled = false;
                ct_play_vlc.Enabled = false;
                ctm_stop_url.Enabled = false;

                if (dg1.SelectedCells.Count == 1)
                {
                    ctm_stop_url.Enabled = true;
                    ctm_stop_url.Text = "Stop capturing " + '\u0022' + dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[3].Value.ToString() + '\u0022';
                }
            }
        }

        private void ct_play_vlc_Click(object sender, EventArgs e)
        {
            if (dg1.SelectedCells.Count > 0)
            {
                try
                {
                    Process vlc_proc = new Process();
                    vlc_proc.StartInfo.FileName = "vlc.exe";
                    vlc_proc.StartInfo.Arguments = dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                    vlc_proc.Start();
                }
                catch (System.Exception exctp)
                {
                    MessageBox.Show("Error trying to play URL with VLC: " + exctp.Message, "Error with vlc", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }

        private void dg1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        private void dg1_DragDrop(object sender, DragEventArgs e)
        {
            string[] file_drop = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (file_drop.Count() > 1)
            {
                MessageBox.Show("Please drop only one file.", "Multiple files dropped", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            long length = new System.IO.FileInfo(file_drop[0]).Length;

            if (length > 1048576)
            {
                MessageBox.Show("Dropped file is too big. Only text files under 1 MB are supported.", "File too big", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dg1.Rows.Clear();
            stop_validating_url = false;


            String[] lines = File.ReadAllLines(file_drop[0]);

            for (int i = 0; i < lines.Count(); i++)
            {
                String out_file = String.Empty;

                if (lines[i].ToString().Length > 4 && lines[i].Substring(0, 4).ToLower() == "http")
                {

                    if (i > 0)

                    {

                        if (lines[i - 1].ToString().Substring(0, 7) == "#EXTINF")
                        {
                            out_file = lines[i - 1].Substring(lines[i - 1].LastIndexOf(",") + 1, lines[i - 1].Length - lines[i - 1].LastIndexOf(",") - 1);

                            dg1.Rows.Add(lines[i], "", "", out_file, "");
                        }

                        else
                        {
                            out_file = Path.GetFileNameWithoutExtension(lines[i]);
                            dg1.Rows.Add(lines[i], "", "", out_file, "");
                        }
                    }
                }
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                ctm_m3u.Enabled = false;
            }
            foreach (Control ct in groupBox_m3u.Controls)
            {
                ct.Enabled = false;
            }
            btn_cancel_validate.Enabled = true;

            BG_Validate_URLs.RunWorkerAsync();


        }

        private void txt_search_url_TextChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[0].Value.ToString().ToLower().Contains(txt_search_url.Text.ToLower()) || row.Cells[3].Value.ToString().ToLower().Contains(txt_search_url.Text.ToLower()))
                {
                    dg1.ClearSelection();
                    dg1.Rows[row.Index].Cells[0].Selected = true;
                    dg1.CurrentCell = dg1.Rows[row.Index].Cells[0];
                    break;
                }
            }
        }

        private void btn_clean_errors_Click(object sender, EventArgs e)
        {
            button12.PerformClick();
        }

        private void timer_urls_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Ticked");
            if (probe_urls.StartInfo.Arguments != String.Empty)
            {
                probe_urls.Kill();
            }

        }

        private void btn_url_info_Click(object sender, EventArgs e)
        {
            Process ff_str = new Process();

            Task t = Task.Run(() =>
            {

                if (dg1.SelectedCells.Count == 1)

                {

                    ff_str.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    ff_str.StartInfo.Arguments = " -i " + dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[0].Value.ToString();
                    ff_str.StartInfo.RedirectStandardOutput = true;
                    ff_str.StartInfo.RedirectStandardError = true;
                    ff_str.StartInfo.UseShellExecute = false;
                    ff_str.StartInfo.CreateNoWindow = true;
                    ff_str.EnableRaisingEvents = true;
                    ff_str.Start();
                    String stream = "";
                    String sub_str = "";
                    //int c = 0;
                    Boolean has_stream = false;
                    String result = String.Empty;
                    while (!ff_str.StandardError.EndOfStream)
                    {
                        stream = ff_str.StandardError.ReadLine();

                        if (stream.Contains("Stream #0:"))
                        {
                            has_stream = true;

                            if (stream.Substring(stream.IndexOf("#0:") + 4, 1) == "(")
                            {
                                if (stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(und)" || stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(unk)")
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 11);
                                    result = result + Environment.NewLine + stream.Substring((stream.LastIndexOf("#0:") + 11), (stream.Length - sub_str.Length));

                                }
                                else
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                                    result = result + Environment.NewLine + stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length));

                                }
                            }
                            else
                            {
                                if (stream.Contains("Video"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    string regex = "(\".*\")|('.*')|(\\(.*\\))";
                                    string output = Regex.Replace(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)), regex, "");
                                    result = result + Environment.NewLine + output + Environment.NewLine;

                                }
                                if (stream.Contains("Audio"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    string regex = "(\".*\")|('.*')|(\\(.*\\))";
                                    string output = Regex.Replace(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)), regex, "");
                                    result = result + Environment.NewLine + output + Environment.NewLine;

                                }
                                if (stream.Contains("Subtitle"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    string regex = "(\".*\")|('.*')|(\\(.*\\))";
                                    string output = Regex.Replace(stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)), regex, "");
                                    result = result + Environment.NewLine + output + Environment.NewLine;
                                }
                            }

                        }

                    }

                    ff_str.WaitForExit();
                    ff_str.StartInfo.Arguments = String.Empty;
                    if (has_stream == false)
                    {
                        MessageBox.Show("No usable streams found", "No streams found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("URL: " + dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + Environment.NewLine + result, "URL MULTIMEDIA INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    ff_str.StartInfo.Arguments = String.Empty;
                }
            });
            if (!t.Wait(9000) && ff_str.StartInfo.Arguments != String.Empty)
            {
                ff_str.Kill();
            }
        }

        private void ct_show_urls_Click(object sender, EventArgs e)
        {
            btn_url_info.PerformClick();
        }

        private void btn_n_urls_Click(object sender, EventArgs e)
        {
            cancelados_paralelos = false;
            notifyIcon1.Visible = true;
            foreach (DataGridViewRow row in dg1.Rows)
            {

                if (row.Cells[4].Value.ToString() == "Error")
                {
                    MessageBox.Show("There are errors on the list", "Errors found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (dg1.RowCount == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txt_m3u_params.Text == String.Empty)
            {
                MessageBox.Show("Capture parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (txt_path_m3u.Text == "No path selected" && chk_output_server.CheckState == CheckState.Unchecked)
            {
                MessageBox.Show("Please select an output folder", "Ouput folder not configured", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            String output_server = String.Empty;

            if (chk_output_server.CheckState == CheckState.Checked && txt_output_server.Text == String.Empty)
            {
                MessageBox.Show("Output to streaming is enabled but field is empty", "Output url is blank", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                output_server = txt_output_server.Text;
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[4].Value.ToString() == "Ready" && row.Cells[1].Value.ToString() != "N/A")
                {

                    if (TimeSpan.Parse(row.Cells[2].Value.ToString()).TotalSeconds > TimeSpan.Parse(row.Cells[1].Value.ToString()).TotalSeconds)
                    {
                        MessageBox.Show("Capture time for item " + row.Cells[3].Value.ToString() + " exceeds source duration", "Incorrect capture duration", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            foreach (DataGridViewRow row in dg1.Rows)
            {

                if (row.Cells[3].Value.ToString().Contains("/") || row.Cells[3].Value.ToString().Contains(":") || row.Cells[3].Value.ToString().Contains("*") || row.Cells[3].Value.ToString().Contains("?") || row.Cells[3].Value.ToString().Contains("¿") || row.Cells[3].Value.ToString().Contains('\u0022') || row.Cells[3].Value.ToString().Contains("<") || row.Cells[3].Value.ToString().Contains(">") || row.Cells[3].Value.ToString().Contains("|") || row.Cells[3].Value.ToString().Contains("\\"))
                {
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("/", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace(":", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("*", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("?", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("¿", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("\u0022", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("<", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace(">", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("|", "");
                    row.Cells[3].Value = row.Cells[3].Value.ToString().Replace("\\", "");
                }
            }

            //Pending validation
            Boolean pre_validate = false;


            foreach (DataGridViewRow row in dg1.Rows)
            {
                if (row.Cells[4].Value.ToString() == String.Empty)
                {
                    pre_validate = true;
                }
            }
            if (pre_validate == true)
            {
                var a = MessageBox.Show("You need to validate URLs before processing. Click OK to validate, or cancel to abort", "Validation pending", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.OK)
                {
                    was_started.Text = btn_start_m3u.Text;
                    btn_validate_url.PerformClick();
                    return;
                }
                else
                {
                    return;
                }

            }

            //Try preset

            this.Cursor = Cursors.WaitCursor;

            String file_prueba = dg1.Rows[0].Cells[0].Value.ToString();
            String file_output = dg1.Rows[0].Cells[4].Value.ToString() + "." + combo_ext_m3u.SelectedItem.ToString();
            String fichero = Path.GetFileName(file_prueba);

            String destino_test = Path.Combine(txt_path_m3u.Text, "FFBatch_test");
            if (!Directory.Exists(destino_test))
            {
                try
                {
                    Directory.CreateDirectory(destino_test);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show("Error writing test file: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    return;
                }
            }
            Process consola_pre = new Process();

            consola_pre.StartInfo.FileName = "ffmpeg.exe";
            consola_pre.StartInfo.Arguments = " -i " + file_prueba + " -t 1 " + txt_m3u_params.Text + " -y " + '\u0022' + destino_test + "\\" + file_output + '\u0022';

            consola_pre.StartInfo.CreateNoWindow = true;
            consola_pre.StartInfo.RedirectStandardError = true;
            consola_pre.StartInfo.RedirectStandardOutput = true;
            consola_pre.StartInfo.UseShellExecute = false;

            consola_pre.Start();

            String err_txt_1 = "";


            while (!consola_pre.StandardError.EndOfStream)
            {
                err_txt_1 = consola_pre.StandardError.ReadLine();
                this.InvokeEx(f => f.listBox4.Items.Add(err_txt_1));
                this.InvokeEx(f => f.listBox4.Refresh());

            }

            consola_pre.WaitForExit(8000);

            consola_pre.StartInfo.Arguments = String.Empty;

            if (consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                MessageBox.Show("Timeout trying to process url", "Url timeout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (consola_pre.ExitCode != 0)
            {
                this.Cursor = Cursors.Arrow;

                listBox4.TopIndex = listBox4.Items.Count - 1;
                listBox4.Refresh();
                MessageBox.Show("FFmpeg command failed on first item. Check FFmpeg output for more information. You may need to set different capture parameters.", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;
                if (File.Exists(Path.Combine(destino_test, file_output)))
                {

                    File.Delete(Path.Combine(destino_test, file_output));
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }

                return;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
                if (File.Exists(Path.Combine(destino_test, file_output)))
                {
                    File.Delete(Path.Combine(destino_test, file_output));
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
            }

            //END try preset

            //Start pre-processing
            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            Disable_Controls();
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;
            txt_remain.Text = "";

            //Disable Datagrid edition

            foreach (DataGridViewRow row in dg1.Rows)
            {
                row.ReadOnly = true;
                row.DefaultCellStyle.BackColor = Color.White;
            }
            //Disable menus
            ct_paste_m3u.Enabled = false;
            ct_del_m3u.Enabled = false;
            ct_show_urls.Enabled = false;
            ct_validate_url.Enabled = false;
            ct_play_vlc.Enabled = false;

            DataGridView list_proc = new DataGridView();
            foreach (DataGridViewColumn col in dg1.Columns)
            {
                list_proc.Columns.Add((DataGridViewColumn)col.Clone());
            }

            foreach (DataGridViewRow row in dg1.Rows)
            {
                list_proc.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(), row.Cells[4].Value.ToString());
            }

            Pg1.Maximum = dg1.RowCount;

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";
            //String remain_time = "0";
            //End get total duration of files

            time_n_tasks = 0;
            timer_tasks.Start();
            String m3u_params = txt_m3u_params.Text;
            String m3u_output_ext = combo_ext_m3u.SelectedItem.ToString();
            m3u_running = true;
            aborted_url = false;

            procs.Clear();
            for (int ii = 0; ii < dg1.RowCount; ii++)
            {
                procs.Add("proc_urls_" + ii.ToString(), new Process());
            }

            Disable_Controls();
            btn_stop_m3u8.Enabled = false;
            rows_running = 0;

            Boolean show_total_prog = false;
            foreach (DataGridViewRow row in dg1.Rows)
            {
                TimeSpan time = new TimeSpan();
                if (TimeSpan.TryParse(row.Cells[2].Value.ToString(), out time))
                {
                    show_total_prog = true;
                }
                else
                {
                    show_total_prog = false;
                    break;
                }
            }
            if (show_total_prog == true)
            {
                Pg1.Value = 0;
                Pg1.Maximum = dg1.RowCount * 100;
            }

            foreach (DataGridViewRow row in list_proc.Rows)
            {
                if (row.IsNewRow) continue;

                rows_running = list_proc.RowCount - 1;

                DataGridViewRow tmp_row = row;

                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;


                    List<string> list_lines = new List<string>();
                    String url_capture_time = String.Empty;

                    Double total_prog = 0;
                    Double row_duration = 0;

                    TimeSpan time2;
                    if (TimeSpan.TryParse(tmp_row.Cells[2].Value.ToString(), out time2))
                    {
                        row_duration = TimeSpan.Parse(tmp_row.Cells[2].Value.ToString()).TotalSeconds;
                        url_capture_time = " -t " + row_duration.ToString();
                    }

                    else
                    {
                        row_duration = 0;
                        url_capture_time = String.Empty;
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String file = tmp_row.Cells[0].Value.ToString();
                    String destino = txt_path_m3u.Text;


                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    add_suffix = "";


                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(file);
                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }

                    String AppParam = String.Empty;

                    if (chk_output_server.CheckState == CheckState.Checked)
                    {
                        AppParam = pre_input_var + " " + pre_ss + " -i " + file + " " + url_capture_time + " " + m3u_params + " -y " + output_server;
                    }
                    else
                    {
                        AppParam = pre_input_var + " " + pre_ss + " -i " + file + " " + url_capture_time + " " + m3u_params + " -y " + '\u0022' + Path.Combine(destino, tmp_row.Cells[3].Value.ToString() + "." + m3u_output_ext);
                    }

                    if (!Directory.Exists(destino))
                    {
                        try
                        {
                            Directory.CreateDirectory(destino);
                        }
                        catch (System.Exception excpt)
                        {
                            MessageBox.Show("Error writing output folder: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.Cursor = Cursors.Arrow;
                            return;
                        }
                    }
                    var tmp = procs["proc_urls_" + tmp_row.Index];
                    tmp.StartInfo.FileName = ffm;
                    tmp.StartInfo.Arguments = AppParam;

                    this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].Cells[4].Value = "Processing");
                    this.InvokeEx(f => f.textBox7.Text = "0%");
                    this.InvokeEx(f => f.textBox7.Refresh());
                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    tmp.StartInfo.RedirectStandardOutput = true;
                    tmp.StartInfo.RedirectStandardError = true;
                    tmp.StartInfo.RedirectStandardInput = true;
                    tmp.StartInfo.UseShellExecute = false;
                    tmp.StartInfo.CreateNoWindow = true;
                    tmp.EnableRaisingEvents = true;

                    tmp.Start();
                    if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                    // this.InvokeEx(f => validate_duration = tmp_row.Cells[2].Value.ToString());
                    Boolean valid_prog2 = false;
                    TimeSpan time3;
                    if (TimeSpan.TryParse(tmp_row.Cells[2].Value.ToString(), out time3))
                    {
                        if (TimeSpan.Parse(tmp_row.Cells[2].Value.ToString()).TotalSeconds > 0)
                        {
                            valid_prog2 = true;
                        }
                    }
                    else
                    {
                        valid_prog2 = false;
                    }

                    String err_txt = "";
                    Double interval = 0;

                    //REVIEW
                    while (!tmp.StandardError.EndOfStream)
                    {
                        err_txt = tmp.StandardError.ReadLine();
                        //list_lines.Add(err_txt);

                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog2 == true)
                            {
                                this.InvokeEx(f => durat_n = row_duration);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                //Double percent_tot = (total_prog * 100 / total_duration);
                                //int percent_tot_2 = Convert.ToInt32(percent_tot);

                                //{
                                //this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                //this.InvokeEx(f => f.Pg1.Refresh());
                                //this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                //this.InvokeEx(f => f.textBox5.Refresh());
                                //}

                                if (percent2 <= 100)
                                {
                                    //this.InvokeEx(f => f.pg_current.Value = percent2);
                                    //this.InvokeEx(f => f.pg_current.Refresh());
                                    this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].Cells[4].Value = percent2.ToString() + "%");

                                    if (show_total_prog == true)
                                    {
                                        int dg_multi_prog = 0;
                                        foreach (DataGridViewRow row_p in dg1.Rows)
                                        {
                                            if (row_p.Cells[4].Value.ToString().Contains("%") == true)
                                            {
                                                this.InvokeEx(f => dg_multi_prog = dg_multi_prog + (Convert.ToInt32(row_p.Cells[4].Value.ToString().Replace("%", ""))));
                                            }

                                            if (row_p.Cells[4].Value.ToString() == "Success" || row_p.Cells[4].Value.ToString() == "Failed" || row_p.Cells[4].Value.ToString() == "Aborted")
                                            {
                                                this.InvokeEx(f => dg_multi_prog = dg_multi_prog + 100);
                                            }
                                        }

                                        this.InvokeEx(f => f.Pg1.Value = dg_multi_prog);
                                        this.InvokeEx(f => f.textBox5.Text = (Pg1.Value / dg1.RowCount).ToString() + "%");
                                        this.InvokeEx(f => f.textBox5.Refresh());
                                    }

                                    //this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                                    //this.InvokeEx(f => f.textBox4.Refresh());
                                }


                                //Estimated remaining time

                                //                                remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                                //                              remain_time = remain_time.Replace("x", String.Empty);
                                //  Double timing1 = 0;

                                //if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                                //{
                                //timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                                //}
                                //else
                                //{
                                //timing1 = Math.Round(Double.Parse(remain_time), 2);
                                //}

                                // Decimal timing = (decimal)timing1;
                                //Decimal total_dur_dec = Convert.ToDecimal(total_duration);
                                //Decimal total_prog_dec = Convert.ToDecimal(total_prog);
                                //Decimal remain_secs = 0;
                                //if (timing > 0)
                                //{
                                //remain_secs = (decimal)(total_dur_dec - total_prog_dec) / timing;
                                //}
                                //
                                //if (remain_secs > 60)
                                //{
                                //remain_secs = remain_secs + 60;
                                //}

                                //String remain_from_secs = "";

                                //TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                                //remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                                //t.Hours,
                                //t.Minutes);

                                //if (remain_secs >= 43200)
                                //{
                                //this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Math.Round(remain_secs / 3600).ToString() + " hours");
                                //}

                                //if (remain_secs >= 3600 && remain_secs < 43200)
                                //{
                                //this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                                //}

                                //if (remain_secs < 3600 && remain_secs >= 600)
                                //{
                                //this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                                //}
                                //if (remain_secs < 600 && remain_secs >= 120)
                                //{
                                //this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                                //}
                                //
                                //if (remain_secs <= 59)
                                //{
                                //this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                                //}


                                //End remaining time
                            }
                        }
                        //Read output, get progress
                        //this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                        //this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                    } // while
                    tmp.WaitForExit();
                    tmp.StartInfo.Arguments = String.Empty;

                    //this.InvokeEx(f => f.pg_current.Value = 100);
                    //this.InvokeEx(f => f.textBox4.Text = "100%");
                    //list_lines.Add("");
                    //list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    //list_lines.Add("");

                    if (tmp.ExitCode == 0)
                    {
                        if (aborted_url == false)
                        {
                            rows_running = rows_running - 1;
                            this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].Cells[4].Value = "Success");
                            this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].DefaultCellStyle.BackColor = dg1.DefaultCellStyle.BackColor);

                        }

                        else
                        {
                            rows_running = rows_running - 1;
                            this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].Cells[4].Value = "Aborted");
                            this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].DefaultCellStyle.BackColor = Color.PaleGoldenrod);
                            if (cancelados_paralelos == false)
                            {
                                aborted_url = false;
                            }
                        }

                    }
                    else
                    {
                        rows_running = rows_running - 1;

                        this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].Cells[4].Value = "Failed");
                        this.InvokeEx(f => f.dg1.Rows[tmp_row.Index].DefaultCellStyle.BackColor = Color.PaleGoldenrod);

                    }

                    if (rows_running == 0)
                    {
                        this.InvokeEx(f => f.Pg1.Value = Pg1.Maximum);
                        this.InvokeEx(f => f.textBox5.Text = "100%");

                        //Automatic shutdown

                        if (chkshut.Checked && cancelados_paralelos == false)
                        {
                            Disable_Controls();
                            this.InvokeEx(f => f.btn_stop_m3u8.Enabled = false);
                            this.InvokeEx(f => f.btn_abort_urls.Enabled = false);

                            this.InvokeEx(f => f.chkshut.Enabled = false);
                            this.InvokeEx(f => f.btn_pause.Enabled = false);
                            this.InvokeEx(f => f.Timer_apaga.Start());

                            this.InvokeEx(f => f.TopMost = true);
                            this.InvokeEx(f => f.TB1.Enabled = true);
                            this.InvokeEx(f => f.TB1.Visible = true);
                            this.InvokeEx(f => f.button10.Enabled = true);
                            this.InvokeEx(f => f.button10.Visible = true);
                            this.InvokeEx(f => f.button20.Enabled = false);
                            this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                            notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                            String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;
                        }

                        //End automatic shutdown
                        else
                        {
                            //cancel queue REVIEW
                            if (cancelados_paralelos == true)
                            {
                                working = false;
                                m3u_running = false;

                                this.InvokeEx(f => f.textBox5.Refresh());
                                foreach (DataGridViewRow row1 in dg1.Rows)
                                {
                                    this.InvokeEx(f => row1.ReadOnly = false);
                                }
                                Enable_Controls();

                                MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            //End cancel queue

                            Enable_Controls();
                            foreach (DataGridViewRow row1 in dg1.Rows)
                            {
                                this.InvokeEx(f => row1.ReadOnly = false);
                            }

                            this.InvokeEx(f => this.Cursor = Cursors.Arrow);

                            this.InvokeEx(f => f.ct_paste_m3u.Enabled = true);
                            this.InvokeEx(f => f.ct_del_m3u.Enabled = true);
                            this.InvokeEx(f => f.ct_show_urls.Enabled = true);
                            this.InvokeEx(f => f.ct_validate_url.Enabled = true);
                            this.InvokeEx(f => f.ct_play_vlc.Enabled = true);
                            this.InvokeEx(f => f.ctm_stop_url.Enabled = false);

                            notifyIcon1.BalloonTipText = "M3U url capturing complete";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);

                            if (checkBox3.Checked == true)
                            {
                                String destino2 = txt_path_m3u.Text;
                                if (Directory.GetFiles(destino).Length != 0)
                                {
                                    Process open_processed = new Process();
                                    open_processed.StartInfo.FileName = "explorer.exe";
                                    open_processed.StartInfo.Arguments = '\u0022' + destino + '\u0022';
                                    open_processed.Start();
                                }
                            }
                        }
                    }

                }).Start();
            }
        }

        private void ctm_stop_url_Click(object sender, EventArgs e)
        {
            if (dg1.SelectedCells.Count == 1 && dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[4].Value.ToString() != "Success" && dg1.Rows[dg1.SelectedCells[0].RowIndex].Cells[4].Value.ToString() != "Ready")
            {
                StreamWriter write_q = procs["proc_urls_" + dg1.Rows[dg1.SelectedCells[0].RowIndex].Index.ToString()].StandardInput;
                write_q.Write("q");
                aborted_url = true;
                return;
            }
        }

        private void BG_Try_preset_DoWork(object sender, DoWorkEventArgs e)
        {
            this.InvokeEx(f => this.Cursor = Cursors.WaitCursor);
            ListBox LB1_o = new ListBox();
            Process consola_pre = new Process();
            String file_prueba = "";
            String sel_test = "";
            this.InvokeEx(f => sel_test = listView1.SelectedItems[0].Text);
            file_prueba = sel_test;
            String destino_test = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            Boolean bad_chars = false;
            Task tt = Task.Run(() =>
            {

                String fichero = Path.GetFileName(file_prueba);

                if (!Directory.Exists(destino_test))
                {
                    try
                    {
                        Directory.CreateDirectory(destino_test);
                    }
                    catch (System.Exception excpt)
                    {
                        MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;
                    }
                }

                String ext_output = textBox2.Text;
                if (textBox2.Text == String.Empty)
                {
                    ext_output = Path.GetExtension(file_prueba);
                }
                else
                {
                    ext_output = "." + textBox2.Text;
                }

                textbox_params = textBox1.Text;
                String file_prueba2 = file_prueba;
                if (textbox_params.Contains("%1"))
                {
                    if (file_prueba2.Contains("[") || file_prueba2.Contains("]"))
                    {
                        MessageBox.Show("Input file name contains characters [ ]. Please remove them from input file name to avoid errors with -vf filter", "Conflicting characters in file name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Enable_Controls();
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        tried_ok = false;
                        bad_chars = true;
                        return;
                    }
                    file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                    file_prueba2 = file_prueba2.Replace(":", "\\\\" + ":");
                    textbox_params = textbox_params.Replace("%1", file_prueba2);

                }

                consola_pre.StartInfo.FileName = "ffmpeg.exe";
                consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " -t 00:00:00.250" + " -y " + textbox_params + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + ext_output + '\u0022';
                consola_pre.StartInfo.RedirectStandardOutput = true;
                consola_pre.StartInfo.RedirectStandardError = true;
                consola_pre.StartInfo.UseShellExecute = false;
                consola_pre.StartInfo.CreateNoWindow = true;
                consola_pre.EnableRaisingEvents = true;
                //consola_pre.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                consola_pre.Start();
                
                while (!consola_pre.StandardError.EndOfStream)
                {
                    this.InvokeEx(f => LB1_o.Items.Add(consola_pre.StandardError.ReadLine()));
                    this.InvokeEx(f => LB1_o.TopIndex = LB1_o.Items.Count - 1);
                    this.InvokeEx(f => LB1_o.Refresh());
                }

                consola_pre.WaitForExit();
                consola_pre.StartInfo.Arguments = String.Empty;
            });


            if (!tt.Wait(10000) && consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                
                var a = MessageBox.Show("Time out trying preset. Do you wish to continue?", "FFmpeg command timed out", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (a == DialogResult.Cancel)
                {

                   tried_ok = false;

                if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    return;
                }

                else
                {
                    tried_ok = true;
                    return;
                }
                
            }

            if (bad_chars == false)
            {
                if (consola_pre.ExitCode != 0)
                {
                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    MessageBox.Show("FFmpeg command failed on first file: " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    tried_ok = false;
                    return;
                }
                else
                {
                    System.Threading.Thread.Sleep(50);
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }
                    tried_ok = true;
                }
            }
            //END try preset
            this.InvokeEx(f => this.Cursor = Cursors.Arrow);

            if (Directory.GetFiles(destino_test).Length == 0)
            {
                System.IO.Directory.Delete(destino_test);
            }
            LB1_o.Items.Clear();
        }

        private void BG_Try_preset_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (tried_ok == true)
            {
                if (was_started.Text == button2.Text)
                {
                    button2.PerformClick();
                }
                if (was_started.Text == button17.Text)
                {
                    button17.PerformClick();
                }
                if (was_started.Text == btn_multi_m.Text)
                {
                    btn_multi_m.PerformClick();
                }
                if (was_started.Text == button15.Text)
                {
                    button15.PerformClick();
                }
                if (was_started.Text == button14.Text)
                {
                    button14.PerformClick();
                }
            }
        }

        private void BG_Try_button_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean failed = false;
            String sel_test = "";
            this.InvokeEx(f => sel_test = listView1.SelectedItems[0].Text);
            this.InvokeEx(f => this.Cursor = Cursors.WaitCursor);

            Form frm_output = new Form();
            frm_output.Name = "Try with con FFmpeg";
            frm_output.Icon = this.Icon;

            frm_output.Height = 575;
            frm_output.Width = 677;
            frm_output.FormBorderStyle = FormBorderStyle.Fixed3D;
            frm_output.MaximizeBox = false;
            frm_output.MinimizeBox = false;

            var fuente_list = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Regular);

            ListBox LB1_o = new ListBox();
            LB1_o.Parent = frm_output;
            LB1_o.Left = 20;
            LB1_o.Top = 65;
            LB1_o.Height = 425;
            LB1_o.Width = 620;
            LB1_o.Font = fuente_list;
            LB1_o.Click += new EventHandler(LB1_o_Click);

            TextBox titulo = new TextBox();
            titulo.Parent = frm_output;
            titulo.Top = 15;
            titulo.Left = 20;
            titulo.Width = 621;
            titulo.TabIndex = 0;
            var fuente = new System.Drawing.Font("Microsoft Sans Serif", 11, FontStyle.Bold);

            titulo.Font = fuente;
            titulo.BorderStyle = BorderStyle.Fixed3D;
            titulo.TextAlign = HorizontalAlignment.Center;
            titulo.ReadOnly = true;

            titulo.Text = "Processing with FFmpeg";

            Button boton_ok_ff = new Button();
            boton_ok_ff.Parent = frm_output;
            boton_ok_ff.Left = 20;
            boton_ok_ff.Top = 495;
            boton_ok_ff.Width = 620;
            boton_ok_ff.Height = 27;
            boton_ok_ff.Text = "Processing file, please wait...";
            boton_ok_ff.Click += new EventHandler(boton_ok_ff_Click);

            Process consola = new Process();
            String file_prueba = "";
            file_prueba = sel_test;
            String destino = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";

            Task tt = Task.Run(() =>
            {
                consola.StartInfo.FileName = "ffmpeg.exe";


                String fichero = Path.GetFileName(file_prueba);
                TextBox titulo2 = new TextBox();
                titulo2.Parent = frm_output;
                titulo2.Top = 42;
                titulo2.Left = 47;
                titulo2.Width = 567;

                var fuente2 = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Regular);

                titulo2.Font = fuente2;
                titulo2.BorderStyle = BorderStyle.None;
                titulo2.TextAlign = HorizontalAlignment.Center;
                titulo2.ReadOnly = true;

                titulo2.Text = (fichero);

                frm_output.StartPosition = FormStartPosition.CenterScreen;


                if (!Directory.Exists(destino))
                {
                    try
                    {
                        Directory.CreateDirectory(destino);
                    }
                    catch (System.Exception excpt)
                    {
                        MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Cursor = Cursors.Arrow;
                        return;
                    }
                }

                String ext_output = textBox2.Text;
                if (textBox2.Text == String.Empty)
                {
                    ext_output = Path.GetExtension(file_prueba);
                }
                else
                {
                    ext_output = "." + textBox2.Text;
                }
                textbox_params = textBox1.Text;
                String file_prueba2 = file_prueba;
                if (textbox_params.Contains("%1"))
                {
                    if (file_prueba2.Contains("[") || file_prueba2.Contains("]"))
                    {
                        MessageBox.Show("Input file name contains characters [ ]. Please remove them from input file name to avoid errors with -vf filter", "Conflicting characters in file name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        Enable_Controls();
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;

                    }

                    file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                    file_prueba2 = file_prueba2.Replace(":", "\\\\" + ":");
                    textbox_params = textbox_params.Replace("%1", file_prueba2);

                }

                consola.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " -t 00:00:00.250 " + " -y " + textbox_params + " " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + ext_output + '\u0022';

                consola.StartInfo.RedirectStandardOutput = true;
                consola.StartInfo.RedirectStandardError = true;
                consola.StartInfo.UseShellExecute = false;
                consola.StartInfo.CreateNoWindow = true;
                consola.EnableRaisingEvents = true;

                consola.Start();
                this.InvokeEx(f => boton_ok_ff.Text = "Close window");

                while (!consola.StandardError.EndOfStream)
                {
                    this.InvokeEx(f => LB1_o.Items.Add(consola.StandardError.ReadLine()));
                    this.InvokeEx(f => LB1_o.TopIndex = LB1_o.Items.Count - 1);
                    this.InvokeEx(f => LB1_o.Refresh());
                }

                //consola.WaitForExit();
                consola.StartInfo.Arguments = String.Empty;

                if (consola.ExitCode == 0)
                {
                    consola.StartInfo.Arguments = String.Empty;
                    LB1_o.Items.Add("");
                    LB1_o.Items.Add("FFMPEG TEST SUCCESSFUL.");

                    LB1_o.TopIndex = LB1_o.Items.Count - 1;
                    LB1_o.BackColor = Color.LightGreen;
                    LB1_o.SetSelected(LB1_o.Items.Count - 1, true);
                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    frm_output.ShowDialog();

                    String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                    if (File.Exists(borrar))
                    {
                        File.Delete(borrar);
                    }
                    if (Directory.GetFiles(destino).Length == 0)
                    {
                        System.IO.Directory.Delete(destino);
                    }
                    return;
                }
                else
                {
                    consola.StartInfo.Arguments = String.Empty;
                    this.InvokeEx(f => LB1_o.Items.Add(""));
                    this.InvokeEx(f => LB1_o.Items.Add("FFMPEG COMMAND FAILED. CHECK OUTPUT ABOVE"));
                    this.InvokeEx(f => LB1_o.TopIndex = LB1_o.Items.Count - 1);
                    this.InvokeEx(f => LB1_o.BackColor = Color.LightSalmon);
                    this.InvokeEx(f => LB1_o.SetSelected(LB1_o.Items.Count - 1, true));
                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    if (failed == false)
                    {
                        frm_output.ShowDialog();
                    }

                    return;
                }

            });


            if (!tt.Wait(5000) && consola.StartInfo.Arguments != String.Empty)
            {
                failed = true;
                consola.Kill();
                LB1_o.Items.Add("");
                LB1_o.Items.Add("FFMPEG PROCESS TIMED OUT AND HAD TO BE KILLED. TRY PROCESSING FULL FILE");
                LB1_o.TopIndex = LB1_o.Items.Count - 1;
                LB1_o.BackColor = Color.LightSalmon;
                LB1_o.SetSelected(LB1_o.Items.Count - 1, true);
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);

                frm_output.ShowDialog();
                this.InvokeEx(f => frm_output.Refresh());

            }
        }


        private void cti6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 1 && listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text != "Success" && listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text != "Queued" && listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text != "Aborted")
            {
                StreamWriter write_q = procs["proc_urls_" + listView1.Items[listView1.SelectedIndices[0]].Index.ToString()].StandardInput;
                write_q.Write("q");
                aborted_url = true;
                listView1.Items[listView1.SelectedIndices[0]].SubItems[4].Text = "Aborting";
                return;
            }
        }

        private void combo_prio_SelectedIndexChanged(object sender, EventArgs e)
        {
            mem_prio.SelectedItem = combo_prio.SelectedItem;
            System.Threading.Thread.Sleep(500);
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
            {
                if (combo_prio.SelectedIndex == 0 && multi_running == false) p.PriorityClass = ProcessPriorityClass.High;
                if (combo_prio.SelectedIndex == 1 && multi_running == false) p.PriorityClass = ProcessPriorityClass.AboveNormal;
                if (combo_prio.SelectedIndex == 2) p.PriorityClass = ProcessPriorityClass.Normal;
                if (combo_prio.SelectedIndex == 3) p.PriorityClass = ProcessPriorityClass.BelowNormal;
                if (combo_prio.SelectedIndex == 4) p.PriorityClass = ProcessPriorityClass.Idle;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {

                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {

            }
            if (comboBox1.Text == "Default saved parameters")
            {
                MessageBox.Show("Please select a different preset, or write a new preset description", "Select a different preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            String path_check = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";


            foreach (string line in File.ReadLines(path_check))
            {


                if (line.LastIndexOf("&") >= 0)
                {

                    if (line.Substring(4, line.LastIndexOf("&") - 5) == comboBox1.Text)
                    {
                        MessageBox.Show("Preset already exists. Change description before saving as a new one.", "Add nuew preset", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }


            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            string createText = "PR: " + comboBox1.Text + " & " + textBox1.Text + " % " + textBox2.Text;
            File.AppendAllText(path, Environment.NewLine + createText);


            comboBox1.Items.Clear();
            comboBox1.Items.Add("Default saved parameters");
            String path2 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            int linea = 0;

            foreach (string line in File.ReadLines(path2))
            {
                linea = linea + 1;

                if (line == "yes")

                {
                    checkBox3.CheckState = CheckState.Checked;
                }

                if (line == "no")
                {
                    checkBox3.CheckState = CheckState.Unchecked;
                }

                if (linea == 1)
                {
                    textBox1.Text = line;
                }

                if (linea == 2)
                {
                    textBox2.Text = line;
                }

                if (line.Contains("PR: "))
                {
                    comboBox1.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));

                }
            }

            comboBox1.SelectedIndex = comboBox1.Items.Count - 1;
            MessageBox.Show("The new preset has been saved.", "Preset saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button4.PerformClick();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.textBox1.Lines.Length >= 2 && e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }
        private void Change_mem_prio()
        {
            System.Threading.Thread.Sleep(250);
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
            {
                if (mem_prio.SelectedIndex == 0 && multi_running == false) p.PriorityClass = ProcessPriorityClass.High;
                if (mem_prio.SelectedIndex == 1 && multi_running == false) p.PriorityClass = ProcessPriorityClass.AboveNormal;
                if (mem_prio.SelectedIndex == 2) p.PriorityClass = ProcessPriorityClass.Normal;
                if (mem_prio.SelectedIndex == 3) p.PriorityClass = ProcessPriorityClass.BelowNormal;
                if (mem_prio.SelectedIndex == 4) p.PriorityClass = ProcessPriorityClass.Idle;
            }
        }

        private void btn_reset_path_Click(object sender, EventArgs e)
        {
            textBox3.Text = "..\\FFBatch";
            textBox9.Text = "..\\FFBatch";
            textBox3.BackColor = groupBox1.BackColor;
            textBox9.BackColor = groupBox1.BackColor;
            String path_s = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_path.ini";
            if (File.Exists(path_s))
            {
                File.Delete(path_s);
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("https://sourceforge.net/projects/ffmpeg-batch"))
                {

                }
            }
            catch
            {
                MessageBox.Show("Connection to the website failed", "Internet error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool exists = false;
            HttpWebResponse response = null;
            var request = (HttpWebRequest)WebRequest.Create("https://sourceforge.net/projects/ffmpeg-batch/files/FFbatch_setup_1.6.3_x86.msi/download");
            request.Method = "HEAD";
            request.Timeout = 5000; // milliseconds
            request.AllowAutoRedirect = true;

            try
            {
                response = (HttpWebResponse)request.GetResponse();
                exists = response.StatusCode == HttpStatusCode.OK;
            }
            catch
            {
                exists = false;
            }
            finally
            {
                // close your response.
                if (response != null)
                    response.Close();
            }
            if (exists == true)
            {
                var a = MessageBox.Show("A new version was found. Do you want to check the update?", "Update found", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.OK)
                {
                    Process.Start("https://www.videohelp.com/software/FFmpeg-Batch");
                }
            }
            else
            {
                var b = MessageBox.Show("You are using the latest version. Check release notes?", "No update found", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (b == DialogResult.OK)
                {
                    Process.Start("https://www.videohelp.com/software/FFmpeg-Batch");
                }
            }
        }

        private void btn_multi_m_Click(object sender, EventArgs e)
        {
            was_started.Text = btn_multi_m.Text;
            if (listView1.Items.Count == 1)
            {
                //   button2.PerformClick();
                //   return;
            }
            cancelados_paralelos = false;

            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ss_time_input.Text != "0:00:00")
            {
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "0:00:00" && item.SubItems[2].Text != "00:00:00" && item.SubItems[2].Text != "Pending")
                    {
                        if (TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds <= TimeSpan.Parse(ss_time_input.Text).TotalSeconds)
                        {
                            MessageBox.Show("Pre-input seeking exceeds duration of file: " + '\u0022' + Path.GetFileName(item.Text) + '\u0022', "Pre-input seeking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

            }

            empty_text = false;
            if (textBox2.Text == "")
            {
                empty_text = true;
                var a = MessageBox.Show("Format field is empty. Source file extension will be used. Continue?", "Format field blank", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Cancel) return;

            }
            avoid_overw();

            if (avoid_overwriting == true && textBox3.Text != "..\\FFBatch" && checkBox1.CheckState != CheckState.Checked)
            {
                avoid_overwriting = false;
                MessageBox.Show("Multiple folders in input files and a single output folder may lead to file overwriting. Please enable " + '\u0022' + "Recreate source path" + '\u0022' + " to avoid opossible overwritings, or double click on the output path textbox to set it to the default relative path", "Different input folders but single output folder", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox1.Text.Contains("libx264") || textBox1.Text.Contains("libx265") || textBox1.Text.Contains("jpeg2000") || textBox1.Text.Contains("libtheora") || textBox1.Text.Contains("libxvid") || textBox1.Text.Contains("mpeg2") || textBox1.Text.Contains("webp") || textBox1.Text.Contains("mpeg4") || textBox1.Text.Contains("libvpx"))
            {
                var a = MessageBox.Show("Video encoding tasks are already multi-thread, thus sequential single file processing is recommended. Continue anyway?", "Confirm video multi-file processing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.No)
                    return;
            }
            //Validated list, start processing
            txt_remain.Text = "";
            txt_remain.Refresh();
            total_time = true;
            n_th_suffix = String.Empty;
            n_th_source_ext = String.Empty;

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }
            //Try preset

            if (tried_ok == false)
            {
                BG_Try_preset.RunWorkerAsync();
                return;
            }
            tried_ok = false;

            //Remove test file/folder
            String file_prueba = "";
            String sel_test = listView1.SelectedItems[0].Text;
            file_prueba = sel_test;
            String destino = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

            if (File.Exists(borrar))
            {
                File.Delete(borrar);
            }

            if (Directory.Exists(destino) == true)
            {
                if (Directory.GetFiles(destino).Length == 0)
                {
                    System.IO.Directory.Delete(destino);
                }
            }

            //END Remove test file/folder

            //END try preset

            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            //if (textBox1.Text.Contains("libx264") || textBox1.Text.Contains("libx265") || textBox1.Text.Contains("jpeg2000") || textBox1.Text.Contains("libtheora") || textBox1.Text.Contains("libxvid") || textBox1.Text.Contains("mpeg2") || textBox1.Text.Contains("webp") || textBox1.Text.Contains("mpeg4") || textBox1.Text.Contains("libvpx"))
            //{
            ////if (a == DialogResult.No)
            //  return;
            //}

            cancel_queue = false;
            cancelados_paralelos = false;
            textBox4.Visible = false;

            working = true;
            listBox4.Items.Clear();
            groupBox5.Focus();
            Disable_Controls();

            //Total duration

            total_multi_duration = 0;
            foreach (ListViewItem item in listView1.Items)
            {

                DateTime time2;
                if (DateTime.TryParse(item.SubItems[2].Text, out time2))

                {
                    total_multi_duration = total_multi_duration + TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds;
                }
            }
            //End total duration

            //String remain_time = "0";
            start_total_time = 0;

            //interval = 0;
            procs.Clear();

            for (int ii = 0; ii < listView1.Items.Count; ii++)
            {
                procs.Add("proc_urls_" + ii.ToString(), new Process());
            }

            rows_running = 0;

            //Beginning multi-thread
            notifyIcon1.Visible = true;
            list_global_proc.Clear();
            foreach (ListViewItem item in listView1.Items)
            {
                list_global_proc.Items.Add((ListViewItem)item.Clone());
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";
            }

            Pg1.Value = 0;
            Pg1.Maximum = list_global_proc.Items.Count * 100;
            textBox5.Text = "";

            //listView1.SelectedIndices.Clear();
            textBox5.Visible = true;
            time_n_tasks = 0;
            timer_tasks.Start();
            timer1.Start();
            textBox7.Text = "Running tasks";
            textBox7.Visible = true;

            List<string> list_items = new List<string>();
            foreach (ListViewItem item in list_global_proc.Items)
            {
                list_items.Add(item.Index.ToString());
            }

            multi_running = true;
            //remains_multis.Clear();
            //for (int i = 0; i < listView1.Items.Count; i++)
            //{
            //  remains_multis.Add((decimal)i);
            //                remains_multis[i] = 0;
            //          }

            ParallelOptions par_op = new ParallelOptions();

            System.Threading.CancellationTokenSource cts = new System.Threading.CancellationTokenSource();

            par_op.CancellationToken = cts.Token;
            aborted = false;
            semaphore = new SemaphoreSlim(0, Convert.ToUInt16(n_threads.Value));

            Parallel.For(0, list_global_proc.Items.Count, par_op, (file_int) => {

                var t = Task.Run(() =>
                {
                    semaphore.Wait();

                    if (cancelados_paralelos == true)
                    {
                        cts.Cancel();
                        rows_running = 0;

                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();
                        if (aborted == true)
                        {
                            aborted = false;
                            MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        cancelados_paralelos = false;
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        cts.Dispose();
                        return;
                    }

                    List<string> list_lines = new List<string>();
                    String item_capture_time = String.Empty;
                    Double total_prog = 0;
                    Double row_duration = 0;
                    Boolean valid_prog2 = false;
                    TimeSpan time2;

                    if (TimeSpan.TryParse(list_global_proc.Items[file_int].SubItems[2].Text, out time2))
                    {
                        row_duration = TimeSpan.Parse(list_global_proc.Items[file_int].SubItems[2].Text).TotalSeconds;
                        valid_prog2 = true;
                    }

                    else
                    {
                        row_duration = 0;
                        valid_prog2 = false;
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = list_global_proc.Items[file_int].Text;
                    //String destino = file.Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";

                    //Begin Shifting
                    String shifting = "";
                    if (chk_shift.Checked == true)
                    {
                        if (Num_Shift.Value >= 0)
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " -map 0:v -map 1:a ";
                        }
                        else
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " -map 1:v -map 0:a ";
                        }
                    }

                    //End Shifting

                    //Change Volume
                    String change_vol = "";
                    if (chk_vol.Checked == true)
                    {
                        change_vol = "-filter:a " + '\u0022' + "volume=" + vol_ch.Value.ToString() + "dB " + '\u0022' + " ";
                    }
                    //End change volume


                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino = list_global_proc.Items[file_int].Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            String pre_dest = Path.GetDirectoryName(list_global_proc.Items[file_int].Text);
                            destino = Path.Combine(textBox3.Text, pre_dest.Substring(3, pre_dest.Length - 3));
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }
                    }

                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    add_suffix = "";
                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                        n_th_suffix = add_suffix;
                    }

                    String ext_output1 = textBox2.Text;
                    if (textBox2.Text == String.Empty)
                    {
                        ext_output1 = Path.GetExtension(list_global_proc.Items[file_int].Text);

                    }
                    else
                    {
                        ext_output1 = "." + textBox2.Text;
                    }
                    n_th_source_ext = ext_output1;

                    textbox_params = textBox1.Text;
                    String file2 = list_global_proc.Items[file_int].Text;
                    if (textbox_params.Contains("%1"))
                    {
                        file2 = file2.Replace("\\", "\\\\\\\\");
                        file2 = file2.Replace(":", "\\\\" + ":");
                        textbox_params = textbox_params.Replace("%1", file2);
                    }
                    String AppParam = pre_input_var + " " + pre_ss + " -i " + "" + '\u0022' + list_global_proc.Items[file_int].Text + '\u0022' + " " + shifting + " " + " -y " + textbox_params + " " + change_vol + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(list_global_proc.Items[file_int].Text) + add_suffix + ext_output1 + '\u0022';

                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    this.InvokeEx(f => f.pg_current.Value = 0);
                    this.InvokeEx(f => f.pg_current.Refresh());

                    var tmp = procs["proc_urls_" + file_int.ToString()];
                    tmp.StartInfo.FileName = ffm;
                    tmp.StartInfo.Arguments = AppParam;
                    tmp.StartInfo.RedirectStandardInput = true;
                    tmp.StartInfo.RedirectStandardOutput = true;
                    tmp.StartInfo.RedirectStandardError = true;
                    tmp.StartInfo.UseShellExecute = false;
                    tmp.StartInfo.CreateNoWindow = true;
                    tmp.EnableRaisingEvents = true;

                    if (cts.IsCancellationRequested == false)
                    {
                        tmp.Start();
                        this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Processing");
                        if (mem_prio.SelectedIndex != 2)
                        {
                            System.Threading.Thread.Sleep(50);
                            Change_mem_prio();
                        }
                    }
                    else
                    {
                        timer1.Stop();
                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();
                        cancelados_paralelos = false;
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        return;
                    }

                    rows_running = rows_running + 1;
                    this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Processing");
                    //this.InvokeEx(f => f.listView1.Items[file_int].EnsureVisible());

                    String err_txt = "";
                    Double interval = 0;
                    Double durat_n2 = 0;


                    //REVIEW
                    while (!tmp.StandardError.EndOfStream)
                    {
                        err_txt = tmp.StandardError.ReadLine();
                        //list_lines.Add(err_txt);

                        if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                        {
                            if (valid_prog2 == true)
                            {
                                this.InvokeEx(f => f.txt_remain.Refresh());
                                this.InvokeEx(f => durat_n2 = row_duration);
                                int start_time_index = err_txt.IndexOf("time=") + 5;
                                Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                                Double percent = (sec_prog * 100 / durat_n2);

                                total_prog = total_prog + (sec_prog - interval);
                                interval = sec_prog;
                                int percent2 = Convert.ToInt32(percent);

                                //Double percent_tot = (total_prog * 100 / total_duration);
                                //int percent_tot_2 = Convert.ToInt32(percent_tot);

                                //{
                                //this.InvokeEx(f => f.Pg1.Value = percent_tot_2);
                                //this.InvokeEx(f => f.Pg1.Refresh());
                                //this.InvokeEx(f => f.textBox5.Text = percent_tot_2.ToString() + "%");
                                //this.InvokeEx(f => f.textBox5.Refresh());
                                //}

                                if (percent2 <= 100)
                                {
                                    //this.InvokeEx(f => f.pg_current.Value = percent2);
                                    //this.InvokeEx(f => f.pg_current.Refresh());

                                    this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = percent2.ToString() + "%");
                                    list_global_proc.Items[file_int].SubItems[4].Text = percent2.ToString() + "%";

                                }
                            }

                        }

                    } // while

                    tmp.WaitForExit();
                    tmp.StartInfo.Arguments = String.Empty;
                    semaphore.Release();

                    //this.InvokeEx(f => f.pg_current.Value = 100);
                    //this.InvokeEx(f => f.textBox4.Text = "100%");
                    //list_lines.Add("");
                    //list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                    //list_lines.Add("");

                    if (tmp.ExitCode == 0)
                    {
                        if (cancelados_paralelos == false && cts.IsCancellationRequested == false)
                        {
                            if (aborted_url == false)
                            {
                                rows_running = rows_running - 1;
                                this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Success");

                                this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.White);
                            }
                            else
                            {
                                rows_running = rows_running - 1;
                                this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Aborted");

                                this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);
                                aborted_url = false;
                            }
                        }

                        else
                        {
                            rows_running = rows_running - 1;
                            this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Aborted");
                            list_global_proc.Items[file_int].SubItems[4].Text = "Aborted";
                            this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);
                            if (cancelados_paralelos == false)
                            {
                                aborted_url = false;
                            }
                        }

                    }

                    else
                    {
                        rows_running = rows_running - 1;

                        this.InvokeEx(f => f.listView1.Items[file_int].SubItems[4].Text = "Failed");
                        list_global_proc.Items[file_int].SubItems[4].Text = "Failed";
                        this.InvokeEx(f => f.listView1.Items[file_int].BackColor = Color.PaleGoldenrod);

                    }
                    file_int++;

                    if (rows_running == 0)
                    {
                        timer1.Stop();
                        timer_tasks.Stop();
                        this.InvokeEx(f => f.Pg1.Value = Pg1.Maximum);
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        this.InvokeEx(f => f.listView1.Enabled = true);
                        working = false;
                        multi_running = false;
                        Enable_Controls();

                        if (cancelados_paralelos == false && cts.IsCancellationRequested == false)
                        {
                            //Automatic shutdown check
                            if (chkshut.Checked)
                            {

                                Disable_Controls();
                                this.InvokeEx(f => f.chkshut.Enabled = false);
                                this.InvokeEx(f => f.btn_pause.Enabled = false);
                                this.InvokeEx(f => f.Timer_apaga.Start());

                                this.InvokeEx(f => this.TopMost = true);
                                this.InvokeEx(f => f.TB1.Enabled = true);
                                this.InvokeEx(f => f.TB1.Visible = true);

                                this.InvokeEx(f => f.button10.Visible = true);
                                this.InvokeEx(f => f.button10.Enabled = true);
                                this.InvokeEx(f => f.button20.Enabled = false);
                                this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                                notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                                notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                                notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                                notifyIcon1.ShowBalloonTip(0);
                                return;
                            }
                            //End shutdown check

                            notifyIcon1.BalloonTipText = "All FFmpeg processes completed";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);

                            if (checkBox3.Checked == true)
                            {
                                String primero_lista = list_global_proc.Items[0].Text;

                                String destino2 = "";
                                if (textBox3.Text == "..\\FFBatch")
                                {
                                    destino2 = destino2 = primero_lista.Substring(0, primero_lista.LastIndexOf('\\')) + "\\" + "FFBatch";
                                }
                                else
                                {
                                    destino2 = textBox3.Text;
                                }

                                if (Directory.GetFiles(destino2).Length == 0)
                                {

                                    if (Directory.Exists(destino2))
                                    {
                                        System.IO.Directory.Delete(destino2);
                                    }
                                }
                            }

                            return;
                        }
                        else
                        {
                            if (aborted == true)
                            {
                                aborted = false;
                                MessageBox.Show("Queue processing aborted by user", "FFmpeg processing aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            cancelados_paralelos = false;
                            this.InvokeEx(f => f.textBox7.Text = "FFmpeg output");
                            return;
                        }

                    }
                });
            });
            semaphore.Release(Convert.ToUInt16(n_threads.Value));
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void button16_Click_2(object sender, EventArgs e)

        {
            cancel_queue = false;
            if (list_tracks.SelectedIndices.Count > 1)
            {
                MessageBox.Show("Please select only one track to be saved", "Multiple tracks selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (ListViewItem file in list_tracks.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the track list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            if (list_tracks.Items.Count == 0)
            {
                MessageBox.Show("Tracks list is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (list_tracks.SelectedItems.Count == 0)
            {
                MessageBox.Show("No track was selected", "No track selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txt_track_format.Text == String.Empty && list_tracks.SelectedItems.Count > 0)
            {
                MessageBox.Show("Track extension is empty. Please select a track format", "No track format selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            String is_overw = textBox3.Text + "\\" + Path.GetFileNameWithoutExtension(list_tracks.Items[0].Text) + "." + txt_track_format;

            if (is_overw == list_tracks.Items[0].Text)
            {
                MessageBox.Show("You can't overwrite the main file with a saved track. Please select a different extension for saved track", "Overwriting not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            DateTime time2;
            if (!DateTime.TryParse(ss_time_input.Text, out time2))
            {
                MessageBox.Show("Pre-input seeking selected in Batch processing tab is incorrect. Change it or reset it by double-clicking on it", "Pre-input seeking format error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Validated list, start processing
            //Pending duration

            if (dur_ok == false)
            {
                list_pending_dur.Items.Clear();
                foreach (ListViewItem item in listView1.Items)
                {
                    list_pending_dur.Items.Add((ListViewItem)item.Clone());
                }
                BG_Dur.RunWorkerAsync();
                return;
            }

            Disable_Controls();
            txt_remain.Text = "Time remaining:";
            time_n_tasks = 0;
            timer_tasks.Start();

            cancel_queue = false;
            Pg1.Value = 0;
            pg_current.Value = 0;
            textBox4.Text = "0%";
            textBox4.Visible = true;
            working = true;
            textBox7.Visible = false;
            textBox5.Visible = true;


            //Copy list of tracks for thread processing
            ListView list_proc = new ListView();
            foreach (ListViewItem item in list_tracks.SelectedItems)
            {
                list_proc.Items.Add((ListViewItem)item.Clone());

            }
            //End of copy list of tracks for thread processing

            Pg1.Maximum = list_proc.Items.Count;

            Double total_duration = 0;
            Double total_prog = 0;
                                    
            //Duration
            Process probe = new Process();
            probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
            probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + list_proc.Items[0].Text + '\u0022';
            probe.StartInfo.RedirectStandardOutput = true;
            probe.StartInfo.UseShellExecute = false;
            probe.StartInfo.CreateNoWindow = true;
            probe.EnableRaisingEvents = true;
            probe.Start();

            String duracion = probe.StandardOutput.ReadLine();

            probe.WaitForExit();

            if (duracion != null)
            {

                if (duracion.Length >= 7)
                {
                    durat_n = TimeSpan.Parse(duracion).TotalSeconds;
                    total_duration = TimeSpan.Parse(duracion).TotalSeconds;
                }
            }
            else
            {
                total_duration = 0;
            }

            //End duration

            //End 

            Pg1.Minimum = 0;
            Pg1.Maximum = 100;
            textBox5.Text = "0%";

            List<string> list_lines = new List<string>();
            String mux_ext = txt_track_format.Text;

            // process_glob.StartInfo.Arguments = String.Empty;

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                String remain_time = "0";
                
                this.InvokeEx(f => f.pg_current.Value = 0);
                this.InvokeEx(f => f.pg_current.Refresh());

                String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");

                String file = list_proc.Items[0].Text;
                String fullPath = file;

                String destino = "";


                if (textBox3.Text == "..\\FFBatch")
                {
                    destino = file.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                }

                else
                {
                    destino = textBox3.Text;
                }

                if (!Directory.Exists(destino))
                {
             try
                {
                    Directory.CreateDirectory(destino);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    Enable_Controls();
                    working = false;
                    return;
                }
                }

                //Create joint inputs variable
                String inputs = String.Empty;
                foreach (ListViewItem input_item in list_proc.Items)
                {
                    String stream_type = String.Empty;
                    if (input_item.SubItems[2].Text.ToLower().Contains("subtitle"))
                    {
                       stream_type = "s";                        
                    }

                    if (input_item.SubItems[2].Text.ToLower().Contains("audio"))
                    {
                        stream_type = "a";
                    }
                    if (input_item.SubItems[2].Text.ToLower().Contains("video"))
                    {
                        stream_type = "v";
                    }
                    //{
                    //inputs = inputs + " -sub_charenc UTF-8" + " -i " + '\u0022' + input_item.Text + '\u0022';
                    //}
                    inputs = " -i " + '\u0022' + input_item.Text + '\u0022' + " -map 0:" + input_item.SubItems[1].Text + " -c:" + stream_type + " " + txt_track_param.Text + " ";

                }
                //End create joint inputs variable

                String AppParam = inputs + "-y " + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file) + "." + mux_ext + '\u0022';
                //MessageBox.Show(AppParam);
                if (!Directory.Exists(destino))
                {
                    Directory.CreateDirectory(destino);
                }

                process_glob.StartInfo.FileName = ffm;
                process_glob.StartInfo.Arguments = AppParam;
                valid_prog = false;

                this.InvokeEx(f => f.textBox7.Text = "0%");
                this.InvokeEx(f => f.textBox7.Refresh());
                this.InvokeEx(f => f.pg_current.Value = 0);
                this.InvokeEx(f => f.pg_current.Refresh());


                process_glob.StartInfo.RedirectStandardOutput = true;
                process_glob.StartInfo.RedirectStandardError = true;
                process_glob.StartInfo.RedirectStandardInput = true;
                process_glob.StartInfo.UseShellExecute = false;
                process_glob.StartInfo.CreateNoWindow = true;
                process_glob.EnableRaisingEvents = true;
                process_glob.Start();
                if (mem_prio.SelectedIndex != 2) Change_mem_prio();

                if (total_duration > 0)
                {
                    valid_prog = true;
                }

                else
                {
                    valid_prog = false;
                }

                String err_txt = "";
                Double interval = 0;

                while (!process_glob.StandardError.EndOfStream)
                {
                    err_txt = process_glob.StandardError.ReadLine();
                    list_lines.Add(err_txt);

                    if (valid_prog == true)
                    { 
                    if (err_txt.Contains("time=") && err_txt.Contains("time=-") == false)
                    {

                        //this.InvokeEx(f => durat_n = TimeSpan.Parse(listView1.Items[0].SubItems[2].Text).TotalSeconds);
                        total_prog = durat_n;
                        int start_time_index = err_txt.IndexOf("time=") + 5;
                        Double sec_prog = TimeSpan.Parse(err_txt.Substring(start_time_index, 8)).TotalSeconds;
                        Double percent = (sec_prog * 100 / durat_n);

                        total_prog = total_prog + (sec_prog - interval);
                        interval = sec_prog;
                        int percent2 = Convert.ToInt32(percent);


                        if (percent2 <= 100)
                        {
                            this.InvokeEx(f => f.pg_current.Value = percent2);
                            this.InvokeEx(f => f.pg_current.Refresh());
                            this.InvokeEx(f => f.textBox4.Text = (percent2).ToString() + "%");
                            this.InvokeEx(f => f.textBox4.Refresh());
                            this.InvokeEx(f => f.Pg1.Value = percent2);
                            this.InvokeEx(f => f.Pg1.Refresh());
                            this.InvokeEx(f => f.textBox5.Text = (percent2).ToString() + "%");
                            this.InvokeEx(f => f.textBox5.Refresh());

                        }

                        //Estimated remaining time

                        remain_time = err_txt.Substring(err_txt.LastIndexOf("speed=") + 6, err_txt.Length - err_txt.LastIndexOf("speed=") - 6);
                        remain_time = remain_time.Replace("x", String.Empty);
                        Double timing1 = 0;

                        if (System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",")
                        {
                            timing1 = Math.Round(Double.Parse(remain_time.Replace(".", ",")), 2);
                        }
                        else
                        {
                            timing1 = Math.Round(Double.Parse(remain_time), 2);
                        }
                        Decimal timing = (decimal)timing1;
                        Decimal total_dur_dec = Convert.ToDecimal(interval);
                        Decimal total_prog_dec = Convert.ToDecimal(total_prog);

                        Decimal remain_secs = 0;
                        if (timing > 0)
                        {
                            remain_secs = (decimal)(total_prog_dec - total_dur_dec) / timing;
                        }

                        if (remain_secs > 60)
                        {
                            remain_secs = remain_secs + 60;
                        }
                        String remain_from_secs = "";

                        TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(remain_secs));
                        remain_from_secs = string.Format("{0:D2}h:{1:D2}",
                           t.Hours,
                          t.Minutes);

                        if (remain_secs >= 3600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs + " min");
                        }

                        if (remain_secs < 3600 && remain_secs >= 600)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 1, 2) + " minutes");
                        }
                        if (remain_secs < 600 && remain_secs >= 120)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + remain_from_secs.Substring(remain_from_secs.LastIndexOf(":") + 2, 1) + " minutes");
                        }

                        if (remain_secs <= 59)
                        {
                            this.InvokeEx(f => f.txt_remain.Text = "Time remaining: " + Convert.ToInt16(remain_secs) + " seconds");
                        }

                        //End remaining time    
                      }
                    }

                    //Read output, get progress
                    this.InvokeEx(f => f.listBox4.Items.Add(err_txt));
                    this.InvokeEx(f => f.listBox4.TopIndex = listBox4.Items.Count - 1);

                }
                process_glob.WaitForExit();
                process_glob.StartInfo.Arguments = String.Empty;

                this.InvokeEx(f => f.pg_current.Value = 100);
                this.InvokeEx(f => f.textBox4.Text = "100%");
                list_lines.Add("");
                list_lines.Add("---------------------End of " + Path.GetFileName(file) + " log-------------------------------");
                list_lines.Add("");

                this.InvokeEx(f => f.Pg1.Value = 100);
                this.InvokeEx(f => f.textBox5.Text = "100%");
                working = false;
                //Save log
                string[] array_err = list_lines.ToArray();
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.log";

                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(path);
                SaveFile.WriteLine("FFmpeg log sesion: " + System.DateTime.Now);
                SaveFile.WriteLine("-------------------------------");
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
                Enable_Controls();


                if (process_glob.ExitCode == 0 && cancel_queue == false)
                {
                    if (chkshut.Checked)
                    {
                        Disable_Controls();
                        groupBox2.Enabled = true;
                        foreach (Control ct in groupBox2.Controls)
                        {
                            this.InvokeEx(f => ct.Enabled = false);
                        }

                        this.InvokeEx(f => f.TB2.Enabled = true);
                        this.InvokeEx(f => f.chkshut.Enabled = false);
                        this.InvokeEx(f => f.btn_pause.Enabled = false);
                        this.InvokeEx(f => f.Timer_apaga.Start());
                        this.InvokeEx(f => f.TopMost = true);
                        this.InvokeEx(f => f.TB1.Enabled = true);
                        this.InvokeEx(f => f.TB1.Visible = true);
                        this.InvokeEx(f => f.button10.Enabled = true);
                        this.InvokeEx(f => f.button10.Visible = true);
                        this.InvokeEx(f => f.button20.Enabled = false);
                        this.InvokeEx(f => f.TB1.Text = "Computer will shutdown in 60 seconds");
                        this.InvokeEx(f => f.TB2.Text = "Shutting shutdown in 60 seconds");
                        notifyIcon1.Visible = true;
                        notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                        notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                        notifyIcon1.ShowBalloonTip(0);
                        //String borrar_s = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                        //foreach (string file_s in Directory.GetFiles(destino_test))
                        //{
                        //File.Delete(file_s);
                        //}
                        //System.IO.Directory.Delete(destino_test);
                        return;
                    }

                    //End shutdown check
                    else
                    {
                        if (Form.ActiveForm == null)
                        {
                            notifyIcon1.Visible = true;
                            notifyIcon1.BalloonTipText = "Stream saving completed";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                            notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                            notifyIcon1.ShowBalloonTip(0);
                        }

                        if (checkBox3.Checked)
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
                }
                else
                {
                    if (process_glob.ExitCode == 0)
                    {
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        this.InvokeEx(f => MessageBox.Show("Stream saving aborted", "Aborted", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                    else
                    {
                        this.InvokeEx(f => f.textBox5.Text = "100%");
                        this.InvokeEx(f => MessageBox.Show("Stream saving failed: " + listBox4.Items[listBox4.Items.Count - 1].ToString(), "Error multiplexing", MessageBoxButtons.OK, MessageBoxIcon.Error));
                    }
                }

            }).Start();
        }

        private void ct3_save_track_Click(object sender, EventArgs e)
        {
            btn_extract.PerformClick();
        }

        private void btn_save_path_Click(object sender, EventArgs e)
        {
            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_path.ini";
            String path_to_save = String.Empty;
            //MessageBox.Show(textBox3.Text);
            if (textBox3.Text != ".." + "\\" + "FFBatch")
            {
                path_to_save = textBox3.Text;
            }

            File.WriteAllText(path, path_to_save);
        }

        private void btn_save_prio_Click(object sender, EventArgs e)
        {
            String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_priority.ini";
            String path_to_prio = String.Empty;
            //MessageBox.Show(textBox3.Text);
            if (combo_prio.SelectedIndex != 2)
            {
                path_to_prio =  combo_prio.SelectedIndex.ToString();
                File.WriteAllText(path, path_to_prio);
            }
            else
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            
        }

        private void btn_del_track_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count >0)
            {
                Boolean has_default = false;

                foreach (ListViewItem elemento in list_tracks.SelectedItems)
                {
                    list_tracks.Items.Remove(elemento);
                    pic_encode_param.Image = null;
                }

                //Review audio track defaults

                foreach (ListViewItem audio_item in list_tracks.Items)
                {
                    if (audio_item.SubItems[2].Text.Contains("Audio"))
                    {
                        if (audio_item.SubItems[4].Text == "Yes")
                        {
                            has_default = true;
                        }
                    }
                }

                if (has_default == false)
                {
                    foreach (ListViewItem audio_item in list_tracks.Items)
                    {
                        if (audio_item.SubItems[2].Text.Contains("Audio"))
                        {
                            audio_item.SubItems[4].Text = "Yes";
                            return;
                        }
                    }
                }
                //End review audio track defaults
                lbl_tr_n.Text = "Tracks: " + list_tracks.Items.Count.ToString();
            }
            else
            {
                MessageBox.Show("No track was selected to be removed.", "No track selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_default_track_Click(object sender, EventArgs e)
        {
            if (list_tracks.SelectedIndices.Count == 1)
            {
                if (list_tracks.SelectedIndices.Count == 0 || list_tracks.SelectedItems[0].SubItems[2].Text.Contains("Video"))
                {
                    return;
                }


                if (list_tracks.SelectedItems[0].SubItems[4].Text == "Yes")
                {
                    list_tracks.SelectedItems[0].SubItems[4].Text = "No";
                }
                else
                {
                    list_tracks.SelectedItems[0].SubItems[4].Text = "Yes";
                }


                //Review audio defaults
                int default_items = 0;

                for (int i = 0; i < list_tracks.Items.Count; i++)
                {
                    if (list_tracks.Items[i].SubItems[2].Text.Contains("Audio"))
                    {
                        if (list_tracks.Items[i].SubItems[4].Text == "Yes")
                        {
                            default_items = default_items + 1;
                        }
                    }
                }

                if (default_items > 1)
                {
                    foreach (ListViewItem audio_item in list_tracks.Items)
                    {
                        if (audio_item.SubItems[2].Text.Contains("Audio"))
                        {
                            if (audio_item.Text != list_tracks.SelectedItems[0].Text)
                            {
                                audio_item.SubItems[4].Text = "No";
                            }

                        }
                    }
                }

                if (default_items == 0)
                {
                    foreach (ListViewItem audio_item in list_tracks.Items)
                    {
                        if (audio_item.SubItems[2].Text.Contains("Audio"))
                        {
                            if (audio_item.Text != list_tracks.SelectedItems[0].Text)
                            {
                                audio_item.SubItems[4].Text = "Yes";
                                return;
                            }
                        }
                    }
                }


                //End review audio track defaults

                //Begin subtitle defaults

                //Review audio defaults
                int default_subs = 0;
                for (int i = 0; i < list_tracks.Items.Count; i++)
                {
                    if (list_tracks.Items[i].SubItems[2].Text.Contains("Subtitle"))
                    {
                        if (list_tracks.Items[i].SubItems[4].Text == "Yes")
                        {
                            default_subs = default_subs + 1;
                        }
                    }
                }

                if (default_subs > 1)
                {
                    foreach (ListViewItem audio_item in list_tracks.Items)
                    {
                        if (audio_item.SubItems[2].Text.Contains("Subtitle"))
                        {
                            if (audio_item != list_tracks.SelectedItems[0])
                            {
                                audio_item.SubItems[4].Text = "No";
                            }

                        }
                    }
                }

                //End subtitle defaults

            }
            else
            {
                MessageBox.Show("Please select just one track to change its default status", "No track selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void tracks_background()
        {            
            foreach (ListViewItem item in list_tracks.Items)
            {
                if (item.SubItems[2].Text.ToLower().Contains("video"))
                {
                    item.BackColor = Color.PapayaWhip;
                }

                if (item.SubItems[2].Text.ToLower().Contains("audio"))
                {
                    item.BackColor = Color.AliceBlue;
                }
                if (item.SubItems[2].Text.ToLower().Contains("subtitle"))
                {
                    item.BackColor = Color.LightGoldenrodYellow;
                }
            }
            //Duplicates
            for (int i = 0; i < list_tracks.Items.Count;i++)
            {
                for (int j = i + 1; j < list_tracks.Items.Count; j++)
                {
                    if (list_tracks.Items[i].Text == list_tracks.Items[j].Text && list_tracks.Items[i].SubItems[1].Text == list_tracks.Items[j].SubItems[1].Text)
                    {
                        //MessageBox.Show("Duplicated");
                        list_tracks.Items.RemoveAt(j);

                    }
                }
            }
            //End Duplicates
            lbl_tr_n.Text = "Tracks: " + list_tracks.Items.Count.ToString();
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            if (working == false) return;
            
            var proc_ffs = Process.GetProcessesByName("ffmpeg");
            foreach (Process proc in proc_ffs)
            {
                var process = Process.GetProcessById(proc.Id);

                process.Suspend();
            }
             var a = MessageBox.Show("FFmpeg encoding was paused. Click Ok to resume, or cancel to abort queue processing", "Encoding paused", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (a == DialogResult.Cancel)
            {
                var proc_ffs2 = Process.GetProcessesByName("ffmpeg");
                foreach (Process proc in proc_ffs2)
                {
                    var process = Process.GetProcessById(proc.Id);

                    process.Resume();
                }
                System.Threading.Thread.Sleep(100);
                button20.PerformClick();
                btn_mux_cancel.PerformClick();
            }
            else
            {
                var proc_ffs3 = Process.GetProcessesByName("ffmpeg");
                foreach (Process proc in proc_ffs3)
                {
                    var process = Process.GetProcessById(proc.Id);

                    process.Resume();
                }
            }
        }

        private void button16_Click_3(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = folderBrowserDialog1.SelectedPath;
                textBox3.BackColor = textBox1.BackColor;
                textBox9.Text = folderBrowserDialog1.SelectedPath;
                textBox9.BackColor = textBox1.BackColor;
            }
        }

        private void textBox9_DoubleClick(object sender, EventArgs e)
        {
            textBox3.Text = "..\\FFBatch";
            textBox3.BackColor = Control.DefaultBackColor;
            textBox9.Text = "..\\FFBatch";
            textBox9.BackColor = Control.DefaultBackColor;
        }
        
        private void old_multi()
         {
            if (listView1.Items.Count == 1)
            {
                button2.PerformClick();
                return;
            }

            cancelados_paralelos = false;

            if (textBox1.Text.Contains("libx264") || textBox1.Text.Contains("libx265") || textBox1.Text.Contains("jpeg2000") || textBox1.Text.Contains("libtheora") || textBox1.Text.Contains("libxvid") || textBox1.Text.Contains("mpeg2") || textBox1.Text.Contains("webp") || textBox1.Text.Contains("mpeg4") || textBox1.Text.Contains("libvpx"))
            {
                var a = MessageBox.Show("Video encoding is already a multi-thread process. Launching multiple video processing tasks is not recommended. Are you sure?", "Confirm multi-processing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.No)
                    return;
            }
            foreach (ListViewItem file in listView1.Items)
            {

                if (!File.Exists(file.Text))
                {
                    MessageBox.Show("File was not found: " + file.Text, "One file in the queue list was not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Processing queue is empty", "No files to be processed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Parameters field can not be empty", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox2.Text == "")
            {
                MessageBox.Show("Format field can not be empty, please add a file format extension", "Parameters error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ss_time_input.Text != "0:00:00")
            {
                foreach (ListViewItem item in listView1.Items)
                {

                    if (item.SubItems[2].Text != "N/A" && item.SubItems[2].Text != "0:00:00")
                    {
                        if (TimeSpan.Parse(item.SubItems[2].Text).TotalSeconds <= TimeSpan.Parse(ss_time_input.Text).TotalSeconds)
                        {
                            MessageBox.Show("Pre-input seeking exceeds duration of file: " + '\u0022' + Path.GetFileName(item.Text) + '\u0022', "Pre-input seeking error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }

            }

            //Validated list, start processing
            txt_remain.Text = "";
            total_time = true;

            if (listView1.SelectedIndices.Count == 0)
            {
                listView1.Items[0].Selected = true;
                listView1.Items[0].Focused = true;
                listView1.Focus();

            }
            //Try preset
            String sel_test = listView1.SelectedItems[0].Text;
            this.Cursor = Cursors.WaitCursor;

            String file_prueba = "";

            file_prueba = sel_test;
            String fichero = Path.GetFileName(file_prueba);

            String destino_test = file_prueba.Substring(0, file_prueba.LastIndexOf('\\')) + "\\" + "FFBatch_test";
            if (!Directory.Exists(destino_test))
            {
                Directory.CreateDirectory(destino_test);
            }
            Process consola_pre = new Process();
            consola_pre.StartInfo.FileName = "ffmpeg.exe";
            consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " " + "-ss 0 -t 1 " + " -y " + textBox1.Text + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text + '\u0022';
            consola_pre.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            consola_pre.Start();
            consola_pre.WaitForExit();


            if (consola_pre.ExitCode != 0)
            {
                this.Cursor = Cursors.Arrow;


                MessageBox.Show("FFmpeg command failed on first file. Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Cursor = Cursors.Arrow;

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }

                return;
            }
            else
            {

                this.Cursor = Cursors.Arrow;

            }

            //END try preset

            if (chkshut.Checked)
            {
                TB1.Visible = true;
            }
            cancel_queue = false;
            Pg1.Value = 0;
            textBox4.Visible = false;
            textBox5.Visible = false;
            working = true;
            listBox4.Items.Clear();
            Disable_Controls();

            //Beginning multi-thread
            notifyIcon1.Visible = true;
            ListView list_proc = new ListView();
            foreach (ListViewItem item in listView1.Items)
            {
                list_proc.Items.Add(item.Text);
                item.BackColor = Color.White;
                item.SubItems[4].Text = "Queued";

            }

            Pg1.Maximum = list_proc.Items.Count;
            listView1.SelectedIndices.Clear();
            textBox7.Text = "Launching tasks";
            textBox7.Visible = true;
            time_n_tasks = 0;
            timer_tasks.Start();

            new System.Threading.Thread(() =>
            {
                System.Threading.Thread.CurrentThread.IsBackground = true;
                /* run your code here */

                int launched = 0;

                int n_threads_int = Convert.ToInt32(n_threads.Value);
                foreach (ListViewItem file in list_proc.Items)
                {

                    if (cancelados_paralelos == true)
                    {
                        working = false;

                        this.InvokeEx(f => f.Pg1.Value = 0);
                        Enable_Controls();
                        MessageBox.Show("Queue processing aborted", "Tasks aborted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cancelados_paralelos = false;
                        this.InvokeEx(f => f.textBox7.Visible = false);
                        return;
                    }

                    String ffm = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
                    String fullPath = file.Text;
                    //String destino = file.Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";

                    //Begin Shifting
                    String shifting = "";
                    if (chk_shift.Checked == true)
                    {
                        if (Num_Shift.Value >= 0)
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 0:v -map 1:a ";
                        }
                        else
                        {
                            shifting = " -itsoffset " + Num_Shift.Value.ToString().Replace(",", ".") + " -i " + '\u0022' + file + '\u0022' + " -map 1:v -map 0:a ";
                        }
                    }

                    //End Shifting

                    //Change Volume
                    String change_vol = "";
                    if (chk_vol.Checked == true)
                    {
                        if (vol_ch.Value >= 0)
                        {
                            if (vol_ch.Value <= 99)
                            {
                                change_vol = "-filter:a " + '\u0022' + "volume=1." + vol_ch.Value.ToString() + '\u0022' + " ";
                            }
                            else
                            {
                                change_vol = "-filter:a " + '\u0022' + "volume=2." + vol_ch.Value.ToString().Substring(1, 2) + '\u0022' + " ";

                            }
                        }
                        else
                        {
                            change_vol = "-filter:a " + '\u0022' + "volume=0." + (100 + vol_ch.Value).ToString() + '\u0022' + " ";

                        }
                    }
                    //End change volume

                    String destino = "";
                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino = file.Text.Substring(0, fullPath.LastIndexOf('\\')) + "\\" + "FFBatch";
                    }
                    else
                    {
                        if (checkBox1.CheckState == CheckState.Checked)
                        {
                            String pre_dest = Path.GetDirectoryName(file.Text);
                            destino = Path.Combine(textBox3.Text, pre_dest.Substring(3, pre_dest.Length - 3));
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }
                    }

                    add_suffix = "";
                    if (chk_suffix.Checked == true && txt_suffix.Text != String.Empty)
                    {
                        add_suffix = txt_suffix.Text;
                        n_th_suffix = add_suffix;
                    }

                    String pre_input_var = "";
                    if (txt_pre_input.Text != "")
                    {
                        pre_input_var = txt_pre_input.Text;
                    }

                    String pre_ss = "";
                    if (TimeSpan.Parse(ss_time_input.Text).TotalSeconds != 0)
                    {
                        pre_ss = " -ss " + ss_time_input.Text;
                    }

                    String AppParam = pre_input_var + " " + pre_ss + " -i " + "" + '\u0022' + file.Text + '\u0022' + " " + shifting + " " + " -y " + textBox1.Text + " " + change_vol + '\u0022' + destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file.Text) + add_suffix + "." + textBox2.Text + '\u0022';

                    if (!Directory.Exists(destino))
                    {
                        Directory.CreateDirectory(destino);
                    }

                    Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = ffm;
                    process.StartInfo.Arguments = AppParam;
                    process.StartInfo.RedirectStandardOutput = false;
                    process.StartInfo.RedirectStandardError = false;
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.CreateNoWindow = false;
                    //process.EnableRaisingEvents = false;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

                    int num = 0;
                    do
                    {
                        Process[] localByName = Process.GetProcessesByName("ffmpeg");
                        num = localByName.Length;
                        System.Threading.Thread.Sleep(250);
                    }

                    while (num >= n_threads.Value);

                    process.Start();

                    if (mem_prio.SelectedIndex != 2)
                    {
                        System.Threading.Thread.Sleep(50);
                        Change_mem_prio();
                    }

                    launched = launched + 1;
                    this.InvokeEx(f => f.textBox7.Text = "Launching tasks: " + launched.ToString() + " of " + Pg1.Maximum.ToString());
                    process.WaitForExit();
                    //process.WaitForExit();
                    this.InvokeEx(f => f.listView1.Items[Pg1.Value].SubItems[4].Text = "Processing");
                    this.InvokeEx(f => f.listView1.Items[Pg1.Value].EnsureVisible());
                    this.InvokeEx(f => f.Pg1.Value = Pg1.Value + 1);
                    this.InvokeEx(f => f.Pg1.Refresh());

                }
                if (Pg1.Value == Pg1.Maximum)
                {

                    this.InvokeEx(f => f.TB1.Visible = false);
                    this.InvokeEx(f => f.Timer_old.Start());
                    this.InvokeEx(f => f.button2.Enabled = true);
                    this.InvokeEx(f => f.Pg1.Value = 0);
                    this.InvokeEx(f => f.Pg1.Maximum = n_threads_int);
                    this.InvokeEx(f => f.timer_tasks.Stop());
                }


                // End thread
                String borrar = destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + textBox2.Text;

                foreach (string file in Directory.GetFiles(destino_test))
                {
                    File.Delete(file);
                }
                System.IO.Directory.Delete(destino_test);
            }).Start();

        }

        private void Timer_old_Tick(object sender, EventArgs e)
        {
            time_n_tasks = time_n_tasks + 1;
            TimeSpan t = TimeSpan.FromSeconds(time_n_tasks);
            String tx_elapsed = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                t.Hours,
                t.Minutes,
                t.Seconds);
            txt_remain.Text = "Time elapsed: " + tx_elapsed;

            Disable_Controls();
            //textBox5.Visible = !textBox5.Visible;
            int num = 0;

            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            num = localByName.Length;
            Pg1.Value = Pg1.Maximum - num;
            textBox7.Text = "Waiting for running tasks: " + num;
            textBox7.Visible = true;

            for (int i = 0; i < (listView1.Items.Count - num); i++)
            {
                listView1.Items[i].SubItems[4].Text = "Waiting";
            }


            if (num == 0)
            {
                Enable_Controls();
                working = false;
                Timer_old.Stop();
                Pg1.Value = Pg1.Maximum;
                timer_tasks.Stop();
                txt_remain.Visible = true;
                TimeSpan t2 = TimeSpan.FromSeconds(time_n_tasks);
                String tx_elapsed2 = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                    t.Hours,
                    t.Minutes,
                    t.Seconds);
                txt_remain.Text = "Total time:  " + tx_elapsed;
                textBox7.Text = "FFmpeg threads finished";
                button14.Text = "Trim";
                int n_threads_int2 = Convert.ToInt32(n_threads.Value);
                //Check size for result
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    String destino_test = "";
                    if (textBox3.Text == "..\\FFBatch")
                    {
                        destino_test = listView1.Items[i].Text.Substring(0, listView1.Items[i].Text.LastIndexOf('\\')) + "\\" + "FFBatch" + "\\" + System.IO.Path.GetFileNameWithoutExtension(listView1.Items[i].Text) + "." + textBox2.Text;
                    }
                    else
                    {
                        destino_test = textBox3.Text + "\\" + System.IO.Path.GetFileNameWithoutExtension(listView1.Items[i].Text) + "." + textBox2.Text;

                    }


                    if (!File.Exists(destino_test))
                    {
                        listView1.Items[i].SubItems[4].Text = "Error";
                        listView1.Items[i].BackColor = Color.PaleGoldenrod;
                    }
                    else
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(destino_test);

                        var size = fileInfo.Length;

                        if (size > 0)
                        {
                            listView1.Items[i].SubItems[4].Text = "Success";
                        }
                        else
                        {
                            listView1.Items[i].SubItems[4].Text = "Error";
                            listView1.Items[i].BackColor = Color.PaleGoldenrod;
                        }
                    }
                }
                //End size for result

                if (cancelados_paralelos == false)
                {
                    //Automatic shutdown check
                    if (chkshut.Checked)
                    {

                        Disable_Controls();
                        Timer_apaga.Start();

                        this.TopMost = true;
                        TB1.Enabled = true;
                        TB1.Visible = true;

                        button10.Visible = true;
                        button10.Enabled = true;
                        button20.Enabled = false;
                        TB1.Text = "Computer will shutdown in 60 seconds";
                        notifyIcon1.BalloonTipText = "Computer will shutdown in 60 seconds";
                        notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                        notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                        notifyIcon1.ShowBalloonTip(0);
                        return;
                    }
                    //End shutdown check

                    notifyIcon1.BalloonTipText = "All FFmpeg processes completed";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                    notifyIcon1.BalloonTipTitle = "FFmpeg Batch";
                    notifyIcon1.ShowBalloonTip(0);
                    // MessageBox.Show("All ffmpeg processes completed", "FFmpeg processes completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (checkBox3.Checked == true)
                    {
                        String primero_lista = listView1.Items[0].Text;

                        String destino = "";
                        if (textBox3.Text == "..\\FFBatch")
                        {
                            destino = destino = primero_lista.Substring(0, primero_lista.LastIndexOf('\\')) + "\\" + "FFBatch";
                        }
                        else
                        {
                            destino = textBox3.Text;
                        }

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

                    return;
                }
                else
                {
                    MessageBox.Show("Process aborted by user. All ffmepg threads have been terminated", "FFmpeg processes terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cancelados_paralelos = false;
                    textBox7.Text = "FFmpeg output";
                    return;
                }

            }
        }

    } //Form Class

    public static class ISynchronizeInvokeExtensions
        {
            public static void InvokeEx<T>(this T @this, Action<T> action) where T : ISynchronizeInvoke
            {
                if (@this.InvokeRequired)
                {
                    @this.Invoke(action, new object[] { @this });
                }
                else
                {
                    action(@this);
                }
            }
        }

        public static class TextFileEncodingDetector
        {
            const long _defaultHeuristicSampleSize = 0x10000; //completely arbitrary - inappropriate for high numbers of files / high speed requirements

            public static Encoding DetectTextFileEncoding(string InputFilename)
            {
                using (FileStream textfileStream = File.OpenRead(InputFilename))
                {
                    return DetectTextFileEncoding(textfileStream, _defaultHeuristicSampleSize);
                }
            }

            public static Encoding DetectTextFileEncoding(FileStream InputFileStream, long HeuristicSampleSize)
            {
                bool uselessBool = false;
                return DetectTextFileEncoding(InputFileStream, _defaultHeuristicSampleSize, out uselessBool);
            }

            public static Encoding DetectTextFileEncoding(FileStream InputFileStream, long HeuristicSampleSize, out bool HasBOM)
            {
                if (InputFileStream == null)
                    throw new ArgumentNullException("Must provide a valid Filestream!", "InputFileStream");

                if (!InputFileStream.CanRead)
                    throw new ArgumentException("Provided file stream is not readable!", "InputFileStream");

                if (!InputFileStream.CanSeek)
                    throw new ArgumentException("Provided file stream cannot seek!", "InputFileStream");

                Encoding encodingFound = null;

                long originalPos = InputFileStream.Position;

                InputFileStream.Position = 0;


                //First read only what we need for BOM detection
                byte[] bomBytes = new byte[InputFileStream.Length > 4 ? 4 : InputFileStream.Length];
                InputFileStream.Read(bomBytes, 0, bomBytes.Length);

                encodingFound = DetectBOMBytes(bomBytes);

                if (encodingFound != null)
                {
                    InputFileStream.Position = originalPos;
                    HasBOM = true;
                    return encodingFound;
                }


                //BOM Detection failed, going for heuristics now.
                //  create sample byte array and populate it
                byte[] sampleBytes = new byte[HeuristicSampleSize > InputFileStream.Length ? InputFileStream.Length : HeuristicSampleSize];
                Array.Copy(bomBytes, sampleBytes, bomBytes.Length);
                if (InputFileStream.Length > bomBytes.Length)
                    InputFileStream.Read(sampleBytes, bomBytes.Length, sampleBytes.Length - bomBytes.Length);
                InputFileStream.Position = originalPos;

                //test byte array content
                encodingFound = DetectUnicodeInByteSampleByHeuristics(sampleBytes);

                HasBOM = false;
                return encodingFound;
            }

            public static Encoding DetectTextByteArrayEncoding(byte[] TextData)
            {
                bool uselessBool = false;
                return DetectTextByteArrayEncoding(TextData, out uselessBool);
            }

            public static Encoding DetectTextByteArrayEncoding(byte[] TextData, out bool HasBOM)
            {
                if (TextData == null)
                    throw new ArgumentNullException("Must provide a valid text data byte array!", "TextData");

                Encoding encodingFound = null;

                encodingFound = DetectBOMBytes(TextData);

                if (encodingFound != null)
                {
                    HasBOM = true;
                    return encodingFound;
                }
                else
                {
                    //test byte array content
                    encodingFound = DetectUnicodeInByteSampleByHeuristics(TextData);

                    HasBOM = false;
                    return encodingFound;
                }
            }

            public static string GetStringFromByteArray(byte[] TextData, Encoding DefaultEncoding)
            {
                return GetStringFromByteArray(TextData, DefaultEncoding, _defaultHeuristicSampleSize);
            }

            public static string GetStringFromByteArray(byte[] TextData, Encoding DefaultEncoding, long MaxHeuristicSampleSize)
            {
                if (TextData == null)
                    throw new ArgumentNullException("Must provide a valid text data byte array!", "TextData");

                Encoding encodingFound = null;

                encodingFound = DetectBOMBytes(TextData);

                if (encodingFound != null)
                {
                    //For some reason, the default encodings don't detect/swallow their own preambles!!
                    return encodingFound.GetString(TextData, encodingFound.GetPreamble().Length, TextData.Length - encodingFound.GetPreamble().Length);
                }
                else
                {
                    byte[] heuristicSample = null;
                    if (TextData.Length > MaxHeuristicSampleSize)
                    {
                        heuristicSample = new byte[MaxHeuristicSampleSize];
                        Array.Copy(TextData, heuristicSample, MaxHeuristicSampleSize);
                    }
                    else
                    {
                        heuristicSample = TextData;
                    }

                    encodingFound = DetectUnicodeInByteSampleByHeuristics(TextData) ?? DefaultEncoding;
                    return encodingFound.GetString(TextData);
                }
            }


            public static Encoding DetectBOMBytes(byte[] BOMBytes)
            {
                if (BOMBytes == null)
                    throw new ArgumentNullException("Must provide a valid BOM byte array!", "BOMBytes");

                if (BOMBytes.Length < 2)
                    return null;

                if (BOMBytes[0] == 0xff
                    && BOMBytes[1] == 0xfe
                    && (BOMBytes.Length < 4
                        || BOMBytes[2] != 0
                        || BOMBytes[3] != 0
                        )
                    )
                    return Encoding.Unicode;

                if (BOMBytes[0] == 0xfe
                    && BOMBytes[1] == 0xff
                    )
                    return Encoding.BigEndianUnicode;

                if (BOMBytes.Length < 3)
                    return null;

                if (BOMBytes[0] == 0xef && BOMBytes[1] == 0xbb && BOMBytes[2] == 0xbf)
                    return Encoding.UTF8;

                if (BOMBytes[0] == 0x2b && BOMBytes[1] == 0x2f && BOMBytes[2] == 0x76)
                    return Encoding.UTF7;

                if (BOMBytes.Length < 4)
                    return null;

                if (BOMBytes[0] == 0xff && BOMBytes[1] == 0xfe && BOMBytes[2] == 0 && BOMBytes[3] == 0)
                    return Encoding.UTF32;

                if (BOMBytes[0] == 0 && BOMBytes[1] == 0 && BOMBytes[2] == 0xfe && BOMBytes[3] == 0xff)
                    return Encoding.GetEncoding(12001);

                return null;
            }

            public static Encoding DetectUnicodeInByteSampleByHeuristics(byte[] SampleBytes)
            {
                long oddBinaryNullsInSample = 0;
                long evenBinaryNullsInSample = 0;
                long suspiciousUTF8SequenceCount = 0;
                long suspiciousUTF8BytesTotal = 0;
                long likelyUSASCIIBytesInSample = 0;

                //Cycle through, keeping count of binary null positions, possible UTF-8 
                //  sequences from upper ranges of Windows-1252, and probable US-ASCII 
                //  character counts.

                long currentPos = 0;
                int skipUTF8Bytes = 0;

                while (currentPos < SampleBytes.Length)
                {
                    //binary null distribution
                    if (SampleBytes[currentPos] == 0)
                    {
                        if (currentPos % 2 == 0)
                            evenBinaryNullsInSample++;
                        else
                            oddBinaryNullsInSample++;
                    }

                    //likely US-ASCII characters
                    if (IsCommonUSASCIIByte(SampleBytes[currentPos]))
                        likelyUSASCIIBytesInSample++;

                    //suspicious sequences (look like UTF-8)
                    if (skipUTF8Bytes == 0)
                    {
                        int lengthFound = DetectSuspiciousUTF8SequenceLength(SampleBytes, currentPos);

                        if (lengthFound > 0)
                        {
                            suspiciousUTF8SequenceCount++;
                            suspiciousUTF8BytesTotal += lengthFound;
                            skipUTF8Bytes = lengthFound - 1;
                        }
                    }
                    else
                    {
                        skipUTF8Bytes--;
                    }

                    currentPos++;
                }

                //1: UTF-16 LE - in english / european environments, this is usually characterized by a 
                //  high proportion of odd binary nulls (starting at 0), with (as this is text) a low 
                //  proportion of even binary nulls.
                //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
                //  60% nulls where you do expect nulls) are completely arbitrary.

                if (((evenBinaryNullsInSample * 2.0) / SampleBytes.Length) < 0.2
                    && ((oddBinaryNullsInSample * 2.0) / SampleBytes.Length) > 0.6
                    )
                    return Encoding.Unicode;


                //2: UTF-16 BE - in english / european environments, this is usually characterized by a 
                //  high proportion of even binary nulls (starting at 0), with (as this is text) a low 
                //  proportion of odd binary nulls.
                //  The thresholds here used (less than 20% nulls where you expect non-nulls, and more than
                //  60% nulls where you do expect nulls) are completely arbitrary.

                if (((oddBinaryNullsInSample * 2.0) / SampleBytes.Length) < 0.2
                    && ((evenBinaryNullsInSample * 2.0) / SampleBytes.Length) > 0.6
                    )
                    return Encoding.BigEndianUnicode;


                //3: UTF-8 - Martin Dürst outlines a method for detecting whether something CAN be UTF-8 content 
                //  using regexp, in his w3c.org unicode FAQ entry: 
                //  http://www.w3.org/International/questions/qa-forms-utf-8
                //  adapted here for C#.
                string potentiallyMangledString = Encoding.ASCII.GetString(SampleBytes);

                Regex UTF8Validator = new Regex(@"\A(" + @"[\x09\x0A\x0D\x20-\x7E]" + @"|[\xC2-\xDF][\x80-\xBF]" + @"|\xE0[\xA0-\xBF][\x80-\xBF]" + @"|[\xE1-\xEC\xEE\xEF][\x80-\xBF]{2}" + @"|\xED[\x80-\x9F][\x80-\xBF]" + @"|\xF0[\x90-\xBF][\x80-\xBF]{2}" + @"|[\xF1-\xF3][\x80-\xBF]{3}" + @"|\xF4[\x80-\x8F][\x80-\xBF]{2}" + @")*\z");
                if (UTF8Validator.IsMatch(potentiallyMangledString))
                {
                    //Unfortunately, just the fact that it CAN be UTF-8 doesn't tell you much about probabilities.
                    //If all the characters are in the 0-127 range, no harm done, most western charsets are same as UTF-8 in these ranges.
                    //If some of the characters were in the upper range (western accented characters), however, they would likely be mangled to 2-byte by the UTF-8 encoding process.
                    // So, we need to play stats.

                    // The "Random" likelihood of any pair of randomly generated characters being one 
                    //   of these "suspicious" character sequences is:
                    //     128 / (256 * 256) = 0.2%.
                    //
                    // In western text data, that is SIGNIFICANTLY reduced - most text data stays in the <127 
                    //   character range, so we assume that more than 1 in 500,000 of these character 
                    //   sequences indicates UTF-8. The number 500,000 is completely arbitrary - so sue me.
                    //
                    // We can only assume these character sequences will be rare if we ALSO assume that this
                    //   IS in fact western text - in which case the bulk of the UTF-8 encoded data (that is 
                    //   not already suspicious sequences) should be plain US-ASCII bytes. This, I 
                    //   arbitrarily decided, should be 80% (a random distribution, eg binary data, would yield 
                    //   approx 40%, so the chances of hitting this threshold by accident in random data are 
                    //   VERY low). 

                    if ((suspiciousUTF8SequenceCount * 500000.0 / SampleBytes.Length >= 1) //suspicious sequences
                        && (
                               //all suspicious, so cannot evaluate proportion of US-Ascii
                               SampleBytes.Length - suspiciousUTF8BytesTotal == 0
                               ||
                               likelyUSASCIIBytesInSample * 1.0 / (SampleBytes.Length - suspiciousUTF8BytesTotal) >= 0.8
                           )
                        )
                        return Encoding.UTF8;
                }

                return null;
            }

            private static bool IsCommonUSASCIIByte(byte testByte)
            {
                if (testByte == 0x0A //lf
                    || testByte == 0x0D //cr
                    || testByte == 0x09 //tab
                    || (testByte >= 0x20 && testByte <= 0x2F) //common punctuation
                    || (testByte >= 0x30 && testByte <= 0x39) //digits
                    || (testByte >= 0x3A && testByte <= 0x40) //common punctuation
                    || (testByte >= 0x41 && testByte <= 0x5A) //capital letters
                    || (testByte >= 0x5B && testByte <= 0x60) //common punctuation
                    || (testByte >= 0x61 && testByte <= 0x7A) //lowercase letters
                    || (testByte >= 0x7B && testByte <= 0x7E) //common punctuation
                    )
                    return true;
                else
                    return false;
            }

            private static int DetectSuspiciousUTF8SequenceLength(byte[] SampleBytes, long currentPos)
            {
                int lengthFound = 0;

                if (SampleBytes.Length >= currentPos + 1
                    && SampleBytes[currentPos] == 0xC2
                    )
                {
                    if (SampleBytes[currentPos + 1] == 0x81
                        || SampleBytes[currentPos + 1] == 0x8D
                        || SampleBytes[currentPos + 1] == 0x8F
                        )
                        lengthFound = 2;
                    else if (SampleBytes[currentPos + 1] == 0x90
                        || SampleBytes[currentPos + 1] == 0x9D
                        )
                        lengthFound = 2;
                    else if (SampleBytes[currentPos + 1] >= 0xA0
                        && SampleBytes[currentPos + 1] <= 0xBF
                        )
                        lengthFound = 2;
                }
                else if (SampleBytes.Length >= currentPos + 1
                    && SampleBytes[currentPos] == 0xC3
                    )
                {
                    if (SampleBytes[currentPos + 1] >= 0x80
                        && SampleBytes[currentPos + 1] <= 0xBF
                        )
                        lengthFound = 2;
                }
                else if (SampleBytes.Length >= currentPos + 1
                    && SampleBytes[currentPos] == 0xC5
                    )
                {
                    if (SampleBytes[currentPos + 1] == 0x92
                        || SampleBytes[currentPos + 1] == 0x93
                        )
                        lengthFound = 2;
                    else if (SampleBytes[currentPos + 1] == 0xA0
                        || SampleBytes[currentPos + 1] == 0xA1
                        )
                        lengthFound = 2;
                    else if (SampleBytes[currentPos + 1] == 0xB8
                        || SampleBytes[currentPos + 1] == 0xBD
                        || SampleBytes[currentPos + 1] == 0xBE
                        )
                        lengthFound = 2;
                }
                else if (SampleBytes.Length >= currentPos + 1
                    && SampleBytes[currentPos] == 0xC6
                    )
                {
                    if (SampleBytes[currentPos + 1] == 0x92)
                        lengthFound = 2;
                }
                else if (SampleBytes.Length >= currentPos + 1
                    && SampleBytes[currentPos] == 0xCB
                    )
                {
                    if (SampleBytes[currentPos + 1] == 0x86
                        || SampleBytes[currentPos + 1] == 0x9C
                        )
                        lengthFound = 2;
                }
                else if (SampleBytes.Length >= currentPos + 2
                    && SampleBytes[currentPos] == 0xE2
                    )
                {
                    if (SampleBytes[currentPos + 1] == 0x80)
                    {
                        if (SampleBytes[currentPos + 2] == 0x93
                            || SampleBytes[currentPos + 2] == 0x94
                            )
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0x98
                            || SampleBytes[currentPos + 2] == 0x99
                            || SampleBytes[currentPos + 2] == 0x9A
                            )
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0x9C
                            || SampleBytes[currentPos + 2] == 0x9D
                            || SampleBytes[currentPos + 2] == 0x9E
                            )
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0xA0
                            || SampleBytes[currentPos + 2] == 0xA1
                            || SampleBytes[currentPos + 2] == 0xA2
                            )
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0xA6)
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0xB0)
                            lengthFound = 3;
                        if (SampleBytes[currentPos + 2] == 0xB9
                            || SampleBytes[currentPos + 2] == 0xBA
                            )
                            lengthFound = 3;
                    }
                    else if (SampleBytes[currentPos + 1] == 0x82
                        && SampleBytes[currentPos + 2] == 0xAC
                        )
                        lengthFound = 3;
                    else if (SampleBytes[currentPos + 1] == 0x84
                        && SampleBytes[currentPos + 2] == 0xA2
                        )
                        lengthFound = 3;
                }

                return lengthFound;
            }

        }

    //Pause code
    [Flags]
    public enum ThreadAccess : int
    {
        TERMINATE = (0x0001),
        SUSPEND_RESUME = (0x0002),
        GET_CONTEXT = (0x0008),
        SET_CONTEXT = (0x0010),
        SET_INFORMATION = (0x0020),
        QUERY_INFORMATION = (0x0040),
        SET_THREAD_TOKEN = (0x0080),
        IMPERSONATE = (0x0100),
        DIRECT_IMPERSONATION = (0x0200)
    }

    public static class ProcessExtension
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);
        [DllImport("kernel32.dll")]
        static extern uint SuspendThread(IntPtr hThread);
        [DllImport("kernel32.dll")]
        static extern int ResumeThread(IntPtr hThread);

        public static void Suspend(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                SuspendThread(pOpenThread);
            }
        }
        public static void Resume(this Process process)
        {
            foreach (ProcessThread thread in process.Threads)
            {
                var pOpenThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                if (pOpenThread == IntPtr.Zero)
                {
                    break;
                }
                ResumeThread(pOpenThread);
            }
        }
    }
    //End pause code
}

