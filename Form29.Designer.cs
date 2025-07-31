
namespace FFBatch
{
    partial class Form29
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form29));
            this.label8 = new System.Windows.Forms.Label();
            this.n_bat_l = new System.Windows.Forms.NumericUpDown();
            this.chk_bat_level = new System.Windows.Forms.CheckBox();
            this.chk_battery = new System.Windows.Forms.CheckBox();
            this.btn_bat_ok = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.n_bat_l)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // n_bat_l
            // 
            resources.ApplyResources(this.n_bat_l, "n_bat_l");
            this.n_bat_l.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.n_bat_l.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.n_bat_l.Name = "n_bat_l";
            this.n_bat_l.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // chk_bat_level
            // 
            resources.ApplyResources(this.chk_bat_level, "chk_bat_level");
            this.chk_bat_level.Name = "chk_bat_level";
            this.chk_bat_level.UseVisualStyleBackColor = true;
            // 
            // chk_battery
            // 
            resources.ApplyResources(this.chk_battery, "chk_battery");
            this.chk_battery.Name = "chk_battery";
            this.chk_battery.UseVisualStyleBackColor = true;
            this.chk_battery.CheckedChanged += new System.EventHandler(this.chk_battery_CheckedChanged);
            // 
            // btn_bat_ok
            // 
            resources.ApplyResources(this.btn_bat_ok, "btn_bat_ok");
            this.btn_bat_ok.Name = "btn_bat_ok";
            this.btn_bat_ok.UseVisualStyleBackColor = true;
            this.btn_bat_ok.Click += new System.EventHandler(this.btn_bat_ok_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chk_battery);
            this.groupBox1.Controls.Add(this.chk_bat_level);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.n_bat_l);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // Form29
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_bat_ok);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form29";
            this.Load += new System.EventHandler(this.Form29_Load);
            ((System.ComponentModel.ISupportInitialize)(this.n_bat_l)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown n_bat_l;
        public System.Windows.Forms.CheckBox chk_bat_level;
        public System.Windows.Forms.CheckBox chk_battery;
        public System.Windows.Forms.Button btn_bat_ok;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}