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

namespace database_backup_manager
{
    public partial class dodaj_serwer : Form
    {
        string sDirectory = Directory.GetCurrentDirectory();
        private string sXmlFileName;
        private int _iRow;

        public zapiszNaLiscie zapiszNaLiscie;
        public aktualizacjaNaLiscie aktualizacjaNaLiscie;
        public usunZlisty usunZListy;

        private DataGridViewRow _aDataRow;

        public dodaj_serwer()
        {
            InitializeComponent();
            sXmlFileName = sDirectory + @"\servers.xml";
            trybFormularza.Text = "dodanie";
            _btAdresSerwera.Text = "";
            _btLogin.Text = "";
            _btHaslo.Text = "";
            _btNazwaBazyDanych.Text = "";
            _iNumPriorytet.Value = 3;

            btUsun.Enabled = false;
        }

        public dodaj_serwer(int iRow, DataGridViewRow aDataRow)
        {
            InitializeComponent();
            sXmlFileName = sDirectory + @"\servers.xml";

            _iRow = iRow;
            _aDataRow = aDataRow;
            _btAdresSerwera.Text = Convert.ToString(aDataRow.Cells[2].Value);
            _btLogin.Text = Convert.ToString(aDataRow.Cells[1].Value);
            _btHaslo.Text = Convert.ToString(aDataRow.Cells[3].Value);
            _btNazwaBazyDanych.Text = Convert.ToString(aDataRow.Cells[0].Value);
            _iNumPriorytet.Value = Convert.ToInt32(aDataRow.Cells[4].Value);

            this.Text = "Edycja serwera";

            trybFormularza.Text = "edycja";
        }

        private void btZapisz_Click(object sender, EventArgs e)
        {
            if (_btAdresSerwera.Text != "" && _btLogin.Text != "" && _btHaslo.Text != "" && _btNazwaBazyDanych.Text != "")
            {
                object[] aDoZapisania = new object[] { _btNazwaBazyDanych.Text, _btLogin.Text, _btAdresSerwera.Text, _btHaslo.Text };

                XmlTextReader oTextReader = new XmlTextReader(sXmlFileName);
                XmlDocument oDoc = new XmlDocument();
                oDoc.Load(oTextReader);
                oTextReader.Close();

                    XmlDocument oDocNew = new XmlDocument();

                    XmlElement server = oDocNew.CreateElement("server");
                    XmlElement xml_login = oDocNew.CreateElement("login");
                    XmlElement xml_haslo = oDocNew.CreateElement("haslo");
                    XmlElement xml_baza = oDocNew.CreateElement("baza");
                    XmlElement xml_host = oDocNew.CreateElement("host");
                    XmlElement xml_type = oDocNew.CreateElement("type");
                    XmlElement xml_prio = oDocNew.CreateElement("priorytet");

                    XmlText host = oDocNew.CreateTextNode(_btAdresSerwera.Text.Trim());
                    XmlText login = oDocNew.CreateTextNode(_btLogin.Text.Trim());
                    XmlText haslo = oDocNew.CreateTextNode(_btHaslo.Text.Trim());
                    XmlText baza = oDocNew.CreateTextNode(_btNazwaBazyDanych.Text.Trim());
                    XmlText prio = oDocNew.CreateTextNode(Convert.ToString(_iNumPriorytet.Value));
                    XmlText type = oDocNew.CreateTextNode("mysql");

                    xml_login.AppendChild(login);
                    xml_haslo.AppendChild(haslo);
                    xml_baza.AppendChild(baza);
                    xml_host.AppendChild(host);
                    xml_type.AppendChild(type);
                    xml_prio.AppendChild(prio);

                    server.AppendChild(xml_login);
                    server.AppendChild(xml_haslo);
                    server.AppendChild(xml_baza);
                    server.AppendChild(xml_host);
                    server.AppendChild(xml_type);
                    server.AppendChild(xml_prio);

                if (trybFormularza.Text == "edycja")
                {
                    XmlNode oNode = oDoc.DocumentElement.ChildNodes.Item(_iRow);
                    oNode["host"].InnerText = _btAdresSerwera.Text.Trim();
                    oNode["login"].InnerText = _btLogin.Text.Trim();
                    oNode["haslo"].InnerText = _btHaslo.Text.Trim();
                    oNode["baza"].InnerText = _btNazwaBazyDanych.Text.Trim();
                    oNode["priorytet"].InnerText = Convert.ToString(_iNumPriorytet.Value);
                    oNode["type"].InnerText = "mysql";

                    aktualizacjaNaLiscie(_iRow, aDoZapisania);
                }
                else
                {
                    oDoc.DocumentElement.InsertAfter(oDoc.ImportNode(server, true), oDoc.DocumentElement.LastChild);
                    zapiszNaLiscie(aDoZapisania);
                }

                FileStream fNewXml = new FileStream(sXmlFileName, FileMode.Truncate, FileAccess.Write, FileShare.ReadWrite);
                oDoc.Save(fNewXml);
                fNewXml.Close();

                this.Close();
            }
            else
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola", "Puste pola", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btAnuluj_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void findInXml(int iIndex)
        {
            XmlDocument oDoc = new XmlDocument();
            oDoc.LoadXml(sXmlFileName);
            XmlNodeList oNodeList = oDoc.DocumentElement.ChildNodes;
            XmlNode oNode = oNodeList.Item(iIndex);
            string test = oNode["host"].InnerText;
        }

        private void btUsun_Click(object sender, EventArgs e)
        {
            usunZListy(_iRow);
            Close();
        }
    }
}
