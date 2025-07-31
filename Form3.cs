﻿using FFBatch.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
//using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FFBatch
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public List<string> ff_ena = new List<string>();
        public String main_out_path = String.Empty;
        public Boolean monitor_folder = false;
        public Boolean no_dest_overwrite = false;
        public Boolean change_ff_ver = false;
        public Boolean keep_dates = false;
        public Boolean totray = false;
        public Boolean autorun = false;
        public Boolean automulti = false;
        public Boolean changed_lang = false;
        public String lang_set = "";
        public String ff_ver = String.Empty;
        private System.Media.SoundPlayer soundPl = new System.Media.SoundPlayer();
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        public Boolean delete_def;
        public Boolean delete_one;
        private String new_ver = "";
        public Boolean edit_presets = false;
        public Boolean presets_online = false;
        private Boolean playing = false;
        private RichTextBox Rtxt = new RichTextBox();
        private Form frm_settings = new Form();
        public Boolean play_end;
        public Boolean played_ok;
        public String play_file;
        public Boolean recreate;
        public Boolean check_open_c;
        public Boolean not_save_logs;
        public Boolean verbose_logs;
        public Boolean full_report;
        public Boolean ext_rep_app;
        public Boolean not_save_cache;
        public Boolean use_cache_os;
        public Boolean suffix;
        public Boolean try_preset;
        public String txt_suffix_str;
        public Boolean cancel = false;
        public String txt_preset_str;
        public String txt_format_str;
        private Boolean is_portable = false;
        public Boolean updates;
        public Boolean remember_w;        
        public Boolean sort_multi;
        public Boolean send_params_console;
        public Boolean warn_successful;
        public Boolean ignore_encoded;
        public Boolean no_warn_0;
        public Boolean reload_config;
        public Boolean subfolders;
        public Boolean to_sleep;
        public Boolean never_cache;
        public Boolean reset_asked = false;        
        public Boolean remember_tab;

        private void toolTips()
        {
            ToolTip toolT4z = new ToolTip();
            toolT4z.AutoPopDelay = 9000;
            toolT4z.InitialDelay = 750;
            toolT4z.ReshowDelay = 500;
            toolT4z.ShowAlways = true;
            toolT4z.SetToolTip(this.pic_ver, FFBatch.Properties.Strings.new_ff_a);

            ToolTip toolT5z = new ToolTip();
            toolT5z.AutoPopDelay = 9000;
            toolT5z.InitialDelay = 750;
            toolT5z.ReshowDelay = 500;
            toolT5z.ShowAlways = true;
            toolT5z.SetToolTip(this.pic_ff_ok, FFBatch.Properties.Strings.latest_ff);

            ToolTip toolT0z = new ToolTip();
            toolT0z.AutoPopDelay = 9000;
            toolT0z.InitialDelay = 750;
            toolT0z.ReshowDelay = 500;
            toolT0z.ShowAlways = true;
            toolT0z.SetToolTip(this.btn_play_sound, FFBatch.Properties.Strings.play_8);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (check_recreate.CheckState == CheckState.Checked) recreate = true;
            else recreate = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            soundPl.Stop();
            timer1.Stop();
            btn_stop_play.Visible = false;
            btn_play_sound.Visible = true;
            cancel = true;
            ActiveForm.Close();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            soundPl.Stop();
            timer1.Stop();
            btn_stop_play.Visible = false;
            btn_play_sound.Visible = true;
            if (chk_play.Checked == true)
            {
                if (play_file == null)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.play_nofile);
                    return;
                }
                if (play_file.Length == 0)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.play_nofile);
                    return;
                }
            }
       
            if (txt_monitor.Text == main_out_path)
            {               
                    MessageBox.Show(Properties.Strings.mon_path_equal);
                    return;
            }

            if (chk_monitor.Checked == true && !Directory.Exists(txt_monitor.Text))
            {
                if (txt_monitor.Text.Length == 0)
                {
                    MessageBox.Show(Properties.Strings.path_empty);
                }
                else MessageBox.Show(Properties.Strings.path_not);                

                return;
            }

            cancel = false;
            Settings.Default.Save();
            ActiveForm.Close();
        }

        public class WebClientWithTimeout : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest wr = base.GetWebRequest(address);
                wr.Timeout = 5000; // timeout in milliseconds (ms)
                return wr;
            }
        }
        public static bool is_startup()
        {
            RegistryKey winLogonKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            return (winLogonKey.GetValueNames().Contains("FFBatch"));
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            if (Settings.Default.visuals == false && Settings.Default.dark_mode == false)
            {
                foreach (var groupbox in this.Controls.OfType<GroupBox>())
                {
                    groupbox.FlatStyle = FlatStyle.System;
                    foreach (var chk in groupbox.Controls.OfType<CheckBox>())
                    {
                        chk.FlatStyle = FlatStyle.System;
                    }
                }
            }
            
            edit_presets = false;
            presets_online = false;
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            chk_visuals.Checked = !Settings.Default.visuals;
            ToolTip toolT2z = new ToolTip();
            toolT2z.AutoPopDelay = 9000;
            toolT2z.InitialDelay = 250;
            toolT2z.ReshowDelay = 500;
            toolT2z.ShowAlways = true;
            toolT2z.SetToolTip(this.chk_visuals, Properties.Strings.improve_start);

            if (Settings.Default.to_tray == false) chk_tray.Checked = false;
            else chk_tray.Checked = true;
            
            if (Settings.Default.auto_dark == true)
            {
                chk_dark.Checked = true;
                n_sunset.Enabled = true;
                n_sunrise.Enabled = true;

                n_sunset.Value = Settings.Default.dark_sunset;
                n_sunrise.Value = Settings.Default.dark_sunrise;
                if (Settings.Default.dark_os == true)
                {
                    chk_dark_win.Checked = true;
                    n_sunset.Enabled = false;
                    n_sunrise.Enabled = false;
                }
                else
                {
                    chk_dark_win.Checked = false;
                    n_sunset.Enabled = true;
                    n_sunrise.Enabled = true;
                }

            }
            else
            {
                chk_dark.Checked = false;
            }

            if (Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                Settings.Default.dark_mode = true;
                btn_dark.Image = pic_night.Image;
                btn_dark.Text = Properties.Strings.en_day;
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                Settings.Default.dark_mode = false;
                btn_dark.Image = pic_night.InitialImage;
                btn_dark.Text = Properties.Strings.en_night;
                textBox1.BackColor = SystemColors.Window;
                txt_format.BackColor = SystemColors.Window;
            }

            toolTips();
            reload_config = false;
            cancel = true;
            reset_asked = false;
            edit_presets = false;
            browse_sound.InitialDirectory = Application.StartupPath;

            //Read configuration
            if (Settings.Default.no_dest_overw == true) chk_no_overw.Checked = true;
            else chk_no_overw.Checked = false;

            if (Settings.Default.filter_zero == true) chk_filter_zero.Checked = true;
            else chk_filter_zero.Checked = false;

            if (Settings.Default.bat_level < 10) Settings.Default.bat_level = 20;
            
            n_bat_l.Value = Settings.Default.bat_level;

            if (Settings.Default.pause_bat == true)
            {
                chk_battery.Checked = true;               
            }
            else
            {
                chk_battery.Checked = false;               
            }
            if (Settings.Default.if_bat_low == true)
            {
                chk_bat_level.Checked = true;
            }
            else
            {
                chk_bat_level.Checked = false;
            }

            if (Settings.Default.no_resize == false) chk_resize.Checked = false;
            else chk_resize.Checked = true;

            if (Settings.Default.no_ctrl_p == false) chk_ctrl_p.Checked = false;
            else chk_ctrl_p.Checked = true;

            if (Settings.Default.report_ext == false) chk_ext_rep.Checked = false;
            else chk_ext_rep.Checked = true;

            if (is_startup() == false) chk_run_st.Checked = false;
            else chk_run_st.Checked = true;

            String path_s = String.Empty;
            if (is_portable == false)
            {
                path_s = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_path.ini";
            }
            else
            {
                path_s = port_path + "ff_path_portable.ini";
            }

            if (File.Exists(path_s))
            {
                String saved_path = File.ReadAllText(path_s);
                if (saved_path == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.config_err);
                    return;
                }
            }
            //Check ffbatch.ini if not found
            String path = String.Empty;
            if (is_portable == false)
            {
                path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            }
            else
            {
                path = port_path + "ff_batch_portable.ini";
            }

            int linea = 0;

            foreach (string line in File.ReadLines(path))
            {
                linea = linea + 1;

                if (line == "yes")

                {
                    check_open_output.CheckState = CheckState.Checked;
                }

                if (line == "no")
                {
                    check_open_output.CheckState = CheckState.Unchecked;
                }

                if (line == "Vn")
                {
                    chk_suffix.CheckState = CheckState.Unchecked;
                    txt_suffix.Enabled = false;
                }
                if (line.Length > 1 && line.Substring(0, 2) == "Vs")
                {
                    chk_suffix.CheckState = CheckState.Checked;
                    txt_suffix.Enabled = true;
                    txt_suffix.Text = line.Substring(3, line.Length - 3);
                }

                if (line == "grid_yes")
                {
                    checkBox1.CheckState = CheckState.Checked;
                }
                if (line == "grid_no")
                {
                    checkBox1.CheckState = CheckState.Unchecked;
                }

                if (line == "subf_yes")
                {
                    chk_subf.CheckState = CheckState.Checked;
                }
                if (line == "subf_no")
                {
                    chk_subf.CheckState = CheckState.Unchecked;
                }

                if (line == "keep_yes")
                {
                    check_recreate.CheckState = CheckState.Checked;
                }
                if (line == "keep_no")
                {
                    check_recreate.CheckState = CheckState.Unchecked;
                }

                if (linea == 1)
                {
                    textBox1.Text = line;
                }

                if (linea == 2)
                {
                    txt_format.Text = line;
                }
            }

            //End read configuration

            if (Settings.Default.no_ctrl_p == true) chk_ctrl_p.Checked = true;
            else chk_ctrl_p.Checked = false;

            if (Settings.Default.quick_queue == true) chk_quick_q.Checked = true;
            else chk_quick_q.Checked = false;

            //Read auto-saved configuration

            //Keep file dates

            String f_dates = String.Empty;
            if (is_portable == false)
            {
                f_dates = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_dates.ini";
            }
            else
            {
                f_dates = port_path + "ff_dates_portable.ini";
            }

            if (File.Exists(f_dates))
            {
                chk_dates.Checked = true;
            }
            else
            {
                chk_dates.Checked = false;
            }

            if (chk_try.CheckState == CheckState.Checked) try_preset = true;
            else try_preset = false;

            //End Keep dates

            //Disable try preset

            String f_try = String.Empty;
            if (is_portable == false)
            {
                f_try = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_try.ini";
            }
            else
            {
                f_try = port_path + "ff_try_portable.ini";
            }

            if (File.Exists(f_try))
            {
                chk_try.Checked = true;
            }
            else
            {
                chk_try.Checked = false;
            }

            if (chk_try.CheckState == CheckState.Checked) try_preset = true;
            else try_preset = false;

            //End Disable preset
                        
            //Auto updates

            String f_updates = String.Empty;
            if (is_portable == false)
            {
                f_updates = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_updates.ini";
            }
            else
            {
                f_updates = port_path + "ff_updates_portable.ini";
            }

            if (File.Exists(f_updates))
            {
                chk_auto_updates.CheckState = CheckState.Unchecked;
                updates = false;
            }

            if (!File.Exists(f_updates))
            {
                chk_auto_updates.CheckState = CheckState.Checked;
                updates = true;
            }

            //End auto updates

            //Computer to sleep

            String f_sleep = String.Empty;
            if (is_portable == false)
            {
                f_sleep = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_sleep.ini";
            }
            else
            {
                f_sleep = port_path + "ff_sleep_portable.ini";
            }

            if (File.Exists(f_sleep))
            {
                chk_sleep.CheckState = CheckState.Checked;
                to_sleep = true;
            }
            else
            {
                chk_sleep.CheckState = CheckState.Unchecked;
                to_sleep = false;
            }

            //End computer to sleep

            //Sort multi

            String f_sort_dur = String.Empty;
            if (is_portable == false)
            {
                f_sort_dur = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_sort_dur.ini";
            }
            else
            {
                f_sort_dur = port_path + "ff_sort_dur_portable.ini";
            }

            if (File.Exists(f_sort_dur))
            {
                chk_sort.CheckState = CheckState.Checked;
            }
            else
            {
                chk_sort.CheckState = CheckState.Unchecked;
            }

            //Send params to console

            String f_params_console = String.Empty;
            if (is_portable == false)
            {
                f_params_console = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_par_con.ini";
            }
            else
            {
                f_params_console = port_path + "ff_par_con_portable.ini";
            }

            if (File.Exists(f_params_console))
            {
                chk_console_params.CheckState = CheckState.Checked;
                send_params_console = false;
            }
            else
            {
                chk_console_params.CheckState = CheckState.Unchecked;
                send_params_console = true;
            }

            //End send params to console

            //Warn Ignore encoded items

            String f_ignore_enc = String.Empty;
            if (is_portable == false)
            {
                f_ignore_enc = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_ignore_enc.ini";
            }
            else
            {
                f_ignore_enc = port_path + "ff_ignore_enc_portable.ini";
            }

            if (File.Exists(f_ignore_enc))
            {
                chk_ignore_enc.CheckState = CheckState.Checked;
                ignore_encoded = true;
            }
            else
            {
                chk_warn_successful.CheckState = CheckState.Unchecked;
                ignore_encoded = false;
            }

            //End ignore encoded items

            //Warn successful items

            String f_warn_suc = String.Empty;
            if (is_portable == false)
            {
                f_warn_suc = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_warn_suc.ini";
            }
            else
            {
                f_warn_suc = port_path + "ff_warn_suc_portable.ini";
            }

            if (File.Exists(f_warn_suc))
            {
                chk_warn_successful.CheckState = CheckState.Checked;
                warn_successful = false;
            }
            else
            {
                chk_warn_successful.CheckState = CheckState.Unchecked;
                warn_successful = true;
            }

            //End warn sucessful items

            String f_delay = String.Empty;
            if (is_portable == false)
            {
                f_delay = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_delay.ini";
            }
            else
            {
                f_delay = port_path + "ff_delay_portable.ini";
            }

            if (File.Exists(f_delay))
            {
                n_delay.Value = Convert.ToInt32(File.ReadAllText(f_delay));
            }
            else n_delay.Value = 0;
            //End startup delay

            //Warn 0 duration

            String f_warn_0 = String.Empty;
            if (is_portable == false)
            {
                f_warn_0 = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_warn_0.ini";
            }
            else
            {
                f_warn_0 = port_path + "ff_warn_0_portable.ini";
            }

            if (File.Exists(f_warn_0))
            {
                chk_non0.CheckState = CheckState.Checked;
                no_warn_0 = true;
            }
            else
            {
                chk_non0.CheckState = CheckState.Unchecked;
                no_warn_0 = false;
            }

            //End warn 0 duration

            //No saving of logs

            String f_nologs = String.Empty;
            if (is_portable == false)
            {
                f_nologs = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_nologs.ini";
            }
            else
            {
                f_nologs = port_path + "ff_nologs_portable.ini";
            }

            if (File.Exists(f_nologs))
            {
                checkBox1.CheckState = CheckState.Checked;
                not_save_logs = true;
            }
            else
            {
                checkBox1.CheckState = CheckState.Unchecked;
                not_save_logs = false;
            }

            //End do not save logs

            //Verbose logs

            String f_verbose = String.Empty;
            if (is_portable == false)
            {
                f_verbose = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_verbose.ini";
            }
            else
            {
                f_verbose = port_path + "ff_verbose_portable.ini";
            }

            if (File.Exists(f_verbose))
            {
                chk_verbose_log.CheckState = CheckState.Checked;
                verbose_logs = true;
            }
            else
            {
                chk_verbose_log.CheckState = CheckState.Unchecked;
                verbose_logs = false;
            }

            //End verbose logs

            //Verbose logs

            String f_report = String.Empty;
            if (is_portable == false)
            {
                f_report = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_report.ini";
            }
            else
            {
                f_report = port_path + "ff_report_portable.ini";
            }

            if (File.Exists(f_report))
            {
                chk_full_info.CheckState = CheckState.Checked;
                full_report = true;
            }
            else
            {
                chk_full_info.CheckState = CheckState.Unchecked;
                full_report = false;
            }

            //End full report

            //Not save network cache

            String f_no_cache = String.Empty;
            if (is_portable == false)
            {
                f_no_cache = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_nocache.ini";
            }
            else
            {
                f_no_cache = port_path + "ff_nocache_portable.ini";
            }

            if (File.Exists(f_no_cache))
            {
                chk_never_cache.CheckState = CheckState.Checked;
                not_save_cache = true;
                chk_cache_dialog.Enabled = false;
            }
            else
            {
                chk_never_cache.CheckState = CheckState.Unchecked;
                not_save_cache = false;
                chk_cache_dialog.Enabled = true;
            }

            //End do not cache network files

            //Use OS cache dialog

            String f_os_cache = String.Empty;
            if (is_portable == false)
            {
                f_os_cache = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_oscache.ini";
            }
            else
            {
                f_os_cache = port_path + "ff_oscache_portable.ini";
            }

            if (File.Exists(f_os_cache))
            {
                chk_cache_dialog.CheckState = CheckState.Checked;
                use_cache_os = true;
            }
            else
            {
                chk_cache_dialog.CheckState = CheckState.Unchecked;
                use_cache_os = false;
            }

            //END use OS cache dialog

            //Remember tab

            String f_remember = String.Empty;
            if (is_portable == false)
            {
                f_remember = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_remember.ini";
            }
            else
            {
                f_remember = port_path + "ff_remember_portable.ini";
            }

            if (File.Exists(f_remember))
            {
                remember_tab = true;
                chk_remember_tab.CheckState = CheckState.Checked;
            }
            else
            {
                remember_tab = false;
                chk_remember_tab.CheckState = CheckState.Unchecked;
            }

            //END remember tab

            //Read play sound

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
                chk_play.Checked = false;
                play_file = String.Empty;
                btn_browse_play.Enabled = false;
                btn_play_sound.Enabled = false;
                play_end = false;
            }
            else
            {
                String read_play = File.ReadAllText(ff_play_sound);
                if (read_play.Length != 0)
                {
                    play_end = true;
                    play_file = read_play;
                    chk_play.Checked = true;
                    btn_browse_play.Enabled = true;
                    btn_play_sound.Enabled = true;
                }
                else
                {
                    play_end = false;
                    play_file = String.Empty;
                    chk_play.Checked = false;
                    btn_browse_play.Enabled = false;
                    btn_play_sound.Enabled = false;
                }
            }
            //End Read play sound

            //Read window position
            String f_remember_w = String.Empty;
            if (is_portable == false)
            {
                f_remember_w = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_remember_w.ini";
            }
            else
            {
                f_remember_w = port_path + "ff_remember_w_portable.ini";
            }

            if (File.Exists(f_remember_w))
            {
                chk_w_position.CheckState = CheckState.Checked;
                remember_tab = true;
            }
            else
            {
                chk_w_position.CheckState = CheckState.Unchecked;
                remember_tab = false;
            }

            //End read window position

            //Read delete definitively
            String f_delete_full = String.Empty;
            if (is_portable == false)
            {
                f_delete_full = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_delete_full.ini";
            }
            else
            {
                f_delete_full = port_path + "ff_delete_full_portable.ini";
            }

            if (File.Exists(f_delete_full))
            {
                chk_delete_def.CheckState = CheckState.Checked;
                delete_def = true;
            }
            else
            {
                chk_delete_def.CheckState = CheckState.Unchecked;
                delete_def = false;
            }

            //End delete definitively

            //Read delete one
            String f_delete_one = String.Empty;
            if (is_portable == false)
            {
                f_delete_one = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_delete_one.ini";
            }
            else
            {
                f_delete_one = port_path + "ff_delete_one_portable.ini";
            }

            if (File.Exists(f_delete_one))
            {
                chk_delete_one.CheckState = CheckState.Checked;
                delete_one = true;
            }
            else
            {
                chk_delete_one.CheckState = CheckState.Unchecked;
                delete_one = false;
            }

            //End delete one

            //Autorun settings

            String f_autorun = String.Empty;
            String f_multi = String.Empty;
            if (is_portable == false)
            {
                f_autorun = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_autorun.ini";
                f_multi = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_autorun_m.ini";
            }
            else
            {
                f_autorun = port_path + "ff_autorun_portable.ini";
                f_multi = port_path + "ff_autorun_m_portable.ini";
            }
            if (File.Exists(f_autorun))
            {
                chk_auto_start.Checked = true;
                chk_auto_multi.Enabled = true;
                if (File.Exists(f_multi)) chk_auto_multi.Checked = true;
                else chk_auto_multi.Checked = false;
            }
            else
            {
                chk_auto_start.Checked = false;
                chk_auto_multi.Enabled = false;
                try
                {
                    File.Delete(f_autorun);
                    File.Delete(f_multi);
                }
                catch
                {
                }
            }

            //End autorun settings

            if (chk_auto_start.Checked) chk_autor.Image = pic_auto_en.Image;
            else chk_autor.Image = pic_auto_dis.Image;

            if (Settings.Default.large_th == true) chk_thumb_big.Checked = true;
            else chk_thumb_big.Checked = false;

            txt_monitor.Text = Settings.Default.fd_monitored;

            if (Settings.Default.monitor_fd == true) chk_monitor.Checked = true;
            else chk_monitor.Checked = false;
            chk_w_subs.Checked = Settings.Default.mon_fd_subs;
            n_monitor.Value = Settings.Default.mon_int;

            //FFmpeg latest version

            if (ff_ver == Properties.Strings.error)
            {
                lbl_ff_latest.Text = FFBatch.Properties.Strings.error;
                pic_ver.Visible = false;
                pic_ff_ok.Visible = false;
                return;
            }
            String ff_Date = "";

            lbl_ff_latest.Text = "FFmpeg " + ff_ver;
            
            try
            {
                ff_Date = lbl_ff_ver.Text.Substring(lbl_ff_ver.Text.IndexOf(Properties.Strings.version) + Properties.Strings.version.Length, 11);
            }
            catch
            {
                Thread.Sleep(1000);
                try { ff_Date = lbl_ff_ver.Text.Substring(lbl_ff_ver.Text.IndexOf(Properties.Strings.version) + Properties.Strings.version.Length, 11); }
                catch { 
                    ff_Date = "";
                    lbl_ff_latest.Text = FFBatch.Properties.Strings.error;
                    pic_ver.Visible = false;
                    pic_ff_ok.Visible = false;
                    return;
                }
            }
            
            DateTime test = new DateTime();
            
            if (!lbl_ff_ver.Text.Contains(ff_ver) && DateTime.TryParse(ff_Date, out test) == false)
            {
                pic_ver.Visible = true;
                pic_ver.Left = lbl_ff_latest.Left + lbl_ff_latest.Text.Length + 60;
                pic_ff_ok.Visible = false;
                new_ver = ff_ver;
            }
            else
            {
                pic_ver.Visible = false;
                pic_ff_ok.Visible = true;
                pic_ff_ok.Left = lbl_ff_latest.Left + lbl_ff_latest.Text.Length + 60;
            }
            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;

        }

        private void boton_load_bck_Click(object sender, System.EventArgs e)
        {
            String path_log_backup = String.Empty;
            if (is_portable == false)
            {
                path_log_backup = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_bck.ini";
            }
            else
            {
                path_log_backup = port_path + "ff_batch_bck_portable.ini";
            }

            if (!File.Exists(path_log_backup))
            {
                MessageBox.Show(FFBatch.Properties.Strings.no_backup, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<string> conf_presets = new List<string>();
                int i = 0;
                foreach (String line in File.ReadLines(path_log_backup))
                {
                    if (i > 7) conf_presets.Add(line);
                    i = i + 1;
                }
                Rtxt.Lines = conf_presets.ToArray();
                MessageBox.Show(FFBatch.Properties.Strings.backup_suc, FFBatch.Properties.Strings.back_l, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void boton_ok_ff_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
            this.Enabled = true;
        }

        private void boton_save_ff_Click(object sender, System.EventArgs e)
        {
            String path_log_file = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            String path_log_backup = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_bck.ini";
            if (is_portable == true)
            {
                path_log_file = port_path + "ff_batch.ini";
                path_log_backup = port_path + "ff_batch_bck.ini";
            }

            try
            {
                File.Delete(path_log_backup);
                File.Copy(path_log_file, path_log_backup);
            }
            catch
            {
                var a = MessageBox.Show(FFBatch.Properties.Strings.back_cont, FFBatch.Properties.Strings.error, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (a == DialogResult.No) return;
            }
            reload_config = true;

            String[] lines = System.IO.File.ReadAllLines(path_log_file);
            List<string> conf_presets = new List<string>();
            int i = 0;
            foreach (String line in lines)
            {
                if (i < 8) conf_presets.Add(line);
                i = i + 1;
            }
            foreach (String line in Rtxt.Lines)
            {
                conf_presets.Add(line);
            }

            File.WriteAllLines(path_log_file, conf_presets.ToArray());
            Form.ActiveForm.Close();
        }

        private void CopyAction(object sender, EventArgs e)
        {
            if (Rtxt.SelectedText != String.Empty) Clipboard.SetText(Rtxt.SelectedText);
        }

        private void check_open_output_CheckedChanged(object sender, EventArgs e)
        {
            if (check_open_output.CheckState == CheckState.Checked) check_open_c = true;
            else check_open_c = false;
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                not_save_logs = true;
            }
            else not_save_logs = false;
        }

        private void chk_suffix_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_suffix.CheckState == CheckState.Checked)
            {
                suffix = true;
                txt_suffix.Enabled = true;
                txt_suffix_str = txt_suffix.Text;
            }
            else
            {
                txt_suffix.Enabled = false;
                txt_suffix.Text = "_FFB";
                suffix = false;
            }
        }

        private void txt_suffix_TextChanged(object sender, EventArgs e)
        {
            txt_suffix_str = txt_suffix.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txt_preset_str = textBox1.Text;
        }

        private void txt_format_TextChanged(object sender, EventArgs e)
        {
            txt_format_str = txt_format.Text;
        }

        private void chk_try_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_try.CheckState == CheckState.Checked) try_preset = true;
            else try_preset = false;
        }

        private void chk_auto_updates_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_auto_updates.CheckState == CheckState.Checked) updates = true;
            else updates = false;
        }
        private void chk_sort_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sort.CheckState == CheckState.Checked)
            {
                sort_multi = true;                
            }
            else
            {
                sort_multi = false;
            }
        }

        private void chk_subf_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_subf.CheckState == CheckState.Checked) subfolders = true;
            else subfolders = false;
        }

        private void chk_sleep_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_sleep.CheckState == CheckState.Checked) to_sleep = true;
            else to_sleep = false;
        }

        private void chk_console_params_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_console_params.CheckState == CheckState.Checked)
            {
                send_params_console = false;
            }
            else
            {
                send_params_console = true;
            }
        }

        private void chk_warn_successful_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_warn_successful.CheckState == CheckState.Checked) warn_successful = false;
            else warn_successful = true;
        }

        private void btn_defaults_Click(object sender, EventArgs e)
        {
            chk_resize.Checked = false;
            chk_ext_rep.Checked = false;
            chk_visuals.Checked = false;
            Settings.Default.visuals = false;
            Settings.Default.visuals_all = false;
            chk_ignore_enc.Checked = false;
            chk_monitor.Checked = false;
            chk_w_subs.Checked = false;
            chk_filter_zero.Checked = false;
            chk_delete_one.CheckState = CheckState.Checked;
            chk_delete_def.CheckState = CheckState.Unchecked;
            chk_subf.CheckState = CheckState.Checked;
            checkBox1.CheckState = CheckState.Unchecked;            
            chk_sort.CheckState = CheckState.Unchecked;
            chk_verbose_log.CheckState = CheckState.Unchecked;
            chk_console_params.CheckState = CheckState.Unchecked;
            chk_auto_updates.CheckState = CheckState.Checked;
            chk_warn_successful.CheckState = CheckState.Unchecked;
            check_open_output.CheckState = CheckState.Checked;
            check_recreate.CheckState = CheckState.Unchecked;
            chk_suffix.CheckState = CheckState.Unchecked;
            txt_suffix.Text = "_FFB";
            chk_try.CheckState = CheckState.Unchecked;
            chk_sleep.CheckState = CheckState.Unchecked;
            chk_never_cache.CheckState = CheckState.Unchecked;
            chk_cache_dialog.CheckState = CheckState.Unchecked;
            chk_remember_tab.CheckState = CheckState.Unchecked;
            chk_full_info.CheckState = CheckState.Unchecked;
            checkBox1.CheckState = CheckState.Unchecked;
            chk_play.Checked = false;
            chk_w_position.Checked = false;
            chk_non0.Checked = false;
            chk_auto_start.Checked = false;
            chk_auto_multi.Checked = false;
            chk_run_st.Checked = false;
            n_delay.Value = 0;
            chk_ctrl_p.Checked = false;
            chk_quick_q.Checked = true;
            chk_thumb_big.Checked = false;
            chk_dark.Checked = false;
            chk_battery.Checked = false;
            chk_dates.Checked = false;
            Settings.Default.dark_mode = false;
            foreach (Control c in this.Controls) UpdateColorDefault(c);
            this.BackColor = SystemColors.InactiveBorder;
            Settings.Default.dark_mode = false;
            btn_dark.Image = pic_night.InitialImage;
            btn_dark.Text = Properties.Strings.en_night;
            textBox1.BackColor = SystemColors.Window;
            txt_format.BackColor = SystemColors.Window;
            Settings.Default.Save();
        }

        private void chk_never_cache_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_never_cache.CheckState == CheckState.Checked)
            {
                not_save_cache = true;
                chk_cache_dialog.Enabled = false;
            }
            else
            {
                not_save_cache = false;
                chk_cache_dialog.Enabled = true;
            }
        }

        private void chk_never_cache_Click(object sender, EventArgs e)
        {
            if (chk_never_cache.CheckState == CheckState.Checked)
            {
                MessageBox.Show(FFBatch.Properties.Strings.cache_rec, FFBatch.Properties.Strings.cache_dis, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void chk_verbose_log_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_verbose_log.CheckState == CheckState.Checked)
            {
                verbose_logs = true;
            }
            else verbose_logs = false;
        }

        private void chk_full_info_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_full_info.CheckState == CheckState.Checked)
            {
                full_report = true;
            }
            else full_report = false;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {           
            reset_asked = true;
            cancel = false;
            ActiveForm.Close();
        }

        private void chk_remember_tab_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_remember_tab.Checked)
            {
                remember_tab = true;
            }
            else
            {
                remember_tab = false;
            }
        }

        private void btn_edit_presets_n_Click(object sender, EventArgs e)
        {
            edit_presets = true;
            cancel = false;
            this.Close();
        }

        private void chk_play_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_play.CheckState == CheckState.Checked)
            {
                play_end = true;
                btn_browse_play.Enabled = true;
                btn_play_sound.Enabled = true;
                play_file = Application.StartupPath + "\\" + "FFBatch_default.wav";
            }
            else
            {
                btn_browse_play.Enabled = false;
                btn_play_sound.Enabled = false;
                btn_play_sound.Visible = true;
                play_file = String.Empty;
                play_end = false;
                btn_stop_play.Visible = false;
                try
                {
                    soundPl.Stop();
                    timer1.Stop();
                }
                catch { }
            }
        }

        private void btn_browse_play_Click(object sender, EventArgs e)
        {
            browse_sound.Filter = Properties.Strings.Aud_wave + " | *.wav| " + Properties.Strings.all_files + " (*.*) | *.*";

            if (browse_sound.ShowDialog() == DialogResult.OK)
            {
                play_file = browse_sound.FileName;
                btn_play_sound.Enabled = true;
                browse_sound.InitialDirectory = Path.GetDirectoryName(browse_sound.FileName);
            }
        }

        private void btn_play_sound_Click(object sender, EventArgs e)
        {
            timer1.Start();
            btn_play_sound.Visible = false;
            btn_stop_play.Visible = true;
            playing = true;
            btn_play_sound.Image = img_play.Images[1];
            btn_cancel.Enabled = false;
            btn_save.Enabled = false;

            Task t = Task.Run(() =>
            {
                try
                {
                    soundPl.SoundLocation = play_file;
                    soundPl.Play();
                    played_ok = true;
                    playing = false;
                }
                catch (Exception excpt)
                {
                    played_ok = false;
                    playing = false;
                    try
                    {
                        btn_play_sound.Invoke(new MethodInvoker(delegate
                        {
                            btn_play_sound.Image = img_play.Images[0];
                        }));
                    }
                    catch { }

                    MessageBox.Show(FFBatch.Properties.Strings.err_wavs + Environment.NewLine + Environment.NewLine + excpt.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            btn_play_sound.Image = img_play.Images[0];
                            btn_cancel.Enabled = true;
                            btn_save.Enabled = true;
                        }));
                    }
                    catch { }
                }
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            soundPl.Stop();
            btn_play_sound.Visible = true;
            btn_stop_play.Visible = false;
        }

        private void btn_stop_play_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            soundPl.Stop();
            btn_cancel.Enabled = true;
            btn_save.Enabled = true;
            btn_stop_play.Visible = false;
            btn_play_sound.Visible = true;
        }

        private void chk_w_position_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_w_position.CheckState == CheckState.Checked) remember_w = true;
            else remember_w = false;
        }

        private void chk_non0_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_non0.CheckState == CheckState.Checked) no_warn_0 = true;
            else no_warn_0 = false;
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.gyan.dev/ffmpeg/builds/"); 
        }

        private void pic_ff_ok_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FFBatch.Properties.Strings.latest_ff);
        }

        private void pic_ver_Click(object sender, EventArgs e)
        {
            MessageBox.Show(FFBatch.Properties.Strings.new_ff_a + ":" + " " + new_ver, FFBatch.Properties.Strings.new_ver, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chk_delete_def_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_delete_def.CheckState == CheckState.Checked)
            {
                delete_def = true;
            }
            else
            {
                delete_def = false;
            }
        }

        private void chk_delete_one_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_delete_one.CheckState == CheckState.Checked)
            {
                delete_one = true;
            }
            else
            {
                delete_one = false;
            }
        }

        private void hide_pics()
        {
            pic_trash_set.Visible = false;
            pic_net_set.Visible = false;
        }

        private void show_pics()
        {
            pic_trash_set.Visible = true;
            pic_net_set.Visible = true;
        }
        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_lang.SelectedIndex == 0)
            {
                lang_set = "en";
                Settings.Default.app_lang = "en";
                show_pics();
            }
            if (combo_lang.SelectedIndex == 1)
            {
                lang_set = "es";
                Settings.Default.app_lang = "es";
                show_pics();
            }

            if (combo_lang.SelectedIndex == 2)
            {
                lang_set = "fr";
                Settings.Default.app_lang = "fr";
                hide_pics();
            }

            if (combo_lang.SelectedIndex == 3)
            {
                lang_set = "it";
                Settings.Default.app_lang = "it";
                hide_pics();
            }
                        
            if (combo_lang.SelectedIndex == 4)
            {
                lang_set = "pl";
                Settings.Default.app_lang = "pl";
                hide_pics();

            }

            if (combo_lang.SelectedIndex == 5)
            {
                lang_set = "pt-BR";
                Settings.Default.app_lang = "pt-BR";
                hide_pics();

            }

            if (combo_lang.SelectedIndex == 6)
            {
                lang_set = "zh-Hans";
                Settings.Default.app_lang = "zh-Hans";
                show_pics();
            }
            if (combo_lang.SelectedIndex == 7)
            {
                lang_set = "ar-EG";
                Settings.Default.app_lang = "ar-EG";
                show_pics();
            }            

            Settings.Default.Save();
            refresh_lang();
            this.Text = FFBatch.Properties.Strings.Settings;            
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            RefreshResources(this, resources);
            changed_lang = true;
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void chk_auto_start_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_auto_start.CheckState == CheckState.Checked)
            {
                autorun = true;
                chk_auto_multi.Enabled = true;
            }
            else
            {
                autorun = false;
                chk_auto_multi.Enabled = false;
            }
            if (chk_auto_start.Checked) chk_autor.Image = pic_auto_en.Image;
            else chk_autor.Image = pic_auto_dis.Image;
        }

        private void chk_auto_multi_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_auto_multi.CheckState == CheckState.Checked)
            {
                automulti = true;
            }
            else
            {
                automulti = false;
            }
        }

        private void chk_autor_Click(object sender, EventArgs e)
        {
            chk_auto_start.Checked = !chk_auto_start.Checked;
            if (chk_auto_start.Checked) chk_autor.Image = pic_auto_en.Image;
            else chk_autor.Image = pic_auto_dis.Image;
        }

        private void pic_earth_Click(object sender, EventArgs e)
        {
            combo_lang.DroppedDown = true;
        }

        private void btn_add_ex_Click(object sender, EventArgs e)
        {
            Form25 frm25 = new Form25();
            frm25.btn_close.Image = btn_cancel.Image;
            this.Cursor = Cursors.WaitCursor;
            frm25.ShowDialog();
            this.Cursor = Cursors.Default;            
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            if (Settings.Default.app_lang == "zh-Hans") this.Height = this.Height + 55;
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

        private void btn_dark_Click(object sender, EventArgs e)
        {
            Settings.Default.dark_mode = !Settings.Default.dark_mode;
            if (Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                Settings.Default.dark_mode = true;
                btn_dark.Image = pic_night.Image;
                btn_dark.Text = Properties.Strings.en_day;
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                Settings.Default.dark_mode = false;
                btn_dark.Image = pic_night.InitialImage;
                btn_dark.Text = Properties.Strings.en_night;
                textBox1.BackColor = SystemColors.Window;
                txt_format.BackColor = SystemColors.Window;
            }
            Settings.Default.Save();
        }

        private void Form3_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (cancel == true)
            {
                Settings.Default.dark_mode = !Settings.Default.dark_mode;
                Settings.Default.Save();
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            presets_online = true;
            cancel = false;
            this.Close();            
        }

        private void chk_sort_Click(object sender, EventArgs e)
        {
            if (chk_sort.CheckState == CheckState.Checked)
            {
                sort_multi = true;
                MessageBox.Show(Properties.Strings.warn_sort);
            }
        }

        private void chk_ignore_enc_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ignore_enc.CheckState == CheckState.Checked) ignore_encoded = true;
            else ignore_encoded = false;
        }

        private void chk_run_st_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey rk = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chk_run_st.CheckState == CheckState.Unchecked) rk.DeleteValue("FFBatch", false);            
            else rk.SetValue("FFBatch", Application.ExecutablePath);
        }

        private void chk_run_st_Click(object sender, EventArgs e)
        {
            if (chk_run_st.Checked)
            {
                chk_warn_successful.Checked = true;
                chk_ignore_enc.Checked = true;
                chk_non0.Checked = true;
                MessageBox.Show(Properties.Strings.warn_run_enc);
            }
        }

        private void n_delay_ValueChanged(object sender, EventArgs e)
        {
            String f_delay = String.Empty;
            if (is_portable == false)
            {
                f_delay = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_delay.ini";
            }
            else
            {
                f_delay = port_path + "ff_delay_portable.ini";
            }
                        
            try
            {
                if (n_delay.Value != 0) File.WriteAllText(f_delay, n_delay.Value.ToString());
                else File.Delete(f_delay);

            } catch { }            
        }

        private void chk_tray_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_tray.CheckState == CheckState.Checked) totray = true;
            else totray = false;
        }

        private void ckk_dates_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_dates.CheckState == CheckState.Checked) keep_dates = true;
            else keep_dates = false;
        }

        private void btn_add_sec_Click(object sender, EventArgs e)
        {
            btn_add_ex.PerformClick();
        }

        private void chk_quick_q_Click(object sender, EventArgs e)
        {
            if (chk_quick_q.Checked) MessageBox.Show(Properties.Strings.quick_f_m + Environment.NewLine + Environment.NewLine + Properties.Strings.quick_f_m2 + Environment.NewLine + Environment.NewLine + Properties.Strings.quick_f_m4, Properties.Strings.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chk_dark_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_dark.CheckState == CheckState.Checked)
            {
                chk_dark_win.Enabled = true;
                n_sunset.Enabled = true;
                n_sunrise.Enabled = true;
            }
            else
            {
                chk_dark_win.Enabled = false;
                chk_dark_win.Checked = false;
                n_sunset.Enabled = false;
                n_sunrise.Enabled = false;
            }
        }

        private void chk_dark_win_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_dark_win.Checked)
            {
                n_sunset.Enabled = false;
                n_sunrise.Enabled = false;                
            }
            else
            {
                n_sunset.Enabled = true;
                n_sunrise.Enabled = true;
                Settings.Default.dark_os = false;
            }
        }

        private void chk_battery_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_battery.Checked == true)
            {
                chk_bat_level.Enabled = true;
                n_bat_l.Enabled = true;
            }
            else
            {
                chk_bat_level.Enabled = false;
                n_bat_l.Enabled = false;
            }
        }

        private void n_bat_l_ValueChanged(object sender, EventArgs e)
        {
            Settings.Default.bat_level = n_bat_l.Value;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            change_ff_ver = true;
            cancel = false;
            this.Close();
        }

        private void chk_no_overw_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_no_overw.CheckState == CheckState.Checked) no_dest_overwrite = true;
            else no_dest_overwrite = false;            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_monitor.CheckState == CheckState.Checked)
            {
                monitor_folder = true;
                txt_monitor.Enabled = true;
                btn_br_mon.Enabled = true;
                chk_w_subs.Enabled = true;
                n_monitor.Enabled = true;
                chk_warn_successful.Checked = true;
                chk_ignore_enc.Checked = true;
                chk_non0.Checked = true;
                chk_no_overw.Checked = true;
            }
            else
            {
                monitor_folder = false;
                txt_monitor.Enabled = false;
                btn_br_mon.Enabled = false;
                chk_w_subs.Enabled = false;
                n_monitor.Enabled = false;
            }
        }

        private void btn_br_mon_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdd = new FolderBrowserDialog();
            fdd.RootFolder = Environment.SpecialFolder.Desktop;
            if (fdd.ShowDialog() == DialogResult.OK)
            {
                txt_monitor.Text = fdd.SelectedPath;
            }

        }

        private void btn_excl_Click(object sender, EventArgs e)
        {
            Form32 frm32 = new Form32();
            frm32.ShowDialog();
            Settings.Default.excl_list.Clear();
            foreach (DataGridViewRow row in frm32.dg_pr.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    if (row.Cells[0].Value.ToString().Length > 0) Settings.Default.excl_list.Add(row.Cells[0].Value.ToString());
                }
            }
        }

        private void show_libs()
        {
            Form frmInfo = new Form();
            frmInfo.Name = "FFmpeg " + FFBatch.Properties.Strings.libr;
            frmInfo.Text = "FFmpeg " + FFBatch.Properties.Strings.libr;
            frmInfo.Icon = this.Icon;
            frmInfo.Height = 478;
            frmInfo.Width = 366;
            frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmInfo.MaximizeBox = false;
            frmInfo.MinimizeBox = false;
            frmInfo.BackColor = this.BackColor;

            ListView lstv = new ListView();
            lstv.Columns.Add(Properties.Strings.libr, 80);
            lstv.Columns.Add(Properties.Strings.information, 180);
            lstv.Parent = frmInfo;
            lstv.View = View.Details;
            lstv.Top = 30;
            lstv.Left = 30;
            lstv.Height = 378;
            lstv.Width = 290;


            foreach (String str in ff_ena)
            {
                ListViewItem item = new ListViewItem(str);
                if (str.Contains("amf")) item.SubItems.Add("AMD Hardware encoder");
                else if (str.Contains("nvenc")) item.SubItems.Add("NVIDIA Hardware encoder");
                else if (str.Contains("nvdec")) item.SubItems.Add("NVIDIA Hardware decoder");
                else if (str.Contains("cuvid")) item.SubItems.Add("NVIDIA");
                else if (str.Contains("cuda")) item.SubItems.Add("NVIDIA");
                else if (str.Contains("libopus")) item.SubItems.Add("OPUS audio codec");
                else if (str.Contains("libaom")) item.SubItems.Add("AV1 AOMedia video codec");
                else if (str.Contains("libvorbis")) item.SubItems.Add("VORBIS audio codec");
                else if (str.Contains("libmp3lame")) item.SubItems.Add("MP3 audio codec");
                else if (str.Contains("libtheora")) item.SubItems.Add("THEORA video codec");
                else if (str.Contains("libvpx")) item.SubItems.Add("VPx video codec");
                else if (str.Contains("libx264")) item.SubItems.Add("AVC H264 video encoder");
                else if (str.Contains("libx265")) item.SubItems.Add("HEVC H265 video encder");
                else if (str.Contains("libxvid")) item.SubItems.Add("XVID video encoder");
                else if (str.Contains("librav1e")) item.SubItems.Add("AV1 video encoder");
                else if (str.Contains("libdav1d")) item.SubItems.Add("AV1 video decoder");
                else if (str.Contains("libvvenc")) item.SubItems.Add("Versatile Video H266 encoder");

                else item.SubItems.Add("-");
                lstv.Items.Add(item);
            }

            lstv.GridLines = true;
            lstv.Sorting = SortOrder.Ascending;
            lstv.Sort();

            frmInfo.StartPosition = FormStartPosition.CenterParent;
            frmInfo.ShowDialog();
        }

        private void lbl_ff_ver_Click(object sender, EventArgs e)
        {
            show_libs();
        }

        private void pic_info2_Click(object sender, EventArgs e)
        {
            show_libs();
        }

        private void chk_cache_dialog_Click(object sender, EventArgs e)
        {
            if (chk_cache_dialog.CheckState == CheckState.Checked)
            {
                use_cache_os = true;
                MessageBox.Show(Strings.cache_os, FFBatch.Properties.Strings.cache_os2, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                use_cache_os = false;
            }
        }

        private void chk_visuals_Click(object sender, EventArgs e)
        {
            Settings.Default.visuals_all = false;
            
            if (chk_visuals.Checked == true)
            {
                Settings.Default.visuals = false;                

            }
            else
            {
                Settings.Default.visuals = true;                
            }            
            Settings.Default.Save();
        }

        private void chk_ext_rep_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_ext_rep.CheckState == CheckState.Checked)
            {
                ext_rep_app = true;
            }
            else ext_rep_app = false;
        }
    }
}
