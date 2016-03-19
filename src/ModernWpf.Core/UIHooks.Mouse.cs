using System;
using System.Windows;
using System.Windows.Input;

namespace ModernWpf
{
    // Contains extra mouse events when using the modern <see cref="Chrome"/> on a <see cref="Window"/>.
    
    static partial class UIHooks
    {
        /// <summary>
        /// Identifies the PreviewMouseHWheel event.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseHWheelEvent =
            EventManager.RegisterRoutedEvent("PreviewMouseHWheel", RoutingStrategy.Tunnel, typeof(MouseWheelEventHandler), typeof(UIHooks));


        /// <summary>
        /// Adds a handler to the PreviewMouseHWheel event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void AddPreviewMouseHWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
        {
            element.AddHandler(PreviewMouseHWheelEvent, handler);
        }

        /// <summary>
        /// Removes a handler to the PreviewMouseHWheel event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void RemovePreviewMouseHWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
        {
            element.RemoveHandler(PreviewMouseHWheelEvent, handler);
        }


        /// <summary>
        /// Identifies the MouseHWheel event.
        /// </summary>
        public static readonly RoutedEvent MouseHWheelEvent =
            EventManager.RegisterRoutedEvent("MouseHWheel", RoutingStrategy.Bubble, typeof(MouseWheelEventHandler), typeof(UIHooks));

        /// <summary>
        /// Adds a handler to the MouseHWheel event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void AddMouseHWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
        {
            element.AddHandler(MouseHWheelEvent, handler);
        }

        /// <summary>
        /// Removes a handler to the MouseHWheel event.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="handler">The handler.</param>
        public static void RemoveMouseHWheelHandler(DependencyObject element, MouseWheelEventHandler handler)
        {
            element.RemoveHandler(MouseHWheelEvent, handler);
        }
    }
}
