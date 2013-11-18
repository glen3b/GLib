using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A basic particle for the particle engine.
    /// </summary>
    public class Particle : Sprite, ITimerSprite
    {
        public Particle(Texture2D texture, Vector2 pos, SpriteBatch batch) : base(texture, pos, batch) { }

        private float _rotationVelocity;

        /// <summary>
        /// Gets or sets the rotation, in degrees, by which the rotation will change each update.
        /// </summary>
        public float RotationVelocity
        {
            get { return _rotationVelocity; }
            set { _rotationVelocity = value; }
        }

        private TimeSpan _timeToLive;

        /// <summary>
        /// Gets or sets the remaining time to live.
        /// </summary>
        public TimeSpan TimeToLive
        {
            get { return _timeToLive; }
            set { _timeToLive = value; }
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        public override void Update()
        {
            base.Update();
            Rotation += RotationVelocity;
        }

        /// <summary>
        /// Updates the particle, changing the time to live.
        /// </summary>
        /// <param name="gt">The current GameTime.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            Update();
            _timeToLive -= gt.ElapsedGameTime;
            
        }
    }
}
