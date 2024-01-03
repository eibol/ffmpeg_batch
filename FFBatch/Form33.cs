using FFBatch.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form33 : Form
    {
        public Form33()
        {
            InitializeComponent();            
        }

        public String mapp = String.Empty;
        
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void clear_rad_a()
        {
            foreach (Control c in tab_audio.Controls)
            {
                if (c is RadioButton) ((RadioButton)c).Checked = false;                
            }
        }
        private void clear_rad_s()
        {
            foreach (Control c in tab_audio.Controls)
            {
                if (c is RadioButton) ((RadioButton)c).Checked = false;
            }
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            String n = String.Empty;
            String v = String.Empty;
            String a = String.Empty;
            String s = String.Empty;

            if (chk_all.Checked)
            {
                mapp = "-map 0 ";
            }
            else
            {
                for (int i = tab_all.Controls.Count - 1; i >= 0; i--)
                {
                    if (((CheckBox)tab_all.Controls[i]).Checked)
                    {
                        n = n + "-map 0:" + tab_all.Controls[i].Text.Substring(tab_all.Controls[i].Text.Length - 1) + " ";
                    }
                }
                mapp = n;
            }

            if (chk_all_v.Checked) mapp = mapp + " -map 0:v ";
            else
            {
                for (int i = tab_video.Controls.Count - 1; i >= 0; i--)
                {
                    if (tab_video.Controls[i] is CheckBox)
                    {
                        if (((CheckBox)tab_video.Controls[i]).Checked)
                        {
                            int ind = Convert.ToInt16(tab_video.Controls[i].Name.Substring(tab_video.Controls[i].Name.Length - 1, 1)) - 1;
                            v = v + "-map 0:v:" + ind.ToString() + " ";
                        }
                    }
                }
                mapp =  mapp + v;
            }

            if (chk_all_a.Checked) mapp = mapp + " -map 0:a ";
            else
            {
                for (int i = tab_audio.Controls.Count - 1; i >= 0; i--)
                {
                    if (tab_audio.Controls[i] is CheckBox)
                    {
                        if (((CheckBox)tab_audio.Controls[i]).Checked)
                        {
                            int ind = Convert.ToInt16(tab_audio.Controls[i].Name.Substring(tab_audio.Controls[i].Name.Length - 1, 1)) - 1;
                            a = a + "-map 0:a:" + ind.ToString() + " ";
                        }
                    }
                }
                mapp = mapp + a;
            }

            if (chk_all_s.Checked) mapp = mapp + " -map 0:s ";
            else
            {
                for (int i = tab_subs.Controls.Count - 1; i >= 0; i--)
                {
                    if (tab_subs.Controls[i] is CheckBox)
                    {
                        if (((CheckBox)tab_subs.Controls[i]).Checked)
                        {
                            int ind = Convert.ToInt16(tab_subs.Controls[i].Name.Substring(tab_subs.Controls[i].Name.Length - 2, 2).Replace("_", "")) - 1;
                            s = s + "-map 0:s:" + ind.ToString() + " ";
                        }
                    }
                }
                mapp = mapp + s;
            }
            if (rad_def_aud1.Enabled || rad_def_aud2.Enabled || rad_def_aud3.Enabled)
            {
                if (rad_def_aud1.Enabled && rad_def_aud1.Checked) mapp = mapp + "-disposition:a none -disposition:a:0 default ";
                if (rad_def_aud2.Enabled && rad_def_aud2.Checked) mapp = mapp + "-disposition:a none -disposition:a:1 default ";
                if (rad_def_aud3.Enabled && rad_def_aud3.Checked) mapp = mapp + "-disposition:a none -disposition:a:2 default ";
            }
            if (rad_def_sub1.Enabled || rad_def_sub2.Enabled || rad_def_sub3.Enabled || rad_def_sub4.Enabled || rad_def_sub5.Enabled || rad_def_sub6.Enabled || rad_def_sub7.Enabled || rad_def_sub8.Enabled || rad_def_sub9.Enabled || rad_def_sub10.Enabled)
            {                
                if (rad_def_sub1.Enabled && rad_def_sub1.Checked) mapp = mapp + "-disposition:s forced -disposition:s:0 default ";
                if (rad_def_sub2.Enabled && rad_def_sub2.Checked) mapp = mapp + "-disposition:s forced -disposition:s:1 default ";
                if (rad_def_sub3.Enabled && rad_def_sub3.Checked) mapp = mapp + "-disposition:s forced -disposition:s:2 default ";
                if (rad_def_sub4.Enabled && rad_def_sub4.Checked) mapp = mapp + "-disposition:s forced -disposition:s:3 default ";
                if (rad_def_sub5.Enabled && rad_def_sub5.Checked) mapp = mapp + "-disposition:s forced -disposition:s:4 default ";
                if (rad_def_sub6.Enabled && rad_def_sub6.Checked) mapp = mapp + "-disposition:s forced -disposition:s:5 default ";
                if (rad_def_sub7.Enabled && rad_def_sub7.Checked) mapp = mapp + "-disposition:s forced -disposition:s:6 default ";
                if (rad_def_sub8.Enabled && rad_def_sub8.Checked) mapp = mapp + "-disposition:s forced -disposition:s:7 default ";
                if (rad_def_sub9.Enabled && rad_def_sub9.Checked) mapp = mapp + "-disposition:s forced -disposition:s:8 default ";
                if (rad_def_sub10.Enabled && rad_def_sub10.Checked) mapp = mapp + "-disposition:s forced -disposition:s:9 default ";

            }
            mapp = mapp.TrimStart(' ');
            this.Close();
        }

        private void chk_all_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all.Checked)
            {
                foreach (Control c in tab_all.Controls) { c.Enabled = false; } 
            }
            else
            {
                foreach (Control c in tab_all.Controls) { c.Enabled = true; }
            }
            chk_all.Enabled = true;
        }

        private void chk_all_v_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all_v.Checked)
            {
                foreach (Control c in tab_video.Controls) { c.Enabled = false; }
            }
            else
            {
                foreach (Control c in tab_video.Controls) { c.Enabled = true; }
            }
            chk_all_v.Enabled = true;
        }

        private void chk_all_a_CheckedChanged(object sender, EventArgs e)
        {            
            if (chk_all_a.Checked)
            {
                foreach (Control c in tab_audio.Controls)
                {
                    if (c is RadioButton)
                    {
                        c.Enabled = true;
                        ((RadioButton)c).Checked = false;
                    }
                    else c.Enabled = false;                    
                }
            }
            else
            {
                foreach (Control c in tab_audio.Controls)
                {
                    if (c is RadioButton)
                    {
                        c.Enabled = true;
                        ((RadioButton)c).Checked = false;
                    }
                    else c.Enabled = true;
                }
            }
            chk_all_a.Enabled = true;
        }

        private void chk_all_s_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_all_s.Checked)
            {
                foreach (Control c in tab_subs.Controls)
                {
                    if (c is RadioButton)
                    {
                        c.Enabled = true;
                        ((RadioButton)c).Checked = false;
                    }
                    else c.Enabled = false;
                }
            }
            else
            {
                foreach (Control c in tab_subs.Controls)
                {
                    if (c is RadioButton)
                    {
                        c.Enabled = true;
                        ((RadioButton)c).Checked = false;
                    }
                    else c.Enabled = true;
                }
            }
            chk_all_s.Enabled = true;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            foreach (Control c in tab_s.Controls)
            {
                foreach (Control c2 in c.Controls)
                {
                    if (c2 is CheckBox)
                    {
                        ((CheckBox)c2).Checked = false;
                    }
                }
            }
        }

        private void chk_a_1_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_aud1.Enabled = chk_a_1.Checked;
            clear_rad_a();
        }

        private void chk_a_2_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_aud2.Enabled = chk_a_2.Checked;
            clear_rad_a();
        }

        private void chk_a_3_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_aud3.Enabled = chk_a_3.Checked;
            clear_rad_a();
        }

        private void chk_s_1_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub1.Enabled = chk_s_1.Checked;
            clear_rad_s();

        }

        private void chk_s_2_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub2.Enabled = chk_s_2.Checked;
            clear_rad_s();
        }

        private void chk_s_3_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub3.Enabled = chk_s_3.Checked;
            clear_rad_s();
        }

        private void chk_s_4_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub4.Enabled = chk_s_4.Checked;
            clear_rad_s();
        }

        private void chk_s_5_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub5.Enabled = chk_s_5.Checked;
            clear_rad_s();
        }

        private void chk_s_6_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub6.Enabled = chk_s_6.Checked;
            clear_rad_s();
        }

        private void chk_s_7_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub7.Enabled = chk_s_7.Checked;
            clear_rad_s();
        }

        private void chk_s_8_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub8.Enabled = chk_s_8.Checked;
            clear_rad_s();
        }

        private void chk_s_9_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub9.Enabled = chk_s_9.Checked;
            clear_rad_s();
        }

        private void chk_s_10_CheckedChanged(object sender, EventArgs e)
        {
            rad_def_sub10.Enabled = chk_s_10.Checked;
            clear_rad_s();
        }

        private void Form33_Load(object sender, EventArgs e)
        {
            pic_f.Image = img_icons.Images[0];
            init_lang();                
        }

        private void init_lang()
        {
            tab_video.Text = Strings.video;
            tab_audio.Text = Strings.audio;
            tab_subs.Text = Strings.subtitles;
            btn_ok.Text = Strings.ok;
            btn_cancel.Text = Strings.cancel;
            this.Text = Strings.map_strs;
            //label1.Text = Strings.map_strs;
            lbl_explain.Text = Strings.def_mapping;
            tab_all.Text = Strings.all;
            btn_clear.Text = Strings.init0;
            chk_all.Text = Strings.mapp_all_str;

            foreach (Control c in tab_all.Controls)
            {
                if (c.Name.Contains("chk_n"))
                {
                    c.Text = Properties.Strings.map_str + " " + c.Name.Substring(Name.Length - 1, 1);
                }
            }
            rad_def_aud1.Text = Strings.Default;
            rad_def_aud2.Text = Strings.Default;
            rad_def_aud3.Text = Strings.Default;
            chk_all_v.Text = Strings.map_all_v;
            chk_v_1.Text = Strings.map + " " + Strings.first;
            chk_v_2.Text = Strings.map + " " + Strings.second;
            chk_v_3.Text = Strings.map + " " + Strings.third;
            chk_all_a.Text = Strings.map_all_a;
            chk_a_1.Text = Strings.map + " " + Strings.first;
            chk_a_2.Text = Strings.map + " " + Strings.second;
            chk_a_3.Text = Strings.map + " " + Strings.third;            
            chk_all_s.Text = Strings.map_all_s;
            chk_s_1.Text = Strings.map + " " + Strings.first;
            chk_s_2.Text = Strings.map + " " + Strings.second;
            chk_s_3.Text = Strings.map + " " + Strings.third;
            chk_s_4.Text = Strings.map + " " + Strings.fourth;
            chk_s_5.Text = Strings.map + " " + Strings.fifth;
            chk_s_6.Text = Strings.map + " " + Strings.sixth;
            chk_s_7.Text = Strings.map + " " + Strings.seventh;
            chk_s_8.Text = Strings.map + " " + Strings.eighth;
            chk_s_9.Text = Strings.map + " " + Strings.nineth;
            chk_s_10.Text = Strings.map + " " + Strings.tenth;
            lbl_f.Text = Strings.forced;

        }
    }
}

