using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es") combo_lang.SelectedIndex = 1;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "it") combo_lang.SelectedIndex = 2;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "pt") combo_lang.SelectedIndex = 3;
            else combo_lang.SelectedIndex = 0;
            label1.TextAlign = HorizontalAlignment.Right;
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
                this.Text = "Application language";
                button1.Text = "OK";
            }
            if (combo_lang.SelectedIndex == 1)
            {
                label1.Text = "Elija idioma";
                this.Text = "Idioma de la aplicación";
                button1.Text = "Aceptar";
            }
            if (combo_lang.SelectedIndex == 2)
            {
                label1.Text = "Seleziona la lingua";
                this.Text = "Seleziona la lingua dell'applicazione";
                button1.Text = "OK";
            }
            if (combo_lang.SelectedIndex == 3)
            {
                label1.Text = "Selecione o língua";
                this.Text = "Selecione o língua do aplicativo";
                button1.Text = "OK";
            }
        }        
    }
}
