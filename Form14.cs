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
    public partial class Form14 : Form
    {
        int i = 5;
        public Process proc = new Process();
        public String args = String.Empty;
        public Form14()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i--;
            if (timer1.Interval == 1000) label8.Text = "Launching application in " + i.ToString();
            else label8.Text = "Starting application on encoded file";
            if (i == 0)
            {
                timer1.Stop();
                label8.Text = "Waiting for external application to finish";
                label8.Refresh();
                proc.StartInfo.FileName = txt_path.Text;
                proc.StartInfo.Arguments = args;                

                try
                {
                    Task t = Task.Run(() =>
                    {
                        proc.Start();                        
                        proc.WaitForExit();
                    });
                    t.Wait();
                }
                catch
                {
                    label8.Text = "Application failed.";
                    btn_abort.Text = "Close";
                    return;
                }
                label8.Text = "Application finished successfully.";
                btn_abort.Text = "Close";
                if (proc.ExitCode == 0)
                {
                    pic_error.Visible = false;
                    pic_success.Visible = true;
                }
                else
                {
                    pic_error.Visible = true;
                    pic_success.Visible = false;
                    label8.Text = "Application finished with error code " + proc.ExitCode.ToString();
                }
                if (timer1.Interval == 100) this.Close();
            }
        }

        private void Form14_Load(object sender, EventArgs e)
        {
            btn_abort.Focus();
            timer1.Start();
            if (args != String.Empty) txt_args.Text = args;
            else txt_args.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            txt_args.Text = args;
        }

        private void Form14_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }
    }
}
