using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.InputLib
{
    /// <summary>
    /// Represents a game component responsible for updating input management classes.
    /// </summary>
    /// <remarks>
    /// This is intended to be added to the Components collection of a game to handle updating of input related classes.
    /// </remarks>
    public class InputManagerComponent : GameComponent
    {
        /// <summary>
        /// Creates a new InputManagerComponent.
        /// </summary>
        /// <param name="game">The Game associated with this component.</param>
        public InputManagerComponent(Game game)
            : base(game)
        {

        }

        /// <summary>
        /// Updates the InputManagerComponent, calling the Update method on all input related classes.
        /// </summary>
        /// <param name="gameTime">The current GameTime.</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            KeyboardManager.Update();
            Mouse.MouseManager.Update();
        }
    }
}
