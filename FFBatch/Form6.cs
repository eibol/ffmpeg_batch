using FFBatch.Properties;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();

        }
        
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        private Boolean is_portable = false;
        private void Form6_Paint(object sender, PaintEventArgs e)
        {
            Rectangle borderRectangle = this.ClientRectangle;
            //borderRectangle.Inflate(-2, -2);
            ControlPaint.DrawBorder3D(e.Graphics, borderRectangle,
                Border3DStyle.Raised);
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

        private static bool IsNightLightEnabled()
        {
            const string BlueLightReductionStateKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\CloudStore\Store\DefaultAccount\Current\default$windows.data.bluelightreduction.bluelightreductionstate\windows.data.bluelightreduction.bluelightreductionstate";
            using (var key = Registry.CurrentUser.OpenSubKey(BlueLightReductionStateKey))
            {
                var data = key?.GetValue("Data");
                if (data is null)
                    return false;
                var byteData = (byte[])data;
                return byteData.Length > 24 && byteData[23] == 0x10 && byteData[24] == 0x00;
            }
        }
        private void Form6_Load(object sender, System.EventArgs e)
        {
            Settings.Default.dark_sunset = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Settings.Default.dark_sunset.Hour, Settings.Default.dark_sunset.Minute, Settings.Default.dark_sunset.Second);
            Settings.Default.dark_sunrise = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Settings.Default.dark_sunrise.Hour, Settings.Default.dark_sunrise.Minute, Settings.Default.dark_sunrise.Second);
            Settings.Default.Save();

            if (Settings.Default.auto_dark == true) Settings.Default.dark_mode = false;

            if (Settings.Default.auto_dark == true && Settings.Default.dark_os == false)
            {                
                TimeSpan sunrise01 = TimeSpan.Parse( Settings.Default.dark_sunrise.ToShortTimeString());
                TimeSpan sunset01 = TimeSpan.Parse(Settings.Default.dark_sunset.ToShortTimeString());                
                TimeSpan now = DateTime.Now.TimeOfDay;

                if (sunrise01 > now || now > sunset01)
                {
                    foreach (Control c in this.Controls) UpdateColorDark(c);
                    this.BackColor = Color.FromArgb(255, 64, 64, 64);
                }                
            }            

            if (Settings.Default.dark_os == true  && Settings.Default.auto_dark == true)
            {
                if (IsNightLightEnabled() == true)
                {
                    Settings.Default.dark_mode = true;
                    foreach (Control c in this.Controls) UpdateColorDark(c);
                    this.BackColor = Color.FromArgb(255, 64, 64, 64);
                }
                else Settings.Default.dark_mode = false;
            }
            
                if (Settings.Default.dark_mode == true)
                {
                    foreach (Control c in this.Controls) UpdateColorDark(c);
                    this.BackColor = Color.FromArgb(255, 64, 64, 64);
                }
            
            Settings.Default.Save();
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            //Startup delay

            String f_delay = String.Empty;
            if (is_portable == false)
            {
                f_delay = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_delay.ini";
            }
            else
            {
                f_delay = port_path + "ff_delay_portable.ini";
            }

            if (File.Exists(f_delay))
            {
                int i = Convert.ToInt32(File.ReadAllText(f_delay));
                lbl_delay.Visible = true;
                //lbl_delay.Text = Properties.Strings.delay_act + ": " + i.ToString();
                lbl_delay.Text = Strings.ResourceManager.GetString("delay_act", new CultureInfo(Properties.Settings.Default.app_lang)) + ": " + i.ToString();

            }
            if (Properties.Settings.Default.visuals == false)
            {
                lbl_delay.Visible = true;
                lbl_delay.Text = lbl_delay.Text + Strings.ResourceManager.GetString("quick_ena", new CultureInfo(Properties.Settings.Default.app_lang));
            }            
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form6_FormClosed(object sender, FormClosedEventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name == "Form1")
                {
                    foreach (Control ct in Application.OpenForms[i].Controls)
                    {
                        if (ct is PictureBox && ct.Name == "pic_title") ((PictureBox)ct).Image = pic1.Image;
                    }
                    break;
                }
            }
        }        
    }
}