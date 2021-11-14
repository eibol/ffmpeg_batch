namespace FFBatch
{
    partial class Form10
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form10));
            this.pic_y = new System.Windows.Forms.PictureBox();
            this.t1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ct_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ct_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.save_img = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pic_y)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pic_y
            // 
            this.pic_y.ContextMenuStrip = this.menu;
            this.pic_y.Location = new System.Drawing.Point(0, 0);
            this.pic_y.Name = "pic_y";
            this.pic_y.Size = new System.Drawing.Size(800, 480);
            this.pic_y.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_y.TabIndex = 0;
            this.pic_y.TabStop = false;
            this.pic_y.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pic_y_MouseClick);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(383, 448);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 20);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ct_copy,
            this.ct_Save});
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(181, 70);
            // 
            // ct_copy
            // 
            this.ct_copy.Image = ((System.Drawing.Image)(resources.GetObject("ct_copy.Image")));
            this.ct_copy.Name = "ct_copy";
            this.ct_copy.Size = new System.Drawing.Size(180, 22);
            this.ct_copy.Text = "Copy";
            this.ct_copy.Click += new System.EventHandler(this.ct_copy_Click);
            // 
            // ct_Save
            // 
            this.ct_Save.Image = ((System.Drawing.Image)(resources.GetObject("ct_Save.Image")));
            this.ct_Save.Name = "ct_Save";
            this.ct_Save.Size = new System.Drawing.Size(180, 22);
            this.ct_Save.Text = "Save image";
            this.ct_Save.Click += new System.EventHandler(this.ct_Save_Click);
            // 
            // save_img
            // 
            this.save_img.FileOk += new System.ComponentModel.CancelEventHandler(this.save_img_FileOk);
            // 
            // Form10
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(800, 480);
            this.Controls.Add(this.pic_y);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form10";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form10_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_y)).EndInit();
            this.menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pic_y;
        private System.Windows.Forms.Timer t1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ContextMenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem ct_copy;
        private System.Windows.Forms.ToolStripMenuItem ct_Save;
        private System.Windows.Forms.SaveFileDialog save_img;
    }
}