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
    /// A game component that manages a <see cref="NetworkSession"/>.
    /// </summary>
    public class SessionManagerComponent : Microsoft.Xna.Framework.GameComponent
    {
        /// <summary>
        /// Creates a <see cref="SessionManagerComponent"/> for the specified <see cref="Microsoft.Xna.Framework.Game"/>.
        /// </summary>
        /// <param name="game">The <see cref="Microsoft.Xna.Framework.Game"/> that this component will be added to.</param>
        public SessionManagerComponent(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Gets a boolean indicating whether we are in an active network session.
        /// </summary>
        public bool InSession
        {
            get
            {
                return Session != null && !Session.IsDisposed && Session.AllGamers.Count > 0;
            }
        }

        /// <summary>
        /// Gets the <see cref="NetworkSession"/> that this <see cref="SessionManagerComponent"/> is managing.
        /// </summary>
        public NetworkSession Session
        {
            get;
            private set;
        }

        /// <summary>
        /// Begins the process of asynchronously finding <see cref="AvailableNetworkSession"/>s.
        /// </summary>
        /// <param name="type">The type of the <see cref="NetworkSession"/> to find.</param>
        /// <param name="gamers">The maximum number of local gamers to allow within the <see cref="NetworkSession"/>.</param>
        /// <param name="reqProps">The <see cref="NetworkSessionProperties"/> that must be present on the found <see cref="NetworkSession"/>, or null if none.</param>
        public void FindSessions(NetworkSessionType type, int gamers, NetworkSessionProperties reqProps)
        {
            if (_asyncSessionOperationsInProgress > 0)
            {
                throw new InvalidOperationException("An asynchronous session operation is already in progress.");
            }
            _asyncSessionOperationsInProgress++;

            NetworkSession.BeginFind(type, gamers, reqProps, NetSessionsFound, null);
        }

        /// <summary>
        /// An event fired after a network session has been asynchronously joined.
        /// </summary>
        public event EventHandler<NetworkSessionJoinedEventArgs> SessionJoined;

        /// <summary>
        /// An event fired after network sessions have been asynchronously found.
        /// </summary>
        public event EventHandler<NetworkSessionsFoundEventArgs> SessionsFound;

        private void NetSessionsFound(IAsyncResult res)
        {
            _asyncSessionOperationsInProgress--;

            if (SessionsFound != null)
            {
                NetworkSessionsFoundEventArgs eventArgs = new NetworkSessionsFoundEventArgs();
                try
                {
                    eventArgs.AvailableSessions = NetworkSession.EndFind(res);
                }
                catch (Exception ex)
                {
                    eventArgs.Error = ex;
                }

                SessionsFound(this, eventArgs);

                if (eventArgs.SessionToJoin != null)
                {
                    JoinSession(eventArgs.SessionToJoin);
                }
            }
        }

        /// <summary>
        /// Begins to asynchronously join the specified <see cref="AvailableNetworkSession"/>.
        /// </summary>
        /// <param name="toJoin">The session to join.</param>
        public void JoinSession(AvailableNetworkSession toJoin)
        {
            if (toJoin == null)
            {
                throw new ArgumentNullException("toJoin");
            }
            if (_asyncSessionOperationsInProgress > 0)
            {
                throw new InvalidOperationException("An asynchronous session operation is already in progress.");
            }
            _asyncSessionOperationsInProgress++;
            NetworkSession.BeginJoin(toJoin, NetSessionJoin, null);
        }

        /// <summary>
        /// Join the specified <see cref="NetworkSession"/>.
        /// </summary>
        /// <param name="session">The session to join.</param>
        public void JoinSession(NetworkSession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            if (session.IsDisposed)
            {
                throw new ObjectDisposedException("session");
            }
            if (_asyncSessionOperationsInProgress > 0)
            {
                throw new InvalidOperationException("An asynchronous session operation is already in progress.");
            }
            Session = session;
        }

        /// <summary>
        /// Leave the current network session.
        /// </summary>
        public void LeaveSession()
        {
            if (Session == null)
            {
                return;
            }

            if (!Session.IsDisposed)
            {
                Session.Dispose();
            }

            Session = null;
        }

        private void NetSessionJoin(IAsyncResult res)
        {
            _asyncSessionOperationsInProgress--;
            Exception err = null;
            NetworkSession joined = null;
            try
            {
                joined = NetworkSession.EndJoin(res);
            }
            catch (Exception e)
            {
                err = e;
            }

            Session = joined;

            if (SessionJoined != null)
            {
                SessionJoined(this, new NetworkSessionJoinedEventArgs() { Error = err, Joined = joined });
            }
        }
        private int _asyncSessionOperationsInProgress = 0;

        /// <summary>
        /// Begins the process of asynchronously finding <see cref="AvailableNetworkSession"/>s.
        /// </summary>
        /// <param name="type">The type of the <see cref="NetworkSession"/> to find.</param>
        /// <param name="gamers">The maximum number of local gamers to allow within the <see cref="NetworkSession"/>.</param>
        public void FindSessions(NetworkSessionType type, int gamers)
        {
            FindSessions(type, gamers, null);
        }

        /// <summary>
        /// An event fired when the session has ended.
        /// </summary>
        /// <remarks>
        /// This event will throw an exception if the <see cref="Session"/> property is null.
        /// </remarks>
        public event EventHandler<NetworkSessionEndedEventArgs> SessionEnded
        {
            remove
            {
                if (Session == null)
                {
                    throw new InvalidOperationException("The session has not been created.");
                }
                Session.SessionEnded -= value;
            }

            add
            {
                if (Session == null)
                {
                    throw new InvalidOperationException("The session has not been created.");
                }
                Session.SessionEnded += value;
            }
        }

        /// <summary>
        /// Updates the <see cref="SessionManagerComponent"/>.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (Session != null && !Session.IsDisposed && Session.SessionState == NetworkSessionState.Ended)
            {
                Session.Dispose();
            }

            if (Session != null && Session.IsDisposed)
            {
                Session = null;
            }

            if (Session != null)
            {
                Session.Update();
            }

            base.Update(gameTime);
        }
    }
}
