﻿using System;
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
        public void TransitionColors(Color backColor, Color foreColor, int transitionTime = 200)
        {
            Transition.run(this, "BackColor", backColor, new TransitionType_EaseInEaseOut(transitionTime));
            Transition labelTransitions = new Transition(new TransitionType_EaseInEaseOut(transitionTime));

            labelTransitions.add(lblEnabled, "ForeColor", foreColor);
            labelTransitions.add(lblTitle, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentColorVal, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsMode, "ForeColor", foreColor);
            labelTransitions.add(lblWindowsModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblAppMode, "ForeColor", foreColor);
            labelTransitions.add(lblAppModeVal, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColor, "ForeColor", foreColor);
            labelTransitions.add(lblAccentForeColorVal, "ForeColor", foreColor);
            labelTransitions.add(lblTransparency, "ForeColor", foreColor);
            labelTransitions.add(lblTransparencyVal, "ForeColor", foreColor);

            labelTransitions.run();
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

        private void tsEnable_CheckedChanged(object sender, EventArgs e)
        {
            ThemeListener.Enabled = tsEnable.Checked;

            lblEnabled.Text = tsEnable.Checked ? "Enabled" : "Disabled";
        }

        private void lblEnabled_Click(object sender, EventArgs e)
        {
            tsEnable.Checked = !tsEnable.Checked;
        }

        #endregion
    }
}