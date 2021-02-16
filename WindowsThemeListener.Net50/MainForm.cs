using System;
using System.Drawing;
using System.Windows.Forms;

using WK.Libraries.WTL;

namespace WindowsThemeListener.Net50
{
    public partial class MainForm : Form
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            UpdateThemeInfo();

            ThemeListener.ThemeSettingsChanged += OnThemeSettingsChanged;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the system theme information.
        /// </summary>
        public void UpdateThemeInfo()
        {
            lblAppModeVal.Text = ThemeListener.WindowsMode.ToString();
            lblWindowsModeVal.Text = ThemeListener.AppMode.ToString();
            lblAccentColorVal.Text = ThemeListener.AccentColor.ToString();
            lblAccentForeColorVal.Text = ThemeListener.AccentForeColor.ToString();
            lblTransparencyVal.Text = ThemeListener.TransparencyEnabled ? "Enabled" : "Disabled";

            TransitionColors(ThemeListener.AccentColor, ThemeListener.AccentForeColor);
        }

        /// <summary>
        /// Transitions the visual elements' colors to any defined color.
        /// </summary>
        public void TransitionColors(Color backColor, Color foreColor)
        {
            BackColor = backColor;
            lblTitle.ForeColor = foreColor;
            lblAccentColor.ForeColor = foreColor;
            lblAccentColorVal.ForeColor = foreColor;
            lblWindowsMode.ForeColor = foreColor;
            lblWindowsModeVal.ForeColor = foreColor;
            lblAppMode.ForeColor = foreColor;
            lblAppModeVal.ForeColor = foreColor;
            lblAccentForeColor.ForeColor = foreColor;
            lblAccentForeColorVal.ForeColor = foreColor;
            lblTransparency.ForeColor = foreColor;
            lblTransparencyVal.ForeColor = foreColor;
        }

        #endregion

        #region Events

        private void OnThemeSettingsChanged(object sender, ThemeListener.ThemeSettingsChangedEventArgs e)
        {
            if (e.SettingsChanged.Contains(ThemeListener.ThemeSettings.WindowsMode))
            {
                lblAppModeVal.Text = e.WindowsMode.ToString();

                if (e.WindowsMode == ThemeListener.ThemeModes.Light)
                    TransitionColors(Color.White, Color.Black);
                else if (e.WindowsMode == ThemeListener.ThemeModes.Dark)
                    TransitionColors(Color.Black, Color.White);
            }

            if (e.SettingsChanged.Contains(ThemeListener.ThemeSettings.AppMode))
            {
                lblWindowsModeVal.Text = e.AppMode.ToString();

                if (e.AppMode == ThemeListener.ThemeModes.Light)
                    TransitionColors(Color.White, Color.Black);
                else if (e.AppMode == ThemeListener.ThemeModes.Dark)
                    TransitionColors(Color.Black, Color.White);
            }

            if (e.SettingsChanged.Contains(ThemeListener.ThemeSettings.Transparency))
                lblTransparencyVal.Text = e.TransparencyEnabled ? "Enabled" : "Disabled";

            if (e.SettingsChanged.Contains(ThemeListener.ThemeSettings.AccentColor))
            {
                lblAccentColorVal.Text = e.AccentColor.ToString();
                TransitionColors(e.AccentColor, e.AccentForeColor);
            }
        }

        #endregion
    }
}