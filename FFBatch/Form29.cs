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
    public partial class Form29 : Form
    {
        public Form29()
        {
            InitializeComponent();
        }

        public void UpdateColorDark(Control myControl)
        {
            myControl.BackColor = Color.FromArgb(255, 64, 64, 64);
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDark(subC);
            }
        }

        public void UpdateColorDefault(Control myControl)
        {
            myControl.BackColor = SystemColors.InactiveBorder;
            myControl.ForeColor = Control.DefaultForeColor;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDefault(subC);
            }
        }

        private void Form29_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
            }

            this.Text = Properties.Strings.pause_bat_ac;
            chk_battery.Checked = Properties.Settings.Default.pause_bat;
            chk_bat_level.Checked = Properties.Settings.Default.if_bat_low;
            n_bat_l.Value = Properties.Settings.Default.bat_level;

            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
        }

        private void btn_bat_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chk_battery_CheckedChanged(object sender, EventArgs e)
        {
            chk_bat_level.Enabled = chk_battery.Checked;
            n_bat_l.Enabled = chk_battery.Checked;
        }
    }
}
