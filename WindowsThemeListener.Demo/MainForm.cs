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
            lblAccentColorVal.Text = ThemeListener.AccentColor.ToString();
            lblAccentForeColorVal.Text = ThemeListener.AccentForeColor.ToString();

            TransitionColors(ThemeListener.AccentColor, ThemeListener.AccentForeColor);
        }

        /// <summary>
        /// Transitions the form's background color to another.
        /// </summary>
        /// <param name="color"></param>
        public void TransitionColors(Color backColor, Color foreColor)
        {
            Transition.run(this, "BackColor", backColor, new TransitionType_EaseInEaseOut(330));
            Transition labelTransitions = new Transition(new TransitionType_EaseInEaseOut(330));

            labelTransitions.add(lblTitle, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColorVal, "ForeColor", foreColor);
            labelTransitions.add(lblAppMode, "ForeColor", foreColor);
            labelTransitions.add(lblAppModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsMode, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColorVal, "ForeColor", foreColor);

            labelTransitions.run();
        }

        #endregion

        #region Events

        private void OnWindowsThemeChanged(object sender, ThemeListener.ThemeChangedEventArgs e)
        {
            TransitionColors(e.NewAccentColor, e.NewAccentForeColor);

            lblWindowsModeVal.Text = e.NewWindowsMode.ToString();
            lblAppModeVal.Text = e.NewAppMode.ToString();
            lblAccentColorVal.Text = e.NewAccentColor.ToString();
            lblAccentForeColorVal.Text = e.NewAccentForeColor.ToString();
        }

        #endregion
    }
}