namespace database_backup_manager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btDodajSerwer = new System.Windows.Forms.Button();
            this.btTworz = new System.Windows.Forms.Button();
            this.lista_serwerow = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.dataSet1 = new System.Data.DataSet();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pokażProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zamknijProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bgWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.oProgramieMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.licencjaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.oAutorze = new System.Windows.Forms.ToolStripMenuItem();
            this.ustawieniaMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.bazy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.host = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.haslo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priorytet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.lista_serwerow)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btDodajSerwer
            // 
            resources.ApplyResources(this.btDodajSerwer, "btDodajSerwer");
            this.btDodajSerwer.Name = "btDodajSerwer";
            this.btDodajSerwer.UseVisualStyleBackColor = true;
            this.btDodajSerwer.Click += new System.EventHandler(this.btDodajSerwer_Click);
            // 
            // btTworz
            // 
            resources.ApplyResources(this.btTworz, "btTworz");
            this.btTworz.Name = "btTworz";
            this.btTworz.UseVisualStyleBackColor = true;
            this.btTworz.Click += new System.EventHandler(this.btTworz_Click);
            // 
            // lista_serwerow
            // 
            this.lista_serwerow.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lista_serwerow.AllowUserToAddRows = false;
            this.lista_serwerow.AllowUserToResizeColumns = false;
            this.lista_serwerow.AllowUserToResizeRows = false;
            this.lista_serwerow.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.lista_serwerow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lista_serwerow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.bazy,
            this.login,
            this.host,
            this.haslo,
            this.type,
            this.priorytet});
            resources.ApplyResources(this.lista_serwerow, "lista_serwerow");
            this.lista_serwerow.MultiSelect = false;
            this.lista_serwerow.Name = "lista_serwerow";
            this.lista_serwerow.ReadOnly = true;
            this.lista_serwerow.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.lista_serwerow.RowHeadersVisible = false;
            this.lista_serwerow.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.lista_serwerow.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.lista_serwerow.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.lista_serwerow_CellDoubleClick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sStripStatus});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.SizingGrip = false;
            // 
            // sStripStatus
            // 
            this.sStripStatus.Name = "sStripStatus";
            resources.ApplyResources(this.sStripStatus, "sStripStatus");
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // progressBar1
            // 
            resources.ApplyResources(this.progressBar1, "progressBar1");
            this.progressBar1.Name = "progressBar1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pokażProgramToolStripMenuItem,
            this.zamknijProgramToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // pokażProgramToolStripMenuItem
            // 
            this.pokażProgramToolStripMenuItem.Name = "pokażProgramToolStripMenuItem";
            resources.ApplyResources(this.pokażProgramToolStripMenuItem, "pokażProgramToolStripMenuItem");
            this.pokażProgramToolStripMenuItem.Click += new System.EventHandler(this.pokażProgramToolStripMenuItem_Click);
            // 
            // zamknijProgramToolStripMenuItem
            // 
            this.zamknijProgramToolStripMenuItem.Name = "zamknijProgramToolStripMenuItem";
            resources.ApplyResources(this.zamknijProgramToolStripMenuItem, "zamknijProgramToolStripMenuItem");
            this.zamknijProgramToolStripMenuItem.Click += new System.EventHandler(this.zamknijProgramToolStripMenuItem_Click);
            // 
            // bgWorker
            // 
            this.bgWorker.WorkerReportsProgress = true;
            this.bgWorker.WorkerSupportsCancellation = true;
            this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
            this.bgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgWorker_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ustawieniaMenu,
            this.oProgramieMenu});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // oProgramieMenu
            // 
            this.oProgramieMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.licencjaMenu,
            this.oAutorze});
            this.oProgramieMenu.Name = "oProgramieMenu";
            resources.ApplyResources(this.oProgramieMenu, "oProgramieMenu");
            // 
            // licencjaMenu
            // 
            this.licencjaMenu.Name = "licencjaMenu";
            resources.ApplyResources(this.licencjaMenu, "licencjaMenu");
            this.licencjaMenu.Click += new System.EventHandler(this.licencjaMenu_Click);
            // 
            // oAutorze
            // 
            this.oAutorze.Name = "oAutorze";
            resources.ApplyResources(this.oAutorze, "oAutorze");
            this.oAutorze.Click += new System.EventHandler(this.oAutorze_Click);
            // 
            // ustawieniaMenu
            // 
            this.ustawieniaMenu.Name = "ustawieniaMenu";
            resources.ApplyResources(this.ustawieniaMenu, "ustawieniaMenu");
            this.ustawieniaMenu.Click += new System.EventHandler(this.ustawieniaMenu_Click);
            // 
            // bazy
            // 
            this.bazy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.bazy.FillWeight = 119.797F;
            resources.ApplyResources(this.bazy, "bazy");
            this.bazy.Name = "bazy";
            this.bazy.ReadOnly = true;
            // 
            // login
            // 
            this.login.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.login.FillWeight = 119.797F;
            resources.ApplyResources(this.login, "login");
            this.login.Name = "login";
            this.login.ReadOnly = true;
            // 
            // host
            // 
            this.host.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.host.FillWeight = 119.797F;
            resources.ApplyResources(this.host, "host");
            this.host.Name = "host";
            this.host.ReadOnly = true;
            // 
            // haslo
            // 
            resources.ApplyResources(this.haslo, "haslo");
            this.haslo.Name = "haslo";
            this.haslo.ReadOnly = true;
            // 
            // type
            // 
            resources.ApplyResources(this.type, "type");
            this.type.Name = "type";
            this.type.ReadOnly = true;
            // 
            // priorytet
            // 
            resources.ApplyResources(this.priorytet, "priorytet");
            this.priorytet.Name = "priorytet";
            this.priorytet.ReadOnly = true;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lista_serwerow);
            this.Controls.Add(this.btDodajSerwer);
            this.Controls.Add(this.btTworz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lista_serwerow)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btDodajSerwer;
        private System.Windows.Forms.Button btTworz;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sStripStatus;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem pokażProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zamknijProgramToolStripMenuItem;
        public System.Windows.Forms.DataGridView lista_serwerow;
        private System.ComponentModel.BackgroundWorker bgWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ustawieniaMenu;
        private System.Windows.Forms.ToolStripMenuItem oProgramieMenu;
        private System.Windows.Forms.ToolStripMenuItem licencjaMenu;
        private System.Windows.Forms.ToolStripMenuItem oAutorze;
        private System.Windows.Forms.DataGridViewTextBoxColumn bazy;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn host;
        private System.Windows.Forms.DataGridViewTextBoxColumn haslo;
        private System.Windows.Forms.DataGridViewTextBoxColumn type;
        private System.Windows.Forms.DataGridViewTextBoxColumn priorytet;
    }
}

