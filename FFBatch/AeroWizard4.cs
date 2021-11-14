using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class AeroWizard4 : Form
    {
        public AeroWizard4()
        {
            InitializeComponent();
        }

        public Boolean ok_silence = true;
        private String pr_1st_params = String.Empty;

        public String pr1_first_params
        {
            get { return pr_1st_params; }
            set { pr_1st_params = value; }
        }

        public Boolean wiz_ok_silence
        {
            get { return ok_silence; }
            set { ok_silence = value; }
        }

        private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ok_silence = false;
        }

        private void wz1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            pr_1st_params = "-af silencedetect=n=-" + n_db.Value.ToString() + "dB" + ":d=" + n_seconds.Value.ToString() + " -f null -";
        }

        private void AeroWizard4_Load(object sender, EventArgs e)
        {
            refresh_lang();
            if (Properties.Settings.Default.app_lang != "en" && Properties.Settings.Default.app_lang != "es")
            {
                wizardControl1.NextButtonText = Properties.Strings2.next;
                wizardControl1.CancelButtonText = Properties.Strings.cancel;
                wizardControl1.FinishButtonText = Properties.Strings2.finish;
            }
        }

        private void refresh_lang()
        {
            //Thread.CurrentThread.CurrentCulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(FFBatch.Properties.Settings.Default.app_lang, true);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AeroWizard4));
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