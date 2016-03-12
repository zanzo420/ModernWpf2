using System;
using System.Collections.Generic;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Values for *ClassLong function.
    /// </summary>
    enum ClassLong
    {
        /// <summary>
        /// Retrieves an ATOM value that uniquely identifies the window class. This is the same atom that the RegisterClassEx function returns.
        /// </summary>
        GCW_ATOM = -32,
        /// <summary>
        /// Retrieves the size, in bytes, of the extra memory associated with the class.
        /// </summary>
        GCL_CBCLSEXTRA = -20,
        /// <summary>
        /// Retrieves the size, in bytes, of the extra window memory associated with each window in the class
        /// </summary>
        GCL_CBWNDEXTRA = -18,
        /// <summary>
        /// Retrieves a handle to the background brush associated with the class.
        /// </summary>
        GCLP_HBRBACKGROUND = -10,
        /// <summary>
        /// Retrieves a handle to the cursor associated with the class.
        /// </summary>
        GCLP_HCURSOR = -12,
        /// <summary>
        /// Retrieves a handle to the icon associated with the class.
        /// </summary>
        GCLP_HICON = -14,
        /// <summary>
        /// Retrieves a handle to the small icon associated with the class.
        /// </summary>
        GCLP_HICONSM = -34,
        /// <summary>
        /// Retrieves a handle to the module that registered the class.
        /// </summary>
        GCLP_HMODULE = -16,
        /// <summary>
        /// Retrieves the pointer to the menu name string. The string identifies the menu resource associated with the class.
        /// </summary>
        GCLP_MENUNAME = -8,
        /// <summary>
        /// Retrieves the window-class style bits.
        /// </summary>
        GCL_STYLE = -26,
        /// <summary>
        /// Retrieves the address of the window procedure, or a handle representing the address of the window procedure. You must use the CallWindowProc function to call the window procedure.
        /// </summary>
        GCLP_WNDPROC = -24

    }
}
