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
    public partial class Form32 : Form
    {
        private String port_path = Application.StartupPath + "\\" + "settings" + "\\";
        private Boolean is_portable = false;

        public Form32()
        {
            InitializeComponent();
        }

        private void refresh_lang()
        {            
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form32));
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

        private void Create_Tooltips()
        {
            ToolTip toolT1 = new ToolTip();
            ToolTip toolT2 = new ToolTip();
            ToolTip toolT3 = new ToolTip();
            toolT1.RemoveAll();
            toolT2.RemoveAll();
            toolT3.RemoveAll();

            toolT1.AutoPopDelay = 9000;
            toolT1.InitialDelay = 750;
            toolT1.ReshowDelay = 500;
            toolT1.ShowAlways = true;
            toolT1.SetToolTip(this.btn_add, FFBatch.Properties.Strings.add_file);

            toolT2.AutoPopDelay = 9000;
            toolT2.InitialDelay = 750;
            toolT2.ReshowDelay = 500;
            toolT2.ShowAlways = true;
            toolT2.SetToolTip(this.btn_close, FFBatch.Properties.Strings.close_win);

            toolT3.AutoPopDelay = 9000;
            toolT3.InitialDelay = 750;
            toolT3.ReshowDelay = 500;
            toolT3.ShowAlways = true;
            toolT3.SetToolTip(this.btn_clear, FFBatch.Properties.Strings.clear_list);
        }

            private void Form32_Load(object sender, EventArgs e)
        {
            Create_Tooltips();
            String portable_flag = Application.StartupPath + "\\" + "portable.ini";            
            if (File.Exists(portable_flag)) is_portable = true;
            else is_portable = false;

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

            refresh_lang();
                      
            dg_pr.AllowUserToAddRows = false;

            foreach (String str in Properties.Settings.Default.excl_list)
            {
                if (str.Length > 0) this.dg_pr.Rows.Add(str);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.dg_pr.Rows.Add("");            
            DataGridViewCell cell = dg_pr.Rows[dg_pr.RowCount - 1].Cells[0];
            dg_pr.CurrentCell = cell;
            dg_pr.BeginEdit(true);
        }

        private void dg_pr_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                try { dg_pr.Rows.RemoveAt(e.RowIndex); } catch { }  
            }
        }

        private void dg_pr_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 1 && e.RowIndex > -1)
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

        private void dg_pr_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            btn_add.Focus();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dg_pr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) MessageBox.Show("Enter");
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show(FFBatch.Properties.Strings.clear_list + ". " + Properties.Strings.contin, FFBatch.Properties.Strings.clear_list, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (a == DialogResult.Yes) { dg_pr.Rows.Clear(); }            
        }
    }
}
