using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// A class representing the event arguments for the NetworkInformationReceived event.
    /// </summary>
    public class NetworkInformationReceivedEventArgs : EventArgs
    {
        private string _propName;

        /// <summary>
        /// Create a new <see cref="NetworkInformationReceivedEventArgs"/> with no data.
        /// </summary>
        protected internal NetworkInformationReceivedEventArgs() { }

        /// <summary>
        /// Gets the name of the received property.
        /// </summary>
        public string PropertyName
        {
            get { return _propName; }
            protected internal set { _propName = value; }
        }

        /// <summary>
        /// Gets the received data.
        /// </summary>
        /// <remarks>
        /// This will always be of type ReceivedType.
        /// </remarks>
        public object Data
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the recipient of this property information.
        /// </summary>
        public Microsoft.Xna.Framework.Net.LocalNetworkGamer Recipient
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the sender of this property information.
        /// </summary>
        public Microsoft.Xna.Framework.Net.NetworkGamer Sender
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the type of the received data.
        /// </summary>
        public Type ReceivedType
        {
            get;
            protected internal set;
        }
        
    }
}
