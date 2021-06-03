namespace FFBatch
{
    partial class Form4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            this.cb_filterby = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_value_f = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_action = new System.Windows.Forms.ComboBox();
            this.btn_apply = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.chk_invalid = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_streams = new System.Windows.Forms.ComboBox();
            this.lb_of = new System.Windows.Forms.Label();
            this.chk_novideo = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.n_width = new System.Windows.Forms.NumericUpDown();
            this.n_height = new System.Windows.Forms.NumericUpDown();
            this.lbl_size = new System.Windows.Forms.Label();
            this.btn_mediainfo = new System.Windows.Forms.Button();
            this.lbl_greater = new System.Windows.Forms.Label();
            this.btn_streams = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_info_search = new System.Windows.Forms.TextBox();
            this.img_streams = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.n_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_height)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_filterby
            // 
            this.cb_filterby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_filterby.FormattingEnabled = true;
            this.cb_filterby.Items.AddRange(new object[] {
            "Status",
            "File extension",
            "File size",
            "File bitrate",
            "Video codec",
            "Video bitrate",
            "Audio codec",
            "Frame rate",
            "Frame size",
            "Custom string (FF)",
            "Custom string (MI)",
            "Metadata"});
            this.cb_filterby.Location = new System.Drawing.Point(24, 40);
            this.cb_filterby.Name = "cb_filterby";
            this.cb_filterby.Size = new System.Drawing.Size(124, 21);
            this.cb_filterby.TabIndex = 0;
            this.cb_filterby.SelectedIndexChanged += new System.EventHandler(this.cb_filterby_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Filter by";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Value";
            // 
            // cb_value_f
            // 
            this.cb_value_f.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_value_f.FormattingEnabled = true;
            this.cb_value_f.Location = new System.Drawing.Point(329, 40);
            this.cb_value_f.Name = "cb_value_f";
            this.cb_value_f.Size = new System.Drawing.Size(81, 21);
            this.cb_value_f.TabIndex = 2;
            this.cb_value_f.SelectedIndexChanged += new System.EventHandler(this.cb_value_f_SelectedIndexChanged);
            this.cb_value_f.DropDownClosed += new System.EventHandler(this.cb_value_f_DropDownClosed);
            this.cb_value_f.TextChanged += new System.EventHandler(this.cb_value_f_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(467, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Action";
            // 
            // cb_action
            // 
            this.cb_action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_action.FormattingEnabled = true;
            this.cb_action.Items.AddRange(new object[] {
            "Keep",
            "Remove"});
            this.cb_action.Location = new System.Drawing.Point(470, 40);
            this.cb_action.Name = "cb_action";
            this.cb_action.Size = new System.Drawing.Size(75, 21);
            this.cb_action.TabIndex = 4;
            this.cb_action.SelectedIndexChanged += new System.EventHandler(this.cb_action_SelectedIndexChanged);
            // 
            // btn_apply
            // 
            this.btn_apply.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_apply.FlatAppearance.BorderSize = 0;
            this.btn_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_apply.Image = ((System.Drawing.Image)(resources.GetObject("btn_apply.Image")));
            this.btn_apply.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_apply.Location = new System.Drawing.Point(511, 137);
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.Size = new System.Drawing.Size(55, 63);
            this.btn_apply.TabIndex = 6;
            this.btn_apply.Text = " Apply";
            this.btn_apply.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cancel.Location = new System.Drawing.Point(454, 137);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(55, 64);
            this.btn_cancel.TabIndex = 7;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // chk_invalid
            // 
            this.chk_invalid.Checked = true;
            this.chk_invalid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_invalid.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chk_invalid.Location = new System.Drawing.Point(171, 175);
            this.chk_invalid.Name = "chk_invalid";
            this.chk_invalid.Size = new System.Drawing.Size(239, 17);
            this.chk_invalid.TabIndex = 8;
            this.chk_invalid.Text = "Remove invalid items (with N/A or 0 duration)";
            this.chk_invalid.UseVisualStyleBackColor = true;
            this.chk_invalid.CheckedChanged += new System.EventHandler(this.chk_invalid_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(315, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "if";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(440, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "then";
            // 
            // cb_streams
            // 
            this.cb_streams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_streams.Enabled = false;
            this.cb_streams.FormattingEnabled = true;
            this.cb_streams.Location = new System.Drawing.Point(185, 40);
            this.cb_streams.Name = "cb_streams";
            this.cb_streams.Size = new System.Drawing.Size(101, 21);
            this.cb_streams.TabIndex = 12;
            this.cb_streams.SelectedIndexChanged += new System.EventHandler(this.cb_streams_SelectedIndexChanged);
            // 
            // lb_of
            // 
            this.lb_of.AutoSize = true;
            this.lb_of.Location = new System.Drawing.Point(168, 44);
            this.lb_of.Name = "lb_of";
            this.lb_of.Size = new System.Drawing.Size(16, 13);
            this.lb_of.TabIndex = 13;
            this.lb_of.Text = "of";
            // 
            // chk_novideo
            // 
            this.chk_novideo.AutoSize = true;
            this.chk_novideo.Checked = true;
            this.chk_novideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_novideo.Enabled = false;
            this.chk_novideo.Location = new System.Drawing.Point(171, 152);
            this.chk_novideo.Name = "chk_novideo";
            this.chk_novideo.Size = new System.Drawing.Size(205, 17);
            this.chk_novideo.TabIndex = 15;
            this.chk_novideo.Text = "Remove files with no 0:0 video stream";
            this.chk_novideo.UseVisualStyleBackColor = true;
            this.chk_novideo.CheckedChanged += new System.EventHandler(this.chk_novideo_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(182, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Selector";
            // 
            // n_width
            // 
            this.n_width.Enabled = false;
            this.n_width.Location = new System.Drawing.Point(312, 72);
            this.n_width.Maximum = new decimal(new int[] {
            3840,
            0,
            0,
            0});
            this.n_width.Minimum = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.n_width.Name = "n_width";
            this.n_width.Size = new System.Drawing.Size(50, 20);
            this.n_width.TabIndex = 37;
            this.n_width.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            this.n_width.Visible = false;
            // 
            // n_height
            // 
            this.n_height.Enabled = false;
            this.n_height.Location = new System.Drawing.Point(379, 72);
            this.n_height.Maximum = new decimal(new int[] {
            2160,
            0,
            0,
            0});
            this.n_height.Minimum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.n_height.Name = "n_height";
            this.n_height.Size = new System.Drawing.Size(50, 20);
            this.n_height.TabIndex = 38;
            this.n_height.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.n_height.Visible = false;
            // 
            // lbl_size
            // 
            this.lbl_size.Font = new System.Drawing.Font("Segoe UI", 7.926606F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_size.Location = new System.Drawing.Point(364, 75);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(11, 13);
            this.lbl_size.TabIndex = 39;
            this.lbl_size.Text = "X";
            this.lbl_size.Visible = false;
            // 
            // btn_mediainfo
            // 
            this.btn_mediainfo.FlatAppearance.BorderSize = 0;
            this.btn_mediainfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_mediainfo.Image = ((System.Drawing.Image)(resources.GetObject("btn_mediainfo.Image")));
            this.btn_mediainfo.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_mediainfo.Location = new System.Drawing.Point(12, 138);
            this.btn_mediainfo.Name = "btn_mediainfo";
            this.btn_mediainfo.Size = new System.Drawing.Size(55, 63);
            this.btn_mediainfo.TabIndex = 40;
            this.btn_mediainfo.Text = "File info";
            this.btn_mediainfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_mediainfo.UseVisualStyleBackColor = true;
            this.btn_mediainfo.Click += new System.EventHandler(this.btn_mediainfo_Click);
            // 
            // lbl_greater
            // 
            this.lbl_greater.AutoSize = true;
            this.lbl_greater.Location = new System.Drawing.Point(364, 21);
            this.lbl_greater.Name = "lbl_greater";
            this.lbl_greater.Size = new System.Drawing.Size(38, 13);
            this.lbl_greater.TabIndex = 41;
            this.lbl_greater.Text = ">= MB";
            this.lbl_greater.Visible = false;
            // 
            // btn_streams
            // 
            this.btn_streams.FlatAppearance.BorderSize = 0;
            this.btn_streams.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_streams.Image = ((System.Drawing.Image)(resources.GetObject("btn_streams.Image")));
            this.btn_streams.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_streams.Location = new System.Drawing.Point(73, 137);
            this.btn_streams.Name = "btn_streams";
            this.btn_streams.Size = new System.Drawing.Size(55, 64);
            this.btn_streams.TabIndex = 43;
            this.btn_streams.Text = "Streams";
            this.btn_streams.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_streams.UseVisualStyleBackColor = true;
            this.btn_streams.Click += new System.EventHandler(this.btn_streams_Click_1);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Location = new System.Drawing.Point(16, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(543, 2);
            this.label6.TabIndex = 44;
            // 
            // lbl_info_search
            // 
            this.lbl_info_search.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lbl_info_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbl_info_search.Location = new System.Drawing.Point(136, 108);
            this.lbl_info_search.Name = "lbl_info_search";
            this.lbl_info_search.ReadOnly = true;
            this.lbl_info_search.Size = new System.Drawing.Size(293, 13);
            this.lbl_info_search.TabIndex = 45;
            this.lbl_info_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.img_streams.Images.SetKeyName(5, "Visualpharm-Must-Have-Text-Document.ico");
            // 
            // Form4
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(571, 209);
            this.Controls.Add(this.lbl_info_search);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_streams);
            this.Controls.Add(this.lbl_greater);
            this.Controls.Add(this.btn_mediainfo);
            this.Controls.Add(this.lbl_size);
            this.Controls.Add(this.n_height);
            this.Controls.Add(this.n_width);
            this.Controls.Add(this.cb_streams);
            this.Controls.Add(this.cb_action);
            this.Controls.Add(this.cb_value_f);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.chk_novideo);
            this.Controls.Add(this.lb_of);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chk_invalid);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_apply);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_filterby);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.n_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_filterby;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_value_f;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_action;
        private System.Windows.Forms.Button btn_apply;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.CheckBox chk_invalid;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cb_streams;
        private System.Windows.Forms.Label lb_of;
        private System.Windows.Forms.CheckBox chk_novideo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown n_width;
        private System.Windows.Forms.NumericUpDown n_height;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.Button btn_mediainfo;
        private System.Windows.Forms.Label lbl_greater;
        private System.Windows.Forms.Button btn_streams;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox lbl_info_search;
        private System.Windows.Forms.ImageList img_streams;
    }
}