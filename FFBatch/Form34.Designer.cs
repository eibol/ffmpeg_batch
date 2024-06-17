namespace FFBatch
{
    partial class Form34
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form34));
            this.btn_br_ff = new System.Windows.Forms.Button();
            this.btn_down_g = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_ff_v = new System.Windows.Forms.Label();
            this.lbl_expl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_d_v = new System.Windows.Forms.Label();
            this.lbl_srv = new System.Windows.Forms.Label();
            this.cb_srv = new System.Windows.Forms.ComboBox();
            this.lbl_val = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_br_ff
            // 
            this.btn_br_ff.FlatAppearance.BorderSize = 0;
            this.btn_br_ff.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_br_ff, "btn_br_ff");
            this.btn_br_ff.Name = "btn_br_ff";
            this.btn_br_ff.UseVisualStyleBackColor = true;
            this.btn_br_ff.Click += new System.EventHandler(this.btn_br_ff_Click);
            // 
            // btn_down_g
            // 
            this.btn_down_g.FlatAppearance.BorderSize = 0;
            this.btn_down_g.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_down_g, "btn_down_g");
            this.btn_down_g.Name = "btn_down_g";
            this.btn_down_g.UseVisualStyleBackColor = true;
            this.btn_down_g.Click += new System.EventHandler(this.btn_down_g_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btn_close, "btn_close");
            this.btn_close.Name = "btn_close";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lbl_ff_v
            // 
            resources.ApplyResources(this.lbl_ff_v, "lbl_ff_v");
            this.lbl_ff_v.Name = "lbl_ff_v";
            // 
            // lbl_expl
            // 
            resources.ApplyResources(this.lbl_expl, "lbl_expl");
            this.lbl_expl.Name = "lbl_expl";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_d_v);
            this.groupBox1.Controls.Add(this.lbl_srv);
            this.groupBox1.Controls.Add(this.cb_srv);
            this.groupBox1.Controls.Add(this.btn_br_ff);
            this.groupBox1.Controls.Add(this.btn_down_g);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // lbl_d_v
            // 
            resources.ApplyResources(this.lbl_d_v, "lbl_d_v");
            this.lbl_d_v.Name = "lbl_d_v";
            // 
            // lbl_srv
            // 
            resources.ApplyResources(this.lbl_srv, "lbl_srv");
            this.lbl_srv.Name = "lbl_srv";
            // 
            // cb_srv
            // 
            this.cb_srv.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_srv.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_srv.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cb_srv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.cb_srv, "cb_srv");
            this.cb_srv.FormattingEnabled = true;
            this.cb_srv.Items.AddRange(new object[] {
            resources.GetString("cb_srv.Items"),
            resources.GetString("cb_srv.Items1"),
            resources.GetString("cb_srv.Items2"),
            resources.GetString("cb_srv.Items3")});
            this.cb_srv.Name = "cb_srv";
            this.cb_srv.SelectedIndexChanged += new System.EventHandler(this.cb_srv_SelectedIndexChanged);
            // 
            // lbl_val
            // 
            resources.ApplyResources(this.lbl_val, "lbl_val");
            this.lbl_val.Name = "lbl_val";
            // 
            // Form34
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.lbl_val);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_expl);
            this.Controls.Add(this.lbl_ff_v);
            this.Controls.Add(this.btn_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form34";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form34_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_br_ff;
        private System.Windows.Forms.Button btn_down_g;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label lbl_expl;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbl_ff_v;
        private System.Windows.Forms.ComboBox cb_srv;
        private System.Windows.Forms.Label lbl_srv;
        private System.Windows.Forms.Label lbl_d_v;
        private System.Windows.Forms.Label lbl_val;
    }
}