
namespace FFBatch
{
    partial class Form30
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form30));
            this.txt_p = new System.Windows.Forms.TextBox();
            this.btn_ok = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.txt_f = new System.Windows.Forms.TextBox();
            this.lbl_format = new System.Windows.Forms.Label();
            this.combo_presets_ext = new System.Windows.Forms.ComboBox();
            this.lbl_pr = new System.Windows.Forms.Label();
            this.lbl_par = new System.Windows.Forms.Label();
            this.txt_prein = new System.Windows.Forms.TextBox();
            this.lbl_pre = new System.Windows.Forms.Label();
            this.btn_reset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_p
            // 
            resources.ApplyResources(this.txt_p, "txt_p");
            this.txt_p.Name = "txt_p";
            // 
            // btn_ok
            // 
            resources.ApplyResources(this.btn_ok, "btn_ok");
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_cancel
            // 
            resources.ApplyResources(this.btn_cancel, "btn_cancel");
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.btn_clear, "btn_clear");
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // txt_f
            // 
            resources.ApplyResources(this.txt_f, "txt_f");
            this.txt_f.Name = "txt_f";
            // 
            // lbl_format
            // 
            resources.ApplyResources(this.lbl_format, "lbl_format");
            this.lbl_format.Name = "lbl_format";
            // 
            // combo_presets_ext
            // 
            this.combo_presets_ext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_presets_ext.FormattingEnabled = true;
            resources.ApplyResources(this.combo_presets_ext, "combo_presets_ext");
            this.combo_presets_ext.Name = "combo_presets_ext";
            this.combo_presets_ext.SelectedIndexChanged += new System.EventHandler(this.combo_presets_ext_SelectedIndexChanged);
            // 
            // lbl_pr
            // 
            resources.ApplyResources(this.lbl_pr, "lbl_pr");
            this.lbl_pr.Name = "lbl_pr";
            // 
            // lbl_par
            // 
            resources.ApplyResources(this.lbl_par, "lbl_par");
            this.lbl_par.Name = "lbl_par";
            // 
            // txt_prein
            // 
            resources.ApplyResources(this.txt_prein, "txt_prein");
            this.txt_prein.Name = "txt_prein";
            // 
            // lbl_pre
            // 
            resources.ApplyResources(this.lbl_pre, "lbl_pre");
            this.lbl_pre.Name = "lbl_pre";
            // 
            // btn_reset
            // 
            resources.ApplyResources(this.btn_reset, "btn_reset");
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.UseVisualStyleBackColor = true;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // Form30
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ControlBox = false;
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.lbl_pre);
            this.Controls.Add(this.txt_prein);
            this.Controls.Add(this.lbl_par);
            this.Controls.Add(this.combo_presets_ext);
            this.Controls.Add(this.lbl_pr);
            this.Controls.Add(this.lbl_format);
            this.Controls.Add(this.txt_f);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.txt_p);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimizeBox = false;
            this.Name = "Form30";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.Form30_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form30_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form30_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txt_p;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TextBox txt_f;
        private System.Windows.Forms.Label lbl_format;
        private System.Windows.Forms.ComboBox combo_presets_ext;
        private System.Windows.Forms.Label lbl_pr;
        private System.Windows.Forms.Label lbl_par;
        public System.Windows.Forms.TextBox txt_prein;
        private System.Windows.Forms.Label lbl_pre;
        private System.Windows.Forms.Button btn_reset;
    }
}