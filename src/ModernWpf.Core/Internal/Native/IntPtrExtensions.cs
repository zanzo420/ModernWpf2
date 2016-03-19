using ModernWpf.Native;
using System;

namespace ModernWpf.Native
{
    /// <summary>
    /// Contains useful things for <see cref="IntPtr"/>.
    /// </summary>
    static class IntPtrExtensions
    {
        /// <summary>
        /// Correctly gets the mouse point location specified in lParam.
        /// </summary>
        /// <param name="lParam">The lParam from WndProc.</param>
        /// <returns></returns>
        public static POINT ToPoint(this IntPtr lParam)
        {
            // new from http://stackoverflow.com/questions/7913325/win-api-in-c-get-hi-and-low-word-from-intptr
            // to handle possible negative values from multi-monitor setup

            int x = unchecked((short)lParam);
            int y = unchecked((short)((uint)lParam >> 16));
            return new POINT { x = x, y = y };
        }
    }
}
