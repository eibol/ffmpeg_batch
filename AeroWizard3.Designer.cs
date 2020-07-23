namespace FFBatch
{
    partial class AeroWizard3
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard3));
            this.wizard3 = new AeroWizard.WizardControl();
            this.wz_mpresets = new AeroWizard.WizardPage();
            this.pic_warn_bitrate = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_advise_1 = new System.Windows.Forms.Label();
            this.btn_tips_1 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_ext_1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pr_1 = new System.Windows.Forms.TextBox();
            this.cb_w_presets = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wz_two_end = new AeroWizard.WizardPage();
            this.pic_status = new System.Windows.Forms.PictureBox();
            this.btn_status = new System.Windows.Forms.Button();
            this.txt_tip_2nd = new System.Windows.Forms.TextBox();
            this.txt_tip_1st = new System.Windows.Forms.TextBox();
            this.btn_abort = new System.Windows.Forms.Button();
            this.btn_start_multi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_tips = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ext_end_2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_pr_end_2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_pr_ext_end = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_pr_two_end = new System.Windows.Forms.TextBox();
            this.BG_Try_1 = new System.ComponentModel.BackgroundWorker();
            this.img_status = new System.Windows.Forms.ImageList(this.components);
            this.BG_Try_Two_Final = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.wizard3)).BeginInit();
            this.wz_mpresets.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_warn_bitrate)).BeginInit();
            this.wz_two_end.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_status)).BeginInit();
            this.SuspendLayout();
            // 
            // wizard3
            // 
            this.wizard3.BackColor = System.Drawing.Color.White;
            this.wizard3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard3.Location = new System.Drawing.Point(0, 0);
            this.wizard3.Name = "wizard3";
            this.wizard3.Pages.Add(this.wz_mpresets);
            this.wizard3.Pages.Add(this.wz_two_end);
            this.wizard3.Size = new System.Drawing.Size(574, 415);
            this.wizard3.TabIndex = 0;
            this.wizard3.Title = "Two pass video encoding wizard";
            this.wizard3.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizard3.TitleIcon")));
            this.wizard3.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizard3_Cancelling);
            this.wizard3.SelectedPageChanged += new System.EventHandler(this.wizard3_SelectedPageChanged);
            // 
            // wz_mpresets
            // 
            this.wz_mpresets.Controls.Add(this.pic_warn_bitrate);
            this.wz_mpresets.Controls.Add(this.label10);
            this.wz_mpresets.Controls.Add(this.lbl_advise_1);
            this.wz_mpresets.Controls.Add(this.btn_tips_1);
            this.wz_mpresets.Controls.Add(this.label9);
            this.wz_mpresets.Controls.Add(this.txt_ext_1);
            this.wz_mpresets.Controls.Add(this.label2);
            this.wz_mpresets.Controls.Add(this.txt_pr_1);
            this.wz_mpresets.Controls.Add(this.cb_w_presets);
            this.wz_mpresets.Controls.Add(this.label3);
            this.wz_mpresets.Name = "wz_mpresets";
            this.wz_mpresets.NextPage = this.wz_two_end;
            this.wz_mpresets.Size = new System.Drawing.Size(527, 261);
            this.wz_mpresets.TabIndex = 0;
            this.wz_mpresets.Text = "Two pass encoding wizard";
            this.wz_mpresets.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            // 
            // pic_warn_bitrate
            // 
            this.pic_warn_bitrate.Image = ((System.Drawing.Image)(resources.GetObject("pic_warn_bitrate.Image")));
            this.pic_warn_bitrate.Location = new System.Drawing.Point(67, 215);
            this.pic_warn_bitrate.Name = "pic_warn_bitrate";
            this.pic_warn_bitrate.Size = new System.Drawing.Size(21, 22);
            this.pic_warn_bitrate.TabIndex = 95;
            this.pic_warn_bitrate.TabStop = false;
            this.pic_warn_bitrate.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(64, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(259, 15);
            this.label10.TabIndex = 94;
            this.label10.Text = "Required parameters will be added on next step.";
            // 
            // lbl_advise_1
            // 
            this.lbl_advise_1.AutoSize = true;
            this.lbl_advise_1.Location = new System.Drawing.Point(92, 215);
            this.lbl_advise_1.Name = "lbl_advise_1";
            this.lbl_advise_1.Size = new System.Drawing.Size(0, 15);
            this.lbl_advise_1.TabIndex = 93;
            // 
            // btn_tips_1
            // 
            this.btn_tips_1.FlatAppearance.BorderSize = 0;
            this.btn_tips_1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_tips_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tips_1.Image = ((System.Drawing.Image)(resources.GetObject("btn_tips_1.Image")));
            this.btn_tips_1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_tips_1.Location = new System.Drawing.Point(466, 8);
            this.btn_tips_1.Name = "btn_tips_1";
            this.btn_tips_1.Size = new System.Drawing.Size(35, 31);
            this.btn_tips_1.TabIndex = 92;
            this.btn_tips_1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_tips_1.UseVisualStyleBackColor = true;
            this.btn_tips_1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(255, 15);
            this.label9.TabIndex = 58;
            this.label9.Text = "Select base preset for two pass video encoding:";
            // 
            // txt_ext_1
            // 
            this.txt_ext_1.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_1.Location = new System.Drawing.Point(471, 117);
            this.txt_ext_1.MaxLength = 4;
            this.txt_ext_1.Name = "txt_ext_1";
            this.txt_ext_1.Size = new System.Drawing.Size(30, 23);
            this.txt_ext_1.TabIndex = 56;
            this.txt_ext_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(424, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 55;
            this.label2.Text = "Format";
            // 
            // txt_pr_1
            // 
            this.txt_pr_1.Location = new System.Drawing.Point(67, 108);
            this.txt_pr_1.MaxLength = 500;
            this.txt_pr_1.Multiline = true;
            this.txt_pr_1.Name = "txt_pr_1";
            this.txt_pr_1.Size = new System.Drawing.Size(354, 40);
            this.txt_pr_1.TabIndex = 54;
            this.txt_pr_1.TextChanged += new System.EventHandler(this.txt_pr_1_TextChanged);
            // 
            // cb_w_presets
            // 
            this.cb_w_presets.FormattingEnabled = true;
            this.cb_w_presets.Location = new System.Drawing.Point(67, 64);
            this.cb_w_presets.Name = "cb_w_presets";
            this.cb_w_presets.Size = new System.Drawing.Size(434, 23);
            this.cb_w_presets.TabIndex = 52;
            this.cb_w_presets.SelectedIndexChanged += new System.EventHandler(this.cb_w_presets_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 53;
            this.label3.Text = "Presets";
            // 
            // wz_two_end
            // 
            this.wz_two_end.Controls.Add(this.pic_status);
            this.wz_two_end.Controls.Add(this.btn_status);
            this.wz_two_end.Controls.Add(this.txt_tip_2nd);
            this.wz_two_end.Controls.Add(this.txt_tip_1st);
            this.wz_two_end.Controls.Add(this.btn_abort);
            this.wz_two_end.Controls.Add(this.btn_start_multi);
            this.wz_two_end.Controls.Add(this.label1);
            this.wz_two_end.Controls.Add(this.btn_tips);
            this.wz_two_end.Controls.Add(this.label8);
            this.wz_two_end.Controls.Add(this.txt_ext_end_2);
            this.wz_two_end.Controls.Add(this.label6);
            this.wz_two_end.Controls.Add(this.txt_pr_end_2);
            this.wz_two_end.Controls.Add(this.label7);
            this.wz_two_end.Controls.Add(this.label5);
            this.wz_two_end.Controls.Add(this.txt_pr_ext_end);
            this.wz_two_end.Controls.Add(this.label4);
            this.wz_two_end.Controls.Add(this.txt_pr_two_end);
            this.wz_two_end.IsFinishPage = true;
            this.wz_two_end.Name = "wz_two_end";
            this.wz_two_end.Size = new System.Drawing.Size(527, 261);
            this.wz_two_end.TabIndex = 1;
            this.wz_two_end.Text = "Ready to start two pass encoding";
            this.wz_two_end.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz_two_end_Commit);
            // 
            // pic_status
            // 
            this.pic_status.Location = new System.Drawing.Point(121, 202);
            this.pic_status.Name = "pic_status";
            this.pic_status.Size = new System.Drawing.Size(28, 28);
            this.pic_status.TabIndex = 97;
            this.pic_status.TabStop = false;
            // 
            // btn_status
            // 
            this.btn_status.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_status.FlatAppearance.BorderSize = 0;
            this.btn_status.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_status.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_status.Location = new System.Drawing.Point(21, 201);
            this.btn_status.Name = "btn_status";
            this.btn_status.Size = new System.Drawing.Size(96, 29);
            this.btn_status.TabIndex = 96;
            this.btn_status.Text = "Preset status";
            this.btn_status.UseVisualStyleBackColor = false;
            this.btn_status.Click += new System.EventHandler(this.btn_status_Click);
            // 
            // txt_tip_2nd
            // 
            this.txt_tip_2nd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_tip_2nd.Location = new System.Drawing.Point(117, 116);
            this.txt_tip_2nd.Name = "txt_tip_2nd";
            this.txt_tip_2nd.Size = new System.Drawing.Size(302, 16);
            this.txt_tip_2nd.TabIndex = 95;
            this.txt_tip_2nd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tip_2nd.Click += new System.EventHandler(this.txt_tip_2nd_Click);
            // 
            // txt_tip_1st
            // 
            this.txt_tip_1st.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_tip_1st.Location = new System.Drawing.Point(117, 49);
            this.txt_tip_1st.Name = "txt_tip_1st";
            this.txt_tip_1st.Size = new System.Drawing.Size(302, 16);
            this.txt_tip_1st.TabIndex = 94;
            this.txt_tip_1st.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_tip_1st.Click += new System.EventHandler(this.txt_tip_1st_Click);
            // 
            // btn_abort
            // 
            this.btn_abort.FlatAppearance.BorderSize = 0;
            this.btn_abort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_abort.Image = ((System.Drawing.Image)(resources.GetObject("btn_abort.Image")));
            this.btn_abort.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_abort.Location = new System.Drawing.Point(248, 185);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(57, 73);
            this.btn_abort.TabIndex = 90;
            this.btn_abort.Text = "Cancel";
            this.btn_abort.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // btn_start_multi
            // 
            this.btn_start_multi.FlatAppearance.BorderSize = 0;
            this.btn_start_multi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_start_multi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start_multi.Image = ((System.Drawing.Image)(resources.GetObject("btn_start_multi.Image")));
            this.btn_start_multi.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_start_multi.Location = new System.Drawing.Point(193, 185);
            this.btn_start_multi.Name = "btn_start_multi";
            this.btn_start_multi.Size = new System.Drawing.Size(49, 72);
            this.btn_start_multi.TabIndex = 89;
            this.btn_start_multi.Text = "Start ";
            this.btn_start_multi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_start_multi.UseVisualStyleBackColor = true;
            this.btn_start_multi.Click += new System.EventHandler(this.btn_start_multi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(174, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 93;
            // 
            // btn_tips
            // 
            this.btn_tips.FlatAppearance.BorderSize = 0;
            this.btn_tips.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_tips.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_tips.Image = ((System.Drawing.Image)(resources.GetObject("btn_tips.Image")));
            this.btn_tips.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_tips.Location = new System.Drawing.Point(466, 8);
            this.btn_tips.Name = "btn_tips";
            this.btn_tips.Size = new System.Drawing.Size(35, 32);
            this.btn_tips.TabIndex = 91;
            this.btn_tips.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_tips.UseVisualStyleBackColor = true;
            this.btn_tips.Click += new System.EventHandler(this.btn_tips_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "Second pass";
            // 
            // txt_ext_end_2
            // 
            this.txt_ext_end_2.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_end_2.Location = new System.Drawing.Point(471, 143);
            this.txt_ext_end_2.MaxLength = 4;
            this.txt_ext_end_2.Name = "txt_ext_end_2";
            this.txt_ext_end_2.Size = new System.Drawing.Size(30, 23);
            this.txt_ext_end_2.TabIndex = 65;
            this.txt_ext_end_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(424, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 15);
            this.label6.TabIndex = 64;
            this.label6.Text = "Format";
            // 
            // txt_pr_end_2
            // 
            this.txt_pr_end_2.Location = new System.Drawing.Point(19, 135);
            this.txt_pr_end_2.MaxLength = 500;
            this.txt_pr_end_2.Multiline = true;
            this.txt_pr_end_2.Name = "txt_pr_end_2";
            this.txt_pr_end_2.Size = new System.Drawing.Size(402, 36);
            this.txt_pr_end_2.TabIndex = 63;
            this.txt_pr_end_2.TextChanged += new System.EventHandler(this.txt_pr_end_2_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 62;
            this.label7.Text = "First pass";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(284, 15);
            this.label5.TabIndex = 60;
            this.label5.Text = "The wizard is ready to start two pass video encoding:";
            // 
            // txt_pr_ext_end
            // 
            this.txt_pr_ext_end.BackColor = System.Drawing.SystemColors.Info;
            this.txt_pr_ext_end.Enabled = false;
            this.txt_pr_ext_end.Location = new System.Drawing.Point(471, 75);
            this.txt_pr_ext_end.MaxLength = 4;
            this.txt_pr_ext_end.Name = "txt_pr_ext_end";
            this.txt_pr_ext_end.Size = new System.Drawing.Size(30, 23);
            this.txt_pr_ext_end.TabIndex = 59;
            this.txt_pr_ext_end.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(424, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 15);
            this.label4.TabIndex = 58;
            this.label4.Text = "Format";
            // 
            // txt_pr_two_end
            // 
            this.txt_pr_two_end.Location = new System.Drawing.Point(19, 68);
            this.txt_pr_two_end.MaxLength = 500;
            this.txt_pr_two_end.Multiline = true;
            this.txt_pr_two_end.Name = "txt_pr_two_end";
            this.txt_pr_two_end.Size = new System.Drawing.Size(402, 36);
            this.txt_pr_two_end.TabIndex = 57;
            this.txt_pr_two_end.TextChanged += new System.EventHandler(this.txt_pr_two_end_TextChanged);
            // 
            // BG_Try_1
            // 
            this.BG_Try_1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BG_Try_1_DoWork);
            this.BG_Try_1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BG_Try_1_RunWorkerCompleted);
            // 
            // img_status
            // 
            this.img_status.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_status.ImageStream")));
            this.img_status.TransparentColor = System.Drawing.Color.Transparent;
            this.img_status.Images.SetKeyName(0, "question_24_3.png");
            this.img_status.Images.SetKeyName(1, "check_24.png");
            this.img_status.Images.SetKeyName(2, "warning_24.png");
            // 
            // BG_Try_Two_Final
            // 
            this.BG_Try_Two_Final.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BG_Try_Two_Final_DoWork);
            this.BG_Try_Two_Final.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BG_Try_Two_Final_RunWorkerCompleted);
            // 
            // AeroWizard3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 415);
            this.ControlBox = false;
            this.Controls.Add(this.wizard3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AeroWizard3";
            this.Text = "FFmpeg Batch A/V Encoder";
            this.Load += new System.EventHandler(this.AeroWizard3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wizard3)).EndInit();
            this.wz_mpresets.ResumeLayout(false);
            this.wz_mpresets.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_warn_bitrate)).EndInit();
            this.wz_two_end.ResumeLayout(false);
            this.wz_two_end.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_status)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizard3;
        private AeroWizard.WizardPage wz_mpresets;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_w_presets;
        private System.Windows.Forms.Label label3;
        private AeroWizard.WizardPage wz_two_end;
        private System.Windows.Forms.TextBox txt_pr_ext_end;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_pr_two_end;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ext_end_2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_pr_end_2;
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Button btn_start_multi;
        private System.Windows.Forms.Button btn_tips;
        private System.Windows.Forms.Button btn_tips_1;
        private System.Windows.Forms.Label lbl_advise_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_tip_1st;
        private System.Windows.Forms.TextBox txt_tip_2nd;
        private System.ComponentModel.BackgroundWorker BG_Try_1;
        private System.Windows.Forms.PictureBox pic_status;
        private System.Windows.Forms.Button btn_status;
        private System.Windows.Forms.ImageList img_status;
        private System.ComponentModel.BackgroundWorker BG_Try_Two_Final;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox pic_warn_bitrate;
        public System.Windows.Forms.TextBox txt_ext_1;
        public System.Windows.Forms.TextBox txt_pr_1;
    }
}