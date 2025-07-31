using FFBatch.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FFBatch
{
    public partial class Form33 : Form
    {
        public Form33()
        {
            InitializeComponent();            
        }

        public String mapp = String.Empty;
        public Image browse_icon = null;
        public String lv1_item = String.Empty;
        public ImageList img_streams;
        private DataGridView dg_streams = new DataGridView();
        private Label lbl_lv = new Label();
        private Button browse_f = new Button();        

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
            if (tab_s.TabCount > 4 && tab_s.SelectedIndex == 4)
            {                
                mapp = String.Empty;
                Boolean forced_sub = false;
                Boolean forced_aud = false;
                int forced_id = 0;
                int checked_subs = 0;
                String id = "";
                int ind = 0;

                int vids = 0;
                int auds = 0;
                int subs = 0;

                foreach (DataGridViewRow row in dg_streams.Rows)
                {
                    if (row.Cells[2].Value.ToString().Contains("Video"))
                    {
                        row.Tag = vids;
                        vids++;                        
                    }

                    if (row.Cells[2].Value.ToString().Contains("Audio"))
                    {
                        row.Tag = auds;
                        auds++;
                    }
                    if (row.Cells[2].Value.ToString().Contains("Subtitle"))
                    {
                        row.Tag = subs;
                        subs++;                        
                    }
                }

                    foreach (DataGridViewRow row in dg_streams.Rows)
                    {                        
                    if (Convert.ToBoolean(row.Cells[3].Value) == true)
                    {
                        if (row.Cells[2].Value.ToString().Contains("Subtitle")) checked_subs++;

                        id = row.Cells[1].Value.ToString().Substring(3, row.Cells[1].Value.ToString().Length - 3);
                        mapp = mapp + "-map 0:" + Convert.ToInt16(id).ToString() + " ";

                        if (Convert.ToBoolean(row.Cells[4].Value) == true)
                        {
                            if (row.Cells[2].Value.ToString().Contains("Subtitle"))
                            {
                                forced_sub = true;
                                forced_id = (int)row.Tag;
                            }
                            if (row.Cells[2].Value.ToString().Contains("Audio"))
                            {
                                forced_aud = true;
                                forced_id = (int)row.Tag;
                            }
                        }
                    }
                }
                if (checked_subs > 0)
                {
                    if (forced_sub == true) mapp = mapp + "-disposition:s forced -disposition:s:" + forced_id + " default ";
                    else mapp = mapp + "-disposition:s none ";
                }
                if (forced_aud == true) mapp = mapp + "-disposition:a none -disposition:a:" + forced_id + " default ";

            }
            else
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
                    mapp = mapp + v;
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

                List<int> n_subs_chk = new List<int>();

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
                                n_subs_chk.Add(ind);
                            }
                        }
                    }
                    mapp = mapp + s;
                    //foreach (int str in n_subs_chk) MessageBox.Show(str.ToString());
                }
                if (rad_def_aud1.Enabled || rad_def_aud2.Enabled || rad_def_aud3.Enabled)
                {
                    if (rad_def_aud1.Enabled && rad_def_aud1.Checked) mapp = mapp + "-disposition:a none -disposition:a:0 default ";
                    if (rad_def_aud2.Enabled && rad_def_aud2.Checked) mapp = mapp + "-disposition:a none -disposition:a:1 default ";
                    if (rad_def_aud3.Enabled && rad_def_aud3.Checked) mapp = mapp + "-disposition:a none -disposition:a:2 default ";
                }
                Boolean forced = false;
                if (rad_def_sub1.Enabled || rad_def_sub2.Enabled || rad_def_sub3.Enabled || rad_def_sub4.Enabled || rad_def_sub5.Enabled || rad_def_sub6.Enabled || rad_def_sub7.Enabled || rad_def_sub8.Enabled || rad_def_sub9.Enabled || rad_def_sub10.Enabled)
                {
                    int index = 0;
                    if (rad_def_sub1.Enabled && rad_def_sub1.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (0)); forced = true;
                    }
                    if (rad_def_sub2.Enabled && rad_def_sub2.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (1)); forced = true;
                    }
                    if (rad_def_sub3.Enabled && rad_def_sub3.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (2)); forced = true;
                    }
                    if (rad_def_sub4.Enabled && rad_def_sub4.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (3)); forced = true;
                    }
                    if (rad_def_sub5.Enabled && rad_def_sub5.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (4)); forced = true;
                    }
                    if (rad_def_sub6.Enabled && rad_def_sub6.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (5)); forced = true;
                    }
                    if (rad_def_sub7.Enabled && rad_def_sub7.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (6)); forced = true;
                    }
                    if (rad_def_sub8.Enabled && rad_def_sub8.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (7)); forced = true;
                    }
                    if (rad_def_sub9.Enabled && rad_def_sub9.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (8)); forced = true;
                    }
                    if (rad_def_sub10.Enabled && rad_def_sub10.Checked)
                    {
                        index = n_subs_chk.FindIndex(inn => inn == (9)); forced = true;
                    }

                    if (forced == true) mapp = mapp + "-disposition:s forced -disposition:s:" + (index).ToString() + " default ";
                    if (forced == true) mapp = mapp + "-disposition:s none -disposition:s:" + (index).ToString() + " default ";
                }
                mapp = mapp.TrimStart(' ');
            }
                if (mapp.Length > 0)
                {
                    if (chk_meta.Checked) mapp = mapp + "-map_metadata 0 ";
                    if (chk_attach.Checked) mapp = mapp + " -map 0:t? -c:t copy ";
                }

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
           foreach (Control c in tab_subs.Controls)
           {                           

                if (chk_all_s.Checked)
                {
                    c.Enabled = false;
                }
                else
                {
                    c.Enabled = true;
                    if (c is CheckBox)
                    {
                        ((CheckBox)c).Checked = false;
                    }
                    if (c is RadioButton)
                    {
                        ((RadioButton)c).Checked = false;
                        ((RadioButton)c).Enabled = false;
                    }
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
            foreach (DataGridViewRow row in dg_streams.Rows)
            {
                row.Cells[3].Value = false;
                row.Cells[4].Value = false;
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
        public void UpdateColorDark(Control myControl)
        {
            myControl.BackColor = Color.FromArgb(255, 64, 64, 64);
            myControl.ForeColor = Color.White;
            foreach (Control subC in myControl.Controls)
            {
                UpdateColorDark(subC);
            }
        }
        private void Form33_Load(object sender, EventArgs e)
        {  

            pic_f.Image = img_icons.Images[0];
            pic_vid.Image = img_streams.Images[0];
            pic_aud.Image = img_streams.Images[1];
            pic_subs.Image = img_streams.Images[2];

            dg_streams.Columns.Clear();
            dg_streams.Columns.Add(new DataGridViewImageColumn());            
            dg_streams.Columns.Add("id", "ID");
            dg_streams.Columns.Add("output", Strings.output);
            dg_streams.Columns.Add(new DataGridViewCheckBoxColumn());            
            dg_streams.Columns.Add(new DataGridViewCheckBoxColumn());            
            dg_streams.Columns[0].Width = 25;            
            dg_streams.Columns[1].Width = 30;
            dg_streams.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.Columns[2].Width = 251;
            dg_streams.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg_streams.Columns[3].Width = 30;
            dg_streams.Columns[3].HeaderText = "✓";
            dg_streams.Columns[4].Width = 30;
            dg_streams.Columns[4].HeaderText = "✓✓";
            dg_streams.Width = tab_s.Width - 9;
            dg_streams.Height = tab_s.Height - 27;
            dg_streams.BackgroundColor = this.BackColor;
            dg_streams.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.RowHeadersVisible = false;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_streams.Columns[1].ReadOnly = true;
            dg_streams.Columns[2].ReadOnly = true;
            dg_streams.Columns[4].ReadOnly = true;
            dg_streams.Columns[3].ToolTipText = Strings.map_str;            
            dg_streams.Columns[4].ToolTipText = Strings.forced;
            dg_streams.AllowUserToAddRows = false;
            dg_streams.SelectionMode = DataGridViewSelectionMode.CellSelect;            

            dg_streams.CellContentClick += new DataGridViewCellEventHandler(dg_streams_CellContent_Click);
            dg_streams.CellValueChanged += new DataGridViewCellEventHandler(dg_streams_CellValueChanged_Click);
            browse_f.Click += new EventHandler(browse_f_Click);            

            if (tab_s.TabPages.Count == 5) tab_s.TabPages.RemoveAt(4);

            lbl_lv.Visible = false;
            browse_f.Visible = false;
            if (lv1_item != String.Empty)
            {                
                if (tab_s.TabPages.Count == 4)
                {
                    tab_s.TabPages.Add(Strings.Stream_file);
                    dg_streams.Parent = tab_s.TabPages[4];
                    lbl_lv.Parent = this;
                    lbl_lv.Width = tab_s.Width - 4;
                    String show_path = Path.GetFileName(lv1_item);
                    if (show_path.Length > 64) { show_path = show_path.Substring(0, 64) + "..." + Path.GetExtension(lv1_item) ; }
                    lbl_lv.Text = show_path;
                    lbl_lv.Top = 5;
                    lbl_lv.Left = tab_s.Left + 1;                    
                    browse_f.Parent = this;
                    browse_f.Left = tab_s.Width - 19;
                    browse_f.Top = tab_s.Top - 1;
                    browse_f.Width = 24;
                    browse_f.Height = 20;                    
                    browse_f.Image = img_icons.Images[1];
                    browse_f.FlatStyle = FlatStyle.Flat;
                    browse_f.FlatAppearance.BorderSize = 0;
                    browse_f.ImageAlign = ContentAlignment.MiddleCenter;
                    browse_f.BringToFront();

                    file_streams();                    
                }       
            }
            
            init_lang();
            foreach (Control ct in this.Controls) ct.AccessibleDescription = ct.Text;
            if (Settings.Default.visuals == false && Settings.Default.visuals_all == false)
            {
                foreach (TabPage t in tab_s.TabPages) t.BackColor = SystemColors.Window;
            }
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                dg_streams.BackgroundColor = Color.Gray;
                dg_streams.RowsDefaultCellStyle.BackColor = Color.Gray;
            }
        }

        private void browse_f_Click(object sender, EventArgs e)
        {
            OpenFileDialog openf = new OpenFileDialog();
            tab_subs.Focus();
            openf.InitialDirectory = Path.GetDirectoryName(lv1_item);
            openf.Filter = Strings.av + "  |*.mp4; *.mkv; *.ts; *.mxf; *.mp3; *.wav; *.flac; *.m4a; *.avi; *.mts; *.flv; *.alac; *.aac; *.mpg; *.mp2; *.mpe; *.ogv; *.webm; *.aiff; *.vob; *.wma; *.wmv; *.mov; *.mka; *.m2ts; *.ac3; *.ogg; *.avs|" + Strings.imgs + " (*.jpg; *.png; *.gif) | *.jpg; *.png; *.gif|" + Strings.all_files + " (*.*) | *.*";
            
            if (openf.ShowDialog() == DialogResult.OK)
            {
                lv1_item = openf.FileName;
                String show_path = Path.GetFileName(lv1_item);
                if (show_path.Length > 64) { show_path = show_path.Substring(0, 64) + "..." + Path.GetExtension(lv1_item); }
                lbl_lv.Text = show_path;
                          
                file_streams();
            }
            openf.Dispose();
        }

        private void dg_streams_CellValueChanged_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    if (Convert.ToBoolean(dg_streams.Rows[e.RowIndex].Cells[3].Value) == true)
                    {
                        dg_streams.Rows[e.RowIndex].Cells[4].ReadOnly = false;
                    }
                    else
                    {
                        dg_streams.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        dg_streams.Rows[e.RowIndex].Cells[4].Value = false;
                    }
                } catch { }
            }
            dg_streams.RefreshEdit();            
        }

        private void dg_streams_CellContent_Click(object sender, DataGridViewCellEventArgs e)
        {
            dg_streams.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void file_streams()
        {
            dg_streams.Rows.Clear();
            Boolean is_av1 = false;
            Process ff_str = new Process();
            String stream = String.Empty;
            String stream_n = String.Empty;

            ff_str.StartInfo.FileName = System.IO.Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            ff_str.StartInfo.Arguments = " -i " + '\u0022' + lv1_item + '\u0022' + " -hide_banner";
            ff_str.StartInfo.RedirectStandardOutput = true;
            ff_str.StartInfo.RedirectStandardError = true;
            ff_str.StartInfo.UseShellExecute = false;
            ff_str.StartInfo.CreateNoWindow = true;
            ff_str.EnableRaisingEvents = true;
            ff_str.Start();

            Form11 frm_prog = new Form11();
            frm_prog.Refresh();
            frm_prog.label1.Text = FFBatch.Properties.Strings.obt_strs;
            frm_prog.label1.Refresh();

            frm_prog.procId = ff_str.Id;            
            String sub_str = "";
            int f_streams = -1;
            Boolean has_stream = false;
            int img = 0;
            while (!ff_str.StandardError.EndOfStream)
            {
                stream = ff_str.StandardError.ReadLine();
                if (stream.ToLower().Contains("av1")) is_av1 = true;

                if (stream.Contains("Stream #0:"))
                {
                    has_stream = true;
                    f_streams = f_streams + 1;

                    if (stream.Contains("Video")) img = 0;
                    if (stream.Contains("Audio")) img = 1;
                    if (stream.Contains("Subtitle")) img = 2;

                    if (stream.Substring(stream.IndexOf("#0:") + 4, 1) == "(")
                    {
                        if (stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(und)" || stream.Substring(stream.IndexOf("#0:") + 4, 5) == "(unk)")
                        {
                            stream_n = stream_n + 1;
                            sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 11);
                            int ind_sar = stream.LastIndexOf("SAR");
                            if (ind_sar > 0) stream = stream.Substring(0, ind_sar - 2);
                            stream = stream.Replace(@"/unknown", "").Replace("progressive", "").Replace("(side)", "").Replace("(pgssub)", "");
                            dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 11), (stream.Length - sub_str.Length)));
                        }
                        else
                        {
                            sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                            int ind_sar = stream.LastIndexOf("SAR");
                            if (ind_sar > 0) stream = stream.Substring(0, ind_sar - 2);
                            stream = stream.Replace(@"/unknown", "").Replace("progressive", "").Replace("(side)", "").Replace("(pgssub)", "");
                            dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length)));
                        }
                    }
                    else
                    {
                        if (stream.Contains("Video"))
                        {
                            sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                            String to_add = stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length));
                            //Remove () and []
                            to_add = Regex.Replace(to_add, @"\([^()]*\)", string.Empty);
                            Regex yourRegex = new Regex(@"\[([^\]]+)\]");
                            to_add = yourRegex.Replace(to_add, "");
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            to_add = regex.Replace(to_add, " ");

                            if (to_add.Substring(0, 2) == ": ") to_add = to_add.Substring(2, to_add.Length - 2);
                            int ind_sar = to_add.LastIndexOf("SAR");
                            if (ind_sar > 0) to_add = to_add.Substring(0, ind_sar - 2);
                            ind_sar = to_add.LastIndexOf("fps");
                            if (ind_sar > 0) to_add = to_add.Substring(0, ind_sar - 5);
                            to_add = to_add.Replace(@"/unknown", "").Replace("progressive", "");

                            dg_streams.Rows.Add(img_streams.Images[0], "#0:" + f_streams.ToString(), to_add);
                        }
                        if (stream.Contains("Audio"))
                        {
                            sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                            String to_add = stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length));
                            //Remove () and []
                            to_add = Regex.Replace(to_add, @"\([^()]*\)", string.Empty);
                            Regex yourRegex = new Regex(@"\[([^\]]+)\]");
                            to_add = yourRegex.Replace(to_add, "");
                            RegexOptions options = RegexOptions.None;
                            Regex regex = new Regex("[ ]{2,}", options);
                            to_add = regex.Replace(to_add, " ");

                            if (to_add.Substring(0, 2) == ": ") to_add = to_add.Substring(2, to_add.Length - 2);

                            this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[1], "#0:" + f_streams.ToString(), to_add));
                        }
                        if (stream.Contains("Subtitle"))
                        {
                            sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                            dg_streams.Rows.Add(img_streams.Images[2], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length)));
                        }
                    }
                }
            }
            this.Enabled = true;
            ff_str.WaitForExit(10000);
            this.Enabled = true;


            this.Enabled = true;
            if (frm_prog.abort_validate == true)
            {
                this.Close();
                return;
            }

            if (has_stream == false)
            {
                dg_streams.Rows.Add(img_streams.Images[3], "0", FFBatch.Properties.Strings.no_str_f);
            }

            dg_streams.ClearSelection();
            dg_streams.CurrentCell = null;

            foreach (DataGridViewRow r in dg_streams.Rows)
            {
                foreach (DataGridViewCell c in r.Cells)
                {
                    if (c.ColumnIndex == 4) c.ToolTipText = Strings.forced;
                }
            }
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
            chk_meta.Text = Strings.map_meta;
            chk_attach.Text = Strings.map_attach;

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
        private void tab_s_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab_s.SelectedIndex == 4)
            {
                lbl_lv.Visible = true;
                browse_f.Visible = true;
            }
            else
            {
                lbl_lv.Visible = false;
                browse_f.Visible = false;
            }
        }
    }
}