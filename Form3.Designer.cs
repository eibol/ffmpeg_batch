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
            this.combo_lang = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pic_ff_ok = new System.Windows.Forms.PictureBox();
            this.pic_ver = new System.Windows.Forms.PictureBox();
            this.lbl_ff_latest = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_update = new System.Windows.Forms.Button();
            this.lbl_ff_ver = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.chk_delete_one = new System.Windows.Forms.CheckBox();
            this.chk_delete_def = new System.Windows.Forms.CheckBox();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ff_ok)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ver)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
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
            this.panel2.Name = "panel2";
            this.panel2.TabStop = false;
            // 
            // chk_non0
            // 
            resources.ApplyResources(this.chk_non0, "chk_non0");
            this.chk_non0.Name = "chk_non0";
            this.chk_non0.UseVisualStyleBackColor = true;
            this.chk_non0.CheckedChanged += new System.EventHandler(this.chk_non0_CheckedChanged);
            // 
            // btn_stop_play
            // 
            resources.ApplyResources(this.btn_stop_play, "btn_stop_play");
            this.btn_stop_play.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_stop_play.FlatAppearance.BorderSize = 0;
            this.btn_stop_play.Name = "btn_stop_play";
            this.btn_stop_play.UseVisualStyleBackColor = true;
            this.btn_stop_play.Click += new System.EventHandler(this.btn_stop_play_Click);
            // 
            // btn_browse_play
            // 
            resources.ApplyResources(this.btn_browse_play, "btn_browse_play");
            this.btn_browse_play.FlatAppearance.BorderSize = 0;
            this.btn_browse_play.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_browse_play.Name = "btn_browse_play";
            this.btn_browse_play.UseVisualStyleBackColor = true;
            this.btn_browse_play.Click += new System.EventHandler(this.btn_browse_play_Click);
            // 
            // btn_play_sound
            // 
            resources.ApplyResources(this.btn_play_sound, "btn_play_sound");
            this.btn_play_sound.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_play_sound.FlatAppearance.BorderSize = 0;
            this.btn_play_sound.Name = "btn_play_sound";
            this.btn_play_sound.UseVisualStyleBackColor = true;
            this.btn_play_sound.Click += new System.EventHandler(this.btn_play_sound_Click);
            // 
            // chk_play
            // 
            resources.ApplyResources(this.chk_play, "chk_play");
            this.chk_play.Name = "chk_play";
            this.chk_play.UseVisualStyleBackColor = true;
            this.chk_play.CheckedChanged += new System.EventHandler(this.chk_play_CheckedChanged);
            // 
            // chk_full_info
            // 
            resources.ApplyResources(this.chk_full_info, "chk_full_info");
            this.chk_full_info.Name = "chk_full_info";
            this.chk_full_info.UseVisualStyleBackColor = true;
            this.chk_full_info.CheckedChanged += new System.EventHandler(this.chk_full_info_CheckedChanged);
            // 
            // chk_warn_successful
            // 
            resources.ApplyResources(this.chk_warn_successful, "chk_warn_successful");
            this.chk_warn_successful.Name = "chk_warn_successful";
            this.chk_warn_successful.UseVisualStyleBackColor = true;
            this.chk_warn_successful.CheckedChanged += new System.EventHandler(this.chk_warn_successful_CheckedChanged);
            // 
            // chk_sleep
            // 
            resources.ApplyResources(this.chk_sleep, "chk_sleep");
            this.chk_sleep.Name = "chk_sleep";
            this.chk_sleep.UseVisualStyleBackColor = true;
            this.chk_sleep.CheckedChanged += new System.EventHandler(this.chk_sleep_CheckedChanged);
            // 
            // txt_suffix
            // 
            resources.ApplyResources(this.txt_suffix, "txt_suffix");
            this.txt_suffix.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txt_suffix.Name = "txt_suffix";
            this.txt_suffix.TextChanged += new System.EventHandler(this.txt_suffix_TextChanged);
            // 
            // chk_try
            // 
            resources.ApplyResources(this.chk_try, "chk_try");
            this.chk_try.Name = "chk_try";
            this.chk_try.UseVisualStyleBackColor = true;
            this.chk_try.CheckedChanged += new System.EventHandler(this.chk_try_CheckedChanged);
            // 
            // check_open_output
            // 
            resources.ApplyResources(this.check_open_output, "check_open_output");
            this.check_open_output.Name = "check_open_output";
            this.check_open_output.UseVisualStyleBackColor = true;
            this.check_open_output.CheckedChanged += new System.EventHandler(this.check_open_output_CheckedChanged);
            // 
            // check_recreate
            // 
            resources.ApplyResources(this.check_recreate, "check_recreate");
            this.check_recreate.Name = "check_recreate";
            this.check_recreate.UseVisualStyleBackColor = true;
            this.check_recreate.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // chk_suffix
            // 
            resources.ApplyResources(this.chk_suffix, "chk_suffix");
            this.chk_suffix.Name = "chk_suffix";
            this.chk_suffix.UseVisualStyleBackColor = true;
            this.chk_suffix.CheckedChanged += new System.EventHandler(this.chk_suffix_CheckedChanged);
            // 
            // checkBox1
            // 
            resources.ApplyResources(this.checkBox1, "checkBox1");
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // btn_save
            // 
            resources.ApplyResources(this.btn_save, "btn_save");
            this.btn_save.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.Name = "btn_save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.txt_format);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // txt_format
            // 
            resources.ApplyResources(this.txt_format, "txt_format");
            this.txt_format.Name = "txt_format";
            this.txt_format.TextChanged += new System.EventHandler(this.txt_format_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.combo_lang);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chk_w_position);
            this.groupBox2.Controls.Add(this.chk_remember_tab);
            this.groupBox2.Controls.Add(this.chk_verbose_log);
            this.groupBox2.Controls.Add(this.chk_console_params);
            this.groupBox2.Controls.Add(this.chk_subf);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.chk_sort);
            this.groupBox2.Controls.Add(this.chk_auto_updates);
            this.groupBox2.Controls.Add(this.check_concat);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // combo_lang
            // 
            resources.ApplyResources(this.combo_lang, "combo_lang");
            this.combo_lang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_lang.FormattingEnabled = true;
            this.combo_lang.Items.AddRange(new object[] {
            resources.GetString("combo_lang.Items"),
            resources.GetString("combo_lang.Items1")});
            this.combo_lang.Name = "combo_lang";
            this.combo_lang.SelectedIndexChanged += new System.EventHandler(this.combo_lang_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // chk_w_position
            // 
            resources.ApplyResources(this.chk_w_position, "chk_w_position");
            this.chk_w_position.Name = "chk_w_position";
            this.chk_w_position.UseVisualStyleBackColor = true;
            this.chk_w_position.CheckedChanged += new System.EventHandler(this.chk_w_position_CheckedChanged);
            // 
            // chk_remember_tab
            // 
            resources.ApplyResources(this.chk_remember_tab, "chk_remember_tab");
            this.chk_remember_tab.Checked = true;
            this.chk_remember_tab.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_remember_tab.Name = "chk_remember_tab";
            this.chk_remember_tab.UseVisualStyleBackColor = true;
            this.chk_remember_tab.CheckedChanged += new System.EventHandler(this.chk_remember_tab_CheckedChanged);
            // 
            // chk_verbose_log
            // 
            resources.ApplyResources(this.chk_verbose_log, "chk_verbose_log");
            this.chk_verbose_log.Name = "chk_verbose_log";
            this.chk_verbose_log.UseVisualStyleBackColor = true;
            this.chk_verbose_log.CheckedChanged += new System.EventHandler(this.chk_verbose_log_CheckedChanged);
            // 
            // chk_console_params
            // 
            resources.ApplyResources(this.chk_console_params, "chk_console_params");
            this.chk_console_params.Name = "chk_console_params";
            this.chk_console_params.UseVisualStyleBackColor = true;
            this.chk_console_params.CheckedChanged += new System.EventHandler(this.chk_console_params_CheckedChanged);
            // 
            // chk_subf
            // 
            resources.ApplyResources(this.chk_subf, "chk_subf");
            this.chk_subf.Name = "chk_subf";
            this.chk_subf.UseVisualStyleBackColor = true;
            this.chk_subf.CheckedChanged += new System.EventHandler(this.chk_subf_CheckedChanged);
            // 
            // chk_sort
            // 
            resources.ApplyResources(this.chk_sort, "chk_sort");
            this.chk_sort.Name = "chk_sort";
            this.chk_sort.UseVisualStyleBackColor = true;
            this.chk_sort.CheckedChanged += new System.EventHandler(this.chk_sort_CheckedChanged);
            // 
            // chk_auto_updates
            // 
            resources.ApplyResources(this.chk_auto_updates, "chk_auto_updates");
            this.chk_auto_updates.Checked = true;
            this.chk_auto_updates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_auto_updates.Name = "chk_auto_updates";
            this.chk_auto_updates.UseVisualStyleBackColor = true;
            this.chk_auto_updates.CheckedChanged += new System.EventHandler(this.chk_auto_updates_CheckedChanged);
            // 
            // check_concat
            // 
            resources.ApplyResources(this.check_concat, "check_concat");
            this.check_concat.Name = "check_concat";
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
            resources.ApplyResources(this.btn_defaults, "btn_defaults");
            this.btn_defaults.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_defaults.FlatAppearance.BorderSize = 0;
            this.btn_defaults.Name = "btn_defaults";
            this.btn_defaults.UseVisualStyleBackColor = true;
            this.btn_defaults.Click += new System.EventHandler(this.btn_defaults_Click);
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.chk_cache_dialog);
            this.groupBox3.Controls.Add(this.chk_never_cache);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // chk_cache_dialog
            // 
            resources.ApplyResources(this.chk_cache_dialog, "chk_cache_dialog");
            this.chk_cache_dialog.Name = "chk_cache_dialog";
            this.chk_cache_dialog.UseVisualStyleBackColor = true;
            this.chk_cache_dialog.CheckedChanged += new System.EventHandler(this.chk_cache_dialog_CheckedChanged);
            this.chk_cache_dialog.Click += new System.EventHandler(this.chk_cache_dialog_Click);
            // 
            // chk_never_cache
            // 
            resources.ApplyResources(this.chk_never_cache, "chk_never_cache");
            this.chk_never_cache.Name = "chk_never_cache";
            this.chk_never_cache.UseVisualStyleBackColor = true;
            this.chk_never_cache.CheckedChanged += new System.EventHandler(this.chk_never_cache_CheckedChanged);
            this.chk_never_cache.Click += new System.EventHandler(this.chk_never_cache_Click);
            // 
            // btn_reset
            // 
            resources.ApplyResources(this.btn_reset, "btn_reset");
            this.btn_reset.FlatAppearance.BorderColor = System.Drawing.SystemColors.InactiveBorder;
            this.btn_reset.FlatAppearance.BorderSize = 0;
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_edit_presets_n
            // 
            resources.ApplyResources(this.btn_edit_presets_n, "btn_edit_presets_n");
            this.btn_edit_presets_n.FlatAppearance.BorderSize = 0;
            this.btn_edit_presets_n.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_edit_presets_n.Name = "btn_edit_presets_n";
            this.btn_edit_presets_n.UseVisualStyleBackColor = true;
            this.btn_edit_presets_n.Click += new System.EventHandler(this.btn_edit_presets_n_Click);
            // 
            // browse_sound
            // 
            resources.ApplyResources(this.browse_sound, "browse_sound");
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
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.pic_ff_ok);
            this.groupBox4.Controls.Add(this.pic_ver);
            this.groupBox4.Controls.Add(this.lbl_ff_latest);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.btn_update);
            this.groupBox4.Controls.Add(this.lbl_ff_ver);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // pic_ff_ok
            // 
            resources.ApplyResources(this.pic_ff_ok, "pic_ff_ok");
            this.pic_ff_ok.Name = "pic_ff_ok";
            this.pic_ff_ok.TabStop = false;
            this.pic_ff_ok.Click += new System.EventHandler(this.pic_ff_ok_Click);
            // 
            // pic_ver
            // 
            resources.ApplyResources(this.pic_ver, "pic_ver");
            this.pic_ver.Name = "pic_ver";
            this.pic_ver.TabStop = false;
            this.pic_ver.Click += new System.EventHandler(this.pic_ver_Click);
            // 
            // lbl_ff_latest
            // 
            resources.ApplyResources(this.lbl_ff_latest, "lbl_ff_latest");
            this.lbl_ff_latest.Name = "lbl_ff_latest";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btn_update
            // 
            resources.ApplyResources(this.btn_update, "btn_update");
            this.btn_update.FlatAppearance.BorderSize = 0;
            this.btn_update.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_update.Name = "btn_update";
            this.btn_update.UseVisualStyleBackColor = true;
            this.btn_update.Click += new System.EventHandler(this.btn_update_Click);
            // 
            // lbl_ff_ver
            // 
            resources.ApplyResources(this.lbl_ff_ver, "lbl_ff_ver");
            this.lbl_ff_ver.Name = "lbl_ff_ver";
            // 
            // groupBox5
            // 
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.Controls.Add(this.chk_delete_one);
            this.groupBox5.Controls.Add(this.chk_delete_def);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // chk_delete_one
            // 
            resources.ApplyResources(this.chk_delete_one, "chk_delete_one");
            this.chk_delete_one.Name = "chk_delete_one";
            this.chk_delete_one.UseVisualStyleBackColor = true;
            this.chk_delete_one.CheckedChanged += new System.EventHandler(this.chk_delete_one_CheckedChanged);
            // 
            // chk_delete_def
            // 
            resources.ApplyResources(this.chk_delete_def, "chk_delete_def");
            this.chk_delete_def.Name = "chk_delete_def";
            this.chk_delete_def.UseVisualStyleBackColor = true;
            this.chk_delete_def.CheckedChanged += new System.EventHandler(this.chk_delete_def_CheckedChanged);
            // 
            // Form3
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ff_ok)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ver)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label lbl_ff_ver;
        private System.Windows.Forms.Button btn_update;
        public System.Windows.Forms.Label lbl_ff_latest;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pic_ver;
        private System.Windows.Forms.PictureBox pic_ff_ok;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chk_delete_one;
        private System.Windows.Forms.CheckBox chk_delete_def;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox combo_lang;
    }
}