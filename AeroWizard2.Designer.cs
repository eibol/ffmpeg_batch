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
            this.wz_mpresets.BackColor = System.Drawing.Color.White;
            this.wz_mpresets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wz_mpresets.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wz_mpresets.Location = new System.Drawing.Point(0, 0);
            this.wz_mpresets.Name = "wz_mpresets";
            this.wz_mpresets.Pages.Add(this.wzp1);
            this.wz_mpresets.Pages.Add(this.wzp2);
            this.wz_mpresets.Size = new System.Drawing.Size(574, 415);
            this.wz_mpresets.TabIndex = 0;
            this.wz_mpresets.Title = "Multiple presets wizard";
            this.wz_mpresets.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wz_mpresets.TitleIcon")));
            this.wz_mpresets.Cancelling += new System.ComponentModel.CancelEventHandler(this.wz_mpresets_Cancelling);
            this.wz_mpresets.Finished += new System.EventHandler(this.wz_mpresets_Finished);
            this.wz_mpresets.SelectedPageChanged += new System.EventHandler(this.wz_mpresets_SelectedPageChanged);
            // 
            // wzp1
            // 
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
            this.wzp1.Size = new System.Drawing.Size(527, 261);
            this.wzp1.TabIndex = 0;
            this.wzp1.Text = "Multiple presets encoding";
            this.wzp1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wzp1_Commit);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(19, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(289, 15);
            this.label9.TabIndex = 51;
            this.label9.Text = "Select up to 3 saved presets to be applied sequentially";
            // 
            // btn_clear
            // 
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.Image")));
            this.btn_clear.Location = new System.Drawing.Point(479, 49);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(24, 28);
            this.btn_clear.TabIndex = 50;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // txt_ext_3
            // 
            this.txt_ext_3.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_3.Location = new System.Drawing.Point(474, 225);
            this.txt_ext_3.MaxLength = 4;
            this.txt_ext_3.Name = "txt_ext_3";
            this.txt_ext_3.Size = new System.Drawing.Size(30, 23);
            this.txt_ext_3.TabIndex = 48;
            this.txt_ext_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(427, 228);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 15);
            this.label7.TabIndex = 47;
            this.label7.Text = "Format";
            // 
            // txt_pr_3
            // 
            this.txt_pr_3.Location = new System.Drawing.Point(70, 217);
            this.txt_pr_3.MaxLength = 500;
            this.txt_pr_3.Multiline = true;
            this.txt_pr_3.Name = "txt_pr_3";
            this.txt_pr_3.Size = new System.Drawing.Size(354, 36);
            this.txt_pr_3.TabIndex = 46;
            // 
            // txt_ext_2
            // 
            this.txt_ext_2.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_2.Location = new System.Drawing.Point(474, 170);
            this.txt_ext_2.MaxLength = 4;
            this.txt_ext_2.Name = "txt_ext_2";
            this.txt_ext_2.Size = new System.Drawing.Size(30, 23);
            this.txt_ext_2.TabIndex = 44;
            this.txt_ext_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(427, 173);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 15);
            this.label5.TabIndex = 43;
            this.label5.Text = "Format";
            // 
            // txt_pr_2
            // 
            this.txt_pr_2.Location = new System.Drawing.Point(70, 163);
            this.txt_pr_2.MaxLength = 500;
            this.txt_pr_2.Multiline = true;
            this.txt_pr_2.Name = "txt_pr_2";
            this.txt_pr_2.Size = new System.Drawing.Size(354, 36);
            this.txt_pr_2.TabIndex = 42;
            // 
            // txt_ext_1
            // 
            this.txt_ext_1.BackColor = System.Drawing.SystemColors.Info;
            this.txt_ext_1.Location = new System.Drawing.Point(474, 116);
            this.txt_ext_1.MaxLength = 4;
            this.txt_ext_1.Name = "txt_ext_1";
            this.txt_ext_1.Size = new System.Drawing.Size(30, 23);
            this.txt_ext_1.TabIndex = 40;
            this.txt_ext_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(427, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 39;
            this.label2.Text = "Format";
            // 
            // txt_pr_1
            // 
            this.txt_pr_1.Location = new System.Drawing.Point(70, 109);
            this.txt_pr_1.MaxLength = 500;
            this.txt_pr_1.Multiline = true;
            this.txt_pr_1.Name = "txt_pr_1";
            this.txt_pr_1.Size = new System.Drawing.Size(354, 36);
            this.txt_pr_1.TabIndex = 38;
            // 
            // cb_w_presets
            // 
            this.cb_w_presets.FormattingEnabled = true;
            this.cb_w_presets.Location = new System.Drawing.Point(70, 52);
            this.cb_w_presets.Name = "cb_w_presets";
            this.cb_w_presets.Size = new System.Drawing.Size(403, 23);
            this.cb_w_presets.TabIndex = 36;
            this.cb_w_presets.SelectedIndexChanged += new System.EventHandler(this.cb_w_presets_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 37;
            this.label3.Text = "Presets";
            // 
            // wzp2
            // 
            this.wzp2.Controls.Add(this.btn_abort);
            this.wzp2.Controls.Add(this.btn_start_multi);
            this.wzp2.Controls.Add(this.pic_warning);
            this.wzp2.Controls.Add(this.lbl_ovw);
            this.wzp2.Controls.Add(this.label1);
            this.wzp2.IsFinishPage = true;
            this.wzp2.Name = "wzp2";
            this.wzp2.Size = new System.Drawing.Size(527, 261);
            this.wzp2.TabIndex = 1;
            this.wzp2.Text = "Ready to start multiple encoding";
            // 
            // btn_abort
            // 
            this.btn_abort.FlatAppearance.BorderSize = 0;
            this.btn_abort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_abort.Image = ((System.Drawing.Image)(resources.GetObject("btn_abort.Image")));
            this.btn_abort.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_abort.Location = new System.Drawing.Point(241, 130);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(60, 74);
            this.btn_abort.TabIndex = 88;
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
            this.btn_start_multi.Location = new System.Drawing.Point(179, 128);
            this.btn_start_multi.Name = "btn_start_multi";
            this.btn_start_multi.Size = new System.Drawing.Size(63, 75);
            this.btn_start_multi.TabIndex = 87;
            this.btn_start_multi.Text = "Start";
            this.btn_start_multi.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_start_multi.UseVisualStyleBackColor = true;
            this.btn_start_multi.Click += new System.EventHandler(this.btn_start_m3u_Click);
            // 
            // pic_warning
            // 
            this.pic_warning.Image = ((System.Drawing.Image)(resources.GetObject("pic_warning.Image")));
            this.pic_warning.InitialImage = null;
            this.pic_warning.Location = new System.Drawing.Point(286, 53);
            this.pic_warning.Name = "pic_warning";
            this.pic_warning.Size = new System.Drawing.Size(24, 24);
            this.pic_warning.TabIndex = 24;
            this.pic_warning.TabStop = false;
            this.pic_warning.Visible = false;
            // 
            // lbl_ovw
            // 
            this.lbl_ovw.AutoSize = true;
            this.lbl_ovw.Location = new System.Drawing.Point(9, 55);
            this.lbl_ovw.Name = "lbl_ovw";
            this.lbl_ovw.Size = new System.Drawing.Size(268, 15);
            this.lbl_ovw.TabIndex = 2;
            this.lbl_ovw.Text = "Output files will be renamed to avoid overwriting.";
            this.lbl_ovw.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 15);
            this.label1.TabIndex = 1;
            // 
            // AeroWizard2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 415);
            this.Controls.Add(this.wz_mpresets);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AeroWizard2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AeroWizard2_FormClosing);
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