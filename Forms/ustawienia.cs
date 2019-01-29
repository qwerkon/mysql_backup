using System;
using System.IO;
using System.Windows.Forms;

namespace database_backup_manager
{
    public partial class ustawienia : Form
    {
        private ustawieniaAplikacji _aUstawienia;
        
        public ustawienia()
        {
            InitializeComponent();
            _aUstawienia = new ustawieniaAplikacji();

            applicationConfig oApplication = new applicationConfig();
            oApplication.sPathToConfigFile = Application.StartupPath + @"\settings.xml";
            oApplication.ReadBinary();

            _aUstawienia = oApplication.aUstawienia;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string sSciezkaDoKataloguZkopiami = folderBrowserDialog1.SelectedPath;
                textBox1.Text = sSciezkaDoKataloguZkopiami;
                if (_aUstawienia.aUstawienia.ContainsKey("sciezka"))
                {
                    _aUstawienia.aUstawienia["sciezka"] = sSciezkaDoKataloguZkopiami;
                }
                else
                {
                    _aUstawienia.aUstawienia.Add("sciezka", sSciezkaDoKataloguZkopiami);
                }

                applicationConfig oApplication = new applicationConfig();
                oApplication.sPathToConfigFile = Application.StartupPath + @"\settings.xml";
                oApplication.Write(_aUstawienia);

                this.Close();
            }
        }

        private void ustawienia_Load(object sender, EventArgs e)
        {
            if (_aUstawienia.aUstawienia.ContainsKey("sciezka"))
            {
                textBox1.Text = Convert.ToString(_aUstawienia.aUstawienia["sciezka"]);
            }
            else
            {
                textBox1.Text = Application.StartupPath + @"\backups\";
            }
        }
    }
}
