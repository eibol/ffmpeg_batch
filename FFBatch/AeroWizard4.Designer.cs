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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.n_seconds = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.n_db = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wizardControl1)).BeginInit();
            this.wz1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_seconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_db)).BeginInit();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            resources.ApplyResources(this.wizardControl1, "wizardControl1");
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.Pages.Add(this.wz1);
            this.wizardControl1.Cancelling += new System.ComponentModel.CancelEventHandler(this.wizardControl1_Cancelling);
            // 
            // wz1
            // 
            resources.ApplyResources(this.wz1, "wz1");
            this.wz1.Controls.Add(this.label5);
            this.wz1.Controls.Add(this.label4);
            this.wz1.Controls.Add(this.n_seconds);
            this.wz1.Controls.Add(this.label3);
            this.wz1.Controls.Add(this.n_db);
            this.wz1.Controls.Add(this.label2);
            this.wz1.Controls.Add(this.label1);
            this.wz1.Name = "wz1";
            this.wz1.Commit += new System.EventHandler<AeroWizard.WizardPageConfirmEventArgs>(this.wz1_Commit);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // n_seconds
            // 
            resources.ApplyResources(this.n_seconds, "n_seconds");
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
            this.n_seconds.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // n_db
            // 
            resources.ApplyResources(this.n_db, "n_db");
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
            this.n_db.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AeroWizard4
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wizardControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AeroWizard4";
            this.Load += new System.EventHandler(this.AeroWizard4_Load);
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