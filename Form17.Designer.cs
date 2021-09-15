
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
            this.btn_add_col = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_del_col = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
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
            // btn_add_col
            // 
            resources.ApplyResources(this.btn_add_col, "btn_add_col");
            this.btn_add_col.FlatAppearance.BorderSize = 0;
            this.btn_add_col.Name = "btn_add_col";
            this.btn_add_col.UseVisualStyleBackColor = true;
            this.btn_add_col.Click += new System.EventHandler(this.btn_add_col_Click);
            // 
            // btn_exit
            // 
            resources.ApplyResources(this.btn_exit, "btn_exit");
            this.btn_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
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
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Name = "panel1";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // Form17
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_exit;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_del_col);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add_col);
            this.Controls.Add(this.cb_col);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form17";
            this.Load += new System.EventHandler(this.Form17_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_add_col;
        private System.Windows.Forms.Button btn_exit;
        public System.Windows.Forms.ComboBox cb_col;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_del_col;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}