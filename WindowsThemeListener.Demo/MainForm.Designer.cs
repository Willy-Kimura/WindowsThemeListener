namespace Library.Demo
{
    partial class MainForm
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
            this.lblWindowsMode = new System.Windows.Forms.Label();
            this.lblAppMode = new System.Windows.Forms.Label();
            this.lblAAccentColor = new System.Windows.Forms.Label();
            this.lblAAccentColorVal = new System.Windows.Forms.Label();
            this.lblAppModeVal = new System.Windows.Forms.Label();
            this.lblWindowsModeVal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWindowsMode
            // 
            this.lblWindowsMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWindowsMode.AutoSize = true;
            this.lblWindowsMode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblWindowsMode.ForeColor = System.Drawing.Color.Black;
            this.lblWindowsMode.Location = new System.Drawing.Point(242, 197);
            this.lblWindowsMode.Name = "lblWindowsMode";
            this.lblWindowsMode.Size = new System.Drawing.Size(94, 15);
            this.lblWindowsMode.TabIndex = 0;
            this.lblWindowsMode.Text = "Windows Mode:";
            // 
            // lblAppMode
            // 
            this.lblAppMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAppMode.AutoSize = true;
            this.lblAppMode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAppMode.ForeColor = System.Drawing.Color.Black;
            this.lblAppMode.Location = new System.Drawing.Point(242, 216);
            this.lblAppMode.Name = "lblAppMode";
            this.lblAppMode.Size = new System.Drawing.Size(66, 15);
            this.lblAppMode.TabIndex = 1;
            this.lblAppMode.Text = "App Mode:";
            // 
            // lblAAccentColor
            // 
            this.lblAAccentColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAAccentColor.AutoSize = true;
            this.lblAAccentColor.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAAccentColor.ForeColor = System.Drawing.Color.Black;
            this.lblAAccentColor.Location = new System.Drawing.Point(242, 235);
            this.lblAAccentColor.Name = "lblAAccentColor";
            this.lblAAccentColor.Size = new System.Drawing.Size(78, 15);
            this.lblAAccentColor.TabIndex = 2;
            this.lblAAccentColor.Text = "Accent Color:";
            // 
            // lblAAccentColorVal
            // 
            this.lblAAccentColorVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAAccentColorVal.AutoSize = true;
            this.lblAAccentColorVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAAccentColorVal.ForeColor = System.Drawing.Color.Black;
            this.lblAAccentColorVal.Location = new System.Drawing.Point(337, 235);
            this.lblAAccentColorVal.Name = "lblAAccentColorVal";
            this.lblAAccentColorVal.Size = new System.Drawing.Size(43, 15);
            this.lblAAccentColorVal.TabIndex = 5;
            this.lblAAccentColorVal.Text = "{value}";
            // 
            // lblAppModeVal
            // 
            this.lblAppModeVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAppModeVal.AutoSize = true;
            this.lblAppModeVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAppModeVal.ForeColor = System.Drawing.Color.Black;
            this.lblAppModeVal.Location = new System.Drawing.Point(337, 216);
            this.lblAppModeVal.Name = "lblAppModeVal";
            this.lblAppModeVal.Size = new System.Drawing.Size(43, 15);
            this.lblAppModeVal.TabIndex = 4;
            this.lblAppModeVal.Text = "{value}";
            // 
            // lblWindowsModeVal
            // 
            this.lblWindowsModeVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWindowsModeVal.AutoSize = true;
            this.lblWindowsModeVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblWindowsModeVal.ForeColor = System.Drawing.Color.Black;
            this.lblWindowsModeVal.Location = new System.Drawing.Point(337, 197);
            this.lblWindowsModeVal.Name = "lblWindowsModeVal";
            this.lblWindowsModeVal.Size = new System.Drawing.Size(43, 15);
            this.lblWindowsModeVal.TabIndex = 3;
            this.lblWindowsModeVal.Text = "{value}";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semilight", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(238, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(265, 32);
            this.label1.TabIndex = 6;
            this.label1.Text = "Personalization Listeners";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(732, 435);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAAccentColorVal);
            this.Controls.Add(this.lblAppModeVal);
            this.Controls.Add(this.lblWindowsModeVal);
            this.Controls.Add(this.lblAAccentColor);
            this.Controls.Add(this.lblAppMode);
            this.Controls.Add(this.lblWindowsMode);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Theme Listener [Demo]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWindowsMode;
        private System.Windows.Forms.Label lblAppMode;
        private System.Windows.Forms.Label lblAAccentColor;
        private System.Windows.Forms.Label lblAAccentColorVal;
        private System.Windows.Forms.Label lblAppModeVal;
        private System.Windows.Forms.Label lblWindowsModeVal;
        private System.Windows.Forms.Label label1;
    }
}

