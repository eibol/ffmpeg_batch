using Microsoft.Win32;
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

        public void UpdateColorDark(Control myControl)
        {
            myControl.BackColor = Color.FromArgb(255, 64, 64, 64);
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDark(subC);
            }
        }
        private void Form21_Load(object sender, EventArgs e)
        { 
            String vers = lbl_ver.Text;
            refresh_lang();
            this.Text = FFBatch.Properties.Strings.soft_update;
            lbl_ver.Text = vers;
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
            btn_trans.Left = label1.Left + label1.Width + 10;
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

        private void Form21_Resize(object sender, EventArgs e)
        {
            //526x323
            if (this.Width < 526) this.Width = 526;
            if (this.Height < 333) this.Height = 333;
        }

        private void btn_trans_Click(object sender, EventArgs e)
        {
            String str = textBox1.Text.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(" ","%20");
            Process pr = new Process();
            pr.StartInfo.FileName = GetStandardBrowserPath();
            pr.StartInfo.Arguments = "https://translate.google.com/?sl=en&tl=" + Properties.Settings.Default.app_lang + "&text=" + str + "&op=translate";                                     
            pr.Start();
            
        }

        private static string GetStandardBrowserPath()
        {
            string browserPath = string.Empty;
            RegistryKey browserKey = null;

            try
            {
                //Read default browser path from Win XP registry key
                browserKey = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //If browser path wasn't found, try Win Vista (and newer) registry key
                if (browserKey == null)
                {
                    browserKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http", false); ;
                }

                //If browser path was found, clean it
                if (browserKey != null)
                {
                    //Remove quotation marks
                    browserPath = (browserKey.GetValue(null) as string).ToLower().Replace("\"", "");

                    //Cut off optional parameters
                    if (!browserPath.EndsWith("exe"))
                    {
                        browserPath = browserPath.Substring(0, browserPath.LastIndexOf(".exe") + 4);
                    }

                    //Close registry key
                    browserKey.Close();
                }
            }
            catch
            {
                //Return empty string, if no path was found
                return string.Empty;
            }
            //Return default browsers path
            return browserPath;
        }
    }
}