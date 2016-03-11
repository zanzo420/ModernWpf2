using ModernWpf.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
    /// A UI piece for window title bar (icon, title, min/max/restore buttons).
    /// </summary>
    [TemplatePart(Name = PART_CloseButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PART_MinButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PART_MaxButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PART_RestoreButton, Type = typeof(TitleBar))]
    public class TitleBar : ContentControl
    {
        /// <summary>
        /// Name of the close button in template.
        /// </summary>
        protected const string PART_CloseButton = "PART_CloseButton";
        /// <summary>
        /// Name of the minimize button in template.
        /// </summary>
        protected const string PART_MinButton = "PART_MinButton";
        /// <summary>
        /// Name of the maximize button in template.
        /// </summary>
        protected const string PART_MaxButton = "PART_MaxButton";
        /// <summary>
        /// Name of the restore button in template.
        /// </summary>
        protected const string PART_RestoreButton = "PART_RestoreButton";


        static TitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
            IsTabStopProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(false));

        }

        #region properties

        ///// <summary>
        ///// Gets or sets the control button style.
        ///// </summary>
        ///// <value>
        ///// The control button style.
        ///// </value>
        //public Style ControlButtonStyle
        //{
        //    get { return (Style)GetValue(ControlButtonStyleProperty); }
        //    set { SetValue(ControlButtonStyleProperty, value); }
        //}
        //public static readonly DependencyProperty ControlButtonStyleProperty =
        //    DependencyProperty.Register("ControlButtonStyle", typeof(Style), typeof(TitleBar), new FrameworkPropertyMetadata(null));


        /// <summary>
        /// Gets the root window.
        /// </summary>
        /// <value>
        /// The root window.
        /// </value>
        public Window RootWindow
        {
            get { return (Window)GetValue(RootWindowProperty); }
            set { SetValue(RootWindowProperty, value); }
        }
        public static readonly DependencyProperty RootWindowProperty =
            DependencyProperty.Register("RootWindow", typeof(Window), typeof(TitleBar), new FrameworkPropertyMetadata(null, WindowChanged));


        static void WindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var old = e.OldValue as Window;
            if (old != null)
            {
                old.StateChanged -= WindowStateChanged;
            }

            var newWin = e.NewValue as Window;
            if (newWin != null)
            {
                newWin.StateChanged += WindowStateChanged;
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether app icon is shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> to show app icon; otherwise, <c>false</c>.
        /// </value>
        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register("ShowIcon", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));
        

        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        public static readonly DependencyProperty ShowTitleProperty =
            DependencyProperty.Register("ShowTitle", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));


        public bool ShowControlBox
        {
            get { return (bool)GetValue(ShowControlBoxProperty); }
            set { SetValue(ShowControlBoxProperty, value); }
        }
        public static readonly DependencyProperty ShowControlBoxProperty =
            DependencyProperty.Register("ShowControlBox", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));


        public bool LargeIcon
        {
            get { return (bool)GetValue(LargeIconProperty); }
            set { SetValue(LargeIconProperty, value); }
        }
        public static readonly DependencyProperty LargeIconProperty =
            DependencyProperty.Register("LargeIcon", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(false));


        #endregion

        private static void WindowStateChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }

            RootWindow = this.FindParentInVisualTree<Window>();

            AttachCommand(PART_CloseButton, CloseCommand);
            AttachCommand(PART_MinButton, MinimizeCommand);
            AttachCommand(PART_RestoreButton, RestoreCommand);
            AttachCommand(PART_MaxButton, MaximizeCommand);
        }

        private void AttachCommand(string partName, ICommand command)
        {
            var btn = GetTemplateChild(partName) as Button;
            if (btn != null)
            {
                btn.Command = command;
            }
        }

        private RelayCommand _closeCommand;
        /// <summary>
        /// Gets the command that closes the window.
        /// </summary>
        /// <value>
        /// The close command.
        /// </value>
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (
                    _closeCommand = new RelayCommand(() =>
                    {
                        if (RootWindow != null) { RootWindow.Close(); }
                    }, () => RootWindow != null)
                );
            }
        }

        private RelayCommand _maximizeCommand;
        /// <summary>
        /// Gets the command that maximizes the window.
        /// </summary>
        /// <value>
        /// The maximize command.
        /// </value>
        public ICommand MaximizeCommand
        {
            get
            {
                return _maximizeCommand ?? (
                    _maximizeCommand = new RelayCommand(() =>
                    {
                        if (RootWindow != null) { RootWindow.WindowState = WindowState.Maximized; }
                    }, () =>
                    {
                        return RootWindow != null &&
                            RootWindow.ResizeMode != ResizeMode.NoResize &&
                            RootWindow.ResizeMode != ResizeMode.CanMinimize &&
                            RootWindow.WindowState != WindowState.Maximized;
                    })
                );
            }
        }

        private RelayCommand _restoreCommand;
        /// <summary>
        /// Gets the command that restores the window.
        /// </summary>
        /// <value>
        /// The restore command.
        /// </value>
        public ICommand RestoreCommand
        {
            get
            {
                return _restoreCommand ?? (
                    _restoreCommand = new RelayCommand(() =>
                    {
                        if (RootWindow != null) { RootWindow.WindowState = WindowState.Normal; }
                    }, () =>
                    {
                        return RootWindow != null &&
                            RootWindow.ResizeMode != ResizeMode.NoResize &&
                            RootWindow.ResizeMode != ResizeMode.CanMinimize &&
                            RootWindow.WindowState == WindowState.Maximized;
                    })
                );
            }
        }

        private RelayCommand _minimizeCommand;
        /// <summary>
        /// Gets the command that minimizes the window.
        /// </summary>
        /// <value>
        /// The minimize command.
        /// </value>
        public ICommand MinimizeCommand
        {
            get
            {
                return _minimizeCommand ?? (
                    _minimizeCommand = new RelayCommand(() =>
                    {
                        if (RootWindow != null) { RootWindow.WindowState = WindowState.Minimized; }
                    }, () =>
                    {
                        return RootWindow != null &&
                            RootWindow.ResizeMode != ResizeMode.NoResize;
                    })
                );
            }
        }
    }
}
