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
    public partial class Form11_2 : Form
    {
        public Boolean abort_validate = false;

        public Form11_2()
        {
            InitializeComponent();
        }
        public int procId = 0;

        private void Form11_Load(object sender, EventArgs e)
        {
            this.Top = this.Top - 35;
            //this.TransparencyKey = Color.LightBlue;
            //this.BackColor = Color.LightBlue;
            abort_validate = false;
            btn_abort.Enabled = true;
            timer1.Stop();
            pic.Focus();
            refresh_lang();
        }
        
        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form11_2));
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
            Process[] localByName = Process.GetProcessesByName("youtube-dl");
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
            btn_abort.Text = "Aborting";
            btn_abort.Enabled = false;
            this.Cursor = Cursors.Arrow;
            this.Close();           
        }
    }
}
