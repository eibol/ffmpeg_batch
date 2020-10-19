namespace FFBatch
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.panel2 = new System.Windows.Forms.GroupBox();
            this.chk_non0 = new System.Windows.Forms.CheckBox();
            this.btn_stop_play = new System.Windows.Forms.Button();
            this.btn_browse_play = new System.Windows.Forms.Button();
            this.btn_play_sound = new System.Windows.Forms.Button();
            this.chk_play = new System.Windows.Forms.CheckBox();
            this.chk_full_info = new System.Windows.Forms.CheckBox();
            this.chk_warn_successful = new System.Windows.Forms.CheckBox();
            this.chk_sleep = new System.Windows.Forms.CheckBox();
            this.txt_suffix = new System.Windows.Forms.TextBox();
            this.chk_try = new System.Windows.Forms.CheckBox();
            this.check_open_output = new System.Windows.Forms.CheckBox();
            this.check_recreate = new System.Windows.Forms.CheckBox();
            this.chk_suffix = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_format = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_w_position = new System.Windows.Forms.CheckBox();
            this.chk_remember_tab = new System.Windows.Forms.CheckBox();
            this.chk_verbose_log = new System.Windows.Forms.CheckBox();
            this.chk_console_params = new System.Windows.Forms.CheckBox();
            this.chk_subf = new System.Windows.Forms.CheckBox();
            this.chk_sort = new System.Windows.Forms.CheckBox();
            this.chk_auto_updates = new System.Windows.Forms.CheckBox();
            this.check_concat = new System.Windows.Forms.CheckBox();
            this.img_edit = new System.Windows.Forms.ImageList(this.components);
            this.btn_defaults = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_cache_dialog = new System.Windows.Forms.CheckBox();
            this.chk_never_cache = new System.Windows.Forms.CheckBox();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_edit_presets_n = new System.Windows.Forms.Button();
            this.browse_sound = new System.Windows.Forms.OpenFileDialog();
            this.img_play = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chk_non0);
            this.panel2.Controls.Add(this.btn_stop_play);
            this.panel2.Controls.Add(this.btn_browse_play);
            this.panel2.Controls.Add(this.btn_play_sound);
            this.panel2.Controls.Add(this.chk_play);
            this.panel2.Controls.Add(this.chk_full_info);
            this.panel2.Controls.Add(this.chk_warn_successful);
            this.panel2.Controls.Add(this.chk_sleep);
            this.panel2.Controls.Add(this.txt_suffix);
            this.panel2.Controls.Add(this.chk_try);
            this.panel2.Controls.Add(this.check_open_output);
            this.panel2.Controls.Add(this.check_recreate);
            this.panel2.Controls.Add(this.chk_suffix);
            this.panel2.Location = new System.Drawing.Point(349, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(323, 254);
            this.panel2.TabIndex = 127;
            this.panel2.TabStop = false;
            this.panel2.Text = "Runtime settings";
            // 
            // chk_non0
            // 
            this.chk_non0.AutoSize = true;
            this.chk_non0.Location = new System.Drawing.Point(17, 55);
            this.chk_non0.Name = "chk_non0";
            this.chk_non0.Size = new System.Drawing.Size(233, 17);
            this.chk_non0.TabIndex = 140;
            this.chk_non0.Text = "Do not display warning for zero duration files";
            this.chk_non0.UseVisualStyleBackColor = true;
            this.chk_non0.CheckedChanged += new System.EventHandler(this.chk_non0_CheckedChanged);
            // 
            // btn_stop_play
            // 
            this.btn_stop_play.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_stop_play.FlatAppearance.BorderSize = 0;
            this.btn_stop_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop_play.Image = ((System.Drawing.Image)(resources.GetObject("btn_stop_play.Image")));
            this.btn_stop_play.Location = new System.Drawing.Point(267, 228);
            this.btn_stop_play.Name = "btn_stop_play";
            this.btn_stop_play.Size = new System.Drawing.Size(20, 20);
            this.btn_stop_play.TabIndex = 139;
            this.btn_stop_play.UseVisualStyleBackColor = true;
            this.btn_stop_play.Visible = false;
            this.btn_stop_play.Click += new System.EventHandler(this.btn_stop_play_Click);
            // 
            // btn_browse_play
            // 
            this.btn_browse_play.Enabled = false;
            this.btn_browse_play.FlatAppearance.BorderSize = 0;
            this.btn_browse_play.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_browse_play.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_browse_play.Location = new System.Drawing.Point(207, 228);
            this.btn_browse_play.Name = "btn_browse_play";
            this.btn_browse_play.Size = new System.Drawing.Size(60, 22);
            this.btn_browse_play.TabIndex = 134;
            this.btn_browse_play.Text = "Browse";
            this.btn_browse_play.UseVisualStyleBackColor = true;
            this.btn_browse_play.Click += new System.EventHandler(this.btn_browse_play_Click);
            // 
            // btn_play_sound
            // 
            this.btn_play_sound.Enabled = false;
            this.btn_play_sound.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_play_sound.FlatAppearance.BorderSize = 0;
            this.btn_play_sound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_play_sound.Image = ((System.Drawing.Image)(resources.GetObject("btn_play_sound.Image")));
            this.btn_play_sound.Location = new System.Drawing.Point(267, 228);
            this.btn_play_sound.Name = "btn_play_sound";
            this.btn_play_sound.Size = new System.Drawing.Size(20, 20);
            this.btn_play_sound.TabIndex = 138;
            this.btn_play_sound.UseVisualStyleBackColor = true;
            this.btn_play_sound.Click += new System.EventHandler(this.btn_play_sound_Click);
            // 
            // chk_play
            // 
            this.chk_play.AutoSize = true;
            this.chk_play.Location = new System.Drawing.Point(17, 231);
            this.chk_play.Name = "chk_play";
            this.chk_play.Size = new System.Drawing.Size(180, 17);
            this.chk_play.TabIndex = 133;
            this.chk_play.Text = "Play sound on queue completion";
            this.chk_play.UseVisualStyleBackColor = true;
            this.chk_play.CheckedChanged += new System.EventHandler(this.chk_play_CheckedChanged);
            // 
            // chk_full_info
            // 
            this.chk_full_info.AutoSize = true;
            this.chk_full_info.Location = new System.Drawing.Point(17, 207);
            this.chk_full_info.Name = "chk_full_info";
            this.chk_full_info.Size = new System.Drawing.Size(171, 17);
            this.chk_full_info.TabIndex = 132;
            this.chk_full_info.Text = "Show full multimedia info report";
            this.chk_full_info.UseVisualStyleBackColor = true;
            this.chk_full_info.CheckedChanged += new System.EventHandler(this.chk_full_info_CheckedChanged);
            // 
            // chk_warn_successful
            // 
            this.chk_warn_successful.AutoSize = true;
            this.chk_warn_successful.Location = new System.Drawing.Point(17, 30);
            this.chk_warn_successful.Name = "chk_warn_successful";
            this.chk_warn_successful.Size = new System.Drawing.Size(205, 17);
            this.chk_warn_successful.TabIndex = 130;
            this.chk_warn_successful.Text = "Do not warn of already encoded items";
            this.chk_warn_successful.UseVisualStyleBackColor = true;
            this.chk_warn_successful.CheckedChanged += new System.EventHandler(this.chk_warn_successful_CheckedChanged);
            // 
            // chk_sleep
            // 
            this.chk_sleep.AutoSize = true;
            this.chk_sleep.Location = new System.Drawing.Point(17, 183);
            this.chk_sleep.Name = "chk_sleep";
            this.chk_sleep.Size = new System.Drawing.Size(202, 17);
            this.chk_sleep.TabIndex = 129;
            this.chk_sleep.Text = "Prevent computer from going to sleep";
            this.chk_sleep.UseVisualStyleBackColor = true;
            this.chk_sleep.CheckedChanged += new System.EventHandler(this.chk_sleep_CheckedChanged);
            // 
            // txt_suffix
            // 
            this.txt_suffix.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_suffix.Enabled = false;
            this.txt_suffix.Location = new System.Drawing.Point(132, 130);
            this.txt_suffix.MaxLength = 12;
            this.txt_suffix.Name = "txt_suffix";
            this.txt_suffix.Size = new System.Drawing.Size(49, 20);
            this.txt_suffix.TabIndex = 69;
            this.txt_suffix.Text = "_FFB";
            this.txt_suffix.TextChanged += new System.EventHandler(this.txt_suffix_TextChanged);
            // 
            // chk_try
            // 
            this.chk_try.AutoSize = true;
            this.chk_try.Location = new System.Drawing.Point(17, 157);
            this.chk_try.Name = "chk_try";
            this.chk_try.Size = new System.Drawing.Size(207, 17);
            this.chk_try.TabIndex = 125;
            this.chk_try.Text = "Do not try preset before start encoding";
            this.chk_try.UseVisualStyleBackColor = true;
            this.chk_try.CheckedChanged += new System.EventHandler(this.chk_try_CheckedChanged);
            // 
            // check_open_output
            // 
            this.check_open_output.AutoSize = true;
            this.check_open_output.Location = new System.Drawing.Point(17, 80);
            this.check_open_output.Name = "check_open_output";
            this.check_open_output.Size = new System.Drawing.Size(216, 17);
            this.check_open_output.TabIndex = 28;
            this.check_open_output.Text = "Open output folder on queue completion";
            this.check_open_output.UseVisualStyleBackColor = true;
            this.check_open_output.CheckedChanged += new System.EventHandler(this.check_open_output_CheckedChanged);
            // 
            // check_recreate
            // 
            this.check_recreate.AutoSize = true;
            this.check_recreate.Location = new System.Drawing.Point(17, 105);
            this.check_recreate.Name = "check_recreate";
            this.check_recreate.Size = new System.Drawing.Size(227, 17);
            this.check_recreate.TabIndex = 67;
            this.check_recreate.Text = "Recreate source path on destination folder";
            this.check_recreate.UseVisualStyleBackColor = true;
            this.check_recreate.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chk_suffix
            // 
            this.chk_suffix.AutoSize = true;
            this.chk_suffix.Location = new System.Drawing.Point(17, 132);
            this.chk_suffix.Name = "chk_suffix";
            this.chk_suffix.Size = new System.Drawing.Size(115, 17);
            this.chk_suffix.TabIndex = 68;
            this.chk_suffix.Text = "Rename output file";
            this.chk_suffix.UseVisualStyleBackColor = true;
            this.chk_suffix.CheckedChanged += new System.EventHandler(this.chk_suffix_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 55);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 17);
            this.checkBox1.TabIndex = 70;
            this.checkBox1.Text = "Do not save logs";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_save.Location = new System.Drawing.Point(612, 436);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(60, 82);
            this.btn_save.TabIndex = 129;
            this.btn_save.Text = "Save changes";
            this.btn_save.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_format);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 340);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660, 90);
            this.groupBox1.TabIndex = 131;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default preset";
            // 
            // txt_format
            // 
            this.txt_format.Location = new System.Drawing.Point(608, 39);
            this.txt_format.Name = "txt_format";
            this.txt_format.Size = new System.Drawing.Size(41, 20);
            this.txt_format.TabIndex = 25;
            this.txt_format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_format.TextChanged += new System.EventHandler(this.txt_format_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(568, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Format";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Parameters";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(97, 29);
            this.textBox1.MaxLength = 1500;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(465, 39);
            this.textBox1.TabIndex = 22;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Image = ((System.Drawing.Image)(resources.GetObject("btn_cancel.Image")));
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_cancel.Location = new System.Drawing.Point(548, 438);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(61, 80);
            this.btn_cancel.TabIndex = 130;
            this.btn_cancel.Text = "Discard changes";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_w_position);
            this.groupBox2.Controls.Add(this.chk_remember_tab);
            this.groupBox2.Controls.Add(this.chk_verbose_log);
            this.groupBox2.Controls.Add(this.chk_console_params);
            this.groupBox2.Controls.Add(this.chk_subf);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.chk_sort);
            this.groupBox2.Controls.Add(this.chk_auto_updates);
            this.groupBox2.Controls.Add(this.check_concat);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(323, 254);
            this.groupBox2.TabIndex = 128;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General settings";
            // 
            // chk_w_position
            // 
            this.chk_w_position.AutoSize = true;
            this.chk_w_position.Location = new System.Drawing.Point(17, 231);
            this.chk_w_position.Name = "chk_w_position";
            this.chk_w_position.Size = new System.Drawing.Size(156, 17);
            this.chk_w_position.TabIndex = 133;
            this.chk_w_position.Text = "Remember window location";
            this.chk_w_position.UseVisualStyleBackColor = true;
            this.chk_w_position.CheckedChanged += new System.EventHandler(this.chk_w_position_CheckedChanged);
            // 
            // chk_remember_tab
            // 
            this.chk_remember_tab.AutoSize = true;
            this.chk_remember_tab.Checked = true;
            this.chk_remember_tab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_remember_tab.Location = new System.Drawing.Point(17, 183);
            this.chk_remember_tab.Name = "chk_remember_tab";
            this.chk_remember_tab.Size = new System.Drawing.Size(114, 17);
            this.chk_remember_tab.TabIndex = 132;
            this.chk_remember_tab.Text = "Remember last tab";
            this.chk_remember_tab.UseVisualStyleBackColor = true;
            this.chk_remember_tab.CheckedChanged += new System.EventHandler(this.chk_remember_tab_CheckedChanged);
            // 
            // chk_verbose_log
            // 
            this.chk_verbose_log.AutoSize = true;
            this.chk_verbose_log.Location = new System.Drawing.Point(17, 80);
            this.chk_verbose_log.Name = "chk_verbose_log";
            this.chk_verbose_log.Size = new System.Drawing.Size(135, 17);
            this.chk_verbose_log.TabIndex = 131;
            this.chk_verbose_log.Text = "Save more verbose log";
            this.chk_verbose_log.UseVisualStyleBackColor = true;
            this.chk_verbose_log.CheckedChanged += new System.EventHandler(this.chk_verbose_log_CheckedChanged);
            // 
            // chk_console_params
            // 
            this.chk_console_params.AutoSize = true;
            this.chk_console_params.Location = new System.Drawing.Point(17, 157);
            this.chk_console_params.Name = "chk_console_params";
            this.chk_console_params.Size = new System.Drawing.Size(254, 17);
            this.chk_console_params.TabIndex = 130;
            this.chk_console_params.Text = "Do not send filename and parameters to console";
            this.chk_console_params.UseVisualStyleBackColor = true;
            this.chk_console_params.CheckedChanged += new System.EventHandler(this.chk_console_params_CheckedChanged);
            // 
            // chk_subf
            // 
            this.chk_subf.AutoSize = true;
            this.chk_subf.Location = new System.Drawing.Point(17, 30);
            this.chk_subf.Name = "chk_subf";
            this.chk_subf.Size = new System.Drawing.Size(149, 17);
            this.chk_subf.TabIndex = 129;
            this.chk_subf.Text = "Add folder and subfolders ";
            this.chk_subf.UseVisualStyleBackColor = true;
            this.chk_subf.CheckedChanged += new System.EventHandler(this.chk_subf_CheckedChanged);
            // 
            // chk_sort
            // 
            this.chk_sort.AutoSize = true;
            this.chk_sort.Location = new System.Drawing.Point(17, 132);
            this.chk_sort.Name = "chk_sort";
            this.chk_sort.Size = new System.Drawing.Size(155, 17);
            this.chk_sort.TabIndex = 128;
            this.chk_sort.Text = "Sort multi-file list by duration";
            this.chk_sort.UseVisualStyleBackColor = true;
            this.chk_sort.CheckedChanged += new System.EventHandler(this.chk_sort_CheckedChanged);
            // 
            // chk_auto_updates
            // 
            this.chk_auto_updates.AutoSize = true;
            this.chk_auto_updates.Checked = true;
            this.chk_auto_updates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_auto_updates.Location = new System.Drawing.Point(17, 207);
            this.chk_auto_updates.Name = "chk_auto_updates";
            this.chk_auto_updates.Size = new System.Drawing.Size(163, 17);
            this.chk_auto_updates.TabIndex = 127;
            this.chk_auto_updates.Text = "Check for updates on startup";
            this.chk_auto_updates.UseVisualStyleBackColor = true;
            this.chk_auto_updates.CheckedChanged += new System.EventHandler(this.chk_auto_updates_CheckedChanged);
            // 
            // check_concat
            // 
            this.check_concat.AutoSize = true;
            this.check_concat.Location = new System.Drawing.Point(17, 105);
            this.check_concat.Name = "check_concat";
            this.check_concat.Size = new System.Drawing.Size(201, 17);
            this.check_concat.TabIndex = 126;
            this.check_concat.Text = "Use concat video filter for joining files";
            this.check_concat.UseVisualStyleBackColor = true;
            this.check_concat.CheckedChanged += new System.EventHandler(this.check_concat_CheckedChanged);
            this.check_concat.Click += new System.EventHandler(this.check_concat_Click);
            // 
            // img_edit
            // 
            this.img_edit.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_edit.ImageStream")));
            this.img_edit.TransparentColor = System.Drawing.Color.Transparent;
            this.img_edit.Images.SetKeyName(0, "Save-icon_39.png");
            this.img_edit.Images.SetKeyName(1, "backup.png");
            this.img_edit.Images.SetKeyName(2, "exit_32.png");
            // 
            // btn_defaults
            // 
            this.btn_defaults.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_defaults.FlatAppearance.BorderSize = 0;
            this.btn_defaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_defaults.Image = ((System.Drawing.Image)(resources.GetObject("btn_defaults.Image")));
            this.btn_defaults.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_defaults.Location = new System.Drawing.Point(75, 437);
            this.btn_defaults.Name = "btn_defaults";
            this.btn_defaults.Size = new System.Drawing.Size(62, 81);
            this.btn_defaults.TabIndex = 133;
            this.btn_defaults.Text = "Use defaults";
            this.btn_defaults.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_defaults.UseVisualStyleBackColor = true;
            this.btn_defaults.Click += new System.EventHandler(this.btn_defaults_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_cache_dialog);
            this.groupBox3.Controls.Add(this.chk_never_cache);
            this.groupBox3.Location = new System.Drawing.Point(12, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(660, 56);
            this.groupBox3.TabIndex = 132;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Network files caching";
            // 
            // chk_cache_dialog
            // 
            this.chk_cache_dialog.AutoSize = true;
            this.chk_cache_dialog.Location = new System.Drawing.Point(266, 26);
            this.chk_cache_dialog.Name = "chk_cache_dialog";
            this.chk_cache_dialog.Size = new System.Drawing.Size(136, 17);
            this.chk_cache_dialog.TabIndex = 132;
            this.chk_cache_dialog.Text = "Use OS file copy dialog";
            this.chk_cache_dialog.UseVisualStyleBackColor = true;
            this.chk_cache_dialog.CheckedChanged += new System.EventHandler(this.chk_cache_dialog_CheckedChanged);
            this.chk_cache_dialog.Click += new System.EventHandler(this.chk_cache_dialog_Click);
            // 
            // chk_never_cache
            // 
            this.chk_never_cache.AutoSize = true;
            this.chk_never_cache.Location = new System.Drawing.Point(17, 26);
            this.chk_never_cache.Name = "chk_never_cache";
            this.chk_never_cache.Size = new System.Drawing.Size(200, 17);
            this.chk_never_cache.TabIndex = 131;
            this.chk_never_cache.Text = "Do not prompt to cache network files";
            this.chk_never_cache.UseVisualStyleBackColor = true;
            this.chk_never_cache.CheckedChanged += new System.EventHandler(this.chk_never_cache_CheckedChanged);
            this.chk_never_cache.Click += new System.EventHandler(this.chk_never_cache_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_reset.FlatAppearance.BorderSize = 0;
            this.btn_reset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_reset.Image = ((System.Drawing.Image)(resources.GetObject("btn_reset.Image")));
            this.btn_reset.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_reset.Location = new System.Drawing.Point(16, 438);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(58, 81);
            this.btn_reset.TabIndex = 136;
            this.btn_reset.Text = "Reset all settings";
            this.btn_reset.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_edit_presets_n
            // 
            this.btn_edit_presets_n.FlatAppearance.BorderSize = 0;
            this.btn_edit_presets_n.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_edit_presets_n.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_edit_presets_n.Image = ((System.Drawing.Image)(resources.GetObject("btn_edit_presets_n.Image")));
            this.btn_edit_presets_n.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_edit_presets_n.Location = new System.Drawing.Point(308, 439);
            this.btn_edit_presets_n.Name = "btn_edit_presets_n";
            this.btn_edit_presets_n.Size = new System.Drawing.Size(68, 80);
            this.btn_edit_presets_n.TabIndex = 137;
            this.btn_edit_presets_n.Text = "Edit saved presets";
            this.btn_edit_presets_n.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_edit_presets_n.UseVisualStyleBackColor = true;
            this.btn_edit_presets_n.Click += new System.EventHandler(this.btn_edit_presets_n_Click);
            // 
            // browse_sound
            // 
            this.browse_sound.Filter = "Audio wave | *.wav| All files (*.*) | *.*";
            // 
            // img_play
            // 
            this.img_play.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("img_play.ImageStream")));
            this.img_play.TransparentColor = System.Drawing.Color.Transparent;
            this.img_play.Images.SetKeyName(0, "play_green_20.png");
            this.img_play.Images.SetKeyName(1, "Stop_20.png");
            // 
            // timer1
            // 
            this.timer1.Interval = 8000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(682, 533);
            this.ControlBox = false;
            this.Controls.Add(this.btn_edit_presets_n);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_defaults);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox panel2;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_format;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox txt_suffix;
        private System.Windows.Forms.CheckBox check_open_output;
        private System.Windows.Forms.CheckBox check_recreate;
        private System.Windows.Forms.CheckBox chk_suffix;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk_auto_updates;
        private System.Windows.Forms.CheckBox chk_try;
        private System.Windows.Forms.CheckBox check_concat;
        private System.Windows.Forms.CheckBox chk_sort;
        private System.Windows.Forms.ImageList img_edit;
        private System.Windows.Forms.CheckBox chk_sleep;
        private System.Windows.Forms.CheckBox chk_subf;
        private System.Windows.Forms.CheckBox chk_warn_successful;
        private System.Windows.Forms.CheckBox chk_console_params;
        private System.Windows.Forms.Button btn_defaults;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_cache_dialog;
        private System.Windows.Forms.CheckBox chk_never_cache;
        private System.Windows.Forms.CheckBox chk_verbose_log;
        private System.Windows.Forms.CheckBox chk_full_info;
        private System.Windows.Forms.CheckBox chk_remember_tab;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_edit_presets_n;
        private System.Windows.Forms.CheckBox chk_play;
        private System.Windows.Forms.Button btn_browse_play;
        private System.Windows.Forms.Button btn_play_sound;
        private System.Windows.Forms.OpenFileDialog browse_sound;
        private System.Windows.Forms.ImageList img_play;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_stop_play;
        private System.Windows.Forms.CheckBox chk_w_position;
        private System.Windows.Forms.CheckBox chk_non0;
    }
}