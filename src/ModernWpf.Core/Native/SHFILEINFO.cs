using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ModernWpf.Native
{
    /// <summary>
    /// Contains information about a file object.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    struct SHFILEINFO
    {
        /// <summary>
        /// A handle to the icon that represents the file. 
        /// </summary>
        public IntPtr hIcon;

        /// <summary>
        /// The index of the icon image within the system image list.
        /// </summary>
        public int iIcon;

        /// <summary>
        /// An array of values that indicates the attributes of the file object.
        /// </summary>
        public uint dwAttributes;

        /// <summary>
        /// A string that contains the name of the file as it appears in the Windows Shell, or the path and file name of the file that contains the icon representing the file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = BasicValues.MAX_PATH)]
        public string szDisplayName;

        /// <summary>
        /// A string that describes the type of file.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }
}
