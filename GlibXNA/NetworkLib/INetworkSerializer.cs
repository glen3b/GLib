using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Represents a type for preparing arbitrary data for network transmission as a string
    /// </summary>
    /// <typeparam name="TSerialized">The serialized type.</typeparam>
    public interface INetworkSerializer<TSerialized>
    {
        /// <summary>
        /// Serializes the speficied data.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <returns>The serialized data.</returns>
        string Serialize(TSerialized data);

        /// <summary>
        /// Deserializes the speficied data.
        /// </summary>
        /// <param name="serializedData">The data as a string.</param>
        /// <returns>The deserialized data.</returns>
        TSerialized Deserialize(String serializedData);
    }
}
