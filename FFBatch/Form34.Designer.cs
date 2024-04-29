namespace FFBatch
{
    partial class Form34
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form34));
            this.btn_br_ff = new System.Windows.Forms.Button();
            this.btn_down_g = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.lbl_ff_v = new System.Windows.Forms.Label();
            this.lbl_expl = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_size = new System.Windows.Forms.Label();
            this.lbl_d_v = new System.Windows.Forms.Label();
            this.lbl_srv = new System.Windows.Forms.Label();
            this.cb_srv = new System.Windows.Forms.ComboBox();
            this.lbl_val = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_br_ff
            // 
            this.btn_br_ff.FlatAppearance.BorderSize = 0;
            this.btn_br_ff.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_br_ff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_br_ff.Image = ((System.Drawing.Image)(resources.GetObject("btn_br_ff.Image")));
            this.btn_br_ff.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_br_ff.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_br_ff.Location = new System.Drawing.Point(6, 11);
            this.btn_br_ff.Name = "btn_br_ff";
            this.btn_br_ff.Size = new System.Drawing.Size(74, 90);
            this.btn_br_ff.TabIndex = 157;
            this.btn_br_ff.Text = "Browse ffmpeg.exe";
            this.btn_br_ff.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_br_ff.UseVisualStyleBackColor = true;
            this.btn_br_ff.Click += new System.EventHandler(this.btn_br_ff_Click);
            // 
            // btn_down_g
            // 
            this.btn_down_g.FlatAppearance.BorderSize = 0;
            this.btn_down_g.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_down_g.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_down_g.Image = ((System.Drawing.Image)(resources.GetObject("btn_down_g.Image")));
            this.btn_down_g.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_down_g.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_down_g.Location = new System.Drawing.Point(104, 12);
            this.btn_down_g.Name = "btn_down_g";
            this.btn_down_g.Size = new System.Drawing.Size(80, 90);
            this.btn_down_g.TabIndex = 159;
            this.btn_down_g.Text = "Download ffmpeg";
            this.btn_down_g.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_down_g.UseVisualStyleBackColor = true;
            this.btn_down_g.Click += new System.EventHandler(this.btn_down_g_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_close.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_close.Location = new System.Drawing.Point(20, 220);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(301, 28);
            this.btn_close.TabIndex = 160;
            this.btn_close.Text = "Close window";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // lbl_ff_v
            // 
            this.lbl_ff_v.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ff_v.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_ff_v.Location = new System.Drawing.Point(17, 17);
            this.lbl_ff_v.Name = "lbl_ff_v";
            this.lbl_ff_v.Size = new System.Drawing.Size(304, 16);
            this.lbl_ff_v.TabIndex = 162;
            this.lbl_ff_v.Text = "Not found";
            this.lbl_ff_v.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_expl
            // 
            this.lbl_expl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_expl.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_expl.Location = new System.Drawing.Point(17, 159);
            this.lbl_expl.Name = "lbl_expl";
            this.lbl_expl.Size = new System.Drawing.Size(304, 13);
            this.lbl_expl.TabIndex = 163;
            this.lbl_expl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_size);
            this.groupBox1.Controls.Add(this.lbl_d_v);
            this.groupBox1.Controls.Add(this.lbl_srv);
            this.groupBox1.Controls.Add(this.cb_srv);
            this.groupBox1.Controls.Add(this.btn_br_ff);
            this.groupBox1.Controls.Add(this.btn_down_g);
            this.groupBox1.Location = new System.Drawing.Point(17, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 115);
            this.groupBox1.TabIndex = 164;
            this.groupBox1.TabStop = false;
            // 
            // lbl_size
            // 
            this.lbl_size.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_size.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_size.Location = new System.Drawing.Point(187, 86);
            this.lbl_size.Name = "lbl_size";
            this.lbl_size.Size = new System.Drawing.Size(89, 15);
            this.lbl_size.TabIndex = 168;
            this.lbl_size.Text = "Size: 43 MB";
            this.lbl_size.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_d_v
            // 
            this.lbl_d_v.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_d_v.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_d_v.Location = new System.Drawing.Point(187, 72);
            this.lbl_d_v.Name = "lbl_d_v";
            this.lbl_d_v.Size = new System.Drawing.Size(89, 13);
            this.lbl_d_v.TabIndex = 167;
            this.lbl_d_v.Text = "v7 Full";
            this.lbl_d_v.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_srv
            // 
            this.lbl_srv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_srv.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_srv.Location = new System.Drawing.Point(190, 23);
            this.lbl_srv.Name = "lbl_srv";
            this.lbl_srv.Size = new System.Drawing.Size(89, 13);
            this.lbl_srv.TabIndex = 165;
            this.lbl_srv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cb_srv
            // 
            this.cb_srv.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_srv.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cb_srv.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.cb_srv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_srv.Font = new System.Drawing.Font("Segoe UI", 7.926606F);
            this.cb_srv.FormattingEnabled = true;
            this.cb_srv.Items.AddRange(new object[] {
            "Gyan.dev",
            "GitHub",
            "Mirror #1",
            "Mirror #2"});
            this.cb_srv.Location = new System.Drawing.Point(190, 41);
            this.cb_srv.Name = "cb_srv";
            this.cb_srv.Size = new System.Drawing.Size(91, 21);
            this.cb_srv.TabIndex = 160;
            this.cb_srv.SelectedIndexChanged += new System.EventHandler(this.cb_srv_SelectedIndexChanged);
            // 
            // lbl_val
            // 
            this.lbl_val.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_val.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_val.Location = new System.Drawing.Point(17, 177);
            this.lbl_val.Name = "lbl_val";
            this.lbl_val.Size = new System.Drawing.Size(304, 13);
            this.lbl_val.TabIndex = 165;
            this.lbl_val.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form34
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(339, 260);
            this.Controls.Add(this.lbl_val);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_expl);
            this.Controls.Add(this.lbl_ff_v);
            this.Controls.Add(this.btn_close);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form34";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Locate ffmpeg.exe";
            this.Load += new System.EventHandler(this.Form34_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_br_ff;
        private System.Windows.Forms.Button btn_down_g;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label lbl_expl;
        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.Label lbl_ff_v;
        private System.Windows.Forms.ComboBox cb_srv;
        private System.Windows.Forms.Label lbl_srv;
        private System.Windows.Forms.Label lbl_d_v;
        private System.Windows.Forms.Label lbl_size;
        private System.Windows.Forms.Label lbl_val;
    }
}