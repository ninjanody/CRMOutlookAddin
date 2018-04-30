namespace CrmOutlookAddin.Utils
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A list which will not be corrupted if used by two or more threads concurrently.
    /// </summary>
    /// <remarks>
    /// C# does not provide a ConcurrentList class which shares the same API as IList; in particular
    /// ConcurrentBag (the nearest equivalent) has no Exists predicate. So I've rolled my own; this
    /// may not be maximally efficient but it should be safe.
    /// </remarks>
    /// <typeparam name="T">The type of item which may be stored in this list.</typeparam>
    public class ThreadSafeList<T> : IList<T>
    {
        /// <summary>
        /// The lock which prevents concurrent access to the underlying list.
        /// </summary>
        private object padlock = new object();

        /// <summary>
        /// The actual list in which items are stored.
        /// </summary>
        private List<T> underlying;

        /// <summary>
        /// Construct a new instance of a ThreadSafeList.
        /// </summary>
        public ThreadSafeList()
        {
            this.underlying = new List<T>();
        }

        public int Count
        {
            get
            {
                lock (this.padlock)
                {
                    return this.underlying.Count;
                }
            }
        }

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                lock (this.padlock)
                {
                    return this.underlying[index];
                }
            }

            set
            {
                lock (this.padlock)
                {
                    this.underlying[index] = value;
                }
            }
        }

        public void Add(T item)
        {
            lock (this.padlock)
            {
                this.underlying.Add(item);
            }
        }

        public void Clear()
        {
            lock (this.padlock)
            {
                this.underlying.Clear();
            }
        }

        public bool Contains(T item)
        {
            lock (this.padlock)
            {
                // Does this need to be locked? Not certain. Err on the side of caution.
                return this.underlying.Contains(item);
            }
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            lock (this.padlock)
            {
                this.underlying.CopyTo(array, arrayIndex);
            }
        }

        public bool Exists(Func<T, bool> p)
        {
            lock (this.padlock)
            {
                return this.underlying.Exists(x => p(x));
            }
        }

        public T FirstOrDefault(Func<T, bool> p)
        {
            lock (this.padlock)
            {
                return this.underlying.FirstOrDefault(x => p(x));
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            lock (this.padlock)
            {
                /* Dodgy, dodgy. An enumerator on this.underlying itself
                 * could break because underlying could be modified by another
                 * thread after this method has released the lock. So we need
                 * to make an immutable copy of this.underlying and return an
                 * enumerator on that. This still doesn't avoid the problem
                 * that actual Outlook items might be smashed under us. */
                List<T> copy = new List<T>();
                copy.AddRange(this.underlying);
                return copy.AsReadOnly().GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (this.padlock)
            {
                /* There's probably some cleaner way of writing the non-generic
                 * variant of GetEnumerator by referencing the generic variant,
                 * but I don't know it. */
                return new List<T>(this.underlying).AsReadOnly().GetEnumerator();
            }
        }

        public int IndexOf(T item)
        {
            lock (this.padlock)
            {
                return this.underlying.IndexOf(item);
            }
        }

        public void Insert(int index, T item)
        {
            lock (this.padlock)
            {
                this.underlying.Insert(index, item);
            }
        }

        public bool Remove(T item)
        {
            lock (this.padlock)
            {
                return this.underlying.Remove(item);
            }
        }

        public void RemoveAt(int index)
        {
            lock (this.padlock)
            {
                this.underlying.RemoveAt(index);
            }
        }
    }
}
