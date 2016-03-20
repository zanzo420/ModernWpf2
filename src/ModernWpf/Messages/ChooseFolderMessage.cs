using System;
using System.Windows;
using System.Windows.Threading;

namespace ModernWpf.Messages
{
    /// <summary>
    /// Message for choosing a folder.
    /// </summary>
    public class ChooseFolderMessage : MessageBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChooseFolderMessage"/> class.
        /// </summary>
        /// <param name="callback">The callback when a folder is chosen.</param>
        public ChooseFolderMessage(Action<string> callback) : this(null, null, callback) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChooseFolderMessage"/> class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="callback">The callback when a folder is chosen.</param>
        public ChooseFolderMessage(object sender, Action<string> callback)
            : this(sender, null, callback)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ChooseFolderMessage"/> class.
        /// </summary>
        /// <param name="sender">The message's original sender.</param>
        /// <param name="target">The message's intended target.</param>
        /// <param name="callback">The callback when a folder is chosen.</param>
        public ChooseFolderMessage(object sender, object target, Action<string> callback)
            : base(sender, target)
        {
            _callback = callback;
        }


        Action<string> _callback;

        /// <summary>
        /// Gets or sets the UI caption.
        /// </summary>
        /// <value>
        /// The caption.
        /// </value>
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the initial folder.
        /// </summary>
        /// <value>
        /// The initial folder.
        /// </value>
        public string InitialFolder { get; set; }

        /// <summary>
        /// Does the callback to notify sender of selected folder.
        /// </summary>
        /// <param name="folder">The folder.</param>
        public void DoCallback(string folder)
        {
            if (_callback != null)
            {
                _callback(folder);
            }
        }


        /// <summary>
        /// Handles the <see cref="ChooseFolderMessage" /> on a window by showing a 
        /// win32 folder dialog based on the message options.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public virtual void HandleWithPlatform(Window owner)
        {
            try
            {
                // could've used windows api codepack but didn't feel like taking depenency for only 1 thing
                if (HackyFolderBrowserDialog.IsSupported)
                {
                    var diag = new HackyFolderBrowserDialog
                    {
                        InitialDirectory = InitialFolder,
                        Title = Caption,
                    };
                    if (diag.ShowDialog(owner).GetValueOrDefault())
                    {
                        Dispatcher d = owner.FindDispatcher();

                        if (d == null || d.CheckAccess())
                        {
                            DoCallback(diag.SelectedPath);
                        }
                        else
                        {
                            d.BeginInvoke(new Action(() =>
                            {
                                DoCallback(diag.SelectedPath);
                            }));
                        }
                    }
                    return;
                }
            }
            catch { }

            // fallback to lame old dialog
            using (var diag = new System.Windows.Forms.FolderBrowserDialog())
            {
                diag.ShowNewFolderButton = true;
                diag.SelectedPath = InitialFolder;

                var winformOwner = owner == null ? null : new Wpf32Window(owner);
                if (diag.ShowDialog(winformOwner) == System.Windows.Forms.DialogResult.OK)
                {
                    Dispatcher d = owner.FindDispatcher();

                    if (d == null || d.CheckAccess())
                    {
                        DoCallback(diag.SelectedPath);
                    }
                    else
                    {
                        d.BeginInvoke(new Action(() =>
                        {
                            DoCallback(diag.SelectedPath);
                        }));
                    }
                }
            }
        }

    }
}
