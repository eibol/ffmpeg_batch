namespace FFBatch
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_streams = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_grid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ct1 = new System.Windows.Forms.ToolStripMenuItem();
            this.txt_file = new System.Windows.Forms.TextBox();
            this.img_streams = new System.Windows.Forms.ImageList(this.components);
            this.ff_str = new System.Diagnostics.Process();
            this.pic_frame = new System.Windows.Forms.PictureBox();
            this.btn_10 = new System.Windows.Forms.Button();
            this.btn_minus10 = new System.Windows.Forms.Button();
            this.btn_fr_start = new System.Windows.Forms.Button();
            this.btn_fr_end = new System.Windows.Forms.Button();
            this.lbl_fr_time = new System.Windows.Forms.TextBox();
            this.btn_min1 = new System.Windows.Forms.Button();
            this.btn_plus1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).BeginInit();
            this.menu_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_frame)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_streams
            // 
            this.dg_streams.AllowUserToAddRows = false;
            this.dg_streams.AllowUserToDeleteRows = false;
            this.dg_streams.AllowUserToOrderColumns = true;
            this.dg_streams.AllowUserToResizeColumns = false;
            this.dg_streams.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dg_streams.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dg_streams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_streams.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column1,
            this.Column2});
            this.dg_streams.ContextMenuStrip = this.menu_grid;
            resources.ApplyResources(this.dg_streams, "dg_streams");
            this.dg_streams.Name = "dg_streams";
            // 
            // Column3
            // 
            this.Column3.FillWeight = 25F;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column1
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // menu_grid
            // 
            this.menu_grid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ct1});
            this.menu_grid.Name = "menu_grid";
            resources.ApplyResources(this.menu_grid, "menu_grid");
            this.menu_grid.Opening += new System.ComponentModel.CancelEventHandler(this.menu_grid_Opening);
            // 
            // ct1
            // 
            this.ct1.Name = "ct1";
            resources.ApplyResources(this.ct1, "ct1");
            this.ct1.Click += new System.EventHandler(this.ct1_Click);
            // 
            // txt_file
            // 
            this.txt_file.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_file.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_file, "txt_file");
            this.txt_file.Name = "txt_file";
            this.txt_file.ReadOnly = true;
            // 
            // img_streams
            // 
            this.img_streams.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_streams.ImageStream")));
            this.img_streams.TransparentColor = System.Drawing.Color.Transparent;
            this.img_streams.Images.SetKeyName(0, "claqueta_icon_16.png");
            this.img_streams.Images.SetKeyName(1, "audio_icon_16.png");
            this.img_streams.Images.SetKeyName(2, "subs_text_32.png");
            this.img_streams.Images.SetKeyName(3, "minus - Red.png");
            this.img_streams.Images.SetKeyName(4, "image_audio_17.png");
            // 
            // ff_str
            // 
            this.ff_str.StartInfo.Domain = "";
            this.ff_str.StartInfo.LoadUserProfile = false;
            this.ff_str.StartInfo.Password = null;
            this.ff_str.StartInfo.StandardErrorEncoding = null;
            this.ff_str.StartInfo.StandardOutputEncoding = null;
            this.ff_str.StartInfo.UserName = "";
            this.ff_str.SynchronizingObject = this;
            // 
            // pic_frame
            // 
            resources.ApplyResources(this.pic_frame, "pic_frame");
            this.pic_frame.Name = "pic_frame";
            this.pic_frame.TabStop = false;
            // 
            // btn_10
            // 
            resources.ApplyResources(this.btn_10, "btn_10");
            this.btn_10.Name = "btn_10";
            this.btn_10.UseVisualStyleBackColor = true;
            this.btn_10.Click += new System.EventHandler(this.btn_10_Click);
            // 
            // btn_minus10
            // 
            resources.ApplyResources(this.btn_minus10, "btn_minus10");
            this.btn_minus10.Name = "btn_minus10";
            this.btn_minus10.UseVisualStyleBackColor = true;
            this.btn_minus10.Click += new System.EventHandler(this.btn_minus10_Click);
            // 
            // btn_fr_start
            // 
            resources.ApplyResources(this.btn_fr_start, "btn_fr_start");
            this.btn_fr_start.Name = "btn_fr_start";
            this.btn_fr_start.UseVisualStyleBackColor = true;
            this.btn_fr_start.Click += new System.EventHandler(this.btn_fr_start_Click);
            // 
            // btn_fr_end
            // 
            resources.ApplyResources(this.btn_fr_end, "btn_fr_end");
            this.btn_fr_end.Name = "btn_fr_end";
            this.btn_fr_end.UseVisualStyleBackColor = true;
            this.btn_fr_end.Click += new System.EventHandler(this.btn_fr_end_Click);
            // 
            // lbl_fr_time
            // 
            this.lbl_fr_time.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.lbl_fr_time, "lbl_fr_time");
            this.lbl_fr_time.Name = "lbl_fr_time";
            this.lbl_fr_time.TextChanged += new System.EventHandler(this.lbl_fr_time_TextChanged);
            // 
            // btn_min1
            // 
            resources.ApplyResources(this.btn_min1, "btn_min1");
            this.btn_min1.Name = "btn_min1";
            this.btn_min1.UseVisualStyleBackColor = true;
            this.btn_min1.Click += new System.EventHandler(this.btn_min1_Click);
            // 
            // btn_plus1
            // 
            resources.ApplyResources(this.btn_plus1, "btn_plus1");
            this.btn_plus1.Name = "btn_plus1";
            this.btn_plus1.UseVisualStyleBackColor = true;
            this.btn_plus1.Click += new System.EventHandler(this.btn_plus1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_file);
            this.groupBox1.Controls.Add(this.btn_plus1);
            this.groupBox1.Controls.Add(this.pic_frame);
            this.groupBox1.Controls.Add(this.btn_min1);
            this.groupBox1.Controls.Add(this.btn_fr_start);
            this.groupBox1.Controls.Add(this.lbl_fr_time);
            this.groupBox1.Controls.Add(this.btn_fr_end);
            this.groupBox1.Controls.Add(this.btn_10);
            this.groupBox1.Controls.Add(this.btn_minus10);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // btn_close
            // 
            resources.ApplyResources(this.btn_close, "btn_close");
            this.btn_close.Name = "btn_close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // Form5
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.dg_streams);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_name);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form5_FormClosing);
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).EndInit();
            this.menu_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_frame)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_streams;
        private System.Windows.Forms.TextBox txt_file;
        private System.Windows.Forms.ImageList img_streams;
        private System.Windows.Forms.ContextMenuStrip menu_grid;
        private System.Windows.Forms.ToolStripMenuItem ct1;
        private System.Diagnostics.Process ff_str;
        private System.Windows.Forms.Button btn_10;
        private System.Windows.Forms.Button btn_minus10;
        private System.Windows.Forms.Button btn_fr_end;
        private System.Windows.Forms.Button btn_fr_start;
        private System.Windows.Forms.TextBox lbl_fr_time;
        private System.Windows.Forms.Button btn_plus1;
        private System.Windows.Forms.Button btn_min1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.PictureBox pic_frame;
    }
}