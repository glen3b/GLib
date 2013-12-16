using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using System.Text;


namespace Glib.XNA.NetworkLib
{
    /// <summary>
    /// A game component for watching the network for information and sending information.
    /// </summary>
    /// <remarks>
    /// Supports sending various types entailed in the WriteData overloads.
    /// </remarks>
    public class NetworkWatcherComponent : Microsoft.Xna.Framework.GameComponent
    {
        /// <summary>
        /// Create a new <see cref="NetworkWatcherComponent"/>.
        /// </summary>
        /// <param name="game">The game to add this component to.</param>
        /// <remarks>
        /// Before this <see cref="NetworkWatcherComponent"/> is active, the <see cref="Session"/> property must be assigned.
        /// </remarks>
        public NetworkWatcherComponent(Game game)
            : base(game)
        {
        }


        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Gets a boolean indicating whether this <see cref="NetworkWatcherComponent"/> is currently performing network logic.
        /// </summary>
        public Boolean IsNetworking
        {
            get
            {
                return Enabled && _session != null && !_session.IsDisposed;
            }
        }

        private NetworkSession _session;

        /// <summary>
        /// Gets or sets the <see cref="NetworkSession"/> that this <see cref="NetworkWatcherComponent"/> uses to send and receive data.
        /// </summary>
        public NetworkSession Session
        {
            get { return _session; }
            set
            {

                if (value != _session)
                {
                    if (value != null)
                    {
                        value.GamerJoined += new EventHandler<GamerJoinedEventArgs>(value_GamerJoined);
                        value.GamerLeft += new EventHandler<GamerLeftEventArgs>(value_GamerLeft);
                    }
                    _session = value;
                }
            }
        }

        private void value_GamerLeft(object sender, GamerLeftEventArgs e)
        {
            if (e.Gamer.IsLocal)
            {
                _dataWriters.Remove(e.Gamer.Id);
                _dataReaders.Remove(e.Gamer.Id);
            }
        }

        private void value_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            if (e.Gamer.IsLocal)
            {
                _dataWriters.Add(e.Gamer.Id, new PacketWriter());
                _dataReaders.Add(e.Gamer.Id, new PacketReader());
            }
        }


        /// <summary>
        /// A dictionary holding <see cref="PacketReader"/>s for every <see cref="LocalNetworkGamer"/>.
        /// </summary>
        protected Dictionary<byte, PacketReader> _dataReaders = new Dictionary<byte, PacketReader>();

        /// <summary>
        /// A dictionary holding <see cref="PacketWriter"/>s for every <see cref="LocalNetworkGamer"/>.
        /// </summary>
        /// <remarks>
        /// These <see cref="PacketWriter"/>s are written to in the following format:
        /// - String name
        /// - String type
        /// - Type value
        /// </remarks>
        protected Dictionary<byte, PacketWriter> _dataWriters = new Dictionary<byte, PacketWriter>();

        #region WriteData
        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - three strings (one is the data itself).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, string data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentOutOfRangeException("senderIndex");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("String");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and a double (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, Double data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentOutOfRangeException("senderIndex");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Double");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and a Vector4 (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, Vector4 data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentOutOfRangeException("senderIndex");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Vector4");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and a Matrix (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, Matrix data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentException("The senderIndex parameter is outside of the bounds of Session.LocalGamers.");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Matrix");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and an integer (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, int data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentException("The senderIndex parameter is outside of the bounds of Session.LocalGamers.");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Integer");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and a vector array (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, Vector4[] data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentException("The senderIndex parameter is outside of the bounds of Session.LocalGamers.");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Vector4Array");
            StringBuilder array = new StringBuilder();
            foreach (Vector4 v4 in data)
            {
                array.Append(string.Format("({0},{1},{2},{3})", v4.X, v4.Y, v4.Z, v4.W));
                array.Append('\0');
            }
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(array.ToString());
        }

        /// <summary>
        /// Writes data to the specified <see cref="LocalNetworkGamer"/>'s <see cref="PacketWriter"/> to be sent across the network.
        /// </summary>
        /// <param name="propertyName">The name of the property to send.</param>
        /// <param name="senderIndex">The index of the sender in the <see cref="Session"/>'s LocalGamers collection.</param>
        /// <param name="data">The data to send.</param>
        /// <remarks>
        /// Sends 3 values - two strings and an integer array (the data).
        /// </remarks>
        public virtual void WriteData(string propertyName, int senderIndex, int[] data)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentException("The senderIndex parameter is outside of the bounds of Session.LocalGamers.");
            }
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("IntegerArray");
            StringBuilder array = new StringBuilder();
            foreach (int i in data)
            {
                array.Append(i);
                array.Append('\0');
            }
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(array.ToString());
        }
        #endregion


        /// <summary>
        /// Sends data for the specified <see cref="LocalNetworkGamer"/> across the network.
        /// </summary>
        /// <param name="senderIndex">The index of the <see cref="LocalNetworkGamer"/> that is in Session.LocalGamers to send data for.</param>
        public void SendData(int senderIndex)
        {
            SendData(senderIndex, SendDataOptions.None);
        }

        /// <summary>
        /// Sends data for the specified <see cref="LocalNetworkGamer"/> across the network.
        /// </summary>
        /// <param name="senderIndex">The index of the <see cref="LocalNetworkGamer"/> that is in Session.LocalGamers to send data for.</param>
        /// <param name="options">The <see cref="SendDataOptions"/> to send the data with.</param>
        public void SendData(int senderIndex, SendDataOptions options)
        {
            SendData(senderIndex, null, options);
        }

        /// <summary>
        /// Sends data for the specified <see cref="LocalNetworkGamer"/> across the network.
        /// </summary>
        /// <param name="senderIndex">The index of the <see cref="LocalNetworkGamer"/> that is in Session.LocalGamers to send data for.</param>
        /// <param name="recipient">The recipient of the data, or null if to send to everyone.</param>
        /// <param name="options">The <see cref="SendDataOptions"/> to send the data with.</param>
        public virtual void SendData(int senderIndex, NetworkGamer recipient, SendDataOptions options)
        {
            if (!IsNetworking)
            {
                throw new InvalidOperationException("This component is not currently active.");
            }
            if (senderIndex >= Session.LocalGamers.Count || senderIndex < 0)
            {
                throw new ArgumentOutOfRangeException("senderIndex", "The senderIndex parameter is outside of the bounds of Session.LocalGamers.");
            }
            if (recipient != null)
            {
                Session.LocalGamers[senderIndex].SendData(_dataWriters[Session.LocalGamers[senderIndex].Id], options, recipient);
            }
            else
            {
                Session.LocalGamers[senderIndex].SendData(_dataWriters[Session.LocalGamers[senderIndex].Id], options);
            }
        }

        /// <summary>
        /// Parse the incoming data from the specified <see cref="PacketReader"/> which is of the specified unknown type.
        /// </summary>
        /// <param name="activeReader">The <see cref="PacketReader"/> containing the data.</param>
        /// <param name="typeString">The string representing the type of the data, according to the sender.</param>
        /// <param name="value">The actual data, represented as an instance of the type represented by typeString.</param>
        /// <returns>Whether this method successfully parsed the data.</returns>
        /// <remarks>
        /// The string typeString should be compared case insensitively.
        /// It is not recommended to call the implementation of this method present in GLib (if you are subclassing this component), because it is a placeholder method.
        /// The base implementation of this method merely sets the output parameters to null and returns false. It should not be invoked.
        /// </remarks>
        protected virtual bool ParseData(PacketReader activeReader, string typeString, out object value)
        {
            //Unless overridden in subclass, does nothing
            value = null;
            return false;
        }

        /// <summary>
        /// An event fired when information is received from the network.
        /// </summary>
        /// <remarks>
        /// If you do not subscribe to this event, sending information over the network is pointless.
        /// </remarks>
        public event EventHandler<NetworkInformationReceivedEventArgs> NetworkInformationReceived;

        /// <summary>
        /// Receives data from the network and updates the <see cref="NetworkSession"/>.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (IsNetworking)
            {
                foreach (var gamer in Session.LocalGamers)
                {
                    if (!gamer.IsDataAvailable)
                    {
                        continue;
                    }

                    while (gamer.IsDataAvailable)
                    {
                        NetworkGamer sender;
                        PacketReader read = _dataReaders[gamer.Id];
                        gamer.ReceiveData(read, out sender);

                        while (read.BaseStream.Position != read.BaseStream.Length)
                        {
                            #region Receive data as type
                            string prop = read.ReadString();
                            string typeStr = read.ReadString();
                            object data = null;
                            switch (typeStr.ToLower())
                            {
                                case "vector4":
                                    data = read.ReadVector4();
                                    break;
                                case "string":
                                    data = read.ReadString();
                                    break;
                                case "double":
                                    data = read.ReadDouble();
                                    break;
                                case "matrix":
                                    data = read.ReadMatrix();
                                    break;
                                case "integer":
                                    data = read.ReadInt32();
                                    break;
                                case "vector4array":
                                    string[] datas = read.ReadString().Split('\0');
                                    Vector4[] result = new Vector4[datas.Length];
                                    for (int i = 0; i < result.Length; i++)
                                    {
                                        string[] numbas = datas[i].Replace("(", "").Replace(")", "").Split(',');
                                        result[i] = new Vector4(float.Parse(numbas[0]), float.Parse(numbas[1]), float.Parse(numbas[2]), float.Parse(numbas[3]));
                                    }
                                    data = result;
                                    break;
                                case "integerarray":
                                    string[] ints = read.ReadString().Split('\0');
                                    int[] intRes = new int[ints.Length];
                                    for (int i = 0; i < intRes.Length; i++)
                                    {
                                        intRes[i] = ints[i].ToInt();
                                    }
                                    data = intRes;
                                    break;
                                default:

                                    if (!ParseData(read, typeStr, out data))
                                    {
                                        throw new InvalidCastException("The received data could not be parsed as a recognized type.");
                                    }
                                    break;
                            }
                            #endregion

                            if (NetworkInformationReceived != null)
                            {
                                NetworkInformationReceivedEventArgs args = new NetworkInformationReceivedEventArgs();
                                args.Data = new NetworkData(prop, data, sender);
                                args.Recipient = gamer;
                                NetworkInformationReceived(this, args);
                            }
                        }

                    }

                }
            }

            base.Update(gameTime);
        }
    }
}
