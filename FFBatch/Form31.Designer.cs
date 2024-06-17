namespace FFBatch
{
    partial class Form31
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form31));
            this.btn_add = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.radio_inputfn = new System.Windows.Forms.RadioButton();
            this.radio_input_fn_noext = new System.Windows.Forms.RadioButton();
            this.radio_fn_path = new System.Windows.Forms.RadioButton();
            this.radio_fn = new System.Windows.Forms.RadioButton();
            this.radio_fn_ext = new System.Windows.Forms.RadioButton();
            this.radio_fd = new System.Windows.Forms.RadioButton();
            this.radio_fdur_1 = new System.Windows.Forms.RadioButton();
            this.btn_copy = new System.Windows.Forms.Button();
            this.txt_pre = new System.Windows.Forms.TextBox();
            this.radio_nul = new System.Windows.Forms.RadioButton();
            this.txt_operator_dur = new System.Windows.Forms.TextBox();
            this.radio_chaps = new System.Windows.Forms.RadioButton();
            this.radio_bitr = new System.Windows.Forms.RadioButton();
            this.txt_operator_bitr = new System.Windows.Forms.TextBox();
            this.radio_target_size = new System.Windows.Forms.RadioButton();
            this.txt_size = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.num_aud_target = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_full_p = new System.Windows.Forms.CheckBox();
            this.chk_full2 = new System.Windows.Forms.CheckBox();
            this.chk_full1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_aud_target)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_add
            // 
            resources.ApplyResources(this.btn_add, "btn_add");
            this.btn_add.Name = "btn_add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // radio_inputfn
            // 
            resources.ApplyResources(this.radio_inputfn, "radio_inputfn");
            this.radio_inputfn.Name = "radio_inputfn";
            this.radio_inputfn.TabStop = true;
            this.radio_inputfn.UseVisualStyleBackColor = true;
            this.radio_inputfn.CheckedChanged += new System.EventHandler(this.radio_inputfn_CheckedChanged);
            this.radio_inputfn.Click += new System.EventHandler(this.radio_inputfn_Click);
            // 
            // radio_input_fn_noext
            // 
            resources.ApplyResources(this.radio_input_fn_noext, "radio_input_fn_noext");
            this.radio_input_fn_noext.Name = "radio_input_fn_noext";
            this.radio_input_fn_noext.TabStop = true;
            this.radio_input_fn_noext.UseVisualStyleBackColor = true;
            this.radio_input_fn_noext.CheckedChanged += new System.EventHandler(this.radio_input_fn_noext_CheckedChanged);
            this.radio_input_fn_noext.Click += new System.EventHandler(this.radio_input_fn_noext_Click);
            // 
            // radio_fn_path
            // 
            resources.ApplyResources(this.radio_fn_path, "radio_fn_path");
            this.radio_fn_path.Name = "radio_fn_path";
            this.radio_fn_path.TabStop = true;
            this.radio_fn_path.UseVisualStyleBackColor = true;
            this.radio_fn_path.CheckedChanged += new System.EventHandler(this.radio_fn_path_CheckedChanged);
            this.radio_fn_path.Click += new System.EventHandler(this.radio_fn_path_Click);
            // 
            // radio_fn
            // 
            resources.ApplyResources(this.radio_fn, "radio_fn");
            this.radio_fn.Name = "radio_fn";
            this.radio_fn.TabStop = true;
            this.radio_fn.UseVisualStyleBackColor = true;
            this.radio_fn.CheckedChanged += new System.EventHandler(this.radio_fn_CheckedChanged);
            this.radio_fn.Click += new System.EventHandler(this.radio_fn_Click);
            // 
            // radio_fn_ext
            // 
            resources.ApplyResources(this.radio_fn_ext, "radio_fn_ext");
            this.radio_fn_ext.Name = "radio_fn_ext";
            this.radio_fn_ext.TabStop = true;
            this.radio_fn_ext.UseVisualStyleBackColor = true;
            this.radio_fn_ext.CheckedChanged += new System.EventHandler(this.radio_fn_ext_CheckedChanged);
            this.radio_fn_ext.Click += new System.EventHandler(this.radio_fn_ext_Click);
            // 
            // radio_fd
            // 
            resources.ApplyResources(this.radio_fd, "radio_fd");
            this.radio_fd.Name = "radio_fd";
            this.radio_fd.TabStop = true;
            this.radio_fd.UseVisualStyleBackColor = true;
            this.radio_fd.CheckedChanged += new System.EventHandler(this.radio_fd_CheckedChanged);
            this.radio_fd.Click += new System.EventHandler(this.radio_fd_Click);
            // 
            // radio_fdur_1
            // 
            resources.ApplyResources(this.radio_fdur_1, "radio_fdur_1");
            this.radio_fdur_1.Name = "radio_fdur_1";
            this.radio_fdur_1.TabStop = true;
            this.radio_fdur_1.UseVisualStyleBackColor = true;
            this.radio_fdur_1.Click += new System.EventHandler(this.radio_fdur_1_Click);
            // 
            // btn_copy
            // 
            resources.ApplyResources(this.btn_copy, "btn_copy");
            this.btn_copy.Name = "btn_copy";
            this.btn_copy.UseVisualStyleBackColor = true;
            this.btn_copy.Click += new System.EventHandler(this.btn_copy_Click);
            // 
            // txt_pre
            // 
            resources.ApplyResources(this.txt_pre, "txt_pre");
            this.txt_pre.Name = "txt_pre";
            this.txt_pre.ReadOnly = true;
            this.txt_pre.TextChanged += new System.EventHandler(this.txt_pre_TextChanged);
            // 
            // radio_nul
            // 
            resources.ApplyResources(this.radio_nul, "radio_nul");
            this.radio_nul.Name = "radio_nul";
            this.radio_nul.TabStop = true;
            this.radio_nul.UseVisualStyleBackColor = true;
            this.radio_nul.Click += new System.EventHandler(this.radio_nul_Click);
            // 
            // txt_operator_dur
            // 
            resources.ApplyResources(this.txt_operator_dur, "txt_operator_dur");
            this.txt_operator_dur.Name = "txt_operator_dur";
            this.txt_operator_dur.TextChanged += new System.EventHandler(this.txt_operator_TextChanged);
            // 
            // radio_chaps
            // 
            resources.ApplyResources(this.radio_chaps, "radio_chaps");
            this.radio_chaps.Name = "radio_chaps";
            this.radio_chaps.TabStop = true;
            this.radio_chaps.UseVisualStyleBackColor = true;
            this.radio_chaps.Click += new System.EventHandler(this.radio_chaps_Click);
            // 
            // radio_bitr
            // 
            resources.ApplyResources(this.radio_bitr, "radio_bitr");
            this.radio_bitr.Name = "radio_bitr";
            this.radio_bitr.TabStop = true;
            this.radio_bitr.UseVisualStyleBackColor = true;
            this.radio_bitr.Click += new System.EventHandler(this.radio_bitr_Click);
            // 
            // txt_operator_bitr
            // 
            resources.ApplyResources(this.txt_operator_bitr, "txt_operator_bitr");
            this.txt_operator_bitr.Name = "txt_operator_bitr";
            this.txt_operator_bitr.TextChanged += new System.EventHandler(this.txt_operator_bitr_TextChanged);
            // 
            // radio_target_size
            // 
            resources.ApplyResources(this.radio_target_size, "radio_target_size");
            this.radio_target_size.Name = "radio_target_size";
            this.radio_target_size.TabStop = true;
            this.radio_target_size.UseVisualStyleBackColor = true;
            this.radio_target_size.CheckedChanged += new System.EventHandler(this.radio_target_size_CheckedChanged);
            this.radio_target_size.Click += new System.EventHandler(this.radio_target_size_Click);
            // 
            // txt_size
            // 
            resources.ApplyResources(this.txt_size, "txt_size");
            this.txt_size.Name = "txt_size";
            this.txt_size.TextChanged += new System.EventHandler(this.txt_size_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // num_aud_target
            // 
            resources.ApplyResources(this.num_aud_target, "num_aud_target");
            this.num_aud_target.Increment = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.num_aud_target.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.num_aud_target.Minimum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.num_aud_target.Name = "num_aud_target";
            this.num_aud_target.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            this.num_aud_target.ValueChanged += new System.EventHandler(this.num_aud_target_ValueChanged);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.chk_full_p);
            this.groupBox1.Controls.Add(this.chk_full2);
            this.groupBox1.Controls.Add(this.chk_full1);
            this.groupBox1.Controls.Add(this.radio_fn);
            this.groupBox1.Controls.Add(this.radio_inputfn);
            this.groupBox1.Controls.Add(this.radio_input_fn_noext);
            this.groupBox1.Controls.Add(this.radio_fn_path);
            this.groupBox1.Controls.Add(this.radio_fn_ext);
            this.groupBox1.Controls.Add(this.radio_fd);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chk_full_p
            // 
            resources.ApplyResources(this.chk_full_p, "chk_full_p");
            this.chk_full_p.Name = "chk_full_p";
            this.chk_full_p.UseVisualStyleBackColor = true;
            // 
            // chk_full2
            // 
            resources.ApplyResources(this.chk_full2, "chk_full2");
            this.chk_full2.Name = "chk_full2";
            this.chk_full2.UseVisualStyleBackColor = true;
            // 
            // chk_full1
            // 
            resources.ApplyResources(this.chk_full1, "chk_full1");
            this.chk_full1.Name = "chk_full1";
            this.chk_full1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.radio_fdur_1);
            this.groupBox2.Controls.Add(this.radio_bitr);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.txt_operator_bitr);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.txt_operator_dur);
            this.groupBox2.Controls.Add(this.num_aud_target);
            this.groupBox2.Controls.Add(this.radio_target_size);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_size);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.radio_chaps);
            this.groupBox3.Controls.Add(this.radio_nul);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // Form31
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_pre);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form31";
            this.Load += new System.EventHandler(this.Form31_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_aud_target)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.RadioButton radio_inputfn;
        private System.Windows.Forms.RadioButton radio_input_fn_noext;
        private System.Windows.Forms.RadioButton radio_fn_path;
        private System.Windows.Forms.RadioButton radio_fn;
        private System.Windows.Forms.RadioButton radio_fn_ext;
        private System.Windows.Forms.RadioButton radio_fd;
        private System.Windows.Forms.RadioButton radio_fdur_1;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.TextBox txt_pre;
        private System.Windows.Forms.RadioButton radio_nul;
        private System.Windows.Forms.TextBox txt_operator_dur;
        public System.Windows.Forms.RadioButton radio_chaps;
        private System.Windows.Forms.RadioButton radio_bitr;
        private System.Windows.Forms.TextBox txt_operator_bitr;
        private System.Windows.Forms.RadioButton radio_target_size;
        private System.Windows.Forms.TextBox txt_size;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.NumericUpDown num_aud_target;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk_full1;
        private System.Windows.Forms.CheckBox chk_full_p;
        private System.Windows.Forms.CheckBox chk_full2;
    }
}