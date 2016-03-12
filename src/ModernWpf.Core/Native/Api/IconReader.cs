using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ModernWpf.Native.Api
{
    // this is modified from http://www.codeproject.com/Articles/2532/Obtaining-and-managing-file-and-folder-icons-using.

    /// <summary>
    /// Provides static methods to read system icons for both folders and files.
    /// </summary>
    /// <example>
    /// <code>IconReader.GetFileIcon("c:\\general.xls");</code>
    /// </example>
    static class IconReader
    {
        /// <summary>
        /// Options to specify the size of icons to return.
        /// </summary>
        public enum IconSize
        {
            /// <summary>
            /// Specify large icon - 32 pixels by 32 pixels.
            /// </summary>
            Large = 0,
            /// <summary>
            /// Specify small icon - 16 pixels by 16 pixels.
            /// </summary>
            Small = 1,
            ///// <summary>
            ///// Specify extra large icon - 48 pixels by 48 pixels.
            ///// </summary>
            //ExtraLarge = 2,
            ///// <summary>
            ///// Specify jumbo icon - 256 pixels by 256 pixels (Vista or later).
            ///// </summary>
            //Jumbo = 4
        }

        /// <summary>
        /// Options to specify whether folders should be in the open or closed state.
        /// </summary>
        public enum FolderType
        {
            /// <summary>
            /// Specify open folder.
            /// </summary>
            Open = 0,
            /// <summary>
            /// Specify closed folder.
            /// </summary>
            Closed = 1
        }

        /// <summary>
        /// Returns an icon for a given file - indicated by the name parameter.
        /// This works whether the file exists or not.
        /// </summary>
        /// <param name="name">Pathname for file.</param>
        /// <param name="size">Large or small</param>
        /// <param name="linkOverlay">Whether to include the link icon</param>
        /// <returns>System.Drawing.Icon</returns>
        public static ImageSource GetFileIcon(string name, IconSize size, bool linkOverlay)
        {
            var info = GetFileIconSHInfo(name, size, linkOverlay);

            try
            {
                return GetImageSource(info.hIcon);
            }
            finally
            {
                if (info.hIcon != IntPtr.Zero)
                {
                    User32.DestroyIcon(info.hIcon);
                }
            }
        }

        private static SHFILEINFO GetFileIconSHInfo(string name, IconSize size, bool linkOverlay)
        {
            var flags = ShellFileFlags.SHGFI_ICON | ShellFileFlags.SHGFI_USEFILEATTRIBUTES;

            if (linkOverlay) { flags |= ShellFileFlags.SHGFI_LINKOVERLAY; }

            /* Check the size specified for return. */
            if (size == IconSize.Small)
            {
                flags |= ShellFileFlags.SHGFI_SMALLICON;
            }
            else
            {
                flags |= ShellFileFlags.SHGFI_LARGEICON;
            }

            var info = new SHFILEINFO();
            Shell32.SHGetFileInfo(name, FileAttributes.FILE_ATTRIBUTE_NORMAL, ref info, (uint)Marshal.SizeOf(info), flags);
            return info;
        }
        
        ///// <summary>
        ///// Used to access system folder icons.
        ///// </summary>
        ///// <param name="size">Specify large or small icons.</param>
        ///// <param name="folderType">Specify open or closed FolderType.</param>
        ///// <returns>System.Drawing.Icon</returns>
        //public static ImageSource GetFolderIcon(IconSize size, FolderType folderType)
        //{
        //    var info = GetFolderIconSHInfo(size, folderType);

        //    try
        //    {
        //        return GetImageSource(info.hIcon);
        //    }
        //    finally
        //    {
        //        if (info.hIcon != IntPtr.Zero)
        //        {
        //            User32.DestroyIcon(info.hIcon);
        //        }
        //    }
        //}

        //private static SHFILEINFO GetFolderIconSHInfo(IconSize size, FolderType folderType)
        //{
        //    // Need to add size check, although errors generated at present!
        //    var flags = ShellFileFlags.SHGFI_ICON | ShellFileFlags.SHGFI_USEFILEATTRIBUTES;

        //    if (FolderType.Open == folderType)
        //    {
        //        flags |= ShellFileFlags.SHGFI_OPENICON;
        //    }

        //    if (IconSize.Small == size)
        //    {
        //        flags |= ShellFileFlags.SHGFI_SMALLICON;
        //    }
        //    else
        //    {
        //        flags |= ShellFileFlags.SHGFI_LARGEICON;
        //    }

        //    // Get the folder icon
        //    var info = new SHFILEINFO();
        //    Shell32.SHGetFileInfo(Environment.GetFolderPath(Environment.SpecialFolder.System), FileAttributes.FILE_ATTRIBUTE_DIRECTORY, ref info, (uint)Marshal.SizeOf(info), flags);
        //    return info;
        //}

        
        private static ImageSource GetImageSource(IntPtr hIcon)
        {
            ImageSource img = null;
            if (hIcon != IntPtr.Zero)
            {
                img = Imaging.CreateBitmapSourceFromHIcon(hIcon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                img.Freeze();
            }
            return img;
        }


        // the System IImageList object from the Shell:
        //static Guid __iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

        //public static ImageSource GetFileIconWpfTest(string fileName, IconSize size, bool shortCut)
        //{
        //    ImageSource img = null;

        //    SHFILEINFO shinfo = new SHFILEINFO();

        //    ShellFileFlags flags = ShellFileFlags.SHGFI_SYSICONINDEX;

        //    //if (!checkDisk)  // This does not seem to work. If I try it, a folder icon is always returned.
        //        flags |= ShellFileFlags.SHGFI_USEFILEATTRIBUTES | ShellFileFlags.SHGFI_LINKOVERLAY;


        //    var res = Shell32.SHGetFileInfo(fileName, FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL, ref shinfo, (uint)Marshal.SizeOf(shinfo), flags);
        //    if (res != IntPtr.Zero)
        //    {
        //        var iconIndex = shinfo.iIcon;


        //        IImageList iml;
        //        var hres = Shell32.NativeMethods.SHGetImageList((int)size, ref __iidImageList, out  iml);
        //        if (hres == HRESULT.S_OK)
        //        {
        //            IntPtr hIcon = IntPtr.Zero;
        //            int ILD_TRANSPARENT = 1;
        //            hres = iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);
        //            if (hres == HRESULT.S_OK)
        //            {

        //                img = GetImageSource(hIcon);
        //                User32.DestroyIcon(hIcon);
        //            }
        //            Marshal.FinalReleaseComObject(iml);
        //        }
        //    }
        //    return img;

        //}
    }
}