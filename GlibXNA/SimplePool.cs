using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA
{
    /// <summary>
    /// Represents a simple object pool.
    /// It is not threadsafe, and it is designed for objects which need a state reset upon being returned to the pool.
    /// The pool will never shrink, but will enlarge if needed.
    /// </summary>
    /// <typeparam name="TPooled">The type of the objects contained in the pool.</typeparam>
    public class SimplePool<TPooled> : Glib.XNA.IObjectPool<TPooled> where TPooled : IResettable
    {
        private Queue<TPooled> _backingCollection;

        /// <summary>
        /// Creates a pool with the specified initial size and object factory.
        /// </summary>
        /// <param name="initialSize">The initial size of the pool.</param>
        /// <param name="factory"></param>
        public SimplePool(int initialSize, ObjectFactory factory)
        {
            if (initialSize <= 0)
            {
                throw new ArgumentException("The initial size must be positive.");
            }

            _size = initialSize;
            Factory = factory;

            _backingCollection = new Queue<TPooled>(initialSize);

            for (int i = 0; i < initialSize; i++)
            {
                _backingCollection.Enqueue(Factory(this));
            }
        }

        private int _refillAmount;

        /// <summary>
        /// Gets or sets the amount of elements to add to the pool when the collection of non-checked-out elements is empty after an object retrieval.
        /// </summary>
        public int RefillAmount
        {
            get { return _refillAmount; }
            set
            {
                if (value <= 0)
                {
                    throw new InvalidOperationException("The refill amount must be at least 1.");
                }
                _refillAmount = value;
            }
        }


        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        /// <returns>An object from the pool.</returns>
        public virtual TPooled GetObject()
        {
            if (_backingCollection.Count == 1)
            {
                // Will be empty after this operation, so add some elements
                for (int i = 0; i < RefillAmount; i++)
                {
                    _backingCollection.Enqueue(Factory(this));
                }
                _size += RefillAmount;
            }

            return _backingCollection.Dequeue();
        }

        /// <summary>
        /// Returns an element to the pool.
        /// The results are undefined if the element was not created by the pool.
        /// </summary>
        /// <param name="element">The element to return to the pool.</param>
        public virtual void ReturnObject(TPooled element)
        {
            element.Reset();
            _backingCollection.Enqueue(element);
        }

        /// <summary>
        /// A delegate method which is used for the creation of pooled objects.
        /// </summary>
        /// <param name="pool">The pool which will contain the object.</param>
        /// <returns>The new object.</returns>
        public delegate TPooled ObjectFactory(SimplePool<TPooled> pool);

        private ObjectFactory _factory;

        /// <summary>
        /// Gets or sets the method which is used for the creation of pooled objects.
        /// </summary>
        public virtual ObjectFactory Factory
        {
            get { return _factory; }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("A factory method must be supplied.");
                }
                _factory = value;
            }
        }

        private int _size;

        /// <summary>
        /// Gets the current underlying pool size.
        /// This includes checked-out objects and objects currently in the pool.
        /// </summary>
        public int Size
        {
            get
            {
                return _size;
            }
        }

        /// <summary>
        /// Gets the number of elements that are not in use which are contained by this pool.
        /// </summary>
        public int ElementCount
        {
            get
            {
                return _backingCollection.Count;
            }
        }
    }

    /// <summary>
    /// Provides many utility object factory delegate creation methods.
    /// </summary>
    public static class ObjectFactories
    {
        /// <summary>
        /// Creates an object factory method from a parameterless constructor method.
        /// </summary>
        /// <typeparam name="T">The type of the objects to create.</typeparam>
        /// <param name="method">The method which creates objects.</param>
        /// <returns>An object factory delegate instance.</returns>
        public static SimplePool<T>.ObjectFactory FromParameterlessMethod<T>(Func<T> method) where T : IResettable
        {
            return pool => method();
        }

        /// <summary>
        /// Creates an object factory method from a parameterless object constructor.
        /// </summary>
        /// <typeparam name="T">The type of the objects to create.</typeparam>
        /// <returns>An object factory delegate instance.</returns>
        public static SimplePool<T>.ObjectFactory FromConstructor<T>() where T : IResettable, new()
        {
            return pool => new T();
        }

        /// <summary>
        /// Creates an object factory method from a method of another delegate type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to create.</typeparam>
        /// <param name="func">The method which creates objects.</param>
        /// <returns>An object factory delegate instance.</returns>
        public static SimplePool<T>.ObjectFactory FromGenericFunction<T>(Func<SimplePool<T>, T> func) where T : IResettable
        {
            return pool => func(pool);
        }
    }
}
