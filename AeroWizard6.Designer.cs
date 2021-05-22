namespace FFBatch
{
    partial class AeroWizard6
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard6));
            this.fd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chk_save_pres = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_start_multi = new System.Windows.Forms.Button();
            this.btn_abort = new System.Windows.Forms.Button();
            this.lbl_warn = new System.Windows.Forms.Label();
            this.pic_warn_two = new System.Windows.Forms.PictureBox();
            this.check_name = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.wz_final = new AeroWizard.WizardPage();
            this.wz1 = new AeroWizard.WizardPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.txt_naming = new System.Windows.Forms.TextBox();
            this.txt_path_main = new System.Windows.Forms.TextBox();
            this.chk_out_name = new System.Windows.Forms.CheckBox();
            this.btn_browse = new System.Windows.Forms.Button();
            this.radio_absolute = new System.Windows.Forms.RadioButton();
            this.radio_relative = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_streamcopy = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_Seconds = new System.Windows.Forms.ComboBox();
            this.radio_time = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.combo_ext = new System.Windows.Forms.ComboBox();
            this.wiz_split = new AeroWizard.WizardControl();
            this.wz_end = new AeroWizard.WizardPage();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_preset_name = new System.Windows.Forms.TextBox();
            this.chk_save_preset = new System.Windows.Forms.CheckBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_warn_two)).BeginInit();
            this.wz_final.SuspendLayout();
            this.wz1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wiz_split)).BeginInit();
            this.wz_end.SuspendLayout();
            this.SuspendLayout();
            // 
            // chk_save_pres
            // 
            this.chk_save_pres.Location = new System.Drawing.Point(0, 0);
            this.chk_save_pres.Name = "chk_save_pres";
            this.chk_save_pres.Size = new System.Drawing.Size(104, 24);
            this.chk_save_pres.TabIndex = 141;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 13);
            this.label4.TabIndex = 106;
            this.label4.Text = "The image extraction preset wizard is ready.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(237, 13);
            this.label5.TabIndex = 107;
            this.label5.Text = "Press Finish or Close button to return to main tab.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(31, 193);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(266, 13);
            this.label6.TabIndex = 108;
            this.label6.Text = "Press Start button to start encoding with current preset.";
            // 
            // btn_start_multi
            // 
            this.btn_start_multi.FlatAppearance.BorderSize = 0;
            this.btn_start_multi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_start_multi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start_multi.Image = ((System.Drawing.Image)(resources.GetObject("btn_start_multi.Image")));
            this.btn_start_multi.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_start_multi.Location = new System.Drawing.Point(387, 174);
            this.btn_start_multi.Name = "btn_start_multi";
            this.btn_start_multi.Size = new System.Drawing.Size(49, 72);
            this.btn_start_multi.TabIndex = 109;
            this.btn_start_multi.Text = "Start ";
            this.btn_start_multi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_start_multi.UseVisualStyleBackColor = true;
            // 
            // btn_abort
            // 
            this.btn_abort.FlatAppearance.BorderSize = 0;
            this.btn_abort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_abort.Image = ((System.Drawing.Image)(resources.GetObject("btn_abort.Image")));
            this.btn_abort.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_abort.Location = new System.Drawing.Point(447, 174);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(60, 74);
            this.btn_abort.TabIndex = 110;
            this.btn_abort.Text = "Close";
            this.btn_abort.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_abort.UseVisualStyleBackColor = true;
            // 
            // lbl_warn
            // 
            this.lbl_warn.AutoSize = true;
            this.lbl_warn.Location = new System.Drawing.Point(31, 228);
            this.lbl_warn.Name = "lbl_warn";
            this.lbl_warn.Size = new System.Drawing.Size(182, 13);
            this.lbl_warn.TabIndex = 137;
            this.lbl_warn.Text = "Absolute path can lead to overwriting";
            this.lbl_warn.Visible = false;
            // 
            // pic_warn_two
            // 
            this.pic_warn_two.Image = ((System.Drawing.Image)(resources.GetObject("pic_warn_two.Image")));
            this.pic_warn_two.Location = new System.Drawing.Point(240, 226);
            this.pic_warn_two.Name = "pic_warn_two";
            this.pic_warn_two.Size = new System.Drawing.Size(21, 22);
            this.pic_warn_two.TabIndex = 138;
            this.pic_warn_two.TabStop = false;
            this.pic_warn_two.Visible = false;
            // 
            // check_name
            // 
            this.check_name.AutoSize = true;
            this.check_name.Checked = true;
            this.check_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_name.Location = new System.Drawing.Point(34, 64);
            this.check_name.Name = "check_name";
            this.check_name.Size = new System.Drawing.Size(170, 17);
            this.check_name.TabIndex = 139;
            this.check_name.Text = "Use source filename for output";
            this.check_name.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(234, 62);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 20);
            this.textBox1.TabIndex = 140;
            // 
            // wz_final
            // 
            this.wz_final.Controls.Add(this.textBox1);
            this.wz_final.Controls.Add(this.check_name);
            this.wz_final.Controls.Add(this.pic_warn_two);
            this.wz_final.Controls.Add(this.lbl_warn);
            this.wz_final.Controls.Add(this.btn_abort);
            this.wz_final.Controls.Add(this.btn_start_multi);
            this.wz_final.Controls.Add(this.label6);
            this.wz_final.Controls.Add(this.label5);
            this.wz_final.Controls.Add(this.label4);
            this.wz_final.Controls.Add(this.chk_save_pres);
            this.wz_final.IsFinishPage = true;
            this.wz_final.Name = "wz_final";
            this.wz_final.Size = new System.Drawing.Size(527, 261);
            this.wz_final.TabIndex = 1;
            this.wz_final.Text = "Preset complete";
            // 
            // wz1
            // 
            this.wz1.Controls.Add(this.groupBox2);
            this.wz1.Controls.Add(this.groupBox1);
            this.wz1.Name = "wz1";
            this.wz1.Size = new System.Drawing.Size(527, 261);
            this.wz1.TabIndex = 0;
            this.wz1.Text = "Batch A/V Split Wizard";
            this.wz1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz1_Commit);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_path);
            this.groupBox2.Controls.Add(this.txt_naming);
            this.groupBox2.Controls.Add(this.txt_path_main);
            this.groupBox2.Controls.Add(this.chk_out_name);
            this.groupBox2.Controls.Add(this.btn_browse);
            this.groupBox2.Controls.Add(this.radio_absolute);
            this.groupBox2.Controls.Add(this.radio_relative);
            this.groupBox2.Location = new System.Drawing.Point(17, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(491, 92);
            this.groupBox2.TabIndex = 137;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // txt_path
            // 
            this.txt_path.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_path.Enabled = false;
            this.txt_path.Location = new System.Drawing.Point(162, 57);
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            this.txt_path.Size = new System.Drawing.Size(259, 23);
            this.txt_path.TabIndex = 131;
            // 
            // txt_naming
            // 
            this.txt_naming.AcceptsReturn = true;
            this.txt_naming.Enabled = false;
            this.txt_naming.Location = new System.Drawing.Point(424, 24);
            this.txt_naming.Name = "txt_naming";
            this.txt_naming.Size = new System.Drawing.Size(62, 23);
            this.txt_naming.TabIndex = 136;
            // 
            // txt_path_main
            // 
            this.txt_path_main.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_path_main.Location = new System.Drawing.Point(162, 24);
            this.txt_path_main.Name = "txt_path_main";
            this.txt_path_main.Size = new System.Drawing.Size(88, 23);
            this.txt_path_main.TabIndex = 124;
            this.txt_path_main.Text = ".\\FF_Split";
            this.txt_path_main.TextChanged += new System.EventHandler(this.txt_path_main_TextChanged);
            // 
            // chk_out_name
            // 
            this.chk_out_name.AutoSize = true;
            this.chk_out_name.Checked = true;
            this.chk_out_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_out_name.Location = new System.Drawing.Point(290, 26);
            this.chk_out_name.Name = "chk_out_name";
            this.chk_out_name.Size = new System.Drawing.Size(132, 19);
            this.chk_out_name.TabIndex = 135;
            this.chk_out_name.Text = "Use source filename";
            this.chk_out_name.UseVisualStyleBackColor = true;
            this.chk_out_name.CheckedChanged += new System.EventHandler(this.chk_out_name_CheckedChanged);
            // 
            // btn_browse
            // 
            this.btn_browse.Enabled = false;
            this.btn_browse.FlatAppearance.BorderSize = 0;
            this.btn_browse.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_browse.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_browse.Location = new System.Drawing.Point(424, 57);
            this.btn_browse.Name = "btn_browse";
            this.btn_browse.Size = new System.Drawing.Size(62, 22);
            this.btn_browse.TabIndex = 132;
            this.btn_browse.Text = "Browse";
            this.btn_browse.UseVisualStyleBackColor = true;
            this.btn_browse.Click += new System.EventHandler(this.button1_Click);
            // 
            // radio_absolute
            // 
            this.radio_absolute.AutoSize = true;
            this.radio_absolute.Location = new System.Drawing.Point(21, 59);
            this.radio_absolute.Name = "radio_absolute";
            this.radio_absolute.Size = new System.Drawing.Size(138, 19);
            this.radio_absolute.TabIndex = 134;
            this.radio_absolute.Text = "Absolute output path";
            this.radio_absolute.UseVisualStyleBackColor = true;
            this.radio_absolute.CheckedChanged += new System.EventHandler(this.radio_absolute_CheckedChanged);
            // 
            // radio_relative
            // 
            this.radio_relative.AutoSize = true;
            this.radio_relative.Checked = true;
            this.radio_relative.Location = new System.Drawing.Point(21, 26);
            this.radio_relative.Name = "radio_relative";
            this.radio_relative.Size = new System.Drawing.Size(132, 19);
            this.radio_relative.TabIndex = 133;
            this.radio_relative.TabStop = true;
            this.radio_relative.Text = "Relative output path";
            this.radio_relative.UseVisualStyleBackColor = true;
            this.radio_relative.CheckedChanged += new System.EventHandler(this.radio_relative_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_streamcopy);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.combo_Seconds);
            this.groupBox1.Controls.Add(this.radio_time);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.combo_ext);
            this.groupBox1.Location = new System.Drawing.Point(17, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 102);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Split";
            // 
            // chk_streamcopy
            // 
            this.chk_streamcopy.AutoSize = true;
            this.chk_streamcopy.Checked = true;
            this.chk_streamcopy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_streamcopy.Location = new System.Drawing.Point(87, 67);
            this.chk_streamcopy.Name = "chk_streamcopy";
            this.chk_streamcopy.Size = new System.Drawing.Size(92, 19);
            this.chk_streamcopy.TabIndex = 137;
            this.chk_streamcopy.Text = "Stream copy";
            this.chk_streamcopy.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "second(s)";
            // 
            // combo_Seconds
            // 
            this.combo_Seconds.FormattingEnabled = true;
            this.combo_Seconds.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "30",
            "60",
            "300",
            "600"});
            this.combo_Seconds.Location = new System.Drawing.Point(87, 26);
            this.combo_Seconds.Name = "combo_Seconds";
            this.combo_Seconds.Size = new System.Drawing.Size(66, 23);
            this.combo_Seconds.TabIndex = 3;
            this.combo_Seconds.TextChanged += new System.EventHandler(this.combo_Seconds_TextChanged);
            // 
            // radio_time
            // 
            this.radio_time.AutoSize = true;
            this.radio_time.Checked = true;
            this.radio_time.Location = new System.Drawing.Point(25, 27);
            this.radio_time.Name = "radio_time";
            this.radio_time.Size = new System.Drawing.Size(56, 19);
            this.radio_time.TabIndex = 1;
            this.radio_time.TabStop = true;
            this.radio_time.Text = "Every ";
            this.radio_time.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(250, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "Output format";
            // 
            // combo_ext
            // 
            this.combo_ext.FormattingEnabled = true;
            this.combo_ext.Items.AddRange(new object[] {
            "mp4",
            "mov",
            "mkv",
            "mp3",
            "flac",
            "wav"});
            this.combo_ext.Location = new System.Drawing.Point(338, 28);
            this.combo_ext.Name = "combo_ext";
            this.combo_ext.Size = new System.Drawing.Size(110, 23);
            this.combo_ext.TabIndex = 8;
            // 
            // wiz_split
            // 
            this.wiz_split.BackColor = System.Drawing.Color.White;
            this.wiz_split.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wiz_split.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wiz_split.Location = new System.Drawing.Point(0, 0);
            this.wiz_split.Name = "wiz_split";
            this.wiz_split.Pages.Add(this.wz1);
            this.wiz_split.Pages.Add(this.wz_end);
            this.wiz_split.Size = new System.Drawing.Size(574, 415);
            this.wiz_split.TabIndex = 0;
            this.wiz_split.Title = "FFmpeg Batch A/V Split Wizard";
            this.wiz_split.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wiz_split.TitleIcon")));
            this.wiz_split.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            // 
            // wz_end
            // 
            this.wz_end.Controls.Add(this.label9);
            this.wz_end.Controls.Add(this.label8);
            this.wz_end.Controls.Add(this.label7);
            this.wz_end.Controls.Add(this.txt_preset_name);
            this.wz_end.Controls.Add(this.chk_save_preset);
            this.wz_end.Controls.Add(this.btn_cancel);
            this.wz_end.Controls.Add(this.btn_Start);
            this.wz_end.IsFinishPage = true;
            this.wz_end.Name = "wz_end";
            this.wz_end.Size = new System.Drawing.Size(527, 261);
            this.wz_end.TabIndex = 1;
            this.wz_end.Text = "Preset complete";
            this.wz_end.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz_end_Commit);
            this.wz_end.Initialize += new System.EventHandler<AeroWizard.WizardPageInitEventArgs>(this.wz_end_Initialize);
            this.wz_end.Rollback += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz_end_Rollback);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(27, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(256, 15);
            this.label9.TabIndex = 95;
            this.label9.Text = "Press Start button to start file split immediately.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(27, 119);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(310, 15);
            this.label8.TabIndex = 94;
            this.label8.Text = "Press Finish to use preset, or Cancel to return to main tab.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(27, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 15);
            this.label7.TabIndex = 93;
            this.label7.Text = "The split preset wizard is ready.";
            // 
            // txt_preset_name
            // 
            this.txt_preset_name.Enabled = false;
            this.txt_preset_name.Location = new System.Drawing.Point(165, 68);
            this.txt_preset_name.Name = "txt_preset_name";
            this.txt_preset_name.Size = new System.Drawing.Size(323, 23);
            this.txt_preset_name.TabIndex = 92;
            // 
            // chk_save_preset
            // 
            this.chk_save_preset.AutoSize = true;
            this.chk_save_preset.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_save_preset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_save_preset.Location = new System.Drawing.Point(27, 71);
            this.chk_save_preset.Name = "chk_save_preset";
            this.chk_save_preset.Size = new System.Drawing.Size(134, 19);
            this.chk_save_preset.TabIndex = 91;
            this.chk_save_preset.Text = "Save selected  preset";
            this.chk_save_preset.UseVisualStyleBackColor = true;
            this.chk_save_preset.CheckedChanged += new System.EventHandler(this.chk_save_preset_CheckedChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cancel.Location = new System.Drawing.Point(455, 185);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(60, 74);
            this.btn_cancel.TabIndex = 90;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.FlatAppearance.BorderSize = 0;
            this.btn_Start.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Start.Image = ((System.Drawing.Image)(resources.GetObject("btn_Start.Image")));
            this.btn_Start.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Start.Location = new System.Drawing.Point(393, 183);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(63, 75);
            this.btn_Start.TabIndex = 89;
            this.btn_Start.Text = "Start";
            this.btn_Start.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // AeroWizard6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 415);
            this.Controls.Add(this.wiz_split);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AeroWizard6";
            ((System.ComponentModel.ISupportInitialize)(this.pic_warn_two)).EndInit();
            this.wz_final.ResumeLayout(false);
            this.wz_final.PerformLayout();
            this.wz1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wiz_split)).EndInit();
            this.wz_end.ResumeLayout(false);
            this.wz_end.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FolderBrowserDialog fd1;
        private System.Windows.Forms.CheckBox chk_save_pres;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_start_multi;
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Label lbl_warn;
        private System.Windows.Forms.PictureBox pic_warn_two;
        private System.Windows.Forms.CheckBox check_name;
        private System.Windows.Forms.TextBox textBox1;
        private AeroWizard.WizardPage wz_final;
        private AeroWizard.WizardPage wz1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radio_absolute;
        private System.Windows.Forms.RadioButton radio_relative;
        private System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Button btn_browse;
        private System.Windows.Forms.TextBox txt_path_main;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radio_time;
        private AeroWizard.WizardControl wiz_split;
        private AeroWizard.WizardPage wz_end;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.CheckBox chk_save_preset;
        private System.Windows.Forms.TextBox txt_naming;
        private System.Windows.Forms.CheckBox chk_out_name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox txt_preset_name;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_streamcopy;
        public System.Windows.Forms.ComboBox combo_ext;
        public System.Windows.Forms.ComboBox combo_Seconds;
    }
}