
namespace FFBatch
{
    partial class Form17
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form17));
            this.label1 = new System.Windows.Forms.Label();
            this.cb_col = new System.Windows.Forms.ComboBox();
            this.btn_add_col = new System.Windows.Forms.Button();
            this.btn_exit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_del_col = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select column type";
            // 
            // cb_col
            // 
            this.cb_col.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_col.FormattingEnabled = true;
            this.cb_col.Items.AddRange(new object[] {
            "Resolution",
            "Video codec",
            "Audio codec"});
            this.cb_col.Location = new System.Drawing.Point(132, 26);
            this.cb_col.Name = "cb_col";
            this.cb_col.Size = new System.Drawing.Size(140, 21);
            this.cb_col.TabIndex = 1;
            this.cb_col.SelectedIndexChanged += new System.EventHandler(this.cb_col_SelectedIndexChanged);
            // 
            // btn_add_col
            // 
            this.btn_add_col.FlatAppearance.BorderSize = 0;
            this.btn_add_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_col.Image = ((System.Drawing.Image)(resources.GetObject("btn_add_col.Image")));
            this.btn_add_col.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_add_col.Location = new System.Drawing.Point(237, 89);
            this.btn_add_col.Name = "btn_add_col";
            this.btn_add_col.Size = new System.Drawing.Size(51, 82);
            this.btn_add_col.TabIndex = 151;
            this.btn_add_col.Text = "Add column";
            this.btn_add_col.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_add_col.UseVisualStyleBackColor = true;
            this.btn_add_col.Click += new System.EventHandler(this.btn_add_col_Click);
            // 
            // btn_exit
            // 
            this.btn_exit.FlatAppearance.BorderSize = 0;
            this.btn_exit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_exit.Image = ((System.Drawing.Image)(resources.GetObject("btn_exit.Image")));
            this.btn_exit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_exit.Location = new System.Drawing.Point(117, 94);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(53, 77);
            this.btn_exit.TabIndex = 152;
            this.btn_exit.Text = "Close window";
            this.btn_exit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(140, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 154;
            this.label3.Text = "(First audio stream found)";
            this.label3.Visible = false;
            // 
            // btn_del_col
            // 
            this.btn_del_col.FlatAppearance.BorderSize = 0;
            this.btn_del_col.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_col.Image = ((System.Drawing.Image)(resources.GetObject("btn_del_col.Image")));
            this.btn_del_col.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_del_col.Location = new System.Drawing.Point(165, 87);
            this.btn_del_col.Name = "btn_del_col";
            this.btn_del_col.Size = new System.Drawing.Size(70, 84);
            this.btn_del_col.TabIndex = 155;
            this.btn_del_col.Text = "Remove last column";
            this.btn_del_col.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_del_col.UseVisualStyleBackColor = true;
            this.btn_del_col.Click += new System.EventHandler(this.btn_del_col_Click);
            // 
            // Form17
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 183);
            this.Controls.Add(this.btn_del_col);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.btn_add_col);
            this.Controls.Add(this.cb_col);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form17";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add custom column";
            this.Load += new System.EventHandler(this.Form17_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_add_col;
        private System.Windows.Forms.Button btn_exit;
        public System.Windows.Forms.ComboBox cb_col;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_del_col;
    }
}