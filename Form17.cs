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
    public partial class Form17 : Form
    {
        public Boolean canceled = true;
        public Boolean to_remove = false;        

        public Form17()
        {            
            InitializeComponent();            
    }

        private void Form17_Load(object sender, EventArgs e)
        {
            if (cb_col.Items.Count > 0)
            {
                cb_col.SelectedIndex = 0;
                btn_add_col.Enabled = true;
            }
            else btn_add_col.Enabled = false;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            canceled = true;
            to_remove = false;
            this.Close();
        }

        private void btn_add_col_Click(object sender, EventArgs e)
        {            
            canceled = false;
            to_remove = false;
            this.Close();
        }

        private void cb_col_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_col.SelectedItem.ToString().Contains("Audio codec"))
            {
                label3.Text = "(First audio stream found)";
            }
            if (cb_col.SelectedItem.ToString().Contains("Video codec"))
            {
                label3.Text = "(First video stream found)";
            }
            if (cb_col.SelectedItem.ToString().Contains("Resolution"))
            {
                label3.Text = "(Width amd height)";
            }
        }

        private void btn_del_col_Click(object sender, EventArgs e)
        {
            canceled = false;
            to_remove = true;
            this.Close();
        }
    }
}
