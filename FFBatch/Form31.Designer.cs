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
            this.radio_fdur = new System.Windows.Forms.RadioButton();
            this.radio_fdur_1 = new System.Windows.Forms.RadioButton();
            this.btn_copy = new System.Windows.Forms.Button();
            this.txt_pre = new System.Windows.Forms.TextBox();
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
            // 
            // radio_input_fn_noext
            // 
            resources.ApplyResources(this.radio_input_fn_noext, "radio_input_fn_noext");
            this.radio_input_fn_noext.Name = "radio_input_fn_noext";
            this.radio_input_fn_noext.TabStop = true;
            this.radio_input_fn_noext.UseVisualStyleBackColor = true;
            this.radio_input_fn_noext.CheckedChanged += new System.EventHandler(this.radio_input_fn_noext_CheckedChanged);
            // 
            // radio_fn_path
            // 
            resources.ApplyResources(this.radio_fn_path, "radio_fn_path");
            this.radio_fn_path.Name = "radio_fn_path";
            this.radio_fn_path.TabStop = true;
            this.radio_fn_path.UseVisualStyleBackColor = true;
            this.radio_fn_path.CheckedChanged += new System.EventHandler(this.radio_fn_path_CheckedChanged);
            // 
            // radio_fn
            // 
            resources.ApplyResources(this.radio_fn, "radio_fn");
            this.radio_fn.Name = "radio_fn";
            this.radio_fn.TabStop = true;
            this.radio_fn.UseVisualStyleBackColor = true;
            this.radio_fn.CheckedChanged += new System.EventHandler(this.radio_fn_CheckedChanged);
            // 
            // radio_fn_ext
            // 
            resources.ApplyResources(this.radio_fn_ext, "radio_fn_ext");
            this.radio_fn_ext.Name = "radio_fn_ext";
            this.radio_fn_ext.TabStop = true;
            this.radio_fn_ext.UseVisualStyleBackColor = true;
            this.radio_fn_ext.CheckedChanged += new System.EventHandler(this.radio_fn_ext_CheckedChanged);
            // 
            // radio_fd
            // 
            resources.ApplyResources(this.radio_fd, "radio_fd");
            this.radio_fd.Name = "radio_fd";
            this.radio_fd.TabStop = true;
            this.radio_fd.UseVisualStyleBackColor = true;
            this.radio_fd.CheckedChanged += new System.EventHandler(this.radio_fd_CheckedChanged);
            // 
            // radio_fdur
            // 
            resources.ApplyResources(this.radio_fdur, "radio_fdur");
            this.radio_fdur.Name = "radio_fdur";
            this.radio_fdur.TabStop = true;
            this.radio_fdur.UseVisualStyleBackColor = true;
            this.radio_fdur.CheckedChanged += new System.EventHandler(this.radio_fdur_CheckedChanged);
            // 
            // radio_fdur_1
            // 
            resources.ApplyResources(this.radio_fdur_1, "radio_fdur_1");
            this.radio_fdur_1.Name = "radio_fdur_1";
            this.radio_fdur_1.TabStop = true;
            this.radio_fdur_1.UseVisualStyleBackColor = true;
            this.radio_fdur_1.CheckedChanged += new System.EventHandler(this.radio_fdur_1_CheckedChanged);
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
            // Form31
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CancelButton = this.btn_cancel;
            this.Controls.Add(this.txt_pre);
            this.Controls.Add(this.btn_copy);
            this.Controls.Add(this.radio_fdur_1);
            this.Controls.Add(this.radio_fdur);
            this.Controls.Add(this.radio_fd);
            this.Controls.Add(this.radio_fn_ext);
            this.Controls.Add(this.radio_fn);
            this.Controls.Add(this.radio_fn_path);
            this.Controls.Add(this.radio_input_fn_noext);
            this.Controls.Add(this.radio_inputfn);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form31";
            this.Load += new System.EventHandler(this.Form31_Load);
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
        private System.Windows.Forms.RadioButton radio_fdur;
        private System.Windows.Forms.RadioButton radio_fdur_1;
        private System.Windows.Forms.Button btn_copy;
        private System.Windows.Forms.TextBox txt_pre;
    }
}