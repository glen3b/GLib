using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Glib.XNA.SpriteLib.ParticleEngine;
using Glib.XNA.InputLib;
using Glib.XNA;
using Glib;

namespace GLibXNASample.Screens
{
    /// <summary>
    /// This is a class extending <see cref="Screen"/>, it is the main menu.
    /// </summary>
    public class MainMenu : Screen
    {
        ParticleEngine mouseParticleGen;
        Sprite mouseCursor;
        TextSprite title;
        TextSprite desc;
        ProgressBar progressBar;

        Dictionary<String, String> buttons = new Dictionary<string, string>();

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
            //Star image courtesy of Wikimedia user Felipe Micaroni Lalli
            //Image obtained from http://commons.wikimedia.org/wiki/File:Star_Ouro.svg
            mouseCursor = new Sprite(GLibXNASampleGame.Instance.Content.Load<Texture2D>("Star"), Vector2.Zero, sb);
            mouseCursor.Scale = new Vector2(.1f);
            mouseCursor.UseCenterAsOrigin = true;

            //TextSprite: Displays text
            //Can be used for titles, descriptions, etc
            title = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Title"), "GlenLibrary XNA");
            //Extension method: GetCenterPosition: Returns the position required to center the object on the specified viewport
            title.Position = new Vector2(title.GetCenterPosition(sb.GraphicsDevice.Viewport).X, 15);
            //Fancy color constructor: Uses a Vector4 with RGBA values expressed as floats from 0 to 1, basically dark gray shadow with 128 alpha
            title.Shadow = new TextShadow(title, new Vector2(-2, 2), new Color(new Vector4(Color.DarkGray.ToVector3(), 0.5f)));

            AdditionalSprites.Add(title);


            desc = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("Subtitle"), "A general purpose XNA library", Color.Chocolate);
            desc.Color *= 0.75f;
            desc.Color.A = 255;
            desc.X = desc.GetCenterPosition(Graphics.Viewport).X;
            desc.Y = title.Y + title.Height + 5;

            AdditionalSprites.Add(desc);

            #region Generation of buttons systematically
            buttons.Add("Video Player", "VideoPlayer");
            buttons.Add("Multiplayer", "MultiPlayer");

            float yCoord = desc.Y + desc.Height + 10;

            foreach (var element in buttons)
            {
                Sprite buttonSprite = new Sprite(GLibXNASampleGame.Instance.TextureCreator.CreateSquare(1, Color.Red), Vector2.Zero, sb);

                TextSprite button = new TextSprite(sb, GLibXNASampleGame.Instance.Content.Load<SpriteFont>("MenuItem"), element.Key);
                //Allows hovering (and therefore clicking) on a sprite
                button.IsHoverable = true;
                //These are the colors that are displayed at the various hovering states
                button.HoverColor = Color.DarkCyan;
                button.NonHoverColor = Color.Black;
                //"Pressed" event handler lambda expression
                button.Pressed += (src, args) => GLibXNASampleGame.Instance.SetScreen(element.Value);
                //Setting width and height on a Sprite scales it
                buttonSprite.Width = button.Width + 6;
                buttonSprite.Height = button.Height + 4;
                buttonSprite.Position = new Vector2(buttonSprite.GetCenterPosition(sb.GraphicsDevice.Viewport).X, yCoord);
                //ParentSprite: Allows for a "button" behind a clickable TextSprite (or not clickable), all collision and position logic done with this sprite
                button.ParentSprite = buttonSprite;

                button.Scale.X = button.Scale.X.Round();
                button.Scale.Y = button.Scale.Y.Round();
                button.X = button.X.Round();
                button.Y = button.Y.Round();

                Sprites.Add(buttonSprite);
                AdditionalSprites.Add(button);

                yCoord += buttonSprite.Height;
                yCoord += 7.5f;
                yCoord = yCoord.Round();
            }
            #endregion

            //ProgressBar: A dynamically generated progress bar, could be used to represent asset loading progress
            //Here it is just used for effects :)
            progressBar = new ProgressBar(Vector2.Zero, _filledColors[0], _emptyColors[0], sb);
            //HeightScale: The progress bar shall be 5 high
            progressBar.HeightScale = 5;
            progressBar.Denominator = Graphics.Viewport.Width - 20;
            progressBar.Value = 0;
            progressBar.X = progressBar.GetCenterPosition(sb.GraphicsDevice.Viewport).X;
            progressBar.Y = Graphics.Viewport.Height - progressBar.Height - 5;
            Sprites.Add(progressBar);

            //Random Particle Generator: A particle generator that uses a Random instance to set properties of the generated particles
            RandomParticleGenerator particlegen = new RandomParticleGenerator(sb, GLibXNASampleGame.Instance.Content.Load<Texture2D>("Star"));
            particlegen.MinimumParticleColorChangeRate = 0.925f;
            particlegen.MinimumTimeToLive = TimeSpan.FromMilliseconds(400);
            particlegen.MaximumTimeToLive = TimeSpan.FromMilliseconds(780);
            particlegen.ParticlesToGenerate = 1;
            particlegen.ScaleFactor = 15;
            //TimeToLiveSettings: When to make a particle expire, binary flaggable enumerator
            particlegen.TTLSettings = TimeToLiveSettings.AlphaLess100 | TimeToLiveSettings.StrictTTL;

            //Particle engine: Generates particles using the specified particle generator
            mouseParticleGen = new ParticleEngine(particlegen);
            mouseParticleGen.FramesPerGeneration = 3;
            mouseParticleGen.Tracked = mouseCursor;

            AdditionalSprites.Add(mouseParticleGen);
            AdditionalSprites.Add(mouseCursor);

        }

        #region Progress bar - variables for demo
        private int _cycleNumber = 0;
        private Color[] _filledColors = new Color[] { Color.DarkGreen, Color.DarkBlue, Color.DarkMagenta };
        private Color[] _emptyColors = new Color[] { Color.LightSlateGray, Color.MediumAquamarine, Color.Magenta };
        #endregion

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
            progressBar.Value = progressBar.Value >= progressBar.Denominator ? progressBar.Denominator : progressBar.Value + 1;
            if (progressBar.Value >= progressBar.Denominator)
            {
                _cycleNumber++;
                progressBar.FillColor = _filledColors[_cycleNumber % _filledColors.Length];
                progressBar.EmptyColor = _emptyColors[_cycleNumber % _emptyColors.Length];
                progressBar.Value = 0;
            }
        }
    }
}
