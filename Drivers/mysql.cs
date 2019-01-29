using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using database_backup_manager.Interfaces;

namespace database_backup_manager.Drivers
{
    class mysql : IConnect
    {
        private string _sSqlQuery;
        public string sSqlQuery
        {
            get
            {
                return _sSqlQuery;
            }
        }
        private string _sConnectionString;
        private MySqlConnection _oConn;

        public MySqlConnection oConn
        {
            get { return this._oConn; }
        }

        private string _sBackupFileName;
        public string sBackupFileName {
            get
            {
                return _sBackupFileName;
            }
            set
            {
                _sBackupFileName = value;
            }
        }

        /// <summary>
        /// ustawienie zmiennej polaczenia oraz zainijowanie obiektu
        /// </summary>
        public string sConnectionString
        {
            get { return _sConnectionString; }
            set
            {
                /// jesl string jest rozny od tego ktory teraz jest ustwiony
                if (_sConnectionString != value)
                {
                    /// przypisz do zmiennej nowe checkConnectionStateenie
                    _sConnectionString = value;
                    /// i zainicjuj obiekt
                    DBConnectRun();
                }
            }
        }

        /// <summary>
        /// konstruktor prywatny zgodnie z wzorcem singleton
        /// </summary>
        private void DBConnectRun()
        {
            _oConn = new MySqlConnection(_sConnectionString);
        }

        /// <summary>
        /// otwarcie polaczenia z baza danych
        /// </summary>
        public void Open()
        {
            if (sConnectionString != null)
            {
                if (_oConn.State != System.Data.ConnectionState.Open)
                {
                    try
                    {
                        _oConn.Open();
                    }
                    catch (MySqlException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        /// <summary>
        /// zamkniecie polaczenia z baza danych
        /// </summary>
        public void Close()
        {
            if (sConnectionString != null)
            {
                _oConn.Close();
            }
        }

        /// <summary>
        /// sprawdzenie stanu polaczenia
        /// </summary>
        public System.Data.ConnectionState State
        {
            get
            {
                return _oConn.State;
            }
        }
        /// <summary>
        /// sprawdzenie stany checkConnectionStatenia z baza danych
        /// </summary>
        /// <returns>jesli checkConnectionStatenie jest zamkniete lub zepsute zwracany jest false w innym przypadku true</returns>
        private bool checkConnectionState()
        {
            if (_oConn != null)
            {
                if (_oConn.State == System.Data.ConnectionState.Closed || _oConn.State == System.Data.ConnectionState.Broken)
                    return false;

                return true;
            }

            return false;
        }
        /// <summary>
        /// jak niszczymy obiekt to rozlaczamy baza danych
        /// </summary>
        public void Dispose()
        {
            if (_oConn != null)
            {
                _oConn.Close();
                _oConn = null;
            }
        }

        /// <summary>
        /// tworzenie pliku kopi bazy danych
        /// </summary>
        public void createBackup()
        {
            if (File.Exists(_sBackupFileName))
            {
                FileInfo oFileInfo = new FileInfo(_sBackupFileName);
                oFileInfo.Delete();
            }

            string sNazwaBazy = _oConn.Database;
            /// pobranie listy tabel
            _sSqlQuery = "SHOW TABLE STATUS FROM " + sNazwaBazy + " WHERE ENGINE IS NOT NULL";
            if (checkConnectionState())
            {
                MySqlCommand oCmd = new MySqlCommand(_sSqlQuery, _oConn);
                MySqlDataReader oReader = oCmd.ExecuteReader();

                //StringBuilder sSchema = new StringBuilder();
                List<SortedList<string, object[]>> aListaTabel = new List<SortedList<string, object[]>>();
                if (oReader.HasRows)
                {
                    while (oReader.Read())
                    {
                        SortedList<string, object[]> aTabela = new SortedList<string, object[]>();

                        object[] aTableInfo = new object[] {
                            oReader["Engine"], oReader["Collation"], oReader["Auto_increment"]
                        };

                        aTabela.Add(oReader["Name"].ToString(), aTableInfo);
                        aListaTabel.Add(aTabela);
                    }
                    oReader.Close();
                }

                foreach (SortedList<string, object[]> aInformacje in aListaTabel)
                {
                    foreach (KeyValuePair<string, object[]> aTabela in aInformacje)
                    {
                        string[] aSklejaniePol = new string[2];

                        zapisanieDoPliku("#", false);
                        zapisanieDoPliku(string.Format("# Struktura tabeli {0}", aTabela.Key), false);
                        zapisanieDoPliku("#", false);
                        zapisanieDoPliku(string.Format("DROP TABLE IF EXISTS `{0}`;", aTabela.Key), false);
                        zapisanieDoPliku(string.Format("CREATE TABLE IF NOT EXISTS {0} (", aTabela.Key), false);

                        _sSqlQuery = string.Format("SHOW KEYS FROM `{0}`", aTabela.Key);
                        oCmd = new MySqlCommand(_sSqlQuery, _oConn);
                        oReader = oCmd.ExecuteReader();

                        SortedList<string, List<string>> aInfoOkluczach = new SortedList<string, List<string>>();
                        if (oReader.HasRows)
                        {
                            while (oReader.Read())
                            {
                                List<string> aInfoKolumny = new List<string>();
                                string sKeyName = oReader["Key_name"].ToString();
                                /// jesli klucz nie jest kluczem glowny i jest unikalny
                                if (sKeyName != "PRIMARY" && oReader["Non_unique"].ToString() == "1")
                                {
                                    sKeyName = "UNIQUE " + sKeyName;
                                }
                                else if (sKeyName != "PRIMARY" && oReader["Non_unique"].ToString() == "0")
                                {

                                }

                                aInfoKolumny.Add(oReader["Column_name"].ToString());
                                int iIndex = aInfoOkluczach.IndexOfKey(sKeyName);
                                if (iIndex == -1)
                                {
                                    aInfoOkluczach.Add(sKeyName, aInfoKolumny);
                                }
                                else
                                {
                                    aInfoOkluczach[sKeyName].Clear();
                                    aInfoOkluczach[sKeyName] = aInfoKolumny;
                                }
                            }
                        }

                        oReader.Close();

                        int iFields = aInfoOkluczach.Count;
                        int iIndexCol = 0;
                        StringBuilder sKlucze = new StringBuilder();
                        
                        string[] aKluczeDoTabeli = new string[aInfoOkluczach.Count];
                        foreach (KeyValuePair<string, List<string>> aInfo in aInfoOkluczach)
                        {
                            string[] aColumns = new string[aInfo.Value.Count];
                            for (int j = 0; j < aInfo.Value.Count; j++)
                            {
                                aColumns[j] = aInfo.Value[j];
                            }

                            int iMaxLen = aInfo.Key.Length;

                            string sKolumny = string.Join("`,`", aColumns);
                            if (aInfo.Key == "PRIMARY")
                            {
                                aKluczeDoTabeli[iIndexCol] = string.Format("   PRIMARY KEY (`{0}`)", sKolumny);
                            }
                            else if (aInfo.Key.Substring(0, iMaxLen) == "UNIQUE")
                            {
                                aKluczeDoTabeli[iIndexCol] = string.Format("   UNIQUE KEY {0} (`{1}`)", aInfo.Key.Substring(7), sKolumny);
                            }
                            else
                            {
                                aKluczeDoTabeli[iIndexCol] = string.Format("   KEY {0} (`{1}`)", aInfo.Key, sKolumny);
                            }
                            iIndexCol++;
                        }

                        aSklejaniePol[1] = string.Join(",\n", aKluczeDoTabeli);

                        _sSqlQuery = string.Format("SHOW FIELDS FROM `{0}`", aTabela.Key);
                        oCmd = new MySqlCommand(_sSqlQuery, _oConn);
                        oReader = oCmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            List<string> aLista = new List<string>();
                            while (oReader.Read())
                            {
                                string sNotNull = null;
                                if (oReader["Null"].ToString() == "Yes")
                                    sNotNull = " NOT NULL";

                                string sDefault = null;
                                if (oReader["Default"].ToString() != "")
                                    sDefault = string.Format(" default '{0}'", oReader["Default"]);

                                string sExtra = null;
                                if (oReader["Extra"].ToString() != "")
                                    sExtra = string.Format(" {0}", oReader["Extra"]);

                                aLista.Add(string.Format("   `{0}` {1}{2}{3}{4}",
                                    oReader["Field"], oReader["Type"], sNotNull, sDefault, sExtra));
                            }
                            string[] aPola = new string[aLista.Count];
                            for (int k = 0; k < aLista.Count; k++)
                                aPola[k] = aLista[k];

                            aSklejaniePol[0] = string.Join(",\n", aPola);
                        }

                        oReader.Close();
                        string sDoZapisania = string.Join(",\n", aSklejaniePol);

                        zapisanieDoPliku(sDoZapisania, false);

                        string sAutoIncrement = null;
                        if ((aTabela.Value[2]).ToString() != "")
                            sAutoIncrement = string.Format(" AUTO_INCREMENT={0}", aTabela.Value[2]);

                        string[] sCharset = aTabela.Value[1].ToString().Split(new char[] {'_'});
                        zapisanieDoPliku(string.Format(") ENGINE={0} DEFAULT CHARSET={1} COLLATE={2}{3};\n\n",
                            aTabela.Value[0], sCharset[0], aTabela.Value[1], sAutoIncrement), false);
                        
                        _sSqlQuery = string.Format("SELECT * FROM {0}", aTabela.Key);
                        oCmd = new MySqlCommand(_sSqlQuery, _oConn);
                        oReader = oCmd.ExecuteReader();
                        if (oReader.HasRows)
                        {
                            zapisanieDoPliku("#", false);
                            zapisanieDoPliku(string.Format("# Dane  z tabeli {0}", aTabela.Key), false);
                            zapisanieDoPliku("#", false);

                            List<string> aRows = new List<string>();
                            while (oReader.Read())
                            {
                                string[] aWartosci = new string[oReader.FieldCount];
                                for (int i = 0; i < oReader.FieldCount; i++)
                                {
                                    string sWartosc = null;

                                    switch (oReader[i].GetType().ToString())
                                    {
                                        case "System.DBNull":
                                            sWartosc = "";
                                            break;

                                        case "System.Boolean":
                                            sWartosc = (oReader.GetBoolean(i)) ? "1" : "0";
                                            break;
                                            
                                        case "System.Byte[]":
                                            Byte[] bBlob = new Byte[(oReader.GetBytes(i, 0, null, 0, int.MaxValue))];
                                            if (bBlob != null)
                                            {
                                                oReader.GetBytes(i, 0, bBlob, 0, bBlob.Length);
                                                if (bBlob != null)
                                                {
                                                    sWartosc = System.Text.Encoding.GetEncoding("utf-8").GetString(bBlob);
                                                    if (true)
                                                    {
                                                        sWartosc = parseString(sWartosc);
                                                    }
                                                }
                                            } 
                                            break;

                                        case "MySql.Data.Types.MySqlDateTime":
                                            MySqlDateTime oDateTime = oReader.GetMySqlDateTime(i);
                                            sWartosc = oDateTime.ToString();
                                            break;

                                        default:
                                            sWartosc = string.Format("{0}", parseString(oReader.GetString(i)));
                                            break;
                                    }

                                    aWartosci[i] = string.Format("'{0}'", sWartosc);
                                }
                                zapisanieDoPliku(string.Format("INSERT INTO {0} VALUES ({1});", aTabela.Key, string.Join(",", aWartosci)), false);
                            }
                        }
                        else
                        {
                            zapisanieDoPliku("#", false);
                            zapisanieDoPliku(string.Format("# Brak danych w tabeli {0}", aTabela.Key), false);
                            zapisanieDoPliku("#", false);
                        }
                        oReader.Close();
                        zapisanieDoPliku("", false);
                    }
                }
            }
        }

        /// <summary>
        /// zmiana danych byte[] na zapis blob-o podobny
        /// </summary>
        /// <param name="sBinData">dane do zmiany</param>
        /// <returns>przerobione na blob-a</returns>
        private object bin2hex(object sBinData)
        {
            return sBinData;
        }

        /// <summary>
        /// zapisanie danych do pliku
        /// </summary>
        /// <param name="sDoZapisania">dane, ktore nalezy zapisac do pliku</param>
        /// <param name="closeStream">czy strumien ma byc zamkniety</param>
        private void zapisanieDoPliku(string sDoZapisania, bool closeStream)
        {
            StreamWriter oSWFile = new StreamWriter(_sBackupFileName, true);
            oSWFile.WriteLine(sDoZapisania);
            oSWFile.Close();
        }

        /// <summary>
        /// dodanie \ do apostrofu
        /// </summary>
        /// <param name="sToParse">string do poprawienia</param>
        /// <returns>poprawiony string</returns>
        private string parseString(string sToParse)
        {
            return Regex.Replace(sToParse, @"\'", @"\'");
        }
    }


}