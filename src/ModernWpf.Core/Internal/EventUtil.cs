using System;
using System.Globalization;
using System.Windows;

namespace ModernWpf.Internal
{
    static class EventUtil
    {
        public static void AddHandler(this DependencyObject element, RoutedEvent routedEvent, Delegate handler)
        {
            if (element == null) { throw new ArgumentNullException("element"); }

            if (!DoSomethingAs(element as UIElement, uie => uie.AddHandler(routedEvent, handler)) &&
                !DoSomethingAs(element as ContentElement, ce => ce.AddHandler(routedEvent, handler)) &&
                !DoSomethingAs(element as UIElement3D, u3d => u3d.AddHandler(routedEvent, handler)))
            {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Invalid element {0}.", element.GetType()));
            }
        }

        public static void RemoveHandler(this DependencyObject element, RoutedEvent routedEvent, Delegate handler)
        {
            if (element == null) { throw new ArgumentNullException("element"); }

            if (!DoSomethingAs(element as UIElement, uie => uie.RemoveHandler(routedEvent, handler)) &&
                !DoSomethingAs(element as ContentElement, ce => ce.RemoveHandler(routedEvent, handler)) &&
                !DoSomethingAs(element as UIElement3D, u3d => u3d.RemoveHandler(routedEvent, handler)))
            {
                throw new NotSupportedException(string.Format(CultureInfo.InvariantCulture, "Invalid element {0}.", element.GetType()));
            }
        }


        static bool DoSomethingAs<T>(T something, Action<T> callback) where T : class
        {
            if (something == null) { return false; }
            callback(something);
            return true;
        }
    }
}
