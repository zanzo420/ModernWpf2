using System.Runtime.InteropServices;

namespace ModernWpf.Native.Api
{
    /// <summary>
    /// API methods in dwmapi.dll.
    /// </summary>
    static class Dwmapi
    {
        #region real hook

        static class NativeMethods
        {
            //[DllImport("dwmapi.dll", CharSet = CharSet.Unicode)]
            //[return: MarshalAs(UnmanagedType.Bool)]
            //public static extern bool DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, ref IntPtr plResult);

            [DllImport("dwmapi.dll", CharSet = CharSet.Unicode)]
            public static extern void DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)]ref bool isEnabled);

            //[DllImport("dwmapi.dll", CharSet = CharSet.Unicode)]
            //public static extern HRESULT DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

            //[DllImport("dwmapi.dll", CharSet = CharSet.Unicode)]
            //public static extern HRESULT DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute,
            //    ref int attribute, uint cbAttribute);
        }
        #endregion

        #region public methods


        /// <summary>
        /// Gets whether this api is supported.
        /// </summary>
        public static bool IsPlatformSupported { get { return PlatformInfo.IsWinVistaUp; } }

        /// <summary>
        /// Obtains a value that indicates whether Desktop Window Manager (DWM) composition is enabled. Applications can listen for composition state changes by handling the WM_DWMCOMPOSITIONCHANGED notification.
        /// </summary>
        public static bool IsCompositionEnabled
        {
            get
            {
                bool val = false;
                if (IsPlatformSupported)
                {
                    NativeMethods.DwmIsCompositionEnabled(ref val);
                }
                return val;
            }
        }

        #endregion
    }
}