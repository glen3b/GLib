using System;
namespace Glib.XNA
{
    public interface IObjectPool<TPooled>
     where TPooled : IResettable
    {
        /// <summary>
        /// Gets an object from the pool.
        /// </summary>
        /// <returns>A useable object from the object pool.</returns>
        public TPooled GetObject();

        /// <summary>
        /// Returns an object that was created by this pool to the pool to be reset.
        /// </summary>
        /// <param name="element">The object to return to the pool.</param>
        public void ReturnObject(TPooled element);

        /// <summary>
        /// The total number of elements tracked by the pool.
        /// </summary>
        public int Size { get; }
    }
}
