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
        /// <summary>
        /// Creates a particle with the specified texture at the specified position to be drawn on the specified <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>.
        /// </summary>
        /// <param name="texture">The texture of the particle.</param>
        /// <param name="pos">The position of the particle.</param>
        /// <param name="batch">The SpriteBatch on which to draw the particle.</param>
        public Particle(Texture2D texture, Vector2 pos, SpriteBatch batch) : base(texture, pos, batch) { }

        private float _rotationVelocity;

        private float _colorVelocity = 1;

        /// <summary>
        /// Gets or sets the amount by which to change the color every update.
        /// </summary>
        /// <remarks>
        /// Should be greater than zero.
        /// </remarks>
        public float ColorChange
        {
            get { return _colorVelocity; }
            set { _colorVelocity = value; }
        }


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

        private bool _isDead = false;

        /// <summary>
        /// Gets a boolean indicating if this particle is dead.
        /// </summary>
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            protected set
            {
                _isDead = value;
            }
        }

        private TimeToLiveSettings _ttlSettings = TimeToLiveSettings.StrictTTL;

        /// <summary>
        /// Gets or sets a value indicating how to apply the time to live setting.
        /// </summary>
        public TimeToLiveSettings TimeToLiveSettings
        {
            get { return _ttlSettings; }
            set { _ttlSettings = value; }
        }


        /// <summary>
        /// Updates the particle.
        /// </summary>
        public override void Update()
        {
            base.Update();
            Rotation += RotationVelocity;
            Color *= ColorChange;

            #region "Dead" particle condition checks
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.StrictTTL) && TimeToLive.Ticks <= 0)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess25) && Color.A <= 25)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess50) && Color.A <= 50)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess75) && Color.A <= 75)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess100) && Color.A <= 100)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess125) && Color.A <= 125)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess150) && Color.A <= 150)
            {
                IsDead = true;
            }
            if (TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess175) && Color.A <= 175)
            {
                IsDead = true;
            }
            #endregion
        }

        /// <summary>
        /// Updates the particle, changing the time to live.
        /// </summary>
        /// <param name="gt">The current GameTime.</param>
        public virtual void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            _timeToLive -= gt.ElapsedGameTime;
            Update();
        }
    }
}
