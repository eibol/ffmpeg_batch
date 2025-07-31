using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form25 : Form
    {
        public Form25()
        {
            InitializeComponent();
        }

        public Boolean check_ff = false;
        private Boolean pre_vis = Properties.Settings.Default.visuals;
        private Boolean is_portable = false;
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";        
        private String cpu = "";
        private String videocard = "";
        private String product = "";
        private String product_state = "";
        private String vir_exe = "";

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form25));
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

        private void Form25_Load(object sender, EventArgs e)
        {
            chk_visuals.Checked = Properties.Settings.Default.visuals_all;

            System.Windows.Forms.ToolTip toolT2z = new ToolTip();
            toolT2z.AutoPopDelay = 9000;
            toolT2z.InitialDelay = 750;
            toolT2z.ReshowDelay = 500;
            toolT2z.ShowAlways = true;
            toolT2z.SetToolTip(this.chk_visuals, Properties.Strings.improve_start);

            this.Cursor = Cursors.Default;
            if (!File.Exists(Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe")))
            {
                this.Close();
                return;
            }
            this.Cursor = Cursors.WaitCursor;
            refresh_lang();
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

            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;

            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (FFBatch.Properties.Settings.Default.app_lang == "zh-Hans") this.Height = this.Height + 38;

            this.Text = Properties.Strings.sec_perf;
            try
            {
                ManagementObjectSearcher mosProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                string Procname = null;

                foreach (ManagementObject moProcessor in mosProcessor.Get().Cast<ManagementObject>())
                {
                    if (moProcessor["name"] != null) cpu = moProcessor["name"].ToString();
                    else cpu = Properties.Strings.unknown;
                }

                lbl_cpu.Text = cpu;

                ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject mo in search.Get().Cast<ManagementObject>())
                {
                    PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                    PropertyData description = mo.Properties["Description"];
                    if (currentBitsPerPixel != null && description != null)
                    {
                        if (currentBitsPerPixel.Value != null)
                            videocard = videocard + (description.Value) + " ";
                    }
                }
                if (videocard == null) videocard = Properties.Strings.integrated;
                if (videocard == String.Empty) videocard = Properties.Strings.integrated;
                lbl_vcard.Text = videocard;

                String result = String.Empty;

                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
                foreach (ManagementObject os in searcher.Get().Cast<ManagementObject>())
                {
                    result = os["Caption"].ToString();
                    break;
                }

                lbl_os.Text = result;
                if (product.ToLower().Contains("windows defender"))
                {
                    btn_add_ex.Enabled = true;
                    txt_tip.Text = Properties.Strings.tip1 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip2 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip3 + " " + Properties.Strings.tip_win_sec + " " + Properties.Strings.tip4 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip5;
                }
                else
                {
                    txt_tip.Text = Properties.Strings.tip1 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip2 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip3 + " " + Properties.Strings.tip_your_av + " " + Properties.Strings.tip4 + Environment.NewLine + Environment.NewLine + Properties.Strings.tip5;
                }

                //Antivirus
                ManagementObjectSearcher wmiData = new ManagementObjectSearcher(@"root\SecurityCenter2", "SELECT * FROM AntiVirusProduct");
                ManagementObjectCollection data = wmiData.Get();

                int i = 0;
                foreach (ManagementObject virusChecker in data.Cast<ManagementObject>())
                {
                    product = (virusChecker.GetPropertyValue("displayName").ToString());
                    product_state = (virusChecker.GetPropertyValue("productState").ToString());
                    vir_exe = (virusChecker.GetPropertyValue("pathToSignedProductExe").ToString());
                    i++;
                }
                lbl_antivir.Text = product;

                lbl_os.Width = groupBox3.Width - lbl_os.Left - 13;
                lbl_cpu.Width = groupBox3.Width - lbl_cpu.Left - 13;
                lbl_vcard.Width = groupBox3.Width - lbl_vcard.Left - 13;
                lbl_antivir.Width = groupBox3.Width - lbl_antivir.Left - 13;

                //pic_2.Image = pic_excl.Image;
            }
            catch
            {
                lbl_os.Text = "-";
                lbl_cpu.Text = "-";
                lbl_vcard.Text = "-";
                lbl_antivir.Text = "-";
            }

            
            Properties.Settings.Default.Save();
            this.Cursor = Cursors.Arrow;
            this.ActiveControl = lbl_os;
        }

        private void btn_add_ex_Click(object sender, EventArgs e)
        {
            this.ActiveControl = lbl_os;
            
            if (!lbl_os.Text.ToLower().Contains("windows 10") && !lbl_os.Text.ToLower().Contains("windows 11"))
            {
                MessageBox.Show(Properties.Strings.req_w1011, Properties.Strings.only_1011, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (vir_exe.Contains("%ProgramFiles%")) vir_exe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + vir_exe.Replace("%ProgramFiles%", "");
                if (File.Exists(vir_exe)) Process.Start(vir_exe);
                return;
            }

            if (!lbl_antivir.Text.ToLower().Contains("defender"))
            {
                if (vir_exe.Contains("%ProgramFiles%")) vir_exe = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + vir_exe.Replace("%ProgramFiles%", "");

                if (vir_exe.ToLower().Contains("wsc_proxy"))
                {
                    if (product.ToLower().Contains("avg")) vir_exe = vir_exe.Replace("wsc_proxy", "AVGUI");
                    if (product.ToLower().Contains("avast")) vir_exe = vir_exe.Replace("wsc_proxy", "AvastUI");
                }

                if (File.Exists(vir_exe)) Process.Start(vir_exe);
                return;
            }
            DialogResult a = MessageBox.Show(Properties.Strings.attempt_exc + Environment.NewLine + Environment.NewLine + Properties.Strings.admin_cmd + Environment.NewLine + Environment.NewLine + Properties.Strings.proceed1, Properties.Strings.add_excl_ws, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (a != DialogResult.Yes) return;

            Boolean writable = false;
            writable = false;

            OpenFileDialog browse_file = new OpenFileDialog();
            browse_file.InitialDirectory = Application.StartupPath;
            String file_path = String.Empty;
            browse_file.Filter = "ffmpeg.exe |ffmpeg.exe";

            if (browse_file.ShowDialog() == DialogResult.OK)
            {
                file_path = browse_file.FileName;
                if (Path.GetFileName(file_path.ToLower()) != "ffmpeg.exe")
                {
                    MessageBox.Show(FFBatch.Properties.Strings.ff_not, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                return;
            }

                        String path = "cmd.exe";
            String param = "/C " + "powershell.exe Add-MpPreference -ExclusionPath " + "'" + file_path + "'";
            Process ff_ext = new Process();
            ff_ext.StartInfo.FileName = path;
            ff_ext.StartInfo.Arguments = param;
            ff_ext.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            if (writable == false) ff_ext.StartInfo.Verb = "runas";
            try
            {
                ff_ext.Start();
                ff_ext.WaitForExit();
                MessageBox.Show(Properties.Strings.excl_added, Properties.Strings.excl_add2, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("windowsdefender://exclusions");
            }
            catch (Exception exc)
            {
                MessageBox.Show(FFBatch.Properties.Strings.error3 + " " + exc.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                
        private void btn_close_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void chk_visuals_Click(object sender, EventArgs e)
        {
            if (chk_visuals.Checked == true)
            {
                Properties.Settings.Default.visuals_all = true;
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.NoneEnabled;
                Properties.Settings.Default.visuals = false;
            }
            else
            {
                Properties.Settings.Default.visuals_all = false;
                Properties.Settings.Default.visuals = pre_vis;
                Application.VisualStyleState = System.Windows.Forms.VisualStyles.VisualStyleState.ClientAndNonClientAreasEnabled;
            }
            Properties.Settings.Default.Save();
        }
    }
}