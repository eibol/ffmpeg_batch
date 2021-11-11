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
            resources.ApplyResources(this.wizard3, "wizard3");
            this.wizard3.BackColor = System.Drawing.Color.White;
            this.wizard3.Name = "wizard3";
            this.wizard3.Pages.Add(this.wz_mpresets);
            this.wizard3.Pages.Add(this.wz_two_end);
            this.wizard3.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizard3_Cancelling);
            this.wizard3.SelectedPageChanged += new System.EventHandler(this.wizard3_SelectedPageChanged);
            // 
            // wz_mpresets
            // 
            resources.ApplyResources(this.wz_mpresets, "wz_mpresets");
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
            this.wz_mpresets.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wizardPage1_Commit);
            // 
            // pic_warn_bitrate
            // 
            resources.ApplyResources(this.pic_warn_bitrate, "pic_warn_bitrate");
            this.pic_warn_bitrate.Name = "pic_warn_bitrate";
            this.pic_warn_bitrate.TabStop = false;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // lbl_advise_1
            // 
            resources.ApplyResources(this.lbl_advise_1, "lbl_advise_1");
            this.lbl_advise_1.Name = "lbl_advise_1";
            // 
            // btn_tips_1
            // 
            resources.ApplyResources(this.btn_tips_1, "btn_tips_1");
            this.btn_tips_1.FlatAppearance.BorderSize = 0;
            this.btn_tips_1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_tips_1.Name = "btn_tips_1";
            this.btn_tips_1.UseVisualStyleBackColor = true;
            this.btn_tips_1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txt_ext_1
            // 
            resources.ApplyResources(this.txt_ext_1, "txt_ext_1");
            this.txt_ext_1.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_1.Name = "txt_ext_1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txt_pr_1
            // 
            resources.ApplyResources(this.txt_pr_1, "txt_pr_1");
            this.txt_pr_1.Name = "txt_pr_1";
            this.txt_pr_1.TextChanged += new System.EventHandler(this.txt_pr_1_TextChanged);
            // 
            // cb_w_presets
            // 
            resources.ApplyResources(this.cb_w_presets, "cb_w_presets");
            this.cb_w_presets.FormattingEnabled = true;
            this.cb_w_presets.Name = "cb_w_presets";
            this.cb_w_presets.SelectedIndexChanged += new System.EventHandler(this.cb_w_presets_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // wz_two_end
            // 
            resources.ApplyResources(this.wz_two_end, "wz_two_end");
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
            this.wz_two_end.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz_two_end_Commit);
            // 
            // pic_status
            // 
            resources.ApplyResources(this.pic_status, "pic_status");
            this.pic_status.Name = "pic_status";
            this.pic_status.TabStop = false;
            // 
            // btn_status
            // 
            resources.ApplyResources(this.btn_status, "btn_status");
            this.btn_status.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_status.FlatAppearance.BorderSize = 0;
            this.btn_status.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_status.Name = "btn_status";
            this.btn_status.UseVisualStyleBackColor = false;
            this.btn_status.Click += new System.EventHandler(this.btn_status_Click);
            // 
            // txt_tip_2nd
            // 
            resources.ApplyResources(this.txt_tip_2nd, "txt_tip_2nd");
            this.txt_tip_2nd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_tip_2nd.Name = "txt_tip_2nd";
            this.txt_tip_2nd.Click += new System.EventHandler(this.txt_tip_2nd_Click);
            // 
            // txt_tip_1st
            // 
            resources.ApplyResources(this.txt_tip_1st, "txt_tip_1st");
            this.txt_tip_1st.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_tip_1st.Name = "txt_tip_1st";
            this.txt_tip_1st.Click += new System.EventHandler(this.txt_tip_1st_Click);
            // 
            // btn_abort
            // 
            resources.ApplyResources(this.btn_abort, "btn_abort");
            this.btn_abort.FlatAppearance.BorderSize = 0;
            this.btn_abort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // btn_start_multi
            // 
            resources.ApplyResources(this.btn_start_multi, "btn_start_multi");
            this.btn_start_multi.FlatAppearance.BorderSize = 0;
            this.btn_start_multi.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_start_multi.Name = "btn_start_multi";
            this.btn_start_multi.UseVisualStyleBackColor = true;
            this.btn_start_multi.Click += new System.EventHandler(this.btn_start_multi_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // btn_tips
            // 
            resources.ApplyResources(this.btn_tips, "btn_tips");
            this.btn_tips.FlatAppearance.BorderSize = 0;
            this.btn_tips.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_tips.Name = "btn_tips";
            this.btn_tips.UseVisualStyleBackColor = true;
            this.btn_tips.Click += new System.EventHandler(this.btn_tips_Click);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // txt_ext_end_2
            // 
            resources.ApplyResources(this.txt_ext_end_2, "txt_ext_end_2");
            this.txt_ext_end_2.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_end_2.Name = "txt_ext_end_2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txt_pr_end_2
            // 
            resources.ApplyResources(this.txt_pr_end_2, "txt_pr_end_2");
            this.txt_pr_end_2.Name = "txt_pr_end_2";
            this.txt_pr_end_2.TextChanged += new System.EventHandler(this.txt_pr_end_2_TextChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_pr_ext_end
            // 
            resources.ApplyResources(this.txt_pr_ext_end, "txt_pr_ext_end");
            this.txt_pr_ext_end.BackColor = System.Drawing.SystemColors.Info;
            this.txt_pr_ext_end.Name = "txt_pr_ext_end";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_pr_two_end
            // 
            resources.ApplyResources(this.txt_pr_two_end, "txt_pr_two_end");
            this.txt_pr_two_end.Name = "txt_pr_two_end";
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
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.wizard3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AeroWizard3";
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