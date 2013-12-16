using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Net;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Represents network information.
    /// </summary>
    public class NetworkData
    {
        /// <summary>
        /// Creates a new piece of network information.
        /// </summary>
        /// <param name="dataName">The name identifying the information.</param>
        /// <param name="data">The actual data.</param>
        public NetworkData(string dataName, object data) : this(dataName, data, null) { }

        /// <summary>
        /// Creates a new piece of network information.
        /// </summary>
        /// <param name="dataName">The name identifying the information.</param>
        /// <param name="data">The actual data.</param>
        /// <param name="sender">The sender of network information.</param>
        public NetworkData(string dataName, object data, NetworkGamer sender)
        {
            if (dataName == null) { throw new ArgumentNullException("dataName"); }
            if (data == null) { throw new ArgumentNullException("data"); }

            Name = dataName;
            Data = data;
            Sender = sender;
        }

        private string _name;

        /// <summary>
        /// Gets the name of this information.
        /// </summary>
        public string Name
        {
            get { return _name; }
            protected internal set { if (value == null) { throw new ArgumentNullException(); } _name = value; }
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <remarks>
        /// This will always be of type DataType.
        /// </remarks>
        public object Data
        {
            get { return _data; }
            protected internal set { if (value == null) { throw new ArgumentNullException(); } _data = value; DataType = value.GetType(); }
        }

        private object _data;

        /// <summary>
        /// Gets the sender of this information.
        /// </summary>
        public Microsoft.Xna.Framework.Net.NetworkGamer Sender
        {
            get;
            protected internal set;
        }

        /// <summary>
        /// Gets the type of the network data.
        /// </summary>
        public Type DataType
        {
            get;
            protected internal set;
        }

    }
}
