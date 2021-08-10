
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
            this.pipeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pipeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pipeNumericUpDown
            // 
            resources.ApplyResources(this.pipeNumericUpDown, "pipeNumericUpDown");
            this.pipeNumericUpDown.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::CustomRPC.Properties.Settings.Default, "pipe", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pipeNumericUpDown.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.pipeNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.pipeNumericUpDown.Name = "pipeNumericUpDown";
            this.pipeNumericUpDown.Value = global::CustomRPC.Properties.Settings.Default.pipe;
            // 
            // PipeSelector
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pipeNumericUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PipeSelector";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.pipeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown pipeNumericUpDown;
    }
}