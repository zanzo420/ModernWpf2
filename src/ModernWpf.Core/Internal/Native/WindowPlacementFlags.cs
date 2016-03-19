using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
    [Flags]
    enum WindowPlacementFlags
    {
        /// <summary>
        /// The coordinates of the minimized window may be specified.
        /// This flag must be specified if the coordinates are set in the ptMinPosition member.
        /// </summary>
        WPF_SETMINPOSITION = 0x1,
        /// <summary>
        /// The restored window will be maximized, regardless of whether it was maximized before it was minimized. This setting is only valid the next time the window is restored. It does not change the default restoration behavior.
        /// This flag is only valid when the SW_SHOWMINIMIZED value is specified for the showCmd member.
        /// </summary>
        WPF_RESTORETOMAXIMIZED = 0x2,
        /// <summary>
        /// If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
        /// </summary>
        WPF_ASYNCWINDOWPLACEMENT = 0x4
    }
}
