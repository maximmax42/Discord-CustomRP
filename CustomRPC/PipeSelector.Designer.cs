
namespace CustomRPC
{
    partial class PipeSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PipeSelector));
            this.numericUpDownPipe = new System.Windows.Forms.NumericUpDown();
            this.labelDefault = new System.Windows.Forms.Label();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.labelUsername = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.labelPipe = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownPipe
            // 
            resources.ApplyResources(this.numericUpDownPipe, "numericUpDownPipe");
            this.numericUpDownPipe.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "pipe", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.numericUpDownPipe.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.numericUpDownPipe.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numericUpDownPipe.Name = "numericUpDownPipe";
            this.numericUpDownPipe.ReadOnly = true;
            this.numericUpDownPipe.Value = global::CustomRPC.Properties.Settings.Default.pipe;
            this.numericUpDownPipe.ValueChanged += new System.EventHandler(this.PipeChanged);
            // 
            // labelDefault
            // 
            resources.ApplyResources(this.labelDefault, "labelDefault");
            this.labelDefault.Name = "labelDefault";
            // 
            // pictureBoxAvatar
            // 
            resources.ApplyResources(this.pictureBoxAvatar, "pictureBoxAvatar");
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.TabStop = false;
            // 
            // labelUsername
            // 
            resources.ApplyResources(this.labelUsername, "labelUsername");
            this.labelUsername.Name = "labelUsername";
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(25)))), ((int)(((byte)(28)))));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(73)))), ((int)(((byte)(162)))));
            this.buttonOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(72)))), ((int)(((byte)(86)))), ((int)(((byte)(193)))));
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // labelPipe
            // 
            resources.ApplyResources(this.labelPipe, "labelPipe");
            this.labelPipe.Name = "labelPipe";
            // 
            // PipeSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.buttonOK;
            this.Controls.Add(this.labelPipe);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelUsername);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.labelDefault);
            this.Controls.Add(this.numericUpDownPipe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PipeSelector";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DisposeClient);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPipe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownPipe;
        private System.Windows.Forms.Label labelDefault;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Label labelUsername;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelPipe;
    }
}