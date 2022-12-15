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

        private void Form29_Load(object sender, EventArgs e)
        {
            this.Text = Properties.Strings2.pause_bat_ac;
            chk_battery.Checked = Properties.Settings.Default.pause_bat;
            chk_bat_level.Checked = Properties.Settings.Default.if_bat_low;
            n_bat_l.Value = Properties.Settings.Default.bat_level;
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
