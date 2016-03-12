using ModernWpf.Internal;
using ModernWpf.Native.Api;
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
    [TemplatePart(Name = PartCloseButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartMinButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartMaxButton, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartRestoreButton, Type = typeof(TitleBar))]
    public class TitleBar : Control
    {
        /// <summary>
        /// Name of the close button in template.
        /// </summary>
        protected const string PartCloseButton = "PART_CloseButton";
        /// <summary>
        /// Name of the minimize button in template.
        /// </summary>
        protected const string PartMinButton = "PART_MinButton";
        /// <summary>
        /// Name of the maximize button in template.
        /// </summary>
        protected const string PartMaxButton = "PART_MaxButton";
        /// <summary>
        /// Name of the restore button in template.
        /// </summary>
        protected const string PartRestoreButton = "PART_RestoreButton";


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static TitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
            IsTabStopProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(false));

        }

        #region properties



        public object BeforeTitleContent
        {
            get { return (object)GetValue(BeforeTitleContentProperty); }
            set { SetValue(BeforeTitleContentProperty, value); }
        }
        public static readonly DependencyProperty BeforeTitleContentProperty =
            DependencyProperty.Register("BeforeTitleContent", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null));




        public object AfterTitleContent
        {
            get { return (object)GetValue(AfterTitleContentProperty); }
            set { SetValue(AfterTitleContentProperty, value); }
        }
        public static readonly DependencyProperty AfterTitleContentProperty =
            DependencyProperty.Register("AfterTitleContent", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null));



        /// <summary>
        /// Gets or sets the control button style.
        /// </summary>
        /// <value>
        /// The control button style.
        /// </value>
        public Style ControlButtonStyle
        {
            get { return (Style)GetValue(ControlButtonStyleProperty); }
            set { SetValue(ControlButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty ControlButtonStyleProperty =
            DependencyProperty.Register("ControlButtonStyle", typeof(Style), typeof(TitleBar), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// Gets or sets the close button style.
        /// </summary>
        /// <value>
        /// The control button style.
        /// </value>
        public Style CloseButtonStyle
        {
            get { return (Style)GetValue(CloseButtonStyleProperty); }
            set { SetValue(CloseButtonStyleProperty, value); }
        }
        public static readonly DependencyProperty CloseButtonStyleProperty =
            DependencyProperty.Register("CloseButtonStyle", typeof(Style), typeof(TitleBar), new FrameworkPropertyMetadata(null));


        /// <summary>
        /// Gets the root window.
        /// </summary>
        /// <value>
        /// The root window.
        /// </value>
        public Window RootWindow
        {
            get { return (Window)GetValue(RootWindowProperty); }
            private set { SetValue(RootWindowProperty, value); }
        }
        static readonly DependencyProperty RootWindowProperty =
            DependencyProperty.Register("RootWindow", typeof(Window), typeof(TitleBar), new FrameworkPropertyMetadata(null, WindowChanged));


        static void WindowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var old = e.OldValue as Window;
            if (old != null)
            {
                old.StateChanged -= WindowStateChanged;
                BindingOperations.ClearAllBindings(d);
            }

            var newWin = e.NewValue as Window;
            if (newWin != null)
            {
                ((TitleBar)d).CreateBinding(Window.IsActiveProperty.Name, newWin, IsActiveProperty);
                newWin.StateChanged += WindowStateChanged;
            }
        }


        private Binding CreateBinding(string sourcePath, object source, DependencyProperty bindToProperty, IValueConverter converter = null, object converterParameter = null)
        {
            var bind = new Binding(sourcePath);
            bind.Source = source;
            bind.NotifyOnSourceUpdated = true;
            bind.Mode = BindingMode.OneWay;
            bind.Converter = converter;
            bind.ConverterParameter = converterParameter;
            BindingOperations.SetBinding(this, bindToProperty, bind);
            return bind;
        }



        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
        }

        static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(false));





        public Brush InactiveBackground
        {
            get { return (Brush)GetValue(InactiveBackgroundProperty); }
            set { SetValue(InactiveBackgroundProperty, value); }
        }
        public static readonly DependencyProperty InactiveBackgroundProperty =
            DependencyProperty.Register("InactiveBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(SystemColors.InactiveCaptionBrush));


        public Brush InactiveForeground
        {
            get { return (Brush)GetValue(InactiveForegroundProperty); }
            set { SetValue(InactiveForegroundProperty, value); }
        }
        public static readonly DependencyProperty InactiveForegroundProperty =
            DependencyProperty.Register("InactiveForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(SystemColors.InactiveCaptionTextBrush));



        public Brush ActiveBackground
        {
            get { return (Brush)GetValue(ActiveBackgroundProperty); }
            set { SetValue(ActiveBackgroundProperty, value); }
        }
        public static readonly DependencyProperty ActiveBackgroundProperty =
            DependencyProperty.Register("ActiveBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(new SolidColorBrush(Dwmapi.GetWindowColor())));


        public Brush ActiveForeground
        {
            get { return (Brush)GetValue(ActiveForegroundProperty); }
            set { SetValue(ActiveForegroundProperty, value); }
        }
        public static readonly DependencyProperty ActiveForegroundProperty =
            DependencyProperty.Register("ActiveForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(SystemColors.ActiveCaptionTextBrush));


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

            AttachCommand(PartCloseButton, CloseCommand);
            AttachCommand(PartMinButton, MinimizeCommand);
            AttachCommand(PartRestoreButton, RestoreCommand);
            AttachCommand(PartMaxButton, MaximizeCommand);
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
