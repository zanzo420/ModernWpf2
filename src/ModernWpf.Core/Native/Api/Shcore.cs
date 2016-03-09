using System;
using System.Runtime.InteropServices;

namespace ModernWpf.Native.Api
{
    /// <summary>
    /// API methods in Shcore.dll.
    /// </summary>
    static class Shcore
    {
        #region real hook

        class NativeMethods
        {
            [DllImport("Shcore.dll", SetLastError = true)]
            public static extern HRESULT GetDpiForMonitor(IntPtr hmonitor, MONITOR_DPI_TYPE dpiType, ref uint dpiX, ref uint dpiY);

            [DllImport("Shcore.dll", SetLastError = true)]
            public static extern HRESULT SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value);

            [DllImport("Shcore.dll", SetLastError = true)]
            public static extern HRESULT GetProcessDpiAwareness(IntPtr hprocess, ref PROCESS_DPI_AWARENESS value);
        }
        #endregion

        #region public methods


        /// <summary>
        /// Gets whether this api is supported.
        /// </summary>
        public static bool IsPlatformSupported { get { return PlatformInfo.IsWin81Up; } }
        
        /// <summary>
        /// Sets the current process to a specified dots per inch (dpi) awareness level. 
        /// </summary>
        /// <param name="value">The DPI awareness value to set.</param>
        /// <returns></returns>
        public static bool SetProcessDpiAwareness(PROCESS_DPI_AWARENESS value)
        {
            if (IsPlatformSupported)
            {
                return NativeMethods.SetProcessDpiAwareness(value).Succeeded;
            }
            return false;
        }


        /// <summary>
        /// Retrieves the dots per inch (dpi) awareness of the specified process.
        /// </summary>
        /// <param name="hprocess">Handle of the process that is being queried. If this parameter is NULL, the current process is queried.</param>
        /// <returns>The DPI awareness of the specified process.</returns>
        public static PROCESS_DPI_AWARENESS GetProcessDpiAwareness(IntPtr hprocess)
        {
            if (IsPlatformSupported)
            {
                PROCESS_DPI_AWARENESS value = PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
                if (NativeMethods.GetProcessDpiAwareness(hprocess, ref value).Succeeded)
                {
                    return value;
                }
            }
            return PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE;
        }

        /// <summary>
        /// Queries the dots per inch (dpi) of a display containing a window.
        /// </summary>
        /// <param name="hWnd">Handle of the window.</param>
        /// <param name="dpiType">The type of DPI being queried</param>
        /// <returns>The value of the DPI.</returns>
        public static int GetDpiForWindow(IntPtr hWnd, MONITOR_DPI_TYPE dpiType = MONITOR_DPI_TYPE.MDT_DEFAULT)
        {
            var hmonitor = User32.MonitorFromWindow(hWnd, MonitorOption.MONITOR_DEFAULTTONEAREST);
            return GetDpiForMonitor(hmonitor, dpiType);
        }

        /// <summary>
        /// Queries the dots per inch (dpi) of a display.
        /// </summary>
        /// <param name="hmonitor">Handle of the monitor being queried.</param>
        /// <param name="dpiType">The type of DPI being queried</param>
        /// <returns>The value of the DPI.</returns>
        public static int GetDpiForMonitor(IntPtr hmonitor, MONITOR_DPI_TYPE dpiType = MONITOR_DPI_TYPE.MDT_DEFAULT)
        {
            if (IsPlatformSupported)
            {
                uint dpiX = 0;
                uint dpiY = 0;
                if (NativeMethods.GetDpiForMonitor(hmonitor, dpiType, ref dpiX, ref dpiY).Succeeded)
                {
                    return (int)dpiX;
                }
            }
            return DefaultDPI;
        }

        const int DefaultDPI = 96;

        #endregion
    }
}