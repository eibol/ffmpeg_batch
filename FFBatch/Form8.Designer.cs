namespace FFBatch
{
    partial class Form8
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form8));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.txt_file = new System.Windows.Forms.TextBox();
            this.dg_streams = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Res_p = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_bitrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.image_streams = new System.Windows.Forms.ImageList(this.components);
            this.btn_select = new System.Windows.Forms.Button();
            this.pic_yout = new System.Windows.Forms.PictureBox();
            this.pic_wait_1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_yout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_wait_1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_name
            // 
            resources.ApplyResources(this.txt_name, "txt_name");
            this.txt_name.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            // 
            // btn_close
            // 
            resources.ApplyResources(this.btn_close, "btn_close");
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Name = "btn_close";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txt_file
            // 
            resources.ApplyResources(this.txt_file, "txt_file");
            this.txt_file.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_file.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_file.Name = "txt_file";
            this.txt_file.ReadOnly = true;
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
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Codec,
            this.Res_p,
            this.col_bitrate});
            this.dg_streams.MultiSelect = false;
            this.dg_streams.Name = "dg_streams";
            this.dg_streams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 25F;
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Image = ((System.Drawing.Image)(resources.GetObject("Column3.Image")));
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column6
            // 
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            // 
            // Column1
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            // 
            // Column4
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            // 
            // Codec
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Codec.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(this.Codec, "Codec");
            this.Codec.Name = "Codec";
            // 
            // Res_p
            // 
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Res_p.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(this.Res_p, "Res_p");
            this.Res_p.Name = "Res_p";
            // 
            // col_bitrate
            // 
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.col_bitrate.DefaultCellStyle = dataGridViewCellStyle12;
            resources.ApplyResources(this.col_bitrate, "col_bitrate");
            this.col_bitrate.Name = "col_bitrate";
            // 
            // image_streams
            // 
            this.image_streams.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("image_streams.ImageStream")));
            this.image_streams.TransparentColor = System.Drawing.Color.Transparent;
            this.image_streams.Images.SetKeyName(0, "youtube_menu.jpg");
            this.image_streams.Images.SetKeyName(1, "audio_icon_22.png");
            // 
            // btn_select
            // 
            resources.ApplyResources(this.btn_select, "btn_select");
            this.btn_select.Name = "btn_select";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // pic_yout
            // 
            resources.ApplyResources(this.pic_yout, "pic_yout");
            this.pic_yout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_yout.Name = "pic_yout";
            this.pic_yout.TabStop = false;
            // 
            // pic_wait_1
            // 
            resources.ApplyResources(this.pic_wait_1, "pic_wait_1");
            this.pic_wait_1.Name = "pic_wait_1";
            this.pic_wait_1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Form8
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic_wait_1);
            this.Controls.Add(this.pic_yout);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_file);
            this.Controls.Add(this.dg_streams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form8";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form8_FormClosing);
            this.Load += new System.EventHandler(this.Form8_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_yout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_wait_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.TextBox txt_file;
        private System.Windows.Forms.DataGridView dg_streams;
        private System.Windows.Forms.ImageList image_streams;
        private System.Windows.Forms.Button btn_select;
        public System.Windows.Forms.PictureBox pic_yout;
        private System.Windows.Forms.PictureBox pic_wait_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewImageColumn Column3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Res_p;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_bitrate;
    }
}