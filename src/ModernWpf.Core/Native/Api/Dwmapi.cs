using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;

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
            //[DllImport("dwmapi.dll")]
            //[return: MarshalAs(UnmanagedType.Bool)]
            //public static extern bool DwmDefWindowProc(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, ref IntPtr plResult);

            [DllImport("dwmapi.dll")]
            public static extern void DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)]ref bool isEnabled);

            //[DllImport("dwmapi.dll")]
            //public static extern HRESULT DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

            //[DllImport("dwmapi.dll")]
            //public static extern HRESULT DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute,
            //    ref int attribute, uint cbAttribute);

            [DllImport("dwmapi.dll")]
            public static extern HRESULT DwmGetColorizationColor(out uint pcrColorization, [Out, MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

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

        public static Color GetWindowColor()
        {
            if (IsPlatformSupported)
            {
                bool isOpaque;
                uint color;
                if(NativeMethods.DwmGetColorizationColor(out color, out isOpaque).Succeeded)
                {
                    return Color.FromArgb(
                        (byte)((color & 0xFF000000) >> 24),
                        (byte)((color & 0x00FF0000) >> 16),
                        (byte)((color & 0x0000FF00) >> 8),
                        (byte)((color & 0x000000FF) >> 0));
                }
            }
            return SystemColors.ActiveCaptionColor;
        }

        #endregion
    }
}