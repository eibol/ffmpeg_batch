namespace FFBatch
{
    partial class Form16
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form16));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_pr = new System.Windows.Forms.DataGridView();
            this.column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.item_up = new System.Windows.Forms.Button();
            this.item_down = new System.Windows.Forms.Button();
            this.btn_inc_font = new System.Windows.Forms.Button();
            this.btn_decr_font = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btn_clear_list = new System.Windows.Forms.Button();
            this.requeue = new System.Windows.Forms.Button();
            this.btn_jobs = new System.Windows.Forms.Button();
            this.btn_logs_url = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_pr
            // 
            resources.ApplyResources(this.dg_pr, "dg_pr");
            this.dg_pr.AllowDrop = true;
            this.dg_pr.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dg_pr.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_pr.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dg_pr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_pr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column4,
            this.Column1,
            this.Column2,
            this.Column6,
            this.Column7,
            this.Column3,
            this.Column5});
            this.dg_pr.Name = "dg_pr";
            this.dg_pr.RowHeadersVisible = false;
            this.dg_pr.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellClick);
            this.dg_pr.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellContentDoubleClick);
            this.dg_pr.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dg_pr_CellPainting);
            this.dg_pr.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellValidated);
            this.dg_pr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dg_pr_RowsRemoved);
            // 
            // column4
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.column4.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(this.column4, "column4");
            this.column4.Name = "column4";
            this.column4.ReadOnly = true;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column6
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column7
            // 
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column3
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            // 
            // Column5
            // 
            this.Column5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            this.Column5.Text = "";
            // 
            // btn_clear
            // 
            resources.ApplyResources(this.btn_clear, "btn_clear");
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // item_up
            // 
            resources.ApplyResources(this.item_up, "item_up");
            this.item_up.FlatAppearance.BorderSize = 0;
            this.item_up.Name = "item_up";
            this.item_up.UseVisualStyleBackColor = true;
            this.item_up.Click += new System.EventHandler(this.item_up_Click);
            // 
            // item_down
            // 
            resources.ApplyResources(this.item_down, "item_down");
            this.item_down.FlatAppearance.BorderSize = 0;
            this.item_down.Name = "item_down";
            this.item_down.UseVisualStyleBackColor = true;
            this.item_down.Click += new System.EventHandler(this.item_down_Click);
            // 
            // btn_inc_font
            // 
            resources.ApplyResources(this.btn_inc_font, "btn_inc_font");
            this.btn_inc_font.FlatAppearance.BorderSize = 0;
            this.btn_inc_font.Name = "btn_inc_font";
            this.btn_inc_font.UseVisualStyleBackColor = true;
            this.btn_inc_font.Click += new System.EventHandler(this.btn_inc_font_Click);
            // 
            // btn_decr_font
            // 
            resources.ApplyResources(this.btn_decr_font, "btn_decr_font");
            this.btn_decr_font.FlatAppearance.BorderSize = 0;
            this.btn_decr_font.Name = "btn_decr_font";
            this.btn_decr_font.UseVisualStyleBackColor = true;
            this.btn_decr_font.Click += new System.EventHandler(this.btn_decr_font_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Name = "label1";
            // 
            // pic
            // 
            resources.ApplyResources(this.pic, "pic");
            this.pic.Name = "pic";
            this.pic.TabStop = false;
            // 
            // btn_clear_list
            // 
            resources.ApplyResources(this.btn_clear_list, "btn_clear_list");
            this.btn_clear_list.FlatAppearance.BorderSize = 0;
            this.btn_clear_list.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_clear_list.Name = "btn_clear_list";
            this.btn_clear_list.UseVisualStyleBackColor = true;
            this.btn_clear_list.Click += new System.EventHandler(this.btn_clear_list_Click);
            // 
            // requeue
            // 
            resources.ApplyResources(this.requeue, "requeue");
            this.requeue.FlatAppearance.BorderSize = 0;
            this.requeue.Name = "requeue";
            this.requeue.UseVisualStyleBackColor = true;
            this.requeue.Click += new System.EventHandler(this.requeue_Click);
            // 
            // btn_jobs
            // 
            resources.ApplyResources(this.btn_jobs, "btn_jobs");
            this.btn_jobs.FlatAppearance.BorderSize = 0;
            this.btn_jobs.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_jobs.Name = "btn_jobs";
            this.btn_jobs.UseVisualStyleBackColor = true;
            this.btn_jobs.Click += new System.EventHandler(this.btn_jobs_Click);
            // 
            // btn_logs_url
            // 
            resources.ApplyResources(this.btn_logs_url, "btn_logs_url");
            this.btn_logs_url.FlatAppearance.BorderSize = 0;
            this.btn_logs_url.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_logs_url.Name = "btn_logs_url";
            this.btn_logs_url.UseVisualStyleBackColor = true;
            this.btn_logs_url.Click += new System.EventHandler(this.btn_logs_url_Click);
            // 
            // Form16
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.btn_logs_url);
            this.Controls.Add(this.btn_jobs);
            this.Controls.Add(this.requeue);
            this.Controls.Add(this.btn_clear_list);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_decr_font);
            this.Controls.Add(this.btn_inc_font);
            this.Controls.Add(this.item_down);
            this.Controls.Add(this.item_up);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.dg_pr);
            this.Name = "Form16";
            this.Load += new System.EventHandler(this.Form16_Load);
            this.Resize += new System.EventHandler(this.Form16_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button item_up;
        private System.Windows.Forms.Button item_down;
        private System.Windows.Forms.Button btn_inc_font;
        private System.Windows.Forms.Button btn_decr_font;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dg_pr;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btn_clear_list;
        private System.Windows.Forms.Button requeue;
        private System.Windows.Forms.Button btn_jobs;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column5;
        public System.Windows.Forms.Button btn_logs_url;
        public System.Windows.Forms.Button btn_cancel;
    }
}