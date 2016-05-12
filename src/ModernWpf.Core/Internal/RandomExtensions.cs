using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace ModernWpf
{
    static class RandomExtensions
    {
        /// <summary>
        /// Finds the dispatcher if posible.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <returns></returns>
        public static Dispatcher FindDispatcher(this DispatcherObject owner)
        {
            if (owner != null) { return owner.Dispatcher; }
            if (Application.Current != null) { return Application.Current.Dispatcher; }
            return null;
        }

        /// <summary>
        /// Calls a possibly non-public instance method of a type using reflection.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public static object CallMethod(this Type type, string methodName, object instance, params object[] args)
        {
            var method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (method != null)
            {
                return method.Invoke(instance, args);
            }
            throw new NotSupportedException($"Method {methodName} is not supported for {type.Name}.");
        }

        ///// <summary>
        ///// Makes CA1305 go away with <see cref="CultureInfo.InvariantCulture"/>.
        ///// </summary>
        ///// <param name="interpolated">The interpolated string.</param>
        ///// <returns></returns>
        //public static string Invariant(this IFormattable interpolated)
        //{
        //    return interpolated.ToString(null, CultureInfo.InvariantCulture);
        //}
    }
}
