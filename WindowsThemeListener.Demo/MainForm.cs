using System;
using System.Drawing;
using System.Windows.Forms;

using Transitions;
using WK.Libraries.WTL;

namespace Library.Demo
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            UpdateThemeInfo();

            ThemeListener.ThemeOptionsChanged += OnThemeOptionsChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the system theme information.
        /// </summary>
        public void UpdateThemeInfo()
        {
            lblWindowsModeVal.Text = ThemeListener.WindowsMode.ToString();
            lblAppModeVal.Text = ThemeListener.AppMode.ToString();
            lblAccentColorVal.Text = ThemeListener.AccentColor.ToString();
            lblAccentForeColorVal.Text = ThemeListener.AccentForeColor.ToString();
            lblTransparencyVal.Text = ThemeListener.TransparencyEnabled ? "Enabled" : "Disabled";

            TransitionColors(ThemeListener.AccentColor, ThemeListener.AccentForeColor);
        }

        /// <summary>
        /// Transitions the visual elements' colors to any defined color.
        /// </summary>
        public void TransitionColors(Color backColor, Color foreColor, int transitionTime = 200)
        {
            Transition.run(this, "BackColor", backColor, new TransitionType_EaseInEaseOut(transitionTime));
            Transition labelTransitions = new Transition(new TransitionType_EaseInEaseOut(transitionTime));

            labelTransitions.add(lblEnabled, "ForeColor", foreColor);
            labelTransitions.add(lblTitle, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColorVal, "ForeColor", foreColor);
            labelTransitions.add(lblAppMode, "ForeColor", foreColor);
            labelTransitions.add(lblAppModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsMode, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColorVal, "ForeColor", foreColor);
            labelTransitions.add(lblTransparency, "ForeColor", foreColor);
            labelTransitions.add(lblTransparencyVal, "ForeColor", foreColor);

            labelTransitions.run();
        }

        #endregion

        #region Events

        private void OnThemeOptionsChanged(object sender, ThemeListener.ThemeOptionsChangedEventArgs e)
        {
            if (e.OptionsChanged.Contains(ThemeListener.ThemeOptions.WindowsMode))
                lblWindowsModeVal.Text = e.WindowsMode.ToString();

            if (e.OptionsChanged.Contains(ThemeListener.ThemeOptions.AppMode))
                lblAppModeVal.Text = e.AppMode.ToString();

            if (e.OptionsChanged.Contains(ThemeListener.ThemeOptions.Transparency))
                lblTransparencyVal.Text = e.TransparencyEnabled ? "Enabled" : "Disabled";

            if (e.OptionsChanged.Contains(ThemeListener.ThemeOptions.AccentColor))
            {
                lblAccentColorVal.Text = e.AccentColor.ToString();
                TransitionColors(e.AccentColor, e.AccentForeColor);
            }
        }

        private void themeSwitch_CheckedChanged(object sender, EventArgs e)
        {
            ThemeListener.Enabled = tsEnable.Checked;

            if (tsEnable.Checked)
                lblEnabled.Text = "Enabled";
            else
                lblEnabled.Text = "Disabled";
        }

        private void lblEnabled_Click(object sender, EventArgs e)
        {
            tsEnable.Checked = !tsEnable.Checked;
        }

        #endregion
    }
}