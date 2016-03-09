using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
	/// <summary>
	/// Values for *WindowLong function.
	/// </summary>
	[Flags]
	enum WindowLong
	{
        /// <summary>
        /// Retrieves the pointer to the window procedure, or a handle representing the pointer to the window procedure. You must use the CallWindowProc function to call the window procedure.
        /// </summary>
        GWL_WNDPROC = (-4),
        /// <summary>
        /// Retrieves a handle to the application instance.
        /// </summary>
        GWL_HINSTANCE = (-6),
        /// <summary>
        /// Retrieves a handle to the parent window, if there is one.
        /// </summary>
        GWL_HWNDPARENT = (-8),
        /// <summary>
        /// Retrieves the window styles.
        /// </summary>
        GWL_STYLE = (-16),
        /// <summary>
        /// Retrieves the extended window styles.
        /// </summary>
        GWL_EXSTYLE = (-20),
        /// <summary>
        /// Retrieves the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
        /// </summary>
        GWL_USERDATA = (-21),
        /// <summary>
        /// Retrieves the identifier of the window.
        /// </summary>
        GWL_ID = (-12)
	}
}
