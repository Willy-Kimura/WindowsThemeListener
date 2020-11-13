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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblWindowsMode = new System.Windows.Forms.Label();
            this.lblAppMode = new System.Windows.Forms.Label();
            this.lblAccentColor = new System.Windows.Forms.Label();
            this.lblAccentColorVal = new System.Windows.Forms.Label();
            this.lblAppModeVal = new System.Windows.Forms.Label();
            this.lblWindowsModeVal = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblAccentForeColorVal = new System.Windows.Forms.Label();
            this.lblAccentForeColor = new System.Windows.Forms.Label();
            this.lblEnabled = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tsEnable = new WK.Libraries.WTL.Controls.ThemeSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWindowsMode
            // 
            this.lblWindowsMode.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWindowsMode.AutoSize = true;
            this.lblWindowsMode.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblWindowsMode.ForeColor = System.Drawing.Color.Black;
            this.lblWindowsMode.Location = new System.Drawing.Point(152, 223);
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
            this.lblAppMode.Location = new System.Drawing.Point(152, 242);
            this.lblAppMode.Name = "lblAppMode";
            this.lblAppMode.Size = new System.Drawing.Size(66, 15);
            this.lblAppMode.TabIndex = 1;
            this.lblAppMode.Text = "App Mode:";
            // 
            // lblAccentColor
            // 
            this.lblAccentColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccentColor.AutoSize = true;
            this.lblAccentColor.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAccentColor.ForeColor = System.Drawing.Color.Black;
            this.lblAccentColor.Location = new System.Drawing.Point(152, 261);
            this.lblAccentColor.Name = "lblAccentColor";
            this.lblAccentColor.Size = new System.Drawing.Size(78, 15);
            this.lblAccentColor.TabIndex = 2;
            this.lblAccentColor.Text = "Accent Color:";
            // 
            // lblAccentColorVal
            // 
            this.lblAccentColorVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccentColorVal.AutoSize = true;
            this.lblAccentColorVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAccentColorVal.ForeColor = System.Drawing.Color.Black;
            this.lblAccentColorVal.Location = new System.Drawing.Point(255, 261);
            this.lblAccentColorVal.Name = "lblAccentColorVal";
            this.lblAccentColorVal.Size = new System.Drawing.Size(43, 15);
            this.lblAccentColorVal.TabIndex = 5;
            this.lblAccentColorVal.Text = "{value}";
            // 
            // lblAppModeVal
            // 
            this.lblAppModeVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAppModeVal.AutoSize = true;
            this.lblAppModeVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAppModeVal.ForeColor = System.Drawing.Color.Black;
            this.lblAppModeVal.Location = new System.Drawing.Point(255, 242);
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
            this.lblWindowsModeVal.Location = new System.Drawing.Point(255, 223);
            this.lblWindowsModeVal.Name = "lblWindowsModeVal";
            this.lblWindowsModeVal.Size = new System.Drawing.Size(43, 15);
            this.lblWindowsModeVal.TabIndex = 3;
            this.lblWindowsModeVal.Text = "{value}";
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(148, 178);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(265, 32);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Personalization Listeners";
            // 
            // lblAccentForeColorVal
            // 
            this.lblAccentForeColorVal.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccentForeColorVal.AutoSize = true;
            this.lblAccentForeColorVal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAccentForeColorVal.ForeColor = System.Drawing.Color.Black;
            this.lblAccentForeColorVal.Location = new System.Drawing.Point(255, 280);
            this.lblAccentForeColorVal.Name = "lblAccentForeColorVal";
            this.lblAccentForeColorVal.Size = new System.Drawing.Size(43, 15);
            this.lblAccentForeColorVal.TabIndex = 8;
            this.lblAccentForeColorVal.Text = "{value}";
            // 
            // lblAccentForeColor
            // 
            this.lblAccentForeColor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAccentForeColor.AutoSize = true;
            this.lblAccentForeColor.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblAccentForeColor.ForeColor = System.Drawing.Color.Black;
            this.lblAccentForeColor.Location = new System.Drawing.Point(152, 280);
            this.lblAccentForeColor.Name = "lblAccentForeColor";
            this.lblAccentForeColor.Size = new System.Drawing.Size(101, 15);
            this.lblAccentForeColor.TabIndex = 7;
            this.lblAccentForeColor.Text = "Accent ForeColor:";
            // 
            // lblEnabled
            // 
            this.lblEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnabled.AutoSize = true;
            this.lblEnabled.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnabled.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblEnabled.ForeColor = System.Drawing.Color.Black;
            this.lblEnabled.Location = new System.Drawing.Point(506, 12);
            this.lblEnabled.Name = "lblEnabled";
            this.lblEnabled.Size = new System.Drawing.Size(49, 15);
            this.lblEnabled.TabIndex = 10;
            this.lblEnabled.Text = "Enabled";
            this.toolTip.SetToolTip(this.lblEnabled, "Enable/disable Theme Listener");
            this.lblEnabled.Click += new System.EventHandler(this.lblEnabled_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(154, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(92, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // tsEnable
            // 
            this.tsEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tsEnable.BackColor = System.Drawing.Color.Transparent;
            this.tsEnable.Checked = true;
            this.tsEnable.CheckedColor = System.Drawing.Color.DodgerBlue;
            this.tsEnable.CheckedForeColor = System.Drawing.Color.White;
            this.tsEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tsEnable.Location = new System.Drawing.Point(468, 10);
            this.tsEnable.Name = "tsEnable";
            this.tsEnable.Size = new System.Drawing.Size(34, 19);
            this.tsEnable.TabIndex = 9;
            this.tsEnable.Texts = null;
            this.toolTip.SetToolTip(this.tsEnable, "Enable/disable Theme Listener");
            this.tsEnable.Type = WK.Libraries.WTL.Controls.ThemeSwitch.SwitchTypes.Round;
            this.tsEnable.UncheckedColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(189)))), ((int)(((byte)(189)))));
            this.tsEnable.UncheckedForeColor = System.Drawing.Color.White;
            this.tsEnable.CheckedChanged += new System.EventHandler(this.themeSwitch_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(568, 381);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblEnabled);
            this.Controls.Add(this.tsEnable);
            this.Controls.Add(this.lblAccentForeColorVal);
            this.Controls.Add(this.lblAccentForeColor);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblAccentColorVal);
            this.Controls.Add(this.lblAppModeVal);
            this.Controls.Add(this.lblWindowsModeVal);
            this.Controls.Add(this.lblAccentColor);
            this.Controls.Add(this.lblAppMode);
            this.Controls.Add(this.lblWindowsMode);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = System.Drawing.Color.Black;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Windows Theme Listener [Demo]";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWindowsMode;
        private System.Windows.Forms.Label lblAppMode;
        private System.Windows.Forms.Label lblAccentColor;
        private System.Windows.Forms.Label lblAccentColorVal;
        private System.Windows.Forms.Label lblAppModeVal;
        private System.Windows.Forms.Label lblWindowsModeVal;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAccentForeColorVal;
        private System.Windows.Forms.Label lblAccentForeColor;
        private WK.Libraries.WTL.Controls.ThemeSwitch tsEnable;
        private System.Windows.Forms.Label lblEnabled;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

