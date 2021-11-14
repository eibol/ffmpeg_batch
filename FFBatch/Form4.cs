using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        public String lv1_item = String.Empty;
        public String filter_type = String.Empty;
        public String filter_value = String.Empty;
        public String filter_action = String.Empty;
        public Boolean cancel_filter = true;
        public Boolean remove_invalid = false;
        public String stream_n = String.Empty;
        public Boolean remove_not_video = false;
        private ListView LB1 = new ListView();
        private Form5 form5 = new Form5();

        private void cb_filterby_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_filterby.SelectedIndex == 0)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDownList;
                cb_value_f.Items.Clear();
                cb_value_f.Items.Add(FFBatch.Properties.Strings.success);
                cb_value_f.Items.Add(FFBatch.Properties.Strings.replaced);
                cb_value_f.Items.Add(FFBatch.Properties.Strings.queued);
                cb_value_f.Items.Add(FFBatch.Properties.Strings.error);
                cb_value_f.Items.Add(FFBatch.Properties.Strings.aborted);
                cb_value_f.SelectedIndex = 0;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.status;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_status;
            }
            if (cb_filterby.SelectedIndex == 1)
            {
                cb_value_f.Items.Clear();
                cb_value_f.DropDownStyle = ComboBoxStyle.Simple;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.file_ext;
                cb_value_f.Text = String.Empty;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_ext;
            }

            if (cb_filterby.SelectedIndex == 2)
            {
                cb_value_f.Items.Clear();
                cb_value_f.DropDownStyle = ComboBoxStyle.Simple;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.f_size;
                cb_value_f.Text = String.Empty;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Text = ">= MB";
                lbl_greater.Visible = true;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_size;
            }

            if (cb_filterby.SelectedIndex == 3)
            {
                cb_value_f.Items.Clear();
                cb_value_f.DropDownStyle = ComboBoxStyle.Simple;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.f_bitr;
                cb_value_f.Text = String.Empty;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Text = ">= Kbps";
                lbl_greater.Visible = true;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_all_bit;
            }

            if (cb_filterby.SelectedIndex == 4)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_value_f.Items.Add("h264");
                cb_value_f.Items.Add("hevc");
                cb_value_f.Items.Add("vp9");
                cb_value_f.Items.Add("mpeg2video");
                cb_value_f.Items.Add("theora");
                cb_value_f.Items.Add("prores");
                cb_value_f.Items.Add("dnxhd");
                cb_value_f.Items.Add("rawvideo");
                cb_value_f.SelectedIndex = 0;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.Video_codec;
                cb_streams.Visible = true;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.f_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.s_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.any_str);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = true;
                chk_novideo.Enabled = true;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_vcodec;
            }

            if (cb_filterby.SelectedIndex == 5)
            {
                cb_value_f.Items.Clear();
                cb_value_f.DropDownStyle = ComboBoxStyle.Simple;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.v_bitr;
                cb_value_f.Text = String.Empty;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Text = ">= Kbps";
                lbl_greater.Visible = true;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_vbr;
            }

            if (cb_filterby.SelectedIndex == 6)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_value_f.Items.Add("aac");
                cb_value_f.Items.Add("eac3");
                cb_value_f.Items.Add("ac3");
                cb_value_f.Items.Add("mp3");
                cb_value_f.Items.Add("vorbis");
                cb_value_f.Items.Add("flac");
                cb_value_f.Items.Add("dts");
                cb_value_f.Items.Add("pcm");
                cb_value_f.SelectedIndex = 0;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.Audio_codec;
                cb_streams.Visible = true;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.f_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.s_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.any_str);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = true;
                lb_of.Visible = true;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_acodec;
            }
            if (cb_filterby.SelectedIndex == 7)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_value_f.Items.Add("15");
                cb_value_f.Items.Add("24");
                cb_value_f.Items.Add("23.976");
                cb_value_f.Items.Add("25");
                cb_value_f.Items.Add("29.970");
                cb_value_f.Items.Add("30");
                cb_value_f.Items.Add("50");
                cb_value_f.Items.Add("59.94");
                cb_value_f.Items.Add("60");
                cb_value_f.SelectedIndex = 0;
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.fr_rate;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                lb_of.Visible = true;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_rate;
            }

            if (cb_filterby.SelectedIndex == 8)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                String[] sizes = new string[] { "Custom", "4096x2160", "3840x2160", "1920x1080", "1440x1080", "1280x720", "1024x768", "1024x576", "800x600", "800x480", "720x576", "720x480" };
                foreach (String v_crops in sizes) cb_value_f.Items.Add(v_crops);
                cb_value_f.SelectedIndex = 1;
                cb_action.SelectedIndex = 0;
                filter_type = "Frame size";
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_items);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                lb_of.Visible = true;
                chk_novideo.Enabled = false;
                n_width.Visible = true;
                n_height.Visible = true;
                lbl_size.Visible = true;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_frame;
            }
            if (cb_filterby.SelectedIndex == 9)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.c_str;
                cb_value_f.Text = String.Empty;
                cb_streams.Visible = true;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.f_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.s_str);
                cb_streams.Items.Add(FFBatch.Properties.Strings.any_str);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = true;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_ff_out;
            }

            if (cb_filterby.SelectedIndex == 10)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.c_str + " " + "(MI)";
                cb_value_f.Text = String.Empty;
                cb_streams.Visible = true;
                cb_streams.Items.Clear();
                cb_streams.Items.Add(FFBatch.Properties.Strings.all_files);
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = false;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_med_out;
            }

            if (cb_filterby.SelectedIndex == 11)
            {
                cb_value_f.DropDownStyle = ComboBoxStyle.DropDown;
                cb_value_f.Items.Clear();
                cb_action.SelectedIndex = 0;
                filter_type = FFBatch.Properties.Strings.Metadata;
                cb_value_f.Text = String.Empty;
                cb_streams.Visible = true;
                cb_streams.Items.Clear();
                cb_streams.Items.Add("Album");
                cb_streams.Items.Add("Performer");
                cb_streams.Items.Add("Composer");
                cb_streams.Items.Add("Year");
                cb_streams.Items.Add("Genre");
                cb_streams.SelectedIndex = 0;
                cb_streams.Enabled = true;
                chk_novideo.Enabled = false;
                n_width.Visible = false;
                n_height.Visible = false;
                lbl_size.Visible = false;
                lbl_greater.Visible = false;
                lbl_info_search.Text = FFBatch.Properties.Strings.filter_meta;
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            cb_filterby.Items.Clear();
            cb_filterby.Items.Add(FFBatch.Properties.Strings.status);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.file_ext);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.f_size);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.f_bitr);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.Video_codec);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.v_bitr);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.Audio_codec);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.fr_rate);
            cb_filterby.Items.Add(FFBatch.Properties.Strings2.frame_size);
            cb_filterby.Items.Add(FFBatch.Properties.Strings.c_str + " (FF)");
            cb_filterby.Items.Add(FFBatch.Properties.Strings.c_str + " (MI)");
            cb_filterby.Items.Add(FFBatch.Properties.Strings.Metadata);

            cb_action.Items.Clear();
            cb_action.Items.Add(FFBatch.Properties.Strings.keep);
            cb_action.Items.Add(FFBatch.Properties.Strings.remove);

            ToolTip toolTipaA6 = new ToolTip();
            toolTipaA6.AutoPopDelay = 9000;
            toolTipaA6.InitialDelay = 750;
            toolTipaA6.ReshowDelay = 500;
            toolTipaA6.ShowAlways = true;
            toolTipaA6.SetToolTip(this.btn_streams, FFBatch.Properties.Strings.Display_file_streams);

            ToolTip toolTipa7 = new ToolTip();
            toolTipa7.AutoPopDelay = 9000;
            toolTipa7.InitialDelay = 750;
            toolTipa7.ReshowDelay = 500;
            toolTipa7.ShowAlways = true;
            toolTipa7.SetToolTip(this.btn_mediainfo, FFBatch.Properties.Strings.Display_multimedia_file_info);
            refresh_lang();
            this.Text = FFBatch.Properties.Strings.file_filtering;

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

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form4));
            RefreshResources(this, resources);
        }

        private void RefreshResources(Control ctrl, ComponentResourceManager res)
        {
            ctrl.SuspendLayout();
            this.InvokeEx(f => res.ApplyResources(ctrl, ctrl.Name, Thread.CurrentThread.CurrentUICulture));
            foreach (Control control in ctrl.Controls)
                RefreshResources(control, res); // recursion
            ctrl.ResumeLayout(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Boolean add_new = false;
            foreach (String item in cb_value_f.Items)
            {
                if (item != cb_value_f.Text) add_new = true;
                break;
            }
            if (add_new == true) cb_value_f.Items.Add(cb_value_f.Text);

            cancel_filter = false;
            if (chk_invalid.CheckState == CheckState.Checked) remove_invalid = true;
            else remove_invalid = false;
            if (chk_novideo.CheckState == CheckState.Checked) remove_not_video = true;
            else remove_not_video = false;

            if (cb_streams.SelectedIndex == 0) stream_n = FFBatch.Properties.Strings.f_str;
            if (cb_streams.SelectedIndex == 1) stream_n = FFBatch.Properties.Strings.s_str;
            if (cb_streams.SelectedIndex == 2) stream_n = FFBatch.Properties.Strings.any_str;

            if (cb_filterby.SelectedIndex == 11) stream_n = cb_streams.Text;

            if (cb_filterby.SelectedIndex == -1)
            {
                MessageBox.Show(FFBatch.Properties.Strings.no_filter);
                cancel_filter = true;
                return;
            }
            if (cb_filterby.SelectedIndex == 0) filter_type = FFBatch.Properties.Strings.status;
            if (cb_filterby.SelectedIndex == 1)
            {
                filter_type = FFBatch.Properties.Strings.file_ext;
                if (filter_value.ToString() == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }
            if (cb_filterby.SelectedIndex == 2) filter_type = FFBatch.Properties.Strings.f_size;
            if (cb_filterby.SelectedIndex == 3)
            {
                filter_type = FFBatch.Properties.Strings.f_bitr;
                if (filter_value.ToString() == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
                try
                {
                    int.Parse(filter_value);
                }
                catch
                {
                    MessageBox.Show(FFBatch.Properties.Strings.invalid_fsize + " " + "(Kbps).");
                    cancel_filter = true;
                    return;
                }
            }

            if (cb_filterby.SelectedIndex == 4)
            {
                filter_type = FFBatch.Properties.Strings.Video_codec;
                if (filter_value.ToString() == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }
            if (cb_filterby.SelectedIndex == 5)
            {
                filter_type = FFBatch.Properties.Strings.v_bitr;
                if (filter_value.ToString() == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }

            if (cb_filterby.SelectedIndex == 6)
            {
                filter_type = FFBatch.Properties.Strings.Audio_codec;
                if (filter_value.ToString() == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }
            if (cb_filterby.SelectedIndex == 7) filter_type = FFBatch.Properties.Strings.fr_rate;
            if (cb_filterby.SelectedIndex == 8) filter_type = "Frame size";
            if (cb_filterby.SelectedIndex == 9)
            {
                filter_type = FFBatch.Properties.Strings.c_str;
                if (filter_value == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }

            if (cb_filterby.SelectedIndex == 10)
            {
                filter_type = FFBatch.Properties.Strings.c_str + " " + "(MI)";
                if (filter_value == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }
            if (cb_filterby.SelectedIndex == 11)
            {
                filter_type = FFBatch.Properties.Strings.Metadata;
                if (filter_value == String.Empty)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.filter_bl);
                    cancel_filter = true;
                    return;
                }
            }

            if (cb_value_f.SelectedIndex != -1)
            {
                filter_value = cb_value_f.SelectedItem.ToString();
            }
            if (cb_action.SelectedIndex != -1)
            {
                filter_action = cb_action.SelectedItem.ToString();
            }

            if (cb_filterby.SelectedIndex == 2)
            {
                try
                {
                    long.Parse(filter_value);
                }
                catch
                {
                    MessageBox.Show(FFBatch.Properties.Strings.invalid_fsize + " " + "(Megabytes).");
                    cancel_filter = true;
                    return;
                }
            }

            if (cb_filterby.SelectedIndex == 7)
            {
                try
                {
                    Decimal test_fr = Convert.ToDecimal(filter_value);
                }
                catch
                {
                    MessageBox.Show(FFBatch.Properties.Strings.invalid_frate);
                    cancel_filter = true;
                    return;
                }
            }
            if (cb_filterby.SelectedIndex == 8)
            {
                if (cb_value_f.SelectedIndex != 0)
                {
                    try
                    {
                        Int16 test_fr = Convert.ToInt16(filter_value.Substring(0, filter_value.LastIndexOf("x")));
                        Int16 test_fr2 = Convert.ToInt16(filter_value.Substring(filter_value.LastIndexOf("x") + 1, filter_value.Length - filter_value.Substring(0, filter_value.LastIndexOf("x")).Length - 1));
                    }
                    catch
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.invalid_frame_s);
                        cancel_filter = true;
                        return;
                    }
                }
                else
                {
                    filter_value = n_width.Value.ToString() + "x" + n_height.Value.ToString();
                }
            }

            Form4.ActiveForm.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancel_filter = true;
            Form4.ActiveForm.Close();
        }

        private void cb_value_f_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_value = cb_value_f.SelectedItem.ToString();
            if (cb_filterby.SelectedIndex == 6 && cb_value_f.SelectedIndex == 0)
            {
                n_width.Enabled = true;
                n_height.Enabled = true;
            }
            else
            {
                n_width.Enabled = false;
                n_height.Enabled = false;
            }
        }

        private void cb_action_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter_action = cb_action.SelectedItem.ToString();
        }

        private void cb_value_f_TextChanged(object sender, EventArgs e)
        {
            filter_value = cb_value_f.Text;
        }

        private void chk_invalid_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_invalid.CheckState == CheckState.Checked)
            {
                remove_invalid = true;
            }
            else
            {
                remove_invalid = false;
            }
        }

        private void cb_streams_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_streams.SelectedIndex == 0) stream_n = FFBatch.Properties.Strings.f_str;
            if (cb_streams.SelectedIndex == 1) stream_n = FFBatch.Properties.Strings.s_str;
            if (cb_streams.SelectedIndex == 1) stream_n = FFBatch.Properties.Strings.any_str;
        }

        private void chk_novideo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_novideo.CheckState == CheckState.Checked) remove_not_video = true;
            else remove_not_video = false;
        }

        private void btn_streams_Click(object sender, EventArgs e)
        {
        }

        private void CopyAction(object sender, EventArgs e)
        {
            if (LB1.SelectedIndices.Count == 1) Clipboard.SetText(LB1.SelectedItems[0].SubItems[1].Text);
            AutoClosingMessageBox.Show(LB1.SelectedItems[0].SubItems[1].Text, "Value", 1000);
        }

        private void boton_ok_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        private void cb_value_f_DropDownClosed(object sender, EventArgs e)
        {
            if (cb_value_f.SelectedIndex != -1) cb_value_f.Text = cb_value_f.SelectedItem.ToString();
            else
            {
                cb_value_f.Text = String.Empty;
            }
        }

        private void btn_mediainfo_Click(object sender, EventArgs e)
        {
            String ffm = System.IO.Path.Combine(Application.StartupPath, "mediainfo.exe");
            if (!File.Exists(ffm))
            {
                MessageBox.Show(FFBatch.Properties.Strings.no_mediainfo, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lv1_item != String.Empty)
            {
                String file1 = System.IO.Path.Combine(Application.StartupPath + "\\", "mediainfo.exe");
                String fullPath = "\u0022" + lv1_item + "\u0022";
                String testPath = lv1_item;

                if (!File.Exists(testPath))
                {
                    MessageBox.Show(FFBatch.Properties.Strings.file_not_f, FFBatch.Properties.Strings.file_miss, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                var process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = file1;
                process.StartInfo.Arguments = fullPath;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                process.Start();

                String salida_n = "";

                Form frmInfo = new Form();
                frmInfo.Name = "Multimedia information";
                frmInfo.Text = "FFmpeg Batch A/V Converter";
                frmInfo.Icon = this.Icon;
                frmInfo.Height = 724;
                frmInfo.Width = 496;
                frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
                frmInfo.MaximizeBox = false;
                frmInfo.MinimizeBox = false;
                frmInfo.BackColor = this.BackColor;

                var fuente_list = new System.Drawing.Font("Microsoft Sans Serif", 9, FontStyle.Regular);

                LB1.Clear();
                LB1.Parent = frmInfo;
                LB1.ShowItemToolTips = true;
                LB1.Left = 14;
                LB1.Top = 56;
                LB1.Height = 591;
                LB1.Width = 447;
                //LB1.Font = fuente_list;
                LB1.View = View.Details;
                LB1.SmallImageList = img_streams;
                LB1.FullRowSelect = true;
                LB1.GridLines = true;
                LB1.Columns.Add("", 130);
                LB1.Columns.Add("", 295);
                LB1.HeaderStyle = ColumnHeaderStyle.None;
                LB1.LabelEdit = true;
                LB1.Refresh();

                ContextMenu Rtxt_menu = new ContextMenu();
                frmInfo.ContextMenu = Rtxt_menu;
                MenuItem CopyItem = new MenuItem("Copy");
                Rtxt_menu.MenuItems.Add(CopyItem);
                CopyItem.Click += new EventHandler(CopyAction);

                TextBox titulo = new TextBox();
                titulo.Parent = frmInfo;
                titulo.Top = 6;
                titulo.Left = 14;
                titulo.Width = 448;
                titulo.TabIndex = 0;
                var fuente = new System.Drawing.Font("Microsoft Sans Serif", 10, FontStyle.Bold);
                titulo.BackColor = this.BackColor;

                titulo.Font = fuente;
                titulo.BorderStyle = BorderStyle.Fixed3D;
                titulo.TextAlign = HorizontalAlignment.Center;
                titulo.ReadOnly = true;

                titulo.Text = FFBatch.Properties.Strings.multi_full;

                Button boton_ok = new Button();
                boton_ok.Parent = frmInfo;
                boton_ok.Left = 13;
                boton_ok.Top = 650;
                boton_ok.Width = 446;
                boton_ok.Height = 27;
                boton_ok.Text = FFBatch.Properties.Strings.close_win;
                boton_ok.Click += new EventHandler(boton_ok_Click);

                String fichero = Path.GetFileName(lv1_item);
                TextBox titulo2 = new TextBox();
                titulo2.Parent = frmInfo;
                titulo2.Top = 34;
                titulo2.Left = 14;
                titulo2.Width = 440;
                titulo2.BackColor = this.BackColor;

                titulo2.BorderStyle = BorderStyle.None;
                titulo2.TextAlign = HorizontalAlignment.Center;
                titulo2.ReadOnly = true;

                titulo2.Text = (fichero);
                int indx = 0;
                List<string> salida1 = new List<string>();
                var font_item = new System.Drawing.Font("Microsoft Sans Serif", 8, FontStyle.Bold);

                while (!process.StandardOutput.EndOfStream)
                {
                    salida_n = process.StandardOutput.ReadLine();
                    salida1.Add(salida_n);
                }
                process.WaitForExit();
                LB1.BeginUpdate();
                foreach (String salida2 in salida1)
                {
                    int derecha = 0;

                    if (!salida2.Contains("  : "))
                    {
                        LB1.Items.Add(salida2.ToUpper());

                        LB1.Items[indx].Font = font_item;
                        LB1.Items[indx].ForeColor = Color.DarkBlue;
                        if (salida2 != String.Empty)
                        {
                            LB1.Items[indx].SubItems[0].BackColor = Color.FromArgb(255, 220, 238, 255);
                            if (salida2.Contains("Video")) LB1.Items[indx].ImageIndex = 0;
                            if (salida2.Contains("Audio")) LB1.Items[indx].ImageIndex = 1;
                            if (salida2.Contains("Text")) LB1.Items[indx].ImageIndex = 2;
                            if (salida2.Contains("General"))
                            {
                                LB1.Items[indx].ImageIndex = 5;
                                LB1.Items[indx].SubItems[0].BackColor = Color.FromArgb(255, 255, 248, 220);
                            }
                        }

                        indx = indx + 1;
                    }
                    else

                    {
                        if (!salida2.Contains("SPF"))
                        {
                            LB1.Items.Add(salida2.Substring(0, salida2.LastIndexOf("  : ")).Replace("  ", ""));
                            derecha = salida2.Length - (salida2.LastIndexOf("  :"));
                            LB1.Items[indx].SubItems.Add(salida2.Substring(salida2.LastIndexOf("  :") + 4, derecha - 4).Replace("kb", "Kb"));
                            indx = indx + 1;
                        }
                    }
                }

                for (int x = 0; x < 2; x++)
                {
                    LB1.Items.RemoveAt(LB1.Items.Count - 1);
                }

                int duraciones = 0;
                String elemento = "";
                for (int i = 0; i < LB1.Items.Count; i++)
                {
                    elemento = LB1.Items[i].Text;

                    if (elemento.Contains("Duration"))
                    {
                        duraciones = duraciones + 1;

                        if (duraciones > 1)
                        {
                            LB1.Items.RemoveAt(i);
                        }
                    }
                }

                foreach (ListViewItem item in LB1.Items)
                {
                    if (item.Text == String.Empty)
                    {
                        item.Remove();
                    }
                }
                LB1.EndUpdate();
                frmInfo.StartPosition = FormStartPosition.CenterParent;
                this.Cursor = Cursors.Arrow;
                frmInfo.ShowDialog();
            }
            else

            {
                MessageBox.Show(FFBatch.Properties.Strings.no_item_sel, FFBatch.Properties.Strings.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Cursor = Cursors.Arrow;
        }

        private void btn_streams_Click_1(object sender, EventArgs e)
        {
            form5.lv1_item = lv1_item;
            form5.ShowDialog();
        }
    }

    public class AutoClosingMessageBox
    {
        private System.Threading.Timer _timeoutTimer;
        private string _caption;

        private AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }

        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }

        private void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }

        private const int WM_CLOSE = 0x0010;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }
}