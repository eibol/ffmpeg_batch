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

        private void Form17_Load(object sender, EventArgs e)
        {
            if (cb_col.Items.Count > 0)
            {
                cb_col.SelectedIndex = 0;
                btn_add_col.Enabled = true;
            }
            else btn_add_col.Enabled = false;

            refresh_lang();
            this.Text = FFBatch.Properties.Strings.add_col_l;
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