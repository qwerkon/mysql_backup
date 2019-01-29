namespace database_backup_manager
{
    partial class dodaj_serwer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dodaj_serwer));
            this.btZapisz = new System.Windows.Forms.Button();
            this._btAdresSerwera = new System.Windows.Forms.TextBox();
            this._btLogin = new System.Windows.Forms.TextBox();
            this._btHaslo = new System.Windows.Forms.TextBox();
            this._btNazwaBazyDanych = new System.Windows.Forms.TextBox();
            this.serwer = new System.Windows.Forms.Label();
            this.serwer_login = new System.Windows.Forms.Label();
            this.serwer_haslo = new System.Windows.Forms.Label();
            this.serwer_baza = new System.Windows.Forms.Label();
            this.btAnuluj = new System.Windows.Forms.Button();
            this.trybFormularza = new System.Windows.Forms.TextBox();
            this.btUsun = new System.Windows.Forms.Button();
            this._iNumPriorytet = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._iNumPriorytet)).BeginInit();
            this.SuspendLayout();
            // 
            // btZapisz
            // 
            this.btZapisz.Location = new System.Drawing.Point(124, 156);
            this.btZapisz.Name = "btZapisz";
            this.btZapisz.Size = new System.Drawing.Size(75, 23);
            this.btZapisz.TabIndex = 0;
            this.btZapisz.Text = "zapisz";
            this.btZapisz.UseVisualStyleBackColor = true;
            this.btZapisz.Click += new System.EventHandler(this.btZapisz_Click);
            // 
            // btAdresSerwera
            // 
            this._btAdresSerwera.Location = new System.Drawing.Point(75, 13);
            this._btAdresSerwera.Name = "btAdresSerwera";
            this._btAdresSerwera.Size = new System.Drawing.Size(205, 20);
            this._btAdresSerwera.TabIndex = 1;
            // 
            // btLogin
            // 
            this._btLogin.Location = new System.Drawing.Point(75, 40);
            this._btLogin.Name = "btLogin";
            this._btLogin.Size = new System.Drawing.Size(205, 20);
            this._btLogin.TabIndex = 2;
            // 
            // btHaslo
            // 
            this._btHaslo.Location = new System.Drawing.Point(75, 67);
            this._btHaslo.Name = "btHaslo";
            this._btHaslo.Size = new System.Drawing.Size(205, 20);
            this._btHaslo.TabIndex = 3;
            // 
            // btNazwaBazyDanych
            // 
            this._btNazwaBazyDanych.Location = new System.Drawing.Point(75, 94);
            this._btNazwaBazyDanych.Name = "btNazwaBazyDanych";
            this._btNazwaBazyDanych.Size = new System.Drawing.Size(205, 20);
            this._btNazwaBazyDanych.TabIndex = 4;
            // 
            // serwer
            // 
            this.serwer.AutoSize = true;
            this.serwer.Location = new System.Drawing.Point(12, 16);
            this.serwer.Name = "serwer";
            this.serwer.Size = new System.Drawing.Size(38, 13);
            this.serwer.TabIndex = 5;
            this.serwer.Text = "serwer";
            // 
            // serwer_login
            // 
            this.serwer_login.AutoSize = true;
            this.serwer_login.Location = new System.Drawing.Point(12, 43);
            this.serwer_login.Name = "serwer_login";
            this.serwer_login.Size = new System.Drawing.Size(29, 13);
            this.serwer_login.TabIndex = 6;
            this.serwer_login.Text = "login";
            // 
            // serwer_haslo
            // 
            this.serwer_haslo.AutoSize = true;
            this.serwer_haslo.Location = new System.Drawing.Point(12, 70);
            this.serwer_haslo.Name = "serwer_haslo";
            this.serwer_haslo.Size = new System.Drawing.Size(34, 13);
            this.serwer_haslo.TabIndex = 7;
            this.serwer_haslo.Text = "hasło";
            // 
            // serwer_baza
            // 
            this.serwer_baza.AutoSize = true;
            this.serwer_baza.Location = new System.Drawing.Point(12, 97);
            this.serwer_baza.Name = "serwer_baza";
            this.serwer_baza.Size = new System.Drawing.Size(63, 13);
            this.serwer_baza.TabIndex = 8;
            this.serwer_baza.Text = "nazwa bazy";
            // 
            // btAnuluj
            // 
            this.btAnuluj.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btAnuluj.Location = new System.Drawing.Point(43, 156);
            this.btAnuluj.Name = "btAnuluj";
            this.btAnuluj.Size = new System.Drawing.Size(75, 23);
            this.btAnuluj.TabIndex = 9;
            this.btAnuluj.Text = "anuluj";
            this.btAnuluj.UseVisualStyleBackColor = true;
            this.btAnuluj.Click += new System.EventHandler(this.btAnuluj_Click);
            // 
            // trybFormularza
            // 
            this.trybFormularza.Location = new System.Drawing.Point(13, 156);
            this.trybFormularza.Name = "trybFormularza";
            this.trybFormularza.ReadOnly = true;
            this.trybFormularza.Size = new System.Drawing.Size(10, 20);
            this.trybFormularza.TabIndex = 10;
            this.trybFormularza.Visible = false;
            // 
            // btUsun
            // 
            this.btUsun.Location = new System.Drawing.Point(205, 156);
            this.btUsun.Name = "btUsun";
            this.btUsun.Size = new System.Drawing.Size(75, 23);
            this.btUsun.TabIndex = 11;
            this.btUsun.Text = "usuń";
            this.btUsun.UseVisualStyleBackColor = true;
            this.btUsun.Click += new System.EventHandler(this.btUsun_Click);
            // 
            // numPriorytet
            // 
            this._iNumPriorytet.Location = new System.Drawing.Point(75, 121);
            this._iNumPriorytet.Name = "numPriorytet";
            this._iNumPriorytet.Size = new System.Drawing.Size(124, 20);
            this._iNumPriorytet.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "priorytet";
            // 
            // dodaj_serwer
            // 
            this.AcceptButton = this.btZapisz;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btAnuluj;
            this.ClientSize = new System.Drawing.Size(292, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._iNumPriorytet);
            this.Controls.Add(this.btUsun);
            this.Controls.Add(this.trybFormularza);
            this.Controls.Add(this.btAnuluj);
            this.Controls.Add(this.serwer_baza);
            this.Controls.Add(this.serwer_haslo);
            this.Controls.Add(this.serwer_login);
            this.Controls.Add(this.serwer);
            this.Controls.Add(this._btNazwaBazyDanych);
            this.Controls.Add(this._btHaslo);
            this.Controls.Add(this._btLogin);
            this.Controls.Add(this._btAdresSerwera);
            this.Controls.Add(this.btZapisz);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dodaj_serwer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj serwer";
            ((System.ComponentModel.ISupportInitialize)(this._iNumPriorytet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btZapisz;
        private System.Windows.Forms.TextBox _btAdresSerwera;
        private System.Windows.Forms.TextBox _btLogin;
        private System.Windows.Forms.TextBox _btHaslo;
        private System.Windows.Forms.TextBox _btNazwaBazyDanych;
        private System.Windows.Forms.Label serwer;
        private System.Windows.Forms.Label serwer_login;
        private System.Windows.Forms.Label serwer_haslo;
        private System.Windows.Forms.Label serwer_baza;
        private System.Windows.Forms.Button btAnuluj;
        private System.Windows.Forms.TextBox trybFormularza;
        private System.Windows.Forms.Button btUsun;
        private System.Windows.Forms.NumericUpDown _iNumPriorytet;
        private System.Windows.Forms.Label label1;
    }
}