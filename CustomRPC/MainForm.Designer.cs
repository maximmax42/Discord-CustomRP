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
            this.loadPresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePresetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile1 = new System.Windows.Forms.ToolStripSeparator();
            this.uploadAssetsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorFile2 = new System.Windows.Forms.ToolStripSeparator();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runOnStartupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMinimizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorSettings1 = new System.Windows.Forms.ToolStripSeparator();
            this.checkUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.deToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTheManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp1 = new System.Windows.Forms.ToolStripSeparator();
            this.translatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ypsolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binarynoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nicolasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hebrewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.galaxy6430ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.djd320ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugeseBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viniciotricolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximmax42ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vietnameseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MykmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Phnthnhnm0612toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.labelParty = new System.Windows.Forms.Label();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.labelButton1 = new System.Windows.Forms.Label();
            this.labelButton2 = new System.Windows.Forms.Label();
            this.labelButton1URL = new System.Windows.Forms.Label();
            this.labelButton2URL = new System.Windows.Forms.Label();
            this.labelButton1Text = new System.Windows.Forms.Label();
            this.labelButton2Text = new System.Windows.Forms.Label();
            this.panelTimestamps = new System.Windows.Forms.Panel();
            this.dateTimePickerTimestamp = new System.Windows.Forms.DateTimePicker();
            this.labelPartyOf = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelPadding = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBoxButton2Text = new System.Windows.Forms.TextBox();
            this.textBoxButton2URL = new System.Windows.Forms.TextBox();
            this.textBoxButton1URL = new System.Windows.Forms.TextBox();
            this.textBoxButton1Text = new System.Windows.Forms.TextBox();
            this.numericUpDownPartySize = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownPartyMax = new System.Windows.Forms.NumericUpDown();
            this.textBoxSmallKey = new System.Windows.Forms.TextBox();
            this.textBoxSmallText = new System.Windows.Forms.TextBox();
            this.textBoxLargeText = new System.Windows.Forms.TextBox();
            this.textBoxLargeKey = new System.Windows.Forms.TextBox();
            this.textBoxState = new System.Windows.Forms.TextBox();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.psychonautToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lithuanianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.panelTimestamps.SuspendLayout();
            this.statusStrip.SuspendLayout();
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
            this.toolStripSeparatorSettings1,
            this.checkUpdatesToolStripMenuItem,
            this.toolStripSeparator1,
            this.languageToolStripMenuItem});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.defaultToolStripMenuItem,
            this.toolStripSeparator2,
            this.deToolStripMenuItem,
            this.enToolStripMenuItem,
            this.frToolStripMenuItem,
            this.heToolStripMenuItem,
            this.itToolStripMenuItem,
            this.ltToolStripMenuItem,
            this.ptBRToolStripMenuItem,
            this.ruToolStripMenuItem,
            this.viToolStripMenuItem});
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
            // deToolStripMenuItem
            // 
            this.deToolStripMenuItem.CheckOnClick = true;
            this.deToolStripMenuItem.Name = "deToolStripMenuItem";
            resources.ApplyResources(this.deToolStripMenuItem, "deToolStripMenuItem");
            this.deToolStripMenuItem.Tag = "de";
            this.deToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // enToolStripMenuItem
            // 
            this.enToolStripMenuItem.CheckOnClick = true;
            this.enToolStripMenuItem.Name = "enToolStripMenuItem";
            resources.ApplyResources(this.enToolStripMenuItem, "enToolStripMenuItem");
            this.enToolStripMenuItem.Tag = "en";
            this.enToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // frToolStripMenuItem
            // 
            this.frToolStripMenuItem.CheckOnClick = true;
            this.frToolStripMenuItem.Name = "frToolStripMenuItem";
            resources.ApplyResources(this.frToolStripMenuItem, "frToolStripMenuItem");
            this.frToolStripMenuItem.Tag = "fr";
            this.frToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // heToolStripMenuItem
            // 
            this.heToolStripMenuItem.CheckOnClick = true;
            this.heToolStripMenuItem.Name = "heToolStripMenuItem";
            resources.ApplyResources(this.heToolStripMenuItem, "heToolStripMenuItem");
            this.heToolStripMenuItem.Tag = "he";
            this.heToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // itToolStripMenuItem
            // 
            this.itToolStripMenuItem.CheckOnClick = true;
            this.itToolStripMenuItem.Name = "itToolStripMenuItem";
            resources.ApplyResources(this.itToolStripMenuItem, "itToolStripMenuItem");
            this.itToolStripMenuItem.Tag = "it";
            this.itToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // ptBRToolStripMenuItem
            // 
            this.ptBRToolStripMenuItem.CheckOnClick = true;
            this.ptBRToolStripMenuItem.Name = "ptBRToolStripMenuItem";
            resources.ApplyResources(this.ptBRToolStripMenuItem, "ptBRToolStripMenuItem");
            this.ptBRToolStripMenuItem.Tag = "pt-br";
            this.ptBRToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // ruToolStripMenuItem
            // 
            this.ruToolStripMenuItem.CheckOnClick = true;
            this.ruToolStripMenuItem.Name = "ruToolStripMenuItem";
            resources.ApplyResources(this.ruToolStripMenuItem, "ruToolStripMenuItem");
            this.ruToolStripMenuItem.Tag = "ru";
            this.ruToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // viToolStripMenuItem
            // 
            this.viToolStripMenuItem.Name = "viToolStripMenuItem";
            resources.ApplyResources(this.viToolStripMenuItem, "viToolStripMenuItem");
            this.viToolStripMenuItem.Tag = "vi";
            this.viToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
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
            this.binarynoiseToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.nicolasToolStripMenuItem,
            this.hebrewToolStripMenuItem,
            this.galaxy6430ToolStripMenuItem,
            this.italianToolStripMenuItem,
            this.djd320ToolStripMenuItem,
            this.cubeToolStripMenuItem,
            this.lithuanianToolStripMenuItem,
            this.psychonautToolStripMenuItem,
            this.portugeseBRToolStripMenuItem,
            this.viniciotricolorToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.maximmax42ToolStripMenuItem,
            this.vietnameseToolStripMenuItem,
            this.MykmToolStripMenuItem,
            this.Phnthnhnm0612toolStripMenuItem});
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
            // binarynoiseToolStripMenuItem
            // 
            this.binarynoiseToolStripMenuItem.Name = "binarynoiseToolStripMenuItem";
            resources.ApplyResources(this.binarynoiseToolStripMenuItem, "binarynoiseToolStripMenuItem");
            this.binarynoiseToolStripMenuItem.Tag = "";
            // 
            // frenchToolStripMenuItem
            // 
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            // 
            // nicolasToolStripMenuItem
            // 
            this.nicolasToolStripMenuItem.Name = "nicolasToolStripMenuItem";
            resources.ApplyResources(this.nicolasToolStripMenuItem, "nicolasToolStripMenuItem");
            this.nicolasToolStripMenuItem.Tag = "";
            // 
            // hebrewToolStripMenuItem
            // 
            resources.ApplyResources(this.hebrewToolStripMenuItem, "hebrewToolStripMenuItem");
            this.hebrewToolStripMenuItem.Name = "hebrewToolStripMenuItem";
            // 
            // galaxy6430ToolStripMenuItem
            // 
            this.galaxy6430ToolStripMenuItem.Name = "galaxy6430ToolStripMenuItem";
            resources.ApplyResources(this.galaxy6430ToolStripMenuItem, "galaxy6430ToolStripMenuItem");
            this.galaxy6430ToolStripMenuItem.Tag = "https://www.youtube.com/channel/UC_cnrLEXfwsZoQxEsM95HXg";
            this.galaxy6430ToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // italianToolStripMenuItem
            // 
            resources.ApplyResources(this.italianToolStripMenuItem, "italianToolStripMenuItem");
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            // 
            // djd320ToolStripMenuItem
            // 
            this.djd320ToolStripMenuItem.Name = "djd320ToolStripMenuItem";
            resources.ApplyResources(this.djd320ToolStripMenuItem, "djd320ToolStripMenuItem");
            this.djd320ToolStripMenuItem.Tag = "";
            this.djd320ToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // cubeToolStripMenuItem
            // 
            this.cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            resources.ApplyResources(this.cubeToolStripMenuItem, "cubeToolStripMenuItem");
            this.cubeToolStripMenuItem.Tag = "https://mrcube.live/";
            this.cubeToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // portugeseBRToolStripMenuItem
            // 
            resources.ApplyResources(this.portugeseBRToolStripMenuItem, "portugeseBRToolStripMenuItem");
            this.portugeseBRToolStripMenuItem.Name = "portugeseBRToolStripMenuItem";
            // 
            // viniciotricolorToolStripMenuItem
            // 
            this.viniciotricolorToolStripMenuItem.Name = "viniciotricolorToolStripMenuItem";
            resources.ApplyResources(this.viniciotricolorToolStripMenuItem, "viniciotricolorToolStripMenuItem");
            this.viniciotricolorToolStripMenuItem.Tag = "";
            this.viniciotricolorToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
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
            // vietnameseToolStripMenuItem
            // 
            resources.ApplyResources(this.vietnameseToolStripMenuItem, "vietnameseToolStripMenuItem");
            this.vietnameseToolStripMenuItem.Name = "vietnameseToolStripMenuItem";
            // 
            // MykmToolStripMenuItem
            // 
            this.MykmToolStripMenuItem.Name = "MykmToolStripMenuItem";
            resources.ApplyResources(this.MykmToolStripMenuItem, "MykmToolStripMenuItem");
            this.MykmToolStripMenuItem.Tag = "";
            // 
            // Phnthnhnm0612toolStripMenuItem
            // 
            this.Phnthnhnm0612toolStripMenuItem.Name = "Phnthnhnm0612toolStripMenuItem";
            resources.ApplyResources(this.Phnthnhnm0612toolStripMenuItem, "Phnthnhnm0612toolStripMenuItem");
            this.Phnthnhnm0612toolStripMenuItem.Tag = "";
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
            // labelParty
            // 
            resources.ApplyResources(this.labelParty, "labelParty");
            this.labelParty.Name = "labelParty";
            this.toolTipInfo.SetToolTip(this.labelParty, resources.GetString("labelParty.ToolTip"));
            // 
            // radioButtonCustom
            // 
            resources.ApplyResources(this.radioButtonCustom, "radioButtonCustom");
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.TabStop = true;
            this.toolTipInfo.SetToolTip(this.radioButtonCustom, resources.GetString("radioButtonCustom.ToolTip"));
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.CheckedChanged += new System.EventHandler(this.TimestampsChanged);
            // 
            // labelButton1
            // 
            resources.ApplyResources(this.labelButton1, "labelButton1");
            this.labelButton1.Name = "labelButton1";
            this.toolTipInfo.SetToolTip(this.labelButton1, resources.GetString("labelButton1.ToolTip"));
            // 
            // labelButton2
            // 
            resources.ApplyResources(this.labelButton2, "labelButton2");
            this.labelButton2.Name = "labelButton2";
            this.toolTipInfo.SetToolTip(this.labelButton2, resources.GetString("labelButton2.ToolTip"));
            // 
            // labelButton1URL
            // 
            resources.ApplyResources(this.labelButton1URL, "labelButton1URL");
            this.labelButton1URL.Name = "labelButton1URL";
            this.toolTipInfo.SetToolTip(this.labelButton1URL, resources.GetString("labelButton1URL.ToolTip"));
            // 
            // labelButton2URL
            // 
            resources.ApplyResources(this.labelButton2URL, "labelButton2URL");
            this.labelButton2URL.Name = "labelButton2URL";
            this.toolTipInfo.SetToolTip(this.labelButton2URL, resources.GetString("labelButton2URL.ToolTip"));
            // 
            // labelButton1Text
            // 
            resources.ApplyResources(this.labelButton1Text, "labelButton1Text");
            this.labelButton1Text.Name = "labelButton1Text";
            this.toolTipInfo.SetToolTip(this.labelButton1Text, resources.GetString("labelButton1Text.ToolTip"));
            // 
            // labelButton2Text
            // 
            resources.ApplyResources(this.labelButton2Text, "labelButton2Text");
            this.labelButton2Text.Name = "labelButton2Text";
            this.toolTipInfo.SetToolTip(this.labelButton2Text, resources.GetString("labelButton2Text.ToolTip"));
            // 
            // panelTimestamps
            // 
            this.panelTimestamps.Controls.Add(this.dateTimePickerTimestamp);
            this.panelTimestamps.Controls.Add(this.radioButtonCustom);
            this.panelTimestamps.Controls.Add(this.radioButtonLocalTime);
            this.panelTimestamps.Controls.Add(this.radioButtonNone);
            this.panelTimestamps.Controls.Add(this.radioButtonStartTime);
            resources.ApplyResources(this.panelTimestamps, "panelTimestamps");
            this.panelTimestamps.Name = "panelTimestamps";
            // 
            // dateTimePickerTimestamp
            // 
            resources.ApplyResources(this.dateTimePickerTimestamp, "dateTimePickerTimestamp");
            this.dateTimePickerTimestamp.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "customTimestamp", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dateTimePickerTimestamp.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimestamp.MinDate = new System.DateTime(1969, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerTimestamp.Name = "dateTimePickerTimestamp";
            this.dateTimePickerTimestamp.Value = global::CustomRPC.Properties.Settings.Default.customTimestamp;
            // 
            // labelPartyOf
            // 
            resources.ApplyResources(this.labelPartyOf, "labelPartyOf");
            this.labelPartyOf.Name = "labelPartyOf";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelPadding,
            this.toolStripStatusLabelStatus});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            // 
            // toolStripStatusLabelPadding
            // 
            this.toolStripStatusLabelPadding.Name = "toolStripStatusLabelPadding";
            resources.ApplyResources(this.toolStripStatusLabelPadding, "toolStripStatusLabelPadding");
            this.toolStripStatusLabelPadding.Spring = true;
            // 
            // toolStripStatusLabelStatus
            // 
            this.toolStripStatusLabelStatus.Name = "toolStripStatusLabelStatus";
            resources.ApplyResources(this.toolStripStatusLabelStatus, "toolStripStatusLabelStatus");
            // 
            // textBoxButton2Text
            // 
            this.textBoxButton2Text.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button2Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton2Text, "textBoxButton2Text");
            this.textBoxButton2Text.Name = "textBoxButton2Text";
            this.textBoxButton2Text.Text = global::CustomRPC.Properties.Settings.Default.button2Text;
            // 
            // textBoxButton2URL
            // 
            this.textBoxButton2URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button2Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton2URL, "textBoxButton2URL");
            this.textBoxButton2URL.Name = "textBoxButton2URL";
            this.textBoxButton2URL.Text = global::CustomRPC.Properties.Settings.Default.button2URL;
            // 
            // textBoxButton1URL
            // 
            this.textBoxButton1URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton1URL, "textBoxButton1URL");
            this.textBoxButton1URL.Name = "textBoxButton1URL";
            this.textBoxButton1URL.Text = global::CustomRPC.Properties.Settings.Default.button1URL;
            // 
            // textBoxButton1Text
            // 
            this.textBoxButton1Text.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton1Text, "textBoxButton1Text");
            this.textBoxButton1Text.Name = "textBoxButton1Text";
            this.textBoxButton1Text.Text = global::CustomRPC.Properties.Settings.Default.button1Text;
            // 
            // numericUpDownPartySize
            // 
            this.numericUpDownPartySize.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "partySize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.numericUpDownPartySize, "numericUpDownPartySize");
            this.numericUpDownPartySize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPartySize.Name = "numericUpDownPartySize";
            this.numericUpDownPartySize.Value = global::CustomRPC.Properties.Settings.Default.partySize;
            // 
            // numericUpDownPartyMax
            // 
            this.numericUpDownPartyMax.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "partyMax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownPartyMax.DataBindings.Add(new System.Windows.Forms.Binding("Minimum", global::CustomRPC.Properties.Settings.Default, "partySize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.numericUpDownPartyMax, "numericUpDownPartyMax");
            this.numericUpDownPartyMax.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDownPartyMax.Minimum = global::CustomRPC.Properties.Settings.Default.partySize;
            this.numericUpDownPartyMax.Name = "numericUpDownPartyMax";
            this.numericUpDownPartyMax.Value = global::CustomRPC.Properties.Settings.Default.partyMax;
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
            // psychonautToolStripMenuItem
            // 
            this.psychonautToolStripMenuItem.Name = "psychonautToolStripMenuItem";
            resources.ApplyResources(this.psychonautToolStripMenuItem, "psychonautToolStripMenuItem");
            this.psychonautToolStripMenuItem.Tag = "";
            // 
            // lithuanianToolStripMenuItem
            // 
            resources.ApplyResources(this.lithuanianToolStripMenuItem, "lithuanianToolStripMenuItem");
            this.lithuanianToolStripMenuItem.Name = "lithuanianToolStripMenuItem";
            // 
            // ltToolStripMenuItem
            // 
            this.ltToolStripMenuItem.CheckOnClick = true;
            this.ltToolStripMenuItem.Name = "ltToolStripMenuItem";
            resources.ApplyResources(this.ltToolStripMenuItem, "ltToolStripMenuItem");
            this.ltToolStripMenuItem.Tag = "lt";
            this.ltToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
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
            this.Controls.Add(this.numericUpDownPartySize);
            this.Controls.Add(this.numericUpDownPartyMax);
            this.Controls.Add(this.labelPartyOf);
            this.Controls.Add(this.labelParty);
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
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
        private System.Windows.Forms.Label labelParty;
        private System.Windows.Forms.Label labelPartyOf;
        private System.Windows.Forms.NumericUpDown numericUpDownPartyMax;
        private System.Windows.Forms.NumericUpDown numericUpDownPartySize;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPadding;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStatus;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimestamp;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.ToolStripMenuItem italianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem djd320ToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem cubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem portugeseBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viniciotricolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hebrewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem galaxy6430ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem heToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem itToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ptBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ruToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vietnameseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MykmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Phnthnhnm0612toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nicolasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarynoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lithuanianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem psychonautToolStripMenuItem;
    }
}

