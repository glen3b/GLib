using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Represents event arguments for the SessionsFound event.
    /// </summary>
    public class NetworkSessionsFoundEventArgs : EventArgs
    {
        private AvailableNetworkSessionCollection _sessions = null;

        private AvailableNetworkSession _toJoin = null;

        /// <summary>
        /// Gets or sets the <see cref="AvailableNetworkSession"/> to join, from the <see cref="AvailableSessions"/> collection.
        /// </summary>
        /// <remarks>
        /// This property does not have to be set.
        /// </remarks>
        public AvailableNetworkSession SessionToJoin
        {
            get { return _toJoin; }
            set { _toJoin = value; }
        }
        

        /// <summary>
        /// Gets the <see cref="AvailableNetworkSessionCollection"/> of available sessions found.
        /// </summary>
        /// <remarks>
        /// This will be null if an error occurred during the asynchronous operation.
        /// </remarks>
        public AvailableNetworkSessionCollection AvailableSessions
        {
            get { return _sessions; }
            protected internal set { _sessions = value; }
        }

        private Exception _error = null;

        /// <summary>
        /// Gets the error that occurred during the session retrieval, if any.
        /// </summary>
        public Exception Error
        {
            get { return _error; }
            protected internal set { _error = value; }
        }
        

        /// <summary>
        /// Gets a boolean indicating whether any <see cref="NetworkSession"/>s were found.
        /// </summary>
        public bool SessionsFound
        {
            get
            {
                return AvailableSessions == null ? false : AvailableSessions.Count > 0;
            }
        }

    }
}
