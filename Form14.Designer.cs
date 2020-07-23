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
            this.btn_abort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_abort.Location = new System.Drawing.Point(334, 7);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(69, 25);
            this.btn_abort.TabIndex = 104;
            this.btn_abort.Text = "Abort";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_path
            // 
            this.txt_path.BackColor = System.Drawing.SystemColors.Window;
            this.txt_path.Location = new System.Drawing.Point(79, 17);
            this.txt_path.Name = "txt_path";
            this.txt_path.ReadOnly = true;
            this.txt_path.Size = new System.Drawing.Size(308, 20);
            this.txt_path.TabIndex = 104;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(79, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 16);
            this.label8.TabIndex = 105;
            this.label8.Text = "Launching application in 5";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 108;
            this.label1.Text = "Executable";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 110;
            this.label2.Text = "Arguments";
            // 
            // txt_args
            // 
            this.txt_args.BackColor = System.Drawing.SystemColors.Window;
            this.txt_args.Location = new System.Drawing.Point(79, 53);
            this.txt_args.Name = "txt_args";
            this.txt_args.ReadOnly = true;
            this.txt_args.Size = new System.Drawing.Size(308, 20);
            this.txt_args.TabIndex = 109;
            // 
            // pic_error
            // 
            this.pic_error.Image = ((System.Drawing.Image)(resources.GetObject("pic_error.Image")));
            this.pic_error.Location = new System.Drawing.Point(371, 92);
            this.pic_error.Name = "pic_error";
            this.pic_error.Size = new System.Drawing.Size(14, 14);
            this.pic_error.TabIndex = 150;
            this.pic_error.TabStop = false;
            this.pic_error.Visible = false;
            // 
            // pic_success
            // 
            this.pic_success.Image = ((System.Drawing.Image)(resources.GetObject("pic_success.Image")));
            this.pic_success.Location = new System.Drawing.Point(371, 92);
            this.pic_success.Name = "pic_success";
            this.pic_success.Size = new System.Drawing.Size(16, 16);
            this.pic_success.TabIndex = 149;
            this.pic_success.TabStop = false;
            this.pic_success.Visible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btn_abort);
            this.panel1.Location = new System.Drawing.Point(2, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(418, 41);
            this.panel1.TabIndex = 151;
            // 
            // Form14
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_abort;
            this.ClientSize = new System.Drawing.Size(415, 161);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pic_error);
            this.Controls.Add(this.pic_success);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_args);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_path);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form14";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Run external command";
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
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.TextBox txt_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txt_args;
        private System.Windows.Forms.PictureBox pic_error;
        private System.Windows.Forms.PictureBox pic_success;
        private System.Windows.Forms.Panel panel1;
    }
}