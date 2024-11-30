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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_streams = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menu_grid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ct1 = new System.Windows.Forms.ToolStripMenuItem();
            this.img_streams = new System.Windows.Forms.ImageList(this.components);
            this.ff_str = new System.Diagnostics.Process();
            this.pic_frame = new System.Windows.Forms.PictureBox();
            this.menu_Img = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ct_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ct_save = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_kplus1 = new System.Windows.Forms.Button();
            this.btn_k_m1 = new System.Windows.Forms.Button();
            this.pic_cut = new System.Windows.Forms.PictureBox();
            this.img_prog = new System.Windows.Forms.PictureBox();
            this.lbl_lapse = new System.Windows.Forms.Label();
            this.lbl_end = new System.Windows.Forms.Label();
            this.lbl_start = new System.Windows.Forms.Label();
            this.trim_right = new System.Windows.Forms.Button();
            this.trim_left = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pg1 = new FFBatch.ProgressBarWithText();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_prog = new System.Windows.Forms.Label();
            this.btn_copy = new System.Windows.Forms.Button();
            this.trackB = new System.Windows.Forms.TrackBar();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_plus1 = new System.Windows.Forms.Button();
            this.btn_min1 = new System.Windows.Forms.Button();
            this.btn_fr_start = new System.Windows.Forms.Button();
            this.lbl_fr_time = new System.Windows.Forms.TextBox();
            this.btn_fr_end = new System.Windows.Forms.Button();
            this.btn_10 = new System.Windows.Forms.Button();
            this.btn_minus10 = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.save_img = new System.Windows.Forms.SaveFileDialog();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.BG_Keyframes = new System.ComponentModel.BackgroundWorker();
            this.txt_file = new System.Windows.Forms.Label();
            this.glassExtenderProvider1 = new Vanara.Interop.DesktopWindowManager.GlassExtenderProvider();
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).BeginInit();
            this.menu_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_frame)).BeginInit();
            this.menu_Img.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_prog)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).BeginInit();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.pic_frame.ContextMenuStrip = this.menu_Img;
            resources.ApplyResources(this.pic_frame, "pic_frame");
            this.pic_frame.Name = "pic_frame";
            this.pic_frame.TabStop = false;
            this.pic_frame.DoubleClick += new System.EventHandler(this.pic_frame_DoubleClick);
            // 
            // menu_Img
            // 
            this.menu_Img.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ct_copy,
            this.ct_save});
            this.menu_Img.Name = "menu_Img";
            resources.ApplyResources(this.menu_Img, "menu_Img");
            // 
            // ct_copy
            // 
            resources.ApplyResources(this.ct_copy, "ct_copy");
            this.ct_copy.Name = "ct_copy";
            this.ct_copy.Click += new System.EventHandler(this.ct_copy_Click);
            // 
            // ct_save
            // 
            resources.ApplyResources(this.ct_save, "ct_save");
            this.ct_save.Name = "ct_save";
            this.ct_save.Click += new System.EventHandler(this.ct_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_kplus1);
            this.groupBox1.Controls.Add(this.btn_k_m1);
            this.groupBox1.Controls.Add(this.pic_cut);
            this.groupBox1.Controls.Add(this.img_prog);
            this.groupBox1.Controls.Add(this.lbl_lapse);
            this.groupBox1.Controls.Add(this.lbl_end);
            this.groupBox1.Controls.Add(this.lbl_start);
            this.groupBox1.Controls.Add(this.trim_right);
            this.groupBox1.Controls.Add(this.trim_left);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.btn_copy);
            this.groupBox1.Controls.Add(this.trackB);
            this.groupBox1.Controls.Add(this.btn_save);
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
            // btn_kplus1
            // 
            resources.ApplyResources(this.btn_kplus1, "btn_kplus1");
            this.btn_kplus1.Name = "btn_kplus1";
            this.btn_kplus1.UseVisualStyleBackColor = true;
            this.btn_kplus1.Click += new System.EventHandler(this.btn_kplus1_Click);
            // 
            // btn_k_m1
            // 
            resources.ApplyResources(this.btn_k_m1, "btn_k_m1");
            this.btn_k_m1.Name = "btn_k_m1";
            this.btn_k_m1.UseVisualStyleBackColor = true;
            this.btn_k_m1.Click += new System.EventHandler(this.btn_k_m1_Click);
            // 
            // pic_cut
            // 
            resources.ApplyResources(this.pic_cut, "pic_cut");
            this.pic_cut.Name = "pic_cut";
            this.pic_cut.TabStop = false;
            // 
            // img_prog
            // 
            resources.ApplyResources(this.img_prog, "img_prog");
            this.img_prog.Name = "img_prog";
            this.img_prog.TabStop = false;
            // 
            // lbl_lapse
            // 
            resources.ApplyResources(this.lbl_lapse, "lbl_lapse");
            this.lbl_lapse.Name = "lbl_lapse";
            // 
            // lbl_end
            // 
            resources.ApplyResources(this.lbl_end, "lbl_end");
            this.lbl_end.Name = "lbl_end";
            // 
            // lbl_start
            // 
            resources.ApplyResources(this.lbl_start, "lbl_start");
            this.lbl_start.Name = "lbl_start";
            // 
            // trim_right
            // 
            resources.ApplyResources(this.trim_right, "trim_right");
            this.trim_right.Name = "trim_right";
            this.trim_right.UseVisualStyleBackColor = true;
            this.trim_right.Click += new System.EventHandler(this.trim_right_Click);
            // 
            // trim_left
            // 
            resources.ApplyResources(this.trim_left, "trim_left");
            this.trim_left.Name = "trim_left";
            this.trim_left.UseVisualStyleBackColor = true;
            this.trim_left.Click += new System.EventHandler(this.trim_left_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.pg1);
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.lbl_prog);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // pg1
            // 
            resources.ApplyResources(this.pg1, "pg1");
            this.pg1.Name = "pg1";
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_prog
            // 
            this.lbl_prog.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lbl_prog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            resources.ApplyResources(this.lbl_prog, "lbl_prog");
            this.lbl_prog.Name = "lbl_prog";
            // 
            // btn_copy
            // 
            this.btn_copy.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_copy.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_copy, "btn_copy");
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // trackB
            // 
            resources.ApplyResources(this.trackB, "trackB");
            this.trackB.LargeChange = 2;
            this.trackB.Name = "trackB";
            this.trackB.ValueChanged += new System.EventHandler(this.trackB_ValueChanged);
            // 
            // btn_save
            // 
            this.btn_save.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_save.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_plus1
            // 
            resources.ApplyResources(this.btn_plus1, "btn_plus1");
            this.btn_plus1.Name = "btn_plus1";
            this.btn_plus1.UseVisualStyleBackColor = true;
            this.btn_plus1.Click += new System.EventHandler(this.btn_plus1_Click);
            // 
            // btn_min1
            // 
            resources.ApplyResources(this.btn_min1, "btn_min1");
            this.btn_min1.Name = "btn_min1";
            this.btn_min1.UseVisualStyleBackColor = true;
            this.btn_min1.Click += new System.EventHandler(this.btn_min1_Click);
            // 
            // btn_fr_start
            // 
            resources.ApplyResources(this.btn_fr_start, "btn_fr_start");
            this.btn_fr_start.Name = "btn_fr_start";
            this.btn_fr_start.UseVisualStyleBackColor = true;
            this.btn_fr_start.Click += new System.EventHandler(this.btn_fr_start_Click);
            // 
            // lbl_fr_time
            // 
            this.lbl_fr_time.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.lbl_fr_time, "lbl_fr_time");
            this.lbl_fr_time.Name = "lbl_fr_time";
            this.lbl_fr_time.TextChanged += new System.EventHandler(this.lbl_fr_time_TextChanged);
            // 
            // btn_fr_end
            // 
            resources.ApplyResources(this.btn_fr_end, "btn_fr_end");
            this.btn_fr_end.Name = "btn_fr_end";
            this.btn_fr_end.UseVisualStyleBackColor = true;
            this.btn_fr_end.Click += new System.EventHandler(this.btn_fr_end_Click);
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
            // btn_refresh
            // 
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_refresh, "btn_refresh");
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
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
            // save_img
            // 
            this.save_img.FileOk += new System.ComponentModel.CancelEventHandler(this.save_img_FileOk);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // BG_Keyframes
            // 
            this.BG_Keyframes.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BG_Keyframes_DoWork);
            this.BG_Keyframes.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BG_Keyframes_RunWorkerCompleted);
            // 
            // txt_file
            // 
            resources.ApplyResources(this.txt_file, "txt_file");
            this.txt_file.Name = "txt_file";
            // 
            // Form5
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.txt_file);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.dg_streams);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_refresh);
            this.glassExtenderProvider1.SetGlassMargins(this, new System.Windows.Forms.Padding(0));
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form5_FormClosing);
            this.Load += new System.EventHandler(this.Form5_Load);
            this.Shown += new System.EventHandler(this.Form5_Shown);
            this.Resize += new System.EventHandler(this.Form5_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).EndInit();
            this.menu_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_frame)).EndInit();
            this.menu_Img.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_cut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.img_prog)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_streams;
        private System.Windows.Forms.ImageList img_streams;
        private System.Windows.Forms.ContextMenuStrip menu_grid;
        private System.Windows.Forms.ToolStripMenuItem ct1;
        private System.Diagnostics.Process ff_str;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.ContextMenuStrip menu_Img;
        private System.Windows.Forms.ToolStripMenuItem ct_save;
        private System.Windows.Forms.ToolStripMenuItem ct_copy;
        private System.Windows.Forms.SaveFileDialog save_img;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_refresh;
        public System.Windows.Forms.PictureBox pic_frame;
        private System.ComponentModel.BackgroundWorker BG_Keyframes;
        private System.Windows.Forms.Label txt_file;
        private Vanara.Interop.DesktopWindowManager.GlassExtenderProvider glassExtenderProvider1;
        private System.Windows.Forms.Button btn_kplus1;
        private System.Windows.Forms.Button btn_k_m1;
        private System.Windows.Forms.PictureBox pic_cut;
        private System.Windows.Forms.PictureBox img_prog;
        private System.Windows.Forms.Label lbl_lapse;
        private System.Windows.Forms.Label lbl_end;
        private System.Windows.Forms.Label lbl_start;
        private System.Windows.Forms.Button trim_right;
        private System.Windows.Forms.Button trim_left;
        private System.Windows.Forms.Panel panel2;
        private ProgressBarWithText pg1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_prog;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.TrackBar trackB;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_plus1;
        private System.Windows.Forms.Button btn_min1;
        private System.Windows.Forms.Button btn_fr_start;
        private System.Windows.Forms.TextBox lbl_fr_time;
        private System.Windows.Forms.Button btn_fr_end;
        private System.Windows.Forms.Button btn_10;
        private System.Windows.Forms.Button btn_minus10;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}