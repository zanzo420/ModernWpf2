using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Defines the x- and y- coordinates of a point.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="POINT"/> struct.
        /// </summary>
        /// <param name="x">The x location.</param>
        /// <param name="y">The y location.</param>
        public POINT(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// The x-coordinate of the point.
        /// </summary>
        public int x;
        /// <summary>
        /// The y-coordinate of the point.
        /// </summary>
        public int y;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}.", x, y);
        }

        #region implicits

        /// <summary>
        /// Converts to <see cref="POINT"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static implicit operator POINT(System.Windows.Point point)
        {
            return new POINT((int)point.X, (int)point.Y);
        }

        /// <summary>
        /// Converts to <see cref="System.Windows.Point"/>.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <returns></returns>
        public static implicit operator System.Windows.Point(POINT point)
        {
            return new System.Windows.Point(point.x, point.y);
        }

        #endregion
    }
}
