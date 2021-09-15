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
            Form1 frm1 = new Form1();
            foreach (Process proc in  frm1.procs.Values)
            {
                try
                {
                    proc.Kill();
                }
                catch { }
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            if (localByName.Count() > 0) foreach (Process p in localByName) p.Kill();
            Process[] localByName2 = Process.GetProcessesByName("youtube-dl");
            if (localByName2.Count() > 0) foreach (Process p in localByName2) p.Kill();
        }
    }
}
