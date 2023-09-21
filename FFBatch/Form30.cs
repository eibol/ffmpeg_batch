using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form30 : Form
    {
        public Form30()
        {
            InitializeComponent();
        }

        public Boolean cancel = true;
        private Point _mouseLoc;
        public String sel_preset = "";
        public Boolean is_portable = false;
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_p.Text = String.Empty;
            txt_f.Text = String.Empty;
        }

        public void UpdateColorDark(Control myControl)
        {
            myControl.BackColor = Color.FromArgb(255, 64, 64, 64);
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDark(subC);
            }
        }

        public void UpdateColorDefault(Control myControl)
        {
            myControl.BackColor = SystemColors.InactiveBorder;
            myControl.ForeColor = Control.DefaultForeColor;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDefault(subC);
            }

            foreach (Control subC in Controls.OfType<System.Windows.Forms.GroupBox>())
            {
                foreach (Control subC2 in subC.Controls.OfType<System.Windows.Forms.TextBox>())
                {
                    { subC2.BackColor = SystemColors.Window; }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
            }

            if (txt_p.Text.Length < 5)
            {
                MessageBox.Show(Properties.Strings.params_short);
                return;
            }
            
            cancel = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void Form30_Load(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (txt_p.Text == "-") txt_p.Text = String.Empty;

            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                txt_f.BackColor = SystemColors.Window;
                txt_p.BackColor = SystemColors.Window;
            }

            sel_preset = String.Empty;
            combo_presets_ext.Items.Clear();
            combo_presets_ext.Items.Add(Properties.Strings.default_param);
            String path, path_pr = "";

            path_pr = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            if (is_portable == true)
            {
                path_pr = port_path + "ff_presets_portable.ini";
                path = port_path + "ff_batch_portable.ini";
            }
            int linea = 0;
            String ext1 = "";
            String pres1 = "";

            combo_presets_ext.SelectedIndex = combo_presets_ext.FindString(Properties.Strings.default_param);
            foreach (string line in File.ReadLines(path))
            {
                if (linea == 0) pres1 = line;
                if (linea == 1) ext1 = line;
                if (linea > 1) break;
                linea = linea + 1;
            }
            if (txt_p.Text.Length == 0)
            {
                txt_f.Text = ext1;
                txt_p.Text = pres1;
            }

            foreach (string line in File.ReadLines(path_pr))
            {
                if (line.Contains("PR: "))
                {
                    combo_presets_ext.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));
                }
            }
        }

        private void Form30_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        private void Form30_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }

        private void combo_presets_ext_SelectedIndexChanged(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (combo_presets_ext.SelectedIndex == 0)
            {
                String path = "";
                if (is_portable == true)
                {
                    path = port_path + "ff_batch_portable.ini";
                    if (!Directory.Exists(System.IO.Path.Combine(Application.StartupPath, "settings")))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, "settings"));
                    }
                }
                else
                {
                    path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                    if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch")))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
                    }
                }

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "-c copy" + "\n" + "mp4" + "\n" + "yes" + "\n" + "Vs" + "\n" + "grid_yes" + "\n" + "keep_no" + "\n" + "subf_no");
                }

                int linea = 0;
                String ext1 = "";
                String pres1 = "";
                foreach (string line in File.ReadLines(path))
                {
                    if (linea == 0) pres1 = line;
                    if (linea == 1) ext1 = line;
                    if (linea > 1) break;
                    linea = linea + 1;
                }
                txt_f.Text = ext1;
                txt_p.Text = pres1;
            }
            else
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
                if (is_portable == true) path = port_path + "ff_presets_portable.ini";
                String pre_name = String.Empty;
                int i = 0;
                foreach (string line in File.ReadLines(path))
                {
                    if (line != "" && line.Contains("Version ") == false)
                    {
                        if (line.Substring(4, line.IndexOf("&") - 5) == combo_presets_ext.SelectedItem.ToString())
                        {
                            int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");

                            txt_f.Text = line.Substring(line.LastIndexOf("%") + 2);
                            txt_p.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                        }
                    }
                    i = i + 1;
                }
            }
        }
    }
}
