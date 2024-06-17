
namespace FFBatch
{
    partial class Form17
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form17));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_col = new System.Windows.Forms.ComboBox();
            this.btn_all = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_del_col = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_custom_med = new System.Windows.Forms.ComboBox();
            this.btn_add_col = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cb_col
            // 
            resources.ApplyResources(this.cb_col, "cb_col");
            this.cb_col.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_col.FormattingEnabled = true;
            this.cb_col.Items.AddRange(new object[] {
            resources.GetString("cb_col.Items")});
            this.cb_col.Name = "cb_col";
            this.cb_col.SelectedIndexChanged += new System.EventHandler(this.cb_col_SelectedIndexChanged);
            // 
            // btn_all
            // 
            resources.ApplyResources(this.btn_all, "btn_all");
            this.btn_all.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_all.FlatAppearance.BorderSize = 0;
            this.btn_all.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_all.Name = "btn_all";
            this.btn_all.UseVisualStyleBackColor = true;
            this.btn_all.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // btn_del_col
            // 
            resources.ApplyResources(this.btn_del_col, "btn_del_col");
            this.btn_del_col.FlatAppearance.BorderSize = 0;
            this.btn_del_col.Name = "btn_del_col";
            this.btn_del_col.UseVisualStyleBackColor = true;
            this.btn_del_col.Click += new System.EventHandler(this.btn_del_col_Click);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Name = "panel1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cb_custom_med
            // 
            resources.ApplyResources(this.cb_custom_med, "cb_custom_med");
            this.cb_custom_med.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_custom_med.FormattingEnabled = true;
            this.cb_custom_med.Items.AddRange(new object[] {
            resources.GetString("cb_custom_med.Items"),
            resources.GetString("cb_custom_med.Items1"),
            resources.GetString("cb_custom_med.Items2"),
            resources.GetString("cb_custom_med.Items3"),
            resources.GetString("cb_custom_med.Items4"),
            resources.GetString("cb_custom_med.Items5"),
            resources.GetString("cb_custom_med.Items6"),
            resources.GetString("cb_custom_med.Items7"),
            resources.GetString("cb_custom_med.Items8"),
            resources.GetString("cb_custom_med.Items9"),
            resources.GetString("cb_custom_med.Items10"),
            resources.GetString("cb_custom_med.Items11"),
            resources.GetString("cb_custom_med.Items12"),
            resources.GetString("cb_custom_med.Items13"),
            resources.GetString("cb_custom_med.Items14"),
            resources.GetString("cb_custom_med.Items15"),
            resources.GetString("cb_custom_med.Items16"),
            resources.GetString("cb_custom_med.Items17"),
            resources.GetString("cb_custom_med.Items18"),
            resources.GetString("cb_custom_med.Items19"),
            resources.GetString("cb_custom_med.Items20"),
            resources.GetString("cb_custom_med.Items21"),
            resources.GetString("cb_custom_med.Items22"),
            resources.GetString("cb_custom_med.Items23"),
            resources.GetString("cb_custom_med.Items24"),
            resources.GetString("cb_custom_med.Items25"),
            resources.GetString("cb_custom_med.Items26"),
            resources.GetString("cb_custom_med.Items27"),
            resources.GetString("cb_custom_med.Items28"),
            resources.GetString("cb_custom_med.Items29"),
            resources.GetString("cb_custom_med.Items30"),
            resources.GetString("cb_custom_med.Items31"),
            resources.GetString("cb_custom_med.Items32")});
            this.cb_custom_med.Name = "cb_custom_med";
            this.cb_custom_med.SelectedIndexChanged += new System.EventHandler(this.cb_custom_med_SelectedIndexChanged);
            // 
            // btn_add_col
            // 
            resources.ApplyResources(this.btn_add_col, "btn_add_col");
            this.btn_add_col.FlatAppearance.BorderSize = 0;
            this.btn_add_col.Name = "btn_add_col";
            this.btn_add_col.UseVisualStyleBackColor = true;
            this.btn_add_col.Click += new System.EventHandler(this.btn_add_col_Click_1);
            // 
            // Form17
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_all;
            this.Controls.Add(this.cb_col);
            this.Controls.Add(this.btn_add_col);
            this.Controls.Add(this.cb_custom_med);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_del_col);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_all);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form17";
            this.Activated += new System.EventHandler(this.Form17_Activated);
            this.Load += new System.EventHandler(this.Form17_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ComboBox cb_col;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox cb_custom_med;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_add_col;
        public System.Windows.Forms.Button btn_all;
        public System.Windows.Forms.Button btn_del_col;
    }
}