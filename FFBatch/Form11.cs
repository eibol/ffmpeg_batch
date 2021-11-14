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
    public partial class Form11 : Form
    {
        public Boolean abort_validate = false;

        public Form11()
        {
            InitializeComponent();
        }

        public int procId = 0;
        public String reading = "";

        private void Form11_Load(object sender, EventArgs e)
        {
            this.Top = this.Top - 35;
            abort_validate = false;
            btn_abort.Enabled = true;
            timer1.Stop();
            refresh_lang();
            if (reading.Length > 0) label1.Text = reading;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11));
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

        private void Form11_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderRectangle = this.ClientRectangle;
            ////borderRectangle.Inflate(-1, -1);
            ControlPaint.DrawBorder3D(e.Graphics, borderRectangle,
            Border3DStyle.Raised);
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            abort_validate = true;
            btn_abort.Text = FFBatch.Properties.Strings.aborting;
            btn_abort.Enabled = false;
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            foreach (Process p in localByName)
            {
                if (p.Id == procId)
                {
                    p.Kill();
                }
            }
        }
    }
}