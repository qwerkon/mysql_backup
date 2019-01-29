using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace database_backup_manager.Interfaces
{
    /// <summary>
    /// interfejs dla klasy polaczenia z baza danych
    /// </summary>
    public interface IConnect
    {
        /// <summary>
        /// wygenerowane zapytanie SQL
        /// </summary>
        string sSqlQuery { get; }
        /// <summary>
        /// stan polaczenia z baza danych
        /// </summary>
        ConnectionState State { get; }
        /// <summary>
        /// string z parametrami poloczenia
        /// </summary>
        string sConnectionString { get; set; }
        /// <summary>
        /// 
        /// </summary>
        string sBackupFileName { get; set; }
        /// <summary>
        /// otwarcie polaczenia z baza danych
        /// </summary>
        void Open();
        /// <summary>
        /// zamkniecie polacznia z baza danych
        /// </summary>
        void Close();
        /// <summary>
        /// wykonanie zapytania
        /// </summary>
        /// <param name="sZapytanie"></param>
        /// <param name="sTyp"></param>
        /// <returns></returns>
        //int queryExecute(string sZapytanie, string sTyp);
        void createBackup();
    }
}
