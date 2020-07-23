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
    public partial class Form12 : Form
    {
        //public Dictionary<string, Process> procs_abort = new Dictionary<string, Process>();        

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            btn_abort.Enabled = true;            
            pic.Enabled = true;
        }

        private void btn_abort_Click(object sender, EventArgs e)
        {
            btn_abort.Enabled = false;
            pic.Enabled = false;            
            Form1 frm1 = new Form1();
            foreach (Process proc in  frm1.procs.Values)
            {
                try
                {
                    proc.Kill();
                }
                catch { }
            }
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            Process[] localByName = Process.GetProcessesByName("ffmpeg");
            if (localByName.Count() > 0) foreach (Process p in localByName) p.Kill();
        }
    }
}
