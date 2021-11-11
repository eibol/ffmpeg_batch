using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard6 : Form
    {
        public AeroWizard6()
        {
            InitializeComponent();
        }

        public Boolean save_preset = false;
        public int list_count = 0;
        String pr_1st_params = String.Empty;
        String out_path = "";
        String out_format = "";
        String jpg_q = "";
        public Boolean canceled = false;
        public Boolean start_enc = false;
        
        
        Boolean  ok_images = false;

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
            if (txt_path_main.TextLength >= 3 && txt_path_main.Text.Substring(0, 2) != ".\\") MessageBox.Show("Please include .\\ on relative path.");
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
                wiz_split.NextButtonText = Properties.Strings2.next;
                wiz_split.CancelButtonText = Properties.Strings.cancel;
                wiz_split.FinishButtonText = Properties.Strings2.finish;
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
    }
}
