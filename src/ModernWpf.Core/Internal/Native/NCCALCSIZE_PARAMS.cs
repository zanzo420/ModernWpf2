using ModernWpf.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
	/// <summary>
	/// Contains information that an application can use while processing the WM_NCCALCSIZE 
	/// message to calculate the size, position, and valid contents of the client area of a window.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct NCCALCSIZE_PARAMS
	{
		/// <summary>
		/// Contains the new coordinates of a window that has been moved or resized, 
		/// that is, it is the proposed new window coordinates.
		/// </summary>
		public RECT rectProposed;
		/// <summary>
		/// Contains the coordinates of the window before it was moved or resized.
		/// </summary>
		public RECT rectBeforeMove;
		/// <summary>
		/// Contains the coordinates of the window's client area before the window 
		/// was moved or resized.
		/// </summary>
		public RECT rectClientBeforeMove;
		/// <summary>
		/// Pointer to a WINDOWPOS structure that contains the size and position 
		/// values specified in the operation that moved or resized the window.
		/// </summary>
		public IntPtr lpPos;
	}
}
