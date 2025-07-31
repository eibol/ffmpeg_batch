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
    public partial class Form12 : Form
    {
        //public Dictionary<string, Process> procs_abort = new Dictionary<string, Process>();

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            btn_abort.Enabled = true;
            pic.Enabled = true;
            refresh_lang();
            if (FFBatch.Properties.Settings.Default.app_lang == "zh-Hans") this.Height = this.Height + 20;

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
            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form12));
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

        private void btn_abort_Click(object sender, EventArgs e)
        {
            btn_abort.Enabled = false;
            pic.Enabled = false;
            foreach (Process proc in ProcessExtensions.GetChildProcesses(Process.GetCurrentProcess()))
            {
                try
                {
                    proc.Kill();
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(10);
                        proc.Kill();
                    }
                    catch { }
                }
            }            
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            foreach (Process proc in ProcessExtensions.GetChildProcesses(Process.GetCurrentProcess()))
            {
                try
                {
                    proc.Kill();
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(10);
                        proc.Kill();
                    }
                    catch { MessageBox.Show(Properties.Strings.error, Properties.Strings.error,MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }
    }
}