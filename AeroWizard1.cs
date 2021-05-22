using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

        public String sel_preset = "";
        Boolean presets_init = false;
        public Boolean start_no_files = false;
        public Boolean add_files = false;
        public Boolean add_folder = false;
        Boolean preset_ok = false;
        String port_path = System.IO.Path.Combine(Application.StartupPath, "settings") + "\\";
        public Boolean is_portable = false;
        Boolean started_video = false;
        Boolean started_audio = false;
        Boolean existing_preset = false;
        String params_result = String.Empty;
        String ext_result = String.Empty;
        String preset_name = String.Empty;
        Boolean is_max = false;
        Boolean audio_preset = false;
        Boolean video_preset = false;
        Boolean save_preset = false;
        String video_encoder_param = String.Empty;
        String libx264_params = String.Empty;
        String libx265_params = String.Empty;
        String h264_qsv_params = String.Empty;
        String hevc_qsv_params = String.Empty;
        String h264_nvenc_params = String.Empty;
        String hevc_nvenc_params = String.Empty;
        String libvpx_vp9_params = String.Empty;
        String prores_ks_params = String.Empty;
        String dnxhd_params = String.Empty;
        String dnxhr_params = String.Empty;
        //String pix_ftm_param = String.Empty;
        //String pix_prores_param = String.Empty;
        //String frame_rate_param = String.Empty;
        //String resize_param = String.Empty;
        //String rotate_param = String.Empty;
        String audio_encoder_param = String.Empty;
        String pcm16_params = String.Empty;
        String pcm24_params = String.Empty;
        String flac_params = String.Empty;
        String aac_params = String.Empty;
        String ac3_params = String.Empty;
        String eac3_params = String.Empty;
        String mp3_params = String.Empty;
        String vorbis_params = String.Empty;
        String opus_params = String.Empty;
        Boolean first_resize_rotate = false;
        Boolean two_pass = false;
        Boolean silence = false;
        Boolean tried_ok = false;
        public String lv1_item = String.Empty;
        public Boolean no_two = false;
        public Boolean no_silence = false;
        public Boolean w_images = false;
        public Boolean w_split = false;
        public Boolean images_v = false;

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            String app_location = Application.StartupPath;
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;                
                
            if (wizardControl1.SelectedPage == wz_end) btn_status.PerformClick();
            
            if (started_video == false)
            {
                String[] video_encoders = new string[] { "copy", "libx264", "libx265", "h264_qsv", "hevc_qsv", "h264_nvenc", "hevc_nvenc", "libvpx-vp9", "prores_ks", "dnxhd", "dnxhr" };
                foreach (String item in video_encoders) Combo_encoders.Items.Add(item);

                combo_crf_mode.Items.Add("Constant Rate Factor");
                combo_crf_mode.Items.Add("Constant Bitrate");
                Combo_encoders.SelectedIndex = 0;
                cb_framerate.Items.Add("Custom");
                cb_framerate.Items.Add("23.976 (Film)");
                cb_framerate.Items.Add("25 (PAL)");
                cb_framerate.Items.Add("29.97 (NTSC)");
                cb_framerate.Items.Add("30");
                cb_framerate.Items.Add("50 (PAL)");
                cb_framerate.Items.Add("59.93 (NTSC)");
                cb_framerate.Items.Add("60");
                String[] v_pixels = new string[] { "yuv420p", "yuv422p", "yuv444p", "yuyv422", "yuv422p10", "yuv422p10le", "yuv444p10", "yuv444p10le", "yuva444p10", "yuva444p10le", "rgb24", "rgb32", "rgb565", "rgb555", "nv12", "gray", "monow", "monob" };
                foreach (String item in v_pixels) cb_pixel.Items.Add(item);
                String[] sizes = new string[] { "Custom", "1920x1080", "1920x800", "1440x1080", "1280x720", "1024x768", "1024x576", "800x600", "800x480", "720x576", "720x480", "640x480", "640x360" };
                foreach (String v_sizes in sizes) cb_resize.Items.Add(v_sizes);
                String[] crops = new string[] { "Custom", "1:1", "4:3", "16:9", "1440x1080", "1280x720", "1024x768", "1024x576", "800x600", "800x480", "720x576", "720x480" };
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
                cb_vendor_prores.Items.Add("default");
                cb_vendor_prores.Items.Add("ap10");
                cb_vendor_prores.SelectedIndex = 1;
                cb_bits_prores.Items.Add("default");
                cb_bits_prores.Items.Add("8000");
                cb_bits_prores.SelectedIndex = 1;

                //End ProRes

            }
            pic_rotate.Image = img_rotate.Images[0];
            started_video = true;
        }

        private void Combo_encoders_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                label6.Text = "Higher quality";
                label7.Text = "Lower quality";
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

            if (Combo_encoders.SelectedItem.ToString() == "libx264" || Combo_encoders.SelectedItem.ToString() == "libx265" || Combo_encoders.SelectedItem.ToString() == "h264_nvenc" || Combo_encoders.SelectedItem.ToString() == "hevc_nvenc" || Combo_encoders.SelectedItem.ToString() == "h264_qsv" || Combo_encoders.SelectedItem.ToString() == "hevc_qsv")
            {
                track_q_v.Enabled = true;
                label6.Text = "Higher quality";
                label7.Text = "Lower quality";
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
                if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc")
                {
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
            if (Combo_encoders.SelectedIndex == 9)
            {
                if (track_q_v.Value <= 36)
                {
                    n_crf.Value = 36;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }

                if (track_q_v.Value > 36 && track_q_v.Value <= 42)
                {
                    n_crf.Value = 42;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 42 && track_q_v.Value <= 45)
                {
                    n_crf.Value = 45;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 45 && track_q_v.Value <= 60)
                {
                    n_crf.Value = 60;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 45 && track_q_v.Value <= 60)
                {
                    n_crf.Value = 60;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 60 && track_q_v.Value <= 63)
                {
                    n_crf.Value = 63;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 63 && track_q_v.Value <= 75)
                {
                    n_crf.Value = 75;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 75 && track_q_v.Value <= 80)
                {
                    n_crf.Value = 80;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 80 && track_q_v.Value <= 84)
                {
                    n_crf.Value = 84;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 84 && track_q_v.Value <= 90)
                {
                    n_crf.Value = 90;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 90 && track_q_v.Value <= 110)
                {
                    n_crf.Value = 110;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 110 && track_q_v.Value <= 115)
                {
                    n_crf.Value = 115;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 115 && track_q_v.Value <= 120)
                {
                    n_crf.Value = 120;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 120 && track_q_v.Value <= 145)
                {
                    n_crf.Value = 145;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 145 && track_q_v.Value <= 175)
                {
                    n_crf.Value = 175;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 175 && track_q_v.Value <= 185)
                {
                    n_crf.Value = 185;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 185 && track_q_v.Value <= 220)
                {
                    n_crf.Value = 220;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 220 && track_q_v.Value <= 240)
                {
                    n_crf.Value = 240;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 240 && track_q_v.Value <= 290)
                {
                    n_crf.Value = 290;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 290 && track_q_v.Value <= 350)
                {
                    n_crf.Value = 350;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 350 && track_q_v.Value <= 365)
                {
                    n_crf.Value = 365;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 365 && track_q_v.Value <= 390)
                {
                    n_crf.Value = 390;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 390 && track_q_v.Value <= 440)
                {
                    n_crf.Value = 440;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 440 && track_q_v.Value <= 730)
                {
                    n_crf.Value = 730;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
                if (track_q_v.Value > 730 && track_q_v.Value <= 880)
                {
                    n_crf.Value = 880;
                    track_q_v.Value = Convert.ToInt32(n_crf.Value);
                    return;
                }
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
            track_q_v.Value = Convert.ToInt32(n_crf.Value);
        }

        private void combo_h264_mode_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (combo_crf_mode.SelectedIndex == 0)
            {
                label14.Text = String.Empty;
                label15.Text = String.Empty;
                track_q_v.Width = 437;
                track_q_v.Minimum = 1;
                track_q_v.Maximum = 51;
                track_q_v.TickFrequency = 1;
                track_q_v.Value = 23;
                n_crf.Width = 40;
                n_crf.Left = 442;
                n_crf.Minimum = 1;
                n_crf.Maximum = 51;
                cb_cq_vp9.Visible = false;
                label23.Visible = false;

                if (Combo_encoders.SelectedItem.ToString() == "libx264") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "libx265") n_crf.Value = 23;
                if (Combo_encoders.SelectedItem.ToString() == "libvpx-vp9") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "h264_nvenc") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_nvenc") n_crf.Value = 25;
                if (Combo_encoders.SelectedItem.ToString() == "h264_qsv") n_crf.Value = 20;
                if (Combo_encoders.SelectedItem.ToString() == "hevc_qsv") n_crf.Value = 25;


                n_crf.Increment = 1;
                label3.Text = String.Empty;
                label6.Visible = true;
                label7.Visible = true;
                label6.Text = "Higher quality";
                label7.Text = "Lower quality";
            }
            if (combo_crf_mode.SelectedIndex == 1)
            {
                track_q_v.Width = 422;
                track_q_v.Minimum = 100;
                track_q_v.Maximum = 50000;
                track_q_v.TickFrequency = 1000;
                track_q_v.Value = 1000;
                n_crf.Width = 60;
                n_crf.Left = 428;
                n_crf.Minimum = 100;
                n_crf.Maximum = 50000;
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
            if (started_audio == false)
            {
                cb_audio_encoder.Items.Clear();
                String[] audio_encoders = new string[] { "copy", "none", "pcm16", "pcm24", "flac", "aac", "ac3", "e-ac3", "mp3", "vorbis", "opus" };
                foreach (String item in audio_encoders) cb_audio_encoder.Items.Add(item);
                cb_audio_encoder.SelectedIndex = 0;
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
            }
            started_audio = true;
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

                label10.Visible = false;
                label11.Visible = false;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label22.Visible = false;
                label29.Visible = false;
                cb_chunk_size.Visible = false;
                if (cb_audio_encoder.SelectedItem.ToString() == "copy") txt_current_audio.Text = "Audio stream copy";
                else txt_current_audio.Text = "Exclude audio stream";
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
                    label15.Text = "Lower size   ";
                    label14.Text = "Bigger size";
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
                label15.Text = "Higher quality";
                label14.Text = "Lower quality";
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
                label14.Text = "Higher quality";
                label15.Text = "Lower quality";
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
                label15.Text = "Higher quality";
                label14.Text = "Lower quality";
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
                label15.Text = "      Lower size";
                label14.Text = "Bigger size";
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
            String filters = String.Empty;

            if (first_resize_rotate == false)
            {
                first_resize_rotate = true;
                if (Combo_encoders.SelectedIndex != Combo_encoders.FindString("copy"))
                {
                    if (cb_resize.SelectedIndex != -1 && cb_crop.SelectedIndex == -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        //Resize only
                        if (cb_resize.SelectedIndex == 0)
                        {
                            video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + '\u0022';
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "scale=" + r_size + '\u0022';
                        }
                    }

                    if (cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0 && cb_resize.SelectedIndex == -1 && cb_crop.SelectedIndex == -1)
                    {
                        //Rotate only
                        if (cb_rotate.SelectedIndex == 1) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "hflip" + '\u0022';

                    }

                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex == -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        //Crop only
                        if (cb_crop.SelectedIndex == 0) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + '\u0022';
                        if (cb_crop.SelectedIndex == 1) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=in_h" + '\u0022';
                        if (cb_crop.SelectedIndex == 2) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=ih/3*4:ih" + '\u0022';
                        if (cb_crop.SelectedIndex == 3) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=ih/9*16:ih" + '\u0022';
                        if (cb_crop.SelectedIndex == 4) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1440:1080" + '\u0022';
                        if (cb_crop.SelectedIndex == 5) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1280:720" + '\u0022';
                        if (cb_crop.SelectedIndex == 6) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1024:768" + '\u0022';
                        if (cb_crop.SelectedIndex == 7) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1024:576" + '\u0022';
                        if (cb_crop.SelectedIndex == 8) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=800:600" + '\u0022';
                        if (cb_crop.SelectedIndex == 9) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=800:480" + '\u0022';
                        if (cb_crop.SelectedIndex == 10) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=720:576" + '\u0022';
                        if (cb_crop.SelectedIndex == 11) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=720:480" + '\u0022';
                    }
                    // All filters chained
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex != -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_crop.SelectedIndex == 0) filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + ", ";
                        if (cb_crop.SelectedIndex == 1) filters = filters + "crop=in_h" + ", ";
                        if (cb_crop.SelectedIndex == 2) filters = filters + "crop=ih/3*4:ih" + ", ";
                        if (cb_crop.SelectedIndex == 3) filters = filters + "crop=ih/9*16:ih" + ", ";
                        if (cb_crop.SelectedIndex == 4) filters = filters + "crop=1440:1080" + ", ";
                        if (cb_crop.SelectedIndex == 5) filters = filters + "crop=1280:720" + ", ";
                        if (cb_crop.SelectedIndex == 6) filters = filters + "crop=1024:768" + ", ";
                        if (cb_crop.SelectedIndex == 7) filters = filters + "crop=1024:576" + ", ";
                        if (cb_crop.SelectedIndex == 8) filters = filters + "crop=800:600" + ", ";
                        if (cb_crop.SelectedIndex == 9) filters = filters + "crop=800:480" + ", ";
                        if (cb_crop.SelectedIndex == 10) filters = filters + "crop=720:576" + ", ";
                        if (cb_crop.SelectedIndex == 11) filters = filters + "crop=720:480" + ", ";

                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';
                        video_encoder_param = video_encoder_param + filters;
                    }
                    // Two filters, resize + crop
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex != -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_crop.SelectedIndex == 0) filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + '\u0022';
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

                        video_encoder_param = video_encoder_param + filters;
                    }

                    // Two filters, resize + rotate
                    if (cb_crop.SelectedIndex == -1 && cb_resize.SelectedIndex != -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';

                        video_encoder_param = video_encoder_param + filters;
                    }
                    // Two filters, crop + rotate
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex == -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_crop.SelectedIndex == 0) filters = " -vf " + '\u0022' + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + ", ";
                        if (cb_crop.SelectedIndex == 1) filters = " -vf " + '\u0022' + "crop=in_h" + ", ";
                        if (cb_crop.SelectedIndex == 2) filters = " -vf " + '\u0022' + "crop=ih/3*4:ih" + ", ";
                        if (cb_crop.SelectedIndex == 3) filters = " -vf " + '\u0022' + "crop=ih/9*16:ih" + ", ";
                        if (cb_crop.SelectedIndex == 4) filters = " -vf " + '\u0022' + "crop=1440:1080" + ", ";
                        if (cb_crop.SelectedIndex == 5) filters = " -vf " + '\u0022' + "crop=1280:720" + ", ";
                        if (cb_crop.SelectedIndex == 6) filters = " -vf " + '\u0022' + "crop=1024:768" + ", ";
                        if (cb_crop.SelectedIndex == 7) filters = " -vf " + '\u0022' + "crop=1024:576" + ", ";
                        if (cb_crop.SelectedIndex == 8) filters = " -vf " + '\u0022' + "crop=800:600" + ", ";
                        if (cb_crop.SelectedIndex == 9) filters = " -vf " + '\u0022' + "crop=800:480" + ", ";
                        if (cb_crop.SelectedIndex == 10) filters = " -vf " + '\u0022' + "crop=720:576" + ", ";
                        if (cb_crop.SelectedIndex == 11) filters = " -vf " + '\u0022' + "crop=720:480" + ", ";

                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';

                        video_encoder_param = video_encoder_param + filters;
                    }
                }
                Decimal v_sp = 0;
                Decimal a_sp = 0;
                Decimal const1 = 0.5M;
                if (n_speed.Value != 0)
                {
                    if (n_speed.Value > 0)
                    {
                        v_sp = const1 + ((100 - n_speed.Value) / 200);
                        a_sp =  1 + (n_speed.Value / 100);
                    }
                    
                    if (n_speed.Value < 0)
                    {
                        v_sp = 1 + Math.Abs(n_speed.Value / 100);
                        a_sp = const1 + (Math.Abs(-100 - n_speed.Value) / 200);
                    }
                    video_encoder_param = video_encoder_param + " -filter_complex " + '\u0022' + "[0:v]setpts=" + v_sp.ToString().Replace(",", ".") + "*PTS[v];[0:a]atempo=" + a_sp.ToString().Replace(",", ".") + "[a]" + '\u0022' + " -map " + '\u0022' + "[v]" + '\u0022' + " -map " + '\u0022' + "[a]" + '\u0022' + " ";
                }

            }

            else //Back and forth
            {
                commit_video_1();

                if (Combo_encoders.SelectedIndex != Combo_encoders.FindString("copy"))
                {
                    if (cb_resize.SelectedIndex != -1 && cb_crop.SelectedIndex == -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        //Resize only
                        if (cb_resize.SelectedIndex == 0)
                        {
                            video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + '\u0022';
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "scale=" + r_size + '\u0022';
                        }
                    }

                    if (cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0 && cb_resize.SelectedIndex == -1 && cb_crop.SelectedIndex == -1)
                    {
                        //Rotate only
                        if (cb_rotate.SelectedIndex == 1) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "hflip" + '\u0022';

                    }

                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex == -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        //Crop only
                        if (cb_crop.SelectedIndex == 0) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + '\u0022';
                        if (cb_crop.SelectedIndex == 1) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=in_h" + '\u0022';
                        if (cb_crop.SelectedIndex == 2) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=ih/3*4:ih" + '\u0022';
                        if (cb_crop.SelectedIndex == 3) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=ih/9*16:ih" + '\u0022';
                        if (cb_crop.SelectedIndex == 4) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1440:1080" + '\u0022';
                        if (cb_crop.SelectedIndex == 5) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1280:720" + '\u0022';
                        if (cb_crop.SelectedIndex == 6) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1024:768" + '\u0022';
                        if (cb_crop.SelectedIndex == 7) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=1024:576" + '\u0022';
                        if (cb_crop.SelectedIndex == 8) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=800:600" + '\u0022';
                        if (cb_crop.SelectedIndex == 9) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=800:480" + '\u0022';
                        if (cb_crop.SelectedIndex == 10) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=720:576" + '\u0022';
                        if (cb_crop.SelectedIndex == 11) video_encoder_param = video_encoder_param + " -vf " + '\u0022' + "crop=720:480" + '\u0022';
                    }
                    // All filters chained
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex != -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_crop.SelectedIndex == 0) filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + ", ";
                        if (cb_crop.SelectedIndex == 1) filters = filters + "crop=in_h" + ", ";
                        if (cb_crop.SelectedIndex == 2) filters = filters + "crop=ih/3*4:ih" + ", ";
                        if (cb_crop.SelectedIndex == 3) filters = filters + "crop=ih/9*16:ih" + ", ";
                        if (cb_crop.SelectedIndex == 4) filters = filters + "crop=1440:1080" + ", ";
                        if (cb_crop.SelectedIndex == 5) filters = filters + "crop=1280:720" + ", ";
                        if (cb_crop.SelectedIndex == 6) filters = filters + "crop=1024:768" + ", ";
                        if (cb_crop.SelectedIndex == 7) filters = filters + "crop=1024:576" + ", ";
                        if (cb_crop.SelectedIndex == 8) filters = filters + "crop=800:600" + ", ";
                        if (cb_crop.SelectedIndex == 9) filters = filters + "crop=800:480" + ", ";
                        if (cb_crop.SelectedIndex == 10) filters = filters + "crop=720:576" + ", ";
                        if (cb_crop.SelectedIndex == 11) filters = filters + "crop=720:480" + ", ";

                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';
                        video_encoder_param = video_encoder_param + filters;
                    }
                    // Two filters, resize + crop
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex != -1 && (cb_rotate.SelectedIndex == -1 || cb_rotate.SelectedIndex == 0))
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_crop.SelectedIndex == 0) filters = filters + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + '\u0022';
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

                        video_encoder_param = video_encoder_param + filters;
                    }

                    // Two filters, resize + rotate
                    if (cb_crop.SelectedIndex == -1 && cb_resize.SelectedIndex != -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_resize.SelectedIndex == 0)
                        {
                            filters = " -vf " + '\u0022' + "scale=" + n_width.Value + ":" + n_height.Value + ", ";
                        }
                        else
                        {
                            String r_size = cb_resize.SelectedItem.ToString().Replace("x", ":");
                            filters = " -vf " + '\u0022' + "scale=" + r_size + ", ";
                        }
                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';

                        video_encoder_param = video_encoder_param + filters;
                    }
                    // Two filters, crop + rotate
                    if (cb_crop.SelectedIndex != -1 && cb_resize.SelectedIndex == -1 && cb_rotate.SelectedIndex != -1 && cb_rotate.SelectedIndex != 0)
                    {
                        if (cb_crop.SelectedIndex == 0) filters = " -vf " + '\u0022' + "crop=in_w-" + (l_crop.Value + r_crop.Value).ToString() + ":" + "h=in_h-" + (u_crop.Value + d_crop.Value).ToString() + ":" + "x=" + l_crop.Value + ":" + "y=" + u_crop.Value + ", ";
                        if (cb_crop.SelectedIndex == 1) filters = " -vf " + '\u0022' + "crop=in_h" + ", ";
                        if (cb_crop.SelectedIndex == 2) filters = " -vf " + '\u0022' + "crop=ih/3*4:ih" + ", ";
                        if (cb_crop.SelectedIndex == 3) filters = " -vf " + '\u0022' + "crop=ih/9*16:ih" + ", ";
                        if (cb_crop.SelectedIndex == 4) filters = " -vf " + '\u0022' + "crop=1440:1080" + ", ";
                        if (cb_crop.SelectedIndex == 5) filters = " -vf " + '\u0022' + "crop=1280:720" + ", ";
                        if (cb_crop.SelectedIndex == 6) filters = " -vf " + '\u0022' + "crop=1024:768" + ", ";
                        if (cb_crop.SelectedIndex == 7) filters = " -vf " + '\u0022' + "crop=1024:576" + ", ";
                        if (cb_crop.SelectedIndex == 8) filters = " -vf " + '\u0022' + "crop=800:600" + ", ";
                        if (cb_crop.SelectedIndex == 9) filters = " -vf " + '\u0022' + "crop=800:480" + ", ";
                        if (cb_crop.SelectedIndex == 10) filters = " -vf " + '\u0022' + "crop=720:576" + ", ";
                        if (cb_crop.SelectedIndex == 11) filters = " -vf " + '\u0022' + "crop=720:480" + ", ";

                        if (cb_rotate.SelectedIndex == 1) filters = filters + "transpose=clock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 2) filters = filters + "transpose=cclock" + '\u0022';
                        if (cb_rotate.SelectedIndex == 3) filters = filters + "vflip,hflip" + '\u0022';
                        if (cb_rotate.SelectedIndex == 4) filters = filters + "hflip" + '\u0022';

                        video_encoder_param = video_encoder_param + filters;
                    }
                }
            }
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
                MessageBox.Show("Only baseline profile is supported in ultrafast preset", "Profile limitation", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            wizardControl1.Pages[1].AllowNext = true;
            pic_1.Image = images.Images[1];
        }

        private void radio_video_CheckedChanged(object sender, EventArgs e)
        {
            audio_preset = false;
            video_preset = true;
            existing_preset = false;
            lbl_two.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_1.Image = images.Images[0];
            wizardControl1.Pages[1].AllowNext = true;
        }


        private void cb_rotate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_rotate.SelectedIndex == 0)
            {
                pic_rotate.Left = 215;
                pic_rotate.Size = new System.Drawing.Size(221, 124);
                pic_rotate.Image = img_rotate.Images[0];
            }
            if (cb_rotate.SelectedIndex == 1)
            {
                pic_rotate.Left = 247;
                pic_rotate.Size = new System.Drawing.Size(70, 124);
                pic_rotate.Image = img_rotate.Images[1];
            }
            if (cb_rotate.SelectedIndex == 2)
            {
                pic_rotate.Left = 247;
                pic_rotate.Size = new System.Drawing.Size(70, 124);
                pic_rotate.Image = img_rotate.Images[2];
            }
            if (cb_rotate.SelectedIndex == 3)
            {
                pic_rotate.Left = 215;
                pic_rotate.Size = new System.Drawing.Size(221, 124);
                pic_rotate.Image = img_rotate.Images[3];
            }
            if (cb_rotate.SelectedIndex == 4)
            {
                pic_rotate.Left = 215;
                pic_rotate.Size = new System.Drawing.Size(221, 124);
                pic_rotate.Image = img_rotate.Images[4];
            }
        }

        private void wz_end_Initialize(object sender, AeroWizard.WizardPageInitEventArgs e)
        {            
            if (lv1_item != String.Empty)
            {
                pic_status.Visible = true;
                btn_status.Visible = true;
                pic_status.Image = img_status.Images[0];
            }
            else
            {
                lbl_help.Text = "Wizard is ready. Add files to main screen and select Start sequential or Multi-file encoding.";
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
                    h264_nvenc_params = h264_nvenc_params + " -qp " + n_crf.Value.ToString();
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

            if (Combo_encoders.SelectedIndex == Combo_encoders.FindString("hevc_nvenc"))
            {
                video_encoder_param = "-c:v hevc_nvenc";
                if (cb_preset.SelectedIndex != -1) hevc_nvenc_params = " -preset " + cb_preset.SelectedItem.ToString();
                if (cb_profile.SelectedIndex != -1) hevc_nvenc_params = hevc_nvenc_params + " -profile:v " + cb_profile.SelectedItem.ToString();
                if (cb_level.SelectedIndex != -1) hevc_nvenc_params = hevc_nvenc_params + " -level " + cb_level.SelectedItem.ToString();
                if (cb_tune.SelectedIndex != -1 && cb_tune.SelectedIndex != 0) hevc_nvenc_params = hevc_nvenc_params + " -tune " + cb_tune.SelectedItem.ToString();

                if (combo_crf_mode.SelectedIndex == 0 && cb_preset.SelectedIndex != cb_preset.FindString("lossless") && cb_preset.SelectedIndex != cb_preset.FindString("losslesshp"))
                {
                    hevc_nvenc_params = hevc_nvenc_params + " -global_quality " + n_crf.Value.ToString() + " -look_ahead 1";
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
                video_encoder_param = video_encoder_param + hevc_nvenc_params;
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
            if (n_speed.Value != 0 && cb_audio_encoder.SelectedItem.ToString() == "copy")
            {
                MessageBox.Show("Speed up/down filter cannot be used with audio stream copy.");
                e.Cancel = true;
            }
            reset_a_params();

            if ((cb_audio_encoder.SelectedItem.ToString() == "none" || cb_audio_encoder.SelectedItem.ToString() == "copy") && audio_preset == true)
            {
                MessageBox.Show("You can't select none or copy for audio preset", "Audio preset", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (n_speed2.Value != 0)
            {
                if (n_speed2.Value > 0)
                {                   
                    a_sp = 1 + (n_speed2.Value / 100);
                }

                if (n_speed2.Value < 0)
                {                 
                    a_sp = const1 + (Math.Abs(-100 - n_speed2.Value) / 200);
                }
                audio_encoder_param = audio_encoder_param + " -filter:a " + '\u0022' + "atempo=" + a_sp.ToString().Replace(",", ".") + '\u0022' +  " ";
            }
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
                lbl_two.Text = "File list is empty";
                lbl_silence.Text = String.Empty;
                pic_warn_two.Visible = true;
                pic_warn_silence.Visible = false;
                wizardControl1.Pages[1].AllowNext = false;
            }
            else
            {
                lbl_two.Text = String.Empty;
                pic_warn_two.Visible = false;
                wizardControl1.Pages[1].AllowNext = true;
            }
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
                        MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        return;
                    }
                }

                String ext_output = String.Empty;
                this.InvokeEx(f => ext_output = cb_container.SelectedItem.ToString());

                String textbox_params = wiz_params;
                String file_prueba2 = file_prueba;
                if (textbox_params.Contains("%1"))
                {
                    if (file_prueba2.Contains("[") || file_prueba2.Contains("]"))
                    {
                        MessageBox.Show("Input test file name contains characters [ ]. Please remove them from input file name to avoid errors with -vf filter", "Conflicting characters in file name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                        bad_chars = true;
                        return;
                    }
                    file_prueba2 = file_prueba2.Replace("\\", "\\\\\\\\");
                    file_prueba2 = file_prueba2.Replace(":", "\\\\" + ":");
                    textbox_params = textbox_params.Replace("%1", file_prueba2);
                }

                consola_pre.StartInfo.FileName = "ffmpeg.exe";
                consola_pre.StartInfo.Arguments = " -i " + "" + '\u0022' + file_prueba + '\u0022' + "" + " -y " + textbox_params + " " + '\u0022' + destino_test + "\\" + System.IO.Path.GetFileNameWithoutExtension(file_prueba) + "." + ext_output + '\u0022';
                consola_pre.StartInfo.RedirectStandardOutput = true;
                consola_pre.StartInfo.RedirectStandardError = true;
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

            if (!tt.Wait(1000) && consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                pic_status.Image = img_status.Images[1];
                preset_ok = true;
                this.InvokeEx(f => lbl_help.Text = "Select sequential or multi-file encoding on main screen to start.");
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

                if (Directory.GetFiles(destino_test).Length == 0)
                {
                    System.IO.Directory.Delete(destino_test);
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
                    this.InvokeEx(f => lbl_help.Text = "Preset is not valid. It may require to review encoding parameters or output container.");
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

                    this.InvokeEx(f => this.Cursor = Cursors.Arrow);
                    foreach (String lin in LB1_o.Items)
                    {
                        if (lin.Contains("not load the requested plugin") || lin.Contains("Cannot load nvcuda.dll"))
                        {
                            unsupported = true;
                        }
                    }
                    if (unsupported == true) MessageBox.Show("FFmpeg command failed on selected file: " + Environment.NewLine + Environment.NewLine + "Possibly unsupported encoder" + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else MessageBox.Show("FFmpeg command failed on selected file: " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                    this.InvokeEx(f => lbl_help.Text = "Select sequential or multi-file encoding on main screen to start.");
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
                        MessageBox.Show("Error: " + excpt.Message, "Error writing to folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        MessageBox.Show("Input test file name contains characters [ ]. Please remove them from input file name to avoid errors with -vf filter", "Conflicting characters in file name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.Cursor = Cursors.Arrow;
                        bad_chars = true;
                        return;
                    }
                    file_prueba3 = file_prueba3.Replace("\\", "\\\\\\\\");
                    file_prueba3 = file_prueba3.Replace(":", "\\\\" + ":");
                    textbox_params = textbox_params.Replace("%1", file_prueba3);
                }

                consola_pre.StartInfo.FileName = "ffmpeg.exe";
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
                        lbl_help.Text = "Preset is not valid. It may require to review encoding parameters.";
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
                        if (unsupported == true) MessageBox.Show("FFmpeg command failed on selected file: " + Environment.NewLine + Environment.NewLine + "Possibly unsupported encoder" + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        else MessageBox.Show("FFmpeg command failed on selected file: " + Environment.NewLine + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 4].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 3].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 2].ToString() + Environment.NewLine + LB1_o.Items[LB1_o.Items.Count - 1].ToString() + Environment.NewLine + Environment.NewLine + "Try preset for more error information", "FFmpeg command failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        lbl_help.Text = "Select sequential or multi-file encoding on main screen to start.";
                        tried_ok = true;
                        this.Cursor = Cursors.Arrow;
                    }
                }

           
            if (consola_pre.StartInfo.Arguments != String.Empty)
            {
                consola_pre.Kill();
                pic_status.Image = img_status.Images[1];
                preset_ok = true;
                lbl_help.Text = "Select sequential or multi-file encoding on main screen to start.";
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
                lbl_silence.Text = "File list is empty";
                pic_warn_silence.Visible = true;
                lbl_two.Text = String.Empty;
                pic_warn_two.Visible = false;
                wizardControl1.Pages[1].AllowNext = false;
            }
            else
            {
                lbl_silence.Text = String.Empty;
                pic_warn_silence.Visible = false;
                wizardControl1.Pages[1].AllowNext = true;
            }
        }

        private void radio_images_CheckedChanged(object sender, EventArgs e)
        {
            pic_1.Image = images.Images[4];
            lbl_two.Text = String.Empty;
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
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
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            lbl_two.Text = String.Empty;
            lbl_silence.Text = String.Empty;
            wizardControl1.Pages[1].AllowNext = true;
        }

        private void wz_end_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            if (preset_ok != true && lv1_item != String.Empty)
            {
                DialogResult a = MessageBox.Show("Preset validation failed. Use preset anyway?", "Invalid preset", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
            pic_warn_two.Visible = false;
            pic_warn_silence.Visible = false;
            pic_1.Image = images.Images[6];
            wizardControl1.Pages[1].AllowNext = true;
        }

        private void txt_pr_1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_w_presets_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            combo_presets_ext.Items.Add("Default parameters");
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

            combo_presets_ext.SelectedIndex = combo_presets_ext.FindString("Default parameters");
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

            if (presets_init = true)
            {
                try { combo_presets_ext.SelectedIndex = combo_presets_ext.FindString(sel_preset); }
                catch { }
            }
            presets_init = true;
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

        private void wz0_0_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            
        }
    }
}
