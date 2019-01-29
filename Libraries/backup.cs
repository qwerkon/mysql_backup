using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Net;
using MySql.Data.MySqlClient;
using database_backup_manager.Drivers;
using database_backup_manager.Interfaces;

namespace database_backup_manager
{
    class MySqlBackup : Backup
    {
        private string _sSciezkaDoKataloguZkopiami = System.Windows.Forms.Application.StartupPath + @"\backups\";

        private string _sBazaDanych;
        public string sBazaDanych
        {
            get
            {
                return _sBazaDanych;
            }

            set
            {
                _sBazaDanych = value;
            }
        }

        private bool _sBaseState;
        public bool sBaseState
        {
            get
            {
                return _sBaseState;
            }

            set
            {
                _sBaseState = value;
            }
        }
        private string _sConnectionString;

        public MySqlBackup(string sServer, string sBaza, string sHaslo, string sLogin, string sSciezka)
        {
            /// stworzenie danych do polaczenia z baza danych
            string[] aConnectionString = new string[5];
            aConnectionString[0] = string.Format("server={0}", sServer);
            aConnectionString[1] = string.Format("user id={0}", sLogin);
            aConnectionString[2] = string.Format("password={0}", sHaslo);
            aConnectionString[3] = string.Format("database={0}", sBaza);
            aConnectionString[4] = string.Format("allow zero datetime=yes");
            _sConnectionString = string.Join(";", aConnectionString);

            _sBazaDanych = sBaza;

            if (sSciezka != null)
            {
                _sSciezkaDoKataloguZkopiami = sSciezka;
            }
        }

        public void makeBackup()
        {
            _sBaseState = false;
            MySqlConnection _oConn = new MySqlConnection(_sConnectionString);
            _oConn.Open();
            if (_oConn.State == System.Data.ConnectionState.Open)
            {
                _sBaseState = true;

                DateTime backupTime = DateTime.Now;
                int year = backupTime.Year;
                int month = backupTime.Month;
                int day = backupTime.Day;

                String sBackupFile = backupTime.ToString();
                sBackupFile = _sSciezkaDoKataloguZkopiami + @"\" + year + "-" + month + "-" + day + "_" + sBazaDanych + ".sql";

                IConnect oBackup = new mysql();
                oBackup.sConnectionString = _sConnectionString;
                oBackup.sBackupFileName = sBackupFile;
                oBackup.Open();
                if (oBackup.State == System.Data.ConnectionState.Open)
                {
                    oBackup.createBackup();
                    oBackup.Close();

                    StreamReader oRead = new StreamReader(sBackupFile);
                    FileStream oStream = new FileStream(sBackupFile + @".zip", FileMode.Create, FileAccess.Write);
                    GZipStream oCompression = new GZipStream(oStream, CompressionMode.Compress);
                    StreamWriter oWrite = new StreamWriter(oCompression);
                    oWrite.Write(oRead.ReadToEnd());
                    oWrite.Close();
                    oRead.Close();
                    FileInfo oFile = new FileInfo(sBackupFile);
                    oFile.Delete();
                }
            }
        }
    }
}
