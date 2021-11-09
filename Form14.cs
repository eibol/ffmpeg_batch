using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form14 : Form
    {
        int i = 5;
        public Process proc = new Process();
        public String args = String.Empty;
        public Form14()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if (timer1.Interval == 1000) label8.Text = FFBatch.Properties.Strings.launch  + " " + i.ToString();
            else label8.Text = FFBatch.Properties.Strings.start_app_enc;
            if (i == 0)
            {
                timer1.Stop();
                label8.Text = FFBatch.Properties.Strings.wait_finish;
                label8.Refresh();
                proc.StartInfo.FileName = txt_path.Text;
                proc.StartInfo.Arguments = args;                

                try
                {
                    Task t = Task.Run(() =>
                    {
                        proc.Start();                        
                        proc.WaitForExit();
                    });
                    t.Wait();
                }
                catch
                {
                    label8.Text = FFBatch.Properties.Strings.app_failed;
                    btn_abort.Text = FFBatch.Properties.Strings.close;
                    return;
                }
                label8.Text = FFBatch.Properties.Strings.app_ok;
                btn_abort.Text = FFBatch.Properties.Strings.close;
                if (proc.ExitCode == 0)
                {
                    pic_error.Visible = false;
                    pic_success.Visible = true;
                }
                else
                {
                    pic_error.Visible = true;
                    pic_success.Visible = false;
                    label8.Text = FFBatch.Properties.Strings.app_err_code + " "  + proc.ExitCode.ToString();
                }
                if (timer1.Interval == 100) this.Close();
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            btn_abort.Focus();
            timer1.Start();
            if (args != String.Empty) txt_args.Text = args;
            else txt_args.Enabled = false;
            refresh_lang();
            this.Text = FFBatch.Properties.Strings.run_ext;
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
        }
        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form14));
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            txt_args.Text = args;
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
