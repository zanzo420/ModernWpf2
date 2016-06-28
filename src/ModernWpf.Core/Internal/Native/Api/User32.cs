using System;
using System.Runtime.InteropServices;
using System.Security;

namespace ModernWpf.Native.Api
{
    /// <summary>
    /// API methods in user32.dll.
    /// </summary>
    static class User32
    {
        [SuppressUnmanagedCodeSecurity]
        static class NativeMethods
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetProcessDPIAware();

            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint GetWindowLong(IntPtr hWnd, WindowLong nIndex);

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, WindowLong nIndex);

            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint SetWindowLong(IntPtr hWnd, WindowLong nIndex, uint dwNewLong);

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
            [DllImport("user32.dll", SetLastError = true)]
            public static extern IntPtr SetWindowLongPtr(IntPtr hWnd, WindowLong nIndex, IntPtr dwNewLong);


            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern uint GetClassLong(IntPtr hWnd, ClassLong nIndex);

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetClassLongPtr(IntPtr hWnd, ClassLong nIndex);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern uint SetClassLong(IntPtr hWnd, ClassLong nIndex, uint dwNewLong);

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SetClassLongPtr(IntPtr hWnd, ClassLong nIndex, IntPtr dwNewLong);


            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DestroyIcon(IntPtr hIcon);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            public static extern IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
                int x, int y, int cx, int cy, SetWindowPosOptions flags);

            //[DllImport("user32.dll")]
            //public static extern IntPtr BeginDeferWindowPos(int nNumWindows);

            //[DllImport("user32.dll")]
            //public static extern IntPtr DeferWindowPos(IntPtr hWinPosInfo, IntPtr hWnd, IntPtr hWndInsertAfter,
            //    int x, int y, int cx, int cy, SetWindowPosOptions flags);

            //[DllImport("user32.dll")]
            //[return: MarshalAs(UnmanagedType.Bool)]
            //public static extern bool EndDeferWindowPos(IntPtr hWinPosInfo);

            //[DllImport("user32.dll")]
            //public static extern int GetSystemMetrics(SystemMetric metric);

            [DllImport("User32.dll")]
            public static extern IntPtr MonitorFromWindow(IntPtr handle, MonitorOption option);

            [DllImport("User32.dll")]
            public static extern IntPtr MonitorFromRect([In]ref RECT rect, MonitorOption option);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO info);

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetForegroundWindow([In] IntPtr hWnd);


            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, [MarshalAs(UnmanagedType.Bool)] bool bRedraw);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowPlacement(IntPtr hwnd, ref WINDOWPLACEMENT placement);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowVisible(IntPtr hWnd);

            [DllImport("user32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);

            //[DllImport("user32.dll", SetLastError = true)]
            //public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        }


        #region window long

        /// <summary>
        /// Retrieves information about the specified window. 
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">Index of the value.</param>
        /// <returns></returns>
        public static IntPtr GetWindowLong(IntPtr hWnd, WindowLong nIndex)
        {
            if (PlatformInfo.Is64BitProcess)
            {
                return NativeMethods.GetWindowLongPtr(hWnd, nIndex);
            }
            return new IntPtr(NativeMethods.GetWindowLong(hWnd, nIndex));
        }

        /// <summary>
        /// Changes an attribute of the specified window. The function also sets a value at the specified offset in the extra window memory.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified offset.
        /// If the function fails, the return value is zero. </returns>
        public static IntPtr SetWindowLong(IntPtr hWnd, WindowLong nIndex, IntPtr dwNewLong)
        {
            if (PlatformInfo.Is64BitProcess)
            {
                return NativeMethods.SetWindowLongPtr(hWnd, nIndex, dwNewLong);
            }
            return new IntPtr(NativeMethods.SetWindowLong(hWnd, nIndex, (uint)dwNewLong));
        }

        #endregion

        #region class long

        /// <summary>
        /// Retrieves the specified value from the WNDCLASSEX structure associated with the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">Index of the value.</param>
        /// <returns></returns>
        public static IntPtr GetClassLong(IntPtr hWnd, ClassLong nIndex)
        {
            if (PlatformInfo.Is64BitProcess)
            {
                return NativeMethods.GetClassLongPtr(hWnd, nIndex);
            }
            return new IntPtr(NativeMethods.GetClassLong(hWnd, nIndex));
        }

        /// <summary>
        /// Replaces the specified value at the specified offset in the extra class memory or the WNDCLASSEX structure for the class to which the specified window belongs.
        /// </summary>
        /// <param name="hWnd">A handle to the window and, indirectly, the class to which the window belongs.</param>
        /// <param name="nIndex">The zero-based offset to the value to be set.</param>
        /// <param name="dwNewLong">The replacement value.</param>
        /// <returns>If the function succeeds, the return value is the previous value of the specified offset. If this was not previously set, the return value is zero.</returns>
        public static IntPtr SetClassLong(IntPtr hWnd, ClassLong nIndex, IntPtr dwNewLong)
        {
            if (PlatformInfo.Is64BitProcess)
            {
                return NativeMethods.SetClassLongPtr(hWnd, nIndex, dwNewLong);
            }
            return new IntPtr(NativeMethods.SetClassLong(hWnd, nIndex, (uint)dwNewLong));
        }

        #endregion

        #region taskbar


        // autohide taskbar fix from http://codekong.wordpress.com/2010/11/10/custom-window-style-and-accounting-for-the-taskbar/

        const int ABS_AUTOHIDE = 1;

        public static void AdjustForAutoHideTaskbar(IntPtr hAppMonitor, ref RECT workspace)
        {
            // NOTE: for xp the adjustment for autohidden taskbar makes maximized window movable 
            // but I don't know the way to fix it.
            IntPtr htaskbar = User32.FindWindow("Shell_TrayWnd", null);
            if (htaskbar != IntPtr.Zero)
            {
                IntPtr monitorWithTaskbarOnIt = User32.MonitorFromWindow(htaskbar, MonitorOption.MONITOR_DEFAULTTONEAREST);
                if (hAppMonitor.Equals(monitorWithTaskbarOnIt))
                {
                    APPBARDATA abd = new APPBARDATA();
                    abd.cbSize = (uint)Marshal.SizeOf(abd);
                    abd.hWnd = htaskbar;
                    bool autoHide = (Shell32.SHAppBarMessage(AppBarMessage.ABM_GETSTATE, ref abd).ToUInt32() & ABS_AUTOHIDE) == ABS_AUTOHIDE;

                    if (autoHide)
                    {
                        Shell32.SHAppBarMessage(AppBarMessage.ABM_GETTASKBARPOS, ref abd);
                        var uEdge = GetEdge(ref abd.rc);

                        switch (uEdge)
                        {
                            case AppBarEdge.ABE_LEFT:
                                workspace.left += 2;
                                break;
                            case AppBarEdge.ABE_RIGHT:
                                workspace.right -= 2;
                                break;
                            case AppBarEdge.ABE_TOP:
                                workspace.top += 2;
                                break;
                            case AppBarEdge.ABE_BOTTOM:
                                workspace.bottom -= 2;
                                break;
                        }
                    }
                }
            }
        }
        static AppBarEdge GetEdge(ref RECT rc)
        {
            if (rc.top == rc.left && rc.bottom > rc.right)
                return AppBarEdge.ABE_LEFT;
            else if (rc.top == rc.left && rc.bottom < rc.right)
                return AppBarEdge.ABE_TOP;
            else if (rc.top > rc.left)
                return AppBarEdge.ABE_BOTTOM;
            else
                return AppBarEdge.ABE_RIGHT;
        }
        #endregion

        public static bool SetProcessDPIAware()
        {
            if (PlatformInfo.IsWinVistaUp)
            {
                return NativeMethods.SetProcessDPIAware();
            }
            return true;
        }

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns></returns>
        public static bool SetForegroundWindow(IntPtr hWnd)
        {
            return NativeMethods.SetForegroundWindow(hWnd);
        }

        /// <summary>
        /// Destroys an icon and frees any memory the icon occupied.
        /// </summary>
        /// <param name="hIcon">A handle to the icon to be destroyed. The icon must not be in use.</param>
        /// <returns></returns>
        public static bool DestroyIcon(IntPtr hIcon)
        {
            return NativeMethods.DestroyIcon(hIcon);
        }

        /// <summary>
        /// Places (posts) a message in the message queue associated with the thread that created the specified window and returns without waiting for the thread to process the message.
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. If this parameter is HWND_BROADCAST ((HWND)0xffff), the message is sent to all top-level windows in the system, including disabled or invisible unowned windows, overlapped windows, and pop-up windows; but the message is not sent to child windows.</param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        public static bool PostMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.PostMessage(hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Sends the specified message to a window or windows. 
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window procedure will receive the message. </param>
        /// <param name="msg">The message to be sent.</param>
        /// <param name="wParam">Additional message-specific information.</param>
        /// <param name="lParam">Additional message-specific information.</param>
        /// <returns>The return value specifies the result of the message processing; it depends on the message sent.</returns>
        public static IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Calls the default window procedure to provide default processing for any window messages that an application does not process. This function ensures that every message is processed. DefWindowProc is called with the same parameters received by the window procedure.
        /// </summary>
        /// <param name="hWnd">A handle to the window procedure that received the message.</param>
        /// <param name="msg">The message.</param>
        /// <param name="wParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        /// <param name="lParam">Additional message information. The content of this parameter depends on the value of the Msg parameter.</param>
        /// <returns></returns>
        public static IntPtr DefWindowProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.DefWindowProc(hWnd, msg, wParam, lParam);
        }

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window. These windows are ordered according to their appearance on the screen. The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">A handle to the window to precede the positioned window in the Z order. This parameter must be a window handle or one of the HWndValues.</param>
        /// <param name="x">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels.</param>
        /// <param name="flags">The window sizing and positioning flags.</param>
        /// <returns></returns>
        public static bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
            int x, int y, int cx, int cy, SetWindowPosOptions flags)
        {
            return NativeMethods.SetWindowPos(hWnd, hWndInsertAfter, x, y, cx, cy, flags);
        }

        //public static IntPtr DeferWindowPos(IntPtr whatever, IntPtr hWnd, IntPtr hWndInsertAfter,
        //    int x, int y, int cx, int cy, SetWindowPosOptions flags)
        //{
        //    return NativeMethods.DeferWindowPos(whatever, hWnd, hWndInsertAfter, x, y, cx, cy, flags);
        //}

        //public static IntPtr BeginDeferWindowPos(int count)
        //{
        //    return NativeMethods.BeginDeferWindowPos(count);
        //}

        //public static bool EndDeferWindowPos(IntPtr handle)
        //{
        //    return NativeMethods.EndDeferWindowPos(handle);
        //}

        /// <summary>
        /// Retrieves a handle to the display monitor that has the largest area of intersection with the bounding rectangle of a specified window.
        /// </summary>
        /// <param name="handle">A handle to the window of interest.</param>
        /// <param name="option">Determines the function's return value if the window does not intersect any display monitor.</param>
        /// <returns></returns>
        public static IntPtr MonitorFromWindow(IntPtr handle, MonitorOption option)
        {
            return NativeMethods.MonitorFromWindow(handle, option);
        }

        /// <summary>
        /// Retrieves a handle to the display monitor that has the largest area of intersection with a specified rectangle.
        /// </summary>
        /// <param name="rect">RECT structure that specifies the rectangle of interest in virtual-screen coordinates.</param>
        /// <param name="option">Determines the function's return value if the rectangle does not intersect any display monitor.</param>
        /// <returns></returns>
        public static IntPtr MonitorFromRect(ref RECT rect, MonitorOption option)
        {
            return NativeMethods.MonitorFromRect(ref rect, option);
        }

        /// <summary>
        /// Retrieves information about a display monitor.
        /// </summary>
        /// <param name="hMonitor">A handle to the display monitor of interest.</param>
        /// <param name="info">A MONITORINFO or MONITORINFOEX structure that receives information about the specified display monitor.</param>
        /// <returns></returns>
        public static bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO info)
        {
            return NativeMethods.GetMonitorInfo(hMonitor, ref info);
        }

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings. This function does not search child windows. This function does not perform a case-sensitive search.
        /// To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        public static IntPtr FindWindow(string lpClassName, string lpWindowName)
        {
            return NativeMethods.FindWindow(lpClassName, lpWindowName);
        }

        /// <summary>
        /// The SetWindowRgn function sets the window region of a window. The window region determines the area within the window where the system permits drawing. The system does not display any portion of a window that lies outside of the window region
        /// </summary>
        /// <param name="hWnd">A handle to the window whose window region is to be set.</param>
        /// <param name="hRgn">A handle to a region. The function sets the window region of the window to this region.</param>
        /// <param name="bRedraw">Specifies whether the system redraws the window after setting the window region. If bRedraw is TRUE, the system does so; otherwise, it does not. Typically, you set bRedraw to TRUE if the window is visible.</param>
        /// <returns></returns>
        public static bool SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw)
        {
            return NativeMethods.SetWindowRgn(hWnd, hRgn, bRedraw);
        }

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hwnd">A handle to the window.</param>
        /// <param name="placement">The WINDOWPLACEMENT structure that receives the show state and position information. Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT).</param>
        /// <returns></returns>
        public static bool GetWindowPlacement(IntPtr hwnd, ref WINDOWPLACEMENT placement)
        {
            return NativeMethods.GetWindowPlacement(hwnd, ref placement);
        }

        /// <summary>
        /// Determines the visibility state of the specified window.
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>
        /// </returns>
        public static bool IsWindowVisible(IntPtr hWnd)
        {
            return NativeMethods.IsWindowVisible(hWnd);
        }

        /// <summary>
        /// Retrieves the dimensions of the bounding rectangle of the specified window. The dimensions are given in screen coordinates that are relative to the upper-left corner of the screen.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="rect">RECT structure that receives the screen coordinates of the upper-left and lower-right corners of the window.</param>
        /// <returns></returns>
        public static bool GetWindowRect(IntPtr hWnd, ref RECT rect)
        {
            return NativeMethods.GetWindowRect(hWnd, ref rect);
        }
    }
}