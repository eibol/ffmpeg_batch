using Bluegrams.Application;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{    
    internal static class Program
    {        
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        ///

        [STAThread]
        private static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    DialogResult a = MessageBox.Show(Properties.Strings.multiple_inst, Properties.Strings.multiple_inst_0, MessageBoxButtons.YesNo);
                    if (a == DialogResult.No) return;                    
                }

                Boolean is_portable = false;
                String portable_flag = Application.StartupPath + "\\" + "portable.ini";
                if (File.Exists(portable_flag))
                {
                    is_portable = true;
                    ProfileOptimization.SetProfileRoot(Application.StartupPath);
                    PortableSettingsProvider.ApplyProvider(Properties.Settings.Default);
                }
                else
                {
                    ProfileOptimization.SetProfileRoot(Path.Combine(Environment.GetEnvironmentVariable("appdata"), "FFBatch"));
                }
                
                ProfileOptimization.StartProfile("FFBatch.Startup.Profile");
                if (Properties.Settings.Default.visuals == true && Properties.Settings.Default.visuals_all == false) Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);                
                Application.Run(new Form1());                
            }
        }

        private static string appGuid = "01c90e3d-419d-4bf5-9fcc-fcebe6b16840";
    }

    internal class SingleInstanceApp : WindowsFormsApplicationBase
    {
        public SingleInstanceApp()
            : base()
        {
            this.IsSingleInstance = true;
        }

        protected override void OnStartupNextInstance(
            StartupNextInstanceEventArgs e)
        {
            base.OnStartupNextInstance(e);
            string[] secondInstanceArgumens = e.CommandLine.Skip(1).ToArray();

            String other_file = Path.GetTempPath() + "\\" + "FFBatch_test" + "\\" + "Other_instance.fftmp";
            try
            {
                File.WriteAllLines(other_file, secondInstanceArgumens);
            }
            catch { }
            // Handle command line arguments of second instance

            //if (e.BringToForeground)
            //{
            this.MainForm.BringToFront();
            //}
        }

        protected override void OnCreateMainForm()
        {
            base.OnCreateMainForm();
            this.MainForm = new Form1();
        }
    }
}