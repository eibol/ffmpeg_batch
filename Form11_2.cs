using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
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
            this.Close();           
        }
    }
}
