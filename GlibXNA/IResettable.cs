using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA
{
    /// <summary>
    /// Represents an object which can be reset to its initial loaded state without re-creation.
    /// Intended for use by object pools.
    /// </summary>
    public interface IResettable
    {
        /// <summary>
        /// Resets the object to its initial useable state.
        /// </summary>
        void Reset();
    }
}
