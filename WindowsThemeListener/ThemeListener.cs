#region Copyright

/*
 * Developer    : Willy Kimura
 * Library      : Windows Theme Listener
 * License      : MIT
 * 
 * Windows Theme Listener is a helper library that was birthed 
 * after a longing to see my applications blend-in with the new 
 * Windows 10 theming modes, that is, Dark and Light themes. 
 * It was quite surprising to realize that this space was empty. 
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
using System.Collections.Generic;

namespace WK.Libraries.WTL
{
    /// <summary>
    /// A library that listens to all modern Windows theming and color settings.
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
            TransparencyEnabled = GetTransparency();

            _nwAppThemeMode = GetAppMode();
            _nwWinThemeMode = GetWindowsMode();
            _nwAccentColor = GetAccentColor();
            _nwTransparencyEnabled = GetTransparency();

            _watcher = new RegistryMonitor(
                new Tuple<string, string>(_regKey, _transparencyKey),
                new Tuple<string, string>(_regKey, _appLightThemeKey),
                new Tuple<string, string>(_regKey, _winLightThemeKey),
                new Tuple<string, string>(_regKey2, _accentColorKey));

            _invoker.CreateControl();
            _watcher.RegistryChanged += RegistryChanged;
        }

        #endregion

        #region Fields

        private static bool _enabled = true;
        private static bool _transparencyEnabled;
        private static bool _nwTransparencyEnabled;

        private static ThemeModes _winThemeMode = ThemeModes.Light;
        private static ThemeModes _appThemeMode = ThemeModes.Light;
        private static ThemeModes _nwWinThemeMode = ThemeModes.Light;
        private static ThemeModes _nwAppThemeMode = ThemeModes.Light;

        private static Color _accentColor;
        private static Color _nwAccentColor;
        private static Color _accentForeColor;

        private static string _accentColorKey = "AccentColor";
        private static string _transparencyKey = "EnableTransparency";
        private static string _appLightThemeKey = "AppsUseLightTheme";
        private static string _winLightThemeKey = "SystemUsesLightTheme";
        private static string _regKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private static string _regKey2 = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\DWM";

        private static RegistryMonitor _watcher;
        private static List<ThemeSettings> _options;
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

        /// <summary>
        /// Provides the list of supported theming settings.
        /// </summary>
        public enum ThemeSettings
        {
            /// <summary>
            /// The <see cref="AppMode"/> setting.
            /// </summary>
            AppMode,

            /// <summary>
            /// The <see cref="WindowsMode"/> setting.
            /// </summary>
            WindowsMode,

            /// <summary>
            /// The <see cref="AccentColor"/> setting.
            /// </summary>
            AccentColor,

            /// <summary>
            /// The <see cref="AccentForeColor"/> setting.
            /// </summary>
            AccentForeColor,

            /// <summary>
            /// The <see cref="TransparencyEnabled"/> setting.
            /// </summary>
            Transparency
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether <see cref="ThemeListener"/> is enabled.
        /// </summary>
        public static bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;

                if (value == true)
                    _watcher.Restart();
                else
                    _watcher.Dispose();
            }
        }

        /// <summary>
        /// Gets a value indicating whether window transparency is enabled.
        /// </summary>
        public static bool TransparencyEnabled
        {
            get => GetTransparency();
            private set => _transparencyEnabled = value;
        }

        /// <summary>
        /// Gets or sets the period in millseconds between Registry polls. 
        /// Default is 3000ms (3 seconds).
        /// </summary>
        public static int Interval
        {
            get => _watcher.Period;
            set => _watcher.Period = value;
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
            private set => _accentForeColor = value;
        }

        /// <summary>
        /// Gets the currently applied applications theme mode.
        /// </summary>
        public static ThemeModes AppMode
        {
            get => GetAppMode();
            private set => _appThemeMode = value;
        }

        /// <summary>
        /// Gets the currently applied Windows system theme mode.
        /// </summary>
        public static ThemeModes WindowsMode
        {
            get => GetWindowsMode();
            private set => _winThemeMode = value;
        }

        #endregion

        #region Methods

        #region Public

        /// <summary>
        /// Generates a contrasting fore color based on a specified accent color.
        /// </summary>
        /// <param name="accentColor">
        /// The accent color to use.
        /// </param>
        public static Color GenerateAccentForeColor(Color accentColor)
        {
            // Calculate the perceptive luminance (aka luma) - human eye favors green color. 
            double luma = ((0.299 * accentColor.R) + (0.587 * accentColor.G) + (0.114 * accentColor.B)) / 255;

            // Return black for bright colors, white for dark colors.
            return luma > 0.5 ? Color.Black : Color.White;
        }

        #endregion

        #region Private

        /// <summary>
        /// Gets the current apps theme mode.
        /// </summary>
        private static ThemeModes GetAppMode()
        {
            try
            {
                bool lightMode = Convert.ToBoolean(Registry.GetValue(_regKey, _appLightThemeKey, null));
                var winMode = Registry.GetValue(_regKey, _winLightThemeKey, null);

                if (lightMode)
                    _appThemeMode = ThemeModes.Light;
                else
                    _appThemeMode = ThemeModes.Dark;

                if (winMode == null)
                    WindowsMode = _appThemeMode;

                return _appThemeMode;
            }
            catch (Exception)
            {
                return ThemeModes.Light;
            }
        }

        /// <summary>
        /// Gets the current Windows theme mode.
        /// </summary>
        private static ThemeModes GetWindowsMode()
        {
            try
            {
                var lightMode = Registry.GetValue(_regKey, _winLightThemeKey, null);
                
                if (lightMode == null)
                {
                    _winThemeMode = GetAppMode();
                }
                else
                {
                    if (Convert.ToBoolean(lightMode))
                        _winThemeMode = ThemeModes.Light;
                    else
                        _winThemeMode = ThemeModes.Dark;
                }

                return _winThemeMode;
            }
            catch (Exception)
            {
                return ThemeModes.Light;
            }
        }

        /// <summary>
        /// Parses a Windows theme mode value.
        /// </summary>
        private static ThemeModes GetThemeMode(int value)
        {
            if (value == 1)
                return ThemeModes.Light;
            else
                return ThemeModes.Dark;
        }

        /// <summary>
        /// Gets the currently applied Windows accent color.
        /// </summary>
        private static Color GetAccentColor()
        {
            try
            {
                _accentColor = ColorTranslator.FromWin32(
                    Convert.ToInt32(Registry.GetValue(_regKey2, _accentColorKey, null)));

                return _accentColor;
            }
            catch (Exception)
            {
                return Color.White;
            }
        }

        /// <summary>
        /// Gets a value indicating whether window transparency is enabled.
        /// </summary>
        private static bool GetTransparency()
        {
            try
            {
                _transparencyEnabled = Convert.ToBoolean(
                    Registry.GetValue(_regKey, _transparencyKey, null));

                return _transparencyEnabled;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether window transparency 
        /// is enabled without modifying its property variable.
        /// </summary>
        private static bool GetTransparencyRaw()
        {
            try
            {
                return Convert.ToBoolean(
                    Registry.GetValue(_regKey, _transparencyKey, null));
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #endregion

        #region Events

        #region Public

        #region Event Handlers

        /// <summary>
        /// Occurs when any of the supported theme settings have been changed.
        /// </summary>
        public static event EventHandler<ThemeSettingsChangedEventArgs> ThemeSettingsChanged;

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="ThemeSettingsChanged"/> event.
        /// </summary>
        public class ThemeSettingsChangedEventArgs : EventArgs
        {
            #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="ThemeSettingsChangedEventArgs"/> class.
            /// </summary>
            /// <param name="newAppMode">The newly set Apps theme.</param>
            /// <param name="newWinMode">The newly set Windows theme.</param>
            /// <param name="newAccentColor">The newly set accent color.</param>
            /// <param name="newTransparencyEnabled">The newly sey transparency option.</param>
            /// <param name="modSettings">The list of options modified</param>
            public ThemeSettingsChangedEventArgs(
                ThemeModes newAppMode, 
                ThemeModes newWinMode, 
                Color newAccentColor, 
                bool newTransparencyEnabled, 
                List<ThemeSettings> modSettings)
            {
                AppMode = newAppMode;
                WindowsMode = newWinMode;
                AccentColor = newAccentColor;
                TransparencyEnabled = newTransparencyEnabled;
                SettingsChanged = modSettings;
            }

            #endregion

            #region Properties

            /// <summary>
            /// Gets a value indicating whether window transparency is enabled.
            /// </summary>
            public bool TransparencyEnabled { get; private set; }

            /// <summary>
            /// Gets the newly applied Windows accent color.
            /// </summary>
            public Color AccentColor { get; private set; }

            /// <summary>
            /// Gets a newly generated accent fore color 
            /// based on the applied system accent color.
            /// </summary>
            public Color AccentForeColor
            {
                get => GenerateAccentForeColor(AccentColor);
            }

            /// <summary>
            /// Gets the newly applied apps theme mode.
            /// </summary>
            public ThemeModes AppMode { get; private set; }

            /// <summary>
            /// Gets the newly applied Windows system theme mode.
            /// </summary>
            public ThemeModes WindowsMode { get; private set; }

            /// <summary>
            /// Gets the list of personalization options modified.
            /// </summary>
            public List<ThemeSettings> SettingsChanged { get; private set; } = new List<ThemeSettings>();

            #endregion
        }

        #endregion

        #endregion

        #region Private

        /// <summary>
        /// Raised whenever the theming Registry keys have changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="RegistryMonitor.RegistryChangeEventArgs"/> instance containing the event data.</param>
        private static void RegistryChanged(object sender, RegistryMonitor.RegistryChangeEventArgs args)
        {
            try
            {
                if (Enabled)
                {
                    if (_invoker.Created)
                    {
                        _invoker.Invoke((Action)delegate
                        {
                            if (_options != null)
                                _options.Clear();

                            _options = new List<ThemeSettings>();
                            var winMode = Registry.GetValue(_regKey, _winLightThemeKey, null);

                            if (args.ValueName == _winLightThemeKey)
                            {
                                _nwWinThemeMode = GetThemeMode((int)args.Value);
                                _options.Add(ThemeSettings.WindowsMode);
                            }

                            if (args.ValueName == _appLightThemeKey)
                            {
                                _nwAppThemeMode = GetThemeMode((int)args.Value);
                                _options.Add(ThemeSettings.AppMode);

                                if (winMode == null)
                                {
                                    _nwWinThemeMode = _nwAppThemeMode;
                                    _options.Add(ThemeSettings.WindowsMode);
                                }
                            }

                            if (args.ValueName == _accentColorKey)
                            {
                                _nwAccentColor = ColorTranslator.FromWin32(Convert.ToInt32(args.Value));

                                _options.Add(ThemeSettings.AccentColor);
                                _options.Add(ThemeSettings.AccentForeColor);
                            }

                            if (args.ValueName == _transparencyKey)
                            {
                                _nwTransparencyEnabled = GetTransparencyRaw();
                                _options.Add(ThemeSettings.Transparency);
                            }

                            if (_winThemeMode != _nwWinThemeMode ||
                                _appThemeMode != _nwAppThemeMode ||
                                _accentColor != _nwAccentColor ||
                                _transparencyEnabled != _nwTransparencyEnabled)
                            {
                                ThemeSettingsChanged?.Invoke(_watcher,
                                    new ThemeSettingsChangedEventArgs(
                                        _nwAppThemeMode, _nwWinThemeMode,
                                        _nwAccentColor, _nwTransparencyEnabled, _options));

                                _winThemeMode = _nwWinThemeMode;
                                _appThemeMode = _nwAppThemeMode;
                                _accentColor = _nwAccentColor;
                                _accentForeColor = GenerateAccentForeColor(_nwAccentColor);
                                _transparencyEnabled = _nwTransparencyEnabled;
                            }
                        });
                    }
                }
            }
            catch (Exception) { }
        }

        #endregion

        #endregion
    }
}