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
            this.txt_app = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.n_width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_height)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_filterby
            // 
            this.cb_filterby.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_filterby.FormattingEnabled = true;
            resources.ApplyResources(this.cb_filterby, "cb_filterby");
            this.cb_filterby.Name = "cb_filterby";
            this.cb_filterby.SelectedIndexChanged += new System.EventHandler(this.cb_filterby_SelectedIndexChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cb_value_f
            // 
            this.cb_value_f.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_value_f.FormattingEnabled = true;
            resources.ApplyResources(this.cb_value_f, "cb_value_f");
            this.cb_value_f.Name = "cb_value_f";
            this.cb_value_f.SelectedIndexChanged += new System.EventHandler(this.cb_value_f_SelectedIndexChanged);
            this.cb_value_f.DropDownClosed += new System.EventHandler(this.cb_value_f_DropDownClosed);
            this.cb_value_f.TextChanged += new System.EventHandler(this.cb_value_f_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cb_action
            // 
            this.cb_action.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_action.FormattingEnabled = true;
            resources.ApplyResources(this.cb_action, "cb_action");
            this.cb_action.Name = "cb_action";
            this.cb_action.SelectedIndexChanged += new System.EventHandler(this.cb_action_SelectedIndexChanged);
            // 
            // btn_apply
            // 
            this.btn_apply.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_apply.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_apply, "btn_apply");
            this.btn_apply.Name = "btn_apply";
            this.btn_apply.UseVisualStyleBackColor = true;
            this.btn_apply.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // chk_invalid
            // 
            this.chk_invalid.Checked = true;
            this.chk_invalid.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.chk_invalid, "chk_invalid");
            this.chk_invalid.Name = "chk_invalid";
            this.chk_invalid.UseVisualStyleBackColor = true;
            this.chk_invalid.CheckedChanged += new System.EventHandler(this.chk_invalid_CheckedChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // cb_streams
            // 
            this.cb_streams.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_streams.FormattingEnabled = true;
            resources.ApplyResources(this.cb_streams, "cb_streams");
            this.cb_streams.Name = "cb_streams";
            this.cb_streams.SelectedIndexChanged += new System.EventHandler(this.cb_streams_SelectedIndexChanged);
            // 
            // lb_of
            // 
            resources.ApplyResources(this.lb_of, "lb_of");
            this.lb_of.Name = "lb_of";
            // 
            // chk_novideo
            // 
            resources.ApplyResources(this.chk_novideo, "chk_novideo");
            this.chk_novideo.Checked = true;
            this.chk_novideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_novideo.Name = "chk_novideo";
            this.chk_novideo.UseVisualStyleBackColor = true;
            this.chk_novideo.CheckedChanged += new System.EventHandler(this.chk_novideo_CheckedChanged);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // n_width
            // 
            resources.ApplyResources(this.n_width, "n_width");
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
            this.n_width.Value = new decimal(new int[] {
            160,
            0,
            0,
            0});
            // 
            // n_height
            // 
            resources.ApplyResources(this.n_height, "n_height");
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
            this.n_height.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // lbl_size
            // 
            resources.ApplyResources(this.lbl_size, "lbl_size");
            this.lbl_size.Name = "lbl_size";
            // 
            // btn_mediainfo
            // 
            this.btn_mediainfo.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_mediainfo, "btn_mediainfo");
            this.btn_mediainfo.Name = "btn_mediainfo";
            this.btn_mediainfo.UseVisualStyleBackColor = true;
            this.btn_mediainfo.Click += new System.EventHandler(this.btn_mediainfo_Click);
            // 
            // lbl_greater
            // 
            resources.ApplyResources(this.lbl_greater, "lbl_greater");
            this.lbl_greater.Name = "lbl_greater";
            // 
            // btn_streams
            // 
            this.btn_streams.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_streams, "btn_streams");
            this.btn_streams.Name = "btn_streams";
            this.btn_streams.UseVisualStyleBackColor = true;
            this.btn_streams.Click += new System.EventHandler(this.btn_streams_Click_1);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // lbl_info_search
            // 
            this.lbl_info_search.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lbl_info_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.lbl_info_search, "lbl_info_search");
            this.lbl_info_search.Name = "lbl_info_search";
            this.lbl_info_search.ReadOnly = true;
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
            // txt_app
            // 
            this.txt_app.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_app.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.txt_app, "txt_app");
            this.txt_app.Name = "txt_app";
            this.txt_app.ReadOnly = true;
            // 
            // Form4
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.ControlBox = false;
            this.Controls.Add(this.txt_app);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form4";
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.n_width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_height)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_value_f;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_action;
        private System.Windows.Forms.Button btn_apply;
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
        private System.Windows.Forms.TextBox txt_app;
        public System.Windows.Forms.ComboBox cb_filterby;
        public System.Windows.Forms.Button btn_cancel;
    }
}