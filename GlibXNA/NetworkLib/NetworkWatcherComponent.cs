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
                throw new ArgumentException("propertyName");
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
                throw new ArgumentException("propertyName");
            }

            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(propertyName);
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write("Integer");
            _dataWriters[Session.LocalGamers[senderIndex].Id].Write(data);
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
        /// <param name="dataType">The actual type of the data.</param>
        /// <param name="value">The actual data, represented as an instance of dataType.</param>
        /// <returns>Whether this method successfully parsed the data.</returns>
        /// <remarks>
        /// The string typeString should be compared case insensitively.
        /// It is not recommended to call the implementation of this method present in GLib (if you are subclassing this component), because it is a placeholder method.
        /// The base implementation of this method merely sets the output parameters to null and returns false. It should not be invoked.
        /// </remarks>
        protected virtual bool ParseData(PacketReader activeReader, string typeString, out Type dataType, out object value)
        {
            //Unless overridden in subclass, does nothing
            dataType = null;
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
                            Type type = null;
                            switch (typeStr.ToLower())
                            {
                                case "vector4":
                                    data = read.ReadVector4();
                                    type = typeof(Vector4);
                                    break;
                                case "string":
                                    data = read.ReadString();
                                    type = typeof(string);
                                    break;
                                case "double":
                                    data = read.ReadDouble();
                                    type = typeof(double);
                                    break;
                                case "matrix":
                                    data = read.ReadMatrix();
                                    type = typeof(Matrix);
                                    break;
                                case "integer":
                                    data = read.ReadInt32();
                                    type = typeof(int);
                                    break;
                                default:
                                    if (!ParseData(read, typeStr, out type, out data))
                                    {
                                        throw new InvalidCastException("The received data could not be parsed as a recognized type.");
                                    }
                                    break;
                            }
                            #endregion

                            if (NetworkInformationReceived != null)
                            {
                                NetworkInformationReceivedEventArgs args = new NetworkInformationReceivedEventArgs();
                                args.PropertyName = prop;
                                args.Data = data;
                                args.ReceivedType = type;
                                args.Sender = sender;
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
