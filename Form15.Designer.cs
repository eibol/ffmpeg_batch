namespace FFBatch
{
    partial class Form15
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form15));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dg_pr = new System.Windows.Forms.DataGridView();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_load = new System.Windows.Forms.Button();
            this.open_file = new System.Windows.Forms.OpenFileDialog();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_load_bck = new System.Windows.Forms.Button();
            this.btn_decr_font = new System.Windows.Forms.Button();
            this.btn_inc_font = new System.Windows.Forms.Button();
            this.item_down = new System.Windows.Forms.Button();
            this.item_up = new System.Windows.Forms.Button();
            this.btn_remove_pr = new System.Windows.Forms.Button();
            this.btn_add_pr = new System.Windows.Forms.Button();
            this.txt_config_ver = new System.Windows.Forms.TextBox();
            this.lbl_config = new System.Windows.Forms.Label();
            this.btn_save_backup = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).BeginInit();
            this.SuspendLayout();
            // 
            // dg_pr
            // 
            this.dg_pr.AllowDrop = true;
            this.dg_pr.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.AliceBlue;
            this.dg_pr.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dg_pr.BackgroundColor = System.Drawing.SystemColors.InactiveBorder;
            this.dg_pr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_pr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dg_pr.Location = new System.Drawing.Point(21, 57);
            this.dg_pr.Name = "dg_pr";
            this.dg_pr.Size = new System.Drawing.Size(903, 369);
            this.dg_pr.TabIndex = 0;
            this.dg_pr.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dg_pr_CellValidating);
            this.dg_pr.DragDrop += new System.Windows.Forms.DragEventHandler(this.dg_pr_DragDrop);
            this.dg_pr.DragOver += new System.Windows.Forms.DragEventHandler(this.dg_pr_DragOver);
            this.dg_pr.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dg_pr_MouseDown);
            this.dg_pr.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dg_pr_MouseMove);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_save.Location = new System.Drawing.Point(866, 432);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(61, 83);
            this.btn_save.TabIndex = 130;
            this.btn_save.Text = " Save  and close";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_load
            // 
            this.btn_load.FlatAppearance.BorderSize = 0;
            this.btn_load.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load.Image = ((System.Drawing.Image)(resources.GetObject("btn_load.Image")));
            this.btn_load.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_load.Location = new System.Drawing.Point(367, 432);
            this.btn_load.Name = "btn_load";
            this.btn_load.Size = new System.Drawing.Size(77, 82);
            this.btn_load.TabIndex = 131;
            this.btn_load.Text = "Load presets from file";
            this.btn_load.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_load.UseVisualStyleBackColor = true;
            this.btn_load.Click += new System.EventHandler(this.button4_Click);
            // 
            // open_file
            // 
            this.open_file.Filter = "Text files | *.ini; *.txt| All files (*.*) | *.*";
            this.open_file.FileOk += new System.ComponentModel.CancelEventHandler(this.open_file_FileOk);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cancel.Location = new System.Drawing.Point(809, 432);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(55, 82);
            this.btn_cancel.TabIndex = 132;
            this.btn_cancel.Text = "Cancel edition";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_load_bck
            // 
            this.btn_load_bck.FlatAppearance.BorderSize = 0;
            this.btn_load_bck.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_load_bck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_load_bck.Image = ((System.Drawing.Image)(resources.GetObject("btn_load_bck.Image")));
            this.btn_load_bck.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_load_bck.Location = new System.Drawing.Point(503, 432);
            this.btn_load_bck.Name = "btn_load_bck";
            this.btn_load_bck.Size = new System.Drawing.Size(68, 84);
            this.btn_load_bck.TabIndex = 133;
            this.btn_load_bck.Text = "Reload last backup";
            this.btn_load_bck.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_load_bck.UseVisualStyleBackColor = true;
            this.btn_load_bck.Click += new System.EventHandler(this.btn_load_bck_Click);
            // 
            // btn_decr_font
            // 
            this.btn_decr_font.FlatAppearance.BorderSize = 0;
            this.btn_decr_font.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_decr_font.Image = ((System.Drawing.Image)(resources.GetObject("btn_decr_font.Image")));
            this.btn_decr_font.Location = new System.Drawing.Point(831, 27);
            this.btn_decr_font.Name = "btn_decr_font";
            this.btn_decr_font.Size = new System.Drawing.Size(20, 27);
            this.btn_decr_font.TabIndex = 137;
            this.btn_decr_font.UseVisualStyleBackColor = true;
            this.btn_decr_font.Click += new System.EventHandler(this.btn_decr_font_Click);
            // 
            // btn_inc_font
            // 
            this.btn_inc_font.FlatAppearance.BorderSize = 0;
            this.btn_inc_font.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_inc_font.Image = ((System.Drawing.Image)(resources.GetObject("btn_inc_font.Image")));
            this.btn_inc_font.Location = new System.Drawing.Point(809, 27);
            this.btn_inc_font.Name = "btn_inc_font";
            this.btn_inc_font.Size = new System.Drawing.Size(20, 27);
            this.btn_inc_font.TabIndex = 136;
            this.btn_inc_font.UseVisualStyleBackColor = true;
            this.btn_inc_font.Click += new System.EventHandler(this.btn_inc_font_Click);
            // 
            // item_down
            // 
            this.item_down.FlatAppearance.BorderSize = 0;
            this.item_down.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_down.Image = ((System.Drawing.Image)(resources.GetObject("item_down.Image")));
            this.item_down.Location = new System.Drawing.Point(876, 27);
            this.item_down.Name = "item_down";
            this.item_down.Size = new System.Drawing.Size(21, 27);
            this.item_down.TabIndex = 135;
            this.item_down.UseVisualStyleBackColor = true;
            this.item_down.Click += new System.EventHandler(this.item_down_Click);
            // 
            // item_up
            // 
            this.item_up.FlatAppearance.BorderSize = 0;
            this.item_up.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.item_up.Image = ((System.Drawing.Image)(resources.GetObject("item_up.Image")));
            this.item_up.Location = new System.Drawing.Point(899, 27);
            this.item_up.Name = "item_up";
            this.item_up.Size = new System.Drawing.Size(21, 27);
            this.item_up.TabIndex = 134;
            this.item_up.UseVisualStyleBackColor = true;
            this.item_up.Click += new System.EventHandler(this.item_up_Click);
            // 
            // btn_remove_pr
            // 
            this.btn_remove_pr.FlatAppearance.BorderSize = 0;
            this.btn_remove_pr.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_remove_pr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_remove_pr.Image = ((System.Drawing.Image)(resources.GetObject("btn_remove_pr.Image")));
            this.btn_remove_pr.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_remove_pr.Location = new System.Drawing.Point(83, 430);
            this.btn_remove_pr.Name = "btn_remove_pr";
            this.btn_remove_pr.Size = new System.Drawing.Size(59, 86);
            this.btn_remove_pr.TabIndex = 138;
            this.btn_remove_pr.Text = "Remove preset";
            this.btn_remove_pr.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_remove_pr.UseVisualStyleBackColor = true;
            this.btn_remove_pr.Click += new System.EventHandler(this.btn_remove_pr_Click);
            // 
            // btn_add_pr
            // 
            this.btn_add_pr.FlatAppearance.BorderSize = 0;
            this.btn_add_pr.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_add_pr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_add_pr.Image = ((System.Drawing.Image)(resources.GetObject("btn_add_pr.Image")));
            this.btn_add_pr.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_add_pr.Location = new System.Drawing.Point(21, 432);
            this.btn_add_pr.Name = "btn_add_pr";
            this.btn_add_pr.Size = new System.Drawing.Size(59, 84);
            this.btn_add_pr.TabIndex = 139;
            this.btn_add_pr.Text = "Add preset";
            this.btn_add_pr.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_add_pr.UseVisualStyleBackColor = true;
            this.btn_add_pr.Click += new System.EventHandler(this.btn_add_pr_Click);
            // 
            // txt_config_ver
            // 
            this.txt_config_ver.BackColor = System.Drawing.SystemColors.Window;
            this.txt_config_ver.Location = new System.Drawing.Point(144, 31);
            this.txt_config_ver.MaxLength = 4;
            this.txt_config_ver.Name = "txt_config_ver";
            this.txt_config_ver.Size = new System.Drawing.Size(29, 20);
            this.txt_config_ver.TabIndex = 141;
            this.txt_config_ver.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_config
            // 
            this.lbl_config.AutoSize = true;
            this.lbl_config.Location = new System.Drawing.Point(63, 34);
            this.lbl_config.Name = "lbl_config";
            this.lbl_config.Size = new System.Drawing.Size(79, 13);
            this.lbl_config.TabIndex = 140;
            this.lbl_config.Text = "Presets version";
            this.lbl_config.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_save_backup
            // 
            this.btn_save_backup.FlatAppearance.BorderSize = 0;
            this.btn_save_backup.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_save_backup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save_backup.Image = ((System.Drawing.Image)(resources.GetObject("btn_save_backup.Image")));
            this.btn_save_backup.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_save_backup.Location = new System.Drawing.Point(444, 431);
            this.btn_save_backup.Name = "btn_save_backup";
            this.btn_save_backup.Size = new System.Drawing.Size(55, 84);
            this.btn_save_backup.TabIndex = 142;
            this.btn_save_backup.Text = " Backup  presets";
            this.btn_save_backup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_save_backup.UseVisualStyleBackColor = true;
            this.btn_save_backup.Click += new System.EventHandler(this.btn_save_backup_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Image = ((System.Drawing.Image)(resources.GetObject("btn_clear.Image")));
            this.btn_clear.Location = new System.Drawing.Point(24, 27);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(20, 27);
            this.btn_clear.TabIndex = 143;
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.Width = 258;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "FFmpeg parameters";
            this.Column2.Name = "Column2";
            this.Column2.Width = 549;
            // 
            // Column3
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column3.HeaderText = "Format";
            this.Column3.Name = "Column3";
            this.Column3.Width = 52;
            // 
            // Form15
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(946, 528);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_save_backup);
            this.Controls.Add(this.txt_config_ver);
            this.Controls.Add(this.lbl_config);
            this.Controls.Add(this.btn_add_pr);
            this.Controls.Add(this.btn_remove_pr);
            this.Controls.Add(this.btn_decr_font);
            this.Controls.Add(this.btn_inc_font);
            this.Controls.Add(this.item_down);
            this.Controls.Add(this.item_up);
            this.Controls.Add(this.btn_load_bck);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_load);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dg_pr);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form15";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FFmpeg Batch AV Converter - Presets";
            this.Load += new System.EventHandler(this.Form15_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dg_pr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dg_pr;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_load;
        private System.Windows.Forms.OpenFileDialog open_file;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_load_bck;
        private System.Windows.Forms.Button btn_decr_font;
        private System.Windows.Forms.Button btn_inc_font;
        private System.Windows.Forms.Button item_down;
        private System.Windows.Forms.Button item_up;
        private System.Windows.Forms.Button btn_remove_pr;
        private System.Windows.Forms.Button btn_add_pr;
        private System.Windows.Forms.TextBox txt_config_ver;
        private System.Windows.Forms.Label lbl_config;
        private System.Windows.Forms.Button btn_save_backup;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
    }
}