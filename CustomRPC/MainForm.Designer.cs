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
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.trayMenuReconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trayMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.linkLabelDeveloperConsole = new System.Windows.Forms.LinkLabel();
            this.toolTipInfo = new System.Windows.Forms.ToolTip(this.components);
            this.trayMenuStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "Double click on the icon to show it again, or right-click to quit.";
            this.trayIcon.BalloonTipTitle = "The app will run in the tray.";
            this.trayIcon.ContextMenuStrip = this.trayMenuStrip;
            this.trayIcon.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.trayIcon.Text = "Custom RPC";
            this.trayIcon.Visible = true;
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
            this.trayMenuStrip.Size = new System.Drawing.Size(131, 54);
            // 
            // trayMenuReconnect
            // 
            this.trayMenuReconnect.Name = "trayMenuReconnect";
            this.trayMenuReconnect.Size = new System.Drawing.Size(130, 22);
            this.trayMenuReconnect.Text = "Reconnect";
            this.trayMenuReconnect.Click += new System.EventHandler(this.Reconnect);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(127, 6);
            // 
            // trayMenuQuit
            // 
            this.trayMenuQuit.Name = "trayMenuQuit";
            this.trayMenuQuit.Size = new System.Drawing.Size(130, 22);
            this.trayMenuQuit.Text = "Quit";
            this.trayMenuQuit.Click += new System.EventHandler(this.Quit);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(467, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.Quit);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runOnStartupToolStripMenuItem,
            this.startMinimizedToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // runOnStartupToolStripMenuItem
            // 
            this.runOnStartupToolStripMenuItem.CheckOnClick = true;
            this.runOnStartupToolStripMenuItem.Name = "runOnStartupToolStripMenuItem";
            this.runOnStartupToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.runOnStartupToolStripMenuItem.Text = "Run on Startup";
            this.runOnStartupToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // startMinimizedToolStripMenuItem
            // 
            this.startMinimizedToolStripMenuItem.CheckOnClick = true;
            this.startMinimizedToolStripMenuItem.Name = "startMinimizedToolStripMenuItem";
            this.startMinimizedToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.startMinimizedToolStripMenuItem.Text = "Start Minimized";
            this.startMinimizedToolStripMenuItem.CheckedChanged += new System.EventHandler(this.SaveSettings);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.ShowAbout);
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(71, 51);
            this.textBoxID.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxID.MaxLength = 18;
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(139, 22);
            this.textBoxID.TabIndex = 2;
            this.textBoxID.TextChanged += new System.EventHandler(this.OnlyNumbers);
            // 
            // textBoxDetails
            // 
            this.textBoxDetails.Location = new System.Drawing.Point(71, 83);
            this.textBoxDetails.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxDetails.Name = "textBoxDetails";
            this.textBoxDetails.Size = new System.Drawing.Size(345, 22);
            this.textBoxDetails.TabIndex = 3;
            // 
            // textBoxState
            // 
            this.textBoxState.Location = new System.Drawing.Point(71, 111);
            this.textBoxState.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxState.Name = "textBoxState";
            this.textBoxState.Size = new System.Drawing.Size(345, 22);
            this.textBoxState.TabIndex = 4;
            // 
            // textBoxSmallKey
            // 
            this.textBoxSmallKey.Location = new System.Drawing.Point(71, 191);
            this.textBoxSmallKey.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxSmallKey.Name = "textBoxSmallKey";
            this.textBoxSmallKey.Size = new System.Drawing.Size(150, 22);
            this.textBoxSmallKey.TabIndex = 5;
            // 
            // textBoxSmallText
            // 
            this.textBoxSmallText.Location = new System.Drawing.Point(71, 221);
            this.textBoxSmallText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxSmallText.Name = "textBoxSmallText";
            this.textBoxSmallText.Size = new System.Drawing.Size(150, 22);
            this.textBoxSmallText.TabIndex = 6;
            // 
            // textBoxLargeKey
            // 
            this.textBoxLargeKey.Location = new System.Drawing.Point(266, 191);
            this.textBoxLargeKey.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxLargeKey.Name = "textBoxLargeKey";
            this.textBoxLargeKey.Size = new System.Drawing.Size(150, 22);
            this.textBoxLargeKey.TabIndex = 7;
            // 
            // textBoxLargeText
            // 
            this.textBoxLargeText.Location = new System.Drawing.Point(266, 221);
            this.textBoxLargeText.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.textBoxLargeText.Name = "textBoxLargeText";
            this.textBoxLargeText.Size = new System.Drawing.Size(150, 22);
            this.textBoxLargeText.TabIndex = 8;
            // 
            // buttonUpdatePresence
            // 
            this.buttonUpdatePresence.Location = new System.Drawing.Point(173, 266);
            this.buttonUpdatePresence.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.buttonUpdatePresence.Name = "buttonUpdatePresence";
            this.buttonUpdatePresence.Size = new System.Drawing.Size(113, 24);
            this.buttonUpdatePresence.TabIndex = 9;
            this.buttonUpdatePresence.Text = "Update Presence";
            this.toolTipInfo.SetToolTip(this.buttonUpdatePresence, "Updates presence.");
            this.buttonUpdatePresence.UseVisualStyleBackColor = true;
            this.buttonUpdatePresence.Click += new System.EventHandler(this.Update);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(234, 49);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(79, 24);
            this.buttonConnect.TabIndex = 10;
            this.buttonConnect.Text = "Connect";
            this.toolTipInfo.SetToolTip(this.buttonConnect, "Links the application to the specified ID with set presence.");
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.Connect);
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(337, 49);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(79, 24);
            this.buttonDisconnect.TabIndex = 11;
            this.buttonDisconnect.Text = "Disconnect";
            this.toolTipInfo.SetToolTip(this.buttonDisconnect, "Disconnects the app from Discord. Please note that it might take a while.");
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.Disconnect);
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(47, 54);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(19, 14);
            this.labelID.TabIndex = 12;
            this.labelID.Text = "ID";
            this.toolTipInfo.SetToolTip(this.labelID, "ID of the application from Discord Developers console.");
            // 
            // labelDetails
            // 
            this.labelDetails.AutoSize = true;
            this.labelDetails.Location = new System.Drawing.Point(24, 86);
            this.labelDetails.Name = "labelDetails";
            this.labelDetails.Size = new System.Drawing.Size(42, 14);
            this.labelDetails.TabIndex = 13;
            this.labelDetails.Text = "Details";
            this.toolTipInfo.SetToolTip(this.labelDetails, "First line in the presence.");
            // 
            // labelState
            // 
            this.labelState.AutoSize = true;
            this.labelState.Location = new System.Drawing.Point(29, 114);
            this.labelState.Name = "labelState";
            this.labelState.Size = new System.Drawing.Size(37, 14);
            this.labelState.TabIndex = 14;
            this.labelState.Text = "State";
            this.toolTipInfo.SetToolTip(this.labelState, "Second line in the presence.");
            // 
            // labelSmallKey
            // 
            this.labelSmallKey.AutoSize = true;
            this.labelSmallKey.Location = new System.Drawing.Point(39, 194);
            this.labelSmallKey.Name = "labelSmallKey";
            this.labelSmallKey.Size = new System.Drawing.Size(27, 14);
            this.labelSmallKey.TabIndex = 15;
            this.labelSmallKey.Text = "Key";
            this.toolTipInfo.SetToolTip(this.labelSmallKey, "Name of the asset for small image.");
            // 
            // labelSmallText
            // 
            this.labelSmallText.AutoSize = true;
            this.labelSmallText.Location = new System.Drawing.Point(33, 224);
            this.labelSmallText.Name = "labelSmallText";
            this.labelSmallText.Size = new System.Drawing.Size(33, 14);
            this.labelSmallText.TabIndex = 17;
            this.labelSmallText.Text = "Text";
            this.toolTipInfo.SetToolTip(this.labelSmallText, "Text displayed when hovering the small image.");
            // 
            // labelLargeText
            // 
            this.labelLargeText.AutoSize = true;
            this.labelLargeText.Location = new System.Drawing.Point(228, 224);
            this.labelLargeText.Name = "labelLargeText";
            this.labelLargeText.Size = new System.Drawing.Size(33, 14);
            this.labelLargeText.TabIndex = 19;
            this.labelLargeText.Text = "Text";
            this.toolTipInfo.SetToolTip(this.labelLargeText, "Text displayed when hovering the large image.");
            // 
            // labelLargeKey
            // 
            this.labelLargeKey.AutoSize = true;
            this.labelLargeKey.Location = new System.Drawing.Point(234, 194);
            this.labelLargeKey.Name = "labelLargeKey";
            this.labelLargeKey.Size = new System.Drawing.Size(27, 14);
            this.labelLargeKey.TabIndex = 18;
            this.labelLargeKey.Text = "Key";
            this.toolTipInfo.SetToolTip(this.labelLargeKey, "Name of the asset for large image.");
            // 
            // labelSmall
            // 
            this.labelSmall.AutoSize = true;
            this.labelSmall.Location = new System.Drawing.Point(99, 163);
            this.labelSmall.Name = "labelSmall";
            this.labelSmall.Size = new System.Drawing.Size(72, 14);
            this.labelSmall.TabIndex = 20;
            this.labelSmall.Text = "Small Image";
            this.toolTipInfo.SetToolTip(this.labelSmall, "Image that shows up in the down right corner of large image.");
            // 
            // labelLarge
            // 
            this.labelLarge.AutoSize = true;
            this.labelLarge.Location = new System.Drawing.Point(291, 163);
            this.labelLarge.Name = "labelLarge";
            this.labelLarge.Size = new System.Drawing.Size(75, 14);
            this.labelLarge.TabIndex = 21;
            this.labelLarge.Text = "Large Image";
            this.toolTipInfo.SetToolTip(this.labelLarge, "Image that shows left to the text set above.");
            // 
            // linkLabelDeveloperConsole
            // 
            this.linkLabelDeveloperConsole.AutoSize = true;
            this.linkLabelDeveloperConsole.Location = new System.Drawing.Point(193, 163);
            this.linkLabelDeveloperConsole.Name = "linkLabelDeveloperConsole";
            this.linkLabelDeveloperConsole.Size = new System.Drawing.Size(83, 14);
            this.linkLabelDeveloperConsole.TabIndex = 22;
            this.linkLabelDeveloperConsole.TabStop = true;
            this.linkLabelDeveloperConsole.Text = "Upload Assets";
            this.toolTipInfo.SetToolTip(this.linkLabelDeveloperConsole, "Takes you to the Discord Developers site.");
            this.linkLabelDeveloperConsole.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OpenDiscordSite);
            // 
            // toolTipInfo
            // 
            this.toolTipInfo.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTipInfo.ToolTipTitle = "Information";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 309);
            this.Controls.Add(this.linkLabelDeveloperConsole);
            this.Controls.Add(this.labelLarge);
            this.Controls.Add(this.labelSmall);
            this.Controls.Add(this.labelLargeText);
            this.Controls.Add(this.labelLargeKey);
            this.Controls.Add(this.labelSmallText);
            this.Controls.Add(this.labelSmallKey);
            this.Controls.Add(this.labelState);
            this.Controls.Add(this.labelDetails);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonUpdatePresence);
            this.Controls.Add(this.textBoxLargeText);
            this.Controls.Add(this.textBoxLargeKey);
            this.Controls.Add(this.textBoxSmallText);
            this.Controls.Add(this.textBoxSmallKey);
            this.Controls.Add(this.textBoxState);
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CustomRPC.Properties.Resources.favicon;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Custom RP";
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
        private System.Windows.Forms.LinkLabel linkLabelDeveloperConsole;
        private System.Windows.Forms.ToolTip toolTipInfo;
    }
}

