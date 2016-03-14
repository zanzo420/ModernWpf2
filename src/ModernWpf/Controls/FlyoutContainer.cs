using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace ModernWpf.Controls
{
    /// <summary>
    /// A container element for hosting <see cref="Flyout"/>s.
    /// </summary>
    [TemplatePart(Name = PARTContent, Type = typeof(ContentPresenter))]
    [TemplatePart(Name = PARTOverlay, Type = typeof(Border))]
    public class FlyoutContainer : ContentControl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static FlyoutContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlyoutContainer), new FrameworkPropertyMetadata(typeof(FlyoutContainer)));

        }
        const string PARTContent = "PART_Content";
        const string PARTOverlay = "PART_Overlay";

        #region properties



        /// <summary>
        /// Gets or sets the z-index when a flyout is displayed.
        /// </summary>
        /// <value>
        /// The z-index when displaying.
        /// </value>
        public int OpenZIndex
        {
            get { return (int)GetValue(OpenZIndexProperty); }
            set { SetValue(OpenZIndexProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="OpenZIndex"/>.
        /// </summary>
        public static readonly DependencyProperty OpenZIndexProperty =
            DependencyProperty.Register("OpenZIndex", typeof(int), typeof(FlyoutContainer), new FrameworkPropertyMetadata(1));




        /// <summary>
        /// Gets a value indicating whether this container has any flyout open.
        /// </summary>
        /// <value>
        ///   <c>true</c> if it has flyout open; otherwise, <c>false</c>.
        /// </value>
        public bool HasFlyoutOpen
        {
            get { return (bool)GetValue(HasFlyoutOpenProperty); }
            private set
            {
                var changed = value != HasFlyoutOpen;

                SetValue(HasFlyoutOpenPropertyKey, value);
                if (changed)
                {
                    if (value)
                    {
                        VisualStateManager.GoToState(this, "IsOpen", Animations.ShouldAnimate);
                    }
                    else
                    {
                        VisualStateManager.GoToState(this, "IsClosed", Animations.ShouldAnimate);
                    }
                }
            }
        }

        static readonly DependencyPropertyKey HasFlyoutOpenPropertyKey =
            DependencyProperty.RegisterReadOnly("HasFlyoutOpen", typeof(bool), typeof(FlyoutContainer), new FrameworkPropertyMetadata(false));
        /// <summary>
        /// The dependency property for <see cref="HasFlyoutOpen"/>.
        /// </summary>
        public static readonly DependencyProperty HasFlyoutOpenProperty = HasFlyoutOpenPropertyKey.DependencyProperty;


        /// <summary>
        /// Gets or sets the target to disable when flyouts are visible.
        /// </summary>
        /// <value>
        /// The disable target.
        /// </value>
        public FrameworkElement DisableTarget
        {
            get { return (FrameworkElement)GetValue(DisableTargetProperty); }
            set { SetValue(DisableTargetProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="DisableTarget"/>.
        /// </summary>
        public static readonly DependencyProperty DisableTargetProperty =
            DependencyProperty.Register("DisableTarget", typeof(FrameworkElement), typeof(FlyoutContainer), new FrameworkPropertyMetadata(null));
        

        #endregion

        ContentPresenter _presenter;
        Border _overlay;

        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code or internal processes call <see cref="M:System.Windows.FrameworkElement.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _presenter = GetTemplateChild(PARTContent) as ContentPresenter;
            _overlay = GetTemplateChild(PARTOverlay) as Border;
            if (HasFlyoutOpen)
            {
                var test = VisualStateManager.GoToState(this, "IsOpen", Animations.ShouldAnimate);
            }
            else
            {
                var test = VisualStateManager.GoToState(this, "IsClosed", Animations.ShouldAnimate);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            var diag = this.Content as Flyout;
            if (diag != null)
            {
                var hitRes = VisualTreeHelper.HitTest(this, e.GetPosition(this));
                if (hitRes.VisualHit == _overlay)
                {
                    if (diag.OverlayClickBehavior == OverlayClickBehavior.Dismiss)
                    {
                        diag.DialogResult = false;
                    }
                    else if (diag.OverlayClickBehavior == OverlayClickBehavior.DragMove)
                    {
                        Window.GetWindow(this).DragMove();
                    }
                }
            }
            base.OnMouseLeftButtonDown(e);
        }

        object _openLock = new object();
        List<Flyout> _openDialogs = new List<Flyout>();

        internal void Close(Flyout dialog)
        {
            lock (_openLock)
            {
                dialog.Container = null;
                _openDialogs.Remove(dialog);
                ShowMostRecentDialogIfNecessary();
            }
        }

        internal void Show(Flyout dialog)
        {
            if (dialog.Container != null && dialog.Container != this) { throw new ArgumentException("This dialog already has a container.", "dialog"); }

            if (Content == dialog) { return; }

            lock (_openLock)
            {
                if (dialog.Container != null)
                {
                    // already somewhere in this stack
                    _openDialogs.Remove(dialog);
                }
                _openDialogs.Add(dialog);
                ShowMostRecentDialogIfNecessary();
            }
        }

        private void ShowMostRecentDialogIfNecessary()
        {
            var next = _openDialogs.LastOrDefault();
            if (next == null)
            {
                HasFlyoutOpen = false;
                this.Content = null;
                if (_presenter != null) { BindingOperations.ClearAllBindings(_presenter); }
                if (DisableTarget != null) { DisableTarget.IsEnabled = true; }
            }
            else
            {
                next.Container = this;
                if (DisableTarget != null) { DisableTarget.IsEnabled = !next.DisableTarget; }
                if (_presenter != null)
                {
                    BindContentAlignment(next);
                }
                this.Content = next;
                if (Animations.ShouldAnimate)
                {
                    DoShowContentAnimation(next);
                }
                HasFlyoutOpen = true;

                var dt = new DispatcherTimer(DispatcherPriority.Send);
                dt.Tick += (s, e) =>
                {
                    dt.Stop();
                    next.TryFocus();
                };
                dt.Interval = TimeSpan.FromMilliseconds(300);
                dt.Start();

            }
        }

        private void BindContentAlignment(Flyout content)
        {
            var hbind = new Binding(HorizontalAlignmentProperty.Name);
            hbind.Source = content;
            hbind.NotifyOnSourceUpdated = true;
            BindingOperations.SetBinding(_presenter, HorizontalAlignmentProperty, hbind);


            var vbind = new Binding(VerticalAlignmentProperty.Name);
            vbind.Source = content;
            vbind.NotifyOnSourceUpdated = true;
            BindingOperations.SetBinding(_presenter, VerticalAlignmentProperty, vbind);
        }

        void DoShowContentAnimation(Flyout content)
        {
            var dir = DetermineAniDirection(content);
            Animations.SlideIn(_presenter, dir, Animations.TypicalDuration, 200, Animations.TypicalEasing);

        }

        static SlideFromDirection DetermineAniDirection(Flyout content)
        {
            if (content != null)
            {
                if (content.VerticalAlignment == System.Windows.VerticalAlignment.Stretch)
                {
                    switch (content.HorizontalAlignment)
                    {
                        case System.Windows.HorizontalAlignment.Left:
                            return SlideFromDirection.Left;
                        case System.Windows.HorizontalAlignment.Right:
                            return SlideFromDirection.Right;
                    }
                }
                else if (content.HorizontalAlignment == System.Windows.HorizontalAlignment.Stretch &&
                    content.VerticalAlignment == System.Windows.VerticalAlignment.Bottom)
                {
                    return SlideFromDirection.Bottom;
                }
            }
            return SlideFromDirection.Top;
        }

    }
}
