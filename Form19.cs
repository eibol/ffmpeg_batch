using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form19 : Form
    {
        public Form19()
        {
            InitializeComponent();
         
        }
        public Boolean canceled = false;

        private void button2_Click(object sender, EventArgs e)
        {
            canceled = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            canceled = false;
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Path field cannot be empty.");
                return;
            }
            if (textBox1.Text.Length < 2)
            {
                MessageBox.Show("Selected path is not valid.");
                return;
            }
            this.Close();
        }

        private void Form19_Load(object sender, EventArgs e)
        {
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.FileSystem;
            canceled = true;
            if (Directory.Exists(Clipboard.GetText()))
            {
                textBox1.Text = Clipboard.GetText();
                textBox1.Select(0,0);
            }
            else textBox1.Focus();
        }
                
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1.PerformClick();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            fd.ShowNewFolderButton = false;
            if (fd.ShowDialog() == DialogResult.OK)
                textBox1.Text = fd.SelectedPath;
        }
    }
}
