namespace FFBatch
{
    partial class Form27
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form27));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_pr = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pre_input = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_load = new System.Windows.Forms.Button();
            this.open_file = new System.Windows.Forms.OpenFileDialog();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_decr_font = new System.Windows.Forms.Button();
            this.btn_inc_font = new System.Windows.Forms.Button();
            this.link1 = new System.Windows.Forms.LinkLabel();
            this.pic_w = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_w)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_pr
            // 
            this.dg_pr.AllowDrop = true;
            this.dg_pr.AllowUserToAddRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.AliceBlue;
            this.dg_pr.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dg_pr.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dg_pr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_pr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.pre_input,
            this.Column2,
            this.Column3});
            resources.ApplyResources(this.dg_pr, "dg_pr");
            this.dg_pr.Name = "dg_pr";
            this.dg_pr.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dg_pr_CellValidating);
            this.dg_pr.DragDrop += new System.Windows.Forms.DragEventHandler(this.dg_pr_DragDrop);
            this.dg_pr.DragOver += new System.Windows.Forms.DragEventHandler(this.dg_pr_DragOver);
            this.dg_pr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dg_pr_MouseDown);
            this.dg_pr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dg_pr_MouseMove);
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // pre_input
            // 
            resources.ApplyResources(this.pre_input, "pre_input");
            this.pre_input.Name = "pre_input";
            this.pre_input.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // btn_load
            // 
            this.btn_load.FlatAppearance.BorderSize = 0;
            this.btn_load.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_load, "btn_load");
            this.btn_load.Name = "btn_load";
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.button4_Click);
            // 
            // open_file
            // 
            resources.ApplyResources(this.open_file, "open_file");
            this.open_file.FileOk += new System.ComponentModel.CancelEventHandler(this.open_file_FileOk);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_decr_font
            // 
            this.btn_decr_font.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_decr_font, "btn_decr_font");
            this.btn_decr_font.Name = "btn_decr_font";
            this.btn_decr_font.UseVisualStyleBackColor = true;
            this.btn_decr_font.Click += new System.EventHandler(this.btn_decr_font_Click);
            // 
            // btn_inc_font
            // 
            this.btn_inc_font.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_inc_font, "btn_inc_font");
            this.btn_inc_font.Name = "btn_inc_font";
            this.btn_inc_font.UseVisualStyleBackColor = true;
            this.btn_inc_font.Click += new System.EventHandler(this.btn_inc_font_Click);
            // 
            // link1
            // 
            resources.ApplyResources(this.link1, "link1");
            this.link1.Name = "link1";
            this.link1.TabStop = true;
            this.link1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pic_w
            // 
            resources.ApplyResources(this.pic_w, "pic_w");
            this.pic_w.Name = "pic_w";
            this.pic_w.TabStop = false;
            // 
            // Form27
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.pic_w);
            this.Controls.Add(this.link1);
            this.Controls.Add(this.btn_decr_font);
            this.Controls.Add(this.btn_inc_font);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.dg_pr);
            this.Name = "Form27";
            this.Load += new System.EventHandler(this.Form27_Load);
            this.Resize += new System.EventHandler(this.Form27_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_w)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_pr;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.OpenFileDialog open_file;
        private System.Windows.Forms.Button btn_decr_font;
        private System.Windows.Forms.Button btn_inc_font;
        private System.Windows.Forms.LinkLabel link1;
        private System.Windows.Forms.PictureBox pic_w;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn pre_input;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        public System.Windows.Forms.Button btn_cancel;
    }
}