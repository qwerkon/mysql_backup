using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace database_backup_manager
{
    public partial class licencja : Form
    {
        public licencja()
        {
            InitializeComponent();
        }

        private void btZamknij_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
