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
    public class Particle : Sprite, ITimerSprite, IResettable
    {
        private WeakReference _originalSpriteBatch;

        /// <summary>
        /// Creates a particle with the specified texture at the specified position to be drawn on the specified <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/>.
        /// </summary>
        /// <param name="texture">The texture of the particle.</param>
        /// <param name="pos">The position of the particle.</param>
        /// <param name="batch">The SpriteBatch on which to draw the particle.</param>
        public Particle(Texture2D texture, Vector2 pos, SpriteBatch batch)
            : base(texture, pos, batch)
        {
            LayerDepth = 1;
            IsDead = false;
            _originalSpriteBatch = new WeakReference(batch, true);
        }

        private float _colorVelocity = 1;

        /// <summary>
        /// Gets or sets the amount of the color to preserve every update.
        /// </summary>
        /// <remarks>
        /// Should be greater than zero. The color is multiplied by this factor every frame, so this factor can be used for color deterioration.
        /// </remarks>
        public float ColorChange
        {
            get { return _colorVelocity; }
            set
            {
                if (value <= 0)
                {
#if WINDOWS
                    throw new ArgumentOutOfRangeException("ColorChange", value, "The ColorChange property must be greater than zero.");
#else
                    throw new ArgumentOutOfRangeException("ColorChange");
#endif
                }
                _colorVelocity = value;
            }
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
            TintColor *= ColorChange;

            #region "Dead" particle condition checks
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.StrictTTL) && TimeToLive.Ticks <= 0)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess25) && TintColor.A <= 25)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess50) && TintColor.A <= 50)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess75) && TintColor.A <= 75)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess100) && TintColor.A <= 100)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess125) && TintColor.A <= 125)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess150) && TintColor.A <= 150)
            {
                IsDead = true;
            }
            if (this.TimeToLiveSettings.HasFlag(TimeToLiveSettings.AlphaLess175) && TintColor.A <= 175)
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

        /// <summary>
        /// Resets the particle to its non-dead state. This restores default properties.
        /// </summary>
        public virtual void Reset()
        {
            if (!_originalSpriteBatch.IsAlive)
            {
                throw new InvalidOperationException("The original SpriteBatch has been disposed.");
            }

            TintColor = Color.White;
            ColorChange = 1;
            DrawRegion = null;
            Effect = SpriteEffects.None;
            LayerDepth = 0;
            OnlyDrawRegion = false;
            Origin = Vector2.Zero;
            Position = Vector2.Zero;
            AngularVelocity = SpriteRotation.Zero;
            Scale = Vector2.One;
            Velocity = Vector2.Zero;
            SpriteBatch = (SpriteBatch)_originalSpriteBatch.Target;
            Texture = null;
            TimeToLive = TimeSpan.Zero;
            TimeToLiveSettings = TimeToLiveSettings.StrictTTL;
            Visible = true;
            IsDead = false;
        }
    }
}
