using ModernWpf.Native.Api;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Provides checks for platform support.
    /// </summary>
    static class PlatformInfo
    {
        static PlatformInfo()
        {
            // see why not use Environment.OSVersion.Version: 
            // http://stackoverflow.com/questions/17399302/how-can-i-detect-windows-8-1-in-a-desktop-application
            // http://msdn.microsoft.com/en-us/library/windows/desktop/dn302074.aspx

            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var ver = Ntdll.RtlGetVersion();
                
                IsWinVistaUp = ver >= new Version(6, 0, 0);
                IsWin7Up = ver >= new Version(6, 1, 0);
                IsWin8Up = ver >= new Version(6, 2, 0);
                IsWin81Up = ver >= new Version(6, 3, 0);
                IsWin10Up = ver >= new Version(10, 0, 0);
            }

            Is64BitProcess = IntPtr.Size == 8;
        }

        /// <summary>
        /// Gets a value indicating whether the current process is 64-bit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the current process is 64-bit; otherwise, <c>false</c>.
        /// </value>
        public static bool Is64BitProcess { get; private set; }
        
        /// <summary>
        /// Gets a value indicating whether the OS is Windows vista or higher.
        /// </summary>
        /// <value>
        /// <c>true</c> if the OS is Windows vista or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWinVistaUp { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the OS is Windows 7 or higher.
        /// </summary>
        /// <value>
        /// <c>true</c> if the OS is Windows 7 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWin7Up { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the OS is Windows 8 or higher.
        /// </summary>
        /// <value>
        /// <c>true</c> if the OS is Windows 8 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWin8Up { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the OS is Windows 8.1 or higher.
        /// </summary>
        /// <value>
        /// <c>true</c> if the OS is Windows 8.1 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWin81Up { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the OS is Windows 10 or higher.
        /// </summary>
        /// <value>
        /// <c>true</c> if the OS is Windows 10 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool IsWin10Up { get; private set; }
    }
}
