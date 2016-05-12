using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ModernWpf.ViewModels
{
    /// <summary>
    /// An <see cref="ObservableCollection{T}"/> that provides a chance to do custom cleanup when items are removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="System.Collections.ObjectModel.ObservableCollection{T}" />
    public class AutoCleanupObservableCollection<T> : ObservableCollection<T>
    {
        Action<T> _cleanCall;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCleanupObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="cleanupCallback">The cleanup callback.</param>
        /// <exception cref="System.ArgumentNullException">cleanupCallback</exception>
        public AutoCleanupObservableCollection(Action<T> cleanupCallback)
        {
            if (cleanupCallback == null) { throw new ArgumentNullException("cleanupCallback"); }
            _cleanCall = cleanupCallback; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCleanupObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="cleanupCallback">The cleanup callback.</param>
        /// <exception cref="System.ArgumentNullException">cleanupCallback</exception>
        public AutoCleanupObservableCollection(IEnumerable<T> collection, Action<T> cleanupCallback) : base(collection)
        {
            if (cleanupCallback == null) { throw new ArgumentNullException("cleanupCallback"); }
            _cleanCall = cleanupCallback;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCleanupObservableCollection{T}" /> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="cleanupCallback">The cleanup callback.</param>
        /// <exception cref="ArgumentNullException">cleanupCallback</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public AutoCleanupObservableCollection(List<T> collection, Action<T> cleanupCallback) : base(collection)
        {
            if (cleanupCallback == null) { throw new ArgumentNullException("cleanupCallback"); }
            _cleanCall = cleanupCallback;
        }

        /// <summary>
        /// Replaces the element at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="item">The item.</param>
        protected override void SetItem(int index, T item)
        {
            T old = this[index];
            try
            {
                base.SetItem(index, item);
            }
            finally
            {
                if (old != null) { _cleanCall(old); }
            }
        }

        /// <summary>
        /// Removes all items from the collection.
        /// </summary>
        protected override void ClearItems()
        {
            // use a handle since the items may still be in UI.
            var handle = new List<T>(this);
            try
            {
                base.ClearItems();
            }
            finally
            {
                foreach (var old in handle) { _cleanCall(old); }
            }
        }

        /// <summary>
        /// Removes the item at the specified index of the collection.
        /// </summary>
        /// <param name="index">The index.</param>
        protected override void RemoveItem(int index)
        {
            T old = this[index];
            try
            {
                base.RemoveItem(index);
            }
            finally
            {
                if (old != null) { _cleanCall(old); }
            }
        }
    }
}
