namespace FFBatch
{
    partial class Form14
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form14));
            this.btn_abort = new System.Windows.Forms.Button();
            this.txt_path = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_args = new System.Windows.Forms.TextBox();
            this.pic_error = new System.Windows.Forms.PictureBox();
            this.pic_success = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pic_error)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_success)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_abort
            // 
            this.btn_abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_abort.FlatAppearance.BorderSize = 0;
            this.btn_abort.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            resources.ApplyResources(this.btn_abort, "btn_abort");
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_path
            // 
            this.txt_path.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txt_path, "txt_path");
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            // txt_args
            // 
            this.txt_args.BackColor = System.Drawing.SystemColors.Window;
            resources.ApplyResources(this.txt_args, "txt_args");
            this.txt_args.Name = "txt_args";
            this.txt_args.ReadOnly = true;
            // 
            // pic_error
            // 
            resources.ApplyResources(this.pic_error, "pic_error");
            this.pic_error.Name = "pic_error";
            this.pic_error.TabStop = false;
            // 
            // pic_success
            // 
            resources.ApplyResources(this.pic_success, "pic_success");
            this.pic_success.Name = "pic_success";
            this.pic_success.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.Controls.Add(this.btn_abort);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // Form14
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btn_abort;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pic_error);
            this.Controls.Add(this.pic_success);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_args);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form14";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form14_FormClosing);
            this.Load += new System.EventHandler(this.Form14_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_error)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_success)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_args;
        private System.Windows.Forms.PictureBox pic_error;
        private System.Windows.Forms.PictureBox pic_success;
        private System.Windows.Forms.Panel panel1;
        public System.Windows.Forms.Timer timer1;
    }
}