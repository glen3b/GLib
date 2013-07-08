using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// A manager of GamePad related information. This class cannot be inherited.
    /// </summary>
    public sealed class GamePadManager
    {
        #region Static Methods, properties, and access of class

        private static GamePadManager _playerOne = new GamePadManager(PlayerIndex.One);

        /// <summary>
        /// Gets the GamePadManager representing player one's controller.
        /// </summary>
        public static GamePadManager One
        {
            get { return _playerOne; }
        }

        private static GamePadManager _playerTwo = new GamePadManager(PlayerIndex.Two);

        /// <summary>
        /// Gets the GamePadManager representing player two's controller.
        /// </summary>
        public static GamePadManager Two
        {
            get { return _playerTwo; }
        }

        private static GamePadManager _playerThree = new GamePadManager(PlayerIndex.Three);

        /// <summary>
        /// Gets the GamePadManager representing player three's controller.
        /// </summary>
        public static GamePadManager Three
        {
            get { return _playerThree; }
        }

        private static GamePadManager _playerFour = new GamePadManager(PlayerIndex.Four);

        /// <summary>
        /// Gets the GamePadManager representing player four's controller.
        /// </summary>
        public static GamePadManager Four
        {
            get { return _playerFour; }
        }
        

        #endregion

        private PlayerIndex _player;

        /// <summary>
        /// Gets the player whos control is associated with this GamePadManager.
        /// </summary>
        public PlayerIndex Player
        {
            get { return _player; }
        }

        /// <summary>
        /// Update the GamePadManager.
        /// </summary>
        public void Update()
        {
            //TODO
        }

        private GamePadManager(PlayerIndex player)
        {
            _player = player;
        }
    }
}
