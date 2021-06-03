namespace FFBatch
{
    partial class AeroWizard2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard2));
            this.wz_mpresets = new AeroWizard.WizardControl();
            this.wzp1 = new AeroWizard.WizardPage();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_clear = new System.Windows.Forms.Button();
            this.txt_ext_3 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_pr_3 = new System.Windows.Forms.TextBox();
            this.txt_ext_2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_pr_2 = new System.Windows.Forms.TextBox();
            this.txt_ext_1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_pr_1 = new System.Windows.Forms.TextBox();
            this.cb_w_presets = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.wzp2 = new AeroWizard.WizardPage();
            this.btn_abort = new System.Windows.Forms.Button();
            this.btn_start_multi = new System.Windows.Forms.Button();
            this.pic_warning = new System.Windows.Forms.PictureBox();
            this.lbl_ovw = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wz_mpresets)).BeginInit();
            this.wzp1.SuspendLayout();
            this.wzp2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_warning)).BeginInit();
            this.SuspendLayout();
            // 
            // wz_mpresets
            // 
            resources.ApplyResources(this.wz_mpresets, "wz_mpresets");
            this.wz_mpresets.BackColor = System.Drawing.Color.White;
            this.wz_mpresets.Name = "wz_mpresets";
            this.wz_mpresets.Pages.Add(this.wzp1);
            this.wz_mpresets.Pages.Add(this.wzp2);
            this.wz_mpresets.Cancelling += new System.ComponentModel.CancelEventHandler(this.wz_mpresets_Cancelling);
            this.wz_mpresets.Finished += new System.EventHandler(this.wz_mpresets_Finished);
            this.wz_mpresets.SelectedPageChanged += new System.EventHandler(this.wz_mpresets_SelectedPageChanged);
            // 
            // wzp1
            // 
            resources.ApplyResources(this.wzp1, "wzp1");
            this.wzp1.Controls.Add(this.label9);
            this.wzp1.Controls.Add(this.btn_clear);
            this.wzp1.Controls.Add(this.txt_ext_3);
            this.wzp1.Controls.Add(this.label7);
            this.wzp1.Controls.Add(this.txt_pr_3);
            this.wzp1.Controls.Add(this.txt_ext_2);
            this.wzp1.Controls.Add(this.label5);
            this.wzp1.Controls.Add(this.txt_pr_2);
            this.wzp1.Controls.Add(this.txt_ext_1);
            this.wzp1.Controls.Add(this.label2);
            this.wzp1.Controls.Add(this.txt_pr_1);
            this.wzp1.Controls.Add(this.cb_w_presets);
            this.wzp1.Controls.Add(this.label3);
            this.wzp1.Name = "wzp1";
            this.wzp1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wzp1_Commit);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // btn_clear
            // 
            resources.ApplyResources(this.btn_clear, "btn_clear");
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // txt_ext_3
            // 
            resources.ApplyResources(this.txt_ext_3, "txt_ext_3");
            this.txt_ext_3.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_3.Name = "txt_ext_3";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // txt_pr_3
            // 
            resources.ApplyResources(this.txt_pr_3, "txt_pr_3");
            this.txt_pr_3.Name = "txt_pr_3";
            // 
            // txt_ext_2
            // 
            resources.ApplyResources(this.txt_ext_2, "txt_ext_2");
            this.txt_ext_2.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_2.Name = "txt_ext_2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txt_pr_2
            // 
            resources.ApplyResources(this.txt_pr_2, "txt_pr_2");
            this.txt_pr_2.Name = "txt_pr_2";
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
            // wzp2
            // 
            resources.ApplyResources(this.wzp2, "wzp2");
            this.wzp2.Controls.Add(this.btn_abort);
            this.wzp2.Controls.Add(this.btn_start_multi);
            this.wzp2.Controls.Add(this.pic_warning);
            this.wzp2.Controls.Add(this.lbl_ovw);
            this.wzp2.Controls.Add(this.label1);
            this.wzp2.IsFinishPage = true;
            this.wzp2.Name = "wzp2";
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
            this.btn_start_multi.Click += new System.EventHandler(this.btn_start_m3u_Click);
            // 
            // pic_warning
            // 
            resources.ApplyResources(this.pic_warning, "pic_warning");
            this.pic_warning.Name = "pic_warning";
            this.pic_warning.TabStop = false;
            // 
            // lbl_ovw
            // 
            resources.ApplyResources(this.lbl_ovw, "lbl_ovw");
            this.lbl_ovw.Name = "lbl_ovw";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AeroWizard2
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wz_mpresets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "AeroWizard2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AeroWizard2_FormClosing);
            this.Load += new System.EventHandler(this.AeroWizard2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.wz_mpresets)).EndInit();
            this.wzp1.ResumeLayout(false);
            this.wzp1.PerformLayout();
            this.wzp2.ResumeLayout(false);
            this.wzp2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_warning)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wz_mpresets;
        private AeroWizard.WizardPage wzp1;
        private AeroWizard.WizardPage wzp2;
        private System.Windows.Forms.TextBox txt_ext_3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_pr_3;
        private System.Windows.Forms.TextBox txt_ext_2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_pr_2;
        private System.Windows.Forms.TextBox txt_ext_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_pr_1;
        private System.Windows.Forms.ComboBox cb_w_presets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_ovw;
        private System.Windows.Forms.PictureBox pic_warning;
        private System.Windows.Forms.Button btn_start_multi;
        private System.Windows.Forms.Button btn_abort;
    }
}