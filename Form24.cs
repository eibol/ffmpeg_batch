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
    public partial class Form24 : Form
    {
        public Form24()
        {
            InitializeComponent();
        }

        private void Form24_Load(object sender, EventArgs e)
        {
            combo_lang.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_lang.SelectedIndex == 0)
            {
                label1.Text = "Select language";
                this.Text = label1.Text;
                button1.Text = "OK";
            }
            if (combo_lang.SelectedIndex == 1)
            {
                label1.Text = "Seleccionar idioma";
                this.Text = label1.Text;
                button1.Text = "Aceptar";
            }
        }
    }
}
