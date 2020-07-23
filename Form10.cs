using System;
using System.Drawing;
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

        private void pic_y_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Lime;
            this.TransparencyKey = Color.Lime;
            pic_y.BackColor = Color.Lime;
            

            Opacity = 0;      //first the opacity is 0

            t1.Interval = 10;  //we'll increase the opacity every 10ms
            t1.Tick += new EventHandler(fadeIn);  //this calls the function that changes opacity 
            t1.Start();
        }               

        void fadeIn(object sender, EventArgs e)
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
    }    
}
