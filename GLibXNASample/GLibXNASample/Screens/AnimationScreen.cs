using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glib.XNA;
using Glib.XNA.SpriteLib;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GLibXNASample.Screens
{
    public class AnimationScreen : Screen
    {
        SpriteSheet animated;

        public AnimationScreen(SpriteBatch b)
            : base(b, Color.DarkRed)
        {
            Name = "AnimatedScreen";

            //SpriteSheet: Allows for animation with a single sprite sheet
            //FrameCollection: A collection of frames
            animated = new SpriteSheet(Vector2.Zero, b,
                FrameCollection.FromSpriteSheet(GLibXNASampleGame.Instance.Content.Load<Texture2D>("TestSpritesheet"),
                new Rectangle(99, 11, 45, 44),
            new Rectangle(150, 9, 45, 46),
            new Rectangle(198, 9, 45, 45),
            new Rectangle(252, 8, 45, 44),
            new Rectangle(303, 9, 45, 46),
            new Rectangle(351, 7, 45, 46),
            new Rectangle(396, 8, 45, 46),
            new Rectangle(444, 9, 45, 46),
            new Rectangle(495, 8, 45, 45),
            new Rectangle(552, 7, 45, 45)));

            animated.Position = animated.GetCenterPosition(b.GraphicsDevice.Viewport);
            //FrameTime: The amount of time to spend on each frame
            animated.FrameTime = new TimeSpan(GLibXNASampleGame.Instance.TargetElapsedTime.Ticks * 5L);
            //FrameChanged: An event fired upon frame change
            animated.FrameChanged += new EventHandler(animated_FrameChanged);

            Sprites.Add(animated);
        }

        public override bool Visible
        {
            get
            {
                return base.Visible;
            }
            set
            {
                base.Visible = value;
                if (value)
                {
                    animated.X = -animated.Width;
                }
            }
        }

        void animated_FrameChanged(object sender, EventArgs e)
        {
            animated.X += 8;
            animated.Y = animated.GetCenterPosition(animated.SpriteBatch.GraphicsDevice.Viewport, animated.Origin).Y;
        }
    }
}
