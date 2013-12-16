using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// Serializes <see cref="Vector2"/> objects.
    /// </summary>
    public sealed class Vector2Serializer : INetworkSerializer
    {
        /// <summary>
        /// Serializes the speficied data.
        /// </summary>
        /// <param name="indata">The data to serialize.</param>
        /// <returns>The serialized data.</returns>
        public string Serialize(object indata)
        {
            Vector2 data = (Vector2)indata;
            return String.Format("({0},{1})", data.X, data.Y);
        }

        private Vector2Serializer() { }

        static Vector2Serializer()
        {
            Instance = new Vector2Serializer();
        }

        /// <summary>
        /// Gets the instance of the serializer.
        /// </summary>
        public static Vector2Serializer Instance { get; private set; }

        /// <summary>
        /// Deserializes the speficied data.
        /// </summary>
        /// <param name="serializedData">The data, which should be an instance of <see cref="Vector2"/>, as a string.</param>
        /// <returns>The deserialized data.</returns>
        public object Deserialize(string serializedData)
        {
            string[] numbers = serializedData.Replace("(", "").Replace(")", "").Split(',');
            return new Vector2(numbers[0].ToFloat(), numbers[1].ToFloat());
        }

        /// <summary>
        /// Gets the supported type of this serializer.
        /// </summary>
        public Type SupportedType
        {
            get { return typeof(Vector2); }
        }
    }

    /// <summary>
    /// Serializes <see cref="Vector3"/> objects.
    /// </summary>
    public sealed class Vector3Serializer : INetworkSerializer
    {
        /// <summary>
        /// Serializes the speficied data.
        /// </summary>
        /// <param name="indata">The data to serialize.</param>
        /// <returns>The serialized data.</returns>
        public string Serialize(object indata)
        {
            Vector3 data = (Vector3)indata;
            return String.Format("({0},{1},{2})", data.X, data.Y,data.Z);
        }

        private Vector3Serializer() { }

        static Vector3Serializer()
        {
            Instance = new Vector3Serializer();
        }

        /// <summary>
        /// Gets the instance of the serializer.
        /// </summary>
        public static Vector3Serializer Instance { get; private set; }

        /// <summary>
        /// Deserializes the speficied data.
        /// </summary>
        /// <param name="serializedData">The data, which should be an instance of <see cref="Vector3"/>, as a string.</param>
        /// <returns>The deserialized data.</returns>
        public object Deserialize(string serializedData)
        {
            string[] numbers = serializedData.Replace("(", "").Replace(")", "").Split(',');
            return new Vector3(numbers[0].ToFloat(), numbers[1].ToFloat(), numbers[2].ToFloat());
        }

        /// <summary>
        /// Gets the supported type of this serializer.
        /// </summary>
        public Type SupportedType
        {
            get { return typeof(Vector3); }
        }
    }

    /// <summary>
    /// Serializes <see cref="Vector4"/> objects.
    /// </summary>
    public sealed class Vector4Serializer : INetworkSerializer
    {
        /// <summary>
        /// Serializes the speficied data.
        /// </summary>
        /// <param name="indata">The data to serialize.</param>
        /// <returns>The serialized data.</returns>
        public string Serialize(object indata)
        {
            Vector4 data = (Vector4)indata;
            return String.Format("({0},{1},{2},{3})", data.X, data.Y, data.Z, data.W);
        }

        private Vector4Serializer() { }

        static Vector4Serializer()
        {
            Instance = new Vector4Serializer();
        }

        /// <summary>
        /// Gets the instance of the serializer.
        /// </summary>
        public static Vector4Serializer Instance { get; private set; }

        /// <summary>
        /// Deserializes the speficied data.
        /// </summary>
        /// <param name="serializedData">The data, which should be an instance of <see cref="Vector4"/>, as a string.</param>
        /// <returns>The deserialized data.</returns>
        public object Deserialize(string serializedData)
        {
            string[] numbers = serializedData.Replace("(", "").Replace(")", "").Split(',');
            return new Vector4(numbers[0].ToFloat(), numbers[1].ToFloat(), numbers[2].ToFloat(), numbers[3].ToFloat());
        }

        /// <summary>
        /// Gets the supported type of this serializer.
        /// </summary>
        public Type SupportedType
        {
            get { return typeof(Vector4); }
        }
    }
}
