using System.Windows.Controls;
using System.Windows.Input;

namespace ModernWpf
{
    /// <summary>
    /// Contains commands for common editor controls.
    /// All commands require the command parameter being the editor control itself.
    /// </summary>
    public static class EditorCommands
    {
        private static ICommand _clearTextBoxCommand;
        /// <summary>
        /// Gets the command that clears a <see cref="TextBox"/>.
        /// </summary>
        /// <value>
        /// The clear text command.
        /// </value>
        public static ICommand ClearTextBoxCommand
        {
            get
            {
                if (_clearTextBoxCommand == null)
                {
                    _clearTextBoxCommand = new RelayCommand<TextBox>(box =>
                    {
                        if (box != null)
                        {
                            box.Clear();
                            box.Focus();
                        }
                    },
                    box => box != null && !box.IsReadOnly && !string.IsNullOrEmpty(box.Text));
                }
                return _clearTextBoxCommand;
            }
        }

        private static ICommand _clearPasswordBoxCommand;
        /// <summary>
        /// Gets the command that clears a <see cref="PasswordBox"/>.
        /// </summary>
        /// <value>
        /// The clear text command.
        /// </value>
        public static ICommand ClearPasswordBoxCommand
        {
            get
            {
                if (_clearPasswordBoxCommand == null)
                {
                    _clearPasswordBoxCommand = new RelayCommand<PasswordBox>(box =>
                    {
                        if (box != null)
                        {
                            box.Clear();
                            box.Focus();
                        }
                    },
                    box => box != null && box.SecurePassword.Length > 0);
                }
                return _clearPasswordBoxCommand;
            }
        }
    }
}
