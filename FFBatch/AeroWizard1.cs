using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard1 : Form
    {
        public AeroWizard1()
        {
            InitializeComponent();
        }

        private String fdur_txt = String.Empty;
        private String trailer_filters = String.Empty;
        private String temp_dur = String.Empty;
        public String curr_ff = String.Empty;
        public String trailer_vparam = String.Empty;
        public String trailer_aparam = String.Empty;
        public String pre_input = String.Empty;
        private Boolean encoder_supp = true;
        private Boolean warn_spf = true;
        private String auto_crop = String.Empty;
        public Boolean online_pr = false;
        public String sel_preset = "";
        private Boolean started_v = false;
        private Boolean internet_up = true;
        public Boolean start_no_files = false;
        public Boolean add_files = false;
        public Boolean add_folder = false;
        private Boolean preset_ok = false;
        private String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        public Boolean is_portable = false;
        private Boolean started_video = false;
        private Boolean existing_preset = false;
        private String params_result = String.Empty;
        private String ext_result = String.Empty;
        private String preset_name = String.Empty;
        private Boolean is_max = false;
        private Boolean audio_preset = false;
        private Boolean video_preset = false;
        private Boolean save_preset = false;
        private String video_encoder_param = String.Empty;
        private String libx264_params = String.Empty;
        private String libsvtav1_params = String.Empty;        
        private String libx265_params = String.Empty;
        private String h264_qsv_params = String.Empty;
        private String hevc_qsv_params = String.Empty;
        private String h264_nvenc_params = String.Empty;
        private String hevc_nvenc_params = String.Empty;
        private String av1_nvenc_params = String.Empty;
        private String h264_amf_params = String.Empty;
        private String hevc_amf_params = String.Empty;
        private String libvpx_vp9_params = String.Empty;
        private String prores_ks_params = String.Empty;
        private String dnxhd_params = String.Empty;
        private String dnxhr_params = String.Empty;
        private String audio_encoder_param = String.Empty;
        private String pcm16_params = String.Empty;
        private String pcm24_params = String.Empty;
        private String flac_params = String.Empty;
        private String aac_params = String.Empty;
        private String ac3_params = String.Empty;
        private String eac3_params = String.Empty;
        private String mp3_params = String.Empty;
        private String vorbis_params = String.Empty;
        private String opus_params = String.Empty;
        private String aspect = String.Empty;
        private Boolean two_pass = false;
        private Boolean silence = false;
        private Boolean img_v = false;
        private Boolean tried_ok = false;
        public String lv1_item = String.Empty;
        public String lv1_dur = String.Empty;
        public Boolean no_two = false;
        public Boolean no_silence = false;
        public Boolean no_img_v = false;
        public Boolean w_images = false;
        public Boolean w_split = false;
        public Boolean images_v = false;
        public Boolean images_to_v = false;

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {            
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (wizardControl1.SelectedPage == wz_end) btn_status.PerformClick();

            if (started_video == false)
            {
                String[] video_encoders = new string[] { "copy", "libx264", "libx265", "libsvtav1", "h264_qsv", "hevc_qsv", "h264_nvenc", "hevc_nvenc", "av1_nvenc", "h264_amf", "hevc_amf", "libvpx-vp9", "prores_ks", "dnxhd", "dnxhr" };
                foreach (String item in video_encoders) Combo_encoders.Items.Add(item);

                combo_crf_mode.Items.Add("Constant Rate Factor");
                combo_crf_mode.Items.Add("Constant Bitrate");
                Combo_encoders.SelectedIndex = 0;
                cb_framerate.Items.Add(FFBatch.Properties.Strings.custom);
                cb_framerate.Items.Add("23.976 (Film)");
                cb_framerate.Items.Add("25 (PAL)");
                cb_framerate.Items.Add("29.97 (NTSC)");
                cb_framerate.Items.Add("30");
                cb_framerate.Items.Add("50 (PAL)");
                cb_framerate.Items.Add("59.93 (NTSC)");
                cb_framerate.Items.Add("60");
                String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                String[] sizes = new string[] { FFBatch.Properties.Strings.custom, "1920x1080", "1920x800", "1440x1080", "1280x720", "1024x768", "1024x576", "960x540", "800x600", "800x480", "720x576", "720x540", "720x480", "640x480", "640x360" };
                foreach (String v_sizes in sizes) cb_resize.Items.Add(v_sizes);
                String[] crops = new string[] { FFBatch.Properties.Strings.custom, "1:1", "4:3", "16:9", "1440x1080", "1280x720", "1024x768", "1024x576", "800x600", "800x480", "720x576", "720x480" };
                foreach (String v_crops in crops) cb_crop.Items.Add(v_crops);

                for (int i = 1; i < 51; i++)
                {
                    cb_cq_vp9.Items.Add(i.ToString());
                }

                //ProRes
                cb_profile_prores.Items.Add("proxy");
                cb_profile_prores.Items.Add("lt");
                cb_profile_prores.Items.Add("standard");
                cb_profile_prores.Items.Add("hq");
                cb_profile_prores.Items.Add("4444");
                cb_profile_prores.SelectedIndex = 2;
                cb_pixel_prores.Items.Add("yuv422p10");
                cb_pixel_prores.Items.Add("yuv422p10le");
                cb_pixel_prores.Items.Add("yuv444p10");
                cb_pixel_prores.Items.Add("yuv444p10le");
                cb_pixel_prores.Items.Add("yuva444p10");
                cb_pixel_prores.Items.Add("yuva444p10le");
                cb_pixel_prores.SelectedIndex = 1;
                cb_vendor_prores.Items.Add(FFBatch.Properties.Strings.Default);
                cb_vendor_prores.Items.Add("ap10");
                cb_vendor_prores.SelectedIndex = 1;
                cb_bits_prores.Items.Add(FFBatch.Properties.Strings.Default);
                cb_bits_prores.Items.Add("8000");
                cb_bits_prores.SelectedIndex = 1;
                //End ProRes

                cb_rotate.Items.Clear();
                cb_rotate.Items.Add(FFBatch.Properties.Strings.none);
                cb_rotate.Items.Add(FFBatch.Properties.Strings.rotate1);
                cb_rotate.Items.Add(FFBatch.Properties.Strings.rotate2);
                cb_rotate.Items.Add(FFBatch.Properties.Strings.rotate3);
                cb_rotate.Items.Add(FFBatch.Properties.Strings.rot_flip);
            }
            pic_rotate.Image = img_rotate.Images[0];
            started_video = true;
        }

        private void Combo_encoders_SelectedIndexChanged(object sender, EventArgs e)
        {
            btn_tips_1.Visible = false;
            lbl_amd.Visible = false;
            cb_q_amd.Visible = false;
            label3.Text = "";

            if (Combo_encoders.SelectedItem.ToString() == "dnxhd")
            {
                txt_video_current.Text = "Avid DNxHD";
                n_crf.Minimum = 36;
                n_crf.Maximum = 880;
                track_q_v.Minimum = 36;
                track_q_v.Maximum = 880;
                track_q_v.TickFrequency = 20;
                n_crf.Value = 175;
                label2.Visible = false;
                label3.Visible = true;
                label3.Text = "Mbps";
                label4.Visible = false;
                label5.Visible = false;
                label6.Text = String.Empty;
                label7.Text = String.Empty;
                label8.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = true;
                label17.Visible = true;
                label23.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;
                chk_vp9_threads.Visible = false;
                cb_profile_prores.Visible = false;
                cb_pixel_prores.Visible = false;
                cb_vendor_prores.Visible = false;
                cb_bits_prores.Visible = false;
                cb_cq_vp9.Visible = false;
                cb_pixel.Visible = false;
                cb_framerate.Visible = true;
                n_framerate.Visible = true;
                cb_preset.Visible = false;
                cb_profile.Visible = false;
                cb_level.Visible = false;
                cb_tune.Visible = false;
                combo_crf_mode.Visible = false;
                track_q_v.Visible = true;
                track_q_v.Enabled = true;
                n_crf.Visible = true;
                wz1_1.Suppress = false;
                cb_profile_dnxhr.Visible = false;
                cb_pixel.Items.Clear();
                String[] v_pixels = new string[] { "yuv422", "yuv444p", "yuyv422p", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "gbrp10" };
                foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                cb_pixel.Visible = true;
                btn_ref.Visible = true;
            }

            if (Combo_encoders.SelectedItem.ToString() == "dnxhr")
            {
                txt_video_current.Text = "Avid DNxHR";

                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Text = String.Empty;
                label7.Text = String.Empty;
                label8.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = true;
                label17.Visible = true;
                label23.Visible = false;
                label25.Visible = true;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;
                chk_vp9_threads.Visible = false;
                cb_profile_prores.Visible = false;
                cb_pixel_prores.Visible = false;
                cb_vendor_prores.Visible = false;
                cb_bits_prores.Visible = false;
                cb_cq_vp9.Visible = false;
                cb_pixel.Visible = false;
                cb_framerate.Visible = true;
                n_framerate.Visible = true;
                cb_preset.Visible = false;
                cb_profile.Visible = false;
                cb_level.Visible = false;
                cb_tune.Visible = false;
                combo_crf_mode.Visible = false;
                track_q_v.Visible = false;
                track_q_v.Enabled = false;
                n_crf.Visible = false;
                wz1_1.Suppress = false;
                cb_pixel.Visible = true;
                cb_profile_dnxhr.Visible = true;
                cb_profile_dnxhr.SelectedIndex = 0;
                cb_profile_dnxhr.Visible = true;
                cb_pixel.Items.Clear();
                String[] v_pixels = new string[] { "yuv422", "yuv444p", "yuyv422p", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "gbrp10" };
                foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                cb_pixel.Visible = true;
                btn_ref.Visible = false;
            }

            if (Combo_encoders.SelectedItem.ToString() == "prores_ks")
            {
                txt_video_current.Text = "Apple ProRes";
                label2.Visible = false;
                label3.Visible = true;
                label3.Text = String.Empty;
                label4.Visible = false;
                label5.Visible = false;
                label6.Text = String.Empty;
                label7.Text = String.Empty;
                label8.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label23.Visible = false;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label28.Visible = true;
                chk_vp9_threads.Visible = false;
                cb_profile_prores.Visible = true;
                cb_pixel_prores.Visible = true;
                cb_vendor_prores.Visible = true;
                cb_bits_prores.Visible = true;
                cb_cq_vp9.Visible = false;
                cb_pixel.Visible = false;
                cb_framerate.Visible = false;
                n_framerate.Visible = false;
                cb_preset.Visible = false;
                cb_profile.Visible = false;
                cb_level.Visible = false;
                cb_tune.Visible = false;
                combo_crf_mode.Visible = false;
                track_q_v.Visible = false;
                track_q_v.Enabled = false;
                n_crf.Visible = false;
                wz1_1.Suppress = false;
                cb_pixel.Visible = false;
                cb_profile_dnxhr.Visible = false;
                btn_ref.Visible = false;
            }

            if (Combo_encoders.SelectedItem.ToString() == "libvpx-vp9")
            {
                txt_video_current.Text = "VP9 Video";
                label6.Text = FFBatch.Properties.Strings.hrq;
                label7.Text = FFBatch.Properties.Strings.lrq;
                label2.Visible = true;
                label3.Visible = true;
                label3.Text = String.Empty;
                label4.Visible = false;
                label5.Visible = false;
                label8.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = true;
                label17.Visible = true;
                label23.Visible = false;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;
                chk_vp9_threads.Visible = true;
                cb_profile_prores.Visible = false;
                cb_pixel_prores.Visible = false;
                cb_vendor_prores.Visible = false;
                cb_bits_prores.Visible = false;
                cb_cq_vp9.Visible = false;
                cb_pixel.Visible = false;
                cb_framerate.Visible = true;
                n_framerate.Visible = true;
                cb_preset.Visible = false;
                cb_profile.Visible = false;
                cb_level.Visible = false;
                cb_tune.Visible = false;
                combo_crf_mode.Visible = true;
                combo_crf_mode.SelectedIndex = 0;
                track_q_v.Visible = true;
                track_q_v.Enabled = true;
                n_crf.Visible = true;
                n_crf.Maximum = 51;
                n_crf.Minimum = 1;
                track_q_v.Minimum = 1;
                track_q_v.Maximum = 50;
                track_q_v.Value = 20;
                n_crf.Value = 20;
                wz1_1.Suppress = false;
                cb_pixel.Items.Clear();
                String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv420p10le", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                cb_pixel.Visible = true;
                cb_profile_dnxhr.Visible = false;
                btn_ref.Visible = false;
            }

            if (Combo_encoders.SelectedItem.ToString() == "libx264" || Combo_encoders.SelectedItem.ToString() == "libx265" || Combo_encoders.SelectedItem.ToString() == "h264_nvenc" || Combo_encoders.SelectedItem.ToString() == "hevc_nvenc" || Combo_encoders.SelectedItem.ToString() == "h264_qsv" || Combo_encoders.SelectedItem.ToString() == "hevc_qsv" || Combo_encoders.SelectedItem.ToString() == "h264_amf" || Combo_encoders.SelectedItem.ToString() == "hevc_amf" || Combo_encoders.SelectedItem.ToString() == "av1_nvenc" || Combo_encoders.SelectedItem.ToString() == "libsvtav1")
            {
                track_q_v.Enabled = true;
                label6.Text = FFBatch.Properties.Strings.hrq;
                label7.Text = FFBatch.Properties.Strings.lrq;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label8.Visible = true;
                label25.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                label28.Visible = false;
                chk_vp9_threads.Visible = false;
                cb_profile_prores.Visible = false;
                cb_pixel_prores.Visible = false;
                cb_vendor_prores.Visible = false;
                cb_bits_prores.Visible = false;
                combo_crf_mode.Visible = true;
                n_crf.Visible = true;
                n_crf.Minimum = 1;
                n_crf.Maximum = 51;
                label23.Visible = false;
                cb_cq_vp9.Visible = false;
                label16.Visible = true;
                label17.Visible = true;
                cb_framerate.Visible = true;
                n_framerate.Visible = true;
                cb_pixel.Visible = true;
                cb_level.Visible = true;

                track_q_v.Visible = true;
                cb_profile.Visible = true;
                cb_preset.Visible = true;
                cb_tune.Visible = true;
                wz1_1.Suppress = false;
                cb_profile_dnxhr.Visible = false;
                btn_ref.Visible = false;

                combo_crf_mode.SelectedIndex = 0;
                if (Combo_encoders.SelectedItem.ToString() == "libx264")
                {
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv420p10le", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                    txt_video_current.Text = "AVC H.264";
                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();

                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "baseline", "main", "high", "high10", "high422", "high444" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "1.1", "1.2", "1.3", "2", "2.1", "2.2", "3", "3.1", "3.2", "4", "4.1", "4.2", "5", "5.1", "5.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "ultrafast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);                    
                    n_crf.Value = 23;
                }

                if (Combo_encoders.SelectedItem.ToString() == "libsvtav1")
                {                    
                    cb_pixel.Items.Clear();
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "Aomedia SVT-AV1";
                    String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv420p10le", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);                    
                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();

                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "main", "high", "professional" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "2.0", "2.1", "2.2", "3.0", "3.1", "3.2", "3.3", "4", "4.1", "4.2", "4.3", "5.1", "5.2", "5.3", "6.0", "6.1", "6.2", "6.3", "7.1", "7.2", "7.3" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "1 (slow)", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12 (fast)" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 28;
                }

                if (Combo_encoders.SelectedItem.ToString() == "av1_nvenc")
                {
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "Video 1 (AV1 Nvidia NVENC)";
                    label14.Visible = false;
                    label15.Visible = false;
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "nv12", "p010le", "yuv444p", "yuv444p16le", "bgr0", "rgb0", "cuda" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "baseline", "main", "high", "high444p" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "1.1", "1.2", "1.3", "2", "2.1", "2.2", "3", "3.1", "3.2", "4", "4.1", "4.2", "5", "5.1" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "fast", "medium", "slow", "hp", "bd", "ll", "llhq", "llhp", "lossless", "losslesshp" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 28;
                }

                if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc")
                {
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "AVC H.264 (Nvidia NVENC)";
                    label14.Visible = false;
                    label15.Visible = false;
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "nv12", "p010le", "yuv444p", "yuv444p16le", "bgr0", "rgb0", "cuda" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "baseline", "main", "high", "high444p" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "1.1", "1.2", "1.3", "2", "2.1", "2.2", "3", "3.1", "3.2", "4", "4.1", "4.2", "5", "5.1" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "fast", "medium", "slow", "hp", "bd", "ll", "llhq", "llhp", "lossless", "losslesshp" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 24;
                }
                if (Combo_encoders.SelectedItem.ToString() == "hevc_nvenc")
                {
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "HEVC H.265 (Nvidia NVENC)";
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "nv12", "p010le", "yuv444p", "yuv444p16le", "bgr0", "rgb0", "cuda" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "baseline", "main", "high", "high444p" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "2", "2.1", "3", "3.1", "4", "4.1", "5", "5.1", "5.2", "6", "6.1", "6.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "fast", "medium", "slow", "hp", "bd", "ll", "llhq", "llhp", "lossless", "losslesshp" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 25;
                }

                if (Combo_encoders.SelectedItem.ToString() == "h264_qsv")
                {
                    txt_video_current.Text = "AVC H.264 (Intel QuickSync)";
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "nv12", "p010le" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "baseline", "main", "high" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "1.1", "1.2", "1.3", "2", "2.1", "2.2", "3", "3.1", "3.2", "4", "4.1", "4.2", "5", "5.1", "5.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 23;
                }

                if (Combo_encoders.SelectedItem.ToString() == "hevc_qsv")
                {
                    txt_video_current.Text = "HEVC H.265 (Intel QuickSync)";

                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "nv12", "p010le" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    String[] h264_tunes = new string[] { "none", "film", "animation", "grain", "stillimage", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "main", "main10", "mainsp" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "2", "2.1", "3", "3.1", "4", "4.1", "5", "5.1", "5.2", "6", "6.1", "6.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);
                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    n_crf.Value = 25;
                }

                if (Combo_encoders.SelectedItem.ToString() == "libx265")
                {
                    txt_video_current.Text = "HEVC H.265";

                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "ultrafast", "veryfast", "faster", "fast", "medium", "slow", "slower", "veryslow" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    String[] h264_tunes = new string[] { "grain", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "main", "main10", "mainstillpicture" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "2", "2.1", "3", "3.1", "4", "4.1", "5", "5.1", "5.2", "6", "6.1", "6.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);

                    n_crf.Value = 25;
                }

                if (Combo_encoders.SelectedItem.ToString() == "h264_amf")
                {
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "AMD VCE H.264";
                    lbl_amd.Visible = true;
                    cb_q_amd.Visible = true;

                    if (cb_q_amd.SelectedIndex == -1) cb_q_amd.SelectedIndex = 0;
                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "transcoding", "ultralowlatency", "lowlatency", "webcam" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    String[] h264_tunes = new string[] { "grain", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "main", "high", "constrained_baseline", "constrained_high" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "2", "2.1", "3", "3.1", "4", "4.1", "5", "5.1", "5.2", "6", "6.1", "6.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);

                    n_crf.Value = 20;
                }

                if (Combo_encoders.SelectedItem.ToString() == "hevc_amf")
                {
                    btn_tips_1.Visible = true;
                    txt_video_current.Text = "AMD VCE H.265";
                    lbl_amd.Visible = true;
                    cb_q_amd.Visible = true;
                    if (cb_q_amd.SelectedIndex == -1) cb_q_amd.SelectedIndex = 0;

                    cb_pixel.Items.Clear();
                    String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuv420p10le", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                    foreach (String item in v_pixels) cb_pixel.Items.Add(item);

                    cb_preset.Items.Clear();
                    String[] h264_presets = new string[] { "transcoding", "ultralowlatency", "lowlatency", "webcam" };
                    foreach (String item in h264_presets) cb_preset.Items.Add(item);
                    String[] h264_tunes = new string[] { "grain", "fastdecode", "zerolatency" };
                    cb_tune.Items.Clear();
                    foreach (String item in h264_tunes) cb_tune.Items.Add(item);
                    String[] h264_profiles = new string[] { "main" , "high" };
                    cb_profile.Items.Clear();
                    foreach (String item in h264_profiles) cb_profile.Items.Add(item);
                    String[] h264_levels = new string[] { "1", "2", "2.1", "3", "3.1", "4", "4.1", "5", "5.1", "5.2", "6", "6.1", "6.2" };
                    cb_level.Items.Clear();
                    foreach (String item in h264_levels) cb_level.Items.Add(item);

                    n_crf.Value = 25;
                }

                track_q_v.Value = Convert.ToInt32(n_crf.Value);
            }

            if (Combo_encoders.SelectedItem.ToString() == "copy")
            {
                wz1_1.Suppress = true;
                txt_video_current.Text = "Video stream copy";
                track_q_v.Enabled = false;
                label2.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label6.Text = String.Empty;
                label7.Text = String.Empty;
                label8.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label23.Visible = false;
                chk_vp9_threads.Visible = false;
                cb_profile_prores.Visible = false;
                cb_pixel_prores.Visible = false;
                cb_vendor_prores.Visible = false;
                cb_bits_prores.Visible = false;
                cb_cq_vp9.Visible = false;
                cb_framerate.Visible = false;
                n_framerate.Visible = false;
                cb_pixel.Visible = false;
                cb_tune.Visible = false;
                combo_crf_mode.Visible = false;
                cb_level.Visible = false;
                n_crf.Visible = false;
                track_q_v.Visible = false;
                cb_profile.Visible = false;
                cb_preset.Visible = false;
                cb_preset.Visible = false;
                cb_profile.Visible = false;
                cb_profile_dnxhr.Visible = false;
                btn_ref.Visible = false;
            }
        }

        private void track_q_v_Scroll(object sender, EventArgs e)
        {            
            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("libvpx-vp9"))
            {
            //    if (track_q_v.Value <= 36)
            //    {
            //        n_crf.Value = 36;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }

            //    if (track_q_v.Value > 36 && track_q_v.Value <= 42)
            //    {
            //        n_crf.Value = 42;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 42 && track_q_v.Value <= 45)
            //    {
            //        n_crf.Value = 45;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 45 && track_q_v.Value <= 60)
            //    {
            //        n_crf.Value = 60;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 45 && track_q_v.Value <= 60)
            //    {
            //        n_crf.Value = 60;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 60 && track_q_v.Value <= 63)
            //    {
            //        n_crf.Value = 63;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 63 && track_q_v.Value <= 75)
            //    {
            //        n_crf.Value = 75;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 75 && track_q_v.Value <= 80)
            //    {
            //        n_crf.Value = 80;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 80 && track_q_v.Value <= 84)
            //    {
            //        n_crf.Value = 84;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 84 && track_q_v.Value <= 90)
            //    {
            //        n_crf.Value = 90;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 90 && track_q_v.Value <= 110)
            //    {
            //        n_crf.Value = 110;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 110 && track_q_v.Value <= 115)
            //    {
            //        n_crf.Value = 115;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 115 && track_q_v.Value <= 120)
            //    {
            //        n_crf.Value = 120;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 120 && track_q_v.Value <= 145)
            //    {
            //        n_crf.Value = 145;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 145 && track_q_v.Value <= 175)
            //    {
            //        n_crf.Value = 175;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 175 && track_q_v.Value <= 185)
            //    {
            //        n_crf.Value = 185;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 185 && track_q_v.Value <= 220)
            //    {
            //        n_crf.Value = 220;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 220 && track_q_v.Value <= 240)
            //    {
            //        n_crf.Value = 240;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 240 && track_q_v.Value <= 290)
            //    {
            //        n_crf.Value = 290;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 290 && track_q_v.Value <= 350)
            //    {
            //        n_crf.Value = 350;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 350 && track_q_v.Value <= 365)
            //    {
            //        n_crf.Value = 365;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 365 && track_q_v.Value <= 390)
            //    {
            //        n_crf.Value = 390;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 390 && track_q_v.Value <= 440)
            //    {
            //        n_crf.Value = 440;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 440 && track_q_v.Value <= 730)
            //    {
            //        n_crf.Value = 730;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            //    if (track_q_v.Value > 730 && track_q_v.Value <= 880)
            //    {
            //        n_crf.Value = 880;
            //        track_q_v.Value = Convert.ToInt32(n_crf.Value);
            //        return;
            //    }
            }

            if (combo_crf_mode.SelectedIndex == 1)
            {
                n_crf.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_q_v.Value) / 100)) * 100;
                track_q_v.Value = Convert.ToInt32(n_crf.Value);
            }
            else
            {
                n_crf.Value = Convert.ToInt32(track_q_v.Value);
            }
        }

        private void n_crf_ValueChanged(object sender, EventArgs e)
        {
            track_q_v.Minimum = (int)n_crf.Minimum;
            track_q_v.Maximum = (int)n_crf.Maximum;
            track_q_v.Value = Convert.ToInt32(n_crf.Value);
        }

        private void combo_h264_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_crf_mode.SelectedIndex == 0)
            {
                label14.Text = String.Empty;
                label15.Text = String.Empty;
                track_q_v.Width = 437;
                n_crf.Minimum = 1;
                n_crf.Maximum = 51;
                track_q_v.Minimum = 1;
                track_q_v.Maximum = 51;
                track_q_v.TickFrequency = 1;
                track_q_v.Value = 23;
                n_crf.Width = 40;
                n_crf.Left = 442;                
                cb_cq_vp9.Visible = false;
                label23.Visible = false;

                if (Combo_encoders.SelectedItem.ToString() == "libx264") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "libx265") n_crf.Value = 23;
                if (Combo_encoders.SelectedItem.ToString() == "libvpx-vp9") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_nvenc") n_crf.Value = 25;
                if (Combo_encoders.SelectedItem.ToString() == "h264_qsv") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_qsv") n_crf.Value = 25;
                if (Combo_encoders.SelectedItem.ToString() == "libsvtav1") n_crf.Value = 30;
                if (Combo_encoders.SelectedItem.ToString() == "av1_nvenc") n_crf.Value = 30;

                n_crf.Increment = 1;
                label3.Text = String.Empty;
                label6.Visible = true;
                label7.Visible = true;
                label6.Text = FFBatch.Properties.Strings.hrq;
                label7.Text = FFBatch.Properties.Strings.lrq;
            }
            if (combo_crf_mode.SelectedIndex == 1)
            {
                track_q_v.Width = 422;
                n_crf.Minimum = 100;
                n_crf.Maximum = 50000;
                track_q_v.Minimum = 100;
                track_q_v.Maximum = 50000;
                track_q_v.TickFrequency = 1000;
                track_q_v.Value = 1000;
                n_crf.Width = 60;
                n_crf.Left = 428;                
                n_crf.Value = 1000;
                label3.Text = "Kbps";
                label6.Text = String.Empty;
                label7.Text = String.Empty;

                if (Combo_encoders.SelectedItem.ToString() == "libx264") n_crf.Value = 4000;
                if (Combo_encoders.SelectedItem.ToString() == "libx265") n_crf.Value = 2000;
                if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc") n_crf.Value = 6000;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_nvenc") n_crf.Value = 3000;
                if (Combo_encoders.SelectedItem.ToString() == "h264_qsv") n_crf.Value = 6000;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_qsv") n_crf.Value = 3000;
                if (Combo_encoders.SelectedItem.ToString() == "libsvtav1") n_crf.Value = 2000;
                if (Combo_encoders.SelectedItem.ToString() == "av1_nvenc") n_crf.Value = 2000;
                if (Combo_encoders.SelectedItem.ToString() == "libvpx-vp9")
                {
                    n_crf.Value = 2000;
                    cb_cq_vp9.Visible = true;
                    cb_cq_vp9.SelectedIndex = 19;
                    label23.Visible = true;
                }

                track_q_v.Value = Convert.ToInt32(n_crf.Value);
            }
        }

        public Boolean wiz_two_pass
        {
            get { return two_pass; }
            set { two_pass = value; }
        }

        public Boolean wiz_silence
        {
            get { return silence; }
            set { silence = value; }
        }

        public Boolean wiz_img_v
        {
            get { return img_v; }
            set { img_v = value; }
        }

        public Boolean wiz_save_preset
        {
            get { return save_preset; }
            set { save_preset = value; }
        }

        public string wiz_params
        {
            get { return params_result; }
            set { params_result = value; }
        }

        public string wiz_ext
        {
            get { return ext_result; }
            set { ext_result = value; }
        }

        public string wiz_preset
        {
            get { return preset_name; }
            set { preset_name = value; }
        }

        private void wz2_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            try { cb_audio_encoder.SelectedIndex = Properties.Settings.Default.wiz_aud; } catch { }
            cb_channels.Items.Clear();

            cb_channels.Items.Add("Source");
            cb_channels.Items.Add("Stereo");
            cb_channels.Items.Add("Mono");
            cb_channels.SelectedIndex = 0;
            cb_sample_rate.Items.Add("Source");
            cb_sample_rate.Items.Add("44.1K");
            cb_sample_rate.Items.Add("48K");
            cb_sample_rate.Items.Add("88.2K");
            cb_sample_rate.Items.Add("96K");
            cb_sample_rate.Items.Add("192K");
            cb_sample_rate.SelectedIndex = 0;
            String[] audio_cutoff = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
            foreach (String item in audio_cutoff) cb_cutoff.Items.Add(item);
            String[] opus_vbr = new string[] { "10 (HQ)", "9", "8", "7", "6", "5", "4", "3", "2", "1", "0 (LQ)" };
            foreach (String item in opus_vbr) cb_opus_vbr.Items.Add(item);
            if (trailer_aparam.Length > 0 && chk_no_aud_tr.Checked)
            {
                cb_audio_encoder.SelectedItem = cb_audio_encoder.FindString("none");
                wizardControl1.NextPage();
            }
        }

        private void cb_audio_encoder_SelectedIndexChanged(object sender, EventArgs e)
        {
            label11.Top = 204;
            cb_channels.Top = 201;

            if (cb_audio_encoder.SelectedItem.ToString() == "copy" || cb_audio_encoder.SelectedItem.ToString() == "none")
            {
                label31.Visible = false;
                label32.Visible = false;
                n_speed2.Visible = false;
                chk_normalize.Visible = false;
                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label22.Visible = false;
                label29.Visible = false;
                cb_chunk_size.Visible = false;
                if (cb_audio_encoder.SelectedItem.ToString() == "copy") txt_current_audio.Text = FFBatch.Properties.Strings.aud_str_c;
                else txt_current_audio.Text = FFBatch.Properties.Strings.exc_aud;
                cb_opus_vbr.Visible = false;
                chk_vbr_opus.Visible = false;
                cb_cutoff.Visible = false;
                cb_sample_rate.Visible = false;
                cb_channels.Visible = false;
                cb_bitrate_mode.Visible = false;
                track_bits_audio.Visible = false;
                n_bit_audio.Visible = false;
            }
            if (cb_audio_encoder.SelectedItem.ToString() == "pcm16" || cb_audio_encoder.SelectedItem.ToString() == "pcm24" || cb_audio_encoder.SelectedItem.ToString() == "flac")
            {
                if (n_speed2.Value != 0)
                {
                    label31.Visible = true;
                    label32.Visible = true;
                    n_speed2.Visible = true;
                }

                chk_normalize.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label22.Visible = false;
                cb_sample_rate.Items.Clear();
                cb_sample_rate.Items.Add("Source");
                cb_sample_rate.Items.Add("44.1K");
                cb_sample_rate.Items.Add("48K");
                cb_sample_rate.Items.Add("88.2K");
                cb_sample_rate.Items.Add("96K");
                cb_sample_rate.Items.Add("192K");
                cb_sample_rate.SelectedIndex = 0;

                if (cb_audio_encoder.SelectedItem.ToString() == "pcm16" || cb_audio_encoder.SelectedItem.ToString() == "pcm24")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }

                    chk_normalize.Visible = true;
                    label11.Top = 96;
                    cb_channels.Top = 93;
                    label29.Visible = true;
                    cb_chunk_size.Items.Add("Source");
                    cb_chunk_size.Items.Add("16K");
                    cb_chunk_size.Items.Add("32K");
                    cb_chunk_size.Items.Add("48K");
                    cb_chunk_size.Items.Add("64K");
                    cb_chunk_size.SelectedIndex = 0;
                    cb_chunk_size.Visible = true;
                }
                txt_current_audio.Text = "PCM (Lossless)";
                cb_opus_vbr.Visible = false;
                chk_vbr_opus.Visible = false;
                cb_cutoff.Visible = false;
                n_bit_audio.Visible = false;
                cb_sample_rate.Visible = true;
                cb_channels.Visible = true;
                cb_bitrate_mode.Visible = false;
                track_bits_audio.Visible = false;

                if (cb_audio_encoder.SelectedItem.ToString() == "flac")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }

                    chk_normalize.Visible = true;
                    label10.Visible = true;
                    label11.Visible = true;
                    label12.Visible = true;
                    label13.Visible = true;
                    label14.Visible = true;
                    label15.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    txt_current_audio.Text = "FLAC (Lossless)";
                    cb_opus_vbr.Visible = false;
                    chk_vbr_opus.Visible = false;
                    label15.Text = FFBatch.Properties.Strings.low_size;
                    label14.Text = FFBatch.Properties.Strings.big_size;
                    n_bit_audio.Visible = true;
                    cb_sample_rate.Visible = true;
                    cb_channels.Visible = true;
                    cb_bitrate_mode.Visible = true;
                    track_bits_audio.Visible = true;
                    cb_bitrate_mode.Items.Clear();
                    track_bits_audio.Minimum = 0;
                    track_bits_audio.Maximum = 12;
                    track_bits_audio.Value = 5;
                    n_bit_audio.Minimum = 0;
                    n_bit_audio.Maximum = 12;
                    n_bit_audio.Value = 4;
                    n_bit_audio.Increment = 1;
                    label13.Text = "";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("VBR");
                    cb_bitrate_mode.SelectedIndex = 0;
                }
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "aac" || cb_audio_encoder.SelectedItem.ToString() == "ac3" || cb_audio_encoder.SelectedItem.ToString() == "e-ac3" || cb_audio_encoder.SelectedItem.ToString() == "mp3" || cb_audio_encoder.SelectedItem.ToString() == "vorbis" || cb_audio_encoder.SelectedItem.ToString() == "opus")
            {
                if (n_speed.Value == 0)
                {
                    label31.Visible = true;
                    label32.Visible = true;
                    n_speed2.Visible = true;
                }

                chk_normalize.Visible = true;
                label10.Visible = true;
                label11.Visible = true;
                label12.Visible = true;
                label13.Visible = true;
                label13.Text = "Kbps";

                label29.Visible = false;
                cb_chunk_size.Visible = false;
                cb_opus_vbr.Visible = false;
                chk_vbr_opus.Visible = false;

                n_bit_audio.Visible = true;
                cb_sample_rate.Visible = true;
                cb_channels.Visible = true;
                cb_bitrate_mode.Visible = true;
                track_bits_audio.Visible = true;
                cb_bitrate_mode.Items.Clear();
                cb_bitrate_mode.Items.Add("CBR");
                cb_sample_rate.Items.Clear();
                cb_sample_rate.Items.Add("Source");
                cb_sample_rate.Items.Add("44.1K");
                cb_sample_rate.Items.Add("48K");
                cb_sample_rate.SelectedIndex = 0;
                cb_cutoff.Items.Clear();
                String[] audio_cutoff = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
                foreach (String item in audio_cutoff) cb_cutoff.Items.Add(item);

                if (cb_audio_encoder.SelectedItem.ToString() == "e-ac3")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }
                    label22.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    cb_opus_vbr.Visible = false;
                    chk_vbr_opus.Visible = false;
                    cb_cutoff.Visible = true;
                    txt_current_audio.Text = "Dolby E-AC3";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 64;
                    n_bit_audio.Maximum = 5600;
                    n_bit_audio.Value = 640;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 175;
                    track_bits_audio.Value = 20;
                    cb_sample_rate.Items.Clear();
                    cb_sample_rate.Items.Add("Source");
                    cb_sample_rate.Items.Add("44.1K");
                    cb_sample_rate.Items.Add("48K");
                    cb_sample_rate.SelectedIndex = 0;
                    cb_cutoff.Items.Clear();
                    String[] audio_cutoff0 = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
                    foreach (String item in audio_cutoff0) cb_cutoff.Items.Add(item);
                }

                if (cb_audio_encoder.SelectedItem.ToString() == "ac3")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }

                    chk_normalize.Visible = true;
                    label22.Visible = true;
                    cb_cutoff.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;

                    txt_current_audio.Text = "Dolby AC3";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 64;
                    n_bit_audio.Maximum = 640;
                    n_bit_audio.Value = 192;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 20;
                    track_bits_audio.Value = 6;
                    cb_sample_rate.Items.Clear();
                    cb_sample_rate.Items.Add("Source");
                    cb_sample_rate.Items.Add("44.1K");
                    cb_sample_rate.Items.Add("48K");
                    cb_sample_rate.SelectedIndex = 0;
                    cb_cutoff.Items.Clear();
                    String[] audio_cutoff_f = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
                    foreach (String item in audio_cutoff_f) cb_cutoff.Items.Add(item);
                }

                if (cb_audio_encoder.SelectedItem.ToString() == "aac")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }

                    chk_normalize.Visible = true;
                    label22.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    cb_opus_vbr.Visible = false;
                    chk_vbr_opus.Visible = false;
                    cb_cutoff.Visible = true;

                    txt_current_audio.Text = "Advanced audio coding (AAC)";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    cb_bitrate_mode.Items.Add("VBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 64;
                    n_bit_audio.Maximum = 448;
                    n_bit_audio.Value = 128;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 14;
                    track_bits_audio.Value = 4;
                }
                if (cb_audio_encoder.SelectedItem.ToString() == "mp3")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }
                    
                    chk_normalize.Visible = true;
                    label22.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    cb_opus_vbr.Visible = false;
                    chk_vbr_opus.Visible = false;
                    cb_cutoff.Visible = true;

                    txt_current_audio.Text = "MPEG-1 Audio Layer III (MP3)";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    cb_bitrate_mode.Items.Add("VBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 64;
                    n_bit_audio.Maximum = 320;
                    n_bit_audio.Value = 192;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 10;
                    track_bits_audio.Value = 6;
                    cb_sample_rate.Items.Clear();
                    cb_sample_rate.Items.Add("Source");
                    cb_sample_rate.Items.Add("44.1K");
                    cb_sample_rate.Items.Add("48K");
                    cb_sample_rate.SelectedIndex = 0;
                    cb_cutoff.Items.Clear();
                    String[] audio_cutoff2 = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
                    foreach (String item in audio_cutoff2) cb_cutoff.Items.Add(item);
                }
                if (cb_audio_encoder.SelectedItem.ToString() == "vorbis")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }
                    
                    chk_normalize.Visible = true;
                    label22.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    cb_opus_vbr.Visible = false;
                    chk_vbr_opus.Visible = false;
                    cb_cutoff.Visible = true;

                    txt_current_audio.Text = "OGG Vorbis";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    cb_bitrate_mode.Items.Add("VBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 64;
                    n_bit_audio.Maximum = 480;
                    n_bit_audio.Value = 192;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 15;
                    track_bits_audio.Value = 6;
                    cb_sample_rate.Items.Clear();
                    cb_sample_rate.Items.Add("Source");
                    cb_sample_rate.Items.Add("44.1K");
                    cb_sample_rate.Items.Add("48K");
                    cb_sample_rate.SelectedIndex = 0;
                    cb_cutoff.Items.Clear();
                    String[] audio_cutoff3 = new string[] { "none", "20KHz", "19KHz", "18KHz", "17KHz", "16KHz", "15KHz", "14KHz", "13KHz", "12KHz", "11KHz", "10KHz" };
                    foreach (String item in audio_cutoff3) cb_cutoff.Items.Add(item);
                }
                if (cb_audio_encoder.SelectedItem.ToString() == "opus")
                {
                    if (n_speed.Value == 0)
                    {
                        label31.Visible = true;
                        label32.Visible = true;
                        n_speed2.Visible = true;
                    }

                    chk_normalize.Visible = true;
                    label22.Visible = true;
                    label29.Visible = false;
                    cb_chunk_size.Visible = false;
                    cb_cutoff.Visible = true;
                    cb_sample_rate.Items.Clear();
                    cb_sample_rate.Items.Add("Source");
                    cb_sample_rate.Items.Add("44.1K");
                    cb_sample_rate.Items.Add("48K");
                    cb_sample_rate.SelectedIndex = 0;
                    cb_cutoff.Items.Clear();
                    String[] audio_cutoff3 = new string[] { "none", "20KHz", "12KHz", "8KHz", "6KHz" };
                    foreach (String item in audio_cutoff3) cb_cutoff.Items.Add(item);
                    cb_opus_vbr.Visible = true;
                    chk_vbr_opus.Visible = true;
                    txt_current_audio.Text = "OPUS audio";
                    cb_bitrate_mode.Items.Clear();
                    cb_bitrate_mode.Items.Add("CBR");
                    label13.Text = "Kbps";
                    cb_bitrate_mode.SelectedIndex = 0;
                    n_bit_audio.Minimum = 16;
                    n_bit_audio.Maximum = 512;
                    n_bit_audio.Value = 192;
                    track_bits_audio.Minimum = 2;
                    track_bits_audio.Maximum = 32;
                    track_bits_audio.Value = 6;
                }
            }
        }
        private void n_bit_audio_ValueChanged(object sender, EventArgs e)
        {
            if (cb_audio_encoder.SelectedItem.ToString() == "flac") track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);

            if (cb_audio_encoder.SelectedIndex == 5 && cb_bitrate_mode.SelectedIndex == 0)
            {
                if (n_bit_audio.Value == 288 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 288 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                if (n_bit_audio.Value == 352 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 352 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                if (n_bit_audio.Value == 416 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 416 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 5 && cb_bitrate_mode.SelectedIndex == 1) track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);

            if (cb_audio_encoder.SelectedIndex == 6)
            {
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;

                if (n_bit_audio.Value == 288) n_bit_audio.Value = n_bit_audio.Value + 32;
                if (n_bit_audio.Value == 352) n_bit_audio.Value = n_bit_audio.Value + 32;
                if (n_bit_audio.Value == 416) n_bit_audio.Value = n_bit_audio.Value + 32;
                if (n_bit_audio.Value > 448) n_bit_audio.Value = 640;

                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 7)
            {
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 8 && cb_bitrate_mode.SelectedIndex == 0)
            {
                if (n_bit_audio.Value == 288 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 288 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }
            if (cb_audio_encoder.SelectedIndex == 8 && cb_bitrate_mode.SelectedIndex == 1) track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);

            if (cb_audio_encoder.SelectedIndex == 9 && cb_bitrate_mode.SelectedIndex == 0)
            {
                if (n_bit_audio.Value == 288 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 288 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                if (n_bit_audio.Value == 352 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 352 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                if (n_bit_audio.Value == 416 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 32;
                    is_max = true;
                }
                if (n_bit_audio.Value == 416 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 32;
                    is_max = false;
                }

                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 9 && cb_bitrate_mode.SelectedIndex == 1) track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);

            if (cb_audio_encoder.SelectedIndex == 10 && cb_bitrate_mode.SelectedIndex == 0)
            {
                if (n_bit_audio.Value == 288 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 16;
                    is_max = true;
                }
                if (n_bit_audio.Value == 288 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 16;
                    is_max = false;
                }

                if (n_bit_audio.Value == 352 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 16;
                    is_max = true;
                }
                if (n_bit_audio.Value == 352 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 16;
                    is_max = false;
                }

                if (n_bit_audio.Value == 416 && is_max == false)
                {
                    n_bit_audio.Value = n_bit_audio.Value + 16;
                    is_max = true;
                }
                if (n_bit_audio.Value == 416 && is_max == true)
                {
                    n_bit_audio.Value = n_bit_audio.Value - 16;
                    is_max = false;
                }

                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 16;
            }
        }

        private void track_bits_audio_Scroll(object sender, EventArgs e)
        {
            if (cb_audio_encoder.SelectedItem.ToString() == "flac")
            {
                n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value)));
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);
            }

            if (cb_audio_encoder.SelectedIndex == 7)
            {
                n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 32));
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 6)
            {
                n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 32));
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
            }

            if (cb_audio_encoder.SelectedIndex == 5)
            {
                if (cb_bitrate_mode.SelectedIndex == 0)
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 32));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
                }
                else
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value)));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);
                }
            }
            if (cb_audio_encoder.SelectedIndex == 8)
            {
                if (cb_bitrate_mode.SelectedIndex == 0)
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 32));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
                }
                else
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value)));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);
                }
            }

            if (cb_audio_encoder.SelectedIndex == 9)
            {
                if (cb_bitrate_mode.SelectedIndex == 0)
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 32));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 32;
                }
                else
                {
                    n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value)));
                    track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value);
                }
            }

            if (cb_audio_encoder.SelectedIndex == 10)
            {
                n_bit_audio.Value = Convert.ToInt32(Math.Floor(Convert.ToDouble(track_bits_audio.Value) * 16));
                track_bits_audio.Value = Convert.ToInt32(n_bit_audio.Value) / 16;
            }
        }

        private void cb_bitrate_mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_audio_encoder.SelectedItem.ToString() == "aac" && cb_bitrate_mode.SelectedIndex == 0)
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = false;
                label15.Visible = false;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 14;
                track_bits_audio.Value = 4;
                n_bit_audio.Minimum = 64;
                n_bit_audio.Maximum = 448;
                n_bit_audio.Value = 128;
                n_bit_audio.Increment = 32;
                label13.Text = "Kbps";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "aac" && cb_bitrate_mode.SelectedIndex == 1)
            {
                label14.Visible = true;
                label15.Visible = true;
                label15.Text = FFBatch.Properties.Strings.hrq;
                label14.Text = FFBatch.Properties.Strings.lrq;
                track_bits_audio.Minimum = 0;
                track_bits_audio.Maximum = 4;
                track_bits_audio.Value = 2;
                n_bit_audio.Minimum = 0;
                n_bit_audio.Maximum = 4;
                n_bit_audio.Value = 2;
                n_bit_audio.Increment = 1;
                label13.Text = "";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "mp3" && cb_bitrate_mode.SelectedIndex == 0)
            {
                label14.Visible = false;
                label15.Visible = false;
                n_bit_audio.Minimum = 64;
                n_bit_audio.Maximum = 320;
                n_bit_audio.Value = 192;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 10;
                track_bits_audio.Value = 6;
                n_bit_audio.Increment = 32;
                label13.Text = "Kbps";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "mp3" && cb_bitrate_mode.SelectedIndex == 1)
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label14.Text = FFBatch.Properties.Strings.hrq;
                label15.Text = FFBatch.Properties.Strings.lrq;
                track_bits_audio.Minimum = 0;
                track_bits_audio.Maximum = 9;
                track_bits_audio.Value = 3;
                n_bit_audio.Minimum = 0;
                n_bit_audio.Maximum = 9;
                n_bit_audio.Value = 3;
                n_bit_audio.Increment = 1;
                label13.Text = "";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "ac3")
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = false;
                label15.Visible = false;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 20;
                track_bits_audio.Value = 6;
                n_bit_audio.Minimum = 64;
                n_bit_audio.Maximum = 640;
                n_bit_audio.Value = 192;
                n_bit_audio.Increment = 32;
                label13.Text = "Kbps";
            }
            if (cb_audio_encoder.SelectedItem.ToString() == "e-ac3")
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = false;
                label15.Visible = false;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 175;
                track_bits_audio.Value = 20;
                n_bit_audio.Minimum = 64;
                n_bit_audio.Maximum = 5600;
                n_bit_audio.Value = 640;
                n_bit_audio.Increment = 32;
                label13.Text = "Kbps";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "vorbis" && cb_bitrate_mode.SelectedIndex == 0)
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = false;
                label15.Visible = false;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 15;
                track_bits_audio.Value = 6;
                n_bit_audio.Minimum = 64;
                n_bit_audio.Maximum = 480;
                n_bit_audio.Value = 192;
                n_bit_audio.Increment = 32;
                label13.Text = "Kbps";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "vorbis" && cb_bitrate_mode.SelectedIndex == 1)
            {
                label10.Visible = true;
                cb_sample_rate.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label15.Text = FFBatch.Properties.Strings.hrq;
                label14.Text = FFBatch.Properties.Strings.lrq;
                track_bits_audio.Minimum = 0;
                track_bits_audio.Maximum = 10;
                track_bits_audio.Value = 5;
                n_bit_audio.Minimum = 0;
                n_bit_audio.Maximum = 10;
                n_bit_audio.Value = 5;
                n_bit_audio.Increment = 1;
                label13.Text = "";
            }

            if (cb_audio_encoder.SelectedItem.ToString() == "flac")
            {
                label14.Visible = true;
                label15.Visible = true;
                label15.Text = FFBatch.Properties.Strings.low_size;
                label14.Text = FFBatch.Properties.Strings.big_size;
                track_bits_audio.Minimum = 0;
                track_bits_audio.Maximum = 12;
                track_bits_audio.Value = 5;
                n_bit_audio.Minimum = 0;
                n_bit_audio.Maximum = 12;
                n_bit_audio.Value = 4;
                n_bit_audio.Increment = 1;
                label13.Text = "";
            }
            if (cb_audio_encoder.SelectedItem.ToString() == "opus")
            {
                label22.Visible = true;
                cb_cutoff.Visible = true;

                label13.Text = "Kbps";
                n_bit_audio.Minimum = 16;
                n_bit_audio.Maximum = 512;
                n_bit_audio.Value = 192;
                track_bits_audio.Minimum = 2;
                track_bits_audio.Maximum = 16;
                track_bits_audio.Value = 6;
                n_bit_audio.Increment = 16;
            }
        }

        private void wz0_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            wizardControl1.Pages[2].Suppress = true;

            if (radio_existing.Checked == true)
            {
                wz1.Suppress = true;
                wz2.Suppress = true;
                wz_0_1.Suppress = false;
                wz1_1.Suppress = true;
                wz2.Suppress = true;
                audio_preset = false;
                video_preset = false;
                existing_preset = true;
            }

            if (radio_audio.Checked == true)
            {
                wz1.Suppress = true;
                wz_0_1.Suppress = true;
                wz1_1.Suppress = true;
                wz2.Suppress = false;
                audio_preset = true;
                video_preset = false;
                existing_preset = false;
            }
            if (radio_video.Checked == true)
            {
                wz1.Suppress = false;
                wz2.Suppress = false;
                wz1_1.Suppress = false;
                wz_0_1.Suppress = true;
                audio_preset = false;
                video_preset = true;
                existing_preset = false;
            }
            if (radio_2pass.Checked == true)
            {
                wizardControl1.Visible = false;
                two_pass = true;
                if (pic_warn_two.Visible == true)
                {
                    no_two = true;
                }
                else
                {
                    no_two = false;
                    ActiveForm.Close();
                }
            }
            if (radio_silence.Checked == true)
            {
                wizardControl1.Visible = false;
                silence = true;
                if (pic_warn_silence.Visible == true)
                {
                    no_silence = true;
                }
                else
                {
                    no_silence = false;
                    ActiveForm.Close();
                }
            }

            if (radio_img_v.Checked == true)
            {
                wizardControl1.Visible = false;
                images_to_v = true;
                wiz_img_v = true;
                if (pic_warn_img_v.Visible == true)
                {
                    no_img_v = true;
                }
                else
                {
                    no_img_v = false;
                    ActiveForm.Close();
                }
            }
            if (radio_images.Checked == true)
            {
                wizardControl1.Visible = false;
                w_images = true;
                wizardControl1.Visible = false;
                ActiveForm.Close();
            }
            if (radio_split.Checked == true)
            {
                wizardControl1.Visible = false;
                w_split = true;
                wizardControl1.Visible = false;
                ActiveForm.Close();
            }

            if (radio_trailer.Checked == true)
            {
                wizardControl1.Pages[2].Suppress = false;
            }
        }

        private void wizardControl1_Finished(object sender, EventArgs e)
        {
            if (audio_preset == true) video_encoder_param = "-vn";

            wiz_params = video_encoder_param + " " + audio_encoder_param;
                        
            if (chk_mapall.CheckState == CheckState.Checked)
            {
                wiz_params = "-map 0 " + wiz_params;
            }
            if (chk_subs_copy.CheckState == CheckState.Checked)
            {
                wiz_params = wiz_params + " -c:s copy";
            }

            if (chk_samples.CheckState == CheckState.Checked)
            {
                wiz_params = wiz_params + " -t " + n_t_samples.Value.ToString(); ;
            }

            if (chk_save_pres.CheckState == CheckState.Checked && txt_preset_name.Text != String.Empty)
            {
                wiz_preset = txt_preset_name.Text;
                wiz_save_preset = true;
            }
            else
            {
                wiz_preset = String.Empty;
                wiz_save_preset = false;
            }

            wiz_ext = cb_container.Text;
        }

        private void wz1_1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("copy")) return;

            String filters = String.Empty;
            int n_fs = 0;
            if (cb_resize.SelectedIndex != -1) n_fs++;
            if (cb_crop.SelectedIndex != -1) n_fs++;
            if (cb_deint.SelectedIndex != -1) n_fs++;
            if (cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0) n_fs++;
            if (n_fs == 0 && n_speed.Value == 0 && trailer_vparam.Length == 0) return;
            if (trailer_vparam.Length > 0) n_fs++;
            if (n_fs == 1)
            {
                filters = " -vf " + '\u0022';
                //Resize
                if (cb_resize.SelectedIndex != -1)
                {
                    String pad1 = ":force_original_aspect_ratio=decrease,pad=";
                    String pad2 = "(ow-iw)/2:(oh-ih)/2";
                    
                    if (cb_resize.SelectedIndex == 0)
                    {
                        if (chk_pad.Checked) filters = filters + "scale=w=" + n_width.Value + ":h=" + n_height.Value + pad1 + n_width.Value + ":" + n_height.Value + ":" + pad2 + '\u0022';
                        else filters = filters + "scale=" + n_width.Value + ":" + n_height.Value + '\u0022';
                    }
                    else
                    {
                        String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                        if (chk_pad.Checked)
                        {
                            String r_size_p = cb_resize.SelectedItem.ToString().Replace("x", ":h=");
                            filters = filters + "scale=w=" + r_size_p + pad1 + r_size + ":" + pad2 + '\u0022';
                        }
                        else filters = filters + "scale=" + r_size + '\u0022';
                    }
                }
                //Rotate
                if (cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                {
                    if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                    if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                    if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                    if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';
                }

                //Crop
                
                if (cb_crop.SelectedIndex != -1 )
                {
                    if (cb_crop.SelectedIndex == 0)
                    {
                        if (auto_crop.Length > 0) filters = filters + auto_crop.Substring(5, auto_crop.Length - 5) + '\u0022';
                        else filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + '\u0022';
                    }
                    if (cb_crop.SelectedIndex == 1) filters = filters + "crop=in_h" + '\u0022';
                    if (cb_crop.SelectedIndex == 2) filters = filters + "crop=ih/3*4:ih" + '\u0022';
                    if (cb_crop.SelectedIndex == 3) filters = filters + "crop=ih/9*16:ih" + '\u0022';
                    if (cb_crop.SelectedIndex == 4) filters = filters + "crop=1440:1080" + '\u0022';
                    if (cb_crop.SelectedIndex == 5) filters = filters + "crop=1280:720" + '\u0022';
                    if (cb_crop.SelectedIndex == 6) filters = filters + "crop=1024:768" + '\u0022';
                    if (cb_crop.SelectedIndex == 7) filters = filters + "crop=1024:576" + '\u0022';
                    if (cb_crop.SelectedIndex == 8) filters = filters + "crop=800:600" + '\u0022';
                    if (cb_crop.SelectedIndex == 9) filters = filters + "crop=800:480" + '\u0022';
                    if (cb_crop.SelectedIndex == 10) filters = filters + "crop=720:576" + '\u0022';
                    if (cb_crop.SelectedIndex == 11) filters = filters + "crop=720:480" + '\u0022';
                }
                
                if (cb_deint.SelectedIndex != -1)
                {
                    filters = filters + cb_deint.SelectedItem.ToString() + "=" + cb_de_mode.SelectedIndex.ToString() + ":" + cb_de_parity.SelectedIndex.ToString() + ":" + cb_de_deint.SelectedIndex.ToString() + '\u0022';
                }
                
                if (trailer_vparam.Length > 0) filters = filters + trailer_vparam + '\u0022';

                video_encoder_param = video_encoder_param + " " + filters;
            }

            if (n_fs > 1) //Several filters combined
            {
                filters = " -vf " + '\u0022';
                //Resize
                if (cb_resize.SelectedIndex != -1)
                {
                    if (cb_resize.SelectedIndex == 0)
                    {
                        filters = filters + "scale=" + n_width.Value + ":" + n_height.Value + ",";
                    }
                    else
                    {
                        String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                        filters = filters + "scale=" + r_size + ",";
                    }
                }
                //Rotate
                if (cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                {
                    if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + ",";
                    if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + ",";
                    if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + ",";
                    if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + ",";
                }

                //Crop
                if (cb_crop.SelectedIndex != -1)
                {
                    if (cb_crop.SelectedIndex == 0)
                    {
                        if (auto_crop.Length > 0) filters = filters + auto_crop.Substring(5, auto_crop.Length - 5) + ",";
                        else filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + ",";
                    }
                    if (cb_crop.SelectedIndex == 1) filters = filters + "crop=in_h" + ",";
                    if (cb_crop.SelectedIndex == 2) filters = filters + "crop=ih/3*4:ih" + ",";
                    if (cb_crop.SelectedIndex == 3) filters = filters + "crop=ih/9*16:ih" + ",";
                    if (cb_crop.SelectedIndex == 4) filters = filters + "crop=1440:1080" + ",";
                    if (cb_crop.SelectedIndex == 5) filters = filters + "crop=1280:720" + ",";
                    if (cb_crop.SelectedIndex == 6) filters = filters + "crop=1024:768" + ",";
                    if (cb_crop.SelectedIndex == 7) filters = filters + "crop=1024:576" + ",";
                    if (cb_crop.SelectedIndex == 8) filters = filters + "crop=800:600" + ",";
                    if (cb_crop.SelectedIndex == 9) filters = filters + "crop=800:480" + ",";
                    if (cb_crop.SelectedIndex == 10) filters = filters + "crop=720:576" + ",";
                    if (cb_crop.SelectedIndex == 11) filters = filters + "crop=720:480" + ",";
                }
                if (cb_deint.SelectedIndex != -1)
                {
                    filters = filters + cb_deint.SelectedItem.ToString() + "=" + cb_de_mode.SelectedIndex.ToString() + ":" + cb_de_parity.SelectedIndex.ToString() + ":" + cb_de_deint.SelectedIndex.ToString() + ",";
                }
                if (trailer_vparam.Length > 0) filters = filters + trailer_vparam + ",";
                
                filters = filters.Substring(0, filters.Length - 1);
                video_encoder_param = video_encoder_param + " " + filters + '\u0022' + " ";                
            }

            Decimal v_sp = 0;
            Decimal a_sp = 0;
            Decimal const1 = 0.5M;
            if (n_speed.Value != 0)
            {
                if (n_speed.Value > 0)
                {
                    v_sp = const1 + ((100 - n_speed.Value) / 200);
                    a_sp = 1 + (n_speed.Value / 100);
                }

                if (n_speed.Value < 0)
                {
                    v_sp = 1 + Math.Abs(n_speed.Value / 100);
                    a_sp = Math.Round(1 / v_sp,3);
                }
                
                video_encoder_param = video_encoder_param + trailer_vparam + " -filter_complex " + '\u0022' + "[0:v]setpts=" + v_sp.ToString().Replace(",", ".") + "*PTS[v];[0:a]atempo=" + a_sp.ToString().Replace(",", ".") + "[a]" + '\u0022' + " -map " + '\u0022' + "[v]" + '\u0022' + " -map " + '\u0022' + "[a]" + '\u0022' + " ";                
            }
            video_encoder_param = video_encoder_param + " " + aspect + " ";
        }

        private void cb_framerate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_framerate.SelectedIndex == 0)
            {
                n_framerate.Enabled = true;
                n_framerate.Minimum = 1;
                n_framerate.Maximum = 60;
                n_framerate.Value = 25;
                n_framerate.PerformLayout();
            }
            else n_framerate.Enabled = false;
        }

        private void cb_profile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_preset.SelectedIndex == cb_preset.FindString("ultrafast") && cb_profile.SelectedIndex != 0)
            {
                MessageBox.Show(FFBatch.Properties.Strings.onlye_basel, FFBatch.Properties.Strings.pr_limit, MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_profile.SelectedIndex = 0;
            }
        }

        private void cb_preset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_preset.SelectedItem.ToString() == "lossless" || cb_preset.SelectedItem.ToString() == "losslesshp")
            {
                track_q_v.Enabled = false;
                n_crf.Enabled = false;
                cb_profile.Enabled = false;
                cb_level.Enabled = false;
                cb_tune.Enabled = false;
                cb_pixel.Enabled = false;
                cb_framerate.Enabled = false;
                wz1_1.Suppress = true;
            }
            else
            {
                track_q_v.Enabled = true;
                n_crf.Enabled = true;
                cb_profile.Enabled = true;
                cb_level.Enabled = true;
                cb_tune.Enabled = true;
                cb_pixel.Enabled = true;
                cb_framerate.Enabled = true;
                wz1_1.Suppress = false;
            }
        }

        private void cb_resize_SelectedIndexChanged(object sender, EventArgs e)
        {
            chk_pad.Enabled = true;
            if (cb_resize.SelectedIndex == 0)
            {
                n_width.Enabled = true;
                n_height.Enabled = true;
            }
            else
            {
                n_width.Enabled = false;
                n_height.Enabled = false;
            }

            switch (cb_resize.SelectedIndex)
            {
                case 0:
                    lbl_a_ratio.Text = Math.Round(n_width.Value / n_height.Value,2).ToString();
                    break;
                case 1:
                    lbl_a_ratio.Text = "16:9";
                    break;
                case 2:
                    lbl_a_ratio.Text = "2:40:1";
                    break;
                case 3:
                    lbl_a_ratio.Text = "4:3";
                    break;
                case 4:
                    lbl_a_ratio.Text = "16:9";
                    break;
                case 5:
                    lbl_a_ratio.Text = "4:3";
                    break;
                case 6:
                    lbl_a_ratio.Text = "16:9";
                    break;
                case 7:
                    lbl_a_ratio.Text = "4:3";
                    break;

                case 8:
                    lbl_a_ratio.Text = "4:3";
                    break;
                case 9:
                    lbl_a_ratio.Text = "16:9";
                    break;

                case 10:
                    lbl_a_ratio.Text = "PAL DV";
                    break;
                case 11:
                    lbl_a_ratio.Text = "4:3";
                    break;
                case 12:
                    lbl_a_ratio.Text = "NTCS";
                    break;
                case 13:
                    lbl_a_ratio.Text = "VGA";
                    break;
                case 14:
                    lbl_a_ratio.Text = "16:9";
                    break;
                default:
                    lbl_a_ratio.Text = "";
                    break;
            }
        }

        private void chk_vbr_opus_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_vbr_opus.CheckState == CheckState.Checked)
            {
                cb_opus_vbr.Enabled = true;
                cb_opus_vbr.SelectedIndex = 10;
            }
            else
            {
                cb_opus_vbr.Enabled = false;
            }
        }

        private void radio_audio_CheckedChanged(object sender, EventArgs e)
        {
            audio_preset = true;
            video_preset = false;
            existing_preset = false;
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            wizardControl1.Pages[1].AllowNext = true;
            pic_1.Image = images.Images[1];
        }

        private void radio_video_CheckedChanged(object sender, EventArgs e)
        {
            audio_preset = false;
            video_preset = true;
            trailer_vparam = String.Empty;
            trailer_aparam = String.Empty;
            existing_preset = false;
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            pic_1.Image = images.Images[0];
            wizardControl1.Pages[1].AllowNext = true;
        }

        private void cb_rotate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_rotate.SelectedIndex == 0)
            {
                pic_rotate.Size = new System.Drawing.Size(145, 81);
                pic_rotate.Image = img_rotate.Images[0];
            }
            if (cb_rotate.SelectedIndex == 1)
            {
                pic_rotate.Size = new System.Drawing.Size(70, 124);
                pic_rotate.Image = img_rotate.Images[1];
            }
            if (cb_rotate.SelectedIndex == 2)
            {
                pic_rotate.Size = new System.Drawing.Size(70, 124);
                pic_rotate.Image = img_rotate.Images[2];
            }
            if (cb_rotate.SelectedIndex == 3)
            {
                pic_rotate.Size = new System.Drawing.Size(145, 81);
                pic_rotate.Image = img_rotate.Images[3];
            }
            if (cb_rotate.SelectedIndex == 4)
            {
                pic_rotate.Size = new System.Drawing.Size(145, 81);
                pic_rotate.Image = img_rotate.Images[4];
            }
        }

        private void get_ff_encoders()
        {
            //Read hardware decoders
            encoder_supp = true;
            Process consola_hw = new Process();

            consola_hw.StartInfo.FileName = System.IO.Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            consola_hw.StartInfo.Arguments = " -h encoder=" + Combo_encoders.SelectedItem.ToString();
            consola_hw.StartInfo.RedirectStandardOutput = true;
            consola_hw.StartInfo.RedirectStandardError = true;
            consola_hw.StartInfo.UseShellExecute = false;
            consola_hw.StartInfo.CreateNoWindow = true;
            consola_hw.EnableRaisingEvents = true;

            String duracion = String.Empty;
            List<string> std_out = new List<string>();
            consola_hw.Start();            

            while (!consola_hw.StandardOutput.EndOfStream)
            {
                std_out.Add(consola_hw.StandardOutput.ReadLine());                
            }
            consola_hw.WaitForExit();
            consola_hw.Close();
            foreach (String str in std_out)
            {
                if (str.ToLower().Contains("is not recognized by ffmpeg"))
                {                    
                    MessageBox.Show(Properties.Strings.enc_not_sup1 + curr_ff.Replace("FFmpeg version", "") + Environment.NewLine + Environment.NewLine + Properties.Strings.enc_not_sup2, Properties.Strings.enc_not_sup3, MessageBoxButtons.OK, MessageBoxIcon.Error) ;
                    encoder_supp = false;
                    break;
                }
            }            
        }

        private void wz_end_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            lbl_vcard.Text = "";
            lbl_help.Text = "";
            pic_warn2.Visible = false;            

            Properties.Settings.Default.wiz_vid = Combo_encoders.SelectedIndex;
            Properties.Settings.Default.wiz_aud = cb_audio_encoder.SelectedIndex;
            Properties.Settings.Default.Save();

            String videocard = "";
            String cpu_info = "";
            String hw_enc = video_encoder_param.ToLower();

            if (hw_enc.Contains("h264_amf") || hw_enc.Contains("hevc_amf") || hw_enc.Contains("h264_nvenc") || hw_enc.Contains("hevc_nvenc") || hw_enc.Contains("av1_nvenc") || cb_deint.SelectedIndex == 1)

            {
                ManagementObjectSearcher search = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                foreach (ManagementObject mo in search.Get().Cast<ManagementObject>())
                {
                    PropertyData currentBitsPerPixel = mo.Properties["CurrentBitsPerPixel"];
                    PropertyData description = mo.Properties["Description"];
                    if (currentBitsPerPixel != null && description != null)
                    {
                        if (currentBitsPerPixel.Value != null)
                            videocard = videocard + (description.Value) + " ";
                    }
                }

                lbl_vcard.Text = "";
                pic_warn2.Visible = false;
                if (video_encoder_param.ToLower().Contains("h264_amf") || video_encoder_param.ToLower().Contains("hevc_amf") || cb_deint.SelectedIndex == 1)
                {
                    if (!videocard.ToLower().Contains("radeon"))
                    {
                        pic_warn2.Visible = true;
                        lbl_vcard.Text = videocard + Properties.Strings.not_amf;
                        if (cb_deint.SelectedIndex == 1) lbl_vcard.Text = videocard + Properties.Strings.not_amf + " (yadif_cuda).";
                        if (cb_deint.SelectedIndex == 1)
                        {
                            pic_warn2.Visible = true;
                            lbl_vcard.Text = videocard + Properties.Strings.not_amf + " (yadif_cuda).";
                        }
                    }
                }

                if (video_encoder_param.ToLower().Contains("h264_nvenc") || video_encoder_param.ToLower().Contains("hevc_nvenc") || video_encoder_param.ToLower().Contains("av1_nvenc"))
                {
                    if (!videocard.ToLower().Contains("nvidia"))
                    {
                        pic_warn2.Visible = true;
                        lbl_vcard.Text = videocard + Properties.Strings.not_nvenc;
                    }
                }
            }
            if (hw_enc.Contains("h264_qsv") || hw_enc.Contains("hevc_qsv"))
            {
                ManagementObjectSearcher mosProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                string Procname = null;

                foreach (ManagementObject moProcessor in mosProcessor.Get().Cast<ManagementObject>())
                {
                    if (moProcessor["name"] != null) cpu_info = moProcessor["name"].ToString();
                    else cpu_info = Properties.Strings.unknown;
                }

                if (video_encoder_param.ToLower().Contains("h264_qsv") || video_encoder_param.ToLower().Contains("hevc_qsv"))
                {
                    if (!cpu_info.ToLower().Contains("intel"))
                    {
                        pic_warn2.Visible = true;
                        lbl_vcard.Text = Properties.Strings.your_sys + " " + Properties.Strings.not_qsv;
                    }
                }
            }


            if (lv1_item != String.Empty)
            {
                pic_status.Visible = true;
                btn_status.Visible = true;
                pic_status.Image = img_status.Images[0];
            }
            else
            {
                lbl_help.Text = FFBatch.Properties.Strings.wiz_ready;
            }

            cb_container.Items.Clear();

            if (existing_preset == true)
            {
                String[] containers = new String[] { txt_ext_format.Text, "mkv", "mp4", "m4v", "mov", "flv", "avi", "ts", "mts", "m2ts", "wav", "flac", "aac", "mp3", "ogg", "ogv" };
                foreach (String contain in containers) cb_container.Items.Add(contain);

                List<object> list = new List<object>();
                foreach (object o in cb_container.Items)
                {
                    if (!list.Contains(o))
                    {
                        list.Add(o);
                    }
                }
                cb_container.Items.Clear();
                cb_container.Items.AddRange(list.ToArray());

                cb_container.SelectedIndex = cb_container.FindString(txt_ext_format.Text);
                txt_preset_name.Text = combo_presets_ext.Text;
            }

            if (video_preset == true)
            {
                String[] containers = new String[] { "mkv", "mp4", "m4v", "mov", "flv", "avi", "ts", "mts", "m2ts", "webm", "mxf" };
                foreach (String contain in containers) cb_container.Items.Add(contain);
                cb_container.SelectedIndex = 0;
                txt_preset_name.Text = "Video: " + Combo_encoders.SelectedItem.ToString() + " - " + cb_audio_encoder.SelectedItem.ToString();
            }
            if (audio_preset == true)
            {
                String[] containers = new String[] { "wav", "flac", "aac", "ac3", "eac3", "mp3", "ogg", "opus", "m4a", "thd", "dts", "mka", "mkv", "mxf" };
                foreach (String contain in containers) cb_container.Items.Add(contain);
                switch (cb_audio_encoder.SelectedIndex)
                {
                    case 2:
                        cb_container.SelectedIndex = 0;
                        break;

                    case 3:
                        cb_container.SelectedIndex = 0;
                        break;

                    case 4:
                        cb_container.SelectedIndex = 1;
                        break;

                    case 5:
                        cb_container.SelectedIndex = 2;
                        break;

                    case 6:
                        cb_container.SelectedIndex = 3;
                        break;

                    case 7:
                        cb_container.SelectedIndex = 4;
                        break;

                    case 8:
                        cb_container.SelectedIndex = 5;
                        break;

                    case 9:
                        cb_container.SelectedIndex = 6;
                        break;

                    case 10:
                        cb_container.SelectedIndex = 7;
                        break;
                }

                txt_preset_name.Text = "Audio: " + cb_audio_encoder.SelectedItem.ToString();
            }
        }

        private void cb_container_SelectedIndexChanged(object sender, EventArgs e)
        {
            pic_status.Image = img_status.Images[0];
            preset_ok = false;

            //Video containers
            if (cb_container.SelectedIndex == cb_container.FindString("mkv")) txt_container.Text = "Matroska Video";
            if (cb_container.SelectedIndex == cb_container.FindString("mp4")) txt_container.Text = "MPEG-4 Video";
            if (cb_container.SelectedIndex == cb_container.FindString("m4v")) txt_container.Text = "MPEG-4 Apple Video";
            if (cb_container.SelectedIndex == cb_container.FindString("flv")) txt_container.Text = "Flash Video";
            if (cb_container.SelectedIndex == cb_container.FindString("mov")) txt_container.Text = "Apple QuickTime Video";
            if (cb_container.SelectedIndex == cb_container.FindString("avi")) txt_container.Text = "Audio Video Interleave";
            if (cb_container.SelectedIndex == cb_container.FindString("mts")) txt_container.Text = "MPEG AVCHD Transport stream";
            if (cb_container.SelectedIndex == cb_container.FindString("ts")) txt_container.Text = "MPEG Transport stream";
            if (cb_container.SelectedIndex == cb_container.FindString("m2ts")) txt_container.Text = "MPEG AVCHD Transport stream";
            if (cb_container.SelectedIndex == cb_container.FindString("webm")) txt_container.Text = "WebM video";
            if (cb_container.SelectedIndex == cb_container.FindString("mxf")) txt_container.Text = "Material eXchange Format";
            //Audio containers
            if (cb_container.SelectedIndex == cb_container.FindString("wav")) txt_container.Text = "PCM audio (Lossless)";
            if (cb_container.SelectedIndex == cb_container.FindString("flac")) txt_container.Text = "FLAC audio (Lossless)";
            if (cb_container.SelectedIndex == cb_container.FindString("ac3")) txt_container.Text = "Dolby Digital audio";
            if (cb_container.SelectedIndex == cb_container.FindString("eac3")) txt_container.Text = "Dolby Digital Plus audio";
            if (cb_container.SelectedIndex == cb_container.FindString("aac")) txt_container.Text = "Advanced audio coding";
            if (cb_container.SelectedIndex == cb_container.FindString("mp3")) txt_container.Text = "MPEG-2 Audio Layer III";
            if (cb_container.SelectedIndex == cb_container.FindString("m4a")) txt_container.Text = "MPEG-4 Apple audio";
            if (cb_container.SelectedIndex == cb_container.FindString("ogg")) txt_container.Text = "Vorbis audio container";
            if (cb_container.SelectedIndex == cb_container.FindString("opus")) txt_container.Text = "Opus audio container";
            if (cb_container.SelectedIndex == cb_container.FindString("thd")) txt_container.Text = "Dolby TrueHD audio";
            if (cb_container.SelectedIndex == cb_container.FindString("dts")) txt_container.Text = "DTS Audio";
            if (cb_container.SelectedIndex == cb_container.FindString("mka")) txt_container.Text = "Matroska audio";

            if (video_preset == true)
            {
                if (Combo_encoders.SelectedIndex == 0 && cb_container.SelectedIndex != 0)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }

                if (Combo_encoders.SelectedIndex == 1 || Combo_encoders.SelectedIndex == 2 || Combo_encoders.SelectedIndex == 3 || Combo_encoders.SelectedIndex == 4 || Combo_encoders.SelectedIndex == 5 || Combo_encoders.SelectedIndex == 6)
                {
                    if (cb_container.SelectedIndex != 0 && cb_container.SelectedIndex != 1 && cb_container.SelectedIndex != 2 && cb_container.SelectedIndex != 3 && cb_container.SelectedIndex != 4 && cb_container.SelectedIndex != 6 && cb_container.SelectedIndex != 7 && cb_container.SelectedIndex != 8)
                    {
                        pic_warning.Visible = true;
                        lbl_container.Visible = true;
                        btn_status.PerformClick();
                        return;
                    }
                    if (cb_container.SelectedIndex != 0)
                    {
                        if (cb_audio_encoder.SelectedIndex != 0 && cb_audio_encoder.SelectedIndex != cb_audio_encoder.FindString("aac") && cb_audio_encoder.SelectedIndex != cb_audio_encoder.FindString("mp3"))
                        {
                            pic_warning.Visible = true;
                            lbl_container.Visible = true;
                            btn_status.PerformClick();
                            return;
                        }
                    }
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }

                if (Combo_encoders.SelectedIndex == 7)
                {
                    if (cb_container.SelectedIndex != 0 && cb_container.SelectedIndex != 9)
                    {
                        pic_warning.Visible = true;
                        lbl_container.Visible = true;
                        btn_status.PerformClick();
                        return;
                    }
                    if (cb_container.SelectedIndex != 0)
                    {
                        if (cb_audio_encoder.SelectedIndex != 9)
                        {
                            pic_warning.Visible = true;
                            lbl_container.Visible = true;
                            btn_status.PerformClick();
                            return;
                        }
                    }
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (Combo_encoders.SelectedIndex == 8)
                {
                    if (cb_container.SelectedIndex != 0 && cb_container.SelectedIndex != 3)
                    {
                        pic_warning.Visible = true;
                        lbl_container.Visible = true;
                        btn_status.PerformClick();
                        return;
                    }
                    if (cb_container.SelectedIndex != 0)
                    {
                        if (cb_audio_encoder.SelectedIndex != 0 && cb_audio_encoder.SelectedIndex != 2 && cb_audio_encoder.SelectedIndex != 3 && cb_audio_encoder.SelectedIndex != 4)
                        {
                            pic_warning.Visible = true;
                            lbl_container.Visible = true;
                            btn_status.PerformClick();
                            return;
                        }
                    }
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
            }

            if (audio_preset == true)
            {
                if (cb_audio_encoder.SelectedIndex == 2 && cb_container.SelectedIndex != 0)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 3 && cb_container.SelectedIndex != 0)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 4 && cb_container.SelectedIndex != 1)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 5 && cb_container.SelectedIndex != 2)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 6 && cb_container.SelectedIndex != 3)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 7 && cb_container.SelectedIndex != 4)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 8 && cb_container.SelectedIndex != 5)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 9 && cb_container.SelectedIndex != 6)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
                if (cb_audio_encoder.SelectedIndex == 10 && cb_container.SelectedIndex != 7)
                {
                    pic_warning.Visible = true;
                    lbl_container.Visible = true;
                    btn_status.PerformClick();
                    return;
                }
                else
                {
                    pic_warning.Visible = false;
                    lbl_container.Visible = false;
                    btn_status.PerformClick();
                }
            }
            if (existing_preset == true)
            {
                pic_warning.Visible = false;
                lbl_container.Visible = false;
                btn_status.PerformClick();
            }
        }

        private void chk_save_pres_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_save_pres.CheckState == CheckState.Checked)
            {
                txt_preset_name.Enabled = true;
                txt_preset_name.Focus();
                save_preset = true;
            }
            else
            {
                txt_preset_name.Enabled = false;
                save_preset = false;
            }
        }

        private void reset_v_params()
        {            
            video_encoder_param = String.Empty;            
            libx264_params = String.Empty;
            libx265_params = String.Empty;
            h264_qsv_params = String.Empty;
            hevc_qsv_params = String.Empty;
            h264_nvenc_params = String.Empty;
            hevc_nvenc_params = String.Empty;
            libvpx_vp9_params = String.Empty;
            prores_ks_params = String.Empty;
            dnxhd_params = String.Empty;
            dnxhr_params = String.Empty;
            libsvtav1_params = String.Empty;
            av1_nvenc_params = String.Empty;
        }

        private void commit_video_1()
        {
            reset_v_params();
            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("copy"))
            {
                video_encoder_param = "-c:v copy";
                wz1_1.Suppress = true;                
            }
            else wz2.Suppress = false;
            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("libx264"))
            {
                video_encoder_param = "-c:v libx264";
                if (cb_preset.SelectedIndex != -1) libx264_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) libx264_params = libx264_params + " -profile " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) libx264_params = libx264_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) libx264_params = libx264_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    libx264_params = libx264_params + " -crf " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    libx264_params = libx264_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -minrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -maxrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -bufsize " + (Math.Round(n_crf.Value * 1024 / 1000 * 2)).ToString() + "K" + " -nal-hrd cbr";
                }

                if (cb_pixel.SelectedIndex != -1) libx264_params = libx264_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        libx264_params = libx264_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    libx264_params = libx264_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    libx264_params = libx264_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    libx264_params = libx264_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    libx264_params = libx264_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + libx264_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("libsvtav1"))
            {
                video_encoder_param = "-c:v libsvtav1";                
                if (cb_preset.SelectedIndex != -1) libsvtav1_params = " -preset " + cb_preset.SelectedItem.ToString().Replace(" (slow)", "").Replace(" (fast)", "");
                if (cb_profile.SelectedIndex != -1) libsvtav1_params = libsvtav1_params + " -profile " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) libsvtav1_params = libsvtav1_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) libsvtav1_params = libsvtav1_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    libsvtav1_params = libsvtav1_params + " -crf " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    libsvtav1_params = libsvtav1_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -minrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -maxrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -bufsize " + (Math.Round(n_crf.Value * 1024 / 1000 * 2)).ToString() + "K" + " -nal-hrd cbr";
                }

                if (cb_pixel.SelectedIndex != -1) libsvtav1_params = libsvtav1_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        libsvtav1_params = libsvtav1_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    libsvtav1_params = libsvtav1_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    libsvtav1_params = libsvtav1_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    libsvtav1_params = libsvtav1_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    libsvtav1_params = libsvtav1_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + libsvtav1_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("libx265"))
            {
                video_encoder_param = "-c:v libx265";
                if (cb_preset.SelectedIndex != -1) libx265_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) libx265_params = libx265_params + " -profile " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) libx265_params = libx265_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) libx265_params = libx265_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    libx265_params = libx265_params + " -crf " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    libx265_params = libx265_params + " -x265-params strict-cbr=1" + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -minrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -maxrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -bufsize " + (Math.Round(n_crf.Value * 1024 / 1000 * 2)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) libx265_params = libx265_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        libx265_params = libx265_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    libx265_params = libx265_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    libx265_params = libx265_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    libx265_params = libx265_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    libx265_params = libx265_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + libx265_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("h264_qsv"))
            {
                video_encoder_param = "-c:v h264_qsv";
                if (cb_preset.SelectedIndex != -1) h264_qsv_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) h264_qsv_params = h264_qsv_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) h264_qsv_params = h264_qsv_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) h264_qsv_params = h264_qsv_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    h264_qsv_params = h264_qsv_params + " -global_quality " + n_crf.Value.ToString() + " -look_ahead 1";
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    h264_qsv_params = h264_qsv_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) h264_qsv_params = h264_qsv_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        h264_qsv_params = h264_qsv_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    h264_qsv_params = h264_qsv_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    h264_qsv_params = h264_qsv_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    h264_qsv_params = h264_qsv_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    h264_qsv_params = h264_qsv_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + h264_qsv_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("hevc_qsv"))
            {
                video_encoder_param = "-c:v hevc_qsv";
                if (cb_preset.SelectedIndex != -1) hevc_qsv_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) hevc_qsv_params = hevc_qsv_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) hevc_qsv_params = hevc_qsv_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) hevc_qsv_params = hevc_qsv_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    hevc_qsv_params = hevc_qsv_params + " -global_quality " + n_crf.Value.ToString() + " -look_ahead 1";
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    hevc_qsv_params = hevc_qsv_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) hevc_qsv_params = hevc_qsv_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        hevc_qsv_params = hevc_qsv_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    hevc_qsv_params = hevc_qsv_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    hevc_qsv_params = hevc_qsv_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    hevc_qsv_params = hevc_qsv_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    hevc_qsv_params = hevc_qsv_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + hevc_qsv_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("h264_nvenc"))
            {
                video_encoder_param = "-c:v h264_nvenc";
                if (cb_preset.SelectedIndex != -1) h264_nvenc_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) h264_nvenc_params = h264_nvenc_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) h264_nvenc_params = h264_nvenc_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) h264_nvenc_params = h264_nvenc_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    h264_nvenc_params = h264_nvenc_params + " -rc constqp -qp " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    h264_nvenc_params = h264_nvenc_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) h264_nvenc_params = h264_nvenc_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        h264_nvenc_params = h264_nvenc_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    h264_nvenc_params = h264_nvenc_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    h264_nvenc_params = h264_nvenc_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    h264_nvenc_params = h264_nvenc_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    h264_nvenc_params = h264_nvenc_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + h264_nvenc_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("av1_nvenc"))
            {
                video_encoder_param = "-c:v av1_nvenc";
                if (cb_preset.SelectedIndex != -1) av1_nvenc_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) av1_nvenc_params = av1_nvenc_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) av1_nvenc_params = av1_nvenc_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) av1_nvenc_params = av1_nvenc_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    av1_nvenc_params = av1_nvenc_params + " -rc constqp -qp " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    av1_nvenc_params = av1_nvenc_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) av1_nvenc_params = av1_nvenc_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        av1_nvenc_params = av1_nvenc_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    av1_nvenc_params = av1_nvenc_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    av1_nvenc_params = av1_nvenc_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    av1_nvenc_params = av1_nvenc_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    av1_nvenc_params = av1_nvenc_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + av1_nvenc_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("hevc_nvenc"))
            {
                video_encoder_param = "-c:v hevc_nvenc";
                if (cb_preset.SelectedIndex != -1) hevc_nvenc_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) hevc_nvenc_params = hevc_nvenc_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) hevc_nvenc_params = hevc_nvenc_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) hevc_nvenc_params = hevc_nvenc_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    hevc_nvenc_params = hevc_nvenc_params + " -rc constqp -qp " + n_crf.Value.ToString() + " -look_ahead 1";
                }
                if (combo_crf_mode.SelectedIndex == 1 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    hevc_nvenc_params = hevc_nvenc_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) hevc_nvenc_params = hevc_nvenc_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        hevc_nvenc_params = hevc_nvenc_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    hevc_nvenc_params = hevc_nvenc_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    hevc_nvenc_params = hevc_nvenc_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    hevc_nvenc_params = hevc_nvenc_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    hevc_nvenc_params = hevc_nvenc_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                //video_encoder_param = video_encoder_param;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("h264_amf"))
            {
                video_encoder_param = "-c:v h264_amf";
                if (cb_preset.SelectedIndex != -1) h264_amf_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) h264_amf_params = h264_amf_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) h264_amf_params = h264_amf_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) h264_amf_params = h264_amf_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    h264_amf_params = h264_amf_params + " -rc cqp -qp_p " + n_crf.Value.ToString() + " -qp_i " + n_crf.Value.ToString() + " -qp_b " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    h264_amf_params = h264_amf_params + " -rc cbr -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) h264_amf_params = h264_amf_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        h264_amf_params = h264_amf_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    h264_amf_params = h264_amf_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    h264_amf_params = h264_amf_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    h264_amf_params = h264_amf_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    h264_amf_params = h264_amf_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + h264_amf_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("hevc_amf"))
            {
                video_encoder_param = "-c:v hevc_amf";
                if (cb_preset.SelectedIndex != -1) hevc_amf_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) hevc_amf_params = hevc_amf_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) hevc_amf_params = hevc_amf_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) hevc_amf_params = hevc_amf_params + " -tune " + cb_tune.SelectedItem.ToString();
                hevc_amf_params = hevc_amf_params + " -quality " + cb_q_amd.SelectedItem.ToString();
                if (combo_crf_mode.SelectedIndex == 0)
                {
                    hevc_amf_params = hevc_amf_params + " -rc cqp -qp_p " + n_crf.Value.ToString() + " -qp_i " + n_crf.Value.ToString();
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    hevc_amf_params = hevc_amf_params + " -rc cbr -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K";
                }

                if (cb_pixel.SelectedIndex != -1) hevc_amf_params = hevc_amf_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        hevc_amf_params = hevc_amf_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    hevc_amf_params = hevc_amf_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    hevc_amf_params = hevc_amf_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    hevc_amf_params = hevc_amf_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    hevc_amf_params = hevc_amf_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + hevc_amf_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("libvpx-vp9"))
            {
                if (chk_vp9_threads.CheckState == CheckState.Checked) video_encoder_param = "-c:v libvpx-vp9 -row-mt 1";
                else video_encoder_param = "-c:v libvpx-vp9";
                if (cb_preset.SelectedIndex != -1) libvpx_vp9_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) libvpx_vp9_params = libvpx_vp9_params + " -profile " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) libvpx_vp9_params = libvpx_vp9_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) libvpx_vp9_params = libvpx_vp9_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0)
                {
                    libvpx_vp9_params = libvpx_vp9_params + " -crf " + n_crf.Value.ToString() + " -b:v 0 ";
                }
                if (combo_crf_mode.SelectedIndex == 1)
                {
                    libvpx_vp9_params = libvpx_vp9_params + " -b:v " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -minrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -maxrate " + (Math.Round(n_crf.Value * 1024 / 1000)).ToString() + "K" + " -crf " + cb_cq_vp9.SelectedItem.ToString();
                }

                if (cb_pixel.SelectedIndex != -1) libvpx_vp9_params = libvpx_vp9_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        libvpx_vp9_params = libvpx_vp9_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    libvpx_vp9_params = libvpx_vp9_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    libvpx_vp9_params = libvpx_vp9_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    libvpx_vp9_params = libvpx_vp9_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    libvpx_vp9_params = libvpx_vp9_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }
                video_encoder_param = video_encoder_param + libvpx_vp9_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("prores_ks"))
            {
                video_encoder_param = "-c:v prores_ks";
                if (cb_profile_prores.SelectedIndex != -1) prores_ks_params = " -profile:v " + cb_profile_prores.SelectedItem.ToString();
                if (cb_pixel_prores.SelectedIndex != -1) prores_ks_params = prores_ks_params + " -pix_fmt " + cb_pixel_prores.SelectedItem.ToString();
                if (cb_vendor_prores.SelectedIndex != -1) prores_ks_params = prores_ks_params + " -vendor " + cb_vendor_prores.SelectedItem.ToString();
                if (cb_bits_prores.SelectedIndex == 1 && cb_profile_prores.SelectedIndex != cb_profile_prores.FindString("4444"))
                {
                    prores_ks_params = prores_ks_params + " -bits_per_mb " + cb_bits_prores.SelectedItem.ToString();
                }

                video_encoder_param = video_encoder_param + prores_ks_params;
            }
            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("dnxhd"))
            {
                video_encoder_param = "-c:v dnxhd";
                dnxhd_params = dnxhd_params + " -b:v " + n_crf.Value.ToString() + "M";
                if (cb_pixel.SelectedIndex != -1) dnxhd_params = dnxhd_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        dnxhd_params = dnxhd_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    dnxhd_params = dnxhd_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    libx264_params = libx264_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    dnxhd_params = dnxhd_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    dnxhd_params = dnxhd_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }

                video_encoder_param = video_encoder_param + dnxhd_params;
            }

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("dnxhr"))
            {
                video_encoder_param = "-c:v dnxhd";
                dnxhr_params = dnxhr_params + " -profile:v " + cb_profile_dnxhr.SelectedItem.ToString();
                if (cb_pixel.SelectedIndex != -1) dnxhr_params = dnxhr_params + " -pix_fmt " + cb_pixel.SelectedItem.ToString();

                if (cb_framerate.SelectedIndex != -1)
                {
                    if (cb_framerate.SelectedIndex == 0)
                    {
                        dnxhr_params = dnxhr_params + " -r " + n_framerate.Value.ToString();
                    }
                    else
                    {
                        switch (cb_framerate.SelectedIndex)
                        {
                            case 1:
                                {
                                    dnxhr_params = dnxhr_params + " -r " + "24000/1001";
                                    break;
                                }
                            case 3:
                                {
                                    dnxhr_params = dnxhr_params + " -r " + "30000/1001";
                                    break;
                                }
                            case 6:
                                {
                                    dnxhr_params = dnxhr_params + " -r " + "60000/1001";
                                    break;
                                }
                            default:
                                {
                                    dnxhr_params = dnxhr_params + " -r " + cb_framerate.SelectedItem.ToString().Substring(0, 2);
                                    break;
                                }
                        }
                    }
                }

                video_encoder_param = video_encoder_param + dnxhr_params;
            }            
        }

        private void wz1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (Combo_encoders.SelectedIndex != 0)
            {
                get_ff_encoders();
                if (encoder_supp == false)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (trailer_vparam.Length > 0 && Combo_encoders.SelectedItem.ToString() == "copy")
            {
                MessageBox.Show(Properties.Strings.stream + " " + Properties.Strings.copy + " " + Properties.Strings.errors1);
                e.Cancel = true;
            }

            if (curr_ff.ToLower().Contains("essential")) 
            {
                if (Combo_encoders.SelectedItem.ToString().Contains("nvenc") || Combo_encoders.SelectedItem.ToString().Contains("amf"))
                    MessageBox.Show(Properties.Strings.hw_ff_1 + Environment.NewLine + Environment.NewLine + Properties.Strings.Hw_ff_2, Properties.Strings.information, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            commit_video_1();
        }

        private void cb_framerate_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cb_framerate.SelectedIndex == 0)
            {
                n_framerate.Enabled = true;
            }
            else
            {
                n_framerate.Enabled = false;
            }
        }

        private void reset_a_params()
        {
            audio_encoder_param = String.Empty;
            pcm16_params = String.Empty;
            pcm24_params = String.Empty;
            flac_params = String.Empty;
            aac_params = String.Empty;
            ac3_params = String.Empty;
            eac3_params = String.Empty;
            mp3_params = String.Empty;
            vorbis_params = String.Empty;
            opus_params = String.Empty;
        }

        private void wz2_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (trailer_vparam.Length > 0 && chk_no_aud_tr.Checked)
            {            
                    audio_encoder_param = "-an";
                    return;
            }

            

            if (trailer_vparam.Length > 0 && !chk_no_aud_tr.Checked && cb_audio_encoder.SelectedItem.ToString() == "none")
            {
                MessageBox.Show(FFBatch.Properties.Strings.none_copy, "Audio preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }
                        
            if (trailer_aparam.Length > 0 && cb_audio_encoder.SelectedItem.ToString() == "copy" && !chk_no_aud_tr.Checked)
            {
                MessageBox.Show(FFBatch.Properties.Strings.none_copy);
                e.Cancel = true;
            }

            if (n_speed.Value != 0 && cb_audio_encoder.SelectedItem.ToString() == "copy")
            {
                MessageBox.Show(FFBatch.Properties.Strings.speed_copy);
                e.Cancel = true;
            }
            reset_a_params();
            if (cb_audio_encoder.SelectedItem == null) cb_audio_encoder.SelectedIndex = 0;

            if ((cb_audio_encoder.SelectedItem.ToString() == "none" || cb_audio_encoder.SelectedItem.ToString() == "copy") && audio_preset == true)
            {
                MessageBox.Show(FFBatch.Properties.Strings.none_copy, "Audio preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
                return;
            }
            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("copy")) audio_encoder_param = "-c:a copy";

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("pcm16"))
            {
                audio_encoder_param = "-c:a pcm_s16le";
                if (cb_sample_rate.SelectedIndex != 0) pcm16_params = " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    pcm16_params = pcm16_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) pcm16_params = pcm16_params + "2";
                    if (cb_channels.SelectedIndex == 2) pcm16_params = pcm16_params + "1";
                }
                if (cb_chunk_size.SelectedIndex != -1 && cb_chunk_size.SelectedIndex != 0) pcm16_params = pcm16_params + " -chunk_size " + cb_chunk_size.SelectedItem.ToString();
                audio_encoder_param = audio_encoder_param + pcm16_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("pcm24"))
            {
                audio_encoder_param = "-c:a pcm_s24le";
                if (cb_sample_rate.SelectedIndex != 0) pcm24_params = " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    pcm24_params = pcm24_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) pcm24_params = pcm24_params + "2";
                    if (cb_channels.SelectedIndex == 2) pcm24_params = pcm24_params + "1";
                }
                if (cb_chunk_size.SelectedIndex != -1 && cb_chunk_size.SelectedIndex != 0) pcm24_params = pcm24_params + " -chunk_size " + cb_chunk_size.SelectedItem.ToString();
                audio_encoder_param = audio_encoder_param + pcm24_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("flac"))
            {
                audio_encoder_param = "-c:a flac";
                flac_params = " -compression_level " + n_bit_audio.Value.ToString();
                if (cb_sample_rate.SelectedIndex != 0) flac_params = flac_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    flac_params = flac_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) flac_params = flac_params + "2";
                    if (cb_channels.SelectedIndex == 2) flac_params = flac_params + "1";
                }

                audio_encoder_param = audio_encoder_param + flac_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("aac"))
            {
                audio_encoder_param = "-c:a aac";
                if (cb_bitrate_mode.SelectedIndex == 0) aac_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (cb_bitrate_mode.SelectedIndex == 1) aac_params = " -vbr " + n_bit_audio.Value.ToString();
                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0) aac_params = aac_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                if (cb_sample_rate.SelectedIndex != 0) aac_params = aac_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    aac_params = aac_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) aac_params = aac_params + "2";
                    if (cb_channels.SelectedIndex == 2) aac_params = aac_params + "1";
                }
                audio_encoder_param = audio_encoder_param + aac_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("ac3"))
            {
                audio_encoder_param = "-c:a ac3";
                ac3_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0) ac3_params = ac3_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                if (cb_sample_rate.SelectedIndex != 0) ac3_params = ac3_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    ac3_params = ac3_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) ac3_params = ac3_params + "2";
                    if (cb_channels.SelectedIndex == 2) ac3_params = ac3_params + "1";
                }
                audio_encoder_param = audio_encoder_param + ac3_params;
            }
            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("e-ac3"))
            {
                audio_encoder_param = "-c:a eac3";
                eac3_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0) eac3_params = eac3_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                if (cb_sample_rate.SelectedIndex != 0) eac3_params = eac3_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    eac3_params = eac3_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) eac3_params = eac3_params + "2";
                    if (cb_channels.SelectedIndex == 2) eac3_params = eac3_params + "1";
                }
                audio_encoder_param = audio_encoder_param + eac3_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("mp3"))
            {
                audio_encoder_param = "-c:a libmp3lame";
                if (cb_bitrate_mode.SelectedIndex == 0) mp3_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (cb_bitrate_mode.SelectedIndex == 1) mp3_params = " -qscale:a " + n_bit_audio.Value.ToString();

                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0) mp3_params = mp3_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                if (cb_sample_rate.SelectedIndex != 0) mp3_params = mp3_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    mp3_params = mp3_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) mp3_params = mp3_params + "2";
                    if (cb_channels.SelectedIndex == 2) mp3_params = mp3_params + "1";
                }
                audio_encoder_param = audio_encoder_param + mp3_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("vorbis"))
            {
                audio_encoder_param = "-c:a libvorbis";
                if (cb_bitrate_mode.SelectedIndex == 0) vorbis_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (cb_bitrate_mode.SelectedIndex == 1) vorbis_params = " -qscale:a " + n_bit_audio.Value.ToString();

                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0) vorbis_params = vorbis_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                if (cb_sample_rate.SelectedIndex != 0) vorbis_params = vorbis_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    vorbis_params = vorbis_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) vorbis_params = vorbis_params + "2";
                    if (cb_channels.SelectedIndex == 2) vorbis_params = vorbis_params + "1";
                }
                audio_encoder_param = audio_encoder_param + vorbis_params;
            }

            if (cb_audio_encoder.SelectedIndex == cb_audio_encoder.FindString("opus"))
            {
                audio_encoder_param = "-c:a libopus";
                if (cb_bitrate_mode.SelectedIndex == 0) opus_params = " -b:a " + n_bit_audio.Value.ToString() + "K";
                if (chk_vbr_opus.CheckState == CheckState.Checked)
                {
                    opus_params = opus_params + " -vbr on -compression_level ";
                    if (cb_opus_vbr.SelectedIndex == 0)
                    {
                        opus_params = opus_params + cb_opus_vbr.SelectedItem.ToString().Substring(0, 2);
                    }
                    else
                    {
                        opus_params = opus_params + cb_opus_vbr.SelectedItem.ToString().Substring(0, 1);
                    }
                }

                if (cb_cutoff.SelectedIndex != -1 && cb_cutoff.SelectedIndex != 0)
                {
                    if (cb_cutoff.SelectedItem.ToString().Length == 5)
                    {
                        opus_params = opus_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 3);
                    }
                    else
                    {
                        opus_params = opus_params + " -cutoff " + cb_cutoff.SelectedItem.ToString().Substring(0, 2);
                    }
                }
                if (cb_sample_rate.SelectedIndex != 0) opus_params = opus_params + " -ar " + cb_sample_rate.SelectedItem.ToString();
                if (cb_channels.SelectedIndex != -1 && cb_channels.SelectedIndex != 0)
                {
                    opus_params = opus_params + " -ac ";
                    if (cb_channels.SelectedIndex == 1) opus_params = opus_params + "2";
                    if (cb_channels.SelectedIndex == 2) opus_params = opus_params + "1";
                }
                audio_encoder_param = audio_encoder_param + opus_params;
            }

            Decimal a_sp = 0;
            Decimal const1 = 0.5M;
            String afilter = String.Empty;
            String speed_a = String.Empty;
            String normalize = String.Empty;

            if (n_speed2.Value != 0)
            {
                afilter = " -filter:a ";
                if (n_speed2.Value > 0)
                {
                    a_sp = 1 + (n_speed2.Value / 100);
                }

                if (n_speed2.Value < 0)
                {
                    a_sp = 1 - (Math.Abs((n_speed2.Value / 100)) / 2);
                }
                speed_a = "atempo=" + a_sp.ToString().Replace(",", ".");
                if (chk_normalize.Checked)
                {   
                    normalize = "loudnorm";
                    speed_a = '\u0022' + speed_a + "," + normalize + "," + '\u0022';
                    afilter = afilter + speed_a;
                }
                else afilter = afilter + speed_a;             
            }
            else if (chk_normalize.Checked)
            {
                normalize = "loudnorm";
                if (trailer_aparam.Length > 0) afilter = " -filter:a " + normalize + "," + trailer_aparam + ",";
                else afilter = " -filter:a " + normalize;
            }
            else
            {
                if (trailer_aparam.Length > 0 ) afilter = " -filter:a " + trailer_aparam;
            }

            audio_encoder_param = audio_encoder_param + " " + afilter + " ";
        }

        private void cb_crop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_crop.SelectedIndex == 0)
            {
                u_crop.Enabled = true;
                d_crop.Enabled = true;
                l_crop.Enabled = true;
                r_crop.Enabled = true;
            }
            else
            {
                u_crop.Enabled = false;
                d_crop.Enabled = false;
                l_crop.Enabled = false;
                r_crop.Enabled = false;
                btn_crop_wiz.BackColor = Color.White;
            }
        }

        private void btn_ref_Click(object sender, EventArgs e)
        {
            Form frmInfo = new Form();
            frmInfo.Name = "DNxHD supported parameters";
            frmInfo.Text = "DNxHD supported parameters";
            frmInfo.Icon = this.Icon;
            frmInfo.Height = 724;
            frmInfo.Width = 440;
            frmInfo.FormBorderStyle = FormBorderStyle.Fixed3D;
            frmInfo.MaximizeBox = false;
            frmInfo.MinimizeBox = false;
            frmInfo.BackColor = this.BackColor;

            ListView LB1 = new ListView();
            LB1.Parent = frmInfo;
            LB1.ShowItemToolTips = true;
            LB1.Left = 14;
            LB1.Top = 33;
            LB1.Height = 611;
            LB1.Width = 390;
            //LB1.Font = fuente_list;
            LB1.View = View.Details;
            LB1.FullRowSelect = true;
            LB1.GridLines = true;
            LB1.Columns.Add("Frame size", 127);
            LB1.Columns.Add("Bitrate", 127);
            LB1.Columns.Add("Pixel format", 127);
            LB1.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            LB1.Refresh();

            TextBox titulo = new TextBox();
            titulo.Parent = frmInfo;
            titulo.Top = 6;
            titulo.Left = 14;
            titulo.Width = 390;
            titulo.TabIndex = 0;
            titulo.BackColor = this.BackColor;

            titulo.BorderStyle = BorderStyle.Fixed3D;
            titulo.TextAlign = HorizontalAlignment.Center;
            titulo.ReadOnly = true;

            titulo.Text = "DNxHD supported parameters";

            Button boton_ok = new Button();
            boton_ok.Parent = frmInfo;
            boton_ok.Left = 14;
            boton_ok.Top = 650;
            boton_ok.Width = 390;
            boton_ok.Height = 27;
            boton_ok.Text = "Close window";
            boton_ok.Click += new EventHandler(boton_ok_Click);

            LB1.Items.Add("960x720p");
            LB1.Items[0].SubItems.Add("42Mbps");
            LB1.Items[0].SubItems.Add("yuv422p");
            LB1.Items.Add("960x720p");
            LB1.Items[1].SubItems.Add("75Mbps");
            LB1.Items[1].SubItems.Add("yuv422p");
            LB1.Items.Add("960x720p");
            LB1.Items[2].SubItems.Add("115Mbps");
            LB1.Items[2].SubItems.Add("yuv422p");
            LB1.Items.Add("---------------------------------");
            LB1.Items[3].SubItems.Add("---------------------------------");
            LB1.Items[3].SubItems.Add("---------------------------------");
            LB1.Items.Add("1280x720p");
            LB1.Items[4].SubItems.Add("60Mbps");
            LB1.Items[4].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[5].SubItems.Add("75Mbps");
            LB1.Items[5].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[6].SubItems.Add("90Mbps");
            LB1.Items[6].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[7].SubItems.Add("90Mbps");
            LB1.Items[7].SubItems.Add("yuv422p10");
            LB1.Items.Add("1280x720p");
            LB1.Items[8].SubItems.Add("110Mbps");
            LB1.Items[8].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[9].SubItems.Add("120Mbps");
            LB1.Items[9].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[10].SubItems.Add("145Mbps");
            LB1.Items[10].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[11].SubItems.Add("180Mbps");
            LB1.Items[11].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[12].SubItems.Add("180Mbps");
            LB1.Items[12].SubItems.Add("yuv422p10");
            LB1.Items.Add("1280x720p");
            LB1.Items[13].SubItems.Add("180Mbps");
            LB1.Items[13].SubItems.Add("yuv422p10");
            LB1.Items.Add("1280x720p");
            LB1.Items[14].SubItems.Add("220Mbps");
            LB1.Items[14].SubItems.Add("yuv422p");
            LB1.Items.Add("1280x720p");
            LB1.Items[15].SubItems.Add("220Mbps");
            LB1.Items[15].SubItems.Add("yuv422p10");
            LB1.Items.Add("---------------------------------");
            LB1.Items[16].SubItems.Add("---------------------------------");
            LB1.Items[16].SubItems.Add("---------------------------------");
            LB1.Items.Add("1440x1080p");
            LB1.Items[17].SubItems.Add("63Mbps");
            LB1.Items[17].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080p");
            LB1.Items[18].SubItems.Add("84Mbps");
            LB1.Items[18].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080p");
            LB1.Items[19].SubItems.Add("100Mbps");
            LB1.Items[19].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080p");
            LB1.Items[20].SubItems.Add("110Mbps");
            LB1.Items[20].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[21].SubItems.Add("80Mbps");
            LB1.Items[21].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[22].SubItems.Add("90Mbps");
            LB1.Items[22].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[23].SubItems.Add("100Mbps");
            LB1.Items[23].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[24].SubItems.Add("110Mbps");
            LB1.Items[24].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[25].SubItems.Add("120Mbps");
            LB1.Items[25].SubItems.Add("yuv422p");
            LB1.Items.Add("1440x1080i");
            LB1.Items[26].SubItems.Add("145Mbps");
            LB1.Items[26].SubItems.Add("yuv422p");
            LB1.Items.Add("---------------------------------");
            LB1.Items[27].SubItems.Add("---------------------------------");
            LB1.Items[27].SubItems.Add("---------------------------------");
            LB1.Items.Add("1920x1080i");
            LB1.Items[28].SubItems.Add("120Mbps");
            LB1.Items[28].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080i");
            LB1.Items[29].SubItems.Add("145Mbps");
            LB1.Items[29].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080i");
            LB1.Items[30].SubItems.Add("185Mbps");
            LB1.Items[30].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080i");
            LB1.Items[31].SubItems.Add("185Mbps");
            LB1.Items[31].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080i");
            LB1.Items[32].SubItems.Add("220Mbps");
            LB1.Items[32].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080i");
            LB1.Items[33].SubItems.Add("220Mbps");
            LB1.Items[33].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[34].SubItems.Add("36Mbps");
            LB1.Items[34].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[35].SubItems.Add("45Mbps");
            LB1.Items[35].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[36].SubItems.Add("75Mbps");
            LB1.Items[36].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[37].SubItems.Add("90Mbps");
            LB1.Items[37].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[38].SubItems.Add("115Mbps");
            LB1.Items[38].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[39].SubItems.Add("120Mbps");
            LB1.Items[39].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[40].SubItems.Add("145Mbps");
            LB1.Items[40].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[41].SubItems.Add("175Mbps");
            LB1.Items[41].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[42].SubItems.Add("185Mbps");
            LB1.Items[42].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[43].SubItems.Add("220Mbps");
            LB1.Items[43].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[44].SubItems.Add("240Mbps");
            LB1.Items[44].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[45].SubItems.Add("290Mbps");
            LB1.Items[45].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[46].SubItems.Add("365Mbps");
            LB1.Items[46].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[47].SubItems.Add("440Mbps");
            LB1.Items[47].SubItems.Add("yuv422p");
            LB1.Items.Add("1920x1080p");
            LB1.Items[48].SubItems.Add("175Mbps");
            LB1.Items[48].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[49].SubItems.Add("185Mbps");
            LB1.Items[49].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[50].SubItems.Add("365Mbps");
            LB1.Items[50].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[51].SubItems.Add("440Mbps");
            LB1.Items[51].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[52].SubItems.Add("350Mbps");
            LB1.Items[52].SubItems.Add("yuv422p10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[53].SubItems.Add("390Mbps");
            LB1.Items[53].SubItems.Add("yuv444p10, gbrp10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[54].SubItems.Add("440Mbps");
            LB1.Items[54].SubItems.Add("yuv444p10, gbrp10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[55].SubItems.Add("730Mbps");
            LB1.Items[55].SubItems.Add("yuv444p10, gbrp10");
            LB1.Items.Add("1920x1080p");
            LB1.Items[56].SubItems.Add("880Mbps");
            LB1.Items[56].SubItems.Add("yuv444p10, gbrp10");

            frmInfo.StartPosition = FormStartPosition.CenterScreen;
            frmInfo.ShowDialog();
        }

        private void boton_ok_Click(object sender, System.EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void radio_2pass_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = images.Images[2];
            if (lv1_item == String.Empty)
            {
                lbl_two.Text = FFBatch.Properties.Strings.list_empty;
                lbl_silence.Text = String.Empty;
                lbl_img_v.Text = String.Empty;
                pic_warn_two.Visible = true;
                pic_warn_silence.Visible = false;
                pic_warn_img_v.Visible = false;
                wizardControl1.Pages[1].AllowNext = false;
            }
            else
            {
                lbl_two.Text = String.Empty;
                lbl_silence.Text = String.Empty;
                lbl_img_v.Text = String.Empty;
                pic_warn_two.Visible = false;
                pic_warn_img_v.Visible = false;
                wizardControl1.Pages[1].AllowNext = true;
            }
        }

        Boolean IsDigitsOnly(String str)
        {
            foreach (char c in str)
            {
                if ((c < '0' || c > '9') && c != '.')
                    return false;
            }

            return true;
        }
        private void BG1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.InvokeEx(f => this.Cursor = Cursors.WaitCursor);
            this.InvokeEx(f => f.wizardControl1.Enabled = false);
            this.InvokeEx(f => f.wizardControl1.Pages[6].AllowNext = false);
            ListBox LB1_o = new ListBox();
            Process consola_pre = new Process();
            String file_prueba = "";
            String sel_test = "";
            sel_test = lv1_item;
            Double dur_secs = 0;
            if (lv1_dur.Length > 1) dur_secs = TimeSpan.Parse(lv1_dur).TotalSeconds;

            file_prueba = sel_test;
            String destino_test = Path.GetTempPath() + "\\" + "FFBatch_test";
            Boolean bad_chars = false;
            Boolean unsupported = false;

            Task tt = Task.Run(() =>
            {
                String fichero = Path.GetFileName(file_prueba);

                if (!Directory.Exists(destino_test))
                {
                    try
                    {
                        Directory.CreateDirectory(destino_test);
                    }
                    catch (System.Exception excpt)
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.error + " " + excpt.Message, FFBatch.Properties.Strings.write_error2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;
                    }
                }

                String ext_output = String.Empty;
                this.InvokeEx(f => ext_output = cb_container.SelectedItem.ToString());

                String textbox_params = wiz_params;
                String file_prueba2 = file_prueba;
                
                while (textbox_params.Contains("%fn"))
                {
                    if (textbox_params.Contains("%fn"))
                    {
                        textbox_params = textbox_params.Replace("%fn", Path.GetFileNameWithoutExtension(file_prueba));
                    }
                }

                while (textbox_params.Contains("%ff"))
                {
                    if (textbox_params.Contains("%ff"))
                    {
                        textbox_params = textbox_params.Replace("%ff", Path.GetFileName(file_prueba));
                    }
                }
                                
                String to_replace = String.Empty;
                int addit = 0;

                while (textbox_params.Contains("%fdur"))
                {
                    if (textbox_params.Contains("%fdur"))
                    {

                        to_replace = "";
                        addit = 0;

                        String operation = "";
                        if (textbox_params.Substring(textbox_params.LastIndexOf("%fdur") + 5, 1) == "+")
                        {
                            operation = "+";
                        }
                        else if (textbox_params.Substring(textbox_params.LastIndexOf("%fdur") + 5, 1) == "-")
                        {
                            operation = "-";
                        }
                        else if (textbox_params.Substring(textbox_params.LastIndexOf("%fdur") + 5, 1) == "*")
                        {
                            operation = "*";
                        }
                        else if (textbox_params.Substring(textbox_params.LastIndexOf("%fdur") + 5, 1) == "/")
                        {
                            operation = "/";
                        }

                        else operation = String.Empty;

                        if (operation != String.Empty)
                        {
                            int operador = textbox_params.LastIndexOf("%fdur") + 5;
                            int length = 0;
                            int limit = textbox_params.Length - operador - 1;

                            for (int ii = 1; ii < limit + 1; ii++)
                            {
                                if (IsDigitsOnly(textbox_params.Substring(operador + ii, 1)))
                                {
                                    length = ii;
                                }
                                else break;
                            }

                            addit = Convert.ToInt32(textbox_params.Substring(operador + 1, length).Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));

                            if (operation == "+") addit = (int)dur_secs + addit;
                            if (operation == "-") addit = (int)dur_secs - addit;
                            if (operation == "*") addit = (int)dur_secs * addit;
                            if (operation == "/") addit = (int)dur_secs / addit;

                            to_replace = textbox_params.Substring(textbox_params.LastIndexOf("%fdur"), 6 + length);
                        }

                        //END %fdur variable operations
                        if (operation == String.Empty) textbox_params = textbox_params.Replace("%fdur", dur_secs.ToString());
                        else textbox_params = textbox_params.Replace(to_replace, addit.ToString());
                    }
                }

                while (textbox_params.Contains("%fp"))
                {
                    if (textbox_params.Contains("%fp"))
                    {
                        textbox_params = textbox_params.Replace("%fp", Path.GetDirectoryName(file_prueba));
                    }
                }

                while (textbox_params.Contains("%fd"))
                {
                    if (textbox_params.Contains("%fd"))
                    {
                        var dirName = new DirectoryInfo(Path.GetDirectoryName(file_prueba)).Name;
                        textbox_params = textbox_params.Replace("%fd", dirName);
                    }
                }

                while (textbox_params.Contains("%1"))
                {
                    if (textbox_params.Contains("%1"))
                    {
                        file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                        file_prueba2 = file_prueba2.Replace(":", ":" + "\\\\");
                        textbox_params = textbox_params.Replace("%1", '\u0022' + file_prueba2 + '\u0022');
                    }
                }

                while (textbox_params.Contains("%2"))
                {
                    if (textbox_params.Contains("%2"))
                    {
                        file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                        file_prueba2 = file_prueba2.Replace(":", ":" + "\\\\");
                        textbox_params = textbox_params.Replace("%2", '\u0022' + Path.Combine(System.IO.Path.GetDirectoryName(file_prueba2), Path.GetFileNameWithoutExtension(file_prueba2)) + '\u0022');
                    }
                }

                while (textbox_params.Contains("%pff"))
                {
                    if (textbox_params.Contains("%pff"))
                    {
                        file_prueba2 = file_prueba2.Replace("\\", "/\\");
                        file_prueba2 = file_prueba2.Replace(":", "\\\\://\\");

                        textbox_params = textbox_params.Replace("%pff", Path.GetDirectoryName(file_prueba2));
                    }
                }

                while (textbox_params.Contains("%f1"))
                {
                    if (textbox_params.Contains("%f1"))
                    {
                        file_prueba2 = file_prueba2.Replace("\\", "/\\");
                        file_prueba2 = file_prueba2.Replace(":", "\\\\:");

                        textbox_params = textbox_params.Replace("%f1", file_prueba2);
                    }
                }

                while (textbox_params.Contains("%f2"))
                {
                    if (textbox_params.Contains("%f2"))
                    {
                        file_prueba2 = file_prueba2.Replace("\\", "/\\");
                        file_prueba2 = file_prueba2.Replace(":", "\\\\://\\");
                        textbox_params = textbox_params.Replace("%f2", Path.Combine(Path.GetDirectoryName(file_prueba2), Path.GetFileNameWithoutExtension(file_prueba2)));
                    }
                }

                consola_pre.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
                consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " -y " + textbox_params + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + ext_output + '\u0022';
                consola_pre.StartInfo.RedirectStandardError = true;
                consola_pre.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                consola_pre.StartInfo.UseShellExecute = false;
                consola_pre.StartInfo.CreateNoWindow = true;
                consola_pre.EnableRaisingEvents = true;                
                consola_pre.Start();

                while (!consola_pre.StandardError.EndOfStream)
                {
                    this.InvokeEx(f => LB1_o.Items.Add(consola_pre.StandardError.ReadLine()));
                    this.InvokeEx(f => LB1_o.TopIndex = LB1_o.Items.Count - 1);
                    this.InvokeEx(f => LB1_o.Refresh());
                }
                consola_pre.WaitForExit();
                consola_pre.StartInfo.Arguments = String.Empty;
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);
            });

            if (!tt.Wait(1500) && consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                pic_status.Image = img_status.Images[1];
                preset_ok = true;
                this.InvokeEx(f => lbl_help.Text = FFBatch.Properties.Strings.sel_seq_mul);
                tried_ok = true;
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);

                if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                {
                    foreach (String file in Directory.GetFiles(destino_test))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch { }
                    }
                }
               
                LB1_o.Items.Clear();
                this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                return;
            }

            if (bad_chars == false)
            {
                if (consola_pre.ExitCode != 0)
                {
                    pic_status.Image = img_status.Images[2];
                    preset_ok = false;
                    this.InvokeEx(f => lbl_help.Text = FFBatch.Properties.Strings.invalid_p);
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        try { System.IO.Directory.Delete(destino_test); } catch { }
                    }

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    foreach (String lin in LB1_o.Items)
                    {
                        if (lin.Contains("not load the requested plugin") || lin.Contains("Cannot load nvcuda.dll"))
                        {
                            unsupported = true;
                        }
                    }
                    if (unsupported == true) MessageBox.Show(FFBatch.Properties.Strings.test_fail1 + " " + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.unsup_enc + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + Properties.Strings.try_pr, Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else this.InvokeEx(f => MessageBox.Show(this, FFBatch.Properties.Strings.test_fail1 + " " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error));

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    tried_ok = false;
                    return;
                }
                else
                {
                    System.Threading.Thread.Sleep(50);
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }
                    pic_status.Image = img_status.Images[1];
                    preset_ok = true;
                    this.InvokeEx(f => lbl_help.Text = FFBatch.Properties.Strings.sel_seq_mul);
                    tried_ok = true;
                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                }
            }
            //END try preset
            this.InvokeEx(f => this.Cursor = Cursors.Arrow);

            if (Directory.Exists(destino_test))
            {
                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
            }
            LB1_o.Items.Clear();
            consola_pre.Dispose();
        }

        private void BG1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            String file_prueba = "";
            String sel_test = lv1_item;
            file_prueba = sel_test;
            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + wiz_ext;

            if (File.Exists(borrar))
            {
                try
                {
                    File.Delete(borrar);
                }
                catch
                {
                }
            }

            if (Directory.Exists(destino) == true)
            {
                try
                {
                    if (Directory.GetFiles(destino).Length == 0)
                    {
                        System.IO.Directory.Delete(destino);
                    }
                }
                catch { }
            }
            this.InvokeEx(f => f.wizardControl1.Enabled = true);
            wizardControl1.Pages[6].AllowNext = true;
        }

        private void btn_status_Click(object sender, EventArgs e)
        {
            if (audio_preset == true) video_encoder_param = "-vn";
            wiz_params = video_encoder_param + " " + audio_encoder_param;
            if (chk_mapall.CheckState == CheckState.Checked)
            {
                wiz_params = "-map 0 " + wiz_params;
            }
            if (chk_subs_copy.CheckState == CheckState.Checked)
            {
                wiz_params = wiz_params + " -c:s copy";
            }

            if (chk_save_pres.CheckState == CheckState.Checked && txt_preset_name.Text != String.Empty)
            {
                wiz_preset = txt_preset_name.Text;
                wiz_save_preset = true;
            }
            else
            {
                wiz_preset = String.Empty;
                wiz_save_preset = false;
            }

            wiz_ext = cb_container.Text;
            if (lv1_item != String.Empty)
                try
                {
                    BG1.RunWorkerAsync();
                }
                catch { }
        }

        private void check_pr()
        {
            this.Cursor = Cursors.WaitCursor;
            wizardControl1.Pages[4].AllowNext = false;
            ListBox LB1_o = new ListBox();
            Process consola_pre = new Process();
            String file_prueba = "";
            String sel_test = "";
            sel_test = lv1_item;
            file_prueba = sel_test;
            String destino_test = Path.GetTempPath() + "\\" + "FFBatch_test";
            Boolean bad_chars = false;
            Boolean unsupported = false;

            String fichero = Path.GetFileName(file_prueba);

            if (!Directory.Exists(destino_test))
            {
                try
                {
                    Directory.CreateDirectory(destino_test);
                }
                catch (System.Exception excpt)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.error + " " + excpt.Message, FFBatch.Properties.Strings.write_error2, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Cursor = Cursors.Arrow;
                    return;
                }
            }

            String ext_output = String.Empty;
            ext_output = cb_container.SelectedItem.ToString();

            String textbox_params = wiz_params;
            String file_prueba3 = file_prueba;
            if (textbox_params.Contains("%1"))
            {
                if (file_prueba3.Contains("[") || file_prueba3.Contains("]"))
                {
                    MessageBox.Show(FFBatch.Properties.Strings.conflict_char, FFBatch.Properties.Strings.conflict_char2, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Arrow;
                    bad_chars = true;
                    return;
                }
                file_prueba3 = file_prueba3.Replace("\\", "\\\\\\\\");
                file_prueba3 = file_prueba3.Replace(":", "\\\\" + ":");
                textbox_params = textbox_params.Replace("%1", file_prueba3);
            }

            consola_pre.StartInfo.FileName = Path.Combine(Properties.Settings.Default.ffm_path, "ffmpeg.exe");
            consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " -y " + textbox_params + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + ext_output + '\u0022';
            consola_pre.StartInfo.RedirectStandardOutput = true;
            consola_pre.StartInfo.RedirectStandardError = true;
            consola_pre.StartInfo.UseShellExecute = false;
            consola_pre.StartInfo.CreateNoWindow = true;
            consola_pre.EnableRaisingEvents = true;
            consola_pre.Start();

            while (!consola_pre.StandardError.EndOfStream)
            {
                LB1_o.Items.Add(consola_pre.StandardError.ReadLine());
                LB1_o.TopIndex = LB1_o.Items.Count - 1;
                LB1_o.Refresh();
            }
            consola_pre.WaitForExit(1000);
            consola_pre.StartInfo.Arguments = String.Empty;
            this.InvokeEx(f => this.Cursor = Cursors.Arrow);

            if (bad_chars == false)
            {
                if (consola_pre.ExitCode != 0)
                {
                    pic_status.Image = img_status.Images[2];
                    preset_ok = false;
                    lbl_help.Text = FFBatch.Properties.Strings.invalid_p;
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }

                    this.Cursor = Cursors.Arrow;
                    foreach (String lin in LB1_o.Items)
                    {
                        if (lin.Contains("not load the requested plugin") || lin.Contains("Cannot load nvcuda.dll"))
                        {
                            unsupported = true;
                        }
                    }
                    if (unsupported == true) MessageBox.Show(FFBatch.Properties.Strings.test_fail1 + " " + Environment.NewLine + Environment.NewLine + FFBatch.Properties.Strings.unsup_enc + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show(FFBatch.Properties.Strings.test_fail1 + " " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    this.Cursor = Cursors.Arrow;
                    tried_ok = false;
                    return;
                }
                else
                {
                    System.Threading.Thread.Sleep(50);
                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                    {
                        foreach (String file in Directory.GetFiles(destino_test))
                        {
                            try
                            {
                                File.Delete(file);
                            }
                            catch
                            {
                            }
                        }
                    }

                    if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length == 0)
                    {
                        System.IO.Directory.Delete(destino_test);
                    }
                    pic_status.Image = img_status.Images[1];
                    preset_ok = true;
                    lbl_help.Text = FFBatch.Properties.Strings.sel_seq_mul;
                    tried_ok = true;
                    this.Cursor = Cursors.Arrow;
                }
            }

            if (consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                pic_status.Image = img_status.Images[1];
                preset_ok = true;
                lbl_help.Text = FFBatch.Properties.Strings.sel_seq_mul;
                tried_ok = true;
                this.Cursor = Cursors.Arrow;

                if (Directory.Exists(destino_test) && Directory.GetFiles(destino_test).Length > 0)
                {
                    foreach (String file in Directory.GetFiles(destino_test))
                    {
                        try
                        {
                            File.Delete(file);
                        }
                        catch { }
                    }
                }

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
                LB1_o.Items.Clear();
                this.Cursor = Cursors.Arrow;
                return;
            }

            //END try preset
            this.Cursor = Cursors.Arrow;

            if (Directory.Exists(destino_test))
            {
                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
                }
            }
            LB1_o.Items.Clear();
            consola_pre.Dispose();

            String file_prueba2 = "";
            String sel_test2 = lv1_item;
            file_prueba2 = sel_test2;
            String destino = Path.Combine(Path.GetTempPath(), "FFBatch_test");
            String borrar = destino + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba2) + "." + wiz_ext;

            if (File.Exists(borrar))
            {
                try
                {
                    File.Delete(borrar);
                }
                catch
                {
                }
            }

            if (Directory.Exists(destino) == true)
            {
                try
                {
                    if (Directory.GetFiles(destino).Length == 0)
                    {
                        System.IO.Directory.Delete(destino);
                    }
                }
                catch { }
            }
            wizardControl1.Pages[4].AllowNext = true;
        }

        private void chk_mapall_CheckedChanged(object sender, EventArgs e)
        {
            pic_status.Image = img_status.Images[0];
            preset_ok = false;
            if (lv1_item != String.Empty) BG1.RunWorkerAsync();
        }

        private void chk_subs_copy_CheckedChanged(object sender, EventArgs e)
        {
            pic_status.Image = img_status.Images[0];
            preset_ok = false;
            if (lv1_item != String.Empty) BG1.RunWorkerAsync();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = images.Images[3];
            if (lv1_item == String.Empty)
            {
                lbl_silence.Text = FFBatch.Properties.Strings.list_empty;
                pic_warn_silence.Visible = true;
                lbl_img_v.Text = String.Empty;
                lbl_two.Text = String.Empty;
                pic_warn_two.Visible = false;
                pic_warn_img_v.Visible = false;
                wizardControl1.Pages[1].AllowNext = false;
            }
            else
            {
                lbl_silence.Text = String.Empty;
                lbl_img_v.Text = String.Empty;
                lbl_two.Text = String.Empty;
                pic_warn_silence.Visible = false;
                pic_warn_img_v.Visible = false;
                wizardControl1.Pages[1].AllowNext = true;
            }
        }

        private void radio_images_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = images.Images[4];
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            lbl_two.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            wizardControl1.Pages[1].AllowNext = true;
        }

        private void chk_start_CheckedChanged(object sender, EventArgs e)
        {
            String f_show_wiz = String.Empty;
            if (is_portable == false)
            {
                f_show_wiz = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_show_wiz.ini";
            }
            else
            {
                f_show_wiz = port_path + "ff_show_wiz_portable.ini";
            }

            if (chk_start.CheckState == CheckState.Unchecked)
            {
                try
                {
                    File.WriteAllText(f_show_wiz, "");
                }
                catch { }
                chk_start_0.CheckState = CheckState.Unchecked;
            }
            else
            {
                try
                {
                    File.Delete(f_show_wiz);
                }
                catch { }
                chk_start_0.CheckState = CheckState.Checked;
            }
        }

        private void radio_split_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = images.Images[5];
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            lbl_two.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            wizardControl1.Pages[1].AllowNext = true;
        }

        private void wz_end_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (preset_ok != true && lv1_item != String.Empty)
            {
                DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.pres_inv, Properties.Strings.error, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (a == DialogResult.Yes) e.Cancel = false;
                else e.Cancel = true;
            }
        }

        private void wz0_0_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            if (lv1_item != String.Empty || start_no_files == false) wizardControl1.NextPage();
        }

        private void btn_add_files_Click(object sender, EventArgs e)
        {
            add_files = true;
            add_folder = false;
            this.Close();
        }

        private void btn_add_folders_Click(object sender, EventArgs e)
        {
            add_files = false;
            add_folder = true;
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            String f_show_wiz = String.Empty;
            if (is_portable == false)
            {
                f_show_wiz = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_show_wiz.ini";
            }
            else
            {
                f_show_wiz = port_path + "ff_show_wiz_portable.ini";
            }

            if (chk_start_0.CheckState == CheckState.Unchecked)
            {
                try
                {
                    File.WriteAllText(f_show_wiz, "");
                }
                catch { }
                chk_start.CheckState = CheckState.Unchecked;
            }
            else
            {
                try
                {
                    File.Delete(f_show_wiz);
                }
                catch { }
                chk_start.CheckState = CheckState.Checked;
            }
        }

        private void radio_existing_CheckedChanged(object sender, EventArgs e)
        {
            audio_preset = false;
            video_preset = false;
            existing_preset = true;
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            pic_1.Image = images.Images[6];
            wizardControl1.Pages[1].AllowNext = true;
        }
                
        private void combo_presets_ext_SelectedIndexChanged(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (combo_presets_ext.SelectedIndex == 0)
            {
                String path = "";
                if (is_portable == true)
                {
                    path = port_path + "ff_batch_portable.ini";
                    if (!Directory.Exists(System.IO.Path.Combine(Application.StartupPath, "settings")))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Application.StartupPath, "settings"));
                    }
                }
                else
                {
                    path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
                    if (!Directory.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch")))
                    {
                        Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
                    }
                }

                if (!File.Exists(path))
                {
                    File.WriteAllText(path, "-c copy" + "\n" + "mp4" + "\n" + "yes" + "\n" + "Vs" + "\n" + "grid_yes" + "\n" + "keep_no" + "\n" + "subf_no");
                }

                int linea = 0;
                String ext1 = "";
                String pres1 = "";
                foreach (string line in File.ReadLines(path))
                {
                    if (linea == 0) pres1 = line;
                    if (linea == 1) ext1 = line;
                    if (linea > 1) break;
                    linea = linea + 1;
                }
                txt_ext_format.Text = ext1;
                txt_pr_1_ex.Text = pres1;
            }
            else
            {
                String path = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
                if (is_portable == true) path = port_path + "ff_presets_portable.ini";
                String pre_name = String.Empty;
                int i = 0;
                foreach (string line in File.ReadLines(path))
                {
                    if (line != "" && line.Contains("Version ") == false)
                    {
                        if (line.Substring(4, line.IndexOf("&") - 5) == combo_presets_ext.SelectedItem.ToString())
                        {
                            int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");

                            txt_ext_format.Text = line.Substring(line.LastIndexOf("%") + 2);
                            txt_pr_1_ex.Text = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                        }
                    }
                    i = i + 1;
                }
            }
        }

        private void wz_0_1_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            sel_preset = String.Empty;
            combo_presets_ext.Items.Clear();
            combo_presets_ext.Items.Add(Properties.Strings.default_param);
            String path, path_pr = "";

            path_pr = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch.ini";
            if (is_portable == true)
            {
                path_pr = port_path + "ff_presets_portable.ini";
                path = port_path + "ff_batch_portable.ini";
            }
            int linea = 0;
            String ext1 = "";
            String pres1 = "";

            combo_presets_ext.SelectedIndex = combo_presets_ext.FindString(Properties.Strings.default_param);
            foreach (string line in File.ReadLines(path))
            {
                if (linea == 0) pres1 = line;
                if (linea == 1) ext1 = line;
                if (linea > 1) break;
                linea = linea + 1;
            }
            if (txt_pr_1.Text.Length == 0)
            {
                txt_ext_1.Text = ext1;
                txt_pr_1.Text = pres1;
            }

            foreach (string line in File.ReadLines(path_pr))
            {
                if (line.Contains("PR: "))
                {
                    combo_presets_ext.Items.Add(line.Substring(4, line.LastIndexOf("&") - 5));
                }
            }            
        }

        private void wz_0_1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            video_encoder_param = txt_pr_1_ex.Text;
            sel_preset = combo_presets_ext.Text;
        }

        private void btn_skip_files_Click(object sender, EventArgs e)
        {
            wizardControl1.NextPage();
        }

        private void AeroWizard1_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (Properties.Settings.Default.app_lang == "zh-Hans") this.Height = this.Height + 35;
            if (Properties.Settings.Default.app_lang != "en" && Properties.Settings.Default.app_lang != "es")
            {
                wizardControl1.NextButtonText = Properties.Strings.next;
                wizardControl1.CancelButtonText = Properties.Strings.cancel;
                wizardControl1.FinishButtonText = Properties.Strings.finish;
            }
            temp_dur = lv1_dur;            
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard1));
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

        private void radio_img_v_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = pic_img_v.Image;
            if (lv1_item == String.Empty)
            {
                lbl_img_v.Text = FFBatch.Properties.Strings.list_empty;
                pic_warn_silence.Visible = false;
                lbl_two.Text = String.Empty;
                lbl_silence.Text = String.Empty;
                pic_warn_two.Visible = false;
                pic_warn_img_v.Visible = true;
                wizardControl1.Pages[1].AllowNext = false;
            }
            else
            {
                lbl_img_v.Text = String.Empty;
                lbl_two.Text = String.Empty;
                lbl_silence.Text = String.Empty;
                pic_warn_silence.Visible = false;
                pic_warn_two.Visible = false;
                wizardControl1.Pages[1].AllowNext = true;
            }
        }

        private void check_internet()
        {
            internet_up = true;

            System.Threading.Thread.CurrentThread.IsBackground = true;

            String content2 = String.Empty;
            try
            {
                WebClient client = new WebClientWithTimeout();
                Stream stream = client.OpenRead("http://google.com/generate_204");
                StreamReader reader = new StreamReader(stream);
                String content = reader.ReadToEnd();
                content2 = content;
            }
            catch
            {
                try
                {
                    //Backup server
                    WebClient client = new WebClientWithTimeout();
                    Stream stream = client.OpenRead("http://www.baidu.com/baidu.html?from=noscript");
                    StreamReader reader = new StreamReader(stream);
                    String content = reader.ReadToEnd();
                    content2 = content;
                }
                catch
                {
                    DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.err_connect, FFBatch.Properties.Strings.err_con2, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (a == DialogResult.OK) internet_up = true;
                    else internet_up = false;
                }
            }
        }

        private void btn_tips_1_Click(object sender, EventArgs e)
        {
            check_internet();
            if (internet_up == false) return;
            if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc") Process.Start("https://sourceforge.net/p/ffmpeg-batch/news/2021/10/nvidia-h264nvenc");
            if (Combo_encoders.SelectedItem.ToString() == "hevc_nvenc") Process.Start("https://sourceforge.net/p/ffmpeg-batch/news/2021/10/nvidia-hevcnvenc");
            if (Combo_encoders.SelectedItem.ToString() == "h264_amf") Process.Start("https://sourceforge.net/p/ffmpeg-batch/news/2021/09/amd-vce--h264amf-encoder-options");
            if (Combo_encoders.SelectedItem.ToString() == "hevc_amf") Process.Start("https://sourceforge.net/p/ffmpeg-batch/news/2021/08/amd-vce--hevcamf-encoder-options");
            if (Combo_encoders.SelectedItem.ToString() == "av1_nvenc") Process.Start("https://docs.nvidia.com/video-technologies/video-codec-sdk/ffmpeg-with-nvidia-gpu/");
            if (Combo_encoders.SelectedItem.ToString() == "libsvtav1") Process.Start("https://gitlab.com/AOMediaCodec/SVT-AV1/-/blob/master/Docs/Parameters.md");            
        }

        public class WebClientWithTimeout : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest wr = base.GetWebRequest(address);
                wr.Timeout = 5000; // timeout in milliseconds (ms)
                return wr;
            }
        }

        private void cb_deint_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_de_mode.SelectedIndex = 0;
            cb_de_parity.SelectedIndex = 0;
            cb_de_deint.SelectedIndex = 0;
        }

        private void n_speed_ValueChanged(object sender, EventArgs e)
        {
            
            if (n_speed.Value != 0)
            {
                foreach (Control ct in wz1_1.Controls)
                    if (ct.Name != n_speed.Name) ct.Enabled = false;
                cb_resize.SelectedIndex = -1;
                cb_crop.SelectedIndex = -1;
                cb_deint.SelectedIndex = -1;
                cb_rotate.SelectedIndex = -1;
                cb_de_mode.SelectedIndex = -1;
                cb_de_parity.SelectedIndex = -1;
                cb_de_deint.SelectedIndex = -1;
                n_speed.Enabled = true;
                if (warn_spf == true)
                {
                    MessageBox.Show(Properties.Strings.speed_not_comp);
                    warn_spf = false;
                }
            }
            else
            {
                foreach (Control ct in wz1_1.Controls) ct.Enabled = true;
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            foreach (var ct in wz1_1.Controls.OfType<ComboBox>())
            {
                ct.SelectedIndex = -1;
            }
            cb_de_mode.SelectedIndex = -1;
            cb_de_parity.SelectedIndex = -1;
            cb_de_deint.SelectedIndex = -1;
            cb_rotate.SelectedIndex = 0;
        }

        private void wz1_1_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            video_encoder_param = String.Empty;
            commit_video_1();
        }

        private void wz1_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {
            wz_0_1.Suppress = true;
            if (started_v == false) Combo_encoders.SelectedIndex = Properties.Settings.Default.wiz_vid;
            started_v = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            online_pr = true;
            this.Close();
        }

        private void n_width_ValueChanged(object sender, EventArgs e)
        {
            get_aspect();
        }

        private void n_height_ValueChanged(object sender, EventArgs e)
        {
            get_aspect();            
        }

        private void get_aspect()
        {
            if (n_width.Value == 0 || n_height.Value == 0)
            {
                lbl_a_ratio.Text = "";
                return;
            }
            lbl_a_ratio.Text = Math.Round(n_width.Value / n_height.Value, 2).ToString();
            Decimal r = Math.Round(n_width.Value / n_height.Value, 6);
            Decimal r2 = Math.Round(n_width.Value / n_height.Value, 3);
            Decimal pan = (decimal)1.777778;
            Decimal box = (decimal)1.333333;
            Decimal box2 = (decimal)1.250000;
            Decimal box3 = (decimal)1.500000;
            Decimal box4 = (decimal)1.666667;
            Decimal pan_cine = (decimal)2.400000;
            Decimal pan_pan = (decimal)2.350;
            Decimal pan_us = (decimal)1.850;
            Decimal wide = (decimal)2.000;

            if (r == pan) lbl_a_ratio.Text = "16:9";
            if (r == pan_cine) lbl_a_ratio.Text = "2:40:1";
            if (r == box) lbl_a_ratio.Text = "4:3";
            if (r == box2) lbl_a_ratio.Text = "5:4";
            if (r == box3) lbl_a_ratio.Text = "3:2";
            if (r == box4) lbl_a_ratio.Text = "5:3";
            if (r2 == pan_pan) lbl_a_ratio.Text = "2:35:1";
            if (r2 == pan_us) lbl_a_ratio.Text = "1:85:1";
            if (r2 == wide) lbl_a_ratio.Text = "2:1";
        }

        private void btn_crop_wiz_Click(object sender, EventArgs e)
        {            
            if (lv1_item.Length  == 0)
            {
                MessageBox.Show(Properties.Strings.list_empty);
                return;
            }
            cb_crop.SelectedIndex = 0;
            Form28 frm28 = new Form28();
            frm28.StartPosition = FormStartPosition.CenterParent;
            frm28.item = lv1_item;
            frm28.dur = lv1_dur;
            frm28.ShowDialog();
            auto_crop = frm28.vf_crop;
            aspect = frm28.aspect_r;
            if (auto_crop.Length > 0) btn_crop_wiz.BackColor = Color.LightGreen;
            else btn_crop_wiz.BackColor = Color.White;
        }

        private void l_crop_ValueChanged(object sender, EventArgs e)
        {
            auto_crop = String.Empty;
            btn_crop_wiz.BackColor = Color.White;
        }

        private void u_crop_ValueChanged(object sender, EventArgs e)
        {
            auto_crop = String.Empty;
            btn_crop_wiz.BackColor = Color.White;
        }

        private void r_crop_ValueChanged(object sender, EventArgs e)
        {
            auto_crop = String.Empty;
            btn_crop_wiz.BackColor = Color.White;
        }

        private void d_crop_ValueChanged(object sender, EventArgs e)
        {
            auto_crop = String.Empty;
            btn_crop_wiz.BackColor = Color.White;
        }

        private void chk_samples_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_samples.CheckState == CheckState.Checked) n_t_samples.Enabled = true;
            else n_t_samples.Enabled = false;
            pic_status.Image = img_status.Images[0];
            preset_ok = false;
            if (lv1_item != String.Empty) BG1.RunWorkerAsync();

        }

        private void radio_trailer_CheckedChanged(object sender, EventArgs e)
        {
            audio_preset = false;
            video_preset = true;
            existing_preset = false;
            lbl_two.Text = String.Empty;
            lbl_img_v.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_warn_img_v.Visible = false;
            pic_1.Image = images.Images[0];            
            wizardControl1.Pages[1].AllowNext = true;            
        }

        private void wz_trailer_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (lbl_dur.Text == "0" && lv1_item.Length > 0) e.Cancel = true;
            
            String ss_t = "";
            String to_t = "";
                        
            if (check_enable_trailer_start.Enabled && check_enable_trailer_start.Checked)
            {
                ss_t = "-ss " + txt_trailer_init.Text;
                if (chk_init_trailer_time.Enabled && chk_init_trailer_time.Checked)
                {
                    ss_t = "-ss %fdur" + txt_init_trailer_dur.Text;
                }
            }
            if (check_enable_trailer_end.Enabled && check_enable_trailer_end.Checked)
            {
                String to_time = "%fdur-" + (TimeSpan.Parse(txt_trailer_final.Text).TotalSeconds).ToString();
                to_t = "-to " + to_time;
                if (chk_dur_trail_end.Enabled && chk_dur_trail_end .Checked)
                {
                    to_t = "-to %fdur" + txt_trailer_end_dur.Text;
                }
            }
            trailer_vparam = "select='lt(mod(t," + n_interval_secs.Value.ToString() + ")," + n_fragment_secs.Value.ToString().Replace(",", ".") + ")',setpts=N/FRAME_RATE/TB";
            if (!chk_no_aud_tr.Checked) trailer_aparam = "aselect='lt(mod(t," + n_interval_secs.Value.ToString() + ")," + n_fragment_secs.Value.ToString().Replace(",", ".") + ")',asetpts=N/SR/TB";
            else trailer_aparam = "-an";
            
            pre_input = ss_t + " " + to_t;

            if (chk_trailer_dur.Checked)
            {
                trailer_vparam = "select='lt(mod(t,%fdur/" + n_trailer_secs.Value.ToString() + ")," + n_fragment_secs.Value.ToString().Replace(",", ".") + ")',setpts=N/FRAME_RATE/TB"; 
                trailer_aparam = "aselect='lt(mod(t,%fdur/" + n_trailer_secs.Value.ToString() + ")," + n_fragment_secs.Value.ToString().Replace(",", ".") + ")',asetpts=N/SR/TB" + " -t " + n_trailer_secs.Value.ToString() + " ";
            }
        }

        private void wz_trailer_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {            
            trailer_vparam = String.Empty;
            trailer_aparam = String.Empty;
            pre_input = String.Empty;
            if (lv1_item.Length > 0) lbl_file_tr.Text = Path.GetFileName(lv1_item);
            get_trailer_info();
        }

        private void get_fdur()
        {            
            String dur_secs = lv1_dur.ToString();
            int length = 0;
            String dur_2 = String.Empty;
            Double result_init = 0;
            Double result_end = 0;

            if (chk_init_trailer_time.Enabled && chk_init_trailer_time.Checked)
            {
                try
                { //Init

                    int limit = txt_init_trailer_dur.Text.Length - 1;

                    for (int ii = 0; ii < limit; ii++)
                    {
                        if (IsDigitsOnly(txt_init_trailer_dur.Text.Substring(ii, 1)))
                        {
                            length = ii + 1;
                        }
                        else break;
                    }

                    dur_2 = TimeSpan.Parse(dur_secs).TotalSeconds.ToString();
                    result_init = Convert.ToDouble(new DataTable().Compute(dur_2 + txt_init_trailer_dur.Text, null));
                    
                    TimeSpan t = TimeSpan.FromSeconds(result_init);
                    String tx_elapsed = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds);
                    
                    txt_trailer_init.Text = tx_elapsed;
                }
                catch { 
                    
                    txt_trailer_init.Text = "00:00:00";
                    result_init = 0;
                };
            }
            
            if (chk_dur_trail_end.Enabled && chk_dur_trail_end.Checked)
            {
                try //End
                {

                    length = 0;
                    int limit = txt_trailer_end_dur.Text.Length - 1;

                    for (int ii = 0; ii < limit; ii++)
                    {
                        if (IsDigitsOnly(txt_trailer_end_dur.Text.Substring(ii, 1)))
                        {
                            length = ii + 1;
                        }
                        else break;
                    }

                    dur_2 = TimeSpan.Parse(dur_secs).TotalSeconds.ToString();
                    result_end = Convert.ToDouble(new DataTable().Compute(dur_2 + txt_trailer_end_dur.Text, null));
                    //lbl_end_trailer.Text = Math.Round(result).ToString();
                    result_end = TimeSpan.Parse(lv1_dur).TotalSeconds - result_end;
                                        
                    TimeSpan t = TimeSpan.FromSeconds(result_end);

                    String tx_elapsed = string.Format("{0:D2}:{1:D2}:{2:D2}",
                        t.Hours,
                        t.Minutes,
                        t.Seconds);                    
                        txt_trailer_final.Text = tx_elapsed;
                }

                catch {
                    
                    txt_trailer_final.Text = "00:00:00";
                    result_end = 0;
                }
            }            
    }

        private void get_trailer_info()
        {
            if (lv1_dur.Length > 0 && n_fragment_secs.Value > 0)
            {
                Double dur_secs = TimeSpan.Parse(lv1_dur).TotalSeconds;                
                Decimal dur_secs_d = Convert.ToDecimal(dur_secs);
                Double temp_secs = 0;
                Decimal dur_start = 0;
                Decimal dur_end = 0;
                Double init_time = 0;
                Double end_time = 0;
                Decimal fragments = dur_secs_d / n_interval_secs.Value;
                Decimal length = fragments * Convert.ToDecimal(n_fragment_secs.Value.ToString());

                if (check_enable_trailer_start.Checked || check_enable_trailer_end.Checked)
                {
                    try
                    {

                        init_time = TimeSpan.Parse(txt_trailer_init.Text).TotalSeconds;
                        fragments = (dur_secs_d - (decimal)init_time) / n_interval_secs.Value;
                        length = fragments * Convert.ToDecimal(n_fragment_secs.Value.ToString());

                    }
                    catch { length = 0; }


                    if (check_enable_trailer_end.Checked)
                    {
                        try
                        {
                            end_time = TimeSpan.Parse(txt_trailer_final.Text).TotalSeconds;
                            fragments = (dur_secs_d - (decimal)end_time) / n_interval_secs.Value;
                            length = fragments * Convert.ToDecimal(n_fragment_secs.Value.ToString());
                        }
                        catch { length = 0; }

                    }

                    if (check_enable_trailer_start.Checked && check_enable_trailer_end.Checked)
                    {
                        init_time = TimeSpan.Parse(txt_trailer_init.Text).TotalSeconds;
                        end_time = TimeSpan.Parse(txt_trailer_final.Text).TotalSeconds;
                        Decimal dur2 = (dur_secs_d - (decimal)init_time - (decimal)end_time);
                        if (dur2 <= 0) dur2 = 0;
                        fragments = ((dur_secs_d - (decimal)init_time - (decimal)end_time)) / n_interval_secs.Value;
                        length = fragments * Convert.ToDecimal(n_fragment_secs.Value.ToString());
                    }
                }

                if (chk_trailer_dur.Checked == false)
                {                 
                    if (length <= 0) length = 0;
                    lbl_dur.Text = Math.Ceiling(length).ToString();
                }
                else
                {
                    //fragments = n_trailer_secs.Value / n_fragment_secs.Value;                    
                    //lbl_dur.Text = Math.Ceiling(fragments).ToString();
                    lbl_dur.Text = n_trailer_secs.Value.ToString();
                    Double dur = 0;
                    if (lv1_dur.Length > 0)
                    {                        
                        dur = TimeSpan.Parse(lv1_dur).TotalSeconds;
                        if ((decimal)dur > n_trailer_secs.Value) n_interval_secs.Value = Math.Round((decimal)dur / n_fragment_secs.Value / n_trailer_secs.Value);
                        else
                        {
                            n_trailer_secs.Value = (decimal)dur;
                        }                        
                    }
                }
            }
        }

        private void n_fragment_secs_ValueChanged(object sender, EventArgs e)
        {
            get_trailer_info();
        }

        private void n_interval_secs_ValueChanged(object sender, EventArgs e)
        {
            get_trailer_info();
        }

        private void n_trailer_secs_ValueChanged(object sender, EventArgs e)
        {
            get_trailer_info();
        }

        private void chk_dur_trail_end_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_dur_trail_end.Checked == true)
            {
                txt_trailer_end_dur.Enabled = true;
                txt_trailer_final.Enabled = false;
                get_fdur();
                
            }
            else
            {
                txt_trailer_end_dur.Enabled = false;
                txt_trailer_final.Enabled = true;                
            }
            get_trailer_info();
        }

        private void check_enable_trailer_start_CheckedChanged(object sender, EventArgs e)
        {           
                if (check_enable_trailer_start.Checked)
                {
                    txt_trailer_init.Enabled = true;
                    chk_init_trailer_time.Enabled = true;                 
                    DateTime t = new DateTime();
                    if (DateTime.TryParse(txt_trailer_init.Text, out t))
                    {
                        get_trailer_info();
                    }
            }
                else
                {
                    chk_init_trailer_time.Enabled = false;
                    chk_init_trailer_time.Checked = false;
                    txt_trailer_init.Enabled = false;
                    get_trailer_info();                                 
                }                   
        }

        private void check_enable_trailer_end_CheckedChanged(object sender, EventArgs e)
        {
            if (check_enable_trailer_end.Checked)
            {
                chk_dur_trail_end.Enabled = true;
                txt_trailer_final.Enabled = true;
            }
            else
            {
                chk_dur_trail_end.Enabled = false;
                chk_dur_trail_end.Checked = false;
                txt_trailer_end_dur.Enabled = false;                
            }
            get_trailer_info();

        }

        private void chk_init_trailer_time_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_init_trailer_time.Checked)
            {
                txt_init_trailer_dur.Enabled = true;
                txt_trailer_init.Enabled = false;
            }
            else
            {
                txt_init_trailer_dur.Enabled = false;
                txt_trailer_init.Enabled = true;                
            }
            get_fdur();
            get_trailer_info();
        }

        private void txt_init_trailer_dur_TextChanged(object sender, EventArgs e)
        {
            get_fdur();            
        }

        private void txt_trailer_end_dur_TextChanged(object sender, EventArgs e)
        {
            get_fdur();
            
        }

        private void txt_trailer_init_TextChanged(object sender, EventArgs e)
        {
            DateTime t = new DateTime();
            if (DateTime.TryParse(txt_trailer_init.Text, out t))
            {
                get_trailer_info();
            }
        }
        private void txt_trailer_final_TextChanged(object sender, EventArgs e)
        {
            DateTime t = new DateTime();
            if (DateTime.TryParse(txt_trailer_final.Text, out t)) get_trailer_info();
        }

        private void chk_trailer_dur_CheckedChanged(object sender, EventArgs e)
        {
            n_trailer_secs.Enabled = chk_trailer_dur.Checked;
            n_interval_secs.Enabled = !chk_trailer_dur.Checked;
            get_trailer_info();
        }

        private void txt_trailer_init_DoubleClick(object sender, EventArgs e)
        {
            txt_trailer_init.Text = "00:00:00";
        }

        private void txt_trailer_final_DoubleClick(object sender, EventArgs e)
        {
            txt_trailer_final.Text = "00:00:00";
        }
    }
}
