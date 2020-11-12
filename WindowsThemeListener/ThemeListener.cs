#region Copyright

/*
 * Developer    : Willy Kimura
 * Library      : Windows Theming Helper
 * License      : MIT
 * 
 * BootMeUp is a library that was inspired by the need for .NET 
 * developers to have an easier one-stop solution when it comes to 
 * automatic launching of their applications at system startup. 
 * Having come across a number of SO (StackOverflow) questions 
 * regarding this topic or revolving around it together with 
 * the many decentralized, undocumented and standalone ways 
 * of incorporating this feature, I saw the desperate of many 
 * and so took to building an all-inclusive library that 
 * caters for 'all-things-startup' in .NET applications. 
 * So I built this nifty little library does just that and 
 * even more. Do check it out and try it with your apps!
 * 
 * Improvements are welcome.
 * 
 */

#endregion


using System;
using Microsoft.Win32;
using System.Diagnostics;
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
            SystemTheme = GetSystemTheme();

            _nwAppsTheme = GetAppsTheme();
            _nwSysTheme = GetSystemTheme();       

            TransparencyEnabled = GetTransparency();

            _watcher = new RegistryWatcher(
                new Tuple<string, string>(_regKey, _transparencyKey),
                new Tuple<string, string>(_regKey, _appsLightThemeKey),
                new Tuple<string, string>(_regKey, _sysLightThemeKey));

            _watcher.RegistryChanged += RegistryChanged;
        }

        #endregion

        #region Fields

        private static Themes _sysTheme;
        private static Themes _appsTheme;
        private static Themes _nwSysTheme;
        private static Themes _nwAppsTheme;
        private static bool _transparencyEnabled;

        private static string _transparencyKey = "EnableTransparency";
        private static string _appsLightThemeKey = "AppsUseLightTheme";
        private static string _sysLightThemeKey = "SystemUsesLightTheme";
        private static string _regKey = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private static RegistryWatcher _watcher;

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
        /// Gets or sets the period in millseconds between Registry polls. 
        /// Default is 30000ms (30 seconds).
        /// </summary>
        public static int PollingInterval { get; set; } = 3000;

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
        public static Themes SystemTheme
        {
            get => GetSystemTheme();
            private set => _sysTheme = value;
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
        /// Gets the currently applied applications theme.
        /// </summary>
        private static Themes GetAppsTheme()
        {
            bool isLight = Convert.ToBoolean(Registry.GetValue(_regKey, _appsLightThemeKey, 0));

            if (isLight)
                _appsTheme = Themes.Light;
            else
                _appsTheme = Themes.Dark;

            return _appsTheme;
        }

        /// <summary>
        /// Gets the currently applied system-wide theme.
        /// </summary>
        private static Themes GetSystemTheme()
        {
            bool isLight = Convert.ToBoolean(Registry.GetValue(_regKey, _sysLightThemeKey, 0));

            if (isLight)
                _sysTheme = Themes.Light;
            else
                _sysTheme = Themes.Dark;

            return _sysTheme;
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
        /// Occurs when either the <see cref="AppsTheme"/> or the <see cref="SystemTheme"/>has been changed.
        /// </summary>
        public static event EventHandler<ThemeChangeEventArgs> ThemeChanged;

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="ThemeChanged"/> event.
        /// </summary>
        public class ThemeChangeEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RegistryChangeEventArgs"/> class.
            /// </summary>
            /// <param name="prevAppsTheme">The previously set Apps theme.</param>
            /// <param name="prevSysTheme">The previously set System theme.</param>
            /// <param name="newAppsTheme">The newly set Apps theme.</param>
            /// <param name="newSysTheme">The newly set System theme.</param>
            public ThemeChangeEventArgs(Themes prevAppsTheme, Themes prevSysTheme, Themes newAppsTheme, Themes newSysTheme)
            {
                OldAppsTheme = prevAppsTheme;
                OldSystemTheme = prevSysTheme;
                NewAppsTheme = newAppsTheme;
                NewSystemTheme = newSysTheme;
            }

            /// <summary>
            /// Gets the previously applied applications theme.
            /// </summary>
            public Themes OldAppsTheme { get; private set; }

            /// <summary>
            /// Gets the currently applied applications theme.
            /// </summary>
            public Themes NewAppsTheme { get; private set; }

            /// <summary>
            /// Gets the previously applied applications theme.
            /// </summary>
            public Themes OldSystemTheme { get; private set; }

            /// <summary>
            /// Gets the currently applied system-wide theme.
            /// </summary>
            public Themes NewSystemTheme { get; private set; }
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
            if (args.ValueName == _sysLightThemeKey)
                _nwSysTheme = GetTheme((int)args.Value);

            if (args.ValueName == _appsLightThemeKey)
                _nwAppsTheme = GetTheme((int)args.Value);

            if (_sysTheme != _nwSysTheme || _appsTheme != _nwAppsTheme)
            {
                ThemeChanged?.Invoke(_watcher, 
                    new ThemeChangeEventArgs(_appsTheme, _sysTheme, _nwAppsTheme, _nwSysTheme));

                _sysTheme = _nwSysTheme;
                _appsTheme = _nwAppsTheme;
            }
        }

        #endregion

        #endregion
    }
}