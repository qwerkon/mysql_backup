using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace database_backup_manager
{
    /// <summary>
    /// klasa zarzadajaca ustawieniami aplikacji
    /// </summary>
    public sealed class applicationConfig
    {
        /// <summary>
        /// sciezka do pliku konfiguracyjnego
        /// </summary>
        private string _sPathToConfigFile;

        public string sPathToConfigFile
        {
            get
            {
                return _sPathToConfigFile;
            }

            set
            {
                _sPathToConfigFile = value;
            }
        }

        /// <summary>
        /// odczytanie ustawien
        /// </summary>
        public void ReadBinary()
        {
            if (File.Exists(_sPathToConfigFile))
            {
                using (FileStream fileStream = new FileStream(_sPathToConfigFile, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    aUstawienia = (binaryFormatter.Deserialize(fileStream) as ustawieniaAplikacji);
                }
            }
        }

        /// <summary>
        /// zapisanie ustawien poprzez serializacje obiektu konfiguracji
        /// </summary>
        /// <param name="oConfig">obiekt konfiguracji</param>
        public void Write(ustawieniaAplikacji oConfig)
        {
            using (FileStream fileStream = new FileStream(_sPathToConfigFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(fileStream, oConfig);
            }
        }

        public ustawieniaAplikacji aUstawienia;
    }

    [Serializable]
    [XmlRootAttribute("ustawieniaAplikacji", Namespace = "", IsNullable = false)]
    public class ustawieniaAplikacji
    {
        [XmlArray("ustawieniaAplikacji"), XmlArrayItem("aUstawienia", typeof(string))]
        public Hashtable aUstawienia
        {
            get
            {
                return _aUstawienia;
            }

            set
            {
                _aUstawienia = value;
            }
        }


        private Hashtable _aUstawienia = new Hashtable();

        public ustawieniaAplikacji()
        {
        }
    }

}
