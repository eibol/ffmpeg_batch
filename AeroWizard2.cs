using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard2 : Form
    {
        public AeroWizard2()
        {
            InitializeComponent();
        }

        Form obj = new FFBatch.Form1();
        Boolean is_portable = false;
        String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        String pr1_params = String.Empty;
        String pr1_ext = String.Empty;
        String pr2_params = String.Empty;
        String pr2_ext = String.Empty;
        String pr3_params = String.Empty;
        String pr3_ext = String.Empty;
        Boolean cancelled = false;
        Boolean rename = false;
        int n_presets = 2;
        Boolean started = false;
        Boolean quit = false;

        public String pr1_string
        {
            get { return pr1_params; }
            set { pr1_params = value; }
        }

        public String pr1_string_ext
        {
            get { return pr1_ext; }
            set { pr1_ext = value; }
        }

        public String pr2_string
        {
            get { return pr2_params; }
            set { pr2_params = value; }
        }

        public String pr2_string_ext
        {
            get { return pr2_ext; }
            set { pr2_ext = value; }
        }

        public String pr3_string
        {
            get { return pr3_params; }
            set { pr3_params = value; }
        }

        public String pr3_string_ext
        {
            get { return pr3_ext; }
            set { pr3_ext = value; }
        }

        public Boolean cancelled_w
        {
            get { return cancelled; }
            set { cancelled = value; }
        }
        public Boolean rename_w
        {
            get { return rename; }
            set { rename = value; }
        }

        public int n_presets_w
        {
            get { return n_presets; }
            set { n_presets = value; }
        }

        private void wz_mpresets_SelectedPageChanged(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;

            wz_mpresets.FinishButtonText = "Start encoding";

            if (wz_mpresets.SelectedPage == wzp1 && started == false)

            {
                Boolean pr_loaded = false;
                if (pr1_string != String.Empty)
                {
                    txt_pr_1.Text = pr1_string;
                    txt_ext_1.Text = pr1_string_ext;
                    pr1_params = txt_pr_1.Text;
                    pr1_ext = txt_ext_1.Text;
                    pr_loaded = true;

                    if (pr2_string != String.Empty)
                    {
                        txt_pr_2.Text = pr2_string;
                        txt_ext_2.Text = pr2_string_ext;
                        pr2_params = txt_pr_2.Text;
                        pr2_ext = txt_ext_2.Text;
                    }
                    if (pr3_string != String.Empty)
                    {
                        txt_pr_3.Text = pr3_string;
                        txt_ext_3.Text = pr3_string_ext;
                        pr3_params = txt_pr_3.Text;
                        pr3_ext = txt_ext_3.Text;
                    }
                }

                ToolTip toolTip1 = new ToolTip();
                toolTip1.AutoPopDelay = 3500;
                toolTip1.InitialDelay = 750;
                toolTip1.ReshowDelay = 500;
                toolTip1.ShowAlways = true;
                toolTip1.SetToolTip(this.btn_clear, "Clear last preset");

                cb_w_presets.Items.Clear();
                cb_w_presets.Items.Add("Default parameters");
                String path, path_pr = "";
                                
                path_pr = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
                path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                if (is_portable == true)
                {
                    path_pr = port_path + "ff_presets_portable.ini";
                    path = port_path + "ff_batch_portable.ini";
                }

                int linea = 0;

                cb_w_presets.SelectedIndex = cb_w_presets.FindString(("Default parameters"));
                
                foreach (string line in File.ReadLines(path_pr))
                {
                    if (line.Contains("PR: "))
                    {
                        cb_w_presets.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));
                    }
                }
            }
            if (wz_mpresets.SelectedPage == wzp2)
            {
                label1.Text = "The wizard is ready to start encoding files using the selected " + n_presets.ToString() + " presets.";
            }
        }

        private void wzp1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            started = true;

            if (txt_pr_1.Text == String.Empty)
            {
                MessageBox.Show("Preset 1 is empty.", "Preset empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_pr_1.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_pr_2.Text == String.Empty && txt_pr_3.Text != String.Empty)
            {
                MessageBox.Show("Preset 2 is empty.", "Preset empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_pr_2.Focus();
                e.Cancel = true;
                return;
            }

            if (txt_ext_2.Text == String.Empty && txt_pr_2.Text != String.Empty)
            {
                MessageBox.Show("Please select output format for preset 2.", "Output format missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_2.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_ext_3.Text == String.Empty && txt_pr_3.Text != String.Empty)
            {
                MessageBox.Show("Please select output format for preset 3.", "Output format missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_3.Focus();
                e.Cancel = true;
                return;
            }

            if (txt_ext_1.Text == String.Empty && txt_pr_1.Text != String.Empty)
            {
                MessageBox.Show("Please select output format for preset 1.", "Output format missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_1.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_ext_2.Text == String.Empty && txt_pr_2.Text != String.Empty)
            {
                MessageBox.Show("Please select output format for preset 2.", "Output format missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_2.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_ext_3.Text == String.Empty && txt_pr_3.Text != String.Empty)
            {
                MessageBox.Show("Please select output format for preset 3.", "Output format missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_3.Focus();
                e.Cancel = true;
                return;
            }

            if (txt_ext_2.Text == String.Empty && txt_ext_3.Text == String.Empty)
            {
                MessageBox.Show("Please select at least two presets to continue.", "At least two presets needed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }

            if ((txt_ext_1.Text == txt_ext_2.Text && txt_ext_2.Text != String.Empty) || (txt_ext_1.Text == txt_ext_3.Text && txt_ext_3.Text != String.Empty) || (txt_ext_2.Text == txt_ext_3.Text && txt_ext_3.Text != String.Empty))
            {
                lbl_ovw.Visible = true;
                pic_warning.Visible = true;
                rename = true;
            }
            else
            {
                rename = false;
                lbl_ovw.Visible = false;
                pic_warning.Visible = false;
            }

            if (txt_ext_2.Text != String.Empty && txt_ext_3.Text == String.Empty)
            {
                n_presets = 2;
                pr1_params = txt_pr_1.Text;
                pr1_ext = txt_ext_1.Text;
                pr2_params = txt_pr_2.Text;
                pr2_ext = txt_ext_2.Text;
            }
            if (txt_ext_2.Text != String.Empty && txt_ext_3.Text != String.Empty)
            {
                n_presets = 3;
                pr1_params = txt_pr_1.Text;
                pr1_ext = txt_ext_1.Text;
                pr2_params = txt_pr_2.Text;
                pr2_ext = txt_ext_2.Text;
                pr3_params = txt_pr_3.Text;
                pr3_ext = txt_ext_3.Text;
            }
        }

        private void cb_w_presets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_w_presets.SelectedIndex == 0)
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                if (is_portable == true)
                {
                    path = port_path + "ff_batch_portable.ini";                    
                }
                else
                {
                    if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch")))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
                    }
                }

                if (!File.Exists(path))
                {

                    File.WriteAllText(path, "-c copy" + "\n" + "mp4" + "\n" + "yes" + "\n" + "Vs" + "\n" + "grid_yes" + "\n" + "keep_no" + "\n" + "subf_no");
                }

                int linea = 0;
                String ext1 = "";
                String pres1 = "";
                foreach (string line in File.ReadLines(path))
                {                    
                    if (linea == 0) pres1 = line;
                    if (linea == 1) ext1 = line;
                    if (linea > 1) break;
                    linea = linea + 1;
                }                 
                        if (txt_pr_1.Text.Length == 0)
                        {
                            txt_ext_1.Text = ext1;
                            pr1_ext = txt_ext_1.Text;
                            txt_pr_1.Text = pres1;
                            pr1_params = txt_pr_1.Text;
                        return;
                        }
                if (txt_pr_1.Text.Length != 0 && txt_pr_2.Text.Length == 0)
                {
                    txt_ext_2.Text = ext1;
                    pr2_ext = txt_ext_2.Text;
                    txt_pr_2.Text = pres1;
                    pr2_params = txt_pr_2.Text;
                    return;
                }
                if (txt_pr_2.Text.Length != 0 && txt_pr_3.Text.Length == 0)
                {
                    txt_ext_3.Text = ext1;
                    pr3_ext = txt_ext_3.Text;
                    txt_pr_3.Text = pres1;
                    pr3_params = txt_pr_3.Text;
                    return;
                }
            }
            else
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
                if (is_portable == true) path = port_path + "ff_presets_portable.ini";
                String pre_name = String.Empty;
                int i = 0;
                foreach (string line in File.ReadLines(path))
                {
                    if (line != "" && line.Contains("Version ") == false)
                    {
                        if (line.Substring(4, line.IndexOf("&") - 5) == cb_w_presets.SelectedItem.ToString())
                        {

                            int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");
                            if (txt_pr_1.Text == String.Empty)
                            {
                                txt_ext_1.Text = line.Substring(line.LastIndexOf("%") + 2);
                                pr1_ext = txt_ext_1.Text;
                                txt_pr_1.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                                pr1_params = txt_pr_1.Text;
                                continue;
                            }

                            if (txt_pr_1.Text != String.Empty && txt_pr_2.Text == String.Empty)
                            {
                                txt_ext_2.Text = line.Substring(line.LastIndexOf("%") + 2);
                                pr2_ext = txt_ext_2.Text;
                                txt_pr_2.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                                pr2_params = txt_pr_2.Text;
                                continue;

                            }
                            if (txt_pr_2.Text != String.Empty && txt_pr_3.Text == String.Empty)
                            {
                                txt_ext_3.Text = line.Substring(line.LastIndexOf("%") + 2);
                                pr3_ext = txt_ext_3.Text;
                                txt_pr_3.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                                pr3_params = txt_pr_3.Text;
                                continue;
                            }
                        }
                    }
                    i = i + 1;
                }
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (txt_pr_3.Text != String.Empty)
            {
                txt_pr_3.Text = String.Empty;
                txt_ext_3.Text = String.Empty;
                pr3_params = String.Empty;
                pr3_ext = String.Empty;
                return;
            }
            if (txt_pr_2.Text != String.Empty)
            {
                txt_pr_2.Text = String.Empty;
                txt_ext_2.Text = String.Empty;
                pr2_params = String.Empty;
                pr2_ext = String.Empty;
                return;
            }
            if (txt_pr_1.Text != String.Empty)
            {
                txt_pr_1.Text = String.Empty;
                txt_ext_1.Text = String.Empty;
                pr1_params = String.Empty;
                pr1_ext = String.Empty;
                return;
            }
        }


        private void wz_mpresets_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            pr1_params = String.Empty;
            pr1_ext = String.Empty;
            pr2_params = String.Empty;
            pr2_ext = String.Empty;
            pr3_params = String.Empty;
            pr3_ext = String.Empty;
            cancelled = true;
            quit = true;

        }

        private void AeroWizard2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (quit == false) cancelled = true;
        }

        private void wz_mpresets_Finished(object sender, EventArgs e)
        {
            quit = true;
        }

        private void btn_start_m3u_Click(object sender, EventArgs e)
        {
            wz_mpresets.NextPage();
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            cancelled = true;
            wz_mpresets.NextPage();
        }
    }
}
