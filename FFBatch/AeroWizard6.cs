using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Design;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FFBatch
{
    public partial class AeroWizard6 : Form
    {
        public AeroWizard6()
        {
            InitializeComponent();
        }

        int ch_count = 0;
        public List<string[]> list_chaps_w = new List<string[]>();
        public int startpage = 0;
        public Boolean chaps_file = false;
        public String dest_chaps = String.Empty;
        public Boolean by_int_chaps = false;
        public Boolean manual_chaps = false;
        public List<string> list_files = new List<string>();        
        public Boolean save_preset = false;
        public int list_count = 0;
        private String pr_1st_params = String.Empty;
        private String out_path = "";
        private String out_format = "";
        private String jpg_q = "";
        public Boolean canceled = false;
        public Boolean start_enc = false;
        private List<String[]> list_chaps = new List<String[]>();

        private Boolean ok_images = false;

        public String pr1_first_params
        {
            get { return pr_1st_params; }
            set { pr_1st_params = value; }
        }

        public Boolean wiz_ok_images
        {
            get { return ok_images; }
            set { ok_images = value; }
        }

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            combo_Seconds.SelectedIndex = 0;
            combo_ext.SelectedIndex = 0;
        }

        private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            canceled = true;
        }

        private void wz1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            canceled = false;
            if (combo_ext.Text == String.Empty)
            {
                MessageBox.Show(FFBatch.Properties.Strings.out_blank);
                e.Cancel = true;
                return;
            }
            if (radio_absolute.Checked == true && txt_path.Text.Length == 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings.abs_blank);
                e.Cancel = true;
                return;
            }

            //Output path
            if (radio_relative.Checked == true)
            {
                out_path = "%fp" + "\\" + txt_path_main.Text.Substring(2, txt_path_main.Text.Length - 2) + "_%fn" + "\\";
            }
            if (radio_absolute.Checked == true)
            {
                out_path = txt_path.Text + "\\";
            }
            if (chk_out_name.Checked == true)
            {
                out_path = out_path + "%fn_%04d";
            }
            if (chk_out_name.Checked == false)
            {
                out_path = out_path + txt_naming + "_%04d";
            }

            out_path = out_path + "." + combo_ext.Text;

            //End
            String strcopy = "";
            chk_save_preset.Enabled = true;
            txt_preset_name.Enabled = true;
            if (chk_streamcopy.Checked == true) strcopy = " -c copy ";
            pr_1st_params = "-f segment -segment_time " + combo_Seconds.Text + " " + "-reset_timestamps 1 ";
            pr_1st_params = pr_1st_params + strcopy + "-map 0 " + "\u0022" + out_path + "\u0022";
            if (radio_size.Checked == true)
            {
                pr_1st_params = "-f segment -segment_time" + " (segment_size) " + "-reset_timestamps 1 ";
                pr_1st_params = pr_1st_params + strcopy + "-map 0 " + "\u0022" + out_path + "\u0022";
                chk_save_preset.Checked = false;
                chk_save_preset.Enabled = false;
            }
        }

        private void txt_path_main_TextChanged(object sender, EventArgs e)
        {
            if (txt_path_main.TextLength >= 3 && txt_path_main.Text.Substring(0, 2) != ".\\") MessageBox.Show(Properties.Strings.pls_include + ".\\ " + Properties.Strings.on_rel_path);
        }

        private void combo_Seconds_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(combo_Seconds.Text, "[^0-9]"))
            {
                MessageBox.Show(FFBatch.Properties.Strings.only_numbers);
                combo_Seconds.Text = combo_Seconds.Text.Remove(combo_Seconds.Text.Length - 1);
            }
            radio_time.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fd1.Description = FFBatch.Properties.Strings.sel_out_f;
            fd1.ShowNewFolderButton = true;
            fd1.ShowDialog();
            if (fd1.SelectedPath.Length > 0) txt_path.Text = fd1.SelectedPath;
            else radio_relative.Checked = true;
        }

        private void radio_absolute_CheckedChanged(object sender, EventArgs e)
        {
            txt_path.Enabled = true;
            txt_path_main.Enabled = false;
            btn_browse.Enabled = true;
        }

        private void radio_relative_CheckedChanged(object sender, EventArgs e)
        {
            txt_path.Enabled = false;
            txt_path_main.Enabled = true;
            btn_browse.Enabled = false;
        }

        private void chk_save_pres_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chk_save_pres.CheckState == CheckState.Checked)
            {
                txt_preset_name.Enabled = true;
                txt_preset_name.Focus();
                save_preset = true;
            }
            else
            {
                txt_preset_name.Enabled = false;
                save_preset = false;
            }
        }

        private void chk_save_preset_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_save_preset.CheckState == CheckState.Checked)
            {
                txt_preset_name.Enabled = true;
                txt_preset_name.Focus();
                save_preset = true;
            }
            else
            {
                txt_preset_name.Enabled = false;
                save_preset = false;
            }
        }

        private void chk_out_name_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_out_name.Checked == false)
            {
                DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.abs_overw, FFBatch.Properties.Strings.warning, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (a == DialogResult.Cancel) chk_out_name.Checked = true;
                else txt_naming.Enabled = true;
            }
            else txt_naming.Enabled = false;
        }

        private void wz_end_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            start_enc = false;
            if (chk_save_preset.Checked == true) save_preset = true;
            if (chk_save_preset.Checked == true && txt_preset_name.Text.Length < 5)
            {
                MessageBox.Show(FFBatch.Properties.Strings.name_5);
                e.Cancel = true;
            }

            pr1_first_params = pr_1st_params;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            start_enc = true;
            if (chk_save_preset.Checked == true) save_preset = true;
            if (chk_save_preset.Checked == true && txt_preset_name.Text.Length < 5)
            {
                MessageBox.Show(FFBatch.Properties.Strings.name_5);
                return;
            }
            pr1_first_params = pr_1st_params;
            this.Close();
        }

        private void wz_end_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            pr_1st_params = "";
            out_path = "";
            out_format = "";
        }

        private void wz_end_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            if (list_count > 0)
            {
                btn_Start.Enabled = true;
                label9.Visible = true;
            }
            else
            {
                btn_Start.Enabled = false;
                label9.Visible = false;
            }
        }

        private void AeroWizard6_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (Properties.Settings.Default.app_lang != "en" && Properties.Settings.Default.app_lang != "es")
            {
                wiz_split.NextButtonText = Properties.Strings.next;
                wiz_split.CancelButtonText = Properties.Strings.cancel;
                wiz_split.FinishButtonText = Properties.Strings.finish;
            }
            if (startpage == 4)
            {
                wiz_split.NextPage(wiz_split.Pages[4]);
                wiz_split.Pages[0].Suppress = true;
                wiz_split.Pages[1].Suppress = true;
                wiz_split.Pages[2].Suppress = true;
                wiz_split.Pages[3].Suppress = true;
            }
            
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard6));
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

        private void combo_Seconds_SelectedIndexChanged(object sender, EventArgs e)
        {
            radio_time.Checked = true;
        }

        private void combo_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            radio_size.Checked = true;
        }

        private void combo_size_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(combo_size.Text, "[^0-9]"))
            {
                MessageBox.Show(FFBatch.Properties.Strings.only_numbers);
                combo_size.Text = combo_size.Text.Remove(combo_size.Text.Length - 1);
            }
            radio_size.Checked = true;
        }

        private void wz0_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (radio_time_size.Checked)
            {
                wz0.NextPage = wiz_split.Pages[2];
            }
            else if (radio_chapters.Checked)
            {
                wz0.NextPage = wiz_split.Pages[1];
            }
            else wz0.NextPage = wiz_split.Pages[4];
        }

        private String safe_out_ffname(String outf)
        {
            outf = outf.Replace("/", "_");
            outf = outf.Replace(":", "_");
            outf = outf.Replace("*", "_");
            outf = outf.Replace("?", "_");
            outf = outf.Replace("¿", "_");
            outf = outf.Replace("@", "_");
            outf = outf.Replace("\u0022", "_");
            outf = outf.Replace("<", "_");
            outf = outf.Replace(">", "_");
            outf = outf.Replace("|", "_");
            outf = outf.Replace(";", "_");
            outf = outf.Replace("\\", "_");
            outf = outf.Replace("(", "_");
            outf = outf.Replace(")", "_");
            return outf;
        }

        private void get_chapters_w()
        {
            Task t = Task.Run(() =>
             {                
            foreach (String lv1_it in list_files)
            {
                String ff_frames = String.Empty;
                int f_l = Path.GetFileName(lv1_it).Length;
                String dots = String.Empty;
                if (f_l > 64) { f_l = 64; dots = ".."; }
                Process get_chap = new Process();
                String args = " -an -vn -sn -f ffmetadata ";
                String output = Path.Combine(Path.GetTempPath(), "FFBatch_test") + "\\" + ch_count.ToString() + "_" + Path.GetFileNameWithoutExtension(safe_out_ffname(lv1_it)) + ".txt";
                get_chap.StartInfo.FileName = System.IO.Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
                get_chap.StartInfo.Arguments = "-i " + '\u0022' + lv1_it + '\u0022' + args + " -y " + '\u0022' + output + '\u0022';
                get_chap.StartInfo.CreateNoWindow = true;
                get_chap.EnableRaisingEvents = true;
                get_chap.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                get_chap.Start();
                get_chap.WaitForExit();
                ch_count++;
                String chaps_file = String.Empty;

                if (File.Exists(output)) chaps_file = File.ReadAllText(output);
                else
                {
                         try
                         {
                             txt_chaps.Invoke(new MethodInvoker(delegate
                        {
                            txt_chaps.AppendText(ch_count.ToString() + ". " + Path.GetFileName(lv1_it).Substring(0, f_l) + dots + " | 0 " + Properties.Strings.chapters + Environment.NewLine);
                            return;
                        }));
                        
                         } catch { }
                }

                Boolean titles = false;

                if (chaps_file.Contains("title=")) titles = true;

                String[] chaps = chaps_file.Split(new[] { "[CHAPTER]", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
                String tb = String.Empty;
                Double tbd = 0;
                String tst = String.Empty;
                Double tstd = 0;
                String tstend = String.Empty;
                Double tsendd = 0;
                String tsttit = String.Empty;
                list_chaps_w.Clear();


                if (titles == true)
                {
                    foreach (String chap in chaps)
                    {
                        if (chap.Contains("TIMEBASE=1/"))
                        {
                            int tbase = chap.LastIndexOf("TIMEBASE=1/");
                            tb = chap.Substring(tbase + 11, (chap.Length - tbase - 11));
                            tbd = Convert.ToDouble(tb);
                            //MessageBox.Show("Base: " + tb);
                        }
                        if (chap.Contains("START="))
                        {
                            int tstart = chap.LastIndexOf("START=");
                            tst = chap.Substring(tstart + 6, chap.Length - tstart - 6);
                            tstd = Convert.ToDouble(tst);
                        }
                        if (chap.Contains("END="))
                        {
                            int tsend = chap.LastIndexOf("END=");
                            tstend = chap.Substring(tsend + 4, chap.Length - tsend - 4);
                            tsendd = Convert.ToDouble(tstend);
                        }

                        if (chap.Contains("title="))
                        {
                            int tstitle = chap.LastIndexOf("title=");
                            tsttit = chap.Substring(tstitle + 6, chap.Length - tstitle - 6);
                            String[] sstt = new String[] { "-ss " + (tstd / tbd).ToString().Replace(",", "."), "-to " + (tsendd / tbd).ToString().Replace(",", "."), tsttit };
                            list_chaps_w.Add(sstt);
                        }
                    }
                }
                else
                {
                    foreach (String chap in chaps)
                    {
                        if (chap.Contains("TIMEBASE=1/"))
                        {
                            int tbase = chap.LastIndexOf("TIMEBASE=1/");
                            tb = chap.Substring(tbase + 11, (chap.Length - tbase - 11));
                            tbd = Convert.ToDouble(tb);
                            //MessageBox.Show("Base: " + tb);
                        }
                        if (chap.Contains("START="))
                        {
                            int tstart = chap.LastIndexOf("START=");
                            tst = chap.Substring(tstart + 6, chap.Length - tstart - 6);
                            tstd = Convert.ToDouble(tst);
                        }
                        if (chap.Contains("END="))
                        {
                            int tsend = chap.LastIndexOf("END=");
                            tstend = chap.Substring(tsend + 4, chap.Length - tsend - 4);
                            tsendd = Convert.ToDouble(tstend);
                            String[] sstt = new String[] { "-ss " + (tstd / tbd).ToString().Replace(",", "."), "-to " + (tsendd / tbd).ToString().Replace(",", "."), tsttit };
                            list_chaps_w.Add(sstt);
                        }
                    }
                }

                     try
                     {
                         txt_chaps.Invoke(new MethodInvoker(delegate
                    {
                        txt_chaps.AppendText(ch_count.ToString() + ". " + Path.GetFileName(lv1_it).Substring(0, f_l) + dots + " | " + list_chaps_w.Count.ToString() + " " + Properties.Strings.chapters + Environment.NewLine);
                    }));
                     }
             catch { }
                 }
             });
        }

        private void radio_by_chap_CheckedChanged(object sender, EventArgs e)
        {
            txt_chaps.ReadOnly = true;
            wiz_split.Pages[1].AllowNext = true;            
            if (radio_by_chap.Checked)
            {
                txt_chaps.Clear();                
                ch_count = 0;
                btn_load_chap.Enabled = false;
                get_chapters_w();                
            }
            else
            {
                if (txt_chaps.Text == Properties.Strings.file_big) wiz_split.Pages[1].AllowNext = false;                
                btn_load_chap.Enabled = true;                
            }
        }

        private void btn_load_chap_Click(object sender, EventArgs e)
        {
            OpenFileDialog browse_file = new OpenFileDialog();
            String file_path = String.Empty;
            browse_file.Filter = "*.txt |*.txt";
            if (browse_file.ShowDialog() == DialogResult.OK)
            {
                file_path = browse_file.FileName;
                long size = new FileInfo(file_path).Length;
                if (size > 1000000)
                {
                    txt_chaps.Text = Properties.Strings.file_big;
                    wiz_split.Pages[1].AllowNext = false;
                    return;
                }
                else
                {

                    try
                    {
                        txt_chaps.Text = File.ReadAllText(file_path);
                        wiz_split.Pages[1].AllowNext = true;
                    }
                    catch
                    {

                    }
                }
            } 
        }
        
        private void wz01_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (radio_by_chap.Checked) by_int_chaps = true;
            if (radio_chap_man.Checked)
            {
                if (list_count > 1)
                {
                    DialogResult a = MessageBox.Show(Properties.Strings.chapters_all, Properties.Strings.question, MessageBoxButtons.OKCancel);
                    if (a == DialogResult.Cancel)
                    {
                        e.Cancel = true;
                    }
                }                        
                manual_chaps = true;
            }
        }

        private void wz01_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            by_int_chaps = false;
            manual_chaps = false;
            ch_count = 0;
            txt_chaps.Clear();            
            if (radio_by_chap.Checked)
            {   
             get_chapters_w();             
            }
        }

        private void radio_chap_man_CheckedChanged(object sender, EventArgs e)
        {
            txt_chaps.ReadOnly = false;
            txt_chaps.Clear();            
            txt_chaps.Text = "-ss 00:00:00.000 -to 00:00:01.500";
            ch_count = 0;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (txt_chaps_tosave.Lines.Count() < 1) return;
            
            list_chaps.Clear();
            Double tstd = 0;
            Double tsendd = 0;
            String chap_name = "";            
                        
            foreach (String str in txt_chaps_tosave.Text.Split(new[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    TimeSpan test = new TimeSpan();
                    String ss = str.Substring(0, str.IndexOf(" | "));
                    if (TimeSpan.TryParse(ss, out test)) tstd = TimeSpan.Parse(ss).TotalMilliseconds;
                    else
                    {
                        MessageBox.Show(Properties.Strings.time_bad, Properties.Strings.error, MessageBoxButtons.OK);
                        return;
                    }
                    String to = str.Substring(str.IndexOf(" | ") + 3, 12);

                    if (TimeSpan.TryParse(to, out test)) tsendd = TimeSpan.Parse(to).TotalMilliseconds;
                    else
                    {
                        MessageBox.Show(Properties.Strings.time_bad, Properties.Strings.error, MessageBoxButtons.OK);
                        return;
                    }
                    chap_name = str.Substring(str.LastIndexOf(" | ") + 4, str.Length - str.LastIndexOf(" | ") - 4);
                    String[] chaps = new String[] { tstd.ToString(), tsendd.ToString(), chap_name };
                    list_chaps.Add(chaps);
                }
                catch
                {
                    MessageBox.Show(Properties.Strings.time_bad, Properties.Strings.error, MessageBoxButtons.OK);
                    return;
                }
            }
            
            dest_chaps = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "*.txt" + " |*.txt| " + Properties.Strings.all_files + " " + "(*.*) | *.*";
            saveFile.DefaultExt = "txt";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                dest_chaps = saveFile.FileName;

                File.WriteAllText(dest_chaps, ";FFMETADATA1" + Environment.NewLine);
                foreach (String[] str in list_chaps)
                {
                    String toadd = "[CHAPTER]" + Environment.NewLine + "TIMEBASE=1/1000" + Environment.NewLine + "START=" + str[0].Replace("-ss ", "") + Environment.NewLine + "END=" + str[1].Replace("-to ", "") + Environment.NewLine + "title=" + str[2];
                    File.AppendAllText(dest_chaps, Environment.NewLine + toadd + Environment.NewLine);
                }
                chaps_file = true;
                this.Close();
            }
        }

        private void btn_add_chap_Click(object sender, EventArgs e)
        {
            TimeSpan test = new TimeSpan();
            if (!TimeSpan.TryParse(txt_init.Text, out test) || !TimeSpan.TryParse(txt_end.Text, out test))
            {
                MessageBox.Show(Properties.Strings.time_bad, Properties.Strings.error, MessageBoxButtons.OK);
                return;
            }
            else
            {
                if (TimeSpan.Parse(txt_end.Text).TotalMilliseconds < TimeSpan.Parse(txt_init.Text).TotalMilliseconds)
                {
                    MessageBox.Show(Properties.Strings.time_wrong, Properties.Strings.error, MessageBoxButtons.OK);
                    return;
                }
                if (TimeSpan.Parse(txt_end.Text).TotalMilliseconds == TimeSpan.Parse(txt_init.Text).TotalMilliseconds)
                {
                    MessageBox.Show(Properties.Strings.time_wrong, Properties.Strings.error, MessageBoxButtons.OK);
                    return;
                }
            }
            
            if (txt_chaps_tosave.Text.Length == 0) txt_chaps_tosave.Text = txt_init.Text + " | " + txt_end.Text + " | " + txt_chapt.Text;
            else txt_chaps_tosave.Text = txt_chaps_tosave.Text + Environment.NewLine + txt_init.Text + " | " + txt_end.Text + " | " + txt_chapt.Text;
            txt_init.Text = txt_end.Text;
        }

        private void btn_clear_chaps_Click(object sender, EventArgs e)
        {
            txt_chaps_tosave.Clear();
        }

        private void txt_init_TextChanged(object sender, EventArgs e)
        {
            txt_init.BackColor = SystemColors.Window;
            TimeSpan test = new TimeSpan();
            if (!TimeSpan.TryParse(txt_init.Text, out test))
            {
                txt_init.BackColor = Color.LightSalmon;
            }
        }

        private void txt_end_TextChanged(object sender, EventArgs e)
        {
            txt_end.BackColor = SystemColors.Window;
            TimeSpan test = new TimeSpan();
            if (!TimeSpan.TryParse(txt_end.Text, out test))
            {
                txt_end.BackColor = Color.LightSalmon;
            }
        }
    }
}