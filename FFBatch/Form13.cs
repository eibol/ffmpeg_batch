using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class Form13 : Form
    {
        public String app_path = "";
        public Boolean cancel = true;

        public Form13()
        {
            InitializeComponent();
        }

        public Boolean run_each = false;

        private void Form13_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (run_each == true)
            {
                this.Text = FFBatch.Properties.Strings.runcomm;
                label3.Text = FFBatch.Properties.Strings.runcom2;
            }
            else
            {
                this.Text = FFBatch.Properties.Strings.open_q_c;
                label3.Text = FFBatch.Properties.Strings.sel_app;
            }
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
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

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form13));
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

        private void btn_browse_path_m3u_Click(object sender, EventArgs e)
        {
            open_F.Filter = FFBatch.Properties.Strings.exe_files + " |*.exe; *.com; *.bat; *.vbs; *.py; *.ps1|" + FFBatch.Properties.Strings.all_files + "(*.*) | *.*";
            open_F.ShowDialog();
        }

        private void open_F_FileOk(object sender, CancelEventArgs e)
        {
            txt_path.Text = open_F.FileName;
            app_path = txt_path.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txt_path.Text != String.Empty)
            {
                if (!File.Exists(txt_path.Text))
                {
                    MessageBox.Show(FFBatch.Properties.Strings.exe_not_f);
                    return;
                }

                cancel = false;
                this.Close();
            }
            else
            {
                MessageBox.Show(FFBatch.Properties.Strings.app_p_empty);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            open_f2.Filter = FFBatch.Properties.Strings.all_files + " (*.*) | *.*";
            open_f2.ShowDialog();
        }

        private void open_f2_FileOk(object sender, CancelEventArgs e)
        {
            txt_args.Text = open_f2.FileName;
        }
    }
}