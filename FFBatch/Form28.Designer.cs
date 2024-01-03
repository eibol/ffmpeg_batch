
namespace FFBatch
{
    partial class Form28
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form28));
            this.txt_seek = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.test_crop = new System.Windows.Forms.Button();
            this.lbl_f = new System.Windows.Forms.Label();
            this.n_secs = new System.Windows.Forms.NumericUpDown();
            this.lbl_sec = new System.Windows.Forms.Label();
            this.btn_detect = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_size1 = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.n_Y = new System.Windows.Forms.NumericUpDown();
            this.n_X = new System.Windows.Forms.NumericUpDown();
            this.n_h = new System.Windows.Forms.NumericUpDown();
            this.n_w = new System.Windows.Forms.NumericUpDown();
            this.btn_frame = new System.Windows.Forms.Button();
            this.pic1 = new System.Windows.Forms.PictureBox();
            this.btn_use = new System.Windows.Forms.Button();
            this.pic_0 = new System.Windows.Forms.PictureBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.lbl_or = new System.Windows.Forms.Label();
            this.lbl_crop = new System.Windows.Forms.Label();
            this.lbl_prop = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.n_secs)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_h)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_w)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_0)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_seek
            // 
            resources.ApplyResources(this.txt_seek, "txt_seek");
            this.txt_seek.Name = "txt_seek";
            this.txt_seek.TextChanged += new System.EventHandler(this.txt_seek_TextChanged);
            this.txt_seek.DoubleClick += new System.EventHandler(this.txt_seek_DoubleClick);
            this.txt_seek.Leave += new System.EventHandler(this.txt_seek_Leave);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // test_crop
            // 
            this.test_crop.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.test_crop, "test_crop");
            this.test_crop.Name = "test_crop";
            this.test_crop.UseVisualStyleBackColor = false;
            this.test_crop.Click += new System.EventHandler(this.test_crop_Click);
            // 
            // lbl_f
            // 
            resources.ApplyResources(this.lbl_f, "lbl_f");
            this.lbl_f.Name = "lbl_f";
            // 
            // n_secs
            // 
            resources.ApplyResources(this.n_secs, "n_secs");
            this.n_secs.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.n_secs.Name = "n_secs";
            this.n_secs.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // lbl_sec
            // 
            resources.ApplyResources(this.lbl_sec, "lbl_sec");
            this.lbl_sec.Name = "lbl_sec";
            // 
            // btn_detect
            // 
            this.btn_detect.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.btn_detect, "btn_detect");
            this.btn_detect.Name = "btn_detect";
            this.btn_detect.UseVisualStyleBackColor = false;
            this.btn_detect.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_size1);
            this.groupBox1.Controls.Add(this.btn_reset);
            this.groupBox1.Controls.Add(this.n_Y);
            this.groupBox1.Controls.Add(this.n_X);
            this.groupBox1.Controls.Add(this.n_h);
            this.groupBox1.Controls.Add(this.n_w);
            this.groupBox1.Controls.Add(this.btn_detect);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.UseCompatibleTextRendering = true;
            // 
            // txt_size1
            // 
            resources.ApplyResources(this.txt_size1, "txt_size1");
            this.txt_size1.Name = "txt_size1";
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.btn_reset, "btn_reset");
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // n_Y
            // 
            resources.ApplyResources(this.n_Y, "n_Y");
            this.n_Y.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.n_Y.Name = "n_Y";
            this.n_Y.ValueChanged += new System.EventHandler(this.n_Y_ValueChanged);
            // 
            // n_X
            // 
            resources.ApplyResources(this.n_X, "n_X");
            this.n_X.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.n_X.Name = "n_X";
            this.n_X.ValueChanged += new System.EventHandler(this.n_X_ValueChanged);
            // 
            // n_h
            // 
            resources.ApplyResources(this.n_h, "n_h");
            this.n_h.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.n_h.Name = "n_h";
            this.n_h.ValueChanged += new System.EventHandler(this.n_h_ValueChanged);
            // 
            // n_w
            // 
            resources.ApplyResources(this.n_w, "n_w");
            this.n_w.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.n_w.Name = "n_w";
            this.n_w.ValueChanged += new System.EventHandler(this.n_w_ValueChanged);
            // 
            // btn_frame
            // 
            this.btn_frame.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.btn_frame, "btn_frame");
            this.btn_frame.Name = "btn_frame";
            this.btn_frame.UseVisualStyleBackColor = false;
            this.btn_frame.Click += new System.EventHandler(this.btn_frame_Click);
            // 
            // pic1
            // 
            this.pic1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pic1, "pic1");
            this.pic1.Name = "pic1";
            this.pic1.TabStop = false;
            // 
            // btn_use
            // 
            this.btn_use.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.btn_use, "btn_use");
            this.btn_use.Name = "btn_use";
            this.btn_use.UseVisualStyleBackColor = false;
            this.btn_use.Click += new System.EventHandler(this.btn_use_Click);
            // 
            // pic_0
            // 
            this.pic_0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pic_0, "pic_0");
            this.pic_0.Name = "pic_0";
            this.pic_0.TabStop = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.SystemColors.InactiveBorder;
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // lbl_or
            // 
            resources.ApplyResources(this.lbl_or, "lbl_or");
            this.lbl_or.Name = "lbl_or";
            // 
            // lbl_crop
            // 
            resources.ApplyResources(this.lbl_crop, "lbl_crop");
            this.lbl_crop.Name = "lbl_crop";
            // 
            // lbl_prop
            // 
            resources.ApplyResources(this.lbl_prop, "lbl_prop");
            this.lbl_prop.Name = "lbl_prop";
            // 
            // Form28
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.Controls.Add(this.lbl_prop);
            this.Controls.Add(this.lbl_crop);
            this.Controls.Add(this.lbl_or);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.pic_0);
            this.Controls.Add(this.btn_use);
            this.Controls.Add(this.btn_frame);
            this.Controls.Add(this.pic1);
            this.Controls.Add(this.lbl_sec);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_f);
            this.Controls.Add(this.txt_seek);
            this.Controls.Add(this.test_crop);
            this.Controls.Add(this.n_secs);
            this.Name = "Form28";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form28_Load);
            this.Resize += new System.EventHandler(this.Form28_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.n_secs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.n_Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_h)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.n_w)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_seek;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button test_crop;
        private System.Windows.Forms.Label lbl_f;
        private System.Windows.Forms.NumericUpDown n_secs;
        private System.Windows.Forms.Label lbl_sec;
        private System.Windows.Forms.Button btn_detect;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pic1;
        private System.Windows.Forms.Button btn_frame;
        private System.Windows.Forms.Button btn_use;
        private System.Windows.Forms.PictureBox pic_0;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label lbl_or;
        private System.Windows.Forms.Label lbl_crop;
        private System.Windows.Forms.NumericUpDown n_w;
        private System.Windows.Forms.NumericUpDown n_Y;
        private System.Windows.Forms.NumericUpDown n_X;
        private System.Windows.Forms.NumericUpDown n_h;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Label txt_size1;
        private System.Windows.Forms.Label lbl_prop;
    }
}