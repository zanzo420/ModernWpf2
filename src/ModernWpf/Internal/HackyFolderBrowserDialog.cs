using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ModernWpf
{
    /// <summary>
    /// Hacks winform's dialog to become folder selector.
    /// </summary>
    /// <seealso cref="Microsoft.Win32.CommonDialog" />
    class HackyFolderBrowserDialog : Microsoft.Win32.CommonDialog
    {
        public string SelectedPath { get; private set; }
        //public string[] SelectedPaths { get; private set; }
        public string InitialDirectory { get; set; }
        public string Title { get; set; }
        //public bool MultiSelect { get; set; }

        public override void Reset()
        {
            SelectedPath = null;
            //SelectedPaths = null;
            InitialDirectory = null;
            Title = null;
            //MultiSelect = false;
        }

        static bool __errored;
        public static bool IsSupported
        {
            get
            {
                return PlatformInfo.IsWinVistaUp && 
                    !__errored &&
                    HiddenDialogType != null && DialogEventsType != null;
            }
        }

        static readonly Type PubDialogType = typeof(System.Windows.Forms.FileDialog);
        static readonly Assembly FormAss = PubDialogType.Assembly;
        static readonly Type HiddenDialogType = FormAss.GetType("System.Windows.Forms.FileDialogNative+IFileDialog");
        static readonly Type DialogEventsType = FormAss.GetType("System.Windows.Forms.FileDialog+VistaDialogEvents");

        const uint FOS_PICKFOLDERS = 0x00000020;

        protected override bool RunDialog(IntPtr hwndOwner)
        {
            using (var setupDlg = new System.Windows.Forms.OpenFileDialog())
            {
                if (!string.IsNullOrEmpty(InitialDirectory))
                {
                    setupDlg.InitialDirectory = InitialDirectory;
                }
                setupDlg.Title = Title;
                setupDlg.AddExtension = false;
                setupDlg.CheckFileExists = false;
                setupDlg.DereferenceLinks = true;
                //setupDlg.Multiselect = MultiSelect;

                try
                {
                    object realDlg = setupDlg.GetType().CallMethod("CreateVistaDialog", setupDlg);
                    setupDlg.GetType().CallMethod("OnBeforeVistaDialog", setupDlg, realDlg);

                    uint options = (uint)PubDialogType.CallMethod("GetOptions", setupDlg);
                    HiddenDialogType.CallMethod("SetOptions", realDlg, options | FOS_PICKFOLDERS);

                    object dlgEvents = Activator.CreateInstance(DialogEventsType, setupDlg);

                    uint cookie = 0;
                    object[] parameters = new object[] { dlgEvents, cookie };
                    HiddenDialogType.CallMethod("Advise", realDlg, parameters);

                    cookie = (uint)parameters[1];
                    try
                    {
                        int result = (int)HiddenDialogType.CallMethod("Show", realDlg, hwndOwner);
                        if (result == 0)
                        {
                            SelectedPath = setupDlg.FileName;
                            //SelectedPaths = setupDlg.FileNames;
                            return true;
                        }
                    }
                    finally
                    {
                        HiddenDialogType.CallMethod("Unadvise", realDlg, cookie);
                        //GC.KeepAlive(dlgEvents);
                    }
                }
                catch
                {
                    __errored = false;
                    throw;
                }
            }
            return false;
        }
    }

}