namespace USIT2020Stundenpläne
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lbSp = new System.Windows.Forms.ListBox();
            this.lbKurse = new System.Windows.Forms.ListBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.tbAdd = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.logo3dp = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cboxMinuten = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAutoupdate = new System.Windows.Forms.CheckBox();
            this.cbAutostart = new System.Windows.Forms.CheckBox();
            this.cbMinimiert = new System.Windows.Forms.CheckBox();
            this.btnAktualisieren = new System.Windows.Forms.Button();
            this.logodiscord = new System.Windows.Forms.Panel();
            this.cbArchiv = new System.Windows.Forms.CheckBox();
            this.lblClose = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(275, 239);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(99, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Hinzufügen";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(512, 239);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(99, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Entfernen";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.BtnRemove_Click);
            // 
            // lbSp
            // 
            this.lbSp.FormattingEnabled = true;
            this.lbSp.ItemHeight = 15;
            this.lbSp.Location = new System.Drawing.Point(13, 39);
            this.lbSp.Name = "lbSp";
            this.lbSp.Size = new System.Drawing.Size(598, 94);
            this.lbSp.Sorted = true;
            this.lbSp.TabIndex = 2;
            // 
            // lbKurse
            // 
            this.lbKurse.FormattingEnabled = true;
            this.lbKurse.ItemHeight = 15;
            this.lbKurse.Location = new System.Drawing.Point(13, 169);
            this.lbKurse.Name = "lbKurse";
            this.lbKurse.Size = new System.Drawing.Size(598, 64);
            this.lbKurse.TabIndex = 2;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(512, 139);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(99, 23);
            this.btnShow.TabIndex = 1;
            this.btnShow.Text = "Anzeigen";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.BtnShow_Click);
            // 
            // tbAdd
            // 
            this.tbAdd.Location = new System.Drawing.Point(13, 238);
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(256, 23);
            this.tbAdd.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Interval = 1800000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
            this.timer2.Tick += new System.EventHandler(this.Timer2_Tick);
            // 
            // logo3dp
            // 
            this.logo3dp.BackColor = System.Drawing.Color.Transparent;
            this.logo3dp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logo3dp.BackgroundImage")));
            this.logo3dp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logo3dp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logo3dp.Location = new System.Drawing.Point(461, 297);
            this.logo3dp.Name = "logo3dp";
            this.logo3dp.Size = new System.Drawing.Size(52, 44);
            this.logo3dp.TabIndex = 4;
            this.logo3dp.Click += new System.EventHandler(this.Logo3dp_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel2.Location = new System.Drawing.Point(519, 297);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 42);
            this.panel2.TabIndex = 4;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "IBB Stundenpläne";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon1_MouseDoubleClick);
            // 
            // cboxMinuten
            // 
            this.cboxMinuten.FormattingEnabled = true;
            this.cboxMinuten.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "20",
            "30",
            "45"});
            this.cboxMinuten.Location = new System.Drawing.Point(13, 313);
            this.cboxMinuten.Name = "cboxMinuten";
            this.cboxMinuten.Size = new System.Drawing.Size(43, 23);
            this.cboxMinuten.TabIndex = 6;
            this.cboxMinuten.Text = "30";
            this.cboxMinuten.SelectedIndexChanged += new System.EventHandler(this.CboxMinuten_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(62, 316);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Aktualisierungsintervall in Minuten";
            // 
            // cbAutoupdate
            // 
            this.cbAutoupdate.AutoSize = true;
            this.cbAutoupdate.BackColor = System.Drawing.Color.Transparent;
            this.cbAutoupdate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbAutoupdate.Location = new System.Drawing.Point(13, 292);
            this.cbAutoupdate.Name = "cbAutoupdate";
            this.cbAutoupdate.Size = new System.Drawing.Size(179, 19);
            this.cbAutoupdate.TabIndex = 8;
            this.cbAutoupdate.Text = "Automatische Aktualisierung";
            this.cbAutoupdate.UseVisualStyleBackColor = false;
            this.cbAutoupdate.CheckedChanged += new System.EventHandler(this.CbAutoupdate_CheckedChanged);
            // 
            // cbAutostart
            // 
            this.cbAutostart.AutoSize = true;
            this.cbAutostart.BackColor = System.Drawing.Color.Transparent;
            this.cbAutostart.ForeColor = System.Drawing.Color.Transparent;
            this.cbAutostart.Location = new System.Drawing.Point(12, 267);
            this.cbAutostart.Name = "cbAutostart";
            this.cbAutostart.Size = new System.Drawing.Size(75, 19);
            this.cbAutostart.TabIndex = 9;
            this.cbAutostart.Text = "Autostart";
            this.cbAutostart.UseVisualStyleBackColor = false;
            this.cbAutostart.CheckedChanged += new System.EventHandler(this.CbAutostart_CheckedChanged);
            // 
            // cbMinimiert
            // 
            this.cbMinimiert.AutoSize = true;
            this.cbMinimiert.BackColor = System.Drawing.Color.Transparent;
            this.cbMinimiert.ForeColor = System.Drawing.Color.Transparent;
            this.cbMinimiert.Location = new System.Drawing.Point(93, 267);
            this.cbMinimiert.Name = "cbMinimiert";
            this.cbMinimiert.Size = new System.Drawing.Size(118, 19);
            this.cbMinimiert.TabIndex = 10;
            this.cbMinimiert.Text = "Minimiert Starten";
            this.cbMinimiert.UseVisualStyleBackColor = false;
            this.cbMinimiert.CheckedChanged += new System.EventHandler(this.CbMinimiert_CheckedChanged);
            // 
            // btnAktualisieren
            // 
            this.btnAktualisieren.Location = new System.Drawing.Point(13, 139);
            this.btnAktualisieren.Name = "btnAktualisieren";
            this.btnAktualisieren.Size = new System.Drawing.Size(99, 23);
            this.btnAktualisieren.TabIndex = 1;
            this.btnAktualisieren.Text = "Aktualisieren";
            this.btnAktualisieren.UseVisualStyleBackColor = true;
            this.btnAktualisieren.Click += new System.EventHandler(this.BtnAktualisieren_Click);
            // 
            // logodiscord
            // 
            this.logodiscord.BackColor = System.Drawing.Color.Transparent;
            this.logodiscord.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("logodiscord.BackgroundImage")));
            this.logodiscord.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.logodiscord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logodiscord.Location = new System.Drawing.Point(352, 297);
            this.logodiscord.Name = "logodiscord";
            this.logodiscord.Size = new System.Drawing.Size(103, 44);
            this.logodiscord.TabIndex = 4;
            this.logodiscord.Click += new System.EventHandler(this.Logodiscord_Click);
            // 
            // cbArchiv
            // 
            this.cbArchiv.AutoSize = true;
            this.cbArchiv.BackColor = System.Drawing.Color.Transparent;
            this.cbArchiv.ForeColor = System.Drawing.Color.Transparent;
            this.cbArchiv.Location = new System.Drawing.Point(217, 267);
            this.cbArchiv.Name = "cbArchiv";
            this.cbArchiv.Size = new System.Drawing.Size(162, 19);
            this.cbArchiv.TabIndex = 11;
            this.cbArchiv.Text = "Stundenpläne Archivieren";
            this.cbArchiv.UseVisualStyleBackColor = false;
            this.cbArchiv.CheckedChanged += new System.EventHandler(this.cbArchiv_CheckedChanged);
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.BackColor = System.Drawing.Color.Transparent;
            this.lblClose.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblClose.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblClose.Location = new System.Drawing.Point(581, 4);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(30, 32);
            this.lblClose.TabIndex = 12;
            this.lblClose.Text = "X";
            this.lblClose.Click += new System.EventHandler(this.lblClose_Click);
            // 
            // FrmMain
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(623, 342);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.cbArchiv);
            this.Controls.Add(this.logodiscord);
            this.Controls.Add(this.btnAktualisieren);
            this.Controls.Add(this.cbMinimiert);
            this.Controls.Add(this.cbAutostart);
            this.Controls.Add(this.cbAutoupdate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboxMinuten);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.logo3dp);
            this.Controls.Add(this.tbAdd);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.lbKurse);
            this.Controls.Add(this.lbSp);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMain";
            this.Text = "IBB Stundenpläne";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FrmMain_MouseDown);
            this.Resize += new System.EventHandler(this.FrmMain_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbSp;
        private System.Windows.Forms.TextBox tbAdd;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ListBox lbKurse;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel logo3dp;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ComboBox cboxMinuten;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbAutoupdate;
        private System.Windows.Forms.CheckBox cbAutostart;
        private System.Windows.Forms.CheckBox cbMinimiert;
        private System.Windows.Forms.Button btnAktualisieren;
        private System.Windows.Forms.Panel logodiscord;
        private System.Windows.Forms.CheckBox cbArchiv;
        private System.Windows.Forms.Label lblClose;
    }
}

