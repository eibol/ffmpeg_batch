
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
            resources.ApplyResources(this.txt_channel, "txt_channel");
            this.txt_channel.Name = "txt_channel";
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
            this.label2.Tag = "";
            // 
            // txt_parameters
            // 
            resources.ApplyResources(this.txt_parameters, "txt_parameters");
            this.txt_parameters.Name = "txt_parameters";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txt_path_main
            // 
            resources.ApplyResources(this.txt_path_main, "txt_path_main");
            this.txt_path_main.BackColor = System.Drawing.Color.White;
            this.txt_path_main.Name = "txt_path_main";
            // 
            // btn_browse_path_m3u
            // 
            resources.ApplyResources(this.btn_browse_path_m3u, "btn_browse_path_m3u");
            this.btn_browse_path_m3u.FlatAppearance.BorderSize = 0;
            this.btn_browse_path_m3u.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_browse_path_m3u.Name = "btn_browse_path_m3u";
            this.btn_browse_path_m3u.UseVisualStyleBackColor = true;
            this.btn_browse_path_m3u.Click += new System.EventHandler(this.btn_browse_path_m3u_Click);
            // 
            // timer_tasks
            // 
            this.timer_tasks.Interval = 1000;
            // 
            // notifyIcon1
            // 
            resources.ApplyResources(this.notifyIcon1, "notifyIcon1");
            // 
            // btn_abort_all
            // 
            resources.ApplyResources(this.btn_abort_all, "btn_abort_all");
            this.btn_abort_all.FlatAppearance.BorderSize = 0;
            this.btn_abort_all.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_abort_all.Name = "btn_abort_all";
            this.btn_abort_all.UseVisualStyleBackColor = true;
            this.btn_abort_all.Click += new System.EventHandler(this.btn_abort_all_Click);
            // 
            // lbl_d_v
            // 
            resources.ApplyResources(this.lbl_d_v, "lbl_d_v");
            this.lbl_d_v.Name = "lbl_d_v";
            this.lbl_d_v.TextChanged += new System.EventHandler(this.lbl_d_v_TextChanged);
            // 
            // fd1
            // 
            resources.ApplyResources(this.fd1, "fd1");
            // 
            // txt_get_url
            // 
            resources.ApplyResources(this.txt_get_url, "txt_get_url");
            this.txt_get_url.BackColor = System.Drawing.Color.White;
            this.txt_get_url.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_get_url.Name = "txt_get_url";
            this.txt_get_url.ReadOnly = true;
            this.txt_get_url.TabStop = false;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_get_url);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btn_clear_list
            // 
            resources.ApplyResources(this.btn_clear_list, "btn_clear_list");
            this.btn_clear_list.FlatAppearance.BorderSize = 0;
            this.btn_clear_list.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_clear_list.Name = "btn_clear_list";
            this.btn_clear_list.Tag = "";
            this.btn_clear_list.UseVisualStyleBackColor = true;
            this.btn_clear_list.Click += new System.EventHandler(this.btn_clear_list_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
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
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.lbl_d_v);
            this.groupBox3.Controls.Add(this.lbl_down_time);
            this.groupBox3.Controls.Add(this.pg2);
            this.groupBox3.Controls.Add(this.Pg1);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // lbl_down_time
            // 
            resources.ApplyResources(this.lbl_down_time, "lbl_down_time");
            this.lbl_down_time.Name = "lbl_down_time";
            // 
            // pg2
            // 
            resources.ApplyResources(this.pg2, "pg2");
            this.pg2.Name = "pg2";
            // 
            // Pg1
            // 
            resources.ApplyResources(this.Pg1, "Pg1");
            this.Pg1.Name = "Pg1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form23
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form23";
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