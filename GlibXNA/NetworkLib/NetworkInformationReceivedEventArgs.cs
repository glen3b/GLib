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
        /// <summary>
        /// Create a new <see cref="NetworkInformationReceivedEventArgs"/> with no data.
        /// </summary>
        protected internal NetworkInformationReceivedEventArgs() { }

        

        /// <summary>
        /// Gets the recipient of this property information.
        /// </summary>
        public Microsoft.Xna.Framework.Net.LocalNetworkGamer Recipient
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the sent data.
        /// </summary>
        public NetworkData Data
        {
            get;
            protected internal set;
        }
        
    }
}
