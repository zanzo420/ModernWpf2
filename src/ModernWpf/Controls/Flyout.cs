using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ModernWpf.Controls
{
    /// <summary>
    /// A user control that can be hosted in a <see cref="FlyoutContainer"/>.
    /// </summary>
    public class Flyout : UserControl
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static Flyout()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Flyout), new FrameworkPropertyMetadata(typeof(Flyout)));
        }

        #region properties

        /// <summary>
        /// Gets or sets the current container reference. This should only be set by <see cref="FlyoutContainer"/>.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        internal FlyoutContainer Container { get; set; }

        private bool? _diaglogResult;

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        /// <value>
        /// The dialog result.
        /// </value>
        [TypeConverter(typeof(DialogResultConverter))]
        public bool? DialogResult
        {
            get { return _diaglogResult; }
            set
            {
                _diaglogResult = value;
                if (Container != null)
                {
                    Container.Close(this);
                    _isOpen = false;
                    OnClosed();
                }
            }
        }


        /// <summary>
        /// Gets or sets a value indicating whether the dialog closes on escape key.
        /// </summary>
        /// <value>
        /// <c>true</c> to close on escape key; otherwise, <c>false</c>.
        /// </value>
        public bool DismissOnEscapeKey
        {
            get { return (bool)GetValue(DismissOnEscapeKeyProperty); }
            set { SetValue(DismissOnEscapeKeyProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="DismissOnEscapeKey"/>.
        /// </summary>
        public static readonly DependencyProperty DismissOnEscapeKeyProperty =
            DependencyProperty.Register("DismissOnEscapeKey", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(true));




        /// <summary>
        /// Gets or sets a value indicating the action to take when mouse clicks on the container's overlay area.
        /// </summary>
        /// <value>
        /// <c>true</c> to close when mouse clicks on container overlay; otherwise, <c>false</c>.
        /// </value>
        public OverlayClickBehavior OverlayClickBehavior
        {
            get { return (OverlayClickBehavior)GetValue(OverlayClickBehaviorProperty); }
            set { SetValue(OverlayClickBehaviorProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="OverlayClickBehavior"/>.
        /// </summary>
        public static readonly DependencyProperty OverlayClickBehaviorProperty =
            DependencyProperty.Register("OverlayClickBehavior", typeof(OverlayClickBehavior), typeof(Flyout), new FrameworkPropertyMetadata(OverlayClickBehavior.None));




        /// <summary>
        /// Gets or sets a value indicating whether the disable target specified in the <see cref="FlyoutContainer"/> is disabled when this is shown.
        /// </summary>
        /// <value>
        ///   <c>true</c> to disable target; otherwise, <c>false</c>.
        /// </value>
        public bool DisableTarget
        {
            get { return (bool)GetValue(DisableTargetProperty); }
            set { SetValue(DisableTargetProperty, value); }
        }

        /// <summary>
        /// The dependency property for <see cref="DismissOnEscapeKey"/>.
        /// </summary>
        public static readonly DependencyProperty DisableTargetProperty =
            DependencyProperty.Register("DisableTarget", typeof(bool), typeof(Flyout), new FrameworkPropertyMetadata(true));




        #endregion

        private bool _isOpen;

        #region methods

        /// <summary>
        /// Invoked when an unhandled <see cref="E:System.Windows.Input.Keyboard.KeyDown" /> attached event reaches an element in its route that is derived from this class. Implement this method to add class handling for this event.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Input.KeyEventArgs" /> that contains the event data.</param>
        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e != null && DismissOnEscapeKey && e.Key == System.Windows.Input.Key.Escape)
            {
                e.Handled = true;
                DialogResult = false;
            }
            
            base.OnKeyDown(e);
        }

        internal void TryFocus()
        {
            // if subclass doesn't override OnFocus then try to find a focusable control ourselves.
            var focusMethod = this.GetType().GetMethod("OnFocus", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (focusMethod != null && focusMethod.DeclaringType != typeof(Flyout))
            {
                OnFocus();
            }
            else
            {
                this.ProcessInVisualTree<FrameworkElement>(fe =>
                {
                    var matches = fe.Visibility == System.Windows.Visibility.Visible && fe.IsEnabled && fe.Focusable;
                    if (matches)
                    {
                        fe.Focus();
                    }
                    return matches;
                });
            }
        }

        /// <summary>
        /// Called when dialog has been shown and focus needs to happen.
        /// </summary>
        protected virtual void OnFocus() { }

        /// <summary>
        /// Called when dialog has been closed.
        /// </summary>
        protected virtual void OnClosed() { }

        /// <summary>
        /// Shows the dialog on a window. The window must have a <see cref="FlyoutContainer"/>
        /// in its visual tree.
        /// </summary>
        /// <param name="window">The window.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public virtual void ShowDialog(Window window)
        {
            ShowDialog(window.FindChildInVisualTree<FlyoutContainer>(true));
        }

        /// <summary>
        /// Shows the dialog on a <see cref="FlyoutContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public virtual void ShowDialog(FlyoutContainer container)
        {
            if (container == null) { throw new ArgumentNullException("container"); }

            container.Show(this);
            _isOpen = true;
            _diaglogResult = null;
        }


        /// <summary>
        /// Shows the dialog on a window. The window must have a <see cref="FlyoutContainer"/>
        /// in its visual tree.
        /// </summary>
        /// <param name="window">The window.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public virtual bool? ShowDialogModal(Window window)
        {
            return ShowDialogModal(window.FindChildInVisualTree<FlyoutContainer>(true));
        }


        /// <summary>
        /// Shows the dialog on a <see cref="FlyoutContainer"/>.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <exception cref="System.ArgumentNullException">container</exception>
        public virtual bool? ShowDialogModal(FlyoutContainer container)
        {
            ShowDialog(container);

            while (_isOpen)
            {
                // from http://www.codeproject.com/Articles/36516/WPF-Modal-Dialog
                // HACK: Stop the thread if the application is about to close
                if (this.Dispatcher.HasShutdownStarted ||
                    this.Dispatcher.HasShutdownFinished)
                {
                    break;
                }

                // HACK: Simulate "DoEvents"
                //this.Dispatcher.Invoke(DispatcherPriority.Background, new ThreadStart(delegate { }));
                this.Dispatcher.DoEvents();
                Thread.Sleep(20);
            }

            return DialogResult;
        }

        #endregion
    }

    /// <summary>
    /// Indicates what happens when <see cref="FlyoutContainer"/>'s overlay is clicked.
    /// </summary>
    public enum OverlayClickBehavior
    {
        /// <summary>
        /// No action taken.
        /// </summary>
        None,
        /// <summary>
        /// Dismisses the current flyout.
        /// </summary>
        Dismiss,
        /// <summary>
        /// Enter the move window loop.
        /// </summary>
        DragMove
    }
}
