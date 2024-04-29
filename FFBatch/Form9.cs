using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form9 : Form
    {
        public Form9()
        {
            InitializeComponent();
        }

        public Boolean abort_validate = false;
        public Boolean prg_marquee = false;

        private void btn_abort_pls_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            lab_err.Visible = false;
            this.InvokeEx(f => TaskbarProgress.SetState(this.Handle, TaskbarProgress.TaskbarStates.NoProgress));
            Thread.Sleep(50);
            abort_validate = true;
            label1.Text = "Aborting...";
            label1.Refresh();
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.Refresh();
            btn_abort_pls.Enabled = false;

            foreach (Process proc in Process.GetProcesses())
            {
                if (proc.ProcessName == "ffprobe" || proc.ProcessName == "yt-dlp")
                    try { proc.Kill(); }
                    catch { }
            }

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process[] localByName = Process.GetProcessesByName("yt-dlp");
            foreach (Process p in localByName)
            {
                try { p.Kill(); }
                catch { }
            }
            this.Close();
        }

        private void Form9_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderRectangle = this.ClientRectangle;
            //borderRectangle.Inflate(-1, -1);
            ControlPaint.DrawBorder3D(e.Graphics, borderRectangle,
            Border3DStyle.Raised);
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            this.TopLevel = true;
            abort_validate = false;
            btn_abort_pls.Enabled = true;
            timer1.Stop();
            lab_count.Refresh();
            refresh_lang();

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form9));
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

        private void Form9_FormClosing(object sender, FormClosingEventArgs e)
        {
            lab_count.Text = "";
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name == "Form1")
                {
                    Application.OpenForms[i].Invoke(new MethodInvoker(delegate
                    {
                        Application.OpenForms[i].Enabled = true;
                        foreach (Control ct in Application.OpenForms[i].Controls)
                        {
                            if (ct.Name == "groupBox_m3u") foreach (Control ct2 in ct.Controls) ct2.Enabled = true;
                            if (ct.Name == "ctm_m3u") ct.Enabled = true;
                        }
                    }));
                    break;
                }
            }
        }
    }
}