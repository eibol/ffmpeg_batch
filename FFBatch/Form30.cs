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
    public partial class Form30 : Form
    {
        public Form30()
        {
            InitializeComponent();
        }

        public Boolean cancel = true;
        private Point _mouseLoc;
        private void btn_clear_Click(object sender, EventArgs e)
        {
            txt_p.Text = String.Empty;
            txt_f.Text = String.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txt_p.Text.Length < 5)
            {
                MessageBox.Show(Properties.Strings.params_short);
                return;
            }
            
            cancel = false;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel = true;
            this.Close();
        }

        private void Form30_Load(object sender, EventArgs e)
        {
            if (txt_p.Text == "-") txt_p.Text = String.Empty;
        }

        private void Form30_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLoc = e.Location;
        }

        private void Form30_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int dx = e.Location.X - _mouseLoc.X;
                int dy = e.Location.Y - _mouseLoc.Y;
                this.Location = new Point(this.Location.X + dx, this.Location.Y + dy);
            }
        }
    }
}
