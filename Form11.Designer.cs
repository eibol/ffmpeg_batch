namespace FFBatch
{
    partial class Form11
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
            this.label1 = new System.Windows.Forms.Label();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btn_abort = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pic_ok = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ok)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Reading playlist contents";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pic
            // 
            this.pic.Image = ((System.Drawing.Image)(resources.GetObject("pic.Image")));
            this.pic.Location = new System.Drawing.Point(95, 30);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(33, 33);
            this.pic.TabIndex = 1;
            this.pic.TabStop = false;
            // 
            // btn_abort
            // 
            this.btn_abort.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_abort.Location = new System.Drawing.Point(80, 78);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(62, 23);
            this.btn_abort.TabIndex = 2;
            this.btn_abort.Text = "Abort";
            this.btn_abort.UseVisualStyleBackColor = false;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 4000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pic_ok
            // 
            this.pic_ok.Image = ((System.Drawing.Image)(resources.GetObject("pic_ok.Image")));
            this.pic_ok.Location = new System.Drawing.Point(99, 30);
            this.pic_ok.Name = "pic_ok";
            this.pic_ok.Size = new System.Drawing.Size(24, 33);
            this.pic_ok.TabIndex = 3;
            this.pic_ok.TabStop = false;
            this.pic_ok.Visible = false;
            // 
            // Form11
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(220, 108);
            this.Controls.Add(this.pic_ok);
            this.Controls.Add(this.btn_abort);
            this.Controls.Add(this.pic);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form11";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form11";
            this.Load += new System.EventHandler(this.Form11_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form11_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ok)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.PictureBox pic_ok;
        public System.Windows.Forms.PictureBox pic;
    }
}