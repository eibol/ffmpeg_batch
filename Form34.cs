using FFBatch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form34 : Form
    {
        public enum DriveType
        {
            None = 0,
            Hdd,
            Ssd
        }
        public Form34()
        {
            InitializeComponent();
        }

        private String vff = "Stable branch";        
        private String ff_latest_exe = "https://github.com/eibol/ffmpeg_batch/releases/download/" + Application.ProductVersion + "/ffmpeg-release-full.7z";
        public String ff_state = String.Empty;
        public Boolean browse_ff = false;
        public Boolean down_g = false;
        public Boolean down_gh = false;
        public Boolean down_v = false;
        public Boolean down_vh = false;

        private void btn_br_ff_Click(object sender, EventArgs e)
        {
            browse_ff = true;
            this.Close();
        }

        private void btn_down_g_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe")) && !lbl_ff_v.Text.ToLower().Contains("essential"))
            {
                cb_srv.Enabled = false;
                Process.Start("https://www.gyan.dev/ffmpeg/builds/");
                return;
            }

            String srv_ok = "";
            lbl_expl.Text = Strings.init1;
            lbl_expl.Refresh();
            btn_down_g.Enabled = false;
            
            switch (cb_srv.SelectedIndex)
            {
                case 0:
                    down_gh = true;
                    srv_ok = get_res(ff_latest_exe);                    
                    break;
                case 1:
                    //MessageBox.Show("Download at gyan.dev a release of your choice, then just extract /bin/ffmpeg.exe to current application folder.");
                    Process.Start("https://www.gyan.dev/ffmpeg/builds/");
                    return;
                    break;                    
                case 2:
                    down_v = true;
                    srv_ok = get_res("https://ffmpeg-batch.sourceforge.io/ffm/ffmpeg-release-full.7z");                    
                    break;
                case 3:
                    down_vh = true;
                    srv_ok = get_res("https://files.videohelp.com/u/273695/ffmpeg-release-full.7z");                    
                    break;
            }

            btn_down_g.Enabled = true;
            lbl_expl.Text = Strings.ff_req; lbl_expl.Refresh();
            if (srv_ok.ToLower() != "ok") return;                        
            this.Close();
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

        private void Form34_Load(object sender, EventArgs e)
        {  
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
            }
            String full_path = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            if (File.Exists(full_path))
            {                
                cb_srv.Enabled = false;
                cb_srv.SelectedIndex = 1;
                lbl_ff_v.Text = lbl_ff_v.Text;                
                String show_path = Path.GetDirectoryName(full_path);
                if (show_path.Length > 46) { show_path = show_path.Substring(0, 46) + "..."; }
                if (Path.GetDirectoryName(full_path) != Path.GetDirectoryName(Application.ExecutablePath))
                {
                    lbl_path.Text = Strings.path + ": " + show_path;
                }
                else lbl_path.Text = Strings.path + ": " + Properties.Strings.local_file;
                
                if (!lbl_ff_v.Text.ToLower().Contains("essential")) btn_down_g.Text = Strings.browse + " " + "ffmpeg";
                else btn_down_g.Text = Strings.download + " " + "ffmpeg";

                ToolTip tt = new ToolTip();                
                tt.ToolTipIcon = ToolTipIcon.Info;
                tt.IsBalloon = true;
                tt.ShowAlways = true;                
                tt.SetToolTip(lbl_path, Path.GetDirectoryName(full_path));                
            }
            else
            {
                lbl_ff_v.Text = lbl_ff_v.Text + " ffmpeg.exe";
                btn_down_g.Text = Strings.download + " " + "ffmpeg";
                lbl_expl.Text = Strings.ff_req;
                cb_srv.SelectedIndex = 0;
            }
            
            lbl_srv.Text = Strings.server;
            lbl_ff_v.Text = lbl_ff_v.Text;            
            this.Text = Strings.browse + " " + "ffmpeg";            
            btn_close.Text = Strings.close_win;
            btn_br_ff.Text = Strings.browse + " " + Strings.pc;

            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private String get_res(String url)
        {
            // create the request
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000;

                // instruct the server to return headers only
                request.Method = "HEAD";

                // make the connection
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                // get the status code
                HttpStatusCode status = response.StatusCode;
                return status.ToString();
            }
            catch (Exception exc) 
            {
                do_nothing();
                MessageBox.Show(exc.Message, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "error"; 
            }            
        }

        private void do_nothing()
        {
            down_gh = false;
            down_v = false;
            down_vh = false;
            browse_ff = false;
        }

        private void cb_srv_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_down_g.Enabled = true;
            if (cb_srv.SelectedIndex == 1) lbl_d_v.Text = "Development branch";
            else lbl_d_v.Text = vff;
        }
    }
}

