using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Represents event arguments for the SessionJoined event.
    /// </summary>
    public class NetworkSessionJoinedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the <see cref="NetworkSession"/> that was joined.
        /// </summary>
        /// <remarks>
        /// This will be null if an error occurred during the asynchronous operation.
        /// </remarks>
        public NetworkSession Joined
        {
            get;
            protected internal set;
        }

        private Exception _error = null;

        /// <summary>
        /// Gets the error that occurred during the session joining, if any.
        /// </summary>
        public Exception Error
        {
            get { return _error; }
            protected internal set { _error = value; }
        }

    }
}
