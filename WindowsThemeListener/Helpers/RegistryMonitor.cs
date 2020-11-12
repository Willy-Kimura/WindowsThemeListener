#region Copyright

/*
 * Developer    : Willy Kimura
 * Library      : Windows Theme Listener
 * License      : MIT
 * 
 */

#endregion


using System;
using System.Linq;
using Microsoft.Win32;
using System.Threading;
using System.Collections.Generic;

namespace WK.Libraries.WTL.Helpers
{
    /// <summary>
    /// Monitors the System Registry for any changes made to specific keys.
    /// </summary>
    public class RegistryMonitor : IDisposable
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryMonitor"/> class.
        /// </summary>
        /// <param name="toWatch">Registry entries to watch.</param>
        public RegistryMonitor(params Tuple<string, string>[] toWatch)
        {
            this.toWatch = toWatch;

            if (toWatch.Length > 0)
            {
                currentRegValues = toWatch.ToDictionary(key => key, key => Registry.GetValue(key.Item1, key.Item2, null));
                timer = new Timer(CheckRegistry, null, Period, Timeout.Infinite);
            }
        }

        #endregion

        #region Fields

        /// <summary>
        /// The current Registry values to be compared against.
        /// </summary>
        private readonly Dictionary<Tuple<string, string>, object> currentRegValues;

        /// <summary>
        /// Registry entries to watch.
        /// </summary>
        private readonly Tuple<string, string>[] toWatch;

        /// <summary>
        /// The timer to trigger Registry polls.
        /// </summary>
        private readonly Timer timer;

        #endregion

        #region Properties

        /// <summary>
        /// The period in millseconds between Registry polls.
        /// </summary>
        public int Period { get; set; } = 3000;

        #endregion

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                timer?.Dispose();
            }
            catch { }
        }

        /// <summary>
        /// Checks the registry.
        /// </summary>
        /// <param name="state">The state.</param>
        private void CheckRegistry(object state)
        {
            foreach (Tuple<string, string> reg in toWatch)
            {
                object newValue = Registry.GetValue(reg.Item1, reg.Item2, null);

                if (currentRegValues[reg] != newValue)
                {
                    RegistryChanged?.Invoke(this, new RegistryChangeEventArgs(reg.Item1, reg.Item2, newValue));
                    currentRegValues[reg] = newValue;
                }
            }

            timer.Change(Period, Timeout.Infinite);
        }

        #endregion

        #region Events

        #region Event Handlers

        /// <summary>
        /// Occurs when Registry value(s) change.
        /// </summary>
        public event EventHandler<RegistryChangeEventArgs> RegistryChanged;

        #endregion

        #region Event Arguments

        /// <summary>
        /// Provides data for the <see cref="RegistryChanged"/> event.
        /// </summary>
        public class RegistryChangeEventArgs : EventArgs
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="RegistryChangeEventArgs"/> class.
            /// </summary>
            /// <param name="keyName">Name of the key.</param>
            /// <param name="valueName">Name of the value.</param>
            /// <param name="value">The value.</param>
            public RegistryChangeEventArgs(string keyName, string valueName, object value)
            {
                KeyName = keyName;
                ValueName = valueName;
                Value = value;
            }

            /// <summary>
            /// Gets the name of the Registry key.
            /// </summary>
            public string KeyName { get; }

            /// <summary>
            /// Gets the name of the <see cref="KeyName"/> value.
            /// </summary>
            public string ValueName { get; }

            /// <summary>
            /// Gets the value.
            /// </summary>
            public object Value { get; }
        }

        #endregion

        #endregion
    }
}