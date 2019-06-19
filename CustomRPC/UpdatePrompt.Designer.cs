namespace CustomRPC
{
    partial class UpdatePrompt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdatePrompt));
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonNotNow = new System.Windows.Forms.Button();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.checkBoxNeverNotify = new System.Windows.Forms.CheckBox();
            this.labelVersionsText = new System.Windows.Forms.Label();
            this.labelVersions = new System.Windows.Forms.Label();
            this.buttonSkipUpdate = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.AutoEllipsis = true;
            this.buttonUpdate.DialogResult = System.Windows.Forms.DialogResult.Yes;
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            // 
            // pictureBoxLogo
            // 
            this.pictureBoxLogo.Image = global::CustomRPC.Properties.Resources.logo;
            resources.ApplyResources(this.pictureBoxLogo, "pictureBoxLogo");
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonNotNow
            // 
            this.buttonNotNow.AutoEllipsis = true;
            this.buttonNotNow.DialogResult = System.Windows.Forms.DialogResult.No;
            resources.ApplyResources(this.buttonNotNow, "buttonNotNow");
            this.buttonNotNow.Name = "buttonNotNow";
            this.buttonNotNow.UseVisualStyleBackColor = true;
            // 
            // labelQuestion
            // 
            resources.ApplyResources(this.labelQuestion, "labelQuestion");
            this.labelQuestion.Name = "labelQuestion";
            // 
            // checkBoxNeverNotify
            // 
            resources.ApplyResources(this.checkBoxNeverNotify, "checkBoxNeverNotify");
            this.checkBoxNeverNotify.Name = "checkBoxNeverNotify";
            this.checkBoxNeverNotify.UseVisualStyleBackColor = true;
            this.checkBoxNeverNotify.CheckedChanged += new System.EventHandler(this.ToggleUpdates);
            // 
            // labelVersionsText
            // 
            resources.ApplyResources(this.labelVersionsText, "labelVersionsText");
            this.labelVersionsText.Name = "labelVersionsText";
            // 
            // labelVersions
            // 
            resources.ApplyResources(this.labelVersions, "labelVersions");
            this.labelVersions.Name = "labelVersions";
            // 
            // buttonSkipUpdate
            // 
            this.buttonSkipUpdate.AutoEllipsis = true;
            this.buttonSkipUpdate.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            resources.ApplyResources(this.buttonSkipUpdate, "buttonSkipUpdate");
            this.buttonSkipUpdate.Name = "buttonSkipUpdate";
            this.buttonSkipUpdate.UseVisualStyleBackColor = true;
            // 
            // UpdatePrompt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.buttonSkipUpdate);
            this.Controls.Add(this.labelVersions);
            this.Controls.Add(this.labelVersionsText);
            this.Controls.Add(this.checkBoxNeverNotify);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.buttonNotNow);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.buttonUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePrompt";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonNotNow;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.CheckBox checkBoxNeverNotify;
        private System.Windows.Forms.Label labelVersionsText;
        private System.Windows.Forms.Label labelVersions;
        private System.Windows.Forms.Button buttonSkipUpdate;
    }
}