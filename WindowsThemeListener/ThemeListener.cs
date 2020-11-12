#region Copyright

/*
 * Developer    : Willy Kimura
 * Library      : Windows Theme Listener
 * License      : MIT
 * 
 * Windows Theme Listener is a helper library that was birthed 
 * after a longing to see my applications blend-in with the new 
 * Windows 10 theming modes, that is, Dark and Light themes. 
 * How I searched through countless StackOverFlow questions! 
 * Oh well, eventually the hardwork had to be done by someone, 
 * so I set out to building a nifty helper library that would 
 * do just that and probably even more. Thus came "WTL" or 
 * Windows Theme Listener, a nifty, static .NET library that 
 * lets one not only capture the default Windows theming modes, 
 * but also listen to any changes made to the theming modes and 
 * the system-wide accent color applied. This library will help 
 * developers modernize their applications to support dark/light 
 * theming options and so create a seamless end-user experience.
 * 
 * Improvements are welcome.
 * 
 */

#endregion


using System;
using System.Drawing;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using WK.Libraries.WTL.Helpers;

namespace WK.Libraries.WTL
{
    /// <summary>
    /// A static class that provides helper properties and 
    /// methods for obtaining Windows 10 Theming settings.
    /// </summary>
    [DebuggerStepThrough]
    public static class ThemeListener
    {
        #region Constructor

        /// <summary>
        /// Initializes the <see cref="Theming"/> class.
        /// </summary>
        static ThemeListener()
        {
            AppsTheme = GetAppsTheme();
            WindowsTheme = GetWindowsTheme();
            AccentColor = GetAccentColor();

            _nwAppsTheme = GetAppsTheme();
            _nwWinTheme = GetWindowsTheme();
            _nwAccentColor = GetAccentColor();

            TransparencyEnabled = GetTransparency();

            _watcher = new RegistryWatcher(
                new Tuple<string, string>(_regKey, _transparencyKey),
                new Tuple<string, string>(_regKey, _appsLightThemeKey),
                new Tuple<string, string>(_regKey, _sysLightThemeKey),
                new Tuple<string, string>(_regKey2, _accentColorKey));

            _invoker.CreateControl();
            _watcher.RegistryChanged += RegistryChanged;
        }

        #endregion

        #region Fields

        private static Themes _winTheme;
        private static Themes _appsTheme;
        private static Themes _nwWinTheme;
        private static Themes _nwAppsTheme;
        private static bool _transparencyEnabled;

        private static Color _accentColor;
        private static Color _nwAccentColor;

        private static string _accentColorKey = "AccentColor";
        private static string _transparencyKey = "EnableTransparency";
        private static string _appsLightThemeKey = "AppsUseLightTheme";
        private static string _sysLightThemeKey = "SystemUsesLightTheme";
        private static string _regKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private static string _regKey2 = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM";

        private static RegistryWatcher _watcher;
        private static UserControl _invoker = new UserControl();

        #endregion

        #region Enumerations

        /// <summary>
        /// Provides the default Windows Themes.
        /// </summary>
        public enum Themes
        {
            /// <summary>
            /// Windows Dark theme.
            /// </summary>
            Dark,

            /// <summary>
            /// Windows Light theme.
            /// </summary>
            Light
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ThemeListener"/> is enabled.
        /// </summary>
        public static bool Enabled { get; set; } = true;

        /// <summary>
        /// Gets or sets the period in millseconds between Registry polls. 
        /// Default is 10,000 ms (10 seconds).
        /// </summary>
        public static int PollingInterval { get; set; } = 10000;

        /// <summary>
        /// Gets the currently applied applications theme.
        /// </summary>
        public static Color AccentColor
        {
            get => GetAccentColor();
            private set => _accentColor = value;
        }

        /// <summary>
        /// Gets the currently applied applications theme.
        /// </summary>
        public static Themes AppsTheme
        {
            get => GetAppsTheme();
            private set => _appsTheme = value;
        }

        /// <summary>
        /// Gets the currently applied system-wide Windows theme.
        /// </summary>
        public static Themes WindowsTheme
        {
            get => GetWindowsTheme();
            private set => _winTheme = value;
        }

        /// <summary>
        /// Gets a value indicating whether transparency is enabled system-wide.
        /// </summary>
        public static bool TransparencyEnabled
        {
            get => GetTransparency();
            private set => _transparencyEnabled = value;
        }

        #endregion

        #region Methods

        #region Private

        /// <summary>
        /// Gets the currently applied theme type.
        /// </summary>
        private static Themes GetTheme(int value)
        {
            if (value == 1)
                return Themes.Light;
            else
                return Themes.Dark;
        }

        /// <summary>
        /// Gets the currently applied system accent color.
        /// </summary>
        private static Color GetAccentColor()
        {
            _accentColor = ColorTranslator.FromWin32(
                Convert.ToInt32(Registry.GetValue(_regKey2, _accentColorKey, "")));

            return _accentColor;
        }

        /// <summary>
        /// Gets the currently applied applications theme.
        /// </summary>
        private static Themes GetAppsTheme()
        {
            bool lightMode = Convert.ToBoolean(Registry.GetValue(_regKey, _appsLightThemeKey, 0));

            if (lightMode)
                _appsTheme = Themes.Light;
            else
                _appsTheme = Themes.Dark;

            return _appsTheme;
        }

        /// <summary>
        /// Gets the currently applied system-wide Windows theme.
        /// </summary>
        private static Themes GetWindowsTheme()
        {
            bool lightMode = Convert.ToBoolean(Registry.GetValue(_regKey, _sysLightThemeKey, 0));

            if (lightMode)
                _winTheme = Themes.Light;
            else
                _winTheme = Themes.Dark;

            return _winTheme;
        }

        /// <summary>
        /// Gets a value indicating whether transparency is enabled system-wide.
        /// </summary>
        private static bool GetTransparency()
        {
            _transparencyEnabled = Convert.ToBoolean(Registry.GetValue(_regKey, _transparencyKey, 0));

            return _transparencyEnabled;
        }

        #endregion

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// Occurs when either the <see cref="AppsTheme"/> or the <see cref="WindowsTheme"/> has been changed.
        /// </summary>
        public static event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="ThemeChanged"/> event.
        /// </summary>
        public class ThemeChangedEventArgs : EventArgs
        {
            #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="RegistryChangeEventArgs"/> class.
            /// </summary>
            /// <param name="oldAppsTheme">The previously set Apps theme.</param>
            /// <param name="oldSysTheme">The previously set System theme.</param>
            /// <param name="oldAccentColor">The previously set accent color.</param>
            /// <param name="newAppsTheme">The newly set Apps theme.</param>
            /// <param name="newSysTheme">The newly set System theme.</param>
            /// <param name="newAccentColor">The newly set accent color.</param>
            public ThemeChangedEventArgs(
                Themes oldAppsTheme, Themes oldSysTheme, Color oldAccentColor,
                Themes newAppsTheme, Themes newSysTheme, Color newAccentColor)
            {
                OldAppsTheme = oldAppsTheme;
                OldWindowsTheme = oldSysTheme;
                OldAccentColor = oldAccentColor;
                NewAppsTheme = newAppsTheme;
                NewWindowsTheme = newSysTheme;
                NewAccentColor = newAccentColor;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the previously applied applications theme.
            /// </summary>
            public Themes OldAppsTheme { get; private set; }

            /// <summary>
            /// Gets the currently applied applications theme.
            /// </summary>
            public Themes NewAppsTheme { get; private set; }

            /// <summary>
            /// Gets the previously applied Windows theme.
            /// </summary>
            public Themes OldWindowsTheme { get; private set; }

            /// <summary>
            /// Gets the currently applied system-wide Windows theme.
            /// </summary>
            public Themes NewWindowsTheme { get; private set; }

            /// <summary>
            /// Gets the previously applied accent color.
            /// </summary>
            public Color OldAccentColor { get; private set; }

            /// <summary>
            /// Gets the currently applied accent color.
            /// </summary>
            public Color NewAccentColor { get; private set; }

            #endregion
        }

        #endregion

        #endregion

        #region Private

        /// <summary>
        /// Raised whenever the theming Registry keys have changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="RegistryWatcher.RegistryChangeEventArgs"/> instance containing the event data.</param>
        private static void RegistryChanged(object sender, RegistryWatcher.RegistryChangeEventArgs args)
        {
            if (Enabled)
            {
                if (_invoker.Created)
                {
                    _invoker.Invoke((Action)delegate
                    {
                        if (args.ValueName == _sysLightThemeKey)
                            _nwWinTheme = GetTheme((int)args.Value);

                        if (args.ValueName == _appsLightThemeKey)
                            _nwAppsTheme = GetTheme((int)args.Value);

                        if (args.ValueName == _accentColorKey)
                        {
                            _nwAccentColor = ColorTranslator.FromWin32(Convert.ToInt32(args.Value));
                        }

                        if (_winTheme != _nwWinTheme || _appsTheme != _nwAppsTheme || _accentColor != _nwAccentColor)
                        {
                            ThemeChanged?.Invoke(_watcher,
                                new ThemeChangedEventArgs(
                                    _appsTheme, _winTheme,
                                    _accentColor, _nwAppsTheme,
                                    _nwWinTheme, _nwAccentColor));

                            _winTheme = _nwWinTheme;
                            _appsTheme = _nwAppsTheme;
                            _accentColor = _nwAccentColor;
                        }
                    });
                }
            }
        }

        #endregion

        #endregion
    }
}