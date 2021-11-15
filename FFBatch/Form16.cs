using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;

//using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form16 : Form
    {
        public String flnm, ff_params, output_flnm = String.Empty;
        public Boolean start_jobs = true;
        public Boolean view_logs = true;
        private Boolean clearing = false;

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
                recount();
            }
            catch { }
        }

        private void item_down_Click(object sender, EventArgs e)
        {
            if (dg_pr.SelectedCells.Count == 0 || dg_pr.SelectedCells.Count > 1) return;
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
                recount();
            }
            catch { }
        }

        private void recount()
        {
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                row.Cells[0].Value = (row.Index + 1).ToString();
            }
        }

        private void dg_pr_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //I supposed your button column is at index 0
            if (e.ColumnIndex == 6)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = pic.Image.Width;
                var h = pic.Image.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(pic.Image, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dg_pr_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (dg_pr.RowCount > 0) btn_jobs.Enabled = true;
            else btn_jobs.Enabled = false;
        }

        private void btn_clear_list_Click(object sender, EventArgs e)
        {
            clearing = true;
            dg_pr.Rows.Clear();
            clearing = false;
        }

        private void dg_pr_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dg_pr.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                MessageBox.Show(Properties.Strings2.ff_copied + Environment.NewLine + Environment.NewLine + dg_pr.Rows[e.RowIndex].Cells[2].Value.ToString());
                Clipboard.SetText(dg_pr.Rows[e.RowIndex].Cells[2].Value.ToString());
            }
        }

        private void tooltips()
        {
            ToolTip toolT0z = new ToolTip();
            toolT0z.AutoPopDelay = 9000;
            toolT0z.InitialDelay = 750;
            toolT0z.ReshowDelay = 500;
            toolT0z.ShowAlways = true;
            toolT0z.SetToolTip(this.requeue, Properties.Strings2.reset_m_jobs);
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            start_jobs = false;
            view_logs = false;
            tooltips();
            btn_jobs.Enabled = true;
            refresh_lang();
            this.Text = FFBatch.Properties.Strings2.mux_jobs;
            dg_pr.Columns[1].HeaderText = FFBatch.Properties.Strings.filename;
            dg_pr.Columns[2].HeaderText = FFBatch.Properties.Strings.ff_params;
            dg_pr.Columns[3].HeaderText = FFBatch.Properties.Strings2.streams;
            dg_pr.Columns[4].HeaderText = FFBatch.Properties.Strings.duration;
            dg_pr.Columns[5].HeaderText = FFBatch.Properties.Strings.output;
            if (Properties.Settings.Default.dark_mode == true)
            {
                foreach (Control c in this.Controls) UpdateColorDark(c);
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                dg_pr.BackgroundColor = Color.Gray;
                dg_pr.RowsDefaultCellStyle.BackColor = Color.Gray;
            }
            else
            {
                foreach (Control c in this.Controls) UpdateColorDefault(c);
                this.BackColor = SystemColors.InactiveBorder;
                dg_pr.BackgroundColor = SystemColors.InactiveBorder;
                dg_pr.RowsDefaultCellStyle.BackColor = Color.White;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form16));
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

        private void btn_inc_font_Click(object sender, EventArgs e)
        {
            try { dg_pr.Font = new Font("Microsoft Sans Serif", dg_pr.Font.Size + 0.5f); } catch { }
        }

        private void btn_decr_font_Click(object sender, EventArgs e)
        {
            try { dg_pr.Font = new Font("Microsoft Sans Serif", dg_pr.Font.Size - 0.5f); } catch { }
        }

        private void requeue_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private void btn_jobs_Click(object sender, EventArgs e)
        {
            view_logs = false;
            List<string> no_overw = new List<string>();
            foreach (DataGridViewRow row in dg_pr.Rows) no_overw.Add(row.Cells[5].Value.ToString());

            var duplicates = no_overw.GroupBy(s => s).SelectMany(grp => grp.Skip(1));
            int count = no_overw.Count;
            no_overw = no_overw.Distinct().ToList();
            if (no_overw.Count < count)
            {
                MessageBox.Show(FFBatch.Properties.Strings.out_exists);
                return;
            }

            Boolean ret = false;
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.LightGreen || row.DefaultCellStyle.BackColor == Color.LightSalmon)
                {
                    MessageBox.Show(FFBatch.Properties.Strings.jobs_reset, FFBatch.Properties.Strings.items_alr, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ret = true;
                    break;
                }
            }
            if (ret == true) return;

            if (dg_pr.RowCount > 0)
            {
                start_jobs = true;
                this.Close();
            }
            else MessageBox.Show(FFBatch.Properties.Strings.jobs_blank);
        }

        private void dg_pr_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            List<string> no_overw = new List<string>();
            foreach (DataGridViewRow row in dg_pr.Rows) no_overw.Add(row.Cells[5].Value.ToString());

            var duplicates = no_overw.GroupBy(s => s).SelectMany(grp => grp.Skip(1));
            int count = no_overw.Count;
            no_overw = no_overw.Distinct().ToList();
            if (no_overw.Count < count && clearing == false)
            {
                MessageBox.Show(FFBatch.Properties.Strings.out_exists);
            }
            if (dg_pr.RowCount > 0) btn_jobs.Enabled = true;
            else btn_jobs.Enabled = false;
        }

        private void dg_pr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                dg_pr.Rows.RemoveAt(e.RowIndex);
            }
            if (dg_pr.RowCount > 0) btn_jobs.Enabled = true;
            else btn_jobs.Enabled = false;
        }

        private void btn_logs_url_Click(object sender, EventArgs e)
        {
            view_logs = true;
            this.Close();
        }

        private void Form16_Resize(object sender, EventArgs e)
        {
            dg_pr.Width = this.Width - 58;
            dg_pr.Height = this.Height - 202;
            dg_pr.Columns[5].Width = this.Width - 950;
            label1.Left = (this.Width / 2) - (label1.Width / 2) - 7;

            btn_clear_list.Left = label1.Left;
            btn_cancel.Left = btn_clear_list.Left + btn_clear_list.Width + 5;
            btn_jobs.Left = btn_cancel.Left + btn_cancel.Width + 5;
            btn_logs_url.Left = btn_jobs.Left + btn_jobs.Width + 5;

            btn_clear_list.Top = this.Height - 127;
            btn_cancel.Top = this.Height - 127;
            btn_jobs.Top = this.Height - 131;
            btn_logs_url.Top = this.Height - 132;

            item_up.Left = this.Width - 73;
            item_down.Left = this.Width - 96;
            requeue.Left = this.Width - 121;
            btn_decr_font.Left = this.Width - 146;
            btn_inc_font.Left = this.Width - 168;

            if (this.Width < 1113) this.Width = 1113;
            if (this.Height < 571) this.Height = 571;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            start_jobs = false;
            view_logs = false;
            this.Close();
        }

        public Form16()
        {
            InitializeComponent();
        }
    }
}