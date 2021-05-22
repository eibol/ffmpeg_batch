
namespace FFBatch
{
    partial class Form22
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form22));
            this.radio_filter = new System.Windows.Forms.RadioButton();
            this.radio_demuxer = new System.Windows.Forms.RadioButton();
            this.chk_copy = new System.Windows.Forms.CheckBox();
            this.chk_filter = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_params = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chk_batch_concat = new System.Windows.Forms.CheckBox();
            this.txt_intro = new System.Windows.Forms.TextBox();
            this.txt_end = new System.Windows.Forms.TextBox();
            this.txt_batch_concat = new System.Windows.Forms.TextBox();
            this.panel_batch = new System.Windows.Forms.Panel();
            this.btn_end = new System.Windows.Forms.Button();
            this.btn_intro = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_c_format = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel_batch.SuspendLayout();
            this.SuspendLayout();
            // 
            // radio_filter
            // 
            this.radio_filter.AutoSize = true;
            this.radio_filter.Location = new System.Drawing.Point(28, 108);
            this.radio_filter.Name = "radio_filter";
            this.radio_filter.Size = new System.Drawing.Size(166, 17);
            this.radio_filter.TabIndex = 0;
            this.radio_filter.TabStop = true;
            this.radio_filter.Text = "Use concatenation video filter";
            this.radio_filter.UseVisualStyleBackColor = true;
            this.radio_filter.CheckedChanged += new System.EventHandler(this.radio_filter_CheckedChanged);
            // 
            // radio_demuxer
            // 
            this.radio_demuxer.AutoSize = true;
            this.radio_demuxer.Location = new System.Drawing.Point(28, 79);
            this.radio_demuxer.Name = "radio_demuxer";
            this.radio_demuxer.Size = new System.Drawing.Size(158, 17);
            this.radio_demuxer.TabIndex = 1;
            this.radio_demuxer.TabStop = true;
            this.radio_demuxer.Text = "Use concatenation demuxer";
            this.radio_demuxer.UseVisualStyleBackColor = true;
            this.radio_demuxer.CheckedChanged += new System.EventHandler(this.radio_demuxer_CheckedChanged);
            // 
            // chk_copy
            // 
            this.chk_copy.AutoSize = true;
            this.chk_copy.Checked = true;
            this.chk_copy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_copy.Location = new System.Drawing.Point(208, 80);
            this.chk_copy.Name = "chk_copy";
            this.chk_copy.Size = new System.Drawing.Size(85, 17);
            this.chk_copy.TabIndex = 3;
            this.chk_copy.Text = "Stream copy";
            this.chk_copy.UseVisualStyleBackColor = true;
            this.chk_copy.CheckedChanged += new System.EventHandler(this.chk_copy_CheckedChanged);
            // 
            // chk_filter
            // 
            this.chk_filter.AutoSize = true;
            this.chk_filter.Checked = true;
            this.chk_filter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_filter.Location = new System.Drawing.Point(208, 109);
            this.chk_filter.Name = "chk_filter";
            this.chk_filter.Size = new System.Drawing.Size(106, 17);
            this.chk_filter.TabIndex = 4;
            this.chk_filter.Text = "Use filter settings";
            this.chk_filter.UseVisualStyleBackColor = true;
            this.chk_filter.CheckedChanged += new System.EventHandler(this.chk_filter_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(25, 143);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(391, 51);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(274, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(348, 7);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(68, 23);
            this.btn_cancel.TabIndex = 8;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_cancel);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-1, 250);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(446, 47);
            this.panel1.TabIndex = 9;
            // 
            // txt_params
            // 
            this.txt_params.Location = new System.Drawing.Point(25, 46);
            this.txt_params.Name = "txt_params";
            this.txt_params.Size = new System.Drawing.Size(305, 20);
            this.txt_params.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Encoding parameters";
            // 
            // chk_batch_concat
            // 
            this.chk_batch_concat.AutoSize = true;
            this.chk_batch_concat.Location = new System.Drawing.Point(27, 207);
            this.chk_batch_concat.Name = "chk_batch_concat";
            this.chk_batch_concat.Size = new System.Drawing.Size(160, 17);
            this.chk_batch_concat.TabIndex = 12;
            this.chk_batch_concat.Text = "Enable batch concatenation";
            this.chk_batch_concat.UseVisualStyleBackColor = true;
            this.chk_batch_concat.CheckedChanged += new System.EventHandler(this.chk_batch_concat_CheckedChanged);
            // 
            // txt_intro
            // 
            this.txt_intro.Location = new System.Drawing.Point(56, 1);
            this.txt_intro.Name = "txt_intro";
            this.txt_intro.Size = new System.Drawing.Size(267, 20);
            this.txt_intro.TabIndex = 13;
            // 
            // txt_end
            // 
            this.txt_end.Location = new System.Drawing.Point(56, 29);
            this.txt_end.Name = "txt_end";
            this.txt_end.Size = new System.Drawing.Size(266, 20);
            this.txt_end.TabIndex = 14;
            // 
            // txt_batch_concat
            // 
            this.txt_batch_concat.BackColor = System.Drawing.Color.White;
            this.txt_batch_concat.Location = new System.Drawing.Point(1, 64);
            this.txt_batch_concat.Margin = new System.Windows.Forms.Padding(4);
            this.txt_batch_concat.Multiline = true;
            this.txt_batch_concat.Name = "txt_batch_concat";
            this.txt_batch_concat.ReadOnly = true;
            this.txt_batch_concat.Size = new System.Drawing.Size(388, 34);
            this.txt_batch_concat.TabIndex = 16;
            this.txt_batch_concat.Text = "In this mode initial and/or end file will be concatenated to every file on the li" +
    "st, instead of concatenating all files together.";
            // 
            // panel_batch
            // 
            this.panel_batch.Controls.Add(this.btn_end);
            this.panel_batch.Controls.Add(this.btn_intro);
            this.panel_batch.Controls.Add(this.label3);
            this.panel_batch.Controls.Add(this.label2);
            this.panel_batch.Controls.Add(this.txt_intro);
            this.panel_batch.Controls.Add(this.txt_batch_concat);
            this.panel_batch.Controls.Add(this.txt_end);
            this.panel_batch.Location = new System.Drawing.Point(25, 241);
            this.panel_batch.Name = "panel_batch";
            this.panel_batch.Size = new System.Drawing.Size(401, 103);
            this.panel_batch.TabIndex = 17;
            this.panel_batch.Visible = false;
            // 
            // btn_end
            // 
            this.btn_end.Location = new System.Drawing.Point(328, 28);
            this.btn_end.Name = "btn_end";
            this.btn_end.Size = new System.Drawing.Size(63, 23);
            this.btn_end.TabIndex = 21;
            this.btn_end.Text = "Browse";
            this.btn_end.UseVisualStyleBackColor = true;
            this.btn_end.Click += new System.EventHandler(this.btn_end_Click);
            // 
            // btn_intro
            // 
            this.btn_intro.Location = new System.Drawing.Point(328, 0);
            this.btn_intro.Name = "btn_intro";
            this.btn_intro.Size = new System.Drawing.Size(62, 23);
            this.btn_intro.TabIndex = 20;
            this.btn_intro.Text = "Browse";
            this.btn_intro.UseVisualStyleBackColor = true;
            this.btn_intro.Click += new System.EventHandler(this.btn_intro_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "End file";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Initial file";
            // 
            // txt_c_format
            // 
            this.txt_c_format.Location = new System.Drawing.Point(380, 46);
            this.txt_c_format.Name = "txt_c_format";
            this.txt_c_format.Size = new System.Drawing.Size(33, 20);
            this.txt_c_format.TabIndex = 18;
            this.txt_c_format.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Format";
            // 
            // Form22
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(436, 292);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_c_format);
            this.Controls.Add(this.panel_batch);
            this.Controls.Add(this.chk_batch_concat);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_params);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.chk_filter);
            this.Controls.Add(this.chk_copy);
            this.Controls.Add(this.radio_demuxer);
            this.Controls.Add(this.radio_filter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form22";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Join files";
            this.Load += new System.EventHandler(this.Form22_Load);
            this.panel1.ResumeLayout(false);
            this.panel_batch.ResumeLayout(false);
            this.panel_batch.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chk_copy;
        private System.Windows.Forms.CheckBox chk_filter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txt_params;
        public System.Windows.Forms.RadioButton radio_filter;
        public System.Windows.Forms.RadioButton radio_demuxer;
        public System.Windows.Forms.TextBox txt_intro;
        public System.Windows.Forms.TextBox txt_end;
        private System.Windows.Forms.TextBox txt_batch_concat;
        private System.Windows.Forms.Panel panel_batch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_end;
        private System.Windows.Forms.Button btn_intro;
        public System.Windows.Forms.CheckBox chk_batch_concat;
        public System.Windows.Forms.TextBox txt_c_format;
        private System.Windows.Forms.Label label4;
    }
}