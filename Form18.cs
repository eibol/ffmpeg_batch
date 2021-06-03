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
    public partial class Form18 : Form
    {
        public Form18()
        {
            InitializeComponent();
        }
        public Boolean canceled = true;

        private void button2_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = false;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openf.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            openf.ShowDialog();
        }

        private void openf_FileOk(object sender, CancelEventArgs e)
        {
            txt_audio_path.Text = openf.FileName;
        }

        private void chk_audio_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_audio.Checked == false)
            {
                txt_audio_path.Enabled = false;
                button3.Enabled = false;
            }
            else
            {
                txt_audio_path.Enabled = true;
                button3.Enabled = true;
            }

        }
    }
}
