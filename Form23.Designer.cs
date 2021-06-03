
namespace FFBatch
{
    partial class Form23
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form23));
            this.txt_channel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_parameters = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_path_main = new System.Windows.Forms.TextBox();
            this.btn_browse_path_m3u = new System.Windows.Forms.Button();
            this.timer_tasks = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btn_abort_all = new System.Windows.Forms.Button();
            this.lbl_d_v = new System.Windows.Forms.Label();
            this.fd1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txt_get_url = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_clear_list = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbl_down_time = new System.Windows.Forms.Label();
            this.pg2 = new FFBatch.ProgressBarWithText();
            this.Pg1 = new FFBatch.ProgressBarWithText();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_channel
            // 
            this.txt_channel.Location = new System.Drawing.Point(125, 18);
            this.txt_channel.Name = "txt_channel";
            this.txt_channel.Size = new System.Drawing.Size(336, 20);
            this.txt_channel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Playlist/channel URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Additional parameters";
            // 
            // txt_parameters
            // 
            this.txt_parameters.Location = new System.Drawing.Point(125, 46);
            this.txt_parameters.Name = "txt_parameters";
            this.txt_parameters.Size = new System.Drawing.Size(336, 20);
            this.txt_parameters.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(203, 105);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(64, 83);
            this.button1.TabIndex = 148;
            this.button1.Text = "Start download";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 152;
            this.label4.Text = "File destination path";
            // 
            // txt_path_main
            // 
            this.txt_path_main.BackColor = System.Drawing.Color.White;
            this.txt_path_main.Location = new System.Drawing.Point(125, 74);
            this.txt_path_main.Name = "txt_path_main";
            this.txt_path_main.Size = new System.Drawing.Size(265, 20);
            this.txt_path_main.TabIndex = 151;
            // 
            // btn_browse_path_m3u
            // 
            this.btn_browse_path_m3u.FlatAppearance.BorderSize = 0;
            this.btn_browse_path_m3u.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_browse_path_m3u.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_browse_path_m3u.Location = new System.Drawing.Point(394, 73);
            this.btn_browse_path_m3u.Name = "btn_browse_path_m3u";
            this.btn_browse_path_m3u.Size = new System.Drawing.Size(67, 22);
            this.btn_browse_path_m3u.TabIndex = 153;
            this.btn_browse_path_m3u.Text = "Browse...";
            this.btn_browse_path_m3u.UseVisualStyleBackColor = true;
            this.btn_browse_path_m3u.Click += new System.EventHandler(this.btn_browse_path_m3u_Click);
            // 
            // timer_tasks
            // 
            this.timer_tasks.Interval = 1000;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "FFmpeg Batch AV Converter";
            this.notifyIcon1.Visible = true;
            // 
            // btn_abort_all
            // 
            this.btn_abort_all.FlatAppearance.BorderSize = 0;
            this.btn_abort_all.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort_all.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_abort_all.Image = ((System.Drawing.Image)(resources.GetObject("btn_abort_all.Image")));
            this.btn_abort_all.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_abort_all.Location = new System.Drawing.Point(269, 102);
            this.btn_abort_all.Name = "btn_abort_all";
            this.btn_abort_all.Size = new System.Drawing.Size(61, 88);
            this.btn_abort_all.TabIndex = 154;
            this.btn_abort_all.Text = "Abort download";
            this.btn_abort_all.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_abort_all.UseVisualStyleBackColor = true;
            this.btn_abort_all.Click += new System.EventHandler(this.btn_abort_all_Click);
            // 
            // lbl_d_v
            // 
            this.lbl_d_v.Location = new System.Drawing.Point(7, 14);
            this.lbl_d_v.Name = "lbl_d_v";
            this.lbl_d_v.Size = new System.Drawing.Size(346, 18);
            this.lbl_d_v.TabIndex = 156;
            this.lbl_d_v.Text = "Downloading file:";
            this.lbl_d_v.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_d_v.TextChanged += new System.EventHandler(this.lbl_d_v_TextChanged);
            // 
            // txt_get_url
            // 
            this.txt_get_url.BackColor = System.Drawing.Color.White;
            this.txt_get_url.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_get_url.Location = new System.Drawing.Point(78, 40);
            this.txt_get_url.Multiline = true;
            this.txt_get_url.Name = "txt_get_url";
            this.txt_get_url.ReadOnly = true;
            this.txt_get_url.Size = new System.Drawing.Size(369, 47);
            this.txt_get_url.TabIndex = 159;
            this.txt_get_url.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(308, 13);
            this.label3.TabIndex = 160;
            this.label3.Text = "HOW TO OBTAIN A YOUTUBE CHANNEL DOWNLOAD LINK";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_get_url);
            this.groupBox1.Location = new System.Drawing.Point(12, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 93);
            this.groupBox1.TabIndex = 161;
            this.groupBox1.TabStop = false;
            // 
            // btn_clear_list
            // 
            this.btn_clear_list.FlatAppearance.BorderSize = 0;
            this.btn_clear_list.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_clear_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear_list.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear_list.Image")));
            this.btn_clear_list.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_clear_list.Location = new System.Drawing.Point(140, 103);
            this.btn_clear_list.Name = "btn_clear_list";
            this.btn_clear_list.Size = new System.Drawing.Size(54, 86);
            this.btn_clear_list.TabIndex = 162;
            this.btn_clear_list.Text = "Clear channel";
            this.btn_clear_list.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_clear_list.UseVisualStyleBackColor = true;
            this.btn_clear_list.Click += new System.EventHandler(this.btn_clear_list_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btn_clear_list);
            this.groupBox2.Controls.Add(this.txt_channel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_parameters);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.txt_path_main);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btn_abort_all);
            this.groupBox2.Controls.Add(this.btn_browse_path_m3u);
            this.groupBox2.Location = new System.Drawing.Point(12, 105);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 195);
            this.groupBox2.TabIndex = 164;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.lbl_d_v);
            this.groupBox3.Controls.Add(this.lbl_down_time);
            this.groupBox3.Controls.Add(this.pg2);
            this.groupBox3.Controls.Add(this.Pg1);
            this.groupBox3.Location = new System.Drawing.Point(12, 301);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(470, 97);
            this.groupBox3.TabIndex = 165;
            this.groupBox3.TabStop = false;
            // 
            // lbl_down_time
            // 
            this.lbl_down_time.Location = new System.Drawing.Point(354, 14);
            this.lbl_down_time.Name = "lbl_down_time";
            this.lbl_down_time.Size = new System.Drawing.Size(104, 16);
            this.lbl_down_time.TabIndex = 164;
            this.lbl_down_time.Text = "Speed/Est. time";
            this.lbl_down_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pg2
            // 
            this.pg2.Location = new System.Drawing.Point(9, 36);
            this.pg2.Name = "pg2";
            this.pg2.Size = new System.Drawing.Size(452, 23);
            this.pg2.TabIndex = 163;
            // 
            // Pg1
            // 
            this.Pg1.Location = new System.Drawing.Point(9, 65);
            this.Pg1.Name = "Pg1";
            this.Pg1.Size = new System.Drawing.Size(452, 23);
            this.Pg1.TabIndex = 161;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form23
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(494, 408);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form23";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick YouTube channel/playlist download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form23_FormClosing);
            this.Load += new System.EventHandler(this.Form23_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_browse_path_m3u;
        private System.Windows.Forms.Timer timer_tasks;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btn_abort_all;
        private System.Windows.Forms.Label lbl_d_v;
        private System.Windows.Forms.FolderBrowserDialog fd1;
        public System.Windows.Forms.TextBox txt_path_main;
        private System.Windows.Forms.TextBox txt_get_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_clear_list;
        public System.Windows.Forms.TextBox txt_parameters;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private ProgressBarWithText pg2;
        private ProgressBarWithText Pg1;
        public System.Windows.Forms.TextBox txt_channel;
        private System.Windows.Forms.Label lbl_down_time;
        private System.Windows.Forms.Timer timer1;
    }
}