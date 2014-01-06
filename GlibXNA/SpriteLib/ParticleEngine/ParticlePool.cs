using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A class for the storage of particles.
    /// </summary>
    /// <remarks>
    /// An attempt to avoid garbage collection. This class stores and manages all particles.
    /// </remarks>
    public static class ParticlePool
    {
        private static SpriteBatch _spriteBatch;
        private static Stack<Particle> _particleStack = new Stack<Particle>(ParticlesToGenerate);

        /// <summary>
        /// Generates a particle. This method should not be called to retrieve a particle for use, use <see cref="GetParticle"/> instead.
        /// </summary>
        /// <returns>A new particle.</returns>
        private static Particle GenerateParticle()
        {
            if (_spriteBatch == null)
            {
                throw new InvalidOperationException("The particle pool has not been initialized.");
            }
            return new Particle(null, Vector2.Zero, _spriteBatch);
        }

        /// <summary>
        /// Initializes the <see cref="ParticlePool"/>, keeping existing particles.
        /// </summary>
        /// <param name="batch">The <see cref="SpriteBatch"/> to use for particle generation.</param>
        public static void InitializePool(SpriteBatch batch)
        {
            InitializePool(batch, false);
        }

        /// <summary>
        /// Initializes the <see cref="ParticlePool"/>.
        /// </summary>
        /// <param name="batch">The <see cref="SpriteBatch"/> to use for particle generation.</param>
        /// <param name="clearPool">Whether to clear the <see cref="ParticlePool"/> of existing particles before initialization.</param>
        public static void InitializePool(SpriteBatch batch, bool clearPool)
        {
            lock (_particleStack)
            {
                if (batch == null)
                {
                    throw new ArgumentNullException("batch");
                }
                if (clearPool)
                {
                    _particleStack.Clear();
                }

                _spriteBatch = batch;

                for (int i = 0; i < ParticlesToGenerate; i++)
                {
                    _particleStack.Push(GenerateParticle());
                }
            }
        }

        /// <summary>
        /// The number of particles at which <see cref="ParticlesLowGeneration"/> more particles will be generated.
        /// </summary>
        public const int ParticleCountGenerate = 25;

        /// <summary>
        /// The number of particles to generate when the particle count drops below <see cref="ParticleCountGenerate"/>.
        /// </summary>
        public const int ParticlesLowGeneration = 30;

        /// <summary>
        /// The number of particles to generate on pool initialization.
        /// </summary>
        public const int ParticlesToGenerate = 4000;

        /// <summary>
        /// Returns a "dead" particle to the pool.
        /// </summary>
        /// <param name="particle">The particle to return.</param>
        public static void ReturnParticle(Particle particle)
        {
            lock (_particleStack)
            {
                if (particle == null)
                {
                    throw new ArgumentNullException("particle");
                }
                if (_spriteBatch == null)
                {
                    throw new InvalidOperationException("The particle pool has not been initialized.");
                }
                particle.Color = Color.White;
                particle.ColorChange = 1;
                particle.DrawRegion = null;
                particle.Effect = SpriteEffects.None;
                particle.LayerDepth = 0;
                particle.OnlyDrawRegion = false;
                particle.Origin = Vector2.Zero;
                particle.Position = Vector2.Zero;
                particle.RotationVelocity = 0;
                particle.Scale = Vector2.One;
                particle.Speed = Vector2.Zero;
                particle.SpriteBatch = _spriteBatch;
                particle.SpriteManager = null;
                particle.Texture = null;
                particle.TimeToLive = TimeSpan.Zero;
                particle.TimeToLiveSettings = TimeToLiveSettings.StrictTTL;
                particle.UpdateParams = new UpdateParamaters();
                particle.UsedViewport = null;
                particle.Visible = true;

                _particleStack.Push(particle);
            }
        }

        /// <summary>
        /// Gets a particle from the pool.
        /// </summary>
        /// <param name="particleImg">The <see cref="Texture2D"/> to display on the particle.</param>
        /// <param name="particleLocation">The location of the particle.</param>
        /// <returns>A particle from the particle pool.</returns>
        public static Particle GetParticle(Texture2D particleImg, Vector2 particleLocation)
        {
            lock (_particleStack)
            {
                if (particleImg == null)
                {
                    throw new ArgumentNullException("particleImg");
                }
                if (_spriteBatch == null)
                {
                    throw new InvalidOperationException("The particle pool has not been initialized.");
                }
                if (_particleStack.Count <= 0)
                {
                    throw new InvalidOperationException("The particle pool has been depleted. Please reinitialize the pool.");
                }

                Particle toReturn = _particleStack.Pop();

                if (_particleStack.Count <= ParticleCountGenerate)
                {
                    for (int i = 0; i < ParticlesLowGeneration; i++)
                    {
                        _particleStack.Push(GenerateParticle());
                    }
                }

                return toReturn;
            }
        }
    }
}
