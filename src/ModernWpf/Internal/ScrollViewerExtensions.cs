using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ModernWpf
{
    static class ScrollViewerExtensions
    {
        internal static bool CanVScrollDown(this ScrollViewer scroller)
        {
            return scroller.ScrollableHeight > 0 && scroller.VerticalOffset < scroller.ScrollableHeight;
        }
        internal static bool CanHScrollRight(this ScrollViewer scroller)
        {
            return scroller.ScrollableWidth > 0 && scroller.HorizontalOffset < scroller.ScrollableWidth;
        }

        internal static bool CanVScrollUp(this ScrollViewer scroller)
        {
            return scroller.ScrollableHeight > 0 && scroller.VerticalOffset > 0;
        }
        internal static bool CanHScrollLeft(this ScrollViewer scroller)
        {
            return scroller.ScrollableWidth > 0 && scroller.HorizontalOffset > 0;
        }



    }
}
