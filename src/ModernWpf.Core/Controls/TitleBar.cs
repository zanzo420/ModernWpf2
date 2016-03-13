using ModernWpf.Internal;
using ModernWpf.Native.Api;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ModernWpf.Controls
{
    /// <summary>
    /// A UI piece for window title bar (icon, title, min/max/restore buttons, and custom content).
    /// </summary>
    [TemplatePart(Name = PartIconName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartTitleName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartControlBoxName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartCloseButtonName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartMinButtonName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartMaxButtonName, Type = typeof(TitleBar))]
    [TemplatePart(Name = PartRestoreButtonName, Type = typeof(TitleBar))]
    public class TitleBar : Control
    {
        /// <summary>
        /// Name of the icon Image in template.
        /// </summary>
        protected const string PartIconName = "PART_Icon";
        /// <summary>
        /// Name of the title TextBlock in template.
        /// </summary>
        protected const string PartTitleName = "PART_Title";
        /// <summary>
        /// Name of the control button box panel in template.
        /// </summary>
        protected const string PartControlBoxName = "PART_ControlBox";
        /// <summary>
        /// Name of the close button in template.
        /// </summary>
        protected const string PartCloseButtonName = "PART_CloseButton";
        /// <summary>
        /// Name of the minimize button in template.
        /// </summary>
        protected const string PartMinButtonName = "PART_MinButton";
        /// <summary>
        /// Name of the maximize button in template.
        /// </summary>
        protected const string PartMaxButtonName = "PART_MaxButton";
        /// <summary>
        /// Name of the restore button in template.
        /// </summary>
        protected const string PartRestoreButtonName = "PART_RestoreButton";


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static TitleBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(typeof(TitleBar)));
            IsTabStopProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(false));
            MinHeightProperty.OverrideMetadata(typeof(TitleBar), new FrameworkPropertyMetadata(28d));
        }

        #region properties



        /// <summary>
        /// Gets or sets the custom content that appears before the title text.
        /// </summary>
        /// <value>
        /// The content before the title text.
        /// </value>
        public object BeforeTitleContent
        {
            get { return (object)GetValue(BeforeTitleContentProperty); }
            set { SetValue(BeforeTitleContentProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="BeforeTitleContent"/>.
        /// </summary>
        public static readonly DependencyProperty BeforeTitleContentProperty =
            DependencyProperty.Register("BeforeTitleContent", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null));


        /// <summary>
        /// Gets or sets the custom content that appears after the title text.
        /// </summary>
        /// <value>
        /// The content after the title text.
        /// </value>
        public object AfterTitleContent
        {
            get { return (object)GetValue(AfterTitleContentProperty); }
            set { SetValue(AfterTitleContentProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="AfterTitleContent"/>.
        /// </summary>
        public static readonly DependencyProperty AfterTitleContentProperty =
            DependencyProperty.Register("AfterTitleContent", typeof(object), typeof(TitleBar), new FrameworkPropertyMetadata(null));
        

        /// <summary>
        /// Gets or sets the button background.
        /// </summary>
        /// <value>
        /// The button background.
        /// </value>
        public Brush ButtonBackground
        {
            get { return (Brush)GetValue(ButtonBackgroundProperty); }
            set { SetValue(ButtonBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonBackground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonBackgroundProperty =
            DependencyProperty.Register("ButtonBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(Brushes.Transparent));



        /// <summary>
        /// Gets or sets the button foreground.
        /// </summary>
        /// <value>
        /// The button foreground.
        /// </value>
        public Brush ButtonForeground
        {
            get { return (Brush)GetValue(ButtonForegroundProperty); }
            set { SetValue(ButtonForegroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonForeground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonForegroundProperty =
            DependencyProperty.Register("ButtonForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleActiveForeground));




        /// <summary>
        /// Gets or sets the button hover background.
        /// </summary>
        /// <value>
        /// The button hover background.
        /// </value>
        public Brush ButtonHoverBackground
        {
            get { return (Brush)GetValue(ButtonHoverBackgroundProperty); }
            set { SetValue(ButtonHoverBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonHoverBackground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonHoverBackgroundProperty =
            DependencyProperty.Register("ButtonHoverBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.MakeBrush("#40dddddd")));



        /// <summary>
        /// Gets or sets the button hover foreground.
        /// </summary>
        /// <value>
        /// The button hover foreground.
        /// </value>
        public Brush ButtonHoverForeground
        {
            get { return (Brush)GetValue(ButtonHoverForegroundProperty); }
            set { SetValue(ButtonHoverForegroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonHoverForeground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonHoverForegroundProperty =
            DependencyProperty.Register("ButtonHoverForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleActiveForeground));



        /// <summary>
        /// Gets or sets the button pressed background.
        /// </summary>
        /// <value>
        /// The button pressed background.
        /// </value>
        public Brush ButtonPressedBackground
        {
            get { return (Brush)GetValue(ButtonPressedBackgroundProperty); }
            set { SetValue(ButtonPressedBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonPressedBackground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonPressedBackgroundProperty =
            DependencyProperty.Register("ButtonPressedBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.MakeBrush("#60dddddd")));


        /// <summary>
        /// Gets or sets the button pressed foreground.
        /// </summary>
        /// <value>
        /// The button pressed foreground.
        /// </value>
        public Brush ButtonPressedForeground
        {
            get { return (Brush)GetValue(ButtonPressedForegroundProperty); }
            set { SetValue(ButtonPressedForegroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ButtonPressedForeground"/>.
        /// </summary>
        public static readonly DependencyProperty ButtonPressedForegroundProperty =
            DependencyProperty.Register("ButtonPressedForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleActiveForeground));
        

        /// <summary>
        /// Gets the associated root window.
        /// </summary>
        /// <value>
        /// The root window.
        /// </value>
        public Window RootWindow
        {
            get { return (Window)GetValue(RootWindowProperty); }
            private set { SetValue(RootWindowPropertyKey, value); }
        }
        static readonly DependencyPropertyKey RootWindowPropertyKey =
            DependencyProperty.RegisterReadOnly("RootWindow", typeof(Window), typeof(TitleBar), new FrameworkPropertyMetadata(null, WindowChanged));
        static readonly DependencyProperty RootWindowProperty = RootWindowPropertyKey.DependencyProperty;

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


        /// <summary>
        /// Gets a value indicating whether the associated window is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the associated window is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
        }

        static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(false));


        /// <summary>
        /// Gets or sets the inactive background.
        /// </summary>
        /// <value>
        /// The inactive background.
        /// </value>
        public Brush InactiveBackground
        {
            get { return (Brush)GetValue(InactiveBackgroundProperty); }
            set { SetValue(InactiveBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="InactiveBackground"/>.
        /// </summary>
        public static readonly DependencyProperty InactiveBackgroundProperty =
            DependencyProperty.Register("InactiveBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleInactiveBackground));

        /// <summary>
        /// Gets or sets the inactive foreground.
        /// </summary>
        /// <value>
        /// The inactive foreground.
        /// </value>
        public Brush InactiveForeground
        {
            get { return (Brush)GetValue(InactiveForegroundProperty); }
            set { SetValue(InactiveForegroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="InactiveForeground"/>.
        /// </summary>
        public static readonly DependencyProperty InactiveForegroundProperty =
            DependencyProperty.Register("InactiveForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleInactiveForeground));


        /// <summary>
        /// Gets or sets the active background.
        /// </summary>
        /// <value>
        /// The active background.
        /// </value>
        public Brush ActiveBackground
        {
            get { return (Brush)GetValue(ActiveBackgroundProperty); }
            set { SetValue(ActiveBackgroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ActiveBackground"/>.
        /// </summary>
        public static readonly DependencyProperty ActiveBackgroundProperty =
            DependencyProperty.Register("ActiveBackground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleActiveBackground));

        /// <summary>
        /// Gets or sets the active foreground.
        /// </summary>
        /// <value>
        /// The active foreground.
        /// </value>
        public Brush ActiveForeground
        {
            get { return (Brush)GetValue(ActiveForegroundProperty); }
            set { SetValue(ActiveForegroundProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ActiveForeground"/>.
        /// </summary>
        public static readonly DependencyProperty ActiveForegroundProperty =
            DependencyProperty.Register("ActiveForeground", typeof(Brush), typeof(TitleBar), new FrameworkPropertyMetadata(DefaultColors.TitleActiveForeground));


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
        /// <summary>
        /// The dependency property for <see cref="ShowIcon"/>.
        /// </summary>
        public static readonly DependencyProperty ShowIconProperty =
            DependencyProperty.Register("ShowIcon", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));


        /// <summary>
        /// Gets or sets a value indicating whether to show large app icon.
        /// </summary>
        /// <value>
        /// <c>true</c> to show large app icon; otherwise, <c>false</c>.
        /// </value>
        public bool LargeIcon
        {
            get { return (bool)GetValue(LargeIconProperty); }
            set { SetValue(LargeIconProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="LargeIcon"/>.
        /// </summary>
        public static readonly DependencyProperty LargeIconProperty =
            DependencyProperty.Register("LargeIcon", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(false));


        /// <summary>
        /// Gets or sets a value indicating whether to show title text.
        /// </summary>
        /// <value>
        /// <c>true</c> to show title; otherwise, <c>false</c>.
        /// </value>
        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ShowTitle"/>.
        /// </summary>
        public static readonly DependencyProperty ShowTitleProperty =
            DependencyProperty.Register("ShowTitle", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));


        /// <summary>
        /// Gets or sets a value indicating whether to show window control buttons.
        /// </summary>
        /// <value>
        /// <c>true</c> to show window control buttons; otherwise, <c>false</c>.
        /// </value>
        public bool ShowControlBox
        {
            get { return (bool)GetValue(ShowControlBoxProperty); }
            set { SetValue(ShowControlBoxProperty, value); }
        }
        /// <summary>
        /// The dependency property for <see cref="ShowControlBox"/>.
        /// </summary>
        public static readonly DependencyProperty ShowControlBoxProperty =
            DependencyProperty.Register("ShowControlBox", typeof(bool), typeof(TitleBar), new FrameworkPropertyMetadata(true));


        #endregion

        private static void WindowStateChanged(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (DesignerProperties.GetIsInDesignMode(this)) { return; }

            RootWindow = this.FindParentInVisualTree<Window>();

            BindCommand(PartCloseButtonName, WindowCommands.CloseCommand);
            BindCommand(PartMinButtonName, WindowCommands.MinimizeCommand);
            BindCommand(PartRestoreButtonName, WindowCommands.RestoreCommand);
            BindCommand(PartMaxButtonName, WindowCommands.MaximizeCommand);
        }

        private void BindCommand(string partName, ICommand command)
        {
            var btn = GetTemplateChild(partName) as Button;
            if (btn != null)
            {
                btn.SetBinding(ButtonBase.CommandProperty, new Binding
                {
                    Source = command
                });
                btn.SetBinding(ButtonBase.CommandParameterProperty, new Binding(RootWindowProperty.Name)
                {
                    Source = this,
                });
            }
        }
    }
}
