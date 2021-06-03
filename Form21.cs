using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form21 : Form
    {
        public Form21()
        {
            InitializeComponent();            
        }
        public Boolean cancel = true;
        public Boolean update = false;
        public Boolean skip_ver = false;

        private void btn_down_Click(object sender, EventArgs e)
        {
            cancel = false;
            update = true;
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel = true;
            update = false;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = false;
            update = false;
            skip_ver = true;
            this.Close();
        }

        private void Form21_Load(object sender, EventArgs e)
        {
            refresh_lang();

            if (FFBatch.Properties.Settings.Default.app_lang == "en")
            {
                this.Text = "Software update";
            }
            if (FFBatch.Properties.Settings.Default.app_lang == "es")
            {
                this.Text = "Actualización del programa";
            }
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form21));
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
    }
}
