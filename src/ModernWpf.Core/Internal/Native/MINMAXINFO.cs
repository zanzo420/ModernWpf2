using ModernWpf.Native;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
	/// <summary>
	/// Contains information about a window's maximized size and position and its minimum and 
	/// maximum tracking size.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	struct MINMAXINFO
	{
		/// <summary>
		/// Reserved; do not use.
		/// </summary>
		public POINT ptReserved;
		/// <summary>
		/// The maximized width (x member) and the maximized height (y member) of the window. 
		/// For top-level windows, this value is based on the width of the primary monitor.
		/// </summary>
		public POINT ptMaxSize;
		/// <summary>
		/// The position of the left side of the maximized window (x member) and the position 
		/// of the top of the maximized window (y member). For top-level windows, this value 
		/// is based on the position of the primary monitor.
		/// </summary>
		public POINT ptMaxPosition;
		/// <summary>
		/// The minimum tracking width (x member) and the minimum tracking height (y member) 
		/// of the window. This value can be obtained programmatically from the system metrics 
		/// SM_CXMINTRACK and SM_CYMINTRACK (see the GetSystemMetrics function).
		/// </summary>
		public POINT ptMinTrackSize;
		/// <summary>
		/// The maximum tracking width (x member) and the maximum tracking height (y member) 
		/// of the window. This value is based on the size of the virtual screen and can be 
		/// obtained programmatically from the system metrics SM_CXMAXTRACK and SM_CYMAXTRACK 
		/// (see the GetSystemMetrics function).
		/// </summary>
		public POINT ptMaxTrackSize;
	}
}
