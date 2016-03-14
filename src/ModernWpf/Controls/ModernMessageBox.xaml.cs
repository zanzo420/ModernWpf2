using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModernWpf.Controls
{
    /// <summary>
    /// A drop-in <see cref="MessageBox"/> replacment that displays in a <see cref="FlyoutContainer"/>.
    /// </summary>
    public sealed partial class ModernMessageBox : MessageBoxFlyout
    {
        #region static stuff
        
        /// <summary>
        /// Displays a message box in front of the specified container (either <see cref="FlyoutContainer" /> or <see cref="Window"/> containing a <see cref="FlyoutContainer"/>).
        /// </summary>
        /// <param name="owner">A <see cref="FlyoutContainer" /> to host this message box.</param>
        /// <param name="messageBoxText">The message box text.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static MessageBoxResult Show(ContentControl owner, string messageBoxText)
        {
            return Show(owner, messageBoxText, null, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
        }
        /// <summary>
        /// Displays a message box in front of the specified container (either <see cref="FlyoutContainer" /> or <see cref="Window"/> containing a <see cref="FlyoutContainer"/>).
        /// </summary>
        /// <param name="owner">A <see cref="FlyoutContainer" /> to host this message box.</param>
        /// <param name="messageBoxText">The message box text.</param>
        /// <param name="caption">The caption.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static MessageBoxResult Show(ContentControl owner, string messageBoxText, string caption)
        {
            return Show(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None);
        }
        /// <summary>
        /// Displays a message box in front of the specified container (either <see cref="FlyoutContainer" /> or <see cref="Window"/> containing a <see cref="FlyoutContainer"/>).
        /// </summary>
        /// <param name="owner">A <see cref="FlyoutContainer" /> to host this message box.</param>
        /// <param name="messageBoxText">The message box text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button to display.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static MessageBoxResult Show(ContentControl owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(owner, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None);
        }
        /// <summary>
        /// Displays a message box in front of the specified container (either <see cref="FlyoutContainer" /> or <see cref="Window"/> containing a <see cref="FlyoutContainer"/>).
        /// </summary>
        /// <param name="owner">A <see cref="FlyoutContainer" /> to host this message box.</param>
        /// <param name="messageBoxText">The message box text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button to display.</param>
        /// <param name="icon">The icon to display.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static MessageBoxResult Show(ContentControl owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);
        }

        /// <summary>
        /// Displays a message box in front of the specified container (either <see cref="FlyoutContainer" /> or <see cref="Window"/> containing a <see cref="FlyoutContainer"/>).
        /// </summary>
        /// <param name="owner">A <see cref="FlyoutContainer" /> to host this message box.</param>
        /// <param name="messageBoxText">The message box text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="button">The button to display.</param>
        /// <param name="icon">The icon to display.</param>
        /// <param name="defaultResult">The default result.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static MessageBoxResult Show(ContentControl owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            if (owner == null) { throw new ArgumentNullException("owner"); }

            var diag = new ModernMessageBox();
            diag.txtMsg.Text = messageBoxText;
            return diag.ShowDialogModal(owner, caption, button, icon, defaultResult);
        }

        #endregion

        #region instance stuff

        private ModernMessageBox()
        {
            InitializeComponent();
        }

        #endregion

    }
}
