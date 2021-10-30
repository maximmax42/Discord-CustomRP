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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.agERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.csToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.daToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.esToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.heToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.huToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.idToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.itToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ltToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ptBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zhhansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zhhantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openTheManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gitHubPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp1 = new System.Windows.Forms.ToolStripSeparator();
            this.translatorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arabicEGToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shadowlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.czechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jayJakeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sebastianHviidToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tobiasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ypsolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ahmadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binarynoiseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.greekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.alexGrivasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vexotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.epicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pabloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nicolasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hebrewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.galaxy6430ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kahpotVanillaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hungarianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ballaBotondToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indonesianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hapnanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.apolyciousToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.italianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.djd320ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cubeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kurdishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.samTheNoobToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lithuanianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.psychonautToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dutchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jeremyzijlemansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lisoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.portugeseBRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viniciotricolorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.russianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximmax42ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thaiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.squisheeFreshyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.turkishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ozanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukrainianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mechaniXToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dmitromintenkoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vietnameseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MykmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Phnthnhnm0612toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsbachleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simplifiedChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zjsunToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zozochaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.traditionalChineseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.westxluToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparatorHelp2 = new System.Windows.Forms.ToolStripSeparator();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.autoconnectToolStripMenuItem,
            this.toolStripSeparatorSettings1,
            this.checkUpdatesToolStripMenuItem,
            this.toolStripSeparator1,
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
            this.runOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            resources.ApplyResources(this.startMinimizedToolStripMenuItem, "startMinimizedToolStripMenuItem");
            this.startMinimizedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // autoconnectToolStripMenuItem
            // 
            this.autoconnectToolStripMenuItem.Checked = true;
            this.autoconnectToolStripMenuItem.CheckOnClick = true;
            this.autoconnectToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autoconnectToolStripMenuItem.Name = "autoconnectToolStripMenuItem";
            resources.ApplyResources(this.autoconnectToolStripMenuItem, "autoconnectToolStripMenuItem");
            this.autoconnectToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
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
            this.agERToolStripMenuItem,
            this.csToolStripMenuItem,
            this.daToolStripMenuItem,
            this.deToolStripMenuItem,
            this.elToolStripMenuItem,
            this.enToolStripMenuItem,
            this.esToolStripMenuItem,
            this.frToolStripMenuItem,
            this.heToolStripMenuItem,
            this.huToolStripMenuItem,
            this.idToolStripMenuItem,
            this.itToolStripMenuItem,
            this.kuToolStripMenuItem,
            this.ltToolStripMenuItem,
            this.nlToolStripMenuItem,
            this.plToolStripMenuItem,
            this.ptBRToolStripMenuItem,
            this.ruToolStripMenuItem,
            this.thToolStripMenuItem,
            this.trToolStripMenuItem,
            this.ukToolStripMenuItem,
            this.viToolStripMenuItem,
            this.zhhansToolStripMenuItem,
            this.zhhantToolStripMenuItem});
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
            // agERToolStripMenuItem
            // 
            this.agERToolStripMenuItem.CheckOnClick = true;
            this.agERToolStripMenuItem.Name = "agERToolStripMenuItem";
            resources.ApplyResources(this.agERToolStripMenuItem, "agERToolStripMenuItem");
            this.agERToolStripMenuItem.Tag = "ar-EG";
            this.agERToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // csToolStripMenuItem
            // 
            this.csToolStripMenuItem.CheckOnClick = true;
            this.csToolStripMenuItem.Name = "csToolStripMenuItem";
            resources.ApplyResources(this.csToolStripMenuItem, "csToolStripMenuItem");
            this.csToolStripMenuItem.Tag = "cs";
            this.csToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // daToolStripMenuItem
            // 
            this.daToolStripMenuItem.CheckOnClick = true;
            this.daToolStripMenuItem.Name = "daToolStripMenuItem";
            resources.ApplyResources(this.daToolStripMenuItem, "daToolStripMenuItem");
            this.daToolStripMenuItem.Tag = "da";
            this.daToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // deToolStripMenuItem
            // 
            this.deToolStripMenuItem.CheckOnClick = true;
            this.deToolStripMenuItem.Name = "deToolStripMenuItem";
            resources.ApplyResources(this.deToolStripMenuItem, "deToolStripMenuItem");
            this.deToolStripMenuItem.Tag = "de";
            this.deToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // elToolStripMenuItem
            // 
            this.elToolStripMenuItem.CheckOnClick = true;
            this.elToolStripMenuItem.Name = "elToolStripMenuItem";
            resources.ApplyResources(this.elToolStripMenuItem, "elToolStripMenuItem");
            this.elToolStripMenuItem.Tag = "el";
            this.elToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // enToolStripMenuItem
            // 
            this.enToolStripMenuItem.CheckOnClick = true;
            this.enToolStripMenuItem.Name = "enToolStripMenuItem";
            resources.ApplyResources(this.enToolStripMenuItem, "enToolStripMenuItem");
            this.enToolStripMenuItem.Tag = "en";
            this.enToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // esToolStripMenuItem
            // 
            this.esToolStripMenuItem.CheckOnClick = true;
            this.esToolStripMenuItem.Name = "esToolStripMenuItem";
            resources.ApplyResources(this.esToolStripMenuItem, "esToolStripMenuItem");
            this.esToolStripMenuItem.Tag = "es";
            this.esToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
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
            // huToolStripMenuItem
            // 
            this.huToolStripMenuItem.CheckOnClick = true;
            this.huToolStripMenuItem.Name = "huToolStripMenuItem";
            resources.ApplyResources(this.huToolStripMenuItem, "huToolStripMenuItem");
            this.huToolStripMenuItem.Tag = "hu";
            this.huToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // idToolStripMenuItem
            // 
            this.idToolStripMenuItem.CheckOnClick = true;
            this.idToolStripMenuItem.Name = "idToolStripMenuItem";
            resources.ApplyResources(this.idToolStripMenuItem, "idToolStripMenuItem");
            this.idToolStripMenuItem.Tag = "id";
            this.idToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // itToolStripMenuItem
            // 
            this.itToolStripMenuItem.CheckOnClick = true;
            this.itToolStripMenuItem.Name = "itToolStripMenuItem";
            resources.ApplyResources(this.itToolStripMenuItem, "itToolStripMenuItem");
            this.itToolStripMenuItem.Tag = "it";
            this.itToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // kuToolStripMenuItem
            // 
            this.kuToolStripMenuItem.CheckOnClick = true;
            this.kuToolStripMenuItem.Name = "kuToolStripMenuItem";
            resources.ApplyResources(this.kuToolStripMenuItem, "kuToolStripMenuItem");
            this.kuToolStripMenuItem.Tag = "ku";
            this.kuToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // ltToolStripMenuItem
            // 
            this.ltToolStripMenuItem.CheckOnClick = true;
            this.ltToolStripMenuItem.Name = "ltToolStripMenuItem";
            resources.ApplyResources(this.ltToolStripMenuItem, "ltToolStripMenuItem");
            this.ltToolStripMenuItem.Tag = "lt";
            this.ltToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // nlToolStripMenuItem
            // 
            this.nlToolStripMenuItem.CheckOnClick = true;
            this.nlToolStripMenuItem.Name = "nlToolStripMenuItem";
            resources.ApplyResources(this.nlToolStripMenuItem, "nlToolStripMenuItem");
            this.nlToolStripMenuItem.Tag = "nl";
            this.nlToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // plToolStripMenuItem
            // 
            this.plToolStripMenuItem.CheckOnClick = true;
            this.plToolStripMenuItem.Name = "plToolStripMenuItem";
            resources.ApplyResources(this.plToolStripMenuItem, "plToolStripMenuItem");
            this.plToolStripMenuItem.Tag = "pl";
            this.plToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
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
            // thToolStripMenuItem
            // 
            this.thToolStripMenuItem.Name = "thToolStripMenuItem";
            resources.ApplyResources(this.thToolStripMenuItem, "thToolStripMenuItem");
            this.thToolStripMenuItem.Tag = "th";
            this.thToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // trToolStripMenuItem
            // 
            this.trToolStripMenuItem.Name = "trToolStripMenuItem";
            resources.ApplyResources(this.trToolStripMenuItem, "trToolStripMenuItem");
            this.trToolStripMenuItem.Tag = "tr";
            this.trToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // ukToolStripMenuItem
            // 
            this.ukToolStripMenuItem.Name = "ukToolStripMenuItem";
            resources.ApplyResources(this.ukToolStripMenuItem, "ukToolStripMenuItem");
            this.ukToolStripMenuItem.Tag = "uk";
            this.ukToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // viToolStripMenuItem
            // 
            this.viToolStripMenuItem.Name = "viToolStripMenuItem";
            resources.ApplyResources(this.viToolStripMenuItem, "viToolStripMenuItem");
            this.viToolStripMenuItem.Tag = "vi";
            this.viToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // zhhansToolStripMenuItem
            // 
            this.zhhansToolStripMenuItem.Name = "zhhansToolStripMenuItem";
            resources.ApplyResources(this.zhhansToolStripMenuItem, "zhhansToolStripMenuItem");
            this.zhhansToolStripMenuItem.Tag = "zh-Hans";
            this.zhhansToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // zhhantToolStripMenuItem
            // 
            this.zhhantToolStripMenuItem.Name = "zhhantToolStripMenuItem";
            resources.ApplyResources(this.zhhantToolStripMenuItem, "zhhantToolStripMenuItem");
            this.zhhantToolStripMenuItem.Tag = "zh-Hant";
            this.zhhantToolStripMenuItem.Click += new System.EventHandler(this.ChangeLanguage);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openTheManualToolStripMenuItem,
            this.gitHubPageToolStripMenuItem,
            this.toolStripSeparatorHelp1,
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
            this.arabicEGToolStripMenuItem,
            this.czechToolStripMenuItem,
            this.danishToolStripMenuItem,
            this.germanToolStripMenuItem,
            this.greekToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.hebrewToolStripMenuItem,
            this.hungarianToolStripMenuItem,
            this.indonesianToolStripMenuItem,
            this.italianToolStripMenuItem,
            this.kurdishToolStripMenuItem,
            this.lithuanianToolStripMenuItem,
            this.dutchToolStripMenuItem,
            this.polishToolStripMenuItem,
            this.portugeseBRToolStripMenuItem,
            this.russianToolStripMenuItem,
            this.thaiToolStripMenuItem,
            this.turkishToolStripMenuItem,
            this.ukrainianToolStripMenuItem,
            this.vietnameseToolStripMenuItem,
            this.simplifiedChineseToolStripMenuItem,
            this.traditionalChineseToolStripMenuItem});
            this.translatorsToolStripMenuItem.Name = "translatorsToolStripMenuItem";
            resources.ApplyResources(this.translatorsToolStripMenuItem, "translatorsToolStripMenuItem");
            // 
            // arabicEGToolStripMenuItem
            // 
            this.arabicEGToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shadowlToolStripMenuItem});
            this.arabicEGToolStripMenuItem.Name = "arabicEGToolStripMenuItem";
            resources.ApplyResources(this.arabicEGToolStripMenuItem, "arabicEGToolStripMenuItem");
            // 
            // shadowlToolStripMenuItem
            // 
            this.shadowlToolStripMenuItem.Name = "shadowlToolStripMenuItem";
            resources.ApplyResources(this.shadowlToolStripMenuItem, "shadowlToolStripMenuItem");
            this.shadowlToolStripMenuItem.Tag = "";
            this.shadowlToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // czechToolStripMenuItem
            // 
            this.czechToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jayJakeToolStripMenuItem});
            this.czechToolStripMenuItem.Name = "czechToolStripMenuItem";
            resources.ApplyResources(this.czechToolStripMenuItem, "czechToolStripMenuItem");
            // 
            // jayJakeToolStripMenuItem
            // 
            this.jayJakeToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.jayJakeToolStripMenuItem.Name = "jayJakeToolStripMenuItem";
            resources.ApplyResources(this.jayJakeToolStripMenuItem, "jayJakeToolStripMenuItem");
            this.jayJakeToolStripMenuItem.Tag = "https://jayjake.eu/";
            this.jayJakeToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // danishToolStripMenuItem
            // 
            this.danishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sebastianHviidToolStripMenuItem,
            this.tobiasToolStripMenuItem});
            this.danishToolStripMenuItem.Name = "danishToolStripMenuItem";
            resources.ApplyResources(this.danishToolStripMenuItem, "danishToolStripMenuItem");
            // 
            // sebastianHviidToolStripMenuItem
            // 
            this.sebastianHviidToolStripMenuItem.Name = "sebastianHviidToolStripMenuItem";
            resources.ApplyResources(this.sebastianHviidToolStripMenuItem, "sebastianHviidToolStripMenuItem");
            // 
            // tobiasToolStripMenuItem
            // 
            this.tobiasToolStripMenuItem.Name = "tobiasToolStripMenuItem";
            resources.ApplyResources(this.tobiasToolStripMenuItem, "tobiasToolStripMenuItem");
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ypsolToolStripMenuItem,
            this.ahmadToolStripMenuItem,
            this.binarynoiseToolStripMenuItem});
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            resources.ApplyResources(this.germanToolStripMenuItem, "germanToolStripMenuItem");
            // 
            // ypsolToolStripMenuItem
            // 
            this.ypsolToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.ypsolToolStripMenuItem.Name = "ypsolToolStripMenuItem";
            resources.ApplyResources(this.ypsolToolStripMenuItem, "ypsolToolStripMenuItem");
            this.ypsolToolStripMenuItem.Tag = "https://www.youtube.com/channel/UCxGqMDnXnEyVt4yugLeBpgA";
            this.ypsolToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // ahmadToolStripMenuItem
            // 
            this.ahmadToolStripMenuItem.Name = "ahmadToolStripMenuItem";
            resources.ApplyResources(this.ahmadToolStripMenuItem, "ahmadToolStripMenuItem");
            this.ahmadToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // binarynoiseToolStripMenuItem
            // 
            this.binarynoiseToolStripMenuItem.Name = "binarynoiseToolStripMenuItem";
            resources.ApplyResources(this.binarynoiseToolStripMenuItem, "binarynoiseToolStripMenuItem");
            this.binarynoiseToolStripMenuItem.Tag = "";
            this.binarynoiseToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // greekToolStripMenuItem
            // 
            this.greekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.alexGrivasToolStripMenuItem});
            this.greekToolStripMenuItem.Name = "greekToolStripMenuItem";
            resources.ApplyResources(this.greekToolStripMenuItem, "greekToolStripMenuItem");
            // 
            // alexGrivasToolStripMenuItem
            // 
            this.alexGrivasToolStripMenuItem.Name = "alexGrivasToolStripMenuItem";
            resources.ApplyResources(this.alexGrivasToolStripMenuItem, "alexGrivasToolStripMenuItem");
            this.alexGrivasToolStripMenuItem.Tag = "";
            this.alexGrivasToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vexotToolStripMenuItem,
            this.epicToolStripMenuItem,
            this.pabloToolStripMenuItem});
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            resources.ApplyResources(this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
            // 
            // vexotToolStripMenuItem
            // 
            this.vexotToolStripMenuItem.Name = "vexotToolStripMenuItem";
            resources.ApplyResources(this.vexotToolStripMenuItem, "vexotToolStripMenuItem");
            this.vexotToolStripMenuItem.Tag = "";
            this.vexotToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // epicToolStripMenuItem
            // 
            this.epicToolStripMenuItem.Name = "epicToolStripMenuItem";
            resources.ApplyResources(this.epicToolStripMenuItem, "epicToolStripMenuItem");
            this.epicToolStripMenuItem.Tag = "";
            this.epicToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // pabloToolStripMenuItem
            // 
            this.pabloToolStripMenuItem.Name = "pabloToolStripMenuItem";
            resources.ApplyResources(this.pabloToolStripMenuItem, "pabloToolStripMenuItem");
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nicolasToolStripMenuItem});
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            // 
            // nicolasToolStripMenuItem
            // 
            this.nicolasToolStripMenuItem.Name = "nicolasToolStripMenuItem";
            resources.ApplyResources(this.nicolasToolStripMenuItem, "nicolasToolStripMenuItem");
            this.nicolasToolStripMenuItem.Tag = "";
            this.nicolasToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // hebrewToolStripMenuItem
            // 
            this.hebrewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.galaxy6430ToolStripMenuItem,
            this.kahpotVanillaToolStripMenuItem});
            this.hebrewToolStripMenuItem.Name = "hebrewToolStripMenuItem";
            resources.ApplyResources(this.hebrewToolStripMenuItem, "hebrewToolStripMenuItem");
            // 
            // galaxy6430ToolStripMenuItem
            // 
            this.galaxy6430ToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.galaxy6430ToolStripMenuItem.Name = "galaxy6430ToolStripMenuItem";
            resources.ApplyResources(this.galaxy6430ToolStripMenuItem, "galaxy6430ToolStripMenuItem");
            this.galaxy6430ToolStripMenuItem.Tag = "https://www.youtube.com/channel/UC_cnrLEXfwsZoQxEsM95HXg";
            this.galaxy6430ToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // kahpotVanillaToolStripMenuItem
            // 
            this.kahpotVanillaToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.kahpotVanillaToolStripMenuItem.Name = "kahpotVanillaToolStripMenuItem";
            resources.ApplyResources(this.kahpotVanillaToolStripMenuItem, "kahpotVanillaToolStripMenuItem");
            this.kahpotVanillaToolStripMenuItem.Tag = "https://linktr.ee/KahpotVanilla";
            this.kahpotVanillaToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // hungarianToolStripMenuItem
            // 
            this.hungarianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ballaBotondToolStripMenuItem});
            this.hungarianToolStripMenuItem.Name = "hungarianToolStripMenuItem";
            resources.ApplyResources(this.hungarianToolStripMenuItem, "hungarianToolStripMenuItem");
            // 
            // ballaBotondToolStripMenuItem
            // 
            this.ballaBotondToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.ballaBotondToolStripMenuItem.Name = "ballaBotondToolStripMenuItem";
            resources.ApplyResources(this.ballaBotondToolStripMenuItem, "ballaBotondToolStripMenuItem");
            this.ballaBotondToolStripMenuItem.Tag = "https://github.com/BallaBotond";
            this.ballaBotondToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // indonesianToolStripMenuItem
            // 
            this.indonesianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hapnanToolStripMenuItem,
            this.apolyciousToolStripMenuItem});
            this.indonesianToolStripMenuItem.Name = "indonesianToolStripMenuItem";
            resources.ApplyResources(this.indonesianToolStripMenuItem, "indonesianToolStripMenuItem");
            // 
            // hapnanToolStripMenuItem
            // 
            this.hapnanToolStripMenuItem.Name = "hapnanToolStripMenuItem";
            resources.ApplyResources(this.hapnanToolStripMenuItem, "hapnanToolStripMenuItem");
            this.hapnanToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // apolyciousToolStripMenuItem
            // 
            this.apolyciousToolStripMenuItem.Name = "apolyciousToolStripMenuItem";
            resources.ApplyResources(this.apolyciousToolStripMenuItem, "apolyciousToolStripMenuItem");
            this.apolyciousToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // italianToolStripMenuItem
            // 
            this.italianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.djd320ToolStripMenuItem,
            this.cubeToolStripMenuItem});
            this.italianToolStripMenuItem.Name = "italianToolStripMenuItem";
            resources.ApplyResources(this.italianToolStripMenuItem, "italianToolStripMenuItem");
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
            this.cubeToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.cubeToolStripMenuItem.Name = "cubeToolStripMenuItem";
            resources.ApplyResources(this.cubeToolStripMenuItem, "cubeToolStripMenuItem");
            this.cubeToolStripMenuItem.Tag = "https://mrcube.live/";
            this.cubeToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // kurdishToolStripMenuItem
            // 
            this.kurdishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.samTheNoobToolStripMenuItem});
            this.kurdishToolStripMenuItem.Name = "kurdishToolStripMenuItem";
            resources.ApplyResources(this.kurdishToolStripMenuItem, "kurdishToolStripMenuItem");
            // 
            // samTheNoobToolStripMenuItem
            // 
            this.samTheNoobToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.samTheNoobToolStripMenuItem.Name = "samTheNoobToolStripMenuItem";
            resources.ApplyResources(this.samTheNoobToolStripMenuItem, "samTheNoobToolStripMenuItem");
            this.samTheNoobToolStripMenuItem.Tag = "https://discord.gg/stn69";
            this.samTheNoobToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // lithuanianToolStripMenuItem
            // 
            this.lithuanianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.psychonautToolStripMenuItem});
            this.lithuanianToolStripMenuItem.Name = "lithuanianToolStripMenuItem";
            resources.ApplyResources(this.lithuanianToolStripMenuItem, "lithuanianToolStripMenuItem");
            // 
            // psychonautToolStripMenuItem
            // 
            this.psychonautToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.psychonautToolStripMenuItem.Name = "psychonautToolStripMenuItem";
            resources.ApplyResources(this.psychonautToolStripMenuItem, "psychonautToolStripMenuItem");
            this.psychonautToolStripMenuItem.Tag = "https://www.twitch.tv/psychonaut303";
            this.psychonautToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // dutchToolStripMenuItem
            // 
            this.dutchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jeremyzijlemansToolStripMenuItem});
            this.dutchToolStripMenuItem.Name = "dutchToolStripMenuItem";
            resources.ApplyResources(this.dutchToolStripMenuItem, "dutchToolStripMenuItem");
            // 
            // jeremyzijlemansToolStripMenuItem
            // 
            this.jeremyzijlemansToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.jeremyzijlemansToolStripMenuItem.Name = "jeremyzijlemansToolStripMenuItem";
            resources.ApplyResources(this.jeremyzijlemansToolStripMenuItem, "jeremyzijlemansToolStripMenuItem");
            this.jeremyzijlemansToolStripMenuItem.Tag = "https://sionhub.co.uk/";
            this.jeremyzijlemansToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // polishToolStripMenuItem
            // 
            this.polishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lolToolStripMenuItem,
            this.lisoToolStripMenuItem});
            this.polishToolStripMenuItem.Name = "polishToolStripMenuItem";
            resources.ApplyResources(this.polishToolStripMenuItem, "polishToolStripMenuItem");
            // 
            // lolToolStripMenuItem
            // 
            this.lolToolStripMenuItem.Name = "lolToolStripMenuItem";
            resources.ApplyResources(this.lolToolStripMenuItem, "lolToolStripMenuItem");
            this.lolToolStripMenuItem.Tag = "";
            this.lolToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // lisoToolStripMenuItem
            // 
            this.lisoToolStripMenuItem.Name = "lisoToolStripMenuItem";
            resources.ApplyResources(this.lisoToolStripMenuItem, "lisoToolStripMenuItem");
            this.lisoToolStripMenuItem.Tag = "";
            this.lisoToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // portugeseBRToolStripMenuItem
            // 
            this.portugeseBRToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viniciotricolorToolStripMenuItem});
            this.portugeseBRToolStripMenuItem.Name = "portugeseBRToolStripMenuItem";
            resources.ApplyResources(this.portugeseBRToolStripMenuItem, "portugeseBRToolStripMenuItem");
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
            this.russianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maximmax42ToolStripMenuItem});
            this.russianToolStripMenuItem.Name = "russianToolStripMenuItem";
            resources.ApplyResources(this.russianToolStripMenuItem, "russianToolStripMenuItem");
            // 
            // maximmax42ToolStripMenuItem
            // 
            this.maximmax42ToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.maximmax42ToolStripMenuItem.Name = "maximmax42ToolStripMenuItem";
            resources.ApplyResources(this.maximmax42ToolStripMenuItem, "maximmax42ToolStripMenuItem");
            this.maximmax42ToolStripMenuItem.Tag = "https://www.maximmax42.ru";
            this.maximmax42ToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // thaiToolStripMenuItem
            // 
            this.thaiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.squisheeFreshyToolStripMenuItem,
            this.gameToolStripMenuItem});
            this.thaiToolStripMenuItem.Name = "thaiToolStripMenuItem";
            resources.ApplyResources(this.thaiToolStripMenuItem, "thaiToolStripMenuItem");
            // 
            // squisheeFreshyToolStripMenuItem
            // 
            this.squisheeFreshyToolStripMenuItem.Name = "squisheeFreshyToolStripMenuItem";
            resources.ApplyResources(this.squisheeFreshyToolStripMenuItem, "squisheeFreshyToolStripMenuItem");
            this.squisheeFreshyToolStripMenuItem.Tag = "";
            this.squisheeFreshyToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            resources.ApplyResources(this.gameToolStripMenuItem, "gameToolStripMenuItem");
            this.gameToolStripMenuItem.Tag = "";
            this.gameToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // turkishToolStripMenuItem
            // 
            this.turkishToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ozanToolStripMenuItem});
            this.turkishToolStripMenuItem.Name = "turkishToolStripMenuItem";
            resources.ApplyResources(this.turkishToolStripMenuItem, "turkishToolStripMenuItem");
            // 
            // ozanToolStripMenuItem
            // 
            this.ozanToolStripMenuItem.Name = "ozanToolStripMenuItem";
            resources.ApplyResources(this.ozanToolStripMenuItem, "ozanToolStripMenuItem");
            this.ozanToolStripMenuItem.Tag = "";
            this.ozanToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // ukrainianToolStripMenuItem
            // 
            this.ukrainianToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mechaniXToolStripMenuItem,
            this.dmitromintenkoToolStripMenuItem});
            this.ukrainianToolStripMenuItem.Name = "ukrainianToolStripMenuItem";
            resources.ApplyResources(this.ukrainianToolStripMenuItem, "ukrainianToolStripMenuItem");
            // 
            // mechaniXToolStripMenuItem
            // 
            this.mechaniXToolStripMenuItem.Name = "mechaniXToolStripMenuItem";
            resources.ApplyResources(this.mechaniXToolStripMenuItem, "mechaniXToolStripMenuItem");
            this.mechaniXToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // dmitromintenkoToolStripMenuItem
            // 
            this.dmitromintenkoToolStripMenuItem.Name = "dmitromintenkoToolStripMenuItem";
            resources.ApplyResources(this.dmitromintenkoToolStripMenuItem, "dmitromintenkoToolStripMenuItem");
            this.dmitromintenkoToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // vietnameseToolStripMenuItem
            // 
            this.vietnameseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MykmToolStripMenuItem,
            this.Phnthnhnm0612toolStripMenuItem,
            this.dsbachleToolStripMenuItem});
            this.vietnameseToolStripMenuItem.Name = "vietnameseToolStripMenuItem";
            resources.ApplyResources(this.vietnameseToolStripMenuItem, "vietnameseToolStripMenuItem");
            // 
            // MykmToolStripMenuItem
            // 
            this.MykmToolStripMenuItem.Name = "MykmToolStripMenuItem";
            resources.ApplyResources(this.MykmToolStripMenuItem, "MykmToolStripMenuItem");
            this.MykmToolStripMenuItem.Tag = "";
            this.MykmToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // Phnthnhnm0612toolStripMenuItem
            // 
            this.Phnthnhnm0612toolStripMenuItem.Name = "Phnthnhnm0612toolStripMenuItem";
            resources.ApplyResources(this.Phnthnhnm0612toolStripMenuItem, "Phnthnhnm0612toolStripMenuItem");
            this.Phnthnhnm0612toolStripMenuItem.Tag = "";
            this.Phnthnhnm0612toolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // dsbachleToolStripMenuItem
            // 
            this.dsbachleToolStripMenuItem.Name = "dsbachleToolStripMenuItem";
            resources.ApplyResources(this.dsbachleToolStripMenuItem, "dsbachleToolStripMenuItem");
            this.dsbachleToolStripMenuItem.Tag = "";
            this.dsbachleToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // simplifiedChineseToolStripMenuItem
            // 
            this.simplifiedChineseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zjsunToolStripMenuItem,
            this.zozochaToolStripMenuItem});
            this.simplifiedChineseToolStripMenuItem.Name = "simplifiedChineseToolStripMenuItem";
            resources.ApplyResources(this.simplifiedChineseToolStripMenuItem, "simplifiedChineseToolStripMenuItem");
            // 
            // zjsunToolStripMenuItem
            // 
            this.zjsunToolStripMenuItem.Name = "zjsunToolStripMenuItem";
            resources.ApplyResources(this.zjsunToolStripMenuItem, "zjsunToolStripMenuItem");
            this.zjsunToolStripMenuItem.Tag = "";
            this.zjsunToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // zozochaToolStripMenuItem
            // 
            this.zozochaToolStripMenuItem.Name = "zozochaToolStripMenuItem";
            resources.ApplyResources(this.zozochaToolStripMenuItem, "zozochaToolStripMenuItem");
            this.zozochaToolStripMenuItem.Tag = "";
            this.zozochaToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
            // 
            // traditionalChineseToolStripMenuItem
            // 
            this.traditionalChineseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.westxluToolStripMenuItem});
            this.traditionalChineseToolStripMenuItem.Name = "traditionalChineseToolStripMenuItem";
            resources.ApplyResources(this.traditionalChineseToolStripMenuItem, "traditionalChineseToolStripMenuItem");
            // 
            // westxluToolStripMenuItem
            // 
            this.westxluToolStripMenuItem.Image = global::CustomRPC.Properties.Resources.globe;
            this.westxluToolStripMenuItem.Name = "westxluToolStripMenuItem";
            resources.ApplyResources(this.westxluToolStripMenuItem, "westxluToolStripMenuItem");
            this.westxluToolStripMenuItem.Tag = "https://linktr.ee/westxlu";
            this.westxluToolStripMenuItem.Click += new System.EventHandler(this.OpenTranslatorPage);
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
            // buttonUpdatePresence
            // 
            resources.ApplyResources(this.buttonUpdatePresence, "buttonUpdatePresence");
            this.buttonUpdatePresence.AutoEllipsis = true;
            this.buttonUpdatePresence.Name = "buttonUpdatePresence";
            this.toolTipInfo.SetToolTip(this.buttonUpdatePresence, resources.GetString("buttonUpdatePresence.ToolTip"));
            this.buttonUpdatePresence.UseVisualStyleBackColor = true;
            this.buttonUpdatePresence.Click += new System.EventHandler(this.Update);
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoEllipsis = true;
            resources.ApplyResources(this.buttonConnect, "buttonConnect");
            this.buttonConnect.Name = "buttonConnect";
            this.toolTipInfo.SetToolTip(this.buttonConnect, resources.GetString("buttonConnect.ToolTip"));
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.Connect);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.AutoEllipsis = true;
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
            this.textBoxButton2Text.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton2Text.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton2URL
            // 
            this.textBoxButton2URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button2Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton2URL, "textBoxButton2URL");
            this.textBoxButton2URL.Name = "textBoxButton2URL";
            this.textBoxButton2URL.Text = global::CustomRPC.Properties.Settings.Default.button2URL;
            this.textBoxButton2URL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton2URL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton1URL
            // 
            this.textBoxButton1URL.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Url", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton1URL, "textBoxButton1URL");
            this.textBoxButton1URL.Name = "textBoxButton1URL";
            this.textBoxButton1URL.Text = global::CustomRPC.Properties.Settings.Default.button1URL;
            this.textBoxButton1URL.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton1URL.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxButton1Text
            // 
            this.textBoxButton1Text.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "button1Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxButton1Text, "textBoxButton1Text");
            this.textBoxButton1Text.Name = "textBoxButton1Text";
            this.textBoxButton1Text.Text = global::CustomRPC.Properties.Settings.Default.button1Text;
            this.textBoxButton1Text.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxButton1Text.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
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
            this.numericUpDownPartySize.Validating += new System.ComponentModel.CancelEventHandler(this.PartySizeValidation);
            // 
            // numericUpDownPartyMax
            // 
            this.numericUpDownPartyMax.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "partyMax", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
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
            // textBoxSmallKey
            // 
            this.textBoxSmallKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxSmallKey, "textBoxSmallKey");
            this.textBoxSmallKey.Name = "textBoxSmallKey";
            this.textBoxSmallKey.Text = global::CustomRPC.Properties.Settings.Default.smallKey;
            this.textBoxSmallKey.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxSmallKey.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxSmallText
            // 
            this.textBoxSmallText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "smallText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxSmallText, "textBoxSmallText");
            this.textBoxSmallText.Name = "textBoxSmallText";
            this.textBoxSmallText.Text = global::CustomRPC.Properties.Settings.Default.smallText;
            this.textBoxSmallText.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxSmallText.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxLargeText
            // 
            this.textBoxLargeText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxLargeText, "textBoxLargeText");
            this.textBoxLargeText.Name = "textBoxLargeText";
            this.textBoxLargeText.Text = global::CustomRPC.Properties.Settings.Default.largeText;
            this.textBoxLargeText.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxLargeText.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxLargeKey
            // 
            this.textBoxLargeKey.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "largeKey", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxLargeKey, "textBoxLargeKey");
            this.textBoxLargeKey.Name = "textBoxLargeKey";
            this.textBoxLargeKey.Text = global::CustomRPC.Properties.Settings.Default.largeKey;
            this.textBoxLargeKey.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxLargeKey.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxState
            // 
            this.textBoxState.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "state", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxState, "textBoxState");
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Text = global::CustomRPC.Properties.Settings.Default.state;
            this.textBoxState.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxState.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::CustomRPC.Properties.Settings.Default, "details", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.Text = global::CustomRPC.Properties.Settings.Default.details;
            this.textBoxDetails.TextChanged += new System.EventHandler(this.LengthValidation);
            this.textBoxDetails.Validating += new System.ComponentModel.CancelEventHandler(this.LengthValidationFocus);
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
            this.AllowDrop = true;
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
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDropHandler);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragDropEnter);
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
        private System.Windows.Forms.ToolStripMenuItem russianToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem portugeseBRToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hebrewToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem frToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ltToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lithuanianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dutchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zhhansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simplifiedChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traditionalChineseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zhhantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem csToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem czechToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thaiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arabicEGToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem esToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spanishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kurdishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem turkishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem idToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indonesianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem huToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hungarianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem trayMenuDisconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem elToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem greekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem alexGrivasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vexotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem epicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nicolasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shadowlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jayJakeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ypsolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binarynoiseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem galaxy6430ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kahpotVanillaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ballaBotondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hapnanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem apolyciousToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cubeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem djd320ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem samTheNoobToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem psychonautToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jeremyzijlemansToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lolToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lisoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viniciotricolorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maximmax42ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem squisheeFreshyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ozanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MykmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Phnthnhnm0612toolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dsbachleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zjsunToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zozochaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem westxluToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukrainianToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mechaniXToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dmitromintenkoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ahmadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem daToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sebastianHviidToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tobiasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pabloToolStripMenuItem;
    }
}

