using System;
using System.Drawing;
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

        private void Form6_Load(object sender, System.EventArgs e)
        {

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
                label1.Visible = true;
                label1.Text = Properties.Strings2.delay_act + ": " + i.ToString();

            }
            //End startup delay
        }

        private void pic1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}