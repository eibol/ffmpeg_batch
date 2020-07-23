using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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

        private void Form13_Load(object sender, EventArgs e)
        {
            
        }

        private void btn_browse_path_m3u_Click(object sender, EventArgs e)
        {
            open_F.Filter = "Executable files |*.exe; *.com; *.bat; *.vbs; *.py; *.ps1|All files(*.*) | *.*";
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
                cancel = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Application path is empty.");                
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            open_f2.Filter = "All files(*.*) | *.*";
            open_f2.ShowDialog();
        }

        private void open_f2_FileOk(object sender, CancelEventArgs e)
        {
            txt_args.Text = open_f2.FileName;            
        }
    }
}
