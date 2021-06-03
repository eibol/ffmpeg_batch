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
            this.txt_file = new System.Windows.Forms.TextBox();
            this.img_streams = new System.Windows.Forms.ImageList(this.components);
            this.btn_close = new System.Windows.Forms.Button();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.ff_str = new System.Diagnostics.Process();
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).BeginInit();
            this.menu_grid.SuspendLayout();
            this.SuspendLayout();
            // 
            // dg_streams
            // 
            resources.ApplyResources(this.dg_streams, "dg_streams");
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
            resources.ApplyResources(this.menu_grid, "menu_grid");
            this.menu_grid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ct1});
            this.menu_grid.Name = "menu_grid";
            this.menu_grid.Opening += new System.ComponentModel.CancelEventHandler(this.menu_grid_Opening);
            // 
            // ct1
            // 
            resources.ApplyResources(this.ct1, "ct1");
            this.ct1.Name = "ct1";
            this.ct1.Click += new System.EventHandler(this.ct1_Click);
            // 
            // txt_file
            // 
            resources.ApplyResources(this.txt_file, "txt_file");
            this.txt_file.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_file.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            // btn_close
            // 
            resources.ApplyResources(this.btn_close, "btn_close");
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Name = "btn_close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_name
            // 
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
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
            // Form5
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_close;
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_file);
            this.Controls.Add(this.dg_streams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form5";
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).EndInit();
            this.menu_grid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_streams;
        private System.Windows.Forms.TextBox txt_file;
        private System.Windows.Forms.ImageList img_streams;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.ContextMenuStrip menu_grid;
        private System.Windows.Forms.ToolStripMenuItem ct1;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Diagnostics.Process ff_str;
    }
}