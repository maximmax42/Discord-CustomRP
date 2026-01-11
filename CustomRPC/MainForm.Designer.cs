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
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorTray1 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuReconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile1 = new System.Windows.Forms.ToolStripSeparator();
            this.uploadAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allowAnalyticsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings2 = new System.Windows.Forms.ToolStripSeparator();
            this.darkModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings3 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.sampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTheManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.faqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp1 = new System.Windows.Forms.ToolStripSeparator();
            this.supportersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testSupporterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.translatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sampleLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.samplePersonToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.radioButtonLastConnection = new System.Windows.Forms.RadioButton();
            this.radioButtonStartTime = new System.Windows.Forms.RadioButton();
            this.radioButtonLocalTime = new System.Windows.Forms.RadioButton();
            this.labelTimestamp = new System.Windows.Forms.Label();
            this.labelParty = new System.Windows.Forms.Label();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.labelButton1 = new System.Windows.Forms.Label();
            this.labelButton2 = new System.Windows.Forms.Label();
            this.labelButton1URL = new System.Windows.Forms.Label();
            this.labelButton2URL = new System.Windows.Forms.Label();
            this.labelButton1Text = new System.Windows.Forms.Label();
            this.labelButton2Text = new System.Windows.Forms.Label();
            this.radioButtonPresence = new System.Windows.Forms.RadioButton();
            this.labelType = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonUpdatePresence = new System.Windows.Forms.Button();
            this.labelDetailsURL = new System.Windows.Forms.Label();
            this.labelStateURL = new System.Windows.Forms.Label();
            this.panelTimestamps = new System.Windows.Forms.Panel();
            this.tableLayoutPanelCustomTimestamps = new System.Windows.Forms.TableLayoutPanel();
            this.dateTimePickerTimestampEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimestampStart = new System.Windows.Forms.DateTimePicker();
            this.labelTimestampStart = new System.Windows.Forms.Label();
            this.checkBoxTimestampEnd = new System.Windows.Forms.CheckBox();
            this.labelPartyOf = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelUsername = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.comboBoxType = new System.Windows.Forms.ComboBox();
            this.panelSeparator1 = new System.Windows.Forms.Panel();
            this.panelSeparator2 = new System.Windows.Forms.Panel();
            this.panelSeparator3 = new System.Windows.Forms.Panel();
            this.panelSeparator4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanelButtons = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanelParty = new System.Windows.Forms.FlowLayoutPanel();
            this.numericUpDownPartySize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPartyMax = new System.Windows.Forms.NumericUpDown();
            this.textBoxStateURL = new System.Windows.Forms.TextBox();
            this.textBoxDetailsURL = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxButton2Text = new System.Windows.Forms.TextBox();
            this.textBoxButton2URL = new System.Windows.Forms.TextBox();
            this.textBoxButton1URL = new System.Windows.Forms.TextBox();
            this.textBoxButton1Text = new System.Windows.Forms.TextBox();
            this.comboBoxSmallKey = new System.Windows.Forms.ComboBox();
            this.textBoxSmallText = new System.Windows.Forms.TextBox();
            this.textBoxLargeText = new System.Windows.Forms.TextBox();
            this.comboBoxLargeKey = new System.Windows.Forms.ComboBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.trayMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelTimestamps.SuspendLayout();
            this.tableLayoutPanelCustomTimestamps.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanelButtons.SuspendLayout();
            this.flowLayoutPanelParty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPartySize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPartyMax)).BeginInit();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.trayIcon, "trayIcon");
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.trayIcon.BalloonTipClicked += new System.EventHandler(this.MaximizeFromTray);
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaximizeFromTray);
            // 
            // trayMenuStrip
            // 
            this.trayMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.toolStripSeparatorTray1,
            this.trayMenuReconnect,
            this.trayMenuDisconnect,
            this.toolStripSeparator3,
            this.trayMenuQuit});
            this.trayMenuStrip.Name = "trayMenuStrip";
            resources.ApplyResources(this.trayMenuStrip, "trayMenuStrip");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.MaximizeFromTray);
            // 
            // toolStripSeparatorTray1
            // 
            this.toolStripSeparatorTray1.Name = "toolStripSeparatorTray1";
            resources.ApplyResources(this.toolStripSeparatorTray1, "toolStripSeparatorTray1");
            // 
            // trayMenuReconnect
            // 
            this.trayMenuReconnect.Name = "trayMenuReconnect";
            resources.ApplyResources(this.trayMenuReconnect, "trayMenuReconnect");
            this.trayMenuReconnect.Click += new System.EventHandler(this.Connect);
            // 
            // trayMenuDisconnect
            // 
            resources.ApplyResources(this.trayMenuDisconnect, "trayMenuDisconnect");
            this.trayMenuDisconnect.Name = "trayMenuDisconnect";
            this.trayMenuDisconnect.Click += new System.EventHandler(this.Disconnect);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
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
            this.newPresetToolStripMenuItem,
            this.loadPresetToolStripMenuItem,
            this.savePresetToolStripMenuItem,
            this.toolStripSeparatorFile1,
            this.uploadAssetsToolStripMenuItem,
            this.toolStripSeparatorFile2,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // newPresetToolStripMenuItem
            // 
            this.newPresetToolStripMenuItem.Name = "newPresetToolStripMenuItem";
            resources.ApplyResources(this.newPresetToolStripMenuItem, "newPresetToolStripMenuItem");
            this.newPresetToolStripMenuItem.Click += new System.EventHandler(this.NewPreset);
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
            // toolStripSeparatorFile1
            // 
            this.toolStripSeparatorFile1.Name = "toolStripSeparatorFile1";
            resources.ApplyResources(this.toolStripSeparatorFile1, "toolStripSeparatorFile1");
            // 
            // uploadAssetsToolStripMenuItem
            // 
            this.uploadAssetsToolStripMenuItem.Name = "uploadAssetsToolStripMenuItem";
            resources.ApplyResources(this.uploadAssetsToolStripMenuItem, "uploadAssetsToolStripMenuItem");
            this.uploadAssetsToolStripMenuItem.Click += new System.EventHandler(this.OpenDiscordSite);
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
            this.autoconnectToolStripMenuItem,
            this.toolStripSeparatorSettings1,
            this.checkUpdatesToolStripMenuItem,
            this.allowAnalyticsToolStripMenuItem,
            this.toolStripSeparatorSettings2,
            this.darkModeToolStripMenuItem,
            this.toolStripSeparatorSettings3,
            this.languageToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // runOnStartupToolStripMenuItem
            // 
            this.runOnStartupToolStripMenuItem.Checked = true;
            this.runOnStartupToolStripMenuItem.CheckOnClick = true;
            this.runOnStartupToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            resources.ApplyResources(this.runOnStartupToolStripMenuItem, "runOnStartupToolStripMenuItem");
            this.runOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            resources.ApplyResources(this.startMinimizedToolStripMenuItem, "startMinimizedToolStripMenuItem");
            this.startMinimizedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // autoconnectToolStripMenuItem
            // 
            this.autoconnectToolStripMenuItem.Checked = true;
            this.autoconnectToolStripMenuItem.CheckOnClick = true;
            this.autoconnectToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoconnectToolStripMenuItem.Name = "autoconnectToolStripMenuItem";
            resources.ApplyResources(this.autoconnectToolStripMenuItem, "autoconnectToolStripMenuItem");
            this.autoconnectToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // toolStripSeparatorSettings1
            // 
            this.toolStripSeparatorSettings1.Name = "toolStripSeparatorSettings1";
            resources.ApplyResources(this.toolStripSeparatorSettings1, "toolStripSeparatorSettings1");
            // 
            // checkUpdatesToolStripMenuItem
            // 
            this.checkUpdatesToolStripMenuItem.Checked = true;
            this.checkUpdatesToolStripMenuItem.CheckOnClick = true;
            this.checkUpdatesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkUpdatesToolStripMenuItem.Name = "checkUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkUpdatesToolStripMenuItem, "checkUpdatesToolStripMenuItem");
            this.checkUpdatesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // allowAnalyticsToolStripMenuItem
            // 
            this.allowAnalyticsToolStripMenuItem.Checked = true;
            this.allowAnalyticsToolStripMenuItem.CheckOnClick = true;
            this.allowAnalyticsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allowAnalyticsToolStripMenuItem.Name = "allowAnalyticsToolStripMenuItem";
            resources.ApplyResources(this.allowAnalyticsToolStripMenuItem, "allowAnalyticsToolStripMenuItem");
            this.allowAnalyticsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // toolStripSeparatorSettings2
            // 
            this.toolStripSeparatorSettings2.Name = "toolStripSeparatorSettings2";
            resources.ApplyResources(this.toolStripSeparatorSettings2, "toolStripSeparatorSettings2");
            // 
            // darkModeToolStripMenuItem
            // 
            this.darkModeToolStripMenuItem.CheckOnClick = true;
            this.darkModeToolStripMenuItem.Name = "darkModeToolStripMenuItem";
            resources.ApplyResources(this.darkModeToolStripMenuItem, "darkModeToolStripMenuItem");
            this.darkModeToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveMenuSettings);
            // 
            // toolStripSeparatorSettings3
            // 
            this.toolStripSeparatorSettings3.Name = "toolStripSeparatorSettings3";
            resources.ApplyResources(this.toolStripSeparatorSettings3, "toolStripSeparatorSettings3");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.toolStripSeparator2,
            this.sampleToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.CheckOnClick = true;
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            resources.ApplyResources(this.defaultToolStripMenuItem, "defaultToolStripMenuItem");
            this.defaultToolStripMenuItem.Tag = "auto";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // sampleToolStripMenuItem
            // 
            this.sampleToolStripMenuItem.Name = "sampleToolStripMenuItem";
            resources.ApplyResources(this.sampleToolStripMenuItem, "sampleToolStripMenuItem");
            this.sampleToolStripMenuItem.Tag = "lol";
            this.sampleToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTheManualToolStripMenuItem,
            this.faqToolStripMenuItem,
            this.gitHubPageToolStripMenuItem,
            this.toolStripSeparatorHelp1,
            this.supportersToolStripMenuItem,
            this.translatorsToolStripMenuItem,
            this.toolStripSeparatorHelp2,
            this.checkForUpdatesToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // openTheManualToolStripMenuItem
            // 
            this.openTheManualToolStripMenuItem.Name = "openTheManualToolStripMenuItem";
            resources.ApplyResources(this.openTheManualToolStripMenuItem, "openTheManualToolStripMenuItem");
            this.openTheManualToolStripMenuItem.Tag = "https://docs.customrp.xyz/setting-up";
            this.openTheManualToolStripMenuItem.Click += new System.EventHandler(this.OpenSite);
            // 
            // faqToolStripMenuItem
            // 
            this.faqToolStripMenuItem.Name = "faqToolStripMenuItem";
            resources.ApplyResources(this.faqToolStripMenuItem, "faqToolStripMenuItem");
            this.faqToolStripMenuItem.Tag = "https://docs.customrp.xyz/faq";
            this.faqToolStripMenuItem.Click += new System.EventHandler(this.OpenSite);
            // 
            // gitHubPageToolStripMenuItem
            // 
            this.gitHubPageToolStripMenuItem.Name = "gitHubPageToolStripMenuItem";
            resources.ApplyResources(this.gitHubPageToolStripMenuItem, "gitHubPageToolStripMenuItem");
            this.gitHubPageToolStripMenuItem.Tag = "https://github.com/maximmax42/Discord-CustomRP";
            this.gitHubPageToolStripMenuItem.Click += new System.EventHandler(this.OpenSite);
            // 
            // toolStripSeparatorHelp1
            // 
            this.toolStripSeparatorHelp1.Name = "toolStripSeparatorHelp1";
            resources.ApplyResources(this.toolStripSeparatorHelp1, "toolStripSeparatorHelp1");
            // 
            // supportersToolStripMenuItem
            // 
            this.supportersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testSupporterToolStripMenuItem});
            this.supportersToolStripMenuItem.Name = "supportersToolStripMenuItem";
            resources.ApplyResources(this.supportersToolStripMenuItem, "supportersToolStripMenuItem");
            // 
            // testSupporterToolStripMenuItem
            // 
            this.testSupporterToolStripMenuItem.Name = "testSupporterToolStripMenuItem";
            resources.ApplyResources(this.testSupporterToolStripMenuItem, "testSupporterToolStripMenuItem");
            // 
            // translatorsToolStripMenuItem
            // 
            this.translatorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sampleLanguageToolStripMenuItem});
            this.translatorsToolStripMenuItem.Name = "translatorsToolStripMenuItem";
            resources.ApplyResources(this.translatorsToolStripMenuItem, "translatorsToolStripMenuItem");
            // 
            // sampleLanguageToolStripMenuItem
            // 
            this.sampleLanguageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.samplePersonToolStripMenuItem});
            this.sampleLanguageToolStripMenuItem.Name = "sampleLanguageToolStripMenuItem";
            resources.ApplyResources(this.sampleLanguageToolStripMenuItem, "sampleLanguageToolStripMenuItem");
            // 
            // samplePersonToolStripMenuItem
            // 
            this.samplePersonToolStripMenuItem.Name = "samplePersonToolStripMenuItem";
            resources.ApplyResources(this.samplePersonToolStripMenuItem, "samplePersonToolStripMenuItem");
            // 
            // toolStripSeparatorHelp2
            // 
            this.toolStripSeparatorHelp2.Name = "toolStripSeparatorHelp2";
            resources.ApplyResources(this.toolStripSeparatorHelp2, "toolStripSeparatorHelp2");
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            resources.ApplyResources(this.checkForUpdatesToolStripMenuItem, "checkForUpdatesToolStripMenuItem");
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.CheckForUpdates);
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
            // labelID
            // 
            resources.ApplyResources(this.labelID, "labelID");
            this.labelID.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelID.Name = "labelID";
            this.toolTipInfo.SetToolTip(this.labelID, resources.GetString("labelID.ToolTip"));
            // 
            // labelDetails
            // 
            resources.ApplyResources(this.labelDetails, "labelDetails");
            this.labelDetails.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelDetails.Name = "labelDetails";
            this.toolTipInfo.SetToolTip(this.labelDetails, resources.GetString("labelDetails.ToolTip"));
            // 
            // labelState
            // 
            resources.ApplyResources(this.labelState, "labelState");
            this.labelState.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelState.Name = "labelState";
            this.toolTipInfo.SetToolTip(this.labelState, resources.GetString("labelState.ToolTip"));
            // 
            // labelSmallKey
            // 
            resources.ApplyResources(this.labelSmallKey, "labelSmallKey");
            this.labelSmallKey.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelSmallKey.Name = "labelSmallKey";
            this.toolTipInfo.SetToolTip(this.labelSmallKey, resources.GetString("labelSmallKey.ToolTip"));
            // 
            // labelSmallText
            // 
            resources.ApplyResources(this.labelSmallText, "labelSmallText");
            this.labelSmallText.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelSmallText.Name = "labelSmallText";
            this.toolTipInfo.SetToolTip(this.labelSmallText, resources.GetString("labelSmallText.ToolTip"));
            // 
            // labelLargeText
            // 
            resources.ApplyResources(this.labelLargeText, "labelLargeText");
            this.labelLargeText.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelLargeText.Name = "labelLargeText";
            this.toolTipInfo.SetToolTip(this.labelLargeText, resources.GetString("labelLargeText.ToolTip"));
            // 
            // labelLargeKey
            // 
            resources.ApplyResources(this.labelLargeKey, "labelLargeKey");
            this.labelLargeKey.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelLargeKey.Name = "labelLargeKey";
            this.toolTipInfo.SetToolTip(this.labelLargeKey, resources.GetString("labelLargeKey.ToolTip"));
            // 
            // labelSmall
            // 
            resources.ApplyResources(this.labelSmall, "labelSmall");
            this.labelSmall.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelSmall.Name = "labelSmall";
            this.toolTipInfo.SetToolTip(this.labelSmall, resources.GetString("labelSmall.ToolTip"));
            // 
            // labelLarge
            // 
            resources.ApplyResources(this.labelLarge, "labelLarge");
            this.labelLarge.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelLarge.Name = "labelLarge";
            this.toolTipInfo.SetToolTip(this.labelLarge, resources.GetString("labelLarge.ToolTip"));
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfo.ToolTipTitle = "Information";
            // 
            // radioButtonLastConnection
            // 
            resources.ApplyResources(this.radioButtonLastConnection, "radioButtonLastConnection");
            this.radioButtonLastConnection.Name = "radioButtonLastConnection";
            this.radioButtonLastConnection.TabStop = true;
            this.radioButtonLastConnection.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonLastConnection, resources.GetString("radioButtonLastConnection.ToolTip"));
            this.radioButtonLastConnection.UseVisualStyleBackColor = true;
            this.radioButtonLastConnection.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
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
            this.labelTimestamp.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelTimestamp.Name = "labelTimestamp";
            this.toolTipInfo.SetToolTip(this.labelTimestamp, resources.GetString("labelTimestamp.ToolTip"));
            // 
            // labelParty
            // 
            resources.ApplyResources(this.labelParty, "labelParty");
            this.labelParty.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelParty.Name = "labelParty";
            this.toolTipInfo.SetToolTip(this.labelParty, resources.GetString("labelParty.ToolTip"));
            // 
            // radioButtonCustom
            // 
            resources.ApplyResources(this.radioButtonCustom, "radioButtonCustom");
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonCustom, resources.GetString("radioButtonCustom.ToolTip"));
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // labelButton1
            // 
            resources.ApplyResources(this.labelButton1, "labelButton1");
            this.labelButton1.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton1.Name = "labelButton1";
            this.toolTipInfo.SetToolTip(this.labelButton1, resources.GetString("labelButton1.ToolTip"));
            // 
            // labelButton2
            // 
            resources.ApplyResources(this.labelButton2, "labelButton2");
            this.labelButton2.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton2.Name = "labelButton2";
            this.toolTipInfo.SetToolTip(this.labelButton2, resources.GetString("labelButton2.ToolTip"));
            // 
            // labelButton1URL
            // 
            resources.ApplyResources(this.labelButton1URL, "labelButton1URL");
            this.labelButton1URL.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton1URL.Name = "labelButton1URL";
            this.toolTipInfo.SetToolTip(this.labelButton1URL, resources.GetString("labelButton1URL.ToolTip"));
            // 
            // labelButton2URL
            // 
            resources.ApplyResources(this.labelButton2URL, "labelButton2URL");
            this.labelButton2URL.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton2URL.Name = "labelButton2URL";
            this.toolTipInfo.SetToolTip(this.labelButton2URL, resources.GetString("labelButton2URL.ToolTip"));
            // 
            // labelButton1Text
            // 
            resources.ApplyResources(this.labelButton1Text, "labelButton1Text");
            this.labelButton1Text.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton1Text.Name = "labelButton1Text";
            this.toolTipInfo.SetToolTip(this.labelButton1Text, resources.GetString("labelButton1Text.ToolTip"));
            // 
            // labelButton2Text
            // 
            resources.ApplyResources(this.labelButton2Text, "labelButton2Text");
            this.labelButton2Text.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelButton2Text.Name = "labelButton2Text";
            this.toolTipInfo.SetToolTip(this.labelButton2Text, resources.GetString("labelButton2Text.ToolTip"));
            // 
            // radioButtonPresence
            // 
            resources.ApplyResources(this.radioButtonPresence, "radioButtonPresence");
            this.radioButtonPresence.Name = "radioButtonPresence";
            this.radioButtonPresence.TabStop = true;
            this.radioButtonPresence.Tag = "";
            this.toolTipInfo.SetToolTip(this.radioButtonPresence, resources.GetString("radioButtonPresence.ToolTip"));
            this.radioButtonPresence.UseVisualStyleBackColor = true;
            this.radioButtonPresence.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // labelType
            // 
            resources.ApplyResources(this.labelType, "labelType");
            this.labelType.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelType.Name = "labelType";
            this.toolTipInfo.SetToolTip(this.labelType, resources.GetString("labelType.ToolTip"));
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelName.Name = "labelName";
            this.toolTipInfo.SetToolTip(this.labelName, resources.GetString("labelName.ToolTip"));
            // 
            // buttonDisconnect
            // 
            resources.ApplyResources(this.buttonDisconnect, "buttonDisconnect");
            this.buttonDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.buttonDisconnect.FlatAppearance.BorderSize = 0;
            this.buttonDisconnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(73)))), ((int)(((byte)(162)))));
            this.buttonDisconnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(86)))), ((int)(((byte)(193)))));
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.toolTipInfo.SetToolTip(this.buttonDisconnect, resources.GetString("buttonDisconnect.ToolTip"));
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.Disconnect);
            this.buttonDisconnect.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonPaint);
            // 
            // buttonConnect
            // 
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.buttonConnect.FlatAppearance.BorderSize = 0;
            this.buttonConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(73)))), ((int)(((byte)(162)))));
            this.buttonConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(86)))), ((int)(((byte)(193)))));
            this.buttonConnect.Name = "buttonConnect";
            this.toolTipInfo.SetToolTip(this.buttonConnect, resources.GetString("buttonConnect.ToolTip"));
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.Connect);
            this.buttonConnect.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonPaint);
            // 
            // buttonUpdatePresence
            // 
            resources.ApplyResources(this.buttonUpdatePresence, "buttonUpdatePresence");
            this.buttonUpdatePresence.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.buttonUpdatePresence.FlatAppearance.BorderSize = 0;
            this.buttonUpdatePresence.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(73)))), ((int)(((byte)(162)))));
            this.buttonUpdatePresence.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(86)))), ((int)(((byte)(193)))));
            this.buttonUpdatePresence.Name = "buttonUpdatePresence";
            this.toolTipInfo.SetToolTip(this.buttonUpdatePresence, resources.GetString("buttonUpdatePresence.ToolTip"));
            this.buttonUpdatePresence.UseVisualStyleBackColor = true;
            this.buttonUpdatePresence.Click += new System.EventHandler(this.Update);
            this.buttonUpdatePresence.Paint += new System.Windows.Forms.PaintEventHandler(this.ButtonPaint);
            // 
            // labelDetailsURL
            // 
            resources.ApplyResources(this.labelDetailsURL, "labelDetailsURL");
            this.labelDetailsURL.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelDetailsURL.Name = "labelDetailsURL";
            this.toolTipInfo.SetToolTip(this.labelDetailsURL, resources.GetString("labelDetailsURL.ToolTip"));
            // 
            // labelStateURL
            // 
            resources.ApplyResources(this.labelStateURL, "labelStateURL");
            this.labelStateURL.Cursor = System.Windows.Forms.Cursors.Help;
            this.labelStateURL.Name = "labelStateURL";
            this.toolTipInfo.SetToolTip(this.labelStateURL, resources.GetString("labelStateURL.ToolTip"));
            // 
            // panelTimestamps
            // 
            this.panelTimestamps.Controls.Add(this.tableLayoutPanelCustomTimestamps);
            this.panelTimestamps.Controls.Add(this.radioButtonPresence);
            this.panelTimestamps.Controls.Add(this.radioButtonCustom);
            this.panelTimestamps.Controls.Add(this.radioButtonLocalTime);
            this.panelTimestamps.Controls.Add(this.radioButtonLastConnection);
            this.panelTimestamps.Controls.Add(this.radioButtonStartTime);
            resources.ApplyResources(this.panelTimestamps, "panelTimestamps");
            this.panelTimestamps.Name = "panelTimestamps";
            // 
            // tableLayoutPanelCustomTimestamps
            // 
            resources.ApplyResources(this.tableLayoutPanelCustomTimestamps, "tableLayoutPanelCustomTimestamps");
            this.tableLayoutPanelCustomTimestamps.Controls.Add(this.dateTimePickerTimestampEnd, 3, 0);
            this.tableLayoutPanelCustomTimestamps.Controls.Add(this.dateTimePickerTimestampStart, 1, 0);
            this.tableLayoutPanelCustomTimestamps.Controls.Add(this.labelTimestampStart, 0, 0);
            this.tableLayoutPanelCustomTimestamps.Controls.Add(this.checkBoxTimestampEnd, 2, 0);
            this.tableLayoutPanelCustomTimestamps.Name = "tableLayoutPanelCustomTimestamps";
            // 
            // dateTimePickerTimestampEnd
            // 
            resources.ApplyResources(this.dateTimePickerTimestampEnd, "dateTimePickerTimestampEnd");
            this.dateTimePickerTimestampEnd.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "customTimestampEnd", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePickerTimestampEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimestampEnd.MinDate = new System.DateTime(1969, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerTimestampEnd.Name = "dateTimePickerTimestampEnd";
            this.dateTimePickerTimestampEnd.Value = global::CustomRPC.Properties.Settings.Default.customTimestampEnd;
            // 
            // dateTimePickerTimestampStart
            // 
            resources.ApplyResources(this.dateTimePickerTimestampStart, "dateTimePickerTimestampStart");
            this.dateTimePickerTimestampStart.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "customTimestamp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePickerTimestampStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimestampStart.MinDate = new System.DateTime(1969, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerTimestampStart.Name = "dateTimePickerTimestampStart";
            this.dateTimePickerTimestampStart.Value = global::CustomRPC.Properties.Settings.Default.customTimestamp;
            // 
            // labelTimestampStart
            // 
            resources.ApplyResources(this.labelTimestampStart, "labelTimestampStart");
            this.labelTimestampStart.Name = "labelTimestampStart";
            // 
            // checkBoxTimestampEnd
            // 
            resources.ApplyResources(this.checkBoxTimestampEnd, "checkBoxTimestampEnd");
            this.checkBoxTimestampEnd.Checked = global::CustomRPC.Properties.Settings.Default.customTimestampEndEnabled;
            this.checkBoxTimestampEnd.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::CustomRPC.Properties.Settings.Default, "customTimestampEndEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBoxTimestampEnd.Name = "checkBoxTimestampEnd";
            this.checkBoxTimestampEnd.UseVisualStyleBackColor = true;
            this.checkBoxTimestampEnd.CheckedChanged += new System.EventHandler(this.TimestampEndChanged);
            // 
            // labelPartyOf
            // 
            resources.ApplyResources(this.labelPartyOf, "labelPartyOf");
            this.labelPartyOf.Name = "labelPartyOf";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelUsername,
            this.toolStripStatusLabelStatus});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.statusStrip.SizingGrip = false;
            // 
            // toolStripStatusLabelUsername
            // 
            this.toolStripStatusLabelUsername.Name = "toolStripStatusLabelUsername";
            resources.ApplyResources(this.toolStripStatusLabelUsername, "toolStripStatusLabelUsername");
            this.toolStripStatusLabelUsername.Spring = true;
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            resources.ApplyResources(this.toolStripStatusLabelStatus, "toolStripStatusLabelStatus");
            // 
            // comboBoxType
            // 
            this.comboBoxType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.comboBoxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxType, "comboBoxType");
            this.comboBoxType.ForeColor = System.Drawing.Color.White;
            this.comboBoxType.Name = "comboBoxType";
            this.comboBoxType.SelectedValueChanged += new System.EventHandler(this.PresenceTypeChanged);
            // 
            // panelSeparator1
            // 
            this.panelSeparator1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panelSeparator1, "panelSeparator1");
            this.panelSeparator1.Name = "panelSeparator1";
            // 
            // panelSeparator2
            // 
            this.panelSeparator2.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panelSeparator2, "panelSeparator2");
            this.panelSeparator2.Name = "panelSeparator2";
            // 
            // panelSeparator3
            // 
            this.panelSeparator3.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panelSeparator3, "panelSeparator3");
            this.panelSeparator3.Name = "panelSeparator3";
            // 
            // panelSeparator4
            // 
            this.panelSeparator4.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.panelSeparator4, "panelSeparator4");
            this.panelSeparator4.Name = "panelSeparator4";
            // 
            // tableLayoutPanelButtons
            // 
            resources.ApplyResources(this.tableLayoutPanelButtons, "tableLayoutPanelButtons");
            this.tableLayoutPanelButtons.Controls.Add(this.buttonUpdatePresence, 3, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.buttonConnect, 0, 0);
            this.tableLayoutPanelButtons.Controls.Add(this.buttonDisconnect, 1, 0);
            this.tableLayoutPanelButtons.Name = "tableLayoutPanelButtons";
            // 
            // flowLayoutPanelParty
            // 
            this.flowLayoutPanelParty.Controls.Add(this.numericUpDownPartySize);
            this.flowLayoutPanelParty.Controls.Add(this.labelPartyOf);
            this.flowLayoutPanelParty.Controls.Add(this.numericUpDownPartyMax);
            resources.ApplyResources(this.flowLayoutPanelParty, "flowLayoutPanelParty");
            this.flowLayoutPanelParty.Name = "flowLayoutPanelParty";
            // 
            // numericUpDownPartySize
            // 
            this.numericUpDownPartySize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.numericUpDownPartySize.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "partySize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownPartySize.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.numericUpDownPartySize, "numericUpDownPartySize");
            this.numericUpDownPartySize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPartySize.Name = "numericUpDownPartySize";
            this.numericUpDownPartySize.Value = global::CustomRPC.Properties.Settings.Default.partySize;
            this.numericUpDownPartySize.Validating += new System.ComponentModel.CancelEventHandler(this.PartySizeValidation);
            // 
            // numericUpDownPartyMax
            // 
            this.numericUpDownPartyMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.numericUpDownPartyMax.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "partyMax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownPartyMax.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.numericUpDownPartyMax, "numericUpDownPartyMax");
            this.numericUpDownPartyMax.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPartyMax.Name = "numericUpDownPartyMax";
            this.numericUpDownPartyMax.Value = global::CustomRPC.Properties.Settings.Default.partyMax;
            this.numericUpDownPartyMax.Validating += new System.ComponentModel.CancelEventHandler(this.PartySizeValidation);
            // 
            // textBoxStateURL
            // 
            this.textBoxStateURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxStateURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "stateURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxStateURL.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxStateURL, "textBoxStateURL");
            this.textBoxStateURL.Name = "textBoxStateURL";
            this.textBoxStateURL.Text = global::CustomRPC.Properties.Settings.Default.stateURL;
            this.textBoxStateURL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxStateURL.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxStateURL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxDetailsURL
            // 
            this.textBoxDetailsURL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxDetailsURL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "detailsURL", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxDetailsURL.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxDetailsURL, "textBoxDetailsURL");
            this.textBoxDetailsURL.Name = "textBoxDetailsURL";
            this.textBoxDetailsURL.Text = global::CustomRPC.Properties.Settings.Default.detailsURL;
            this.textBoxDetailsURL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxDetailsURL.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxDetailsURL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxName
            // 
            this.textBoxName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxName.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "name", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxName.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Text = global::CustomRPC.Properties.Settings.Default.name;
            this.textBoxName.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxName.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxName.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton2Text
            // 
            this.textBoxButton2Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxButton2Text.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button2Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxButton2Text.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxButton2Text, "textBoxButton2Text");
            this.textBoxButton2Text.Name = "textBoxButton2Text";
            this.textBoxButton2Text.Text = global::CustomRPC.Properties.Settings.Default.button2Text;
            this.textBoxButton2Text.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton2Text.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxButton2Text.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton2URL
            // 
            this.textBoxButton2URL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxButton2URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button2Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxButton2URL.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxButton2URL, "textBoxButton2URL");
            this.textBoxButton2URL.Name = "textBoxButton2URL";
            this.textBoxButton2URL.Text = global::CustomRPC.Properties.Settings.Default.button2URL;
            this.textBoxButton2URL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton2URL.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxButton2URL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton1URL
            // 
            this.textBoxButton1URL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxButton1URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxButton1URL.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxButton1URL, "textBoxButton1URL");
            this.textBoxButton1URL.Name = "textBoxButton1URL";
            this.textBoxButton1URL.Text = global::CustomRPC.Properties.Settings.Default.button1URL;
            this.textBoxButton1URL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton1URL.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxButton1URL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton1Text
            // 
            this.textBoxButton1Text.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxButton1Text.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxButton1Text.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxButton1Text, "textBoxButton1Text");
            this.textBoxButton1Text.Name = "textBoxButton1Text";
            this.textBoxButton1Text.Text = global::CustomRPC.Properties.Settings.Default.button1Text;
            this.textBoxButton1Text.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton1Text.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxButton1Text.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // comboBoxSmallKey
            // 
            this.comboBoxSmallKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.comboBoxSmallKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.comboBoxSmallKey, "comboBoxSmallKey");
            this.comboBoxSmallKey.ForeColor = System.Drawing.Color.White;
            this.comboBoxSmallKey.Name = "comboBoxSmallKey";
            this.comboBoxSmallKey.Sorted = true;
            this.comboBoxSmallKey.Text = global::CustomRPC.Properties.Settings.Default.smallKey;
            this.comboBoxSmallKey.DropDown += new System.EventHandler(this.FetchAssets);
            this.comboBoxSmallKey.TextChanged += new System.EventHandler(this.LengthValidation);
            this.comboBoxSmallKey.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.comboBoxSmallKey.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxSmallText
            // 
            this.textBoxSmallText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxSmallText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxSmallText.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxSmallText, "textBoxSmallText");
            this.textBoxSmallText.Name = "textBoxSmallText";
            this.textBoxSmallText.Text = global::CustomRPC.Properties.Settings.Default.smallText;
            this.textBoxSmallText.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxSmallText.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxSmallText.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxLargeText
            // 
            this.textBoxLargeText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxLargeText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxLargeText.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxLargeText, "textBoxLargeText");
            this.textBoxLargeText.Name = "textBoxLargeText";
            this.textBoxLargeText.Text = global::CustomRPC.Properties.Settings.Default.largeText;
            this.textBoxLargeText.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxLargeText.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxLargeText.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // comboBoxLargeKey
            // 
            this.comboBoxLargeKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.comboBoxLargeKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.comboBoxLargeKey, "comboBoxLargeKey");
            this.comboBoxLargeKey.ForeColor = System.Drawing.Color.White;
            this.comboBoxLargeKey.Name = "comboBoxLargeKey";
            this.comboBoxLargeKey.Sorted = true;
            this.comboBoxLargeKey.Text = global::CustomRPC.Properties.Settings.Default.largeKey;
            this.comboBoxLargeKey.DropDown += new System.EventHandler(this.FetchAssets);
            this.comboBoxLargeKey.TextChanged += new System.EventHandler(this.LengthValidation);
            this.comboBoxLargeKey.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.comboBoxLargeKey.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxState
            // 
            this.textBoxState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxState.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "state", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxState.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxState, "textBoxState");
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Text = global::CustomRPC.Properties.Settings.Default.state;
            this.textBoxState.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxState.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxState.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxDetails.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "details", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxDetails.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.Text = global::CustomRPC.Properties.Settings.Default.details;
            this.textBoxDetails.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxDetails.Leave += new System.EventHandler(this.TrimTextBoxes);
            this.textBoxDetails.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxID
            // 
            this.textBoxID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(68)))), ((int)(((byte)(75)))));
            this.textBoxID.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "id", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxID.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxID, "textBoxID");
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Text = global::CustomRPC.Properties.Settings.Default.id;
            this.textBoxID.TextChanged += new System.EventHandler(this.OnlyNumbers);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.Controls.Add(this.labelStateURL);
            this.Controls.Add(this.textBoxStateURL);
            this.Controls.Add(this.labelDetailsURL);
            this.Controls.Add(this.textBoxDetailsURL);
            this.Controls.Add(this.flowLayoutPanelParty);
            this.Controls.Add(this.panelSeparator4);
            this.Controls.Add(this.panelSeparator3);
            this.Controls.Add(this.panelSeparator2);
            this.Controls.Add(this.panelSeparator1);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelType);
            this.Controls.Add(this.comboBoxType);
            this.Controls.Add(this.tableLayoutPanelButtons);
            this.Controls.Add(this.labelButton2Text);
            this.Controls.Add(this.labelButton1Text);
            this.Controls.Add(this.labelButton2URL);
            this.Controls.Add(this.textBoxButton2Text);
            this.Controls.Add(this.textBoxButton2URL);
            this.Controls.Add(this.labelButton1);
            this.Controls.Add(this.labelButton2);
            this.Controls.Add(this.labelButton1URL);
            this.Controls.Add(this.textBoxButton1URL);
            this.Controls.Add(this.textBoxButton1Text);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.labelParty);
            this.Controls.Add(this.labelTimestamp);
            this.Controls.Add(this.panelTimestamps);
            this.Controls.Add(this.comboBoxSmallKey);
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
            this.Controls.Add(this.textBoxLargeText);
            this.Controls.Add(this.comboBoxLargeKey);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.menuStrip);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MinimizeToTray);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropHandler);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragDropEnter);
            this.trayMenuStrip.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelTimestamps.ResumeLayout(false);
            this.panelTimestamps.PerformLayout();
            this.tableLayoutPanelCustomTimestamps.ResumeLayout(false);
            this.tableLayoutPanelCustomTimestamps.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanelButtons.ResumeLayout(false);
            this.tableLayoutPanelButtons.PerformLayout();
            this.flowLayoutPanelParty.ResumeLayout(false);
            this.flowLayoutPanelParty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPartySize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPartyMax)).EndInit();
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
        private System.Windows.Forms.ComboBox comboBoxSmallKey;
        private System.Windows.Forms.TextBox textBoxSmallText;
        private System.Windows.Forms.ComboBox comboBoxLargeKey;
        private System.Windows.Forms.TextBox textBoxLargeText;
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
        private System.Windows.Forms.RadioButton radioButtonLastConnection;
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
        private System.Windows.Forms.ToolStripMenuItem loadPresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePresetToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorFile2;
        private System.Windows.Forms.Label labelParty;
        private System.Windows.Forms.Label labelPartyOf;
        private System.Windows.Forms.NumericUpDown numericUpDownPartyMax;
        private System.Windows.Forms.NumericUpDown numericUpDownPartySize;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelUsername;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimestampStart;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.TextBox textBoxButton2Text;
        private System.Windows.Forms.TextBox textBoxButton2URL;
        private System.Windows.Forms.Label labelButton1;
        private System.Windows.Forms.Label labelButton2;
        private System.Windows.Forms.Label labelButton1URL;
        private System.Windows.Forms.TextBox textBoxButton1URL;
        private System.Windows.Forms.TextBox textBoxButton1Text;
        private System.Windows.Forms.Label labelButton2URL;
        private System.Windows.Forms.Label labelButton1Text;
        private System.Windows.Forms.Label labelButton2Text;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSettings2;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayMenuDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem allowAnalyticsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem darkModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparatorSettings3;
        private System.Windows.Forms.ToolStripMenuItem sampleLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem samplePersonToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testSupporterToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioButtonPresence;
        private System.Windows.Forms.ToolStripMenuItem faqToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newPresetToolStripMenuItem;
        private System.Windows.Forms.Label labelType;
        private System.Windows.Forms.ComboBox comboBoxType;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Panel panelSeparator1;
        private System.Windows.Forms.Panel panelSeparator2;
        private System.Windows.Forms.Panel panelSeparator3;
        private System.Windows.Forms.Panel panelSeparator4;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonUpdatePresence;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButtons;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelParty;
        private System.Windows.Forms.Label labelDetailsURL;
        private System.Windows.Forms.TextBox textBoxDetailsURL;
        private System.Windows.Forms.Label labelStateURL;
        private System.Windows.Forms.TextBox textBoxStateURL;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCustomTimestamps;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimestampEnd;
        private System.Windows.Forms.Label labelTimestampStart;
        private System.Windows.Forms.CheckBox checkBoxTimestampEnd;
    }
}

