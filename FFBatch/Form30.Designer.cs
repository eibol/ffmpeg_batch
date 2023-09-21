
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.txt_f = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.combo_presets_ext = new System.Windows.Forms.ComboBox();
            this.label43 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txt_p
            // 
            resources.ApplyResources(this.txt_p, "txt_p");
            this.txt_p.Name = "txt_p";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // combo_presets_ext
            // 
            this.combo_presets_ext.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_presets_ext.FormattingEnabled = true;
            resources.ApplyResources(this.combo_presets_ext, "combo_presets_ext");
            this.combo_presets_ext.Name = "combo_presets_ext";
            this.combo_presets_ext.SelectedIndexChanged += new System.EventHandler(this.combo_presets_ext_SelectedIndexChanged);
            // 
            // label43
            // 
            resources.ApplyResources(this.label43, "label43");
            this.label43.Name = "label43";
            // 
            // Form30
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ControlBox = false;
            this.Controls.Add(this.combo_presets_ext);
            this.Controls.Add(this.label43);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_f);
            this.Controls.Add(this.btn_clear);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_clear;
        public System.Windows.Forms.TextBox txt_f;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combo_presets_ext;
        private System.Windows.Forms.Label label43;
    }
}