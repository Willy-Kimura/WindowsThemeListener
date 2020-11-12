#region Copyright

/*
 * Developer    : Willy Kimura
 * Library      : Windows Theme Listener
 * License      : MIT
 * 
 */

#endregion


using System;
using System.IO;
using Microsoft.Win32;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace WK.Libraries.WTL.Helpers
{
	/// <summary>
	/// <b>RegistryMonitor</b> allows you to monitor specific registry key.
	/// </summary>
	public class RegistryMonitor : IDisposable
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistryMonitor"/> class.
		/// </summary>
		/// <param name="registryKey">
		/// The registry key to monitor.
		/// </param>
		public RegistryMonitor(RegistryKey registryKey)
		{
			InitRegistryKey(registryKey.Name);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistryMonitor"/> class.
		/// </summary>
		/// <param name="name">
		/// The name.
		/// </param>
		public RegistryMonitor(string name)
		{
			if (name == null || name.Length == 0)
				throw new ArgumentNullException("name");

			InitRegistryKey(name);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RegistryMonitor"/> class.
		/// </summary>
		/// <param name="registryHive">The registry hive.</param>
		/// <param name="subKey">The sub key.</param>
		public RegistryMonitor(RegistryHive registryHive, string subKey)
		{
			InitRegistryKey(registryHive, subKey);
		}

		#endregion

		#region Fields

		private const int KEY_QUERY_VALUE = 0x0001;
		private const int KEY_NOTIFY = 0x0010;
		private const int STANDARD_RIGHTS_READ = 0x00020000;

		private static readonly IntPtr HKEY_CLASSES_ROOT = new IntPtr(unchecked((int)0x80000000));
		private static readonly IntPtr HKEY_CURRENT_USER = new IntPtr(unchecked((int)0x80000001));
		private static readonly IntPtr HKEY_LOCAL_MACHINE = new IntPtr(unchecked((int)0x80000002));
		private static readonly IntPtr HKEY_USERS = new IntPtr(unchecked((int)0x80000003));
		private static readonly IntPtr HKEY_PERFORMANCE_DATA = new IntPtr(unchecked((int)0x80000004));
		private static readonly IntPtr HKEY_CURRENT_CONFIG = new IntPtr(unchecked((int)0x80000005));
		private static readonly IntPtr HKEY_DYN_DATA = new IntPtr(unchecked((int)0x80000006));

		private bool _disposed = false;
		private string _registrySubName;
		private object _threadLock = new object();

		private Thread _thread;
		private IntPtr _registryHive;

		private ManualResetEvent _eventTerminate = new ManualResetEvent(false);
		private RegChangeNotifyFilter _regFilter = RegChangeNotifyFilter.Key | RegChangeNotifyFilter.Attribute |
												   RegChangeNotifyFilter.Value | RegChangeNotifyFilter.Security;

		#endregion

		#region Properties

		/// <summary>
		/// Gets a value indicating whether the Registry is being monitored.
		/// </summary>
		public bool IsMonitoring
		{
			get => _thread != null;
		}

		/// <summary>
		/// Gets or sets the <see cref="RegChangeNotifyFilter"/>.
		/// </summary>
		public RegChangeNotifyFilter RegChangeNotifyFilter
		{
			get => _regFilter;
			set
			{
				lock (_threadLock)
				{
					if (IsMonitoring)
						throw new InvalidOperationException("Monitoring thread is already running");

					_regFilter = value;
				}
			}
		}

		#endregion

		#region Methods

		#region Public

		/// <summary>
		/// Starts the monitoring thread.
		/// </summary>
		public void Start()
		{
			if (_disposed)
				throw new ObjectDisposedException(null, "This instance is already disposed.");

			lock (_threadLock)
			{
				if (!IsMonitoring)
				{
					_eventTerminate.Reset();
					_thread = new Thread(new ThreadStart(MonitorThread));
					_thread.IsBackground = true;
					_thread.Start();
				}
			}
		}

		/// <summary>
		/// Stops the monitoring thread.
		/// </summary>
		public void Stop()
		{
			if (_disposed)
				throw new ObjectDisposedException(null, "This instance is already disposed.");

			lock (_threadLock)
			{
				Thread thread = _thread;

				if (thread != null)
				{
					_eventTerminate.Set();
					thread.Join();
				}
			}
		}

		/// <summary>
		/// Disposes this object.
		/// </summary>
		public void Dispose()
		{
			Stop();

			_disposed = true;

			GC.SuppressFinalize(this);
		}

		#endregion

		#region Private

		#region Win32

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegOpenKeyEx(IntPtr hKey, string subKey, uint options, int samDesired,
											   out IntPtr phkResult);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegNotifyChangeKeyValue(IntPtr hKey, bool bWatchSubtree,
														  RegChangeNotifyFilter dwNotifyFilter, IntPtr hEvent,
														  bool fAsynchronous);

		[DllImport("advapi32.dll", SetLastError = true)]
		private static extern int RegCloseKey(IntPtr hKey);

		#endregion

		private void InitRegistryKey(RegistryHive hive, string name)
		{
			switch (hive)
			{
				case RegistryHive.ClassesRoot:
					_registryHive = HKEY_CLASSES_ROOT;
					break;

				case RegistryHive.CurrentConfig:
					_registryHive = HKEY_CURRENT_CONFIG;
					break;

				case RegistryHive.CurrentUser:
					_registryHive = HKEY_CURRENT_USER;
					break;

				case RegistryHive.DynData:
					_registryHive = HKEY_DYN_DATA;
					break;

				case RegistryHive.LocalMachine:
					_registryHive = HKEY_LOCAL_MACHINE;
					break;

				case RegistryHive.PerformanceData:
					_registryHive = HKEY_PERFORMANCE_DATA;
					break;

				case RegistryHive.Users:
					_registryHive = HKEY_USERS;
					break;

				default:
					throw new InvalidEnumArgumentException("hive", (int)hive, typeof(RegistryHive));
			}

			_registrySubName = name;
		}

		private void InitRegistryKey(string name)
		{
			string[] nameParts = name.Split('\\');

			switch (nameParts[0])
			{
				case "HKEY_CLASSES_ROOT":
				case "HKCR":
					_registryHive = HKEY_CLASSES_ROOT;
					break;

				case "HKEY_CURRENT_USER":
				case "HKCU":
					_registryHive = HKEY_CURRENT_USER;
					break;

				case "HKEY_LOCAL_MACHINE":
				case "HKLM":
					_registryHive = HKEY_LOCAL_MACHINE;
					break;

				case "HKEY_USERS":
					_registryHive = HKEY_USERS;
					break;

				case "HKEY_CURRENT_CONFIG":
					_registryHive = HKEY_CURRENT_CONFIG;
					break;

				default:
					_registryHive = IntPtr.Zero;
					throw new ArgumentException("The registry hive '" + nameParts[0] + "' is not supported", "value");
			}

			_registrySubName = String.Join("\\", nameParts, 1, nameParts.Length - 1);
		}

		private void MonitorThread()
		{
			try
			{
				ThreadLoop();
			}
			catch (Exception e)
			{
				OnRegistryAccessError(e);
			}

			_thread = null;
		}

		private void ThreadLoop()
		{
			IntPtr registryKey;

			int result = RegOpenKeyEx(_registryHive, _registrySubName, 0, 
									  STANDARD_RIGHTS_READ | KEY_QUERY_VALUE | KEY_NOTIFY,
									  out registryKey);

			if (result != 0)
				throw new Win32Exception(result);

			try
			{
				AutoResetEvent _eventNotify = new AutoResetEvent(false);
				WaitHandle[] waitHandles = new WaitHandle[] { _eventNotify, _eventTerminate };

				while (!_eventTerminate.WaitOne(0, true))
				{
					result = RegNotifyChangeKeyValue(registryKey, true, _regFilter, _eventNotify.Handle, true);

					if (result != 0)
						throw new Win32Exception(result);

					if (WaitHandle.WaitAny(waitHandles) == 0)
						OnRegistryChanged();
				}
			}
			finally
			{
				if (registryKey != IntPtr.Zero)
					RegCloseKey(registryKey);
			}
		}

		#endregion

		#endregion

		#region Events

		#region Public

		#region Event Handlers

		/// <summary>
		/// Occurs when the specified Registry key has changed.
		/// </summary>
		public event EventHandler RegistryChanged;

		/// <summary>
		/// Occurs when access to the Registry fails.
		/// </summary>
		public event ErrorEventHandler RegistryAccessError;

		/// <summary>
		/// Raises the <see cref="RegistryChanged"/> event.
		/// </summary>
		/// <remarks>
		/// <p>
		/// <b>OnRegChanged</b> is called when the specified registry key has changed.
		/// </p>
		/// <note type="inheritinfo">
		/// When overriding <see cref="OnRegistryChanged"/> in a derived class, be sure to call
		/// the base class's <see cref="OnRegistryChanged"/> method.
		/// </note>
		/// </remarks>
		protected virtual void OnRegistryChanged()
		{
			EventHandler handler = RegistryChanged;
			if (handler != null)
				handler(this, null);
		}

		/// <summary>
		/// Raises the <see cref="RegistryAccessError"/> event.
		/// </summary>
		/// <param name="e">The <see cref="Exception"/> which occured while watching the registry.</param>
		/// <remarks>
		/// <p>
		/// <b>OnError</b> is called when an exception occurs while watching the registry.
		/// </p>
		/// <note type="inheritinfo">
		/// When overriding <see cref="OnRegistryAccessError"/> in a derived class, be sure to call
		/// the base class's <see cref="OnRegistryAccessError"/> method.
		/// </note>
		/// </remarks>
		protected virtual void OnRegistryAccessError(Exception e)
		{
			ErrorEventHandler handler = RegistryAccessError;
			if (handler != null)
				handler(this, new ErrorEventArgs(e));
		}

		#endregion

		#endregion

		#region Private

		#endregion

		#endregion
	}
	
	/// <summary>
	/// Filter for notifications reported by <see cref="RegistryMonitor"/>.
	/// </summary>
	[Flags]
	public enum RegChangeNotifyFilter
	{
		/// <summary>
		/// Notify the caller if a subkey is added or deleted.
		/// </summary>
		Key = 1,

		/// <summary>
		/// Notify the caller of changes to the attributes of the key 
		/// such as the security descriptor information.
		/// </summary>
		Attribute = 2,

		/// <summary>
		/// Notify the caller of changes to a value of the key. This can
		/// include adding or deleting a value, or changing an existing value.
		/// </summary>
		Value = 4,

		/// <summary>
		/// Notify the caller of changes to the security descriptor
		/// of the key.
		/// </summary>
		Security = 8,
	}
}