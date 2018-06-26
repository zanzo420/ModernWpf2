using ModernWpf.Controls;
using ModernWpf.Native;
using ModernWpf.Native.Api;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;

namespace ModernWpf
{
    sealed class BorderManager : DependencyObject
    {
        #region DP

        public static BorderManager GetManager(DependencyObject obj)
        {
            return (BorderManager)obj.GetValue(ManagerProperty);
        }

        public static void SetManager(DependencyObject obj, BorderManager value)
        {
            obj.SetValue(ManagerProperty, value);
        }

        public static readonly DependencyProperty ManagerProperty =
            DependencyProperty.RegisterAttached("Manager", typeof(BorderManager), typeof(BorderManager), new FrameworkPropertyMetadata(null, ManagerChanged));

        private static void ManagerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (d != null)
            {
                var oldManager = e.OldValue as BorderManager;
                var newManager = e.NewValue as BorderManager;

                if (oldManager != newManager)
                {
                    if (oldManager != null)
                    {
                        oldManager.DetatchWindow();
                    }

                    if (newManager != null)
                    {
                        newManager.AttachWindow(window);
                    }
                }
            }
        }

        #endregion


        internal Window ContentWindow { get; private set; }
        internal IntPtr hWndContent { get; private set; }


        ResizeGrip _resizeGrip;

        BorderWindow _left;
        BorderWindow _top;
        BorderWindow _right;
        BorderWindow _bottom;
        DispatcherTimer _showTimer;

        public BorderManager()
        {
            _showTimer = new DispatcherTimer();
            // Magic # for windows animation duration.
            // This is used to not show border before content window 
            // is fully restored from min/max states
            _showTimer.Interval = TimeSpan.FromMilliseconds(200);
            _showTimer.Tick += (s, e) =>
            {
                ShowBorders();
            };
        }

        bool _visible;
        void ShowBorders()
        {
            _showTimer.Stop();
            if (!_visible)
            {
                _visible = true;
                _left.Owner = ContentWindow;
                _left.ShowNoActivate();
                _top.Owner = ContentWindow;
                _top.ShowNoActivate();
                _right.Owner = ContentWindow;
                _right.ShowNoActivate();
                _bottom.Owner = ContentWindow;
                _bottom.ShowNoActivate();
            }
        }
        void HideBorders()
        {
            _showTimer.Stop();

            _left.Hide();
            _left.Owner = null;
            _top.Hide();
            _top.Owner = null;
            _right.Hide();
            _right.Owner = null;
            _bottom.Hide();
            _bottom.Owner = null;
            _visible = false;
        }

        void UpdatePosn()
        {
            // use GetWindowRect to work correctly with aero snap
            // since GetWindowPlacement doesn't change
            var rcNative = default(RECT);
            if (User32.GetWindowRect(hWndContent, ref rcNative))
            {
                Rect rcWpf = TranslateToWpf(ref rcNative);
                //var scale = UIHooks.GetWindowDpiScale(ContentWindow);

                var leftW = _left.PadSize;
                var topH = _top.PadSize;
                var rightW = _right.PadSize;
                var botH = _bottom.PadSize;

                _left.UpdatePosnWpf(
                    rcWpf.Left - leftW,
                    rcWpf.Top - topH,
                    leftW,
                    rcWpf.Height + topH + botH);
                _top.UpdatePosnWpf(
                    rcWpf.Left - leftW,
                    rcWpf.Top - topH,
                    rcWpf.Width + leftW + rightW,
                    topH);
                _right.UpdatePosnWpf(
                    rcWpf.Right,
                    rcWpf.Top - topH,
                    rightW,
                    rcWpf.Height + topH + botH);
                _bottom.UpdatePosnWpf(
                    rcWpf.Left - leftW,
                    rcWpf.Bottom,
                    rcWpf.Width + leftW + rightW,
                    botH);

                //Rect rcWpf = rcNative;
                //IntPtr blah = User32.BeginDeferWindowPos(4);
                //blah = _left.UpdatePosnExp(blah, rcWpf.Left - _left.PadSize, rcWpf.Top - _left.PadSize, _left.PadSize, rcWpf.Height + 2 * _left.PadSize);
                //blah = _top.UpdatePosnExp(blah, rcWpf.Left - _top.PadSize, rcWpf.Top - _top.PadSize, rcWpf.Width + 2 * _top.PadSize, _top.PadSize);
                //blah = _right.UpdatePosnExp(blah, rcWpf.Right, rcWpf.Top - _right.PadSize, _right.PadSize, rcWpf.Height + 2 * _right.PadSize);
                //blah = _bottom.UpdatePosnExp(blah, rcWpf.Left - _bottom.PadSize, rcWpf.Bottom, rcWpf.Width + 2 * _bottom.PadSize, _bottom.PadSize);
                //User32.EndDeferWindowPos(blah);

                if (SystemParameters.MinimizeAnimation)
                {
                    _showTimer.Start();
                }
                else
                {
                    ShowBorders();
                }
            }

        }



        internal void UpdateChrome(Chrome chrome)
        {
            Debug.WriteLine("BorderManager updating chrome.");
            if (_left != null) _left.UpdateChromeBindings(chrome);
            if (_top != null) _top.UpdateChromeBindings(chrome);
            if (_right != null) _right.UpdateChromeBindings(chrome);
            if (_bottom != null) _bottom.UpdateChromeBindings(chrome);
        }

        void AttachWindow(Window contentWindow)
        {
            ContentWindow = contentWindow;
            ContentWindow.Closed += ContentWindow_Closed;
            ContentWindow.ContentRendered += ContentWindow_ContentRendered;

            hWndContent = new WindowInteropHelper(contentWindow).Handle;
            if (hWndContent == IntPtr.Zero)
            {
                ContentWindow.SourceInitialized += ContentWindow_SourceInitialized;
            }
            else
            {
                InitBorders();
            }
        }

        void DetatchWindow()
        {
            if (ContentWindow != null)
            {
                ContentWindow.Closed -= ContentWindow_Closed;
                ContentWindow.ContentRendered -= ContentWindow_ContentRendered;
                if (hWndContent != IntPtr.Zero)
                {
                    try
                    {
                        var hSrc = HwndSource.FromHwnd(hWndContent);
                        if (hSrc != null)
                        {
                            hSrc.RemoveHook(WndProc);
                        }
                    }
                    catch { }
                }
                ContentWindow = null;
            }

            _left?.Close();
            _top?.Close();
            _right?.Close();
            _bottom?.Close();
        }

        private void ContentWindow_Closed(object sender, EventArgs e)
        {
            BorderManager.SetManager((Window)sender, null);
        }

        private void ContentWindow_ContentRendered(object sender, EventArgs e)
        {
            _resizeGrip = ((Window)sender).FindChildInVisualTree<ResizeGrip>();
            // hack to make sure the border is shown after content is shown
            UpdatePosn();
        }

        private void ContentWindow_SourceInitialized(object sender, EventArgs e)
        {
            Window win = (Window)sender;
            win.SourceInitialized -= ContentWindow_SourceInitialized;
            hWndContent = new WindowInteropHelper(win).Handle;
            InitBorders();
        }

        void InitBorders()
        {
            _left = new BorderWindow(this) { Side = BorderSide.Left };
            _top = new BorderWindow(this) { Side = BorderSide.Top };
            _right = new BorderWindow(this) { Side = BorderSide.Right };
            _bottom = new BorderWindow(this) { Side = BorderSide.Bottom };

            var wpl = default(WINDOWPLACEMENT);
            wpl.length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT));

            if (User32.GetWindowPlacement(hWndContent, ref wpl))
            {
                UpdateClipRegion(hWndContent, wpl, ClipRegionChangeType.FromPropertyChange);
            }

            var hSrc = HwndSource.FromHwnd(hWndContent);
            hSrc.AddHook(WndProc);

            // SWP_DRAWFRAME makes window bg really transparent (visible during resize) and not black
            User32.SetWindowPos(hWndContent, IntPtr.Zero, 0, 0, 0, 0,
                SetWindowPosOptions.SWP_NOOWNERZORDER |
                SetWindowPosOptions.SWP_DRAWFRAME |
                SetWindowPosOptions.SWP_NOACTIVATE |
                SetWindowPosOptions.SWP_NOZORDER |
                SetWindowPosOptions.SWP_NOMOVE |
                SetWindowPosOptions.SWP_NOSIZE);
        }


        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            IntPtr retVal = IntPtr.Zero;
            if (!handled)
            {
                try
                {
                    var wmsg = (WindowMessage)msg;
                    //Debug.WriteLine(wmsg);
                    switch (wmsg)
                    {
                        case WindowMessage.WM_NCCALCSIZE:
                            HandleNcCalcSize(hwnd, wParam, lParam);
                            handled = true;
                            break;
                        //case WindowMessage.WM_SIZING:
                        //    UpdatePosn();
                        //    break;
                        //case WindowMessage.WM_WINDOWPOSCHANGING:
                        case WindowMessage.WM_WINDOWPOSCHANGED:
                            //HandleWindowPosChangedOld(hwnd, lParam);
                            HandleWindowPosChanged(hwnd, lParam);
                            break;
                        case WindowMessage.WM_NCHITTEST:
                            retVal = new IntPtr((int)HandleNcHitTest(lParam));
                            handled = true;
                            break;
                        //case WindowMessage.WM_NCLBUTTONDOWN:
                        //    if ((ChromeHitTest)wParam.ToInt32() == ChromeHitTest.SystemMenu)
                        //    {
                        //        // display sys menu
                        //        User32.PostMessage(hwnd, (uint)WindowMessage.WM_POPUPSYSTEMMENU, IntPtr.Zero, lParam);
                        //        //handled = true;
                        //    }
                        //    break;
                        case WindowMessage.WM_NCRBUTTONDOWN:
                            switch ((ChromeHitTest)wParam.ToInt32())
                            {
                                case ChromeHitTest.Caption:
                                case ChromeHitTest.SystemMenu:
                                    // display sys menu
                                    User32.PostMessage(hwnd, (uint)WindowMessage.WM_POPUPSYSTEMMENU, IntPtr.Zero, lParam);
                                    //handled = true;
                                    break;
                            }
                            break;
                        case WindowMessage.WM_NCUAHDRAWCAPTION:
                        case WindowMessage.WM_NCUAHDRAWFRAME:
                            // undocumented stuff for non-dwm themes that will sometimes draw old control buttons
                            handled = true;
                            break;
                        case WindowMessage.WM_NCPAINT:
                            // prevent non-dwm flicker
                            handled = !Dwmapi.IsCompositionEnabled;
                            break;
                        case WindowMessage.WM_NCACTIVATE:
                            // handled to prevent default non-client border from showing in classic mode
                            // wParam False means draw inactive title bar (which we do nothing).
                            retVal = HandleNcActivate(hwnd, msg, wParam, retVal);
                            handled = true;
                            break;
                        case WindowMessage.WM_DWMCOMPOSITIONCHANGED:
                            SetRegion(hwnd, 0, 0, true);
                            break;
                        case WindowMessage.WM_ERASEBKGND:
                            // prevent more flickers?
                            handled = true;
                            break;
                        case WindowMessage.WM_MOUSEHWHEEL:
                            // do our own horizontal wheel event
                            var element = Mouse.DirectlyOver;
                            if (element != null)
                            {
                                try
                                {
                                    //var delta = wParam.ToInt32() >> 16;
                                    var delta = ((int)(wParam.ToInt64() & 0xffffffff)) >> 16;
                                    var arg = new MouseWheelEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount, delta)
                                    {
                                        RoutedEvent = UIHooks.PreviewMouseHWheelEvent
                                    };
                                    element.RaiseEvent(arg);
                                    if (!arg.Handled)
                                    {
                                        arg.RoutedEvent = UIHooks.MouseHWheelEvent;
                                        arg.Handled = false;
                                        element.RaiseEvent(arg);

                                        handled = arg.Handled;
                                    }
                                }
                                catch { }
                            }
                            break;
                    }
                }
                catch { }
            }
            return retVal;
        }

        private ChromeHitTest HandleNcHitTest(IntPtr lParam)
        {
            Point screenPoint = lParam.ToPoint();
            Point windowPoint = ContentWindow.PointFromScreen(screenPoint);

            ChromeHitTest location = ChromeHitTest.Client;
            var element = ContentWindow.InputHitTest(windowPoint);

            if (element != null)
            {
                location = Chrome.GetHitTestType((DependencyObject)element);
            }

            if (_resizeGrip != null && _resizeGrip.Visibility == System.Windows.Visibility.Visible &&
                VisualTreeHelper.HitTest(_resizeGrip, _resizeGrip.PointFromScreen(screenPoint)) != null)
            {
                location = _resizeGrip.FlowDirection == System.Windows.FlowDirection.LeftToRight ?
                    ChromeHitTest.BottomRight : ChromeHitTest.BottomLeft;
            }

            //Debug.WriteLine(location);
            return location;
        }

        private static void HandleNcCalcSize(IntPtr hwnd, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == BasicValues.TRUE)
            {
                var wpl = default(WINDOWPLACEMENT);
                wpl.length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                if (User32.GetWindowPlacement(hwnd, ref wpl))
                {
                    // detect if maximizd and set to workspace remove padding
                    if (wpl.showCmd == ShowWindowOption.SW_MAXIMIZE)
                    {
                        // in multi-monitor case where app is minimized to a monitor on the right/bottom 
                        // the MonitorFromWindow will incorrectly return the leftmost monitor due to the minimized
                        // window being set to the far left, so this routine now uses the proposed rect to correctly
                        // identify the real nearest monitor to calc the nc size.

                        NCCALCSIZE_PARAMS para = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(lParam, typeof(NCCALCSIZE_PARAMS));

                        var windowRect = para.rectProposed;
                        IntPtr hMonitor = User32.MonitorFromRect(ref windowRect, MonitorOption.MONITOR_DEFAULTTONEAREST);// MonitorFromWindow(hWnd, 2);
                        if (hMonitor != IntPtr.Zero)
                        {
                            MONITORINFO lpmi = new MONITORINFO();
                            lpmi.cbSize = (uint)Marshal.SizeOf(typeof(MONITORINFO));
                            if (User32.GetMonitorInfo(hMonitor, ref lpmi))
                            {
                                var workArea = lpmi.rcWork;
                                User32.AdjustForAutoHideTaskbar(hMonitor, ref workArea);
                                Debug.WriteLine($@"NCCalc original = {para.rectProposed}, new ={workArea}");
                                para.rectProposed = workArea;
                                Marshal.StructureToPtr(para, lParam, true);
                            }
                        }
                    }
                }
            }
        }

        bool _inHiding = false;
        ShowWindowOption _lastShowCmd;
        enum ClipRegionChangeType
        {
            FromSize,
            FromPosition,
            FromPropertyChange
        }

        private void UpdateClipRegion(IntPtr hWnd, WINDOWPLACEMENT placement, ClipRegionChangeType changeType)
        {
            if (placement.showCmd == ShowWindowOption.SW_MAXIMIZE)
            {
                ClipMaximizedRegion(hWnd);
            }
            else if (changeType != ClipRegionChangeType.FromSize &&
                changeType != ClipRegionChangeType.FromPropertyChange &&
                _lastShowCmd == placement.showCmd)
            {
                // do nothing
            }
            else
            {
                ClearClipRegion(hWnd);
            }
            _lastShowCmd = placement.showCmd;
        }

        bool _hasClip;

        private void ClipMaximizedRegion(IntPtr hwnd)
        {
            RECT winRC = default(RECT);
            User32.GetWindowRect(hwnd, ref winRC);
            IntPtr rectRgnIndirect = IntPtr.Zero;
            try
            {
                rectRgnIndirect = Gdi32.CreateRectRgnIndirect(ref winRC);
                _hasClip = User32.SetWindowRgn(hwnd, rectRgnIndirect, User32.IsWindowVisible(hWnd));
            }
            finally
            {
                if (rectRgnIndirect != IntPtr.Zero) Gdi32.DeleteObject(rectRgnIndirect);
            }
        }
        private void ClearClipRegion(IntPtr hwnd)
        {
            if (_hasClip)
            {
                var draw = User32.IsWindowVisible(hwnd);
                Debug.WriteLine("Should clear region redraw=" + draw);
                User32.SetWindowRgn(hwnd, IntPtr.Zero, draw);
                _hasClip = false;
            }
        }

        private void HandleWindowPosChanged(IntPtr hwnd, IntPtr lParam)
        {
            WINDOWPOS windowpos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));
            var wpl = default(WINDOWPLACEMENT);
            wpl.length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT));

            if (User32.GetWindowPlacement(hwnd, ref wpl))
            {
                if ((windowpos.flags & SetWindowPosOptions.SWP_NOSIZE) != SetWindowPosOptions.SWP_NOSIZE)
                {
                    UpdateClipRegion(hwnd, wpl, ClipRegionChangeType.FromSize);
                }
                else if ((windowpos.flags & SetWindowPosOptions.SWP_NOMOVE) != SetWindowPosOptions.SWP_NOMOVE)
                {
                    UpdateClipRegion(hwnd, wpl, ClipRegionChangeType.FromPosition);
                }

                if (windowpos.flags.HasFlag(SetWindowPosOptions.SWP_HIDEWINDOW))
                {
                    // necessary to keep track on whether window is hidden
                    // since this msg is received again after being hidden.
                    _inHiding = true;
                }
                if (windowpos.flags.HasFlag(SetWindowPosOptions.SWP_SHOWWINDOW))
                {
                    _inHiding = false;
                }

                if (_inHiding)
                {
                    HideBorders();
                }
                else
                {
                    CommandManager.InvalidateRequerySuggested();
                    switch (wpl.showCmd)
                    {
                        case ShowWindowOption.SW_SHOWNORMAL:
                            //Debug.WriteLine("Should reposn shadow");
                            UpdatePosn();
                            break;
                        case ShowWindowOption.SW_MAXIMIZE:
                        case ShowWindowOption.SW_MINIMIZE:
                        case ShowWindowOption.SW_SHOWMINIMIZED:
                            HideBorders();
                            break;
                    }
                }

            }
        }
        //private void HandleWindowPosChangedOld(IntPtr hwnd, IntPtr lParam)
        //{
        //    var windowpos = (WINDOWPOS)Marshal.PtrToStructure(lParam, typeof(WINDOWPOS));

        //    if ((windowpos.flags & SetWindowPosOptions.SWP_NOSIZE) != SetWindowPosOptions.SWP_NOSIZE)
        //    {
        //        SetRegion(hwnd, windowpos.cx, windowpos.cy, false);
        //    }


        //    if (windowpos.flags.HasFlag(SetWindowPosOptions.SWP_HIDEWINDOW))
        //    {
        //        // necessary to keep track on whether window is hidden
        //        // since this msg is received again after being hidden.
        //        _inHiding = true;
        //    }
        //    if (windowpos.flags.HasFlag(SetWindowPosOptions.SWP_SHOWWINDOW))
        //    {
        //        _inHiding = false;
        //    }

        //    if (_inHiding)
        //    {
        //        HideBorders();
        //    }
        //    else
        //    {
        //        var wpl = default(WINDOWPLACEMENT);
        //        wpl.length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT));

        //        if (User32.GetWindowPlacement(hwnd, ref wpl))
        //        {
        //            //Debug.WriteLine("Chrome {0} placement cmd {1}, flag {2}.", _id, wpl.showCmd, wpl.flags);

        //            switch (wpl.showCmd)
        //            {
        //                case ShowWindowOption.SW_SHOWNORMAL:
        //                    //Debug.WriteLine("Should reposn shadow");
        //                    UpdatePosn();
        //                    break;
        //                case ShowWindowOption.SW_MAXIMIZE:
        //                case ShowWindowOption.SW_MINIMIZE:
        //                case ShowWindowOption.SW_SHOWMINIMIZED:
        //                    HideBorders();
        //                    break;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// translate screen pixels to wpf units for high-dpi scaling.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        private Rect TranslateToWpf(ref RECT r)
        {
            var source = PresentationSource.FromVisual(ContentWindow);
            if (source != null)
            {
                var transform = source.CompositionTarget.TransformToDevice;
                if (!transform.IsIdentity)
                {
                    var xScale = transform.M11;
                    var yScale = transform.M22;

                    return new Rect(r.left / xScale, r.top / yScale, r.Width / xScale, r.Height / yScale);
                }
            }
            return new Rect(r.left, r.top, r.Width, r.Height);
        }

        private IntPtr HandleNcActivate(IntPtr hwnd, int msg, IntPtr wParam, IntPtr retVal)
        {
            //Debug.WriteLine(hwnd.ToInt64() + " wparam " + wParam.ToInt32());

            if (wParam == BasicValues.FALSE)
            {
                retVal = BasicValues.TRUE;
            }
            else
            {
                // Also skip default wndproc on maximized window to prevent non-dwm theme titlebar being drawn
                if (ContentWindow.WindowState != WindowState.Maximized ||
                    Dwmapi.IsCompositionEnabled)
                {
                    retVal = User32.DefWindowProc(hwnd, (uint)msg, wParam, new IntPtr(-1));
                }
            }
            return retVal;
        }


        static void SetRegion(IntPtr hwnd, int width, int height, bool force)
        {
            if (Dwmapi.IsCompositionEnabled)
            {
                //clear
                if (force)
                {
                    User32.SetWindowRgn(hwnd, IntPtr.Zero, User32.IsWindowVisible(hwnd));
                }
            }
            else
            {
                var wpl = default(WINDOWPLACEMENT);
                wpl.length = (uint)Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                if (User32.GetWindowPlacement(hwnd, ref wpl))
                {
                    if (wpl.showCmd == ShowWindowOption.SW_MAXIMIZE)
                    {
                        //clear
                        User32.SetWindowRgn(hwnd, IntPtr.Zero, User32.IsWindowVisible(hwnd));
                    }
                    else
                    {
                        // always rectangle to prevent rounded corners for some themes
                        IntPtr rgn = IntPtr.Zero;
                        try
                        {
                            if (width == 0 || height == 0)
                            {
                                RECT r = default(RECT);
                                User32.GetWindowRect(hwnd, ref r);
                                width = r.Width;
                                height = r.Height;
                            }

                            rgn = Gdi32.CreateRectRgn(0, 0, width, height);
                            User32.SetWindowRgn(hwnd, rgn, User32.IsWindowVisible(hwnd));
                        }
                        finally
                        {
                            if (rgn != IntPtr.Zero)
                            {
                                Gdi32.DeleteObject(rgn);
                            }
                        }
                    }
                }
            }
        }

    }
}
