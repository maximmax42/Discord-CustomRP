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
            this.buttonYes = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.buttonNo = new System.Windows.Forms.Button();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.checkBoxNeverNotify = new System.Windows.Forms.CheckBox();
            this.labelVersionsText = new System.Windows.Forms.Label();
            this.labelVersions = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonYes
            // 
            resources.ApplyResources(this.buttonYes, "buttonYes");
            this.buttonYes.AutoEllipsis = true;
            this.buttonYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonYes.Name = "buttonYes";
            this.buttonYes.UseVisualStyleBackColor = true;
            // 
            // pictureBoxLogo
            // 
            resources.ApplyResources(this.pictureBoxLogo, "pictureBoxLogo");
            this.pictureBoxLogo.Image = global::CustomRPC.Properties.Resources.logo;
            this.pictureBoxLogo.Name = "pictureBoxLogo";
            this.pictureBoxLogo.TabStop = false;
            // 
            // buttonNo
            // 
            resources.ApplyResources(this.buttonNo, "buttonNo");
            this.buttonNo.AutoEllipsis = true;
            this.buttonNo.DialogResult = System.Windows.Forms.DialogResult.No;
            this.buttonNo.Name = "buttonNo";
            this.buttonNo.UseVisualStyleBackColor = true;
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
            // UpdatePrompt
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.labelVersions);
            this.Controls.Add(this.labelVersionsText);
            this.Controls.Add(this.checkBoxNeverNotify);
            this.Controls.Add(this.labelQuestion);
            this.Controls.Add(this.buttonNo);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.buttonYes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdatePrompt";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.PictureBox pictureBoxLogo;
        private System.Windows.Forms.Button buttonNo;
        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.CheckBox checkBoxNeverNotify;
        private System.Windows.Forms.Label labelVersionsText;
        private System.Windows.Forms.Label labelVersions;
    }
}