using System;
using System.Reflection;
using System.Windows;
using System.Windows.Interop;

namespace ModernWpf
{
    /// <summary>
    /// Hacks winform's dialog to become folder selector.
    /// </summary>
    /// <seealso cref="Microsoft.Win32.CommonDialog" />
    class HackyFolderBrowserDialog : Microsoft.Win32.CommonDialog
    {
        public string FileName { get; set; }
        public string[] FileNames { get; private set; }
        public string InitialDirectory { get; set; }
        public string Title { get; set; }
        public bool MultiSelect { get; set; }

        public bool ShowDialog()
        {
            return RunDialog(GetForegroundWindow());
        }

        public bool ShowDialog(Window owner)
        {
            IntPtr handle = owner == null ? GetForegroundWindow() : new WindowInteropHelper(owner).Handle;
            return RunDialog(handle);
        }


        private IntPtr GetForegroundWindow()
        {
            // TODO: get foreground window.
            return IntPtr.Zero;
        }

        public override void Reset()
        {
        }



        public static bool IsSupported
        {
            get
            {
                return PlatformInfo.IsWinVistaUp &&
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
                setupDlg.FileName = FileName;
                setupDlg.InitialDirectory = InitialDirectory;
                setupDlg.Title = Title;
                setupDlg.AddExtension = false;
                setupDlg.CheckFileExists = false;
                setupDlg.DereferenceLinks = true;
                setupDlg.Multiselect = MultiSelect;


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
                        FileName = setupDlg.FileName;
                        FileNames = setupDlg.FileNames;
                        return true;
                    }
                }
                finally
                {
                    HiddenDialogType.CallMethod("Unadvise", realDlg, cookie);
                    GC.KeepAlive(dlgEvents);
                }
            }
            return false;
        }
    }
    static class TypeExtensions
    {
        public static object CallMethod(this Type type, string methodName, object instance, params object[] args)
        {
            var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (method != null)
            {
                return method.Invoke(instance, args);
            }
            throw new NotSupportedException($"Method {methodName} is not supported for {type.Name}.");
        }
    }

}