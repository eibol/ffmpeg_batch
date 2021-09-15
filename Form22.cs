using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form22 : Form
    {
        public Form22()
        {
            InitializeComponent();
        }

        public String intro_dur = "0";
        public String end_dur = "0";
        Boolean intro = true;
        Boolean is_portable = false;
        String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        public String tx_params = String.Empty;
        public String tx_params_ext = String.Empty;
        public String tx_params_ext_or = String.Empty;
        public Boolean cancel = true;
        OpenFileDialog of1 = new OpenFileDialog();

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form22));
            RefreshResources(this, resources);
        }
        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void Form22_Load(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            //Concat video filter

            refresh_lang();
            this.Text = FFBatch.Properties.Strings2.join_files;

            String f_concat = String.Empty;
            if (is_portable == false)
            {
                f_concat = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_concat.ini";
            }
            else
            {
                f_concat = port_path + "ff_concat_portable.ini";
            }

            if (File.Exists(f_concat))
            {
                radio_filter.Checked = true;
            }
            else
            {
                radio_demuxer.Checked = true;
            }

            if (chk_batch_concat.Checked == true)
            {
                panel_batch.Visible = true;
                this.Height = 435;
                panel1.Top = 356;
            }
            else
            {
                panel_batch.Visible = false;
                this.Height = 326;
                panel1.Top = 250;
            }
        }

        private void radio_demuxer_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_demuxer.Checked == true)
            {
                textBox1.Text = FFBatch.Properties.Strings2.demuxer_help;
                chk_copy.Enabled = true;
                chk_filter.Enabled = false;
                if (chk_copy.Checked == true) txt_params.Text = "-c copy";
                String f_concat = String.Empty;
                if (is_portable == false)
                {
                    f_concat = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_concat.ini";
                }
                else
                {
                    f_concat = port_path + "ff_concat_portable.ini";
                }
                try
                {
                    File.Delete(f_concat);
                }
                catch { }
            }
        }

        private void radio_filter_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_filter.Checked == true)
            {
                textBox1.Text = FFBatch.Properties.Strings2.concat_vid_help;
                chk_copy.Enabled = false;
                chk_filter.Enabled = true;
                if (chk_filter.Checked == true) txt_params.Text = String.Empty;

                String f_concat = String.Empty;
                if (is_portable == false)
                {
                    f_concat = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_concat.ini";
                }
                else
                {
                    f_concat = port_path + "ff_concat_portable.ini";
                }
                try
                {
                    File.WriteAllText(f_concat, "");
                }
                catch { }

                //End concat video filter
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = false;
            if (chk_batch_concat.Checked == true && txt_intro.Text.Length == 0 && txt_end.Text.Length == 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings2.batch_concat_msg,FFBatch.Properties.Strings2.no_concat_ef);
                return;
            }
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void chk_copy_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_copy.Checked == true)
            {
                txt_params.Text = "-c copy";
                txt_c_format.Text = tx_params_ext;
            }
            else
            {
                txt_params.Text = tx_params;
                txt_c_format.Text = tx_params_ext_or;
            }
        }

        private void chk_filter_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_filter.Checked == true) txt_params.Text = String.Empty;
            else txt_params.Text = tx_params;
        }

        private void chk_batch_concat_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_batch_concat.Checked == true)
            {
                panel_batch.Visible = true;
                this.Height = 435;
                panel1.Top = 356;
            }
            else
            {
                panel_batch.Visible = false;
                this.Height = 326;
                panel1.Top = 250;
            }
        }


        private void of1_FileOk(object sender, CancelEventArgs e)
        {
            if (intro == true) txt_intro.Text = of1.FileName;
            else txt_end.Text = of1.FileName;
        }

        private void btn_intro_Click(object sender, EventArgs e)
        {
            intro = true;
            if (of1.ShowDialog() == DialogResult.Cancel) return;
            txt_intro.Text = of1.FileName;
            get_dur();
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            intro = false;
            if (of1.ShowDialog() == DialogResult.Cancel) return;
            txt_end.Text = of1.FileName;
            get_dur();
        }

        private void get_dur()
        {
            String dur = "";
            Process probe = new Process();
            probe.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffprobe.exe");
            probe.StartInfo.RedirectStandardOutput = true;
            probe.StartInfo.UseShellExecute = false;
            probe.StartInfo.CreateNoWindow = true;
            probe.EnableRaisingEvents = true;
            if (intro == true) probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + txt_intro.Text + '\u0022';
            else probe.StartInfo.Arguments = "-v error -show_entries format=duration -sexagesimal -of default=noprint_wrappers=1:nokey=1 " + " -i " + '\u0022' + txt_end.Text + '\u0022';
            probe.Start();

            String duracion = probe.StandardOutput.ReadLine();
            probe.WaitForExit();

            if (duracion != null)
            {
                if (duracion.Length >= 7)
                {
                    dur = duracion.Substring(0, 7);
                    if (duracion.Substring(0, 7) == "0:00:00")
                    {
                        dur = "00:00:00";
                    }
                }
                else
                {
                    dur = "0";
                }
            }
            else
            {
                dur = "0";
            }
            if (intro == true) intro_dur = dur;
            else end_dur = dur;
        }
    }
}
