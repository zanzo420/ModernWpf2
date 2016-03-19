using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Return values for WM_MOUSEACTIVATE message.
    /// </summary>
    enum MouseActivate
    {
        /// <summary>
        /// Activates the window, and does not discard the mouse message.
        /// </summary>
        MA_ACTIVATE = 1,
        /// <summary>
        /// Activates the window, and discards the mouse message.
        /// </summary>
        MA_ACTIVATEANDEAT = 2,
        /// <summary>
        /// Does not activate the window, and does not discard the mouse message.
        /// </summary>
        MA_NOACTIVATE = 3,
        /// <summary>
        /// Does not activate the window, but discards the mouse message.
        /// </summary>
        MA_NOACTIVATEANDEAT = 4
    }
}
