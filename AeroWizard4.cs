using System;
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
        String pr_1st_params = String.Empty;

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

        private void wizardControl1_SelectedPageChanged(object sender, EventArgs e)
        {

        }

        private void wizardControl1_Cancelling(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ok_silence = false;
        }

        private void wz1_Commit(object sender, AeroWizard.WizardPageConfirmEventArgs e)
        {
            pr_1st_params = "-af silencedetect=n=-" + n_db.Value.ToString() + "dB" + ":d=" + n_seconds.Value.ToString() + " -f null -";
        }
    }
}
