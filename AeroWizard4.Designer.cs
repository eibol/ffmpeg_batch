namespace FFBatch
{
    partial class AeroWizard4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard4));
            this.wizardControl1 = new AeroWizard.WizardControl();
            this.wz1 = new AeroWizard.WizardPage();
            this.label4 = new System.Windows.Forms.Label();
            this.n_seconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.n_db = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wz1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_db)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackColor = System.Drawing.Color.White;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wz1);
            this.wizardControl1.Size = new System.Drawing.Size(574, 415);
            this.wizardControl1.TabIndex = 0;
            this.wizardControl1.Title = "FFmpeg Batch silence detector";
            this.wizardControl1.TitleIcon = ((System.Drawing.Icon)(resources.GetObject("wizardControl1.TitleIcon")));
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            this.wizardControl1.SelectedPageChanged += new System.EventHandler(this.wizardControl1_SelectedPageChanged);
            // 
            // wz1
            // 
            this.wz1.Controls.Add(this.label5);
            this.wz1.Controls.Add(this.label4);
            this.wz1.Controls.Add(this.n_seconds);
            this.wz1.Controls.Add(this.label3);
            this.wz1.Controls.Add(this.n_db);
            this.wz1.Controls.Add(this.label2);
            this.wz1.Controls.Add(this.label1);
            this.wz1.Name = "wz1";
            this.wz1.Size = new System.Drawing.Size(527, 261);
            this.wz1.TabIndex = 0;
            this.wz1.Text = "Batch silence detector";
            this.wz1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz1_Commit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(159, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "second(s)";
            // 
            // n_seconds
            // 
            this.n_seconds.Location = new System.Drawing.Point(96, 79);
            this.n_seconds.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.n_seconds.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.n_seconds.Name = "n_seconds";
            this.n_seconds.Size = new System.Drawing.Size(57, 23);
            this.n_seconds.TabIndex = 4;
            this.n_seconds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "dB";
            // 
            // n_db
            // 
            this.n_db.Location = new System.Drawing.Point(96, 38);
            this.n_db.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.n_db.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.n_db.Name = "n_db";
            this.n_db.Size = new System.Drawing.Size(57, 23);
            this.n_db.TabIndex = 2;
            this.n_db.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lenght";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Threshold";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 216);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(198, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Press finish to start silence detection";
            // 
            // AeroWizard4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 415);
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AeroWizard4";
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).EndInit();
            this.wz1.ResumeLayout(false);
            this.wz1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_seconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_db)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AeroWizard.WizardControl wizardControl1;
        private AeroWizard.WizardPage wz1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown n_seconds;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown n_db;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
    }
}