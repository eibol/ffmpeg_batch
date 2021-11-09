using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard7 : Form
    {
        public AeroWizard7()
        {
            InitializeComponent();
        }

        public Boolean save_preset = false;
        public Boolean one_to_one = false;
        public Boolean multi_to_one = false;
        public int list_count = 0;
        String img_dur_prog = "0";
        String pr_1st_params = String.Empty;
        String pr_pre_prms = String.Empty;
        String out_format = "";
        String imgs_time = "5";
        String img_aud= "";
        public Boolean canceled = true;
        public Boolean start_enc = false;
        
        Boolean  ok_images = false;

        public String pr1_img_dur
        {
            get { return img_dur_prog; }
            set { img_dur_prog = value; }
        }

        public String pr1_img_aud
        {
            get { return img_aud; }
            set { img_aud = value; }
        }
        public String pr1_imgs_time
        {
            get { return imgs_time; }
            set { imgs_time = value; }
        }

        public String pr1_first_params
        {
            get { return pr_1st_params; }
            set { pr_1st_params = value; }
        }
        public String pr1_pre_params
        {
            get { return pr_pre_prms; }
            set { pr_pre_prms = value; }
        }

        public String pr1_out_format
        {
            get { return out_format; }
            set { out_format = value; }
        }

        public Boolean wiz_ok_images
        {
            get { return ok_images; }
            set { ok_images = value; }
        }

        private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            canceled = true;
        }

        private void wz1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Double t_to = (double)n_multiv_secs.Value * (double)list_count;
            TimeSpan t1 = TimeSpan.FromSeconds((t_to));
            String tx_1 = string.Format("{0:D2}:{1:D2}:{2:D2}",
                t1.Hours,
                t1.Minutes,
                t1.Seconds);

            pr_pre_prms = "";
            pr_1st_params = "-c:v libx264 -preset fast" + " " + "-pix_fmt " + cb_pixel.SelectedItem.ToString() + " " + "-r " + cb_framerate1.Text + " " + "-to " + tx_1;
            pr1_img_dur = t_to.ToString();

            String resize = "";
            //"-vf scale=";            
            if (check_resize.Checked)
            {
                if (chk_width1.Checked)
                {
                    if (chk_even.Checked == false) resize = combo_resize.Text.Substring(0, combo_resize.Text.IndexOf("x")) + ":-1";
                    else resize = combo_resize.Text.Substring(0, combo_resize.Text.IndexOf("x")) + ":-2";
                }
                else resize = combo_resize.Text.Replace("x", ":");
                pr_1st_params = "-c:v libx264 -preset fast" + " " + "-pix_fmt " + cb_pixel.SelectedItem.ToString() + " " + "-r " + cb_framerate1.Text + " " + "-vf scale=" + resize + " " + "-to " + tx_1;
            }
            if (chk_audio.Checked && txt_audio_path.Text.Length > 5)
            {
                pr_1st_params = pr_1st_params + " " + "-c:a aac -b:a 128K";
                img_aud = txt_audio_path.Text;
                pr1_img_aud = "\u0022" + img_aud + "\u0022";
            }
            out_format = combo_ext.Text;
            pr1_out_format = out_format;
            imgs_time = n_multiv_secs.Value.ToString();
            pr1_imgs_time = imgs_time;
            
        }        

       
        private void check_resize_CheckedChanged(object sender, EventArgs e)
        {
            if (check_resize.Checked == true)
            {
                combo_resize.Enabled = true;
                radio_16_9.Enabled = true;
                radio_4_3.Enabled = true;
                chk_width1.Enabled = true;

                if (radio_16_9.Checked == true)
                {
                    combo_resize.Items.Clear();
                    combo_resize.Items.Add("320x180");
                    combo_resize.Items.Add("640x360");
                    combo_resize.Items.Add("800x480");
                    combo_resize.Items.Add("1024x640");
                    combo_resize.Items.Add("1280x720");
                    combo_resize.Items.Add("1920x1080");                    

                }
                if (radio_4_3.Checked == true)
                {
                    combo_resize.Items.Clear();
                    combo_resize.Items.Add("320x240");
                    combo_resize.Items.Add("800x600");
                    combo_resize.Items.Add("1024x768");
                    combo_resize.Items.Add("1280x1024");                    
                }
                
                combo_resize.SelectedIndex = 0;
            }
            else
            {
                combo_resize.Enabled = false;
                radio_16_9.Enabled = false;
                radio_4_3.Enabled = false;
                chk_width1.Enabled = false;
            }
        }

        private void radio_4_3_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_4_3.Checked == true)
            {
                combo_resize.Items.Clear();
                combo_resize.Items.Add("320x240");
                combo_resize.Items.Add("800x600");
                combo_resize.Items.Add("1024x768");
                combo_resize.Items.Add("1280x1024");
                combo_resize.SelectedIndex = 0;
            }
        }

        private void radio_16_9_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_16_9.Checked == true)
            {
                combo_resize.Items.Clear();
                combo_resize.Items.Add("320x180");
                combo_resize.Items.Add("640x360");
                combo_resize.Items.Add("800x480");
                combo_resize.Items.Add("1024x640");
                combo_resize.Items.Add("1280x720");
                combo_resize.Items.Add("1920x1080");
                combo_resize.SelectedIndex = 0;
            }
        }        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {           
            this.Close();
        }

        private void wz_end_Rollback(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            pr_1st_params = "";            
            out_format = "";
            pr1_img_aud = "";            
        }

        private void wz_end_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
          
        }

        private void AeroWizard5_Load(object sender, EventArgs e)
        {
            refresh_lang();
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard7));
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

        private void wz0_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
            foreach (String item in v_pixels) cb_pixel.Items.Add(item);
            foreach (String item in v_pixels) cb_pixel2.Items.Add(item);

            if (radio_1_v.Checked)
            {
                if (list_count < 1)
                {
                    MessageBox.Show(Properties.Strings.list_empty);
                    wz0.AllowNext = false;
                    this.Close();
                }
                else
                {
                    wz0.AllowNext = true;
                    wz1.Suppress = true;
                    wz2.Suppress = false;
                    multi_to_one = true;
                    one_to_one = false;
                }
            }

            if (radio_multi_v.Checked)
            {
                if (list_count < 2)
                {
                    MessageBox.Show(Properties.Strings.two_files);
                    wz0.AllowNext = false;
                    return;
                }
                else
                {
                    wz0.AllowNext = true;
                    wz1.Suppress = false;
                    wz2.Suppress = true;
                    multi_to_one = false;
                    one_to_one = true;
                }
            }

        }

        private void cb_framerate_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(cb_framerate2.Text, "[^0-9]"))
            {
                MessageBox.Show(FFBatch.Properties.Strings.only_numbers);
                cb_framerate2.Text = cb_framerate2.Text.Remove(cb_framerate2.Text.Length - 1);
            }
        }

        private void wz2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            Double t_to = (double)n_single_v_secs.Value;
            pr1_img_dur = t_to.ToString();

            pr_pre_prms = "-r 1/" + n_single_v_secs.Value.ToString();
            pr_1st_params = "-c:v libx264 -preset fast" + " " + "-pix_fmt " + cb_pixel2.SelectedItem.ToString() + " " + "-vf fps=" + cb_framerate2.Text;
            String resize = "";
            //"-vf scale=";            
            if (check_resize2.Checked)
            {
                if (chk_width2.Checked)
                {
                    if (chk_even2.Checked == false) resize = combo_resize2.Text.Substring(0, combo_resize2.Text.IndexOf("x")) + ":-1";
                    resize = combo_resize2.Text.Substring(0, combo_resize2.Text.IndexOf("x")) + ":-2";
                }
                else resize = combo_resize2.SelectedItem.ToString().Replace("x",":");
                pr_1st_params = "-c:v libx264 -preset fast" + " " + "-pix_fmt " + cb_pixel2.SelectedItem.ToString() + " " + "-vf " + "\u0022" + "fps="  + cb_framerate2.Text + "," + "scale=" + resize + "\u0022";                
            }

            if (chk_audio2.Checked && txt_audio2.Text.Length > 5)
            {
                img_aud = txt_audio2.Text;
                pr1_img_aud = img_aud;
                pr_1st_params = " " + " -i " + "\u0022" + img_aud + "\u0022"  + " " + pr_1st_params + " " + "-c:a aac -b:a 128K";
                
            }

            out_format = combo_ext2.Text;
            pr1_out_format = out_format;
        }   

        private void check_resize2_CheckedChanged(object sender, EventArgs e)
        {
            if (check_resize2.Checked == true)
            {
                combo_resize2.Enabled = true;
                radio_16_9_2.Enabled = true;
                radio_4_3_2.Enabled = true;
                chk_width2.Enabled = true;

                if (radio_16_9_2.Checked == true)
                {
                    combo_resize2.Items.Clear();
                    combo_resize2.Items.Add("320x180");
                    combo_resize2.Items.Add("640x360");
                    combo_resize2.Items.Add("800x480");
                    combo_resize2.Items.Add("1024x640");
                    combo_resize2.Items.Add("1280x720");
                    combo_resize2.Items.Add("1920x1080");

                }
                if (radio_4_3_2.Checked == true)
                {
                    combo_resize2.Items.Clear();
                    combo_resize2.Items.Add("320x240");
                    combo_resize2.Items.Add("800x600");
                    combo_resize2.Items.Add("1024x768");
                    combo_resize2.Items.Add("1280x1024");
                }

                combo_resize2.SelectedIndex = 0;
            }
            else
            {
                combo_resize2.Enabled = false;
                radio_16_9_2.Enabled = false;
                radio_4_3_2.Enabled = false;
                chk_width2.Enabled = false;
            }
        }

        private void wz2_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            cb_framerate2.SelectedIndex = 0;
            combo_ext2.SelectedIndex = 0;
            cb_pixel2.SelectedIndex = 0;
        }

        private void wz1_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            cb_framerate1.SelectedIndex = 0;
            combo_ext.SelectedIndex = 0;
            cb_pixel.SelectedIndex = 0;
            lbl_c.Text = list_count.ToString() + " " + Properties.Strings.files;
        }

        private void chk_audio_CheckedChanged(object sender, EventArgs e)
        {
            txt_audio_path.Enabled = !txt_audio_path.Enabled;
            button3.Enabled = !button3.Enabled;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            openf.ShowDialog();
        }

        private void radio_multi_v_CheckedChanged(object sender, EventArgs e)
        {
            wz0.AllowNext = true;
            one_to_one = true;
            multi_to_one = false;
            if (list_count < 2)
            {                
                wz0.AllowNext = false;                
            }
        }

        private void radio_1_v_CheckedChanged(object sender, EventArgs e)
        {
            wz0.AllowNext = true;
            one_to_one = false;
            multi_to_one = true;
            if (list_count < 1)
            {
                wz0.AllowNext = false;                
            }
        }

        private void openf_FileOk(object sender, CancelEventArgs e)
        {
            txt_audio_path.Text = openf.FileName;
            Process probe = new Process();
            String dur_hex = String.Empty;
            probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
            probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + openf.FileName + '\u0022';
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
                    dur_hex = duracion.Substring(0, 7);

                    if (duracion.Substring(0, 7) == "0:00:00")
                    {
                        dur_hex = String.Empty;
                    }
                }
                else
                {
                    dur_hex = String.Empty;
                }
            }
            else
            {
                dur_hex = String.Empty;
            }
            TimeSpan time = new TimeSpan();
            if (TimeSpan.TryParse(dur_hex, out time))
            {
                duracion = TimeSpan.Parse(dur_hex).TotalSeconds.ToString();
                n_multiv_secs.Value = Math.Round(Convert.ToDecimal(duracion) / list_count);
            }
        }

        private void chk_audio2_CheckedChanged(object sender, EventArgs e)
        {
            txt_audio2.Enabled = !txt_audio_path.Enabled;
            browse_aud2.Enabled = !browse_aud2.Enabled;
        }

        private void browse_aud2_Click(object sender, EventArgs e)
        {
            openf2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            openf2.ShowDialog();
        }

        private void openf2_FileOk(object sender, CancelEventArgs e)
        {
            txt_audio2.Text = openf2.FileName;

            Process probe = new Process();
            String dur_hex = String.Empty;
            probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
            probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + openf2.FileName + '\u0022';
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
                    dur_hex = duracion.Substring(0, 7);

                    if (duracion.Substring(0, 7) == "0:00:00")
                    {
                        dur_hex = String.Empty;
                    }
                }
                else
                {
                    dur_hex = String.Empty;
                }
            }
            else
            {
                dur_hex = String.Empty;
            }
            TimeSpan time = new TimeSpan();
            if (TimeSpan.TryParse(dur_hex, out time))
            {
                duracion = TimeSpan.Parse(dur_hex).TotalSeconds.ToString();
                n_single_v_secs.Value = Math.Round(Convert.ToDecimal(duracion));
            }
        }

        private void btn_cancel_Click_1(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void btn_Start_Click_1(object sender, EventArgs e)
        {
            canceled = false;
            start_enc = true;
            this.Close();
        }

        private void wz0_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            if (list_count < 2)
            {                
                wz0.AllowNext = false;
                return;
            }
            else
            {
                wz0.AllowNext = true;
                wz1.Suppress = false;
                wz2.Suppress = true;
                multi_to_one = false;
                one_to_one = true;
            }
        }

        private void wz_end_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            canceled = false;
        }

        private void chk_width1_CheckedChanged(object sender, EventArgs e)
        {
            chk_even.Enabled = !chk_even.Enabled;
        }

        private void chk_width2_CheckedChanged(object sender, EventArgs e)
        {
            chk_even2.Enabled = !chk_even2.Enabled;
        }
    }
}
