using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using database_backup_manager.Interfaces;

namespace database_backup_manager
{
    public delegate void zapiszNaLiscie(object[] aDoZapisania);
    public delegate void aktualizacjaNaLiscie(int iRow, object[] aDoZapisania);
    public delegate void usunZlisty(int iRow);

    public partial class Form1 : Form
    {
        string sDirectory = Directory.GetCurrentDirectory();
        private string sXmlFileName;
        List<SortedList<string, string>> aListSerwerow = new List<SortedList<string, string>>();
        private PriorityQueue<Backup, int> pq = new PriorityQueue<Backup, int>();
        private int _iTimeShowBaloon = 5;

        public Form1()
        {
            InitializeComponent();
            sStripStatus.Text = "";
            notifyIcon.Visible = false;

            sXmlFileName = sDirectory + @"\servers.xml";
            if (!File.Exists(sXmlFileName))
            {
                stworzXML();
            }

            getListServers();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                for (int iCnt = 0; iCnt < aListSerwerow.Count; iCnt++)
                {
                    lista_serwerow.Rows.Add(new object[] { 
                        aListSerwerow[iCnt]["baza"], aListSerwerow[iCnt]["login"], aListSerwerow[iCnt]["host"], 
                        aListSerwerow[iCnt]["haslo"], aListSerwerow[iCnt]["priorytet"]
                    });
                }
            }
            catch (XmlException ex)
            {
                MessageBox.Show(ex.Message, "XML Error");
            }
        }

        private void getListServers()
        {
            aListSerwerow.Clear();
            dataSet1.Clear();
            dataSet1.ReadXml(sXmlFileName, XmlReadMode.Auto);

            foreach (DataTable aTable in dataSet1.Tables)
            {
                foreach (DataRow dtr in aTable.Rows)
                {
                    SortedList<string, string> aRow = new SortedList<string, string>();
                    foreach (DataColumn c in aTable.Columns)
                    {
                        /// MessageBox.Show(c.ColumnName.ToString(), dtr[c.ColumnName].ToString());
                        if (dtr[c.ColumnName].ToString() != null)
                        {
                            // MessageBox.Show(dtr[c.ColumnName].ToString(), c.ColumnName.ToString());
                            string sKlucz = Convert.ToString(c.ColumnName.ToString());
                            string sWartosc = Convert.ToString(dtr[c.ColumnName].ToString());
                            aRow.Add(sKlucz, sWartosc);
                        }
                    }

                    if (aRow.Count == 6)
                        aListSerwerow.Add(aRow);
                }
            }
        }

        private void stworzXML()
        {
            XmlTextWriter oTextWriter = new XmlTextWriter(sXmlFileName, null);
            oTextWriter.WriteStartDocument();
            oTextWriter.WriteStartElement("lista_serwerow");
            oTextWriter.WriteEndDocument();
            oTextWriter.Close();
        }

        private void btTworz_Click(object sender, EventArgs e)
        {
            //Hide();
            notifyIcon.Visible = true;

            sStripStatus.Text = "Rozpoczęcie tworzenia kopi zapasowych...";
            notifyIcon.Text = sStripStatus.Text;

            btDodajSerwer.Enabled = false;
            btTworz.Enabled = false;

            bgWorker.RunWorkerAsync();
        }

        private void btDodajSerwer_Click(object sender, EventArgs e)
        {
            dodaj_serwer oForm = new dodaj_serwer();
            oForm.zapiszNaLiscie = new zapiszNaLiscie(this.zapisz_naLiscie);
            oForm.ShowDialog();
        }

        private void zapisz_naLiscie(object[] aDoDodania)
        {
            lista_serwerow.Rows.Add(aDoDodania);
            getListServers();
        }

        private void aktualizuj_naLiscie(int iRow, object[] aDoAktualizacji)
        {
            lista_serwerow.Rows.RemoveAt(iRow);
            lista_serwerow.Rows.Insert(iRow, aDoAktualizacji);
            getListServers();
        }

        private void usun_z_listy(int iRow)
        {
            lista_serwerow.Rows.RemoveAt(iRow);

            XmlDocument oDoc = new XmlDocument();
            oDoc.Load(sXmlFileName);
            XmlNode oNode = oDoc.DocumentElement.ChildNodes.Item(iRow);
            oNode.ParentNode.RemoveChild(oNode);
            oDoc.Save(sXmlFileName);

            getListServers();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = true;
        }

        private void pokażProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Activate();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void zamknijProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Zamknięcie programu spowoduje przerwanie procesu tworzenia kopi.\nCzy jesteś tego pewien?", 
                "Zamknięcie programu", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Visible = true;
            }
        }

        /// <summary>
        /// edycja rekordu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lista_serwerow_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // numer wiersza
            int iRow = e.RowIndex;
            if (iRow > -1)
            {
                // wartosc pierwszego pola z listy, czyli identyfikator uzytkownika
                object oValue = lista_serwerow.Rows[iRow].Cells[0].Value;

                if (oValue != null)
                {
                    DataGridViewRow aRow = lista_serwerow.Rows[iRow];
                    dodaj_serwer oForm = new dodaj_serwer(iRow, aRow);
                    oForm.aktualizacjaNaLiscie = new aktualizacjaNaLiscie(this.aktualizuj_naLiscie);
                    oForm.usunZListy = new usunZlisty(this.usun_z_listy);
                    oForm.ShowDialog();
                }
            }
        }

        private int _iAllCnt;
        private delegate void rozpoczlijBackupDelegate();
        private void rozpocznijBackup()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new rozpoczlijBackupDelegate(this.rozpocznijBackup));
                return;
            }
            try
            {
                this.Hide();

                string sSciezka = String.Empty;

                applicationConfig oApplication = new applicationConfig();
                oApplication.sPathToConfigFile = Application.StartupPath + @"\settings.xml";
                oApplication.ReadBinary();

                if (oApplication.aUstawienia.aUstawienia.ContainsKey("sciezka"))
                {
                    sSciezka = Convert.ToString(oApplication.aUstawienia.aUstawienia["sciezka"]);
                }
                else
                {
                    sSciezka = Application.StartupPath;
                }

                this.getListServers();

                _iAllCnt = aListSerwerow.Count;
                int iCnt;
                progressBar1.Maximum = _iAllCnt;

                for (iCnt = 0; iCnt < _iAllCnt; iCnt++)
                {
                    if (aListSerwerow[iCnt]["type"] == "mysql")
                    {
                        pq.Enqueue(new MySqlBackup(
                            aListSerwerow[iCnt]["host"],
                            aListSerwerow[iCnt]["baza"],
                            aListSerwerow[iCnt]["haslo"],
                            aListSerwerow[iCnt]["login"],
                            sSciezka), Convert.ToInt32(aListSerwerow[iCnt]["priorytet"]));
                    }
                }

                progressBar1.Value = 0;
                iCnt = 1;
                while (pq.Count > 0)
                {
                    PriorityQueueItem<Backup, int> item = pq.Dequeue();

                    sStripStatus.Text = "Tworzenie kopi bazy: " + item.Value.sBazaDanych;

                    int iStep = (int)(iCnt / _iAllCnt);

                    string sTxt;
                    if (sStripStatus.Text.Length > 60)
                        sTxt = sStripStatus.Text.Substring(1, 60);
                    else
                        sTxt = sStripStatus.Text;

                    notifyIcon.Text = sTxt + "...";
                    notifyIcon.ShowBalloonTip(_iTimeShowBaloon, "Tworznie kopi bazy danych", sTxt, ToolTipIcon.Info);

                    item.Value.makeBackup();

                    if (!item.Value.sBaseState)
                    {
                        notifyIcon.Text = "Błąd tworzenia kopi bazy: " + item.Value.sBazaDanych + "...";
                        notifyIcon.ShowBalloonTip(_iTimeShowBaloon, "Tworznie kopi bazy danych", notifyIcon.Text, ToolTipIcon.Error);
                        System.Threading.Thread.Sleep(1000);
                    }
                }

                pq.Clear();
            }
            catch (Win32Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Win32 Error!!!");
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "IO Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace, "Critical Error");
            }
        }
        
        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            rozpocznijBackup();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btDodajSerwer.Enabled = true;
            btTworz.Enabled = true;

            progressBar1.Value = _iAllCnt;
            sStripStatus.Text = "Kopie baz danych zostały wykonane";
            notifyIcon.Text = sStripStatus.Text;
            notifyIcon.ShowBalloonTip(_iTimeShowBaloon, "Tworznie kopi bazy danych", notifyIcon.Text, ToolTipIcon.Info);

            this.Show();
        }

        private void tlOprogramie_Click(object sender, EventArgs e)
        {

        }

        private void licencjaMenu_Click(object sender, EventArgs e)
        {
            licencja oForm = new licencja();
            oForm.ShowDialog();
        }

        private void oAutorze_Click(object sender, EventArgs e)
        {

        }

        private void ustawieniaMenu_Click(object sender, EventArgs e)
        {
            ustawienia oUstawienia = new ustawienia();
            oUstawienia.ShowDialog();
        }
    }
}
