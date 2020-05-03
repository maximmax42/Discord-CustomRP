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

            if (disposing && (client != null)) client.Dispose();

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
            this.toolStripSeparatorTray1 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile1 = new System.Windows.Forms.ToolStripSeparator();
            this.loadPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTheManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp1 = new System.Windows.Forms.ToolStripSeparator();
            this.translatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ypsolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximmax42ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.radioButtonNone = new System.Windows.Forms.RadioButton();
            this.radioButtonStartTime = new System.Windows.Forms.RadioButton();
            this.radioButtonLocalTime = new System.Windows.Forms.RadioButton();
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.panelTimestamps = new System.Windows.Forms.Panel();
            this.textBoxSmallKey = new System.Windows.Forms.TextBox();
            this.textBoxSmallText = new System.Windows.Forms.TextBox();
            this.textBoxLargeText = new System.Windows.Forms.TextBox();
            this.textBoxLargeKey = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.trayMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelTimestamps.SuspendLayout();
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
            this.toolStripSeparatorTray1,
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
            // toolStripSeparatorTray1
            // 
            this.toolStripSeparatorTray1.Name = "toolStripSeparatorTray1";
            resources.ApplyResources(this.toolStripSeparatorTray1, "toolStripSeparatorTray1");
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
            this.helpToolStripMenuItem,
            this.downloadUpdateToolStripMenuItem});
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Name = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadPresetToolStripMenuItem,
            this.savePresetToolStripMenuItem,
            this.toolStripSeparatorFile1,
            this.uploadAssetsToolStripMenuItem,
            this.toolStripSeparatorFile2,
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
            // toolStripSeparatorFile1
            // 
            this.toolStripSeparatorFile1.Name = "toolStripSeparatorFile1";
            resources.ApplyResources(this.toolStripSeparatorFile1, "toolStripSeparatorFile1");
            // 
            // loadPresetToolStripMenuItem
            // 
            this.loadPresetToolStripMenuItem.Name = "loadPresetToolStripMenuItem";
            resources.ApplyResources(this.loadPresetToolStripMenuItem, "loadPresetToolStripMenuItem");
            this.loadPresetToolStripMenuItem.Click += new System.EventHandler(this.LoadPreset);
            // 
            // savePresetToolStripMenuItem
            // 
            this.savePresetToolStripMenuItem.Name = "savePresetToolStripMenuItem";
            resources.ApplyResources(this.savePresetToolStripMenuItem, "savePresetToolStripMenuItem");
            this.savePresetToolStripMenuItem.Click += new System.EventHandler(this.SavePreset);
            // 
            // toolStripSeparatorFile2
            // 
            this.toolStripSeparatorFile2.Name = "toolStripSeparatorFile2";
            resources.ApplyResources(this.toolStripSeparatorFile2, "toolStripSeparatorFile2");
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
            this.startMinimizedToolStripMenuItem,
            this.toolStripSeparatorSettings1,
            this.checkUpdatesToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // runOnStartupToolStripMenuItem
            // 
            this.runOnStartupToolStripMenuItem.Checked = global::CustomRPC.Properties.Settings.Default.runOnStartup;
            this.runOnStartupToolStripMenuItem.CheckOnClick = true;
            this.runOnStartupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            resources.ApplyResources(this.runOnStartupToolStripMenuItem, "runOnStartupToolStripMenuItem");
            this.runOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.Checked = global::CustomRPC.Properties.Settings.Default.startMinimized;
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            resources.ApplyResources(this.startMinimizedToolStripMenuItem, "startMinimizedToolStripMenuItem");
            this.startMinimizedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // toolStripSeparatorSettings1
            // 
            this.toolStripSeparatorSettings1.Name = "toolStripSeparatorSettings1";
            resources.ApplyResources(this.toolStripSeparatorSettings1, "toolStripSeparatorSettings1");
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Checked = global::CustomRPC.Properties.Settings.Default.checkUpdates;
            this.checkUpdatesToolStripMenuItem.CheckOnClick = true;
            this.checkUpdatesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkUpdatesToolStripMenuItem, "checkUpdatesToolStripMenuItem");
            this.checkUpdatesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTheManualToolStripMenuItem,
            this.gitHubPageToolStripMenuItem,
            this.toolStripSeparatorHelp1,
            this.translatorsToolStripMenuItem,
            this.toolStripSeparatorHelp2,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // openTheManualToolStripMenuItem
            // 
            this.openTheManualToolStripMenuItem.Name = "openTheManualToolStripMenuItem";
            resources.ApplyResources(this.openTheManualToolStripMenuItem, "openTheManualToolStripMenuItem");
            this.openTheManualToolStripMenuItem.Click += new System.EventHandler(this.OpenManual);
            // 
            // gitHubPageToolStripMenuItem
            // 
            this.gitHubPageToolStripMenuItem.Name = "gitHubPageToolStripMenuItem";
            resources.ApplyResources(this.gitHubPageToolStripMenuItem, "gitHubPageToolStripMenuItem");
            this.gitHubPageToolStripMenuItem.Click += new System.EventHandler(this.OpenGitHub);
            // 
            // toolStripSeparatorHelp1
            // 
            this.toolStripSeparatorHelp1.Name = "toolStripSeparatorHelp1";
            resources.ApplyResources(this.toolStripSeparatorHelp1, "toolStripSeparatorHelp1");
            // 
            // translatorsToolStripMenuItem
            // 
            this.translatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.germanToolStripMenuItem,
            this.ypsolToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.maximmax42ToolStripMenuItem});
            this.translatorsToolStripMenuItem.Name = "translatorsToolStripMenuItem";
            resources.ApplyResources(this.translatorsToolStripMenuItem, "translatorsToolStripMenuItem");
            // 
            // germanToolStripMenuItem
            // 
            resources.ApplyResources(this.germanToolStripMenuItem, "germanToolStripMenuItem");
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            // 
            // ypsolToolStripMenuItem
            // 
            this.ypsolToolStripMenuItem.Name = "ypsolToolStripMenuItem";
            resources.ApplyResources(this.ypsolToolStripMenuItem, "ypsolToolStripMenuItem");
            this.ypsolToolStripMenuItem.Tag = "https://www.youtube.com/channel/UCxGqMDnXnEyVt4yugLeBpgA";
            this.ypsolToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // russianToolStripMenuItem
            // 
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            // 
            // maximmax42ToolStripMenuItem
            // 
            this.maximmax42ToolStripMenuItem.Name = "maximmax42ToolStripMenuItem";
            resources.ApplyResources(this.maximmax42ToolStripMenuItem, "maximmax42ToolStripMenuItem");
            this.maximmax42ToolStripMenuItem.Tag = "https://www.maximmax42.ru";
            this.maximmax42ToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // toolStripSeparatorHelp2
            // 
            this.toolStripSeparatorHelp2.Name = "toolStripSeparatorHelp2";
            resources.ApplyResources(this.toolStripSeparatorHelp2, "toolStripSeparatorHelp2");
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.logo;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ShowAbout);
            // 
            // downloadUpdateToolStripMenuItem
            // 
            this.downloadUpdateToolStripMenuItem.ForeColor = System.Drawing.Color.Red;
            this.downloadUpdateToolStripMenuItem.Name = "downloadUpdateToolStripMenuItem";
            resources.ApplyResources(this.downloadUpdateToolStripMenuItem, "downloadUpdateToolStripMenuItem");
            this.downloadUpdateToolStripMenuItem.Click += new System.EventHandler(this.DownloadUpdate);
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
            this.toolTipInfo.ToolTipTitle = "Information";
            // 
            // radioButtonNone
            // 
            resources.ApplyResources(this.radioButtonNone, "radioButtonNone");
            this.radioButtonNone.Name = "radioButtonNone";
            this.radioButtonNone.TabStop = true;
            this.radioButtonNone.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonNone, resources.GetString("radioButtonNone.ToolTip"));
            this.radioButtonNone.UseVisualStyleBackColor = true;
            this.radioButtonNone.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // radioButtonStartTime
            // 
            resources.ApplyResources(this.radioButtonStartTime, "radioButtonStartTime");
            this.radioButtonStartTime.Name = "radioButtonStartTime";
            this.radioButtonStartTime.TabStop = true;
            this.radioButtonStartTime.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonStartTime, resources.GetString("radioButtonStartTime.ToolTip"));
            this.radioButtonStartTime.UseVisualStyleBackColor = true;
            this.radioButtonStartTime.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // radioButtonLocalTime
            // 
            resources.ApplyResources(this.radioButtonLocalTime, "radioButtonLocalTime");
            this.radioButtonLocalTime.Name = "radioButtonLocalTime";
            this.radioButtonLocalTime.TabStop = true;
            this.radioButtonLocalTime.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonLocalTime, resources.GetString("radioButtonLocalTime.ToolTip"));
            this.radioButtonLocalTime.UseVisualStyleBackColor = true;
            this.radioButtonLocalTime.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // labelTimestamp
            // 
            resources.ApplyResources(this.labelTimestamp, "labelTimestamp");
            this.labelTimestamp.Name = "labelTimestamp";
            this.toolTipInfo.SetToolTip(this.labelTimestamp, resources.GetString("labelTimestamp.ToolTip"));
            // 
            // panelTimestamps
            // 
            this.panelTimestamps.Controls.Add(this.radioButtonLocalTime);
            this.panelTimestamps.Controls.Add(this.radioButtonNone);
            this.panelTimestamps.Controls.Add(this.radioButtonStartTime);
            resources.ApplyResources(this.panelTimestamps, "panelTimestamps");
            this.panelTimestamps.Name = "panelTimestamps";
            // 
            // textBoxSmallKey
            // 
            this.textBoxSmallKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxSmallKey, "textBoxSmallKey");
            this.textBoxSmallKey.Name = "textBoxSmallKey";
            this.textBoxSmallKey.Text = global::CustomRPC.Properties.Settings.Default.smallKey;
            // 
            // textBoxSmallText
            // 
            this.textBoxSmallText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxSmallText, "textBoxSmallText");
            this.textBoxSmallText.Name = "textBoxSmallText";
            this.textBoxSmallText.Text = global::CustomRPC.Properties.Settings.Default.smallText;
            // 
            // textBoxLargeText
            // 
            this.textBoxLargeText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxLargeText, "textBoxLargeText");
            this.textBoxLargeText.Name = "textBoxLargeText";
            this.textBoxLargeText.Text = global::CustomRPC.Properties.Settings.Default.largeText;
            // 
            // textBoxLargeKey
            // 
            this.textBoxLargeKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxLargeKey, "textBoxLargeKey");
            this.textBoxLargeKey.Name = "textBoxLargeKey";
            this.textBoxLargeKey.Text = global::CustomRPC.Properties.Settings.Default.largeKey;
            // 
            // textBoxState
            // 
            this.textBoxState.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "state", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxState, "textBoxState");
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Text = global::CustomRPC.Properties.Settings.Default.state;
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "details", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.Text = global::CustomRPC.Properties.Settings.Default.details;
            // 
            // textBoxID
            // 
            this.textBoxID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "id", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxID, "textBoxID");
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Text = global::CustomRPC.Properties.Settings.Default.id;
            this.textBoxID.TextChanged += new System.EventHandler(this.OnlyNumbers);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.labelTimestamp);
            this.Controls.Add(this.panelTimestamps);
            this.Controls.Add(this.textBoxSmallKey);
            this.Controls.Add(this.textBoxSmallText);
            this.Controls.Add(this.labelSmallKey);
            this.Controls.Add(this.labelSmallText);
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
            this.panelTimestamps.ResumeLayout(false);
            this.panelTimestamps.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorTray1;
        private System.Windows.Forms.ToolTip toolTipInfo;
        private System.Windows.Forms.ToolStripMenuItem uploadAssetsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFile1;
        private System.Windows.Forms.RadioButton radioButtonLocalTime;
        private System.Windows.Forms.RadioButton radioButtonStartTime;
        private System.Windows.Forms.RadioButton radioButtonNone;
        private System.Windows.Forms.Panel panelTimestamps;
        private System.Windows.Forms.Label labelTimestamp;
        private System.Windows.Forms.ToolStripMenuItem downloadUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gitHubPageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHelp1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openTheManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSettings1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorHelp2;
        private System.Windows.Forms.ToolStripMenuItem translatorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ypsolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maximmax42ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFile2;
    }
}

