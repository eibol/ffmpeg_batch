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
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
        }

        public Boolean canceled = false;
        public Boolean subfs = false;

        private void button2_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = false;
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings.path_empty);
                return;
            }
            if (textBox1.Text.Length < 2)
            {
                MessageBox.Show(FFBatch.Properties.Strings.invalid_path);
                return;
            }
            this.Close();
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            refresh_lang();
            this.Text = FFBatch.Properties.Strings.man_path;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.FileSystem;
            canceled = true;
            if (Directory.Exists(Clipboard.GetText()))
            {
                textBox1.Text = Clipboard.GetText();
                textBox1.Select(0, 0);
            }
            else textBox1.Focus();

            this.Text = FFBatch.Properties.Strings.man_path;

            if (subfs == true) label4.Text = FFBatch.Properties.Strings.subs_en;
            else label4.Text = FFBatch.Properties.Strings.subs_dis;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form19));
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowNewFolderButton = false;
            if (fd.ShowDialog() == DialogResult.OK)
                textBox1.Text = fd.SelectedPath;
        }
    }
}