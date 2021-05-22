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
    }
}
