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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.btn_close = new System.Windows.Forms.Button();
            this.txt_file = new System.Windows.Forms.TextBox();
            this.dg_streams = new System.Windows.Forms.DataGridView();
            this.image_streams = new System.Windows.Forms.ImageList(this.components);
            this.btn_select = new System.Windows.Forms.Button();
            this.pic_yout = new System.Windows.Forms.PictureBox();
            this.pic_wait_1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Column3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg_streams)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_yout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_wait_1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_name
            // 
            this.txt_name.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_name.Location = new System.Drawing.Point(15, 10);
            this.txt_name.Name = "txt_name";
            this.txt_name.ReadOnly = true;
            this.txt_name.Size = new System.Drawing.Size(694, 20);
            this.txt_name.TabIndex = 7;
            this.txt_name.Text = "OBTAINING YOUTUBE STREAMS...";
            this.txt_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_close
            // 
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.Location = new System.Drawing.Point(375, 655);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(331, 25);
            this.btn_close.TabIndex = 6;
            this.btn_close.Text = "Close window";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // txt_file
            // 
            this.txt_file.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_file.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_file.Enabled = false;
            this.txt_file.Location = new System.Drawing.Point(17, 37);
            this.txt_file.Name = "txt_file";
            this.txt_file.ReadOnly = true;
            this.txt_file.Size = new System.Drawing.Size(691, 13);
            this.txt_file.TabIndex = 2;
            this.txt_file.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Codec});
            this.dg_streams.Location = new System.Drawing.Point(15, 138);
            this.dg_streams.MultiSelect = false;
            this.dg_streams.Name = "dg_streams";
            this.dg_streams.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dg_streams.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_streams.Size = new System.Drawing.Size(697, 511);
            this.dg_streams.TabIndex = 4;
            this.dg_streams.Visible = false;
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
            this.btn_select.Location = new System.Drawing.Point(14, 655);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(331, 25);
            this.btn_select.TabIndex = 8;
            this.btn_select.Text = "Use selected format ID";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // pic_yout
            // 
            this.pic_yout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic_yout.Image = ((System.Drawing.Image)(resources.GetObject("pic_yout.Image")));
            this.pic_yout.Location = new System.Drawing.Point(299, 60);
            this.pic_yout.Name = "pic_yout";
            this.pic_yout.Size = new System.Drawing.Size(119, 67);
            this.pic_yout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_yout.TabIndex = 127;
            this.pic_yout.TabStop = false;
            this.pic_yout.Visible = false;
            // 
            // pic_wait_1
            // 
            this.pic_wait_1.Image = ((System.Drawing.Image)(resources.GetObject("pic_wait_1.Image")));
            this.pic_wait_1.Location = new System.Drawing.Point(342, 55);
            this.pic_wait_1.Name = "pic_wait_1";
            this.pic_wait_1.Size = new System.Drawing.Size(35, 35);
            this.pic_wait_1.TabIndex = 146;
            this.pic_wait_1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(257, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 29);
            this.label1.TabIndex = 147;
            this.label1.Text = "No streams found";
            this.label1.Visible = false;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.FillWeight = 25F;
            this.Column3.HeaderText = "";
            this.Column3.Image = ((System.Drawing.Image)(resources.GetObject("Column3.Image")));
            this.Column3.Name = "Column3";
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Use";
            this.Column6.Name = "Column6";
            this.Column6.Width = 30;
            // 
            // Column1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle1;
            this.Column1.HeaderText = "Format ID";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 75;
            // 
            // Column2
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column2.HeaderText = "Extension";
            this.Column2.Name = "Column2";
            this.Column2.Width = 65;
            // 
            // Column4
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column4.HeaderText = "Resolution";
            this.Column4.Name = "Column4";
            this.Column4.Width = 142;
            // 
            // Codec
            // 
            this.Codec.HeaderText = "Codec";
            this.Codec.Name = "Codec";
            this.Codec.Width = 347;
            // 
            // Form8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(723, 687);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic_wait_1);
            this.Controls.Add(this.pic_yout);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.txt_name);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.txt_file);
            this.Controls.Add(this.dg_streams);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "YOUTUBE AVAILABLE QUALITY STREAMS";
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
    }
}