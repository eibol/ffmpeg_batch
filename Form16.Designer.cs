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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form16));
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
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_pr
            // 
            this.dg_pr.AllowDrop = true;
            this.dg_pr.AllowUserToAddRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.AliceBlue;
            this.dg_pr.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
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
            this.dg_pr.Location = new System.Drawing.Point(22, 69);
            this.dg_pr.Name = "dg_pr";
            this.dg_pr.RowHeadersVisible = false;
            this.dg_pr.Size = new System.Drawing.Size(1055, 369);
            this.dg_pr.TabIndex = 144;
            this.dg_pr.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellClick);
            this.dg_pr.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellContentDoubleClick);
            this.dg_pr.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dg_pr_CellPainting);
            this.dg_pr.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_pr_CellValidated);
            this.dg_pr.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dg_pr_RowsRemoved);
            // 
            // column4
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.column4.DefaultCellStyle = dataGridViewCellStyle10;
            this.column4.HeaderText = "Nr";
            this.column4.MinimumWidth = 35;
            this.column4.Name = "column4";
            this.column4.ReadOnly = true;
            this.column4.Width = 35;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Filename";
            this.Column1.MinimumWidth = 50;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 153;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "FFmpeg parameters";
            this.Column2.MinimumWidth = 50;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 548;
            // 
            // Column6
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle11;
            this.Column6.HeaderText = "Streams";
            this.Column6.MinimumWidth = 55;
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 55;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Duration";
            this.Column7.MinimumWidth = 80;
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.Width = 80;
            // 
            // Column3
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle12;
            this.Column3.HeaderText = "Output";
            this.Column3.MinimumWidth = 50;
            this.Column3.Name = "Column3";
            this.Column3.Width = 153;
            // 
            // Column5
            // 
            this.Column5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Column5.HeaderText = "";
            this.Column5.MinimumWidth = 25;
            this.Column5.Name = "Column5";
            this.Column5.Text = "";
            this.Column5.Width = 25;
            // 
            // btn_clear
            // 
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.Image")));
            this.btn_clear.Location = new System.Drawing.Point(-50, -19);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(20, 27);
            this.btn_clear.TabIndex = 158;
            this.btn_clear.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cancel.Location = new System.Drawing.Point(480, 444);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(55, 82);
            this.btn_cancel.TabIndex = 147;
            this.btn_cancel.Text = "Close window";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // item_up
            // 
            this.item_up.FlatAppearance.BorderSize = 0;
            this.item_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_up.Image = ((System.Drawing.Image)(resources.GetObject("item_up.Image")));
            this.item_up.Location = new System.Drawing.Point(1040, 36);
            this.item_up.Name = "item_up";
            this.item_up.Size = new System.Drawing.Size(21, 27);
            this.item_up.TabIndex = 149;
            this.item_up.UseVisualStyleBackColor = true;
            this.item_up.Click += new System.EventHandler(this.item_up_Click);
            // 
            // item_down
            // 
            this.item_down.FlatAppearance.BorderSize = 0;
            this.item_down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_down.Image = ((System.Drawing.Image)(resources.GetObject("item_down.Image")));
            this.item_down.Location = new System.Drawing.Point(1017, 36);
            this.item_down.Name = "item_down";
            this.item_down.Size = new System.Drawing.Size(21, 27);
            this.item_down.TabIndex = 150;
            this.item_down.UseVisualStyleBackColor = true;
            this.item_down.Click += new System.EventHandler(this.item_down_Click);
            // 
            // btn_inc_font
            // 
            this.btn_inc_font.FlatAppearance.BorderSize = 0;
            this.btn_inc_font.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_inc_font.Image = ((System.Drawing.Image)(resources.GetObject("btn_inc_font.Image")));
            this.btn_inc_font.Location = new System.Drawing.Point(945, 36);
            this.btn_inc_font.Name = "btn_inc_font";
            this.btn_inc_font.Size = new System.Drawing.Size(20, 27);
            this.btn_inc_font.TabIndex = 151;
            this.btn_inc_font.UseVisualStyleBackColor = true;
            this.btn_inc_font.Click += new System.EventHandler(this.btn_inc_font_Click);
            // 
            // btn_decr_font
            // 
            this.btn_decr_font.FlatAppearance.BorderSize = 0;
            this.btn_decr_font.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_decr_font.Image = ((System.Drawing.Image)(resources.GetObject("btn_decr_font.Image")));
            this.btn_decr_font.Location = new System.Drawing.Point(967, 36);
            this.btn_decr_font.Name = "btn_decr_font";
            this.btn_decr_font.Size = new System.Drawing.Size(20, 27);
            this.btn_decr_font.TabIndex = 152;
            this.btn_decr_font.UseVisualStyleBackColor = true;
            this.btn_decr_font.Click += new System.EventHandler(this.btn_decr_font_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(433, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 20);
            this.label1.TabIndex = 159;
            this.label1.Text = "Multiplex Jobs manager";
            // 
            // pic
            // 
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(866, 15);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(20, 18);
            this.pic.TabIndex = 160;
            this.pic.TabStop = false;
            this.pic.Visible = false;
            // 
            // btn_clear_list
            // 
            this.btn_clear_list.FlatAppearance.BorderSize = 0;
            this.btn_clear_list.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_clear_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear_list.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear_list.Image")));
            this.btn_clear_list.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_clear_list.Location = new System.Drawing.Point(429, 444);
            this.btn_clear_list.Name = "btn_clear_list";
            this.btn_clear_list.Size = new System.Drawing.Size(43, 82);
            this.btn_clear_list.TabIndex = 162;
            this.btn_clear_list.Text = "Clear  list ";
            this.btn_clear_list.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_clear_list.UseVisualStyleBackColor = true;
            this.btn_clear_list.Click += new System.EventHandler(this.btn_clear_list_Click);
            // 
            // requeue
            // 
            this.requeue.FlatAppearance.BorderSize = 0;
            this.requeue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.requeue.Image = ((System.Drawing.Image)(resources.GetObject("requeue.Image")));
            this.requeue.Location = new System.Drawing.Point(992, 37);
            this.requeue.Name = "requeue";
            this.requeue.Size = new System.Drawing.Size(23, 26);
            this.requeue.TabIndex = 163;
            this.requeue.UseVisualStyleBackColor = true;
            this.requeue.Click += new System.EventHandler(this.requeue_Click);
            // 
            // btn_jobs
            // 
            this.btn_jobs.FlatAppearance.BorderSize = 0;
            this.btn_jobs.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_jobs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_jobs.Image = ((System.Drawing.Image)(resources.GetObject("btn_jobs.Image")));
            this.btn_jobs.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_jobs.Location = new System.Drawing.Point(540, 440);
            this.btn_jobs.Name = "btn_jobs";
            this.btn_jobs.Size = new System.Drawing.Size(56, 87);
            this.btn_jobs.TabIndex = 164;
            this.btn_jobs.Text = "Start mux jobs";
            this.btn_jobs.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_jobs.UseVisualStyleBackColor = true;
            this.btn_jobs.Click += new System.EventHandler(this.btn_jobs_Click);
            // 
            // Form16
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 532);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form16";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multiplex jobs manager";
            this.Load += new System.EventHandler(this.Form16_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button item_up;
        private System.Windows.Forms.Button item_down;
        private System.Windows.Forms.Button btn_inc_font;
        private System.Windows.Forms.Button btn_decr_font;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dg_pr;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.Button btn_clear_list;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewButtonColumn Column5;
        private System.Windows.Forms.Button requeue;
        private System.Windows.Forms.Button btn_jobs;
    }
}