using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard3 : Form
    {
        public AeroWizard3()
        {
            InitializeComponent();
        }

        public Boolean is_target = false;
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        private Boolean is_portable = false;
        private String saved_pres1 = "";
        private String saved_ext1 = "";
        private Boolean tried_ok = false;
        private Boolean two_try_fail = false;
        private String pr1_params_two = String.Empty;
        private String pr1_ext_two = String.Empty;
        private String pr_1st_params = String.Empty;
        private Boolean cancelled = false;
        private Boolean started = false;
        private Boolean valid = false;
        private Boolean first_page_change = false;
        private String preset_status = FFBatch.Properties.Strings.pending;
        public String lv1_item = String.Empty;

        public String saved_pres
        {
            get { return saved_pres1; }
            set { saved_pres1 = value; }
        }

        public String saved_ext
        {
            get { return saved_ext1; }
            set { saved_ext1 = value; }
        }

        public String pr1_first_params
        {
            get { return pr_1st_params; }
            set { pr_1st_params = value; }
        }

        public String pr1_string_two_params
        {
            get { return pr1_params_two; }
            set { pr1_params_two = value; }
        }

        public String pr1_string_two_ext
        {
            get { return pr1_ext_two; }
            set { pr1_ext_two = value; }
        }

        public Boolean cancelled_w
        {
            get { return cancelled; }
            set { cancelled = value; }
        }

        private void cb_w_presets_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_pr_1.Clear();
            txt_ext_1.Clear();

            if (saved_pres.Length > 0)
            {
                cb_w_presets.Text = FFBatch.Properties.Strings.last_pr;
                txt_pr_1.Text = saved_pres;
                txt_ext_1.Text = saved_ext;
                saved_pres = "";
                saved_ext = "";
                return;
            }
            if (cb_w_presets.SelectedIndex == 0)
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                if (is_portable == true) path = port_path + "ff_batch_portable.ini";

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
                txt_ext_1.Text = ext1;
                pr1_ext_two = txt_ext_1.Text;
                txt_pr_1.Text = pres1;
                pr1_params_two = txt_pr_1.Text;
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

                            txt_ext_1.Text = line.Substring(line.LastIndexOf("%") + 2);
                            pr1_ext_two = txt_ext_1.Text;
                            txt_pr_1.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                            pr1_params_two = txt_pr_1.Text;
                        }
                    }
                    i = i + 1;
                }
            }
        }

        private void wizard3_SelectedPageChanged(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;

            wizard3.FinishButtonText = FFBatch.Properties.Strings.start_enc;

            if (wizard3.SelectedPage.Name == "wz_two_end")
            {
                label5.Focus();
            }

            if (wizard3.SelectedPage == wz_mpresets)
            {
                first_page_change = false;
                wizard3.Pages[0].AllowNext = true;
                if (started == false)
                {
                    cb_w_presets.Items.Clear();
                    cb_w_presets.Items.Add(FFBatch.Properties.Strings.default_param);
                    String path, path_pr = "";

                    path_pr = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
                    path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                    if (is_portable == true)
                    {
                        path_pr = port_path + "ff_presets_portable.ini";
                        path = port_path + "ff_batch_portable.ini";
                    }
                    int linea = 0;
                    String ext1 = "";
                    String pres1 = "";

                    cb_w_presets.SelectedIndex = cb_w_presets.FindString(FFBatch.Properties.Strings.default_param);
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
                        txt_pr_1.Text = pres1;
                        pr1_params_two = txt_pr_1.Text;
                    }

                    foreach (string line in File.ReadLines(path_pr))
                    {
                        if (line.Contains("PR: "))
                        {
                            cb_w_presets.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));
                        }
                    }
                }
                else
                {
                    if (preset_status == FFBatch.Properties.Strings.pending)
                    {
                        btn_start_multi.Enabled = false;
                        pic_status.Image = img_status.Images[0];
                    }
                    if (preset_status == FFBatch.Properties.Strings.failed)
                    {
                        btn_start_multi.Enabled = false;
                        pic_status.Image = img_status.Images[2];
                    }
                    if (preset_status == "OK")
                    {
                        btn_start_multi.Enabled = true;
                        pic_status.Image = img_status.Images[1];
                    }
                }
                if (saved_pres.Length > 0)
                {
                    cb_w_presets.Text = FFBatch.Properties.Strings.last_pr;
                    txt_pr_1.Text = saved_pres;
                    txt_ext_1.Text = saved_ext;
                }
                if (is_target == true)
                {
                    txt_pr_1.Clear();
                    txt_ext_1.Text = Path.GetExtension(lv1_item).Remove(0,1);
                }
            }
        }

        private void wizardPage1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            saved_pres = txt_pr_1.Text;
            saved_ext = txt_ext_1.Text;
            started = true;
            txt_pr_two_end.Enabled = true;
            txt_pr_end_2.Enabled = true;
            pic_status.Visible = true;

            String pass_format = "mp4";
            if (txt_pr_1.Text.ToLower().Contains("libvpx-vp9")) pass_format = "webm";
            txt_pr_two_end.Text = String.Empty;
            txt_pr_two_end.Text = txt_pr_1.Text.Replace("-c:a copy", "") + " -pass 1 -an -sn -f " + pass_format;
            txt_pr_ext_end.Text = String.Empty;
            txt_pr_ext_end.Text = "nul";
            txt_pr_end_2.Text = String.Empty;
            txt_pr_end_2.Text = txt_pr_1.Text + " -pass 2";
            txt_ext_end_2.Text = String.Empty;
            txt_ext_end_2.Text = txt_ext_1.Text;

            if (is_target == true)
            {
                lbl_enabled_target.Text = Properties.Strings.en_target_size + " " + n_target_size.Value.ToString() + " MB";
                txt_pr_two_end.Enabled = false;
                txt_pr_end_2.Enabled = false;
                pic_status.Visible = false;
                btn_start_multi.Enabled = true;
                this.Cursor = Cursors.WaitCursor;
                wizard3.Pages[0].AllowNext = false;
                e.Cancel = true;
                BG_Try_1.RunWorkerAsync();
                return;
            }

            if (txt_pr_1.Text.Contains("h264_nvenc") || txt_pr_1.Text.Contains("hevc_nvenc"))
            {
                DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.two_nvenc + " " + '\u0022' + "-preset slow" + '\u0022' + " " + FFBatch.Properties.Strings.std_pr, " " + FFBatch.Properties.Strings.nvenc_det, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (a == DialogResult.Cancel)
                {
                    cancelled = true;
                    ActiveForm.Close();
                }
            }

            if (txt_pr_1.Text == String.Empty)
            {
                MessageBox.Show(FFBatch.Properties.Strings.pr_emp, FFBatch.Properties.Strings.warning, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_pr_1.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_ext_1.Text == String.Empty && txt_pr_1.Text != String.Empty)
            {
                MessageBox.Show(FFBatch.Properties.Strings.sel_format_pr + " " + "1.", FFBatch.Properties.Strings.out_miss, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_ext_1.Focus();
                e.Cancel = true;
                return;
            }
            if (txt_pr_1.Text.ToLower().Contains("-c:v copy") || txt_pr_1.Text.ToLower().Contains("-vcodec copy"))
            {
                MessageBox.Show(FFBatch.Properties.Strings.two_str_c, FFBatch.Properties.Strings.strv_c_not, MessageBoxButtons.OK, MessageBoxIcon.Information);
                txt_pr_1.Focus();
                e.Cancel = true;
                return;
            }

            if (first_page_change == false) return;

            this.Cursor = Cursors.WaitCursor;
            wizard3.Pages[0].AllowNext = false;
            e.Cancel = true;
            BG_Try_1.RunWorkerAsync();
            
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            cancelled = true;
            wizard3.NextPage();
        }

        private void btn_start_multi_Click(object sender, EventArgs e)
        {
            wizard3.NextPage();
        }

        private void wizard3_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cancelled = true;
        }

        private void wz_two_end_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (!txt_pr_two_end.Text.ToLower().Contains("-pass 1") || !txt_pr_two_end.Text.ToLower().Contains(" -f "))
            {
                MessageBox.Show("Parameters " + '\u0022' + "-pass 1" + '\u0022' + " and " + '\u0022' + "-f" + '\u0022' + " are required for first pass.", "No first pass required parameter found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            if (!txt_pr_end_2.Text.ToLower().Contains("-pass 2"))
            {
                MessageBox.Show(FFBatch.Properties.Strings.Parameter + " " + '\u0022' + "-pass 2" + '\u0022' + " was not found on second pass parameters.", "No first pass required parameter found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
                return;
            }

            pr1_first_params = txt_pr_two_end.Text;
            pr1_params_two = txt_pr_end_2.Text;
            pr1_ext_two = txt_ext_end_2.Text;
        }

        private void btn_tips_Click(object sender, EventArgs e)
        {
            Form frmInfo = new Form();
            frmInfo.Name = FFBatch.Properties.Strings.two_tips;
            frmInfo.Text = FFBatch.Properties.Strings.two_tips;
            frmInfo.Icon = this.Icon;
            frmInfo.Height = 396;
            frmInfo.Width = 550;
            frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmInfo.MaximizeBox = false;
            frmInfo.MinimizeBox = false;
            frmInfo.BackColor = Color.White;

            TextBox txt_tips = new TextBox();
            txt_tips.Parent = frmInfo;
            txt_tips.Top = 15;
            txt_tips.Left = 14;
            txt_tips.Width = 501;
            txt_tips.Multiline = true;
            txt_tips.Height = 300;
            txt_tips.BackColor = Color.White;
            txt_tips.BorderStyle = BorderStyle.Fixed3D;
            txt_tips.TextAlign = HorizontalAlignment.Left;
            txt_tips.ReadOnly = true;
            txt_tips.Text = FFBatch.Properties.Strings.General + " " + Environment.NewLine + FFBatch.Properties.Strings.two_tip1 + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.two_tip2 + Environment.NewLine + FFBatch.Properties.Strings.two_tip3 + Environment.NewLine + FFBatch.Properties.Strings.two_tip4 + Environment.NewLine + FFBatch.Properties.Strings.two_tip5 + Environment.NewLine + FFBatch.Properties.Strings.two_tip6 + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.two_tip7 + Environment.NewLine + FFBatch.Properties.Strings.two_tip8 + Environment.NewLine + FFBatch.Properties.Strings.two_tip9 + Environment.NewLine + FFBatch.Properties.Strings.two_tip10 + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.two_tip11 + Environment.NewLine + FFBatch.Properties.Strings.first_p + " " + "-c:v libx264 -preset:v fast -profile:v high -b:v 3000K -an -sn -f mp4 -pass 1" + Environment.NewLine + FFBatch.Properties.Strings.second_p + " " + "-map 0 -c:v libx264 -preset:v fast -profile:v high -b:v 3000K -c:a copy -c:s copy -pass 2" + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.two_tip_12;
            txt_tips.TabIndex = 1;

            Button boton_ok_ff = new Button();
            boton_ok_ff.Parent = frmInfo;
            boton_ok_ff.Left = 212;
            boton_ok_ff.TextAlign = ContentAlignment.MiddleCenter;
            boton_ok_ff.Top = 320;
            boton_ok_ff.Width = 100;
            boton_ok_ff.Height = 25;
            boton_ok_ff.Text = FFBatch.Properties.Strings.close;
            boton_ok_ff.Click += new EventHandler(boton_ok_ff_Click);
            boton_ok_ff.TabIndex = 0;

            frmInfo.StartPosition = FormStartPosition.CenterParent;
            frmInfo.ShowDialog();
        }

        private void boton_ok_ff_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frmInfo = new Form();
            frmInfo.Name = FFBatch.Properties.Strings.two_tips;
            frmInfo.Text = FFBatch.Properties.Strings.two_tips;
            frmInfo.Icon = this.Icon;
            frmInfo.Height = 356;
            frmInfo.Width = 550;
            frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmInfo.MaximizeBox = false;
            frmInfo.MinimizeBox = false;
            frmInfo.BackColor = Color.White;

            TextBox txt_tips = new TextBox();
            txt_tips.Parent = frmInfo;
            txt_tips.Top = 15;
            txt_tips.Left = 14;
            txt_tips.Width = 501;
            txt_tips.Multiline = true;
            txt_tips.Height = 260;
            txt_tips.BackColor = Color.White;
            txt_tips.BorderStyle = BorderStyle.Fixed3D;
            txt_tips.TextAlign = HorizontalAlignment.Left;
            txt_tips.ReadOnly = true;
            txt_tips.Text = FFBatch.Properties.Strings.two_sup + Environment.NewLine + FFBatch.Properties.Strings.two_sup1 + Environment.NewLine + FFBatch.Properties.Strings.two_sup2 + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.parameters + Environment.NewLine + FFBatch.Properties.Strings.two_sup3 + Environment.NewLine + FFBatch.Properties.Strings.two_sup4 + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.warnings + Environment.NewLine + FFBatch.Properties.Strings.two_sup5 + Environment.NewLine + Environment.NewLine + "-c:v libvpx-vp9 -row-mt 1 -b:v 8M -minrate 8M -maxrate 8M";
            txt_tips.TabIndex = 1;

            Button boton_ok_ff = new Button();
            boton_ok_ff.Parent = frmInfo;
            boton_ok_ff.Left = 212;
            boton_ok_ff.TextAlign = ContentAlignment.MiddleCenter;
            boton_ok_ff.Top = 280;
            boton_ok_ff.Width = 100;
            boton_ok_ff.Height = 25;
            boton_ok_ff.Text = FFBatch.Properties.Strings.close;
            boton_ok_ff.Click += new EventHandler(boton_ok_ff_Click);
            boton_ok_ff.TabIndex = 0;

            frmInfo.StartPosition = FormStartPosition.CenterParent;
            frmInfo.ShowDialog();
        }

        private void txt_pr_1_TextChanged(object sender, EventArgs e)
        {
            if (is_target == true) return;
            first_page_change = true;
            if (!txt_pr_1.Text.ToLower().Contains("-b:v") && !txt_pr_1.Text.ToLower().Contains("-vb"))
            {
                lbl_advise_1.Text = FFBatch.Properties.Strings.two_sup6;
                pic_warn_bitrate.Visible = true;
            }
            else
            {
                lbl_advise_1.Text = String.Empty;
                pic_warn_bitrate.Visible = false;
            }
        }

        private void txt_pr_two_end_TextChanged(object sender, EventArgs e)
        {
            pic_status.Image = img_status.Images[0];
            preset_status = FFBatch.Properties.Strings.pending;
            btn_start_multi.Enabled = false;
            valid = false;
            if (!txt_pr_two_end.Text.ToLower().Contains("-pass 1"))
            {
                txt_tip_1st.Text = FFBatch.Properties.Strings.Parameter + " " + '\u0022' + "-pass 1" + '\u0022' + " " + FFBatch.Properties.Strings.p_req_first;
                pic_status.Image = img_status.Images[2];
                btn_status.Enabled = false;
                preset_status = FFBatch.Properties.Strings.failed;
            }
            else
            {
                if (!txt_pr_two_end.Text.ToLower().Contains(" -f "))
                {
                    txt_tip_1st.Text = FFBatch.Properties.Strings.Parameter + " " + '\u0022' + "-f" + '\u0022' + " " + FFBatch.Properties.Strings.p_req_first;
                    pic_status.Image = img_status.Images[2];
                    btn_status.Enabled = false;
                    preset_status = FFBatch.Properties.Strings.failed;
                }
                else
                {
                    if (!txt_pr_two_end.Text.ToLower().Contains(" -b:v ") && !txt_pr_two_end.Text.ToLower().Contains(" -vb "))
                    {
                        txt_tip_1st.Text = FFBatch.Properties.Strings.two_sup6;
                        pic_status.Image = img_status.Images[2];
                        preset_status = FFBatch.Properties.Strings.failed;
                        btn_status.Enabled = true;
                    }
                    else
                    {
                        txt_tip_1st.Text = String.Empty;
                        pic_status.Image = img_status.Images[0];
                        preset_status = FFBatch.Properties.Strings.pending;
                        btn_status.Enabled = true;
                    }
                }
            }
        }

        private void txt_pr_end_2_TextChanged(object sender, EventArgs e)
        {
            pic_status.Image = img_status.Images[0];
            btn_start_multi.Enabled = false;
            valid = false;
            if (!txt_pr_end_2.Text.ToLower().Contains("-pass 2"))
            {
                txt_tip_2nd.Text = FFBatch.Properties.Strings.Parameter + " " + '\u0022' + "-pass 2" + '\u0022' + " " + FFBatch.Properties.Strings.p_req_second;
                pic_status.Image = img_status.Images[2];
                preset_status = FFBatch.Properties.Strings.failed;
                btn_status.Enabled = false;
            }
            else
            {
                if (!txt_pr_end_2.Text.ToLower().Contains(" -b:v ") && !txt_pr_end_2.Text.ToLower().Contains(" -vb "))
                {
                    txt_tip_2nd.Text = FFBatch.Properties.Strings.two_sup6;
                    pic_status.Image = img_status.Images[2];
                    preset_status = FFBatch.Properties.Strings.failed;
                    btn_status.Enabled = true;
                }
                else
                {
                    txt_tip_2nd.Text = String.Empty;
                    pic_status.Image = img_status.Images[0];
                    preset_status = FFBatch.Properties.Strings.pending;
                    btn_status.Enabled = true;
                }
            }
        }

        private void BG_Try_1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            tried_ok = false;
            valid = false;

            this.InvokeEx(f => f.btn_start_multi.Enabled = false);
            this.InvokeEx(f => f.btn_status.Enabled = false);

            two_try_fail = false;
            this.InvokeEx(f => this.Cursor = Cursors.WaitCursor);
            ListBox LB1_o = new ListBox();
            Process consola_pre = new Process();
            String file_prueba = "";
            String sel_test = lv1_item;

            file_prueba = sel_test;
            String destino_test = Path.GetTempPath() + "\\" + "FFBatch_test";
            Boolean bad_chars = false;
            Boolean unsupported = false;

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
                        MessageBox.Show(FFBatch.Properties.Strings.error + " " + excpt.Message, FFBatch.Properties.Strings.write_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;
                    }
                }

                String ext_output = "nul";

                //textbox_params = multi_1st_pass;
                String templog = Path.GetTempPath() + "\\" + "FF_pass2.log";
                String tempfile = Path.GetTempPath() + "\\" + "FF_pass2";
                consola_pre.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");

                this.Invoke(new MethodInvoker(delegate
                {
                    if (is_target == true) consola_pre.StartInfo.Arguments = " -y -i " + '\u0022' + file_prueba + '\u0022' + " -t 00:00:0.100 " + "-c:v " + combo_codec_t.SelectedItem.ToString() + " " + "-b:v 1000K " + "-c:a " + combo_audio_target.SelectedItem.ToString() + " -b:a " + num_aud_target.Value.ToString() + "K " + txt_pr_1.Text + " " + '\u0022' + tempfile + "." + txt_ext_end_2.Text + '\u0022';
                    else consola_pre.StartInfo.Arguments = " -y -i " + '\u0022' + file_prueba + '\u0022' + " -t 00:00:0.100 " + txt_pr_two_end.Text + " -passlogfile " + '\u0022' + templog + '\u0022' + " " + ext_output;
                    //MessageBox.Show(consola_pre.StartInfo.Arguments);
                }));
                                
                consola_pre.StartInfo.RedirectStandardOutput = true;
                consola_pre.StartInfo.RedirectStandardError = true;
                consola_pre.StartInfo.UseShellExecute = false;
                consola_pre.StartInfo.CreateNoWindow = true;
                consola_pre.EnableRaisingEvents = true;
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

            if (!tt.Wait(1800) && consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                tried_ok = true;
                two_try_fail = false;
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);

                if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                {
                    foreach (String file in Directory.GetFiles(destino_test))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        {
                        }
                    }
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
                LB1_o.Items.Clear();
                return;
            }

            if (bad_chars == false)
            {
                if (consola_pre.ExitCode != 0)
                {
                    two_try_fail = true;
                    tried_ok = false;
                    pic_status.Image = img_status.Images[2];
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    foreach (String lin in LB1_o.Items)
                    {
                        if (lin.Contains("not load the requested plugin") || lin.Contains("Cannot load nvcuda.dll"))
                        {
                            unsupported = true;
                        }
                    }
                    if (unsupported == true) MessageBox.Show(FFBatch.Properties.Strings.enc_fail_first + " " + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.unsup_enc + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.try_pr, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show(FFBatch.Properties.Strings.enc_fail_first + " " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.try_pr, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    tried_ok = false;
                    return;
                }
            }
            //END try preset

            LB1_o.Items.Clear();
            consola_pre.Dispose();
        }

        private void BG_Try_1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            btn_status.Enabled = true;
            if (two_try_fail == false)
            {
                btn_status.Enabled = false;
                BG_Try_Two_Final.RunWorkerAsync();
            }
            else
            {
                if (wizard3.SelectedPage == wz_mpresets)
                {
                    wizard3.NextPage(wz_two_end, true);
                }
            }
        }

        private void BG_Try_Two_Final_DoWork(object sender, DoWorkEventArgs e)
        {
            tried_ok = false;
            if (is_target == true)
            {
                tried_ok = true;
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                this.InvokeEx(f => f.pic_status.Visible = true);
                pic_status.Image = img_status.Images[1];
                this.InvokeEx(f => f.btn_start_multi.Enabled = true);
                return;
            }
            
            this.InvokeEx(f => this.Cursor = Cursors.WaitCursor);
            ListBox LB1_o = new ListBox();
            Process consola_pre = new Process();
            String file_prueba = "";
            String sel_test = lv1_item;

            file_prueba = sel_test;
            String destino_test = Path.GetTempPath() + "\\" + "FFBatch_test";
            Boolean bad_chars = false;
            Boolean unsupported = false;
            String textbox_params = String.Empty;

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
                        MessageBox.Show(FFBatch.Properties.Strings.error + " " + excpt.Message, FFBatch.Properties.Strings.write_error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;
                    }
                }

                String ext_output = txt_ext_end_2.Text;
                if (txt_ext_end_2.Text == String.Empty)
                {
                    ext_output = Path.GetExtension(file_prueba);
                }
                else
                {
                    ext_output = "." + txt_ext_end_2.Text;
                }

                textbox_params = txt_pr_end_2.Text;
                String file_prueba2 = file_prueba;

                if (textbox_params.Contains("%1"))
                {
                    if (file_prueba2.Contains("[") || file_prueba2.Contains("]"))
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.conflict_char, FFBatch.Properties.Strings.conflict_char2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        tried_ok = false;
                        bad_chars = true;
                        pic_status.Image = img_status.Images[2];
                        return;
                    }
                    file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                    file_prueba2 = file_prueba2.Replace(":", "\\\\" + ":");
                    textbox_params = textbox_params.Replace("%1", file_prueba2);
                }

                String templog = Path.GetTempPath() + "\\" + "FF_pass2.log";
                consola_pre.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
                consola_pre.StartInfo.Arguments = " -y -i " + "" + '\u0022' + file_prueba + '\u0022' + " -t 00:00:0.100 " + "-y " + textbox_params + " -passlogfile " + '\u0022' + templog + '\u0022' + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + ext_output + '\u0022';

                consola_pre.StartInfo.RedirectStandardOutput = true;
                consola_pre.StartInfo.RedirectStandardError = true;
                consola_pre.StartInfo.UseShellExecute = false;
                consola_pre.StartInfo.CreateNoWindow = true;
                consola_pre.EnableRaisingEvents = true;
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

            if (!tt.Wait(2500) && consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                tried_ok = true;
                this.InvokeEx(f => f.btn_start_multi.Enabled = true);
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);

                if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                {
                    foreach (String file in Directory.GetFiles(destino_test))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch
                        {
                        }
                    }
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
                LB1_o.Items.Clear();
                pic_status.Image = img_status.Images[1];
                this.InvokeEx(f => f.btn_start_multi.Enabled = true);
                valid = true;
                preset_status = "OK";
                return;
            }

            if (bad_chars == false)
            {
                if (consola_pre.ExitCode != 0)
                {
                    pic_status.Image = img_status.Images[2];
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    foreach (String lin in LB1_o.Items)
                    {
                        if (lin.Contains("not load the requested plugin") || lin.Contains("Cannot load nvcuda.dll"))
                        {
                            unsupported = true;
                        }
                    }

                    if (unsupported == true) MessageBox.Show(FFBatch.Properties.Strings.enc_fail_second + " " + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.unsup_enc + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.try_pr, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show(FFBatch.Properties.Strings.enc_fail_second + " " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.try_pr, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    tried_ok = false;
                    return;
                }
                else
                {
                    pic_status.Image = img_status.Images[1];
                    valid = true;
                    preset_status = "OK";
                    this.InvokeEx(f => f.btn_start_multi.Enabled = true);
                    System.Threading.Thread.Sleep(50);
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }
                    this.InvokeEx(f => f.btn_start_multi.Enabled = true);
                    tried_ok = true;
                }
            }
            //END try preset
            this.InvokeEx(f => this.Cursor = Cursors.Arrow);

            if (Directory.Exists(destino_test))
            {
                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
            }
            LB1_o.Items.Clear();
            consola_pre.Dispose();
        }

        private void BG_Try_Two_Final_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btn_status.Enabled = true;
            String file_prueba = "";
            String sel_test = lv1_item;

            file_prueba = sel_test;
            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + txt_ext_end_2.Text;

            if (File.Exists(borrar))
            {
                try
                {
                    File.Delete(borrar);
                }
                catch
                {
                }
            }

            if (Directory.Exists(destino) == true)
            {
                if (Directory.GetFiles(destino).Length == 0)
                {
                    System.IO.Directory.Delete(destino);
                }
            }
            if (wizard3.SelectedPage == wz_mpresets)
            {
                wizard3.NextPage(wz_two_end, true);
            }
        }

        private void btn_status_Click(object sender, EventArgs e)
        {
            label5.Focus();
            if (valid == false) BG_Try_1.RunWorkerAsync();
        }

        private void txt_tip_1st_Click(object sender, EventArgs e)
        {
            label5.Focus();
        }

        private void txt_tip_2nd_Click(object sender, EventArgs e)
        {
            label5.Focus();
        }

        private void AeroWizard3_Load(object sender, EventArgs e)
        {
            //if (Properties.Settings.Default.quick_queue == true) chk_target_size.Enabled = false;
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            refresh_lang();
            if (Properties.Settings.Default.app_lang != "en" && Properties.Settings.Default.app_lang != "es")
            {
                wizard3.NextButtonText = Properties.Strings.next;
                wizard3.CancelButtonText = Properties.Strings.cancel;
                wizard3.FinishButtonText = Properties.Strings.finish;
            }
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard3));
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

        private void wizardPage1_Commit_1(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (radio_target.Checked) {
                is_target = true;
                gr_targ.Visible = true;
                combo_size.SelectedIndex = 0;
            } 
            else { 
                is_target = false;
                gr_targ.Visible = false;                
            } 
        }

        private void wz_mpresets_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            if (is_target == true)
            {
                chk_one_pass.Visible = true;
                chk_one_pass.Enabled = true;
                combo_codec_t.Enabled = true;
                combo_codec_t.SelectedIndex = 0;
                profile_target.Enabled = true;
                profile_target.SelectedIndex = 2;
                txt_pr_1.Clear();
                txt_ext_1.Clear();
                txt_ext_1.Text = Path.GetExtension(lv1_item).Remove(0, 1);
                cb_w_presets.Enabled = false;             
                combo_audio_target.Visible = true;
                combo_audio_target.SelectedIndex = 0;
                num_aud_target.Visible = true;
                label14.Visible = true; lbl_bitrate.Visible = true;
                label10.Visible = false;
                lbl_advise_1.Visible = false;
                pic_warn_bitrate.Visible = false;
                label9.Visible = false;             
            }
            else
            {
                chk_one_pass.Visible = false;
                combo_codec_t.Enabled = false;
                profile_target.Enabled = false;
                combo_codec_t.SelectedIndex = -1;
                profile_target.SelectedIndex = -1;
                cb_w_presets.Enabled = true;
                combo_audio_target.Visible = false;
                num_aud_target.Visible = false;
                label14.Visible = false; lbl_bitrate.Visible = false;               
                label10.Visible = true;
                lbl_advise_1.Visible = true;
                pic_warn_bitrate.Visible = true;
                label9.Visible = true;
            }
        }

        private void combo_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_size.SelectedIndex == 0)
            {
                n_target_size.Maximum = 1000;
                n_target_size.Minimum = 1;
                n_target_size.Increment = 10;

            }
            else
            {
                n_target_size.Value = 1;
                n_target_size.Minimum = 1;
                n_target_size.Maximum = 10;
                n_target_size.Increment = 1;
            }
        }

        private void n_target_size_ValueChanged(object sender, EventArgs e)
        {
            if (n_target_size.Value == 11) n_target_size.Value = 10;
        }
    }
}