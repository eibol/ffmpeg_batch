using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form27 : Form
    {
        public String desc_pr = "";
        public String pre_in_pr = "";
        public String params_pr = "";
        public String format_pr = "";
        private String port_path = Application.StartupPath + "\\" + "settings" + "\\";
        private Boolean is_portable = false;
        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;        
        private Boolean duplicates = false;

        public Form27()
        {
            InitializeComponent();
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

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form27));
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

        private void Form27_Load(object sender, EventArgs e)
        {
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                dg_pr.BackgroundColor = Color.Gray;
                dg_pr.RowsDefaultCellStyle.BackColor = Color.Gray;
                dg_pr.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 79, 79, 99);
                dg_pr.RowsDefaultCellStyle.BackColor = Color.Gray;
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                dg_pr.BackgroundColor = SystemColors.InactiveBorder;
                dg_pr.RowsDefaultCellStyle.BackColor = Color.White;
                dg_pr.AlternatingRowsDefaultCellStyle.BackColor = Color.AliceBlue;
            }

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
                      
            
            read_presets();
            dg_pr.ClearSelection();
            refresh_lang();
            this.Text = Properties.Strings.online_pr;
            char[] letters = this.Text.ToCharArray();
            letters[0] = char.ToUpper(letters[0]);
            this.Text = new string(letters);
            // upper case the first char

            dg_pr.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_pr.Columns[0].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_pr.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_pr.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_pr.Columns[0].HeaderText = FFBatch.Properties.Strings.Name;
            dg_pr.Columns[1].HeaderText = FFBatch.Properties.Strings.pre_input;
            dg_pr.Columns[2].HeaderText = FFBatch.Properties.Strings.ff_params;
            dg_pr.Columns[3].HeaderText = FFBatch.Properties.Strings.Format;
        }
                
        private void read_presets()
        {
            String url_presets = "https://raw.githubusercontent.com/eibol/ffmpeg_batch/master/presets.md";
            String dsc = "";
            String pre_i = "";
            String param = "";
            String format = "";

            String path = Path.Combine(Path.GetTempPath(), "FFBatch_test") + "\\" + "presets.md";

            try
            {
                WebClient client = new WebClientWithTimeout();
                client.DownloadFile(url_presets, path);
            }
            catch  (Exception exc) { MessageBox.Show(FFBatch.Properties.Strings.err_con2 + Environment.NewLine + Environment.NewLine + exc.Message, FFBatch.Properties.Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); return; }

            try
            {                
                foreach (String line in File.ReadLines(path))
                {
                    String line_out = line;
                    if (line.Contains("|"))
                    {                        
                        if (line.Substring(0, 1) == "|" && !line.ToLower().Contains("description") && !line.ToLower().Contains("----"))
                        {                            
                            if (line.Contains("\\|"))
                            {                                
                                line_out = line.Replace("\\|", "to_replace");                                
                            }
                            String[] split = line_out.Split('|');                         
                            dsc = split[1].TrimStart().TrimEnd();
                            pre_i = split[2].TrimStart().TrimEnd();
                            param = split[3].TrimStart().TrimEnd();
                            format = split[4].TrimStart().TrimEnd();                           
                            pre_i = pre_i.Replace("to_replace", "|");
                            param = param.Replace("to_replace", "|");                           
                            dg_pr.Rows.Add(dsc, pre_i, param, format);
                      
                        }
                    }
                }
            }
            catch { MessageBox.Show(Properties.Strings.Format + " " + FFBatch.Properties.Strings.error); }
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
            if (dg_pr.SelectedCells.Count == 0) return;
            if (dg_pr.SelectedCells.Count == 1 || dg_pr.SelectedRows.Count == 1)
            {
                desc_pr = dg_pr.Rows[dg_pr.SelectedCells[0].RowIndex].Cells[0].Value.ToString() + "-(online)";
                pre_in_pr = dg_pr.Rows[dg_pr.SelectedCells[0].RowIndex].Cells[1].Value.ToString();
                params_pr = dg_pr.Rows[dg_pr.SelectedCells[0].RowIndex].Cells[2].Value.ToString();
                format_pr = dg_pr.Rows[dg_pr.SelectedCells[0].RowIndex].Cells[3].Value.ToString();
            }
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void Form27_Resize(object sender, EventArgs e)
        {
            link1.Left = this.Width / 2 - link1.Width / 2 - 9;
            dg_pr.Width = this.Width - 59;
            dg_pr.Height = this.Height - 198;
            dg_pr.Columns[2].Width = dg_pr.Width - 454;          
            btn_load.Top = this.Height - 135;
            btn_load.Left = this.Width / 2 - 75;
            btn_cancel.Top = this.Height - 135;
            btn_cancel.Left = this.Width / 2 + 10;
            btn_decr_font.Left = this.Width - 63;
            btn_inc_font.Left = this.Width - 86;

            if (this.Width < 962)
            {
                this.Width = 962;
                return;
            }
            if (this.Height < 567)
            {
                this.Height = 567;
                return;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/eibol/ffmpeg_batch/blob/master/presets.md");
        }
    }
}