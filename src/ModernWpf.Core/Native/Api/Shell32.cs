using System;
using System.Runtime.InteropServices;

namespace ModernWpf.Native.Api
{
    /// <summary>
    /// API methods in shell32.dll.
    /// </summary>
    static class Shell32
    {
        static class NativeMethods
        {
            [DllImport("shell32.dll")]
            public static extern UIntPtr SHAppBarMessage(AppBarMessage dwMessage, ref APPBARDATA pData);

            [DllImport("shell32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
            public static extern IntPtr ExtractAssociatedIcon(IntPtr hInst, [MarshalAs(UnmanagedType.LPTStr)] string iconPath, ref int index);

            //[DllImport("shell32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
            //public static extern uint ExtractIconEx([MarshalAs(UnmanagedType.LPTStr)] string lpszFile, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

            [DllImport("shell32.dll", CharSet = CharSet.Auto, BestFitMapping = false)]
            public static extern IntPtr SHGetFileInfo([MarshalAs(UnmanagedType.LPTStr)] string pszPath, FileAttributes dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, ShellFileFlags uFlags);

            //[DllImport("shell32.dll", EntryPoint = "#727")]
            //public extern static HRESULT SHGetImageList(int iImageList, ref Guid riid, out IImageList ppv);

        }

        /// <summary>
        /// Sends an appbar message to the system.
        /// </summary>
        /// <param name="dwMessage">Appbar message value to send.</param>
        /// <param name="pData">A pointer to an APPBARDATA structure. The content of the structure on entry and on exit depends on the value set in the dwMessage parameter. See the individual message pages for specifics.</param>
        /// <returns></returns>
        public static UIntPtr SHAppBarMessage(AppBarMessage dwMessage, ref APPBARDATA pData)
        {
            return NativeMethods.SHAppBarMessage(dwMessage, ref pData);
        }


        /// <summary>
        /// Retrieves a handle to an indexed icon found in a file or an icon found in an associated executable file.
        /// </summary>
        /// <param name="hInst">A handle to the instance of the application calling the function.</param>
        /// <param name="iconPath">The full path and file name of the file that contains the icon. The function extracts the icon handle from that file, or from an executable file associated with that file.</param>
        /// <param name="index">The index of the icon whose handle is to be obtained. If the icon handle is obtained from an executable file, the function stores the icon's identifier in this parameter.</param>
        /// <returns>If the function succeeds, the return value is an icon handle.</returns>
        public static IntPtr ExtractAssociatedIcon(IntPtr hInst, string iconPath, ref int index)
        {
            return NativeMethods.ExtractAssociatedIcon(hInst, iconPath, ref index);
        }

        ///// <summary>
        ///// Creates an array of handles to large or small icons extracted from the specified executable file, DLL, or icon file.
        ///// </summary>
        ///// <param name="filePath">The name of an executable file, DLL, or icon file from which icons will be extracted.</param>
        ///// <param name="iconIndex">The zero-based index of the first icon to extract.</param>
        ///// <param name="phiconLarge">An array of icon handles that receives handles to the large icons extracted from the file. If this parameter is NULL, no large icons are extracted from the file.</param>
        ///// <param name="phiconSmall">An array of icon handles that receives handles to the small icons extracted from the file. If this parameter is NULL, no small icons are extracted from the file.</param>
        ///// <param name="nIcons">The number of icons to be extracted from the file.</param>
        ///// <returns></returns>
        //public static uint ExtractIconEx(string filePath, int iconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons)
        //{
        //    return NativeMethods.ExtractIconEx(filePath, iconIndex, phiconLarge, phiconSmall, nIcons);
        //}


        /// <summary>
        /// Retrieves information about an object in the file system, such as a file, folder, directory, or drive root.
        /// </summary>
        /// <param name="pszPath">The path and file name. Both absolute and relative paths are valid.</param>
        /// <param name="fileAttributes">A combination of one or more file attribute flags.</param>
        /// <param name="psfi">A SHFILEINFO structure to receive the file information.</param>
        /// <param name="cbFileInfo">The size, in bytes, of the SHFILEINFO structure pointed to by the psfi parameter.</param>
        /// <param name="flags">The flags that specify the file information to retrieve.</param>
        /// <returns></returns>
        public static IntPtr SHGetFileInfo(string pszPath, FileAttributes fileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, ShellFileFlags flags)
        {
            return NativeMethods.SHGetFileInfo(pszPath, fileAttributes, ref psfi, cbFileInfo, flags);
        }

    }
}
