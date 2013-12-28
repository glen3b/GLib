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
using Glib.XNA;
using Glib.XNA.InputLib;
using GLibXNASample.Screens;

namespace GLibXNASample
{
    /// <summary>
    /// The official GLib XNA sample game.
    /// This game extends <see cref="ScreenGame"/>, an abstract class GLib provides which makes developing screen based games easier.
    /// <see cref="ScreenGame"/> implements the Update and Draw methods, rendering all <see cref="Screen"/> objects within <see cref="AllScreens"/>.
    /// IMPORTANT NOTE: The Initialize function provided by <see cref="ScreenGame"/> adds an <see cref="InputManagerComponent"/> to the game automatically.
    /// </summary>
    public class GLibXNASampleGame : ScreenGame
    {
        /// <summary>
        /// Gets the background color, rendered behind all <see cref="Screen"/>s.
        /// </summary>
        protected override Color BackgroundColor
        {
            get { return Color.Black; }
        }

        /// <summary>
        /// Initializes the game, invoking the superclass method, which adds an <see cref="InputManagerComponent"/> and invokes the XNA framework implementation.
        /// </summary>
        protected override void Initialize()
        {
            Instance = this;

            //IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// Loads content for the game, including loading screens (done in the superclass).
        /// </summary>
        protected override void LoadContent()
        {
            TextureCreator = new TextureFactory(GraphicsDevice);
            base.LoadContent();
        }

        /// <summary>
        /// Gets the instance of this class.
        /// </summary>
        /// <remarks>
        /// This game is not a true singleton (it is not enforced), but in all practical circumstances it will only be initialized once.
        /// This property is primarily used for loading of content from <see cref="Screen"/> deriving classes.
        /// </remarks>
        public static GLibXNASampleGame Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a <see cref="TextureFactory"/>, an object for creating simple textures at runtime.
        /// </summary>
        public TextureFactory TextureCreator
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes all <see cref="Screen"/>s for this game, and adds them to <see cref="AllScreens"/>.
        /// </summary>
        protected override void InitializeScreens()
        {
            //I have subclassed the Screen class for the various screens, such as the main menu
            //The constructor of my subclass (for each of them) performs initialization tasks

            //Special case for the main menu: I use an object initializer to set it to visible.
            AllScreens.Add(new MainMenu(SpriteBatch) { Visible = true });
        }
    }
}
