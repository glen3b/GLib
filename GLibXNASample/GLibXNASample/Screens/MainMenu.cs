using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glib.XNA.SpriteLib.ParticleEngine;
using Glib.XNA.InputLib;

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>, it is the main menu.
    /// </summary>
    public class MainMenu : Screen
    {
        ParticleEngine mouseParticleGen;
        Sprite mouseCursor;

        /// <summary>
        /// Creates and initializes the main menu.
        /// </summary>
        /// <param name="sb">The <see cref="SpriteBatch"/> to render to.</param>
        public MainMenu(SpriteBatch sb)
            : base(sb, Color.Lime)
        {
            Name = "MainMenu";

            //Sprite: A simple bitmap image, with convenient methods and properties
            //Yet it has so many possibilities...
            //Here it is used as a mouse cursor
            mouseCursor = new Sprite(GLibXNASampleGame.Instance.Content.Load<Texture2D>("Star"), Vector2.Zero, sb);
            mouseCursor.Scale = new Vector2(.075f);
            mouseCursor.UseCenterAsOrigin = true;

            Sprites.Add(mouseCursor);

            //Random Particle Generator: A particle generator that uses a Random instance to set properties of the generated particles
            RandomParticleGenerator particlegen = new RandomParticleGenerator(sb, GLibXNASampleGame.Instance.Content.Load<Texture2D>("Star"));
            particlegen.MinimumParticleColorChangeRate = 0.975f;
            particlegen.ParticlesToGenerate = 1;
            particlegen.ScaleFactor = 15;

            //Particle engine: Generates particles using the specified particle generator
            mouseParticleGen = new ParticleEngine(particlegen);
            mouseParticleGen.FramesPerGeneration = 3;
            mouseParticleGen.Tracked = mouseCursor;

            AdditionalSprites.Add(mouseParticleGen);
        }

        /// <summary>
        /// There are 2 update methods provided by <see cref="Screen"/>, one accepting <see cref="GameTime"/>, the other not.
        /// Since this is being updated through a <see cref="ScreenGame"/>, a <see cref="GameTime"/> will be provided, so this method will be called.
        /// Override whichever method is used by your game.
        /// </summary>
        /// <param name="game">The current <see cref="GameTime"/> object, which provides a snapshot of game timing values.</param>
        public override void Update(GameTime game)
        {
            base.Update(game);

            //The mouse cursor follows the mouse
            mouseCursor.Position = MouseManager.MousePositionable.Position;
        }
    }
}
