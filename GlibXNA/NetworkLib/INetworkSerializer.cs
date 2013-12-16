using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Represents a type for preparing arbitrary data for network transmission as a string
    /// </summary>
    public interface INetworkSerializer
    {
        /// <summary>
        /// Serializes the speficied data.
        /// </summary>
        /// <param name="data">The data to serialize.</param>
        /// <returns>The serialized data.</returns>
        string Serialize(object data);

        /// <summary>
        /// Deserializes the speficied data.
        /// </summary>
        /// <param name="serializedData">The data as a string.</param>
        /// <returns>The deserialized data.</returns>
        object Deserialize(String serializedData);

        /// <summary>
        /// Gets the supported type of this serializer.
        /// </summary>
        Type SupportedType { get; }
    }
}
