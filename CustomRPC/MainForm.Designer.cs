namespace CustomRPC
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayMenuReconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxSmallKey = new System.Windows.Forms.TextBox();
            this.textBoxSmallText = new System.Windows.Forms.TextBox();
            this.textBoxLargeKey = new System.Windows.Forms.TextBox();
            this.textBoxLargeText = new System.Windows.Forms.TextBox();
            this.buttonUpdatePresence = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.labelID = new System.Windows.Forms.Label();
            this.labelDetails = new System.Windows.Forms.Label();
            this.labelState = new System.Windows.Forms.Label();
            this.labelSmallKey = new System.Windows.Forms.Label();
            this.labelSmallText = new System.Windows.Forms.Label();
            this.labelLargeText = new System.Windows.Forms.Label();
            this.labelLargeKey = new System.Windows.Forms.Label();
            this.labelSmall = new System.Windows.Forms.Label();
            this.labelLarge = new System.Windows.Forms.Label();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.trayMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.trayIcon, "trayIcon");
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.trayIcon.BalloonTipClicked += new System.EventHandler(this.MaximizeFromTray);
            this.trayIcon.DoubleClick += new System.EventHandler(this.MaximizeFromTray);
            // 
            // trayMenuStrip
            // 
            this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trayMenuReconnect,
            this.toolStripSeparator1,
            this.trayMenuQuit});
            this.trayMenuStrip.Name = "trayMenuStrip";
            resources.ApplyResources(this.trayMenuStrip, "trayMenuStrip");
            // 
            // trayMenuReconnect
            // 
            this.trayMenuReconnect.Name = "trayMenuReconnect";
            resources.ApplyResources(this.trayMenuReconnect, "trayMenuReconnect");
            this.trayMenuReconnect.Click += new System.EventHandler(this.Connect);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // trayMenuQuit
            // 
            this.trayMenuQuit.Name = "trayMenuQuit";
            resources.ApplyResources(this.trayMenuQuit, "trayMenuQuit");
            this.trayMenuQuit.Click += new System.EventHandler(this.Quit);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.WhiteSmoke;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadAssetsToolStripMenuItem,
            this.toolStripSeparator2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // uploadAssetsToolStripMenuItem
            // 
            this.uploadAssetsToolStripMenuItem.Name = "uploadAssetsToolStripMenuItem";
            resources.ApplyResources(this.uploadAssetsToolStripMenuItem, "uploadAssetsToolStripMenuItem");
            this.uploadAssetsToolStripMenuItem.Click += new System.EventHandler(this.OpenDiscordSite);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            resources.ApplyResources(this.quitToolStripMenuItem, "quitToolStripMenuItem");
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.Quit);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runOnStartupToolStripMenuItem,
            this.startMinimizedToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // runOnStartupToolStripMenuItem
            // 
            this.runOnStartupToolStripMenuItem.CheckOnClick = true;
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            resources.ApplyResources(this.runOnStartupToolStripMenuItem, "runOnStartupToolStripMenuItem");
            this.runOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            resources.ApplyResources(this.startMinimizedToolStripMenuItem, "startMinimizedToolStripMenuItem");
            this.startMinimizedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ShowAbout);
            // 
            // textBoxID
            // 
            resources.ApplyResources(this.textBoxID, "textBoxID");
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.TextChanged += new System.EventHandler(this.OnlyNumbers);
            // 
            // textBoxDetails
            // 
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.Name = "textBoxDetails";
            // 
            // textBoxState
            // 
            resources.ApplyResources(this.textBoxState, "textBoxState");
            this.textBoxState.Name = "textBoxState";
            // 
            // textBoxSmallKey
            // 
            resources.ApplyResources(this.textBoxSmallKey, "textBoxSmallKey");
            this.textBoxSmallKey.Name = "textBoxSmallKey";
            // 
            // textBoxSmallText
            // 
            resources.ApplyResources(this.textBoxSmallText, "textBoxSmallText");
            this.textBoxSmallText.Name = "textBoxSmallText";
            // 
            // textBoxLargeKey
            // 
            resources.ApplyResources(this.textBoxLargeKey, "textBoxLargeKey");
            this.textBoxLargeKey.Name = "textBoxLargeKey";
            // 
            // textBoxLargeText
            // 
            resources.ApplyResources(this.textBoxLargeText, "textBoxLargeText");
            this.textBoxLargeText.Name = "textBoxLargeText";
            // 
            // buttonUpdatePresence
            // 
            resources.ApplyResources(this.buttonUpdatePresence, "buttonUpdatePresence");
            this.buttonUpdatePresence.Name = "buttonUpdatePresence";
            this.toolTipInfo.SetToolTip(this.buttonUpdatePresence, resources.GetString("buttonUpdatePresence.ToolTip"));
            this.buttonUpdatePresence.UseVisualStyleBackColor = true;
            this.buttonUpdatePresence.Click += new System.EventHandler(this.Update);
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.toolTipInfo.SetToolTip(this.buttonConnect, resources.GetString("buttonConnect.ToolTip"));
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.Connect);
            // 
            // buttonDisconnect
            // 
            resources.ApplyResources(this.buttonDisconnect, "buttonDisconnect");
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.toolTipInfo.SetToolTip(this.buttonDisconnect, resources.GetString("buttonDisconnect.ToolTip"));
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.Disconnect);
            // 
            // labelID
            // 
            resources.ApplyResources(this.labelID, "labelID");
            this.labelID.Name = "labelID";
            this.toolTipInfo.SetToolTip(this.labelID, resources.GetString("labelID.ToolTip"));
            // 
            // labelDetails
            // 
            resources.ApplyResources(this.labelDetails, "labelDetails");
            this.labelDetails.Name = "labelDetails";
            this.toolTipInfo.SetToolTip(this.labelDetails, resources.GetString("labelDetails.ToolTip"));
            // 
            // labelState
            // 
            resources.ApplyResources(this.labelState, "labelState");
            this.labelState.Name = "labelState";
            this.toolTipInfo.SetToolTip(this.labelState, resources.GetString("labelState.ToolTip"));
            // 
            // labelSmallKey
            // 
            resources.ApplyResources(this.labelSmallKey, "labelSmallKey");
            this.labelSmallKey.Name = "labelSmallKey";
            this.toolTipInfo.SetToolTip(this.labelSmallKey, resources.GetString("labelSmallKey.ToolTip"));
            // 
            // labelSmallText
            // 
            resources.ApplyResources(this.labelSmallText, "labelSmallText");
            this.labelSmallText.Name = "labelSmallText";
            this.toolTipInfo.SetToolTip(this.labelSmallText, resources.GetString("labelSmallText.ToolTip"));
            // 
            // labelLargeText
            // 
            resources.ApplyResources(this.labelLargeText, "labelLargeText");
            this.labelLargeText.Name = "labelLargeText";
            this.toolTipInfo.SetToolTip(this.labelLargeText, resources.GetString("labelLargeText.ToolTip"));
            // 
            // labelLargeKey
            // 
            resources.ApplyResources(this.labelLargeKey, "labelLargeKey");
            this.labelLargeKey.Name = "labelLargeKey";
            this.toolTipInfo.SetToolTip(this.labelLargeKey, resources.GetString("labelLargeKey.ToolTip"));
            // 
            // labelSmall
            // 
            resources.ApplyResources(this.labelSmall, "labelSmall");
            this.labelSmall.Name = "labelSmall";
            this.toolTipInfo.SetToolTip(this.labelSmall, resources.GetString("labelSmall.ToolTip"));
            // 
            // labelLarge
            // 
            resources.ApplyResources(this.labelLarge, "labelLarge");
            this.labelLarge.Name = "labelLarge";
            this.toolTipInfo.SetToolTip(this.labelLarge, resources.GetString("labelLarge.ToolTip"));
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfo.ToolTipTitle = "Информация";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.textBoxSmallKey);
            this.Controls.Add(this.textBoxSmallText);
            this.Controls.Add(this.labelSmallKey);
            this.Controls.Add(this.labelSmallText);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelLarge);
            this.Controls.Add(this.labelSmall);
            this.Controls.Add(this.labelLargeText);
            this.Controls.Add(this.labelLargeKey);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonUpdatePresence);
            this.Controls.Add(this.textBoxLargeText);
            this.Controls.Add(this.textBoxLargeKey);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MinimizeToTray);
            this.trayMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem trayMenuReconnect;
        private System.Windows.Forms.ToolStripMenuItem trayMenuQuit;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runOnStartupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startMinimizedToolStripMenuItem;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.TextBox textBoxDetails;
        private System.Windows.Forms.TextBox textBoxState;
        private System.Windows.Forms.TextBox textBoxSmallKey;
        private System.Windows.Forms.TextBox textBoxSmallText;
        private System.Windows.Forms.TextBox textBoxLargeKey;
        private System.Windows.Forms.TextBox textBoxLargeText;
        private System.Windows.Forms.Button buttonUpdatePresence;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelDetails;
        private System.Windows.Forms.Label labelState;
        private System.Windows.Forms.Label labelSmallKey;
        private System.Windows.Forms.Label labelSmallText;
        private System.Windows.Forms.Label labelLargeText;
        private System.Windows.Forms.Label labelLargeKey;
        private System.Windows.Forms.Label labelSmall;
        private System.Windows.Forms.Label labelLarge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ToolStripMenuItem uploadAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

