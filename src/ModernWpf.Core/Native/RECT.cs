using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Defines the coordinates of the upper-left and lower-right corners of a rectangle.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        /// <summary>
        /// The x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int left;
        /// <summary>
        /// The y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
        public int top;
        /// <summary>
        /// The x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int right;
        /// <summary>
        /// The y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
        public int bottom;

        /// <summary>
        /// Gets the calculated width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get { return right - left; } }
        /// <summary>
        /// Gets the calculated height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get { return bottom - top; } }


        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Left = {0}, Top = {1}, Width = {2}, Height = {3}.", left, top, Width, Height);
        }

        #region implicits

        /// <summary>
        /// Converts to <see cref="RECT" />.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static implicit operator RECT(System.Windows.Rect rect)
        {
            return new RECT { left = (int)rect.Left, top = (int)rect.Top, right = (int)rect.Right, bottom = (int)rect.Bottom };
        }

        /// <summary>
        /// Converts to <see cref="System.Windows.Rect" />.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <returns></returns>
        public static implicit operator System.Windows.Rect(RECT rect)
        {
            return new System.Windows.Rect(rect.left, rect.top, rect.Width, rect.Height);
        }

        #endregion
    }
}
