using System;
using System.Collections.Generic;

namespace ModernWpf.ViewModels
{
    /// <summary>
    /// An observable collection that will dispose items when they are removed.
    /// </summary>
    /// <typeparam name="T">A type that implements <see cref="IDisposable" />.</typeparam>
    public class AutoDisposeObservableCollection<T> : AutoCleanupObservableCollection<T> where T : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDisposeObservableCollection{T}"/> class.
        /// </summary>
        public AutoDisposeObservableCollection() : base(t => t.Dispose()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDisposeObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        public AutoDisposeObservableCollection(IEnumerable<T> collection) : base(collection, t => t.Dispose()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoDisposeObservableCollection{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public AutoDisposeObservableCollection(List<T> collection) : base(collection, t => t.Dispose()) { }

    }
}
