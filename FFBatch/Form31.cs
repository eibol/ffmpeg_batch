using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FFBatch
{
    public partial class Form31 : Form
    {
        public String variab;
        public String file;
        public String dur;
        public Boolean canceled = false;
        public Form31()
        {
            InitializeComponent();
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }
        private void refresh_lang()
        {            
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form31));
            RefreshResources(this, resources);
        }

        private void Form31_Load(object sender, EventArgs e)
        {
            refresh_lang();
            variab = "";
        }

        private Boolean selected()
        {
            foreach (var pb in this.Controls.OfType<RadioButton>())
            {
                if (pb.Checked == true)
                {
                    return true;
                }
            }
            return false;
        }
        private void set_var()
        {
            if (radio_inputfn.Checked == true) variab = "%1";
            if (radio_input_fn_noext.Checked == true) variab = "%2";
            if (radio_fn_path.Checked == true) variab = "%fp";
            if (radio_fn.Checked == true) variab = "%fn";
            if (radio_fn_ext.Checked == true) variab = "%ff";
            if (radio_fd.Checked == true) variab = "%fd";
            if (radio_fdur.Checked == true) variab = "%fdur";
            if (radio_fdur_1.Checked == true) variab = "%fdur+1";
        } 

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (selected() == false) return;
            canceled = false;
            set_var();
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            canceled = true;
            variab = "";
            this.Close();
        }

        private void btn_copy_Click(object sender, EventArgs e)
        {
            if (selected() == false) return;
            canceled = true;
            set_var();
            Clipboard.SetText(variab);
            this.Close();
        }

        private void radio_inputfn_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            txt_pre.Text = file;
        }

        private void radio_input_fn_noext_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4)
            {
                int ind = file.LastIndexOf(".");
                txt_pre.Text = file.Substring(0, ind);
            }
            else txt_pre.Text = "";
        }

        private void radio_fn_path_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFullPath(file).Substring(0, Path.GetFullPath(file).LastIndexOf("\\"));
            else txt_pre.Text = "";
        }

        private void radio_fn_ext_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFileName(file);
            else txt_pre.Text = "";
        }

        private void radio_fn_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetFileNameWithoutExtension(file);
            else txt_pre.Text = "";
        }

        private void radio_fd_CheckedChanged(object sender, EventArgs e)
        {
            txt_pre.Text = "";
            if (file.Length > 4) txt_pre.Text = Path.GetDirectoryName(file);
            else txt_pre.Text = "";
        }

        private void radio_fdur_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                txt_pre.Text = TimeSpan.Parse(dur).TotalSeconds.ToString();
            }
            catch { txt_pre.Text = Properties.Strings.error; }
        }

        private void radio_fdur_1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Double t = TimeSpan.Parse(dur).TotalSeconds + 1;
                txt_pre.Text = Math.Round(t,0).ToString();
            }
            catch { txt_pre.Text = Properties.Strings.error; }
        }

        private void txt_pre_TextChanged(object sender, EventArgs e)
        {
            txt_pre.SelectionStart = txt_pre.TextLength;
            txt_pre.ScrollToCaret();
         
        }
    }
}
