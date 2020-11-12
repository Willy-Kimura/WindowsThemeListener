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

            ThemeListener.ThemeChanged += OnWindowsThemeChanged;
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
            lblAAccentColorVal.Text = ThemeListener.AccentColor.ToString();

            TransitionColor(ThemeListener.AccentColor);
        }

        /// <summary>
        /// Transitions the form's background color to another.
        /// </summary>
        /// <param name="color"></param>
        public void TransitionColor(Color color)
        {
            Transition.run(this, "BackColor", color, new TransitionType_EaseInEaseOut(330));
        }

        #endregion

        #region Events

        private void OnWindowsThemeChanged(object sender, ThemeListener.ThemeChangedEventArgs e)
        {
            TransitionColor(e.NewAccentColor);

            lblWindowsModeVal.Text = e.NewWindowsMode.ToString();
            lblAppMode.Text = e.NewAppMode.ToString();
            lblAAccentColorVal.Text = e.NewAccentColor.ToString();
        }

        #endregion
    }
}