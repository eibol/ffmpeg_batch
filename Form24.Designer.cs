
namespace FFBatch
{
    partial class Form24
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form24));
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pic_earth = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_earth)).BeginInit();
            this.SuspendLayout();
            // 
            // combo_lang
            // 
            this.combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            "English",
            "Español",
            "Italiano",
            "Português (BR)",
            "中文"});
            this.combo_lang.Location = new System.Drawing.Point(146, 25);
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.Size = new System.Drawing.Size(96, 21);
            this.combo_lang.TabIndex = 157;
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.combo_lang_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 159;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pic_earth
            // 
            this.pic_earth.Image = ((System.Drawing.Image)(resources.GetObject("pic_earth.Image")));
            this.pic_earth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pic_earth.Location = new System.Drawing.Point(12, 24);
            this.pic_earth.Name = "pic_earth";
            this.pic_earth.Size = new System.Drawing.Size(24, 24);
            this.pic_earth.TabIndex = 160;
            this.pic_earth.TabStop = false;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.label1.Location = new System.Drawing.Point(49, 29);
            this.label1.Name = "label1";
            this.label1.ReadOnly = true;
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 161;
            this.label1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form24
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 67);
            this.Controls.Add(this.pic_earth);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.combo_lang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form24";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Application language";
            this.Load += new System.EventHandler(this.Form24_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_earth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox combo_lang;
        private System.Windows.Forms.PictureBox pic_earth;
        private System.Windows.Forms.TextBox label1;
    }
}