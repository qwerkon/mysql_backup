using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace database_backup_manager.Interfaces
{
    public interface Backup
    {
        void makeBackup();
        bool sBaseState { get; set; }
        string sBazaDanych { get; set; }
    }
}
