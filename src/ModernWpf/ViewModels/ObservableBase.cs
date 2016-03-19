using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ModernWpf.ViewModels
{
    /// <summary>
    /// Extremely basic view model base class that implements <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void RaisePropertyChanged(string propertyName = null)
        {
            var handle = PropertyChanged;
            if (handle != null) { handle(this, new PropertyChangedEventArgs(propertyName)); }
        }

        /// <summary>
        /// Raises the property changed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Name of the property using linq expression.</param>
        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyName)
        {
            var handle = PropertyChanged;
            if (handle != null && propertyName != null)
            {
                var memberExpression = propertyName.Body as MemberExpression;
                if (memberExpression != null)
                {
                    handle(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }
    }
}
