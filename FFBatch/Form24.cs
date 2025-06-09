using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFBatch
{
    public partial class Form24 : Form
    {
        public Form24()
        {
            InitializeComponent();
        }
        
        String restart = String.Empty;
        private void Form24_Load(object sender, EventArgs e)
        {
            if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "es") combo_lang.SelectedIndex = 1;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "fr") combo_lang.SelectedIndex = 2;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "it") combo_lang.SelectedIndex = 3;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "pl") combo_lang.SelectedIndex = 4;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "pt") combo_lang.SelectedIndex = 5;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "cn") combo_lang.SelectedIndex = 6;
            else if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ar") combo_lang.SelectedIndex = 7;
            else combo_lang.SelectedIndex = 0;
            label1.TextAlign = HorizontalAlignment.Right;         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void combo_lang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combo_lang.SelectedIndex == 0)
            {
                label1.Text = "Select language";
                this.Text = "Application language";
                button1.Text = "OK";
                chk_dark.Text = "User dark theme";
                restart = "Application will be restarted.";
            }
            if (combo_lang.SelectedIndex == 1)
            {
                label1.Text = "Elija idioma";
                this.Text = "Idioma de la aplicación";
                button1.Text = "Aceptar";
                chk_dark.Text = "Usar tema oscuro";
                restart = "La aplicación se reiniciará.";
            }

            if (combo_lang.SelectedIndex == 2)
            {
                label1.Text = "Choisir la langue";
                this.Text = "Sélectionner la langue de l'application";
                button1.Text = "D'ACCORD";
                chk_dark.Text = "Activer le mode sombre";
                restart = "L'application sera redémarrée.";
            }

            if (combo_lang.SelectedIndex == 3)
            {
                label1.Text = "Seleziona lingua";
                this.Text = "Seleziona lingua applicazione";
                button1.Text = "OK";
                chk_dark.Text = "Usa la modalità scura";
                restart = "L'applicazione verrà riavviata.";
            }            

            if (combo_lang.SelectedIndex == 4)
            {
                label1.Text = "Wybierz język";
                this.Text = "Język aplikacji";
                button1.Text = "OK";
                chk_dark.Text = "Ciemny motyw użytkownika";
                restart = "Aplikacja zostanie uruchomiona ponownie.";
            }

            if (combo_lang.SelectedIndex == 5)
            {
                label1.Text = "Selecione o língua";
                this.Text = "Idioma do aplicativo";
                button1.Text = "OK";
                chk_dark.Text = "Use o modo escuro";
                restart = "O aplicativo será reiniciado.";
            }

            if (combo_lang.SelectedIndex == 6)
            {
                label1.Text = "选择语言";
                this.Text = "应用语言";
                button1.Text = "确定";
                chk_dark.Text = "开启夜间模式";
                restart = "应用程序将重新启动。";
            }
            if (combo_lang.SelectedIndex == 7)
            {
                label1.Text = "اختار اللغة";
                this.Text = "لغة التطبيق";
                button1.Text = "نعم";
                chk_dark.Text = "استخدم الوضع الداكن";
                restart = "سيتم إعادة تشغيل التطبيق.";
            }
            chk_dark.Checked = !chk_dark.Checked;
            chk_dark.Checked = !chk_dark.Checked;
        }

        private void chk_dark_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_dark.Checked)
            {
                this.BackColor = Color.FromArgb(255, 64, 64, 64);
                button1.BackColor = Color.LightGray;

            }
            else
            {
                this.BackColor = SystemColors.InactiveBorder;
                button1.BackColor = SystemColors.InactiveBorder;
            }
            
        }
    }
}