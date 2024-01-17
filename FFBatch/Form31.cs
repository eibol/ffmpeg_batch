using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace FFBatch
{
    public partial class Form31 : Form
    {
        public String variab;
        public String file;
        public String dur;
        public Boolean canceled = false;
        private List<string[]> list_chaps_w = new List<string[]>();
        public Form31()
        {
            InitializeComponent();
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }
        private void refresh_lang()
        {            
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form31));
            RefreshResources(this, resources);
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
            
            foreach (Control subC in Controls.OfType<System.Windows.Forms.GroupBox>())
            {
                foreach (Control subC2 in subC.Controls.OfType<System.Windows.Forms.TextBox>())
                {
                    { subC2.BackColor = SystemColors.Window; }
                }                
            }
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

        private void get_chapters()
        {
            String ff_frames = String.Empty;
            Process get_chap = new Process();
            String args = " -an -vn -sn -f ffmetadata ";
            
            String output = Path.Combine(Path.GetTempPath(), "FFBatch_test") + "\\" + Path.GetFileNameWithoutExtension(safe_out_ffname(file)) + "_chapters" + ".txt";
            get_chap.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
            get_chap.StartInfo.Arguments = "-i " + '\u0022' + file + '\u0022' + args + " -y " + '\u0022' + output + '\u0022';
            get_chap.StartInfo.CreateNoWindow = true;
            get_chap.EnableRaisingEvents = true;
            get_chap.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            get_chap.Start();
            get_chap.WaitForExit();
            String chaps_file = String.Empty;

            if (File.Exists(output)) chaps_file = File.ReadAllText(output);
            else
            {
                txt_pre.Text = Properties.Strings.no_chaps;
                return;
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

            txt_pre.Text = list_chaps_w.Count.ToString() + " " + Properties.Strings.chaps_f;
        }

        private void clean_gp1()
        {
            foreach (Control c in groupBox1.Controls)
            {
                if (c is RadioButton) ((RadioButton)c).Checked = false;
                if (c is CheckBox) ((CheckBox)c).Checked = false;
            }
        }

        private void clean_gp2()
        {
            foreach (Control c in groupBox2.Controls)
            {
                if (c is RadioButton) ((RadioButton)c).Checked = false;                
            }
        }

        private void clean_gp3()
        {
            foreach (Control c in groupBox3.Controls)
            {
                if (c is RadioButton) ((RadioButton)c).Checked = false;                
            }
        }

        private void Form31_Load(object sender, EventArgs e)
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

            refresh_lang();
            variab = "";
        }

        private Boolean selected()
        {
            foreach (var pb in this.Controls.OfType<GroupBox>())
            {
                foreach (var pb2 in pb.Controls.OfType<RadioButton>())
                {
                    if (pb2.Checked == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void set_var()
        {
            if (radio_inputfn.Checked == true)
            {
                if (chk_full1.Checked == true) variab = "%f1";
                else variab = "%1";
            }
            if (radio_input_fn_noext.Checked == true)
            {
                if (chk_full2.Checked == true) variab = "%f2";
                else variab = "%2";
            }
            if (radio_fn_path.Checked == true)
            {
                if (chk_full_p.Checked == true) variab = "%pff";
                else variab = "%fp";
            }
            if (radio_fn.Checked == true) variab = "%fn";
            if (radio_fn_ext.Checked == true) variab = "%ff";
            if (radio_fd.Checked == true) variab = "%fd";            
            if (radio_fdur_1.Checked == true) variab = "%fdur" + txt_operator_dur.Text;
            if (radio_bitr.Checked == true) variab = "%fbitr" + txt_operator_bitr.Text;
            if (radio_nul.Checked == true) variab = "nul";
            if (radio_chaps.Checked == true) variab = "[[split_chapters]]";
            if (radio_target_size.Checked == true) variab = " [[target_size=" + txt_size.Text + "MB-" + num_aud_target.Value.ToString() + "Kbps]] ";
        } 

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (selected() == false) return;
            canceled = false;
            set_var();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            variab = "";
            this.Close();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (selected() == false) return;
            canceled = true;
            set_var();
            Clipboard.SetText(variab);
            this.Close();
        }

        private void radio_inputfn_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            txt_pre.Text = file;
            if (radio_inputfn.Checked)
            {
                chk_full1.Enabled = true;
            }
            else chk_full1.Enabled = false;
        }

        private void radio_input_fn_noext_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4)
            {
                int ind = file.LastIndexOf(".");
                txt_pre.Text = file.Substring(0, ind);
            }
            else txt_pre.Text = "";
            if (radio_input_fn_noext.Checked)
            {
                chk_full2.Enabled = true;
            }
            else chk_full2.Enabled = false;
        }

        private void radio_fn_path_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFullPath(file).Substring(0, Path.GetFullPath(file).LastIndexOf("\\"));
            else txt_pre.Text = "";
            if (radio_fn_path.Checked)
            {
                chk_full_p.Enabled = true;
            }
            else chk_full_p.Enabled = false;
        }

        private void radio_fn_ext_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFileName(file);
            else txt_pre.Text = "";
        }

        private void radio_fn_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFileNameWithoutExtension(file);
            else txt_pre.Text = "";
        }

        private void radio_fd_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4)
            {
                txt_pre.Text = new DirectoryInfo(Path.GetDirectoryName(file)).Name;
            }
            else txt_pre.Text = "";
        }

        public Double get_bitrate(String file, String dur_file)
        {
            Double bytes = 0;
            try
            {
                FileInfo fi = new FileInfo(file);
                Double size = fi.Length;
                Double dur = 0;

                TimeSpan time;
                if (TimeSpan.TryParse(dur_file, out time))
                {
                    dur = TimeSpan.Parse(dur_file).TotalSeconds;
                    if (dur > 0) bytes = Math.Round((size / dur * 8 / 1000 * 0.998), 0);
                    else bytes = 0;
                }
                return bytes;                               
            }
            catch { return 0; }
        }

        private void get_fdur()
        {
            try
            {
                Double dur_rpl = TimeSpan.Parse(dur).TotalSeconds;
                String dur_secs = dur.ToString();

                int length = 0;
                int limit = txt_operator_dur.Text.Length - 1;

                for (int ii = 0; ii < limit; ii++)
                {
                    if (IsDigitsOnly(txt_operator_dur.Text.Substring(ii, 1)))
                    {
                        length = ii + 1;
                    }
                    else break;
                }
                                
                String dur_2 = TimeSpan.Parse(dur_secs).TotalSeconds.ToString();
                Double result = Convert.ToDouble(new DataTable().Compute(dur_2 + txt_operator_dur.Text, null));
                txt_pre.Text = Math.Round(result).ToString();
                txt_pre.Refresh();  
            }

            catch { txt_pre.Text = ""; }
        }
        Boolean IsDigitsOnly(String str)
        {
            foreach (char c in str)
            {
                if ((c < '0' || c > '9') && c != '.')
                {
                    return false;
                }
            }

            return true;
        }

        private void txt_pre_TextChanged(object sender, EventArgs e)
        {
            //txt_pre.SelectionStart = txt_pre.TextLength;
            //txt_pre.ScrollToCaret();
         
        }
        private void txt_operator_TextChanged(object sender, EventArgs e)
        {
            if (radio_fdur_1.Checked) get_fdur();
        }

        private void txt_operator_bitr_TextChanged(object sender, EventArgs e)
        {
            if (radio_bitr.Checked)
            {
                try
                {
                    Double result = Convert.ToDouble(new DataTable().Compute(get_bitrate(file, dur) + txt_operator_bitr.Text, null));
                    txt_pre.Text = Math.Round(result).ToString() + "K";
                }
                catch { txt_pre.Text = ""; }
            }
        }

        private void get_bitrate(Double target_mb)
        {
            Double bitr = target_mb * 8 * 1024 / TimeSpan.Parse(dur).TotalSeconds;
            bitr = Math.Round(bitr * 0.99, 0) - (double)num_aud_target.Value;
            if (bitr < 100) bitr = 100;
            String bitrs = bitr.ToString() + "K";
            txt_pre.Text = "-vb " + bitrs + " [[target_size=" + txt_size.Text + "MB-" + num_aud_target.Value.ToString() + "Kbps]] ";
        }
        private void txt_size_TextChanged(object sender, EventArgs e)
        {            
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_size.Text, "[^0-9]"))
            {
                txt_size.Text = txt_size.Text.Remove(txt_size.Text.Length - 1);
            }
            else
            {
                if (file.Length > 0 && txt_size.Text.Length > 0)
                {
                    get_bitrate(Convert.ToDouble(txt_size.Text));
                }                
            }
        }

        private void radio_target_size_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_target_size.Checked)
            {
                txt_pre.Text = "";
                txt_size.Enabled = true;
                if (file.Length > 0 && txt_size.Text.Length > 0)
                {
                    get_bitrate(Convert.ToDouble(txt_size.Text));
                }
            }
            else
            {
                txt_size.Enabled = false;
            }
        }

        private void num_aud_target_ValueChanged(object sender, EventArgs e)
        {
            if (file.Length > 0 && txt_size.Text.Length > 0)
            {
                get_bitrate(Convert.ToDouble(txt_size.Text));
            }
        }

        private void radio_inputfn_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_input_fn_noext_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_fn_path_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_fn_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_fn_ext_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_fd_Click(object sender, EventArgs e)
        {
            clean_gp2();
            clean_gp3();
        }

        private void radio_fdur_1_Click(object sender, EventArgs e)
        {
            clean_gp1();
            clean_gp3();
            get_fdur();
        }

        private void radio_bitr_Click(object sender, EventArgs e)
        {
            clean_gp1();
            clean_gp3();
            txt_pre.Text = get_bitrate(file, dur).ToString() + "K";
        }

        private void radio_target_size_Click(object sender, EventArgs e)
        {
            clean_gp1();
            clean_gp3();
        }

        private void radio_chaps_Click(object sender, EventArgs e)
        {
            clean_gp1();
            clean_gp2();
            if (file.Length > 0)
            {
                if (radio_chaps.Checked) get_chapters();
            }
            else txt_pre.Text = "";
        }

        private void radio_nul_Click(object sender, EventArgs e)
        {
            clean_gp1();
            clean_gp2();
            if (radio_nul.Checked)
            {
                txt_pre.Text = "";
                btn_copy.Enabled = false;
            }
            else
            {
                btn_copy.Enabled = true;
            }
        }
    }
}
