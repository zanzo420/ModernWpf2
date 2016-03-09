using System;
using System.Runtime.InteropServices;

namespace ModernWpf.Native.Api
{
    static class Ntdll
    {
        class NativeMethods
        {
            [DllImport("ntdll.dll", CharSet = CharSet.Unicode)]
            public static extern int RtlGetVersion([In, Out]ref OSVERSIONINFOEX version);
        }

        public static Version RtlGetVersion()
        {
            var v = default(OSVERSIONINFOEX);
            v.dwOSVersionInfoSize = (uint)Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            if (NativeMethods.RtlGetVersion(ref v) == 0)
            {
                return new Version((int)v.dwMajorVersion, (int)v.dwMinorVersion, (int)v.dwBuildNumber, 0);
            }
            // didn't work ???
            return Environment.OSVersion.Version;
        }
    }
}
