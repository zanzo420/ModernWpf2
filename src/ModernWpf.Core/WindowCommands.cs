using System.Windows;
using System.Windows.Input;

namespace ModernWpf
{
    /// <summary>
    /// Contains commands for window state control.
    /// All commands require the command parameter being the window itself.
    /// </summary>
    public static class WindowCommands
    {
        private static ICommand _closeCommand;
        /// <summary>
        /// Gets the command that closes the window.
        /// </summary>
        /// <value>
        /// The close command.
        /// </value>
        public static ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (
                    _closeCommand = new RelayCommand<Window>(window =>
                    {
                        if (window != null) { window.Close(); }
                    },
                    window => window != null)
                );
            }
        }

        private static ICommand _maximizeCommand;
        /// <summary>
        /// Gets the command that maximizes the window.
        /// </summary>
        /// <value>
        /// The maximize command.
        /// </value>
        public static ICommand MaximizeCommand
        {
            get
            {
                return _maximizeCommand ?? (
                    _maximizeCommand = new RelayCommand<Window>(window =>
                    {
                        if (window != null) { window.WindowState = WindowState.Maximized; }
                    },
                    window => window != null &&
                            window.ResizeMode != ResizeMode.NoResize &&
                            window.ResizeMode != ResizeMode.CanMinimize &&
                            window.WindowState != WindowState.Maximized)
                );
            }
        }

        private static ICommand _restoreCommand;
        /// <summary>
        /// Gets the command that restores the window.
        /// </summary>
        /// <value>
        /// The restore command.
        /// </value>
        public static ICommand RestoreCommand
        {
            get
            {
                return _restoreCommand ?? (
                    _restoreCommand = new RelayCommand<Window>(window =>
                    {
                        if (window != null) { window.WindowState = WindowState.Normal; }
                    }, 
                    window => window != null &&
                            window.ResizeMode != ResizeMode.NoResize &&
                            window.ResizeMode != ResizeMode.CanMinimize &&
                            window.WindowState == WindowState.Maximized)
                );
            }
        }

        private static ICommand _minimizeCommand;
        /// <summary>
        /// Gets the command that minimizes the window.
        /// </summary>
        /// <value>
        /// The minimize command.
        /// </value>
        public static ICommand MinimizeCommand
        {
            get
            {
                return _minimizeCommand ?? (
                    _minimizeCommand = new RelayCommand<Window>(window =>
                    {
                        if (window != null) { window.WindowState = WindowState.Minimized; }
                    }, 
                    window => window != null &&
                            window.ResizeMode != ResizeMode.NoResize)
                );
            }
        }
    }

}
