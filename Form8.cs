using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form8 : Form
    {
        public String url_dg_item = String.Empty;
        public String thumb_url_streams = String.Empty;
        public String format_ID = "";
        Boolean working = true;

        public Form8()
        {
            InitializeComponent();
        }

        private void init_dg()
        {
            dg_streams.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dg_streams.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dg_streams.RowHeadersVisible = false;
            dg_streams.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            dg_streams.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dg_streams.Columns[2].ReadOnly = true;
            dg_streams.Columns[3].ReadOnly = true;
            dg_streams.Columns[4].ReadOnly = true;
            dg_streams.Rows.Clear();
        }

        private void Form8_Load(object sender, EventArgs e)
        {            
            init_dg();
            btn_close.Focus();

            Process yt = new Process();
            Task t2 = Task.Run(() =>
            {
                yt.StartInfo.FileName = System.IO.Path.Combine(Application.StartupPath, "youtube-dl.exe");
                yt.StartInfo.Arguments = "-F " + url_dg_item;
                yt.StartInfo.RedirectStandardOutput = true;
                yt.StartInfo.UseShellExecute = false;
                yt.StartInfo.CreateNoWindow = true;
                yt.EnableRaisingEvents = true;
                yt.Start();
                String result = String.Empty;
                String stream = "";
                while (!yt.StandardOutput.EndOfStream)
                {
                    stream = yt.StandardOutput.ReadLine();
                    result = result + stream;
                    if (stream != null && stream != String.Empty && !stream.ToLower().Contains("format code") && !stream.ToLower().Contains("downloading webpage") && !stream.ToLower().Contains("available formats") && !stream.ToLower().Contains("downloading mpd manifest") && !stream.ToLower().Contains("downloading m3u8 information"))
                    {
                        dg_streams.Invoke(new MethodInvoker(delegate
                        {
                            String ext = stream.Substring(stream.IndexOf("          ") + 10, 4).Trim();
                            int j = stream.Length - (stream.Length - stream.Substring(stream.LastIndexOf("       ") + 7).Length);
                            j = j - stream.Substring(stream.LastIndexOf(" , ") - 6).Length;
                            String res = stream.Substring(stream.LastIndexOf("       ") + 7, j);

                            int i = stream.Length - (stream.Length - stream.Substring(stream.LastIndexOf(" , ") + 3).Length);
                            String codec = UppercaseFirst(stream.Substring(stream.LastIndexOf(" , ") + 3, i));
                            if (!stream.ToLower().Contains("audio only"))
                            {
                                dg_streams.Rows.Add(image_streams.Images[0], false, stream.Substring(0, stream.IndexOf("     ")).Trim(), ext, res, codec.Replace("MiB", " MB"));
                            }
                            else
                            {
                                dg_streams.Rows.Add(image_streams.Images[1], false, stream.Substring(0, stream.IndexOf("     ")).Trim(), ext, res.Replace("tiny", "   0p"), codec.Replace("MiB", " MB"));
                            }                            
                        }));

                    }
                }

                yt.WaitForExit();
                working = false;
                pic_wait_1.Invoke(new MethodInvoker(delegate
                { pic_wait_1.Visible = false; }));

                pic_yout.Invoke(new MethodInvoker(delegate
                { pic_yout.Visible = true; }));

                dg_streams.Invoke(new MethodInvoker(delegate
                { dg_streams.Visible = true; }));
                                
                txt_file.Invoke(new MethodInvoker(delegate
                {
                    txt_file.Text = url_dg_item;
                    txt_name.Enabled = true;
                }));
                txt_name.Invoke(new MethodInvoker(delegate
                {
                    txt_name.Text = "YOUTUBE AVAILABLE STREAMS";
                }));

                yt.StartInfo.Arguments = String.Empty;
                if (result == String.Empty || result == null)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        btn_select.Enabled = false;
                        label1.Visible = true;
                    }));
                    
                    
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        btn_close.Enabled = true;                        
                    }));

                }
                dg_streams.Invoke(new MethodInvoker(delegate
                {
                    dg_streams.Refresh();
                }));

            });
            
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_select_Click(object sender, EventArgs e)
        {
            String f_vid = String.Empty;
            String f_aud = String.Empty;
            int i = 0;

            foreach (DataGridViewRow row in dg_streams.Rows)
            {
                if (Convert.ToBoolean(row.Cells[1].Value) == true)
                {
                    i = i + 1;
                    if (row.Cells[4].Value.ToString().ToLower().Contains("audio"))
                    {
                        f_aud =row.Cells[2].Value.ToString();                        
                    }
                    else
                    {

                        f_vid = row.Cells[2].Value.ToString();
                    }
                    if (i > 2)
                    {
                        MessageBox.Show("Please select only two compatible formats.");
                        return;                        
                    }
                }
            }           

            if (f_vid == String.Empty) format_ID = f_aud;
            if (f_aud == String.Empty) format_ID = f_vid;
            if (f_vid != String.Empty && f_aud != String.Empty) format_ID = f_vid + "+" + f_aud;
            this.Close();
        }

        public string UppercaseFirst(string text)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            // Return char and concat substring.
            return char.ToUpper(text[0]) + text.Substring(1);
        }
        
        private void Form8_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (working == true) e.Cancel = true;
        }     
    }
}
