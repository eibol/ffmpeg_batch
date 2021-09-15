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

namespace FFBatch
{
    public partial class Form15 : Form
    {

        String port_path = Application.StartupPath + "\\" + "settings" + "\\";
        Boolean is_portable = false;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;
        public Boolean saved = false;
        Boolean duplicates = false;

        public Form15()
        {
            InitializeComponent();
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form15));
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

        private void Form15_Load(object sender, EventArgs e)
        {            
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;            

            dg_pr.RowHeadersWidth = 25;
            String path = "";
            if (is_portable == false)
            {
                path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            }
            else
            {
                path = port_path + "ff_presets_portable.ini";                
            }

            String path_log_backup = "";
            if (is_portable == false)
            {
                path_log_backup = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets_bck.ini";                
            }
            else
            {
                path_log_backup = port_path + "ff_presets_bck_portable.ini";                
            }

            if (!File.Exists(path_log_backup) && File.Exists(path))
            {
                File.Copy(path, path_log_backup, true);
            }
            create_tips();
            read_presets();
            dg_pr.ClearSelection();
            
            refresh_lang();
            
                dg_pr.Columns[0].HeaderText = FFBatch.Properties.Strings.Name;
                dg_pr.Columns[1].HeaderText = FFBatch.Properties.Strings.ff_params;
                dg_pr.Columns[2].HeaderText = FFBatch.Properties.Strings.Format;
            
        }

        private void create_tips()
        {
            ToolTip T001 = new ToolTip();
            T001.AutoPopDelay = 3500;
            T001.InitialDelay = 750;
            T001.ReshowDelay = 500;
            T001.ShowAlways = true;
            T001.SetToolTip(this.btn_save_backup, FFBatch.Properties.Strings.bck_conf);
        }

        private void read_presets()
        {            
            string path_presets;

            if (is_portable == false)
            {                
                path_presets = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            }
            else
            {                
                path_presets = port_path + "ff_presets_portable.ini";
            }            

            String param = "";
            String format = "";
            if (!File.Exists(path_presets))
            {
                File.WriteAllText(path_presets, "Version 1.0" + Environment.NewLine
        + "PR: Video: MP4 Stream copy & -c copy % mp4" + Environment.NewLine
        + "PR: Video: Convert audio track to AAC HQ 2 channels & -c:v copy -c:a aac -cutoff 20K -b:a 256K -ac 2 % mkv" + Environment.NewLine + "PR: Video: Convert audio tracks to AC3 2 channels & -map 0 -c:v copy -c:a ac3 -b:a 256K -ac 2 -c:s copy % mkv" + Environment.NewLine + "PR: Video: Convert to ProRes MKV & -c:v prores_ks -profile:v standard -vendor:v ap10 -pix_fmt yuv422p10le -c:a pcm_s16le -chunk_size 64K % mkv" + Environment.NewLine + "PR: Video: Convert to H264 HQ + Source Audio & -map 0 -c:v libx264 -crf 20 -c:a copy % mkv" + Environment.NewLine + "PR: Video: Convert to H264 Ultrafast + Source Audio & -map 0 -c:v libx264 -crf 23 -preset ultrafast -c:a copy % mkv" + Environment.NewLine + "PR: Video: Convert to H265 HQ + Source Audio & -map 0 -c:v libx265 -crf 23 -c:a copy % mkv" + Environment.NewLine + "PR: Video: Resize 1280x720 H264-AAC & -map 0 -c:v libx264 -crf 23 -vf scale=1280:720 -c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Rotate 90 degress Clockwise to H264 + Source audio & -c:v libx264 -crf 21 -vf " + "\u0022" + "transpose = 1" + "\u0022" + "-c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Rotate 90 degress CounterCLockwise and Vertical Flip to H264 + Source audio & -c:v libx264 -crf 21 -vf" + "\u0022" + "transpose = 0" + "\u0022" + "-c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Rotate 90 degress CounterClockwise to H264 + Source audio & -c:v libx264 -crf 21 -vf" + "\u0022" + "transpose = 2" + "\u0022" + "-c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Rotate 90 degress Clockwise and Vertical Flip to H264 + Source audio & -c:v libx264 -crf 21 -vf" + "\u0022" + "transpose = 3" + "\u0022" + "-c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Rotate 180 degress to H264 + Source audio & -c:v libx264 -crf 21 -vf" + "\u0022" + "transpose = 2, transpose = 2" + "\u0022" + "-c:a aac -b:a 128K % mp4" + Environment.NewLine + "PR: Video: Remove subtitles to MP4 & -map 0 -c copy -sn % mp4" + Environment.NewLine + "PR: Audio: Convert to FLAC 16/44,1KHz 2 channels & -vn -c:a flac -ar 44100 -sample_fmt s16 -ac 2 % flac" + Environment.NewLine + "PR: Audio: Convert to MP3 VBR HQ 2 ch embedded cover & -c:v copy -c:a libmp3lame -qscale:a 0 -ac 2  % mp3" + Environment.NewLine + "PR: Audio: Convert to MP3 VBR HQ 2 ch & -vn -c:a libmp3lame -qscale:a 1 -ac 2 % mp3" + Environment.NewLine + "PR: Audio: Convert to MP3 CBR HQ 2 ch & -vn -c:a libmp3lame -b:a 224K -ac 2 % mp3" + Environment.NewLine + "PR: Audio: Convert to WAV 16/44,1KHz 2 channels & -vn -c:a pcm_s16le -ar 44100 -sample_fmt s16 -ac 2 % wav" + Environment.NewLine + "PR: Subtitle: Extract first subtitle track to SRT & -map 0:2 -c:s copy % srt" + Environment.NewLine + "PR: Image: Extract frame as image & -vframes 1 -f image2  % png" + Environment.NewLine + "PR: Record screen at 24 fps + Audio AAC to MKV & -r 24 -c:a aac -b:a 128K % mkv" + Environment.NewLine + "PR: Record screen at 15 fps 1280x720 + Audio to MKV & -r 15 -vf scale=1280x720 -c:a aac -b:a 128K % mkv" + Environment.NewLine + "PR: Record screen at 30 fps Nvidia NVENC + Audio AAC to MKV & -c:v h264_nvenc -qp 20 -r 30 -c:a aac -b:a 128K % mkv" + Environment.NewLine + "PR: Record screen at 25 fps Intel QuickSync + Audio AAC to MKV & -c:v  h264_qsv -qp 20 -r 25 -c:a aac -b:a 128K % mkv");
            }
            foreach (string line in File.ReadLines(path_presets))
            {
                if (line.Length > 8)
                {
                    if (line.Substring(0, 7).ToLower() == "version")
                    {
                        txt_config_ver.Text = line.Substring(8, line.Length - 8);
                        continue;
                    }
                }
                if (line.Contains("PR: "))
                {
                    int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");
                    format = line.Substring(line.LastIndexOf("%") + 2);
                    try
                    {
                        param = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);
                        dg_pr.Rows.Add(line.Substring(4, line.LastIndexOf("&") - 5), param, format);

                    }
                    catch
                    {
                        // MessageBox.Show("Invalid preset data found.", "Preset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (duplicates == true)
            {
                MessageBox.Show(FFBatch.Properties.Strings.pres_name);
                return;
            }
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.pres_empty);
                    dg_pr.ClearSelection();//If you want

                    int nRowIndex = row.Index;
                    int nColumnIndex = 0;

                    dg_pr.Rows[nRowIndex].Selected = true;
                    dg_pr.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                    //In case if you want to scroll down as well.
                    dg_pr.FirstDisplayedScrollingRowIndex = nRowIndex;
                    return;
                }
                if (row.Cells[1].Value == null)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.pres_empty2);
                    int nRowIndex = row.Index;
                    int nColumnIndex = 0;

                    dg_pr.Rows[nRowIndex].Selected = true;
                    dg_pr.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                    //In case if you want to scroll down as well.
                    dg_pr.FirstDisplayedScrollingRowIndex = nRowIndex;
                    return;
                }
                if (row.Cells[1].Value.ToString().Length < 6)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.params_short);
                    int nRowIndex = row.Index;
                    int nColumnIndex = 0;

                    dg_pr.Rows[nRowIndex].Selected = true;
                    dg_pr.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;

                    //In case if you want to scroll down as well.
                    dg_pr.FirstDisplayedScrollingRowIndex = nRowIndex;
                    return;
                }
            }

            string path;
            string path_log_backup;

            if (is_portable == false)
            {
                path_log_backup = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets_bck.ini";
                path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            }
            else
            {
                path_log_backup = port_path + "ff_presets_bck_portable.ini";
                path = port_path + "ff_presets_portable.ini";
            }            
            if (!File.Exists(path)) File.WriteAllText(path, "");
            File.Copy(path, path_log_backup, true);

            List<string> pres = new List<string>();
            String param = "";
            String format = "";
            if (txt_config_ver.Text.Length != 0) pres.Add("Version " + txt_config_ver.Text);

            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    pres.Add("PR: " + row.Cells[0].Value.ToString() + " & " + row.Cells[1].Value.ToString() + " % " + row.Cells[2].Value.ToString());
                }
            }

            File.WriteAllLines(path, pres.ToArray());
            saved = true;
            this.Close();
        }

        private void open_file_FileOk(object sender, CancelEventArgs e)
        {
            FileInfo fi = new FileInfo(open_file.FileName);
            if (fi.Length > 100000)
            {
                MessageBox.Show(FFBatch.Properties.Strings.file_big);
                return;
            }
            String param = "";
            String format = "";
            dg_pr.Rows.Clear();
            foreach (string line in File.ReadLines(open_file.FileName))
            {
                if (line.Contains("PR: "))
                {
                    int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");
                    format = line.Substring(line.LastIndexOf("%") + 2);
                    try
                    {
                        param = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);

                        dg_pr.Rows.Add(line.Substring(4, line.LastIndexOf("&") - 5), param, format);

                    }
                    catch
                    {
                        // MessageBox.Show("Invalid preset data found.", "Preset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("It is recommended to backup before loading new presets." + Environment.NewLine + Environment.NewLine + "Every preset begins with " + '\u0022' + "PR:" + '\u0022' + " It is composed of a description, the character " + '\u0022' + "&" + '\u0022' + " and then the ffmpeg parameters." + Environment.NewLine + Environment.NewLine + "After " + '\u0022' + "%" + '\u0022' + " character it comes the output extension.", "Load presets file", MessageBoxButtons.OK, MessageBoxIcon.Information);
            open_file.ShowDialog();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void item_up_Click(object sender, EventArgs e)
        {

            if (dg_pr.SelectedCells.Count == 0 || dg_pr.SelectedCells.Count > 1) return;
            DataGridView dgv = dg_pr;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == 0)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex - 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex - 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void item_down_Click(object sender, EventArgs e)
        {
            if (dg_pr.SelectedCells.Count == 0 || dg_pr.SelectedCells.Count > 1 || dg_pr.SelectedCells[0].RowIndex == dg_pr.RowCount - 1) return;
            DataGridView dgv = dg_pr;
            try
            {
                int totalRows = dgv.Rows.Count;
                // get index of the row for the selected cell
                int rowIndex = dgv.SelectedCells[0].OwningRow.Index;
                if (rowIndex == totalRows - 1)
                    return;
                // get index of the column for the selected cell
                int colIndex = dgv.SelectedCells[0].OwningColumn.Index;
                DataGridViewRow selectedRow = dgv.Rows[rowIndex];
                dgv.Rows.Remove(selectedRow);
                dgv.Rows.Insert(rowIndex + 1, selectedRow);
                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Cells[colIndex].Selected = true;
            }
            catch { }
        }

        private void dg_pr_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                    !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dg_pr.DoDragDrop(
                    dg_pr.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dg_pr_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = dg_pr.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dg_pr_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dg_pr_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dg_pr.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
                dg_pr.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dg_pr.Rows.RemoveAt(rowIndexFromMouseDown);
                try { dg_pr.Rows.Insert(rowIndexOfItemUnderMouseToDrop, rowToMove); } catch { }

            }
        }

        private void btn_inc_font_Click(object sender, EventArgs e)
        {
            try { dg_pr.Font = new Font("Microsoft Sans Serif", dg_pr.Font.Size + 0.5f); } catch { }
        }

        private void btn_decr_font_Click(object sender, EventArgs e)
        {
            try { dg_pr.Font = new Font("Microsoft Sans Serif", dg_pr.Font.Size - 0.5f); } catch { }
        }

        private void btn_load_bck_Click(object sender, EventArgs e)
        {
            string path;

            if (is_portable == false)
            {
                path = Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets_bck.ini";

            }
            else
            {
                path = port_path + "ff_presets_bck_portable.ini";
            }

            String param = "";
            String format = "";
            dg_pr.Rows.Clear();
            if (!File.Exists(path))
            {
                MessageBox.Show(FFBatch.Properties.Strings.no_bck);
                return;
            }
            foreach (string line in File.ReadLines(path))
            {
                if (line.Contains("PR: "))
                {
                    int cortar = line.LastIndexOf("%") - line.LastIndexOf("&");
                    format = line.Substring(line.LastIndexOf("%") + 2);
                    try
                    {
                        param = line.Substring(line.LastIndexOf("&") + 2, cortar - 3);

                        dg_pr.Rows.Add(line.Substring(4, line.LastIndexOf("&") - 5), param, format);

                    }
                    catch
                    {
                        // MessageBox.Show("Invalid preset data found.", "Preset error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btn_remove_pr_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dg_pr.SelectedCells)
            {
                try
                {
                    dg_pr.Rows.RemoveAt(cell.RowIndex);
                }
                catch
                {

                }
            }
        }

        private void btn_add_pr_Click(object sender, EventArgs e)
        {
            dg_pr.Rows.Add(FFBatch.Properties.Strings.new_preset2 + (dg_pr.RowCount + 1).ToString(), "", "");
            dg_pr.ClearSelection();//If you want

            int nRowIndex = dg_pr.Rows.Count - 1;
            int nColumnIndex = 0;

            dg_pr.Rows[nRowIndex].Selected = true;
            dg_pr.Rows[nRowIndex].Cells[nColumnIndex].Selected = true;
            dg_pr.CurrentCell = dg_pr.Rows[nRowIndex].Cells[0];
            dg_pr.BeginEdit(true);

            //In case if you want to scroll down as well.
            dg_pr.FirstDisplayedScrollingRowIndex = nRowIndex;
        }

        private void dg_pr_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            duplicates = false;
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                if (row.Index != e.RowIndex & !row.IsNewRow)
                {
                    if (row.Cells[0].Value.ToString() == e.FormattedValue.ToString())
                    {
                        dg_pr.Rows[e.RowIndex].ErrorText =
                            FFBatch.Properties.Strings.dup_not;
                        MessageBox.Show(FFBatch.Properties.Strings.dup_pres_n);
                        e.Cancel = true;
                        return;
                    }
                }
            }
            dg_pr.Rows[e.RowIndex].ErrorText = string.Empty;
            duplicates = false;
        }

        private void btn_save_backup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (is_portable == false) folderBrowserDialog1.Description = FFBatch.Properties.Strings.sel_path_pres + " " +"(ff_presets.ini)";
            else folderBrowserDialog1.Description = FFBatch.Properties.Strings.sel_path_pres + " " + "(ff_presets_portable.ini)";

            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                folderBrowserDialog1.Description = "";
                return;
            }

            folderBrowserDialog1.Description = "";

            if (is_portable == false)
            {
                if (File.Exists(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini"))
                {
                    if (File.Exists(folderBrowserDialog1.SelectedPath + "\\" + "ff_presets.ini"))
                    {
                        var a = MessageBox.Show(FFBatch.Properties.Strings.over_pres_loc, FFBatch.Properties.Strings.conf_act, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (a == DialogResult.No) return;
                    }

                    try
                    {
                        File.Copy(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini", folderBrowserDialog1.SelectedPath + "\\" + "ff_presets.ini", true);
                        MessageBox.Show(FFBatch.Properties.Strings.conf_saved, FFBatch.Properties.Strings.conf_rest, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception excpt)
                    {
                        MessageBox.Show(FFBatch.Properties.Strings.err_saving_pres + Environment.NewLine + excpt.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                try
                {
                    if (File.Exists(port_path + "ff_presets_portable.ini"))
                    {
                        File.Copy(System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_batch_presets.ini", folderBrowserDialog1.SelectedPath + "\\" + "ff_batch_presets.ini", true);
                        MessageBox.Show(FFBatch.Properties.Strings.conf_saved, FFBatch.Properties.Strings.conf_rest, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception excpt)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.err_set + Environment.NewLine + excpt.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog3 = new OpenFileDialog();
            openFileDialog3.Filter = "Presets" + " " + "|ff_presets*.ini|" + FFBatch.Properties.Strings.conf_files + " " + "|*.ini|" + FFBatch.Properties.Strings.all_files + "(*.*)|*.*";

            if (openFileDialog3.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            String backup = String.Empty;
            String dest = String.Empty;
            if (is_portable == false)
            {
                backup = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets_bck.ini";
                dest = System.IO.Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch") + "\\" + "ff_presets.ini";
            }
            else
            {
                backup = port_path + "ff_presets_bck_portable.ini";
                dest = port_path + "ff_presets_portable.ini";
            }
            try
            {
                File.Copy(dest, backup, true);
                File.Copy(openFileDialog3.FileName, dest, true);
                //reload_config = true;
                //cancel = true;
                //ActiveForm.Close();
                dg_pr.Rows.Clear();
                read_presets();
                MessageBox.Show(FFBatch.Properties.Strings.pres_file + " " + FFBatch.Properties.Strings.was_rest, FFBatch.Properties.Strings.pres_rest, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception excpt)
            {
                MessageBox.Show(FFBatch.Properties.Strings.err_set + Environment.NewLine + Environment.NewLine + excpt.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            dg_pr.Rows.Clear();
        }
    }
}
  