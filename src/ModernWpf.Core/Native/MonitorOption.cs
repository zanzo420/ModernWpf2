using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Option used with monitor APIs.
    /// </summary>
    enum MonitorOption : uint
    {
        /// <summary>
        /// Returns NULL.
        /// </summary>
        MONITOR_DEFAULTTONULL = 0x00000000,
        /// <summary>
        /// Returns a handle to the primary display monitor.
        /// </summary>
        MONITOR_DEFAULTTOPRIMARY = 0x00000001,
        /// <summary>
        /// Returns a handle to the display monitor that is nearest to the window.
        /// </summary>
        MONITOR_DEFAULTTONEAREST = 0x00000002
    }
}
