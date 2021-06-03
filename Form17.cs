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
    public partial class Form17 : Form
    {
        public Boolean canceled = true;
        public Boolean to_remove = false;        

        public Form17()
        {            
            InitializeComponent();            
    }

        private void Form17_Load(object sender, EventArgs e)
        {
            if (cb_col.Items.Count > 0)
            {
                cb_col.SelectedIndex = 0;
                btn_add_col.Enabled = true;
            }
            else btn_add_col.Enabled = false;

            refresh_lang();

            if (FFBatch.Properties.Settings.Default.app_lang == "en")
            {
                this.Text = "Add custom column";
            }
            if (FFBatch.Properties.Settings.Default.app_lang == "es")
            {
                this.Text = "Agregar columna";
            }
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form17));
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

        private void btn_exit_Click(object sender, EventArgs e)
        {
            canceled = true;
            to_remove = false;
            this.Close();
        }

        private void btn_add_col_Click(object sender, EventArgs e)
        {            
            canceled = false;
            to_remove = false;
            this.Close();
        }

        private void cb_col_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_col.SelectedItem.ToString().Contains(FFBatch.Properties.Strings.Audio_codec))
            {
                label3.Text = FFBatch.Properties.Strings.first_audio;
            }
            if (cb_col.SelectedItem.ToString().Contains(FFBatch.Properties.Strings.Video_codec))
            {
                label3.Text = FFBatch.Properties.Strings.first_video;
            }
            if (cb_col.SelectedItem.ToString().Contains(FFBatch.Properties.Strings.resolution))
            {
                label3.Text = FFBatch.Properties.Strings.width_heigh;
            }
        }

        private void btn_del_col_Click(object sender, EventArgs e)
        {
            canceled = false;
            to_remove = true;
            this.Close();
        }
    }
}
