using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.IO;
using System.Linq;
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
                //if (!mutex.WaitOne(0, false))
                //{
                //}

                Application.SetCompatibleTextRenderingDefault(false);
                Application.EnableVisualStyles();
                //if (Environment.GetCommandLineArgs().Count() == 1)
                //{
                    Form6 form_intro = new Form6();
                    form_intro.Show();
                    form_intro.Refresh();
                //}
                new SingleInstanceApp().Run(Environment.GetCommandLineArgs());
                //Application.Run(new Form1());
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