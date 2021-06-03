using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public String stream_n = String.Empty;
        public String lv1_item = String.Empty;
        public int Id = 0;

        private void Form5_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (FFBatch.Properties.Settings.Default.app_lang == "en")
            {
                this.Text = "Multimedia streams";
                dg_streams.Columns[2].HeaderText = "Stream output";

            }
            if (FFBatch.Properties.Settings.Default.app_lang == "es")
            {
                this.Text = "Flujos multimedia";
                dg_streams.Columns[2].HeaderText = "Información de salida";                
            }

            this.Enabled = false;

            dg_streams.BackgroundColor = this.BackColor;
            dg_streams.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.RowHeadersVisible = false;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_streams.Columns[1].ReadOnly = true;
            dg_streams.Columns[2].ReadOnly = true;
            dg_streams.Rows.Clear();
            String filepath, name = "";
            

            if (!lv1_item.ToLower().Contains("http"))
            {
                filepath = lv1_item.Substring(0, lv1_item.LastIndexOf("\\"));
                name = lv1_item.Substring(filepath.Length + 1, lv1_item.Length - filepath.Length - 1);
            }
            else
            {
                name = lv1_item;
            }
            new System.Threading.Thread(() =>
            {
                this.InvokeEx(f => txt_file.Text = name);
            
            ff_str.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "ffmpeg.exe");
            ff_str.StartInfo.Arguments = " -i " + '\u0022' + lv1_item + '\u0022';
            ff_str.StartInfo.RedirectStandardOutput = true;
            ff_str.StartInfo.RedirectStandardError = true;
            ff_str.StartInfo.UseShellExecute = false;
            ff_str.StartInfo.CreateNoWindow = true;
            ff_str.EnableRaisingEvents = true;
            ff_str.Start();
                
                Form11 frm_prog = new Form11();               
                frm_prog.Refresh();
                frm_prog.label1.Text = "Obtaining streams information";
                frm_prog.label1.Refresh();
                
                new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.CurrentThread.IsBackground = true;                              
                    frm_prog.ShowDialog();
                    frm_prog.Refresh();

                }).Start();
                frm_prog.procId = ff_str.Id;                
                String stream = "";
            String sub_str = "";
            int f_streams = -1;
            Boolean has_stream = false;
            int img = 0;
              while (!ff_str.StandardError.EndOfStream)
                {                    
                        stream = ff_str.StandardError.ReadLine();

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
                                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 11), (stream.Length - sub_str.Length))));

                                }
                                else
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 4);
                                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[img], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 4), (stream.Length - sub_str.Length))));

                                }
                            }
                            else
                            {
                                if (stream.Contains("Video"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[0], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length))));
                                }
                                if (stream.Contains("Audio"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[1], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length))));
                                }
                                if (stream.Contains("Subtitle"))
                                {
                                    sub_str = stream.Substring(0, stream.LastIndexOf("#0:") + 6);
                                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[2], "#0:" + f_streams.ToString(), stream.Substring((stream.LastIndexOf("#0:") + 6), (stream.Length - sub_str.Length))));
                                }
                            }
                        }               
                    
                }                
                    this.InvokeEx(f => this.Enabled = true);
                    ff_str.WaitForExit(10000);
                    this.InvokeEx(f => this.Enabled = true);                
           
                try
                {
                    frm_prog.Invoke(new MethodInvoker(delegate
                    {
                        frm_prog.Dispose();
                    }));
                }
                catch { }

                    this.InvokeEx(f => this.Enabled = true);
                    if (frm_prog.abort_validate == true)
                    {
                        this.InvokeEx(f => this.Close());
                        return;
                    }
         
            if (has_stream == false)
            {
                    this.InvokeEx(f => dg_streams.Rows.Add(img_streams.Images[3], "0", "No usable streams found"));
            }
            if (dg_streams.Rows.Count <= 11)
            {
                    this.InvokeEx(f => this.Width = 516);
                    this.InvokeEx(f => f.dg_streams.Width = 473);
                    this.InvokeEx(f => f.txt_file.Width = 473);
                    this.InvokeEx(f => f.txt_name.Width = 473);
                    this.InvokeEx(f => f.btn_close.Width = 473);
            }
            else
            {
                    this.InvokeEx(f => this.Width = 533);
                    this.InvokeEx(f => dg_streams.Width = 490);
                    this.InvokeEx(f => txt_file.Width = 490);
                    this.InvokeEx(f => txt_name.Width = 490);
                    this.InvokeEx(f => btn_close.Width = 490);
            }

                this.InvokeEx(f => dg_streams.ClearSelection());
                this.InvokeEx(f => dg_streams.CurrentCell = null);
                this.InvokeEx(f => btn_close.Focus());
            try
            {
                frm_prog.Invoke(new MethodInvoker(delegate
                {
                    frm_prog.Dispose();
                }));
            }
            catch { }
            }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ct1_Click(object sender, EventArgs e)
        {
            if (dg_streams.SelectedCells.Count > 0)
            {
                Clipboard.SetText(dg_streams.SelectedCells[0].Value.ToString());
            }
        }

        private void menu_grid_Opening(object sender, CancelEventArgs e)
        {
            if (dg_streams.SelectedCells.Count > 1) e.Cancel = true;
            if (dg_streams.SelectedCells.Count == 0) e.Cancel = true;
            if (dg_streams.SelectedCells[0].ColumnIndex == 0) e.Cancel = true;

        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form5));
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
    }
}
