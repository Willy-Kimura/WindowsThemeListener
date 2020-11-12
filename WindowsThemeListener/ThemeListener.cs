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
            AppMode = GetAppMode();
            WindowsMode = GetWindowsMode();
            AccentColor = GetAccentColor();

            _nwAppsThemeMode = GetAppMode();
            _nwWinThemeMode = GetWindowsMode();
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

        private static ThemeModes _winThemeMode;
        private static ThemeModes _appsThemeMode;
        private static ThemeModes _nwWinThemeMode;
        private static ThemeModes _nwAppsThemeMode;
        private static bool _transparencyEnabled;

        private static Color _accentColor;
        private static Color _nwAccentColor;
        private static Color _accentForeColor;
        private static Color _nwAccentForeColor;

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
        public enum ThemeModes
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
        /// Default is 3000ms (3 seconds).
        /// </summary>
        public static int PollingInterval
        {
            get => _watcher.Period;
            set => _watcher.Period = value;
        }

        /// <summary>
        /// Gets the currently applied applications theme mode.
        /// </summary>
        public static ThemeModes AppMode
        {
            get => GetAppMode();
            private set => _appsThemeMode = value;
        }

        /// <summary>
        /// Gets the currently applied system-wide Windows theme mode.
        /// </summary>
        public static ThemeModes WindowsMode
        {
            get => GetWindowsMode();
            private set => _winThemeMode = value;
        }

        /// <summary>
        /// Gets the currently applied system accent color.
        /// </summary>
        public static Color AccentColor
        {
            get => GetAccentColor();
            private set => _accentColor = value;
        }

        /// <summary>
        /// Gets a generated accent fore color based 
        /// on the applied system accent color.
        /// </summary>
        public static Color AccentForeColor
        {
            get => GenerateAccentForeColor(AccentColor);
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
        /// Parses a Windows theme mode value.
        /// </summary>
        private static ThemeModes GetTheme(int value)
        {
            if (value == 1)
                return ThemeModes.Light;
            else
                return ThemeModes.Dark;
        }

        /// <summary>
        /// Gets the current apps theme mode.
        /// </summary>
        private static ThemeModes GetAppMode()
        {
            bool lightMode = Convert.ToBoolean(Registry.GetValue(_regKey, _appsLightThemeKey, 0));

            if (lightMode)
                _appsThemeMode = ThemeModes.Light;
            else
                _appsThemeMode = ThemeModes.Dark;

            return _appsThemeMode;
        }

        /// <summary>
        /// Gets the current Windows theme mode.
        /// </summary>
        private static ThemeModes GetWindowsMode()
        {
            bool lightMode = Convert.ToBoolean(Registry.GetValue(_regKey, _sysLightThemeKey, 0));

            if (lightMode)
                _winThemeMode = ThemeModes.Light;
            else
                _winThemeMode = ThemeModes.Dark;

            return _winThemeMode;
        }

        /// <summary>
        /// Gets the currently applied Windows accent color.
        /// </summary>
        private static Color GetAccentColor()
        {
            _accentColor = ColorTranslator.FromWin32(
                Convert.ToInt32(Registry.GetValue(_regKey2, _accentColorKey, "")));

            return _accentColor;
        }

        /// <summary>
        /// Generates a contrasting fore color based on a specified accent color.
        /// </summary>
        /// <param name="accent">
        /// The accent color to use.
        /// </param>
        private static Color GenerateAccentForeColor(Color accent)
        {
            // Calculate the perceptive luminance (aka luma) - human eye favors green color... 
            double luma = ((0.299 * accent.R) + (0.587 * accent.G) + (0.114 * accent.B)) / 255;

            // Return black for bright colors, white for dark colors
            return luma > 0.5 ? Color.Black : Color.White;
        }

        /// <summary>
        /// Gets a value indicating whether window transparency is enabled system-wide.
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
        /// Occurs whenever the <see cref="AppMode"/>, <see cref="WindowsMode"/> or 
        /// <see cref="AccentColor"/> have been changed.
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
            /// <param name="oldAppsMode">The previously set Apps theme.</param>
            /// <param name="oldWinMode">The previously set Windows theme.</param>
            /// <param name="oldAccentColor">The previously set accent color.</param>
            /// <param name="newAppsMode">The newly set Apps theme.</param>
            /// <param name="newWinMode">The newly set Windows theme.</param>
            /// <param name="newAccentColor">The newly set accent color.</param>
            public ThemeChangedEventArgs(
                ThemeModes oldAppsMode, ThemeModes oldWinMode, Color oldAccentColor,
                ThemeModes newAppsMode, ThemeModes newWinMode, Color newAccentColor)
            {
                OldAppMode = oldAppsMode;
                OldWindowsMode = oldWinMode;
                OldAccentColor = oldAccentColor;
                NewAppMode = newAppsMode;
                NewWindowsMode = newWinMode;
                NewAccentColor = newAccentColor;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets the previously applied apps theme mode.
            /// </summary>
            public ThemeModes OldAppMode { get; private set; }

            /// <summary>
            /// Gets the newly applied apps theme mode.
            /// </summary>
            public ThemeModes NewAppMode { get; private set; }

            /// <summary>
            /// Gets the previously applied Windows theme mode.
            /// </summary>
            public ThemeModes OldWindowsMode { get; private set; }

            /// <summary>
            /// Gets the newly applied Windows theme mode.
            /// </summary>
            public ThemeModes NewWindowsMode { get; private set; }

            /// <summary>
            /// Gets the previously applied Windows accent color.
            /// </summary>
            public Color OldAccentColor { get; private set; }

            /// <summary>
            /// Gets the newly applied Windows accent color.
            /// </summary>
            public Color NewAccentColor { get; private set; }

            /// <summary>
            /// Gets the previously generated accent fore color.
            /// </summary>
            public Color OldAccentForeColor { get; private set; }

            /// <summary>
            /// Gets a newly generated accent fore color 
            /// based on the applied system accent color.
            /// </summary>
            public Color NewAccentForeColor
            {
                get => GenerateAccentForeColor(NewAccentColor);
            }

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
                            _nwWinThemeMode = GetTheme((int)args.Value);

                        if (args.ValueName == _appsLightThemeKey)
                            _nwAppsThemeMode = GetTheme((int)args.Value);

                        if (args.ValueName == _accentColorKey)
                            _nwAccentColor = ColorTranslator.FromWin32(Convert.ToInt32(args.Value));

                        if (_winThemeMode != _nwWinThemeMode || 
                            _appsThemeMode != _nwAppsThemeMode || 
                            _accentColor != _nwAccentColor)
                        {
                            ThemeChanged?.Invoke(_watcher,
                                new ThemeChangedEventArgs(
                                    _appsThemeMode, _winThemeMode,
                                    _accentColor, _nwAppsThemeMode,
                                    _nwWinThemeMode, _nwAccentColor));

                            _winThemeMode = _nwWinThemeMode;
                            _appsThemeMode = _nwAppsThemeMode;
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