using ModernWpf.Controls;
using ModernWpf.Messages;
using System;
using System.Windows;

namespace ModernWpf
{
    /// <summary>
    /// Extension methods for mvvm messages.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// Handles a basic <see cref="MessageBoxMessage" /> on a window by showing a <see cref="ModernMessageBox" />
        /// and invokes the callback.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static void HandleWithModern(this MessageBoxMessage message, Window owner)
        {
            if (owner == null) { throw new ArgumentNullException("owner"); }

            var d = owner.FindDispatcher();
            if (d != null && !d.CheckAccess())
            {
                d.BeginInvoke(new Action<Window>(win =>
                {
                    HandleWithModern(message, win);
                }), owner);
                return;
            }

            var res = ModernMessageBox.Show(owner, message.Content, message.Caption, message.Button, message.Icon, message.DefaultResult);
            message.DoCallback(res);
        }

        /// <summary>
        /// Handles a basic <see cref="MessageBoxMessage" /> on a flyout container by showing a <see cref="ModernMessageBox" />
        /// and invokes the callback.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="owner">The owner.</param>
        /// <exception cref="System.ArgumentNullException">owner</exception>
        public static void HandleWithModern(this MessageBoxMessage message, FlyoutContainer owner)
        {
            if (owner == null) { throw new ArgumentNullException("owner"); }

            var d = owner.FindDispatcher();
            if (d != null && !d.CheckAccess())
            {
                d.BeginInvoke(new Action<Window>(win =>
                {
                    HandleWithModern(message, win);
                }), owner);
                return;
            }

            var res = ModernMessageBox.Show(owner, message.Content, message.Caption, message.Button, message.Icon, message.DefaultResult);
            message.DoCallback(res);
        }
    }
}
