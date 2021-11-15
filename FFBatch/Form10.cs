using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form10 : Form
    {
        public Form10()
        {
            InitializeComponent();
        }

        private Boolean copy_img = false;
        private Boolean save_ok = false;

        private void Form10_Load(object sender, EventArgs e)
        {
            ct_copy.Text = Properties.Strings.copy;
            ct_Save.Text = Properties.Strings2.save_img;
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;
            pic_y.BackColor = Color.Lime;

            Opacity = 0;      //first the opacity is 0

            t1.Interval = 10;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity
            t1.Start();
        }

        private void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                t1.Stop();   //this stops the timer if the form is completely displayed
            else
                Opacity += 0.05;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pic_y_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) this.Close();
        }

        private void save_copy_img()
        {
            if (copy_img == false)
            {
                save_img.Filter = Properties.Strings2.imgs + " PNG|*.png|" + Properties.Strings2.imgs + " JPEG |*.jpg";
                save_img.ShowDialog();
                if (save_ok == false) return;
                save_ok = false;
                try
                {
                    if (Path.GetExtension(save_img.FileName) == ".png") pic_y.Image.Save(save_img.FileName, ImageFormat.Png);
                    else pic_y.Image.Save(save_img.FileName, ImageFormat.Jpeg);
                    MessageBox.Show(Properties.Strings2.saved_img);
                }
                catch
                {
                    MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (copy_img == true)
                {
                    try
                    {
                        String temp = Path.GetTempPath() + "FFBatch_Test" + "\\" + "Temp_img.jpg";
                        pic_y.Image.Save(temp, ImageFormat.Jpeg);
                        Bitmap imageToAdd = new Bitmap(temp);
                        Clipboard.SetImage(imageToAdd);
                        MessageBox.Show(Properties.Strings2.img_copied);
                    }
                    catch { MessageBox.Show(Properties.Strings.err_dest, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            copy_img = false;
            return;
        }

        private void save_img_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            save_ok = true;
        }

        private void ct_copy_Click(object sender, EventArgs e)
        {
            copy_img = true;
            save_copy_img();
        }

        private void ct_Save_Click(object sender, EventArgs e)
        {
            copy_img = false;
            save_copy_img();
        }
    }
}