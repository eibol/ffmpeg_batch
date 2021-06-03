using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form16 : Form
    {
        public String flnm, ff_params, output_flnm = String.Empty;
        public Boolean start_jobs = false;
        public Boolean view_logs = false;


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
            dg_pr.Rows.Clear();
        }

        private void dg_pr_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                dg_pr.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                MessageBox.Show("FFMPEG PARAMETERS COPIED TO CLIPBOARD:" + Environment.NewLine + Environment.NewLine + dg_pr.Rows[e.RowIndex].Cells[2].Value.ToString());
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
            toolT0z.SetToolTip(this.requeue, "Reset jobs list status");
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            start_jobs = false;
            tooltips();
            btn_jobs.Enabled = true;

            refresh_lang();

            if (FFBatch.Properties.Settings.Default.app_lang == "en")
            {
                this.Text = "Multiplex jobs manager";
                dg_pr.Columns[1].HeaderText = "Filename";
                dg_pr.Columns[2].HeaderText = "FFmpeg parameters";
                dg_pr.Columns[3].HeaderText = "Streams";
                dg_pr.Columns[4].HeaderText = "Duration";
                dg_pr.Columns[5].HeaderText = "Output";
            }

            if (FFBatch.Properties.Settings.Default.app_lang == "es")
            {
                this.Text = "Gestor de tareas de multiplexado";
                dg_pr.Columns[1].HeaderText = "Nombre de archivo";
                dg_pr.Columns[2].HeaderText = "Parámetros FFmpeg";
                dg_pr.Columns[3].HeaderText = "Flujos";
                dg_pr.Columns[4].HeaderText = "Duración";
                dg_pr.Columns[5].HeaderText = "Salida";
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
                MessageBox.Show("Output filename already exists. Please use a different name");
                return;
            }

            Boolean ret = false;
            foreach (DataGridViewRow row in dg_pr.Rows)
            {
                if (row.DefaultCellStyle.BackColor == Color.LightGreen || row.DefaultCellStyle.BackColor == Color.LightSalmon)
                {
                    MessageBox.Show("Jobs list was already processed. Please reset status with botton at the top right corner to start it over.", "Items already processed on jobs lists", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else MessageBox.Show("Jobs list is empty");
        }

        private void dg_pr_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            List<string> no_overw = new List<string>();
            foreach (DataGridViewRow row in dg_pr.Rows) no_overw.Add(row.Cells[5].Value.ToString());

            var duplicates = no_overw.GroupBy(s => s).SelectMany(grp => grp.Skip(1));
            int count = no_overw.Count;
            no_overw = no_overw.Distinct().ToList();
            if (no_overw.Count < count)
            {
                MessageBox.Show("Output filename already exists. Please use a different name");             
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
