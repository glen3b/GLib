using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A class that generates particles randomly.
    /// </summary>
    public class RandomParticleGenerator : IParticleGenerator
    {
        private int _particlesToGenerate = 40;

        /// <summary>
        /// Gets or sets the number of particles to create within an update.
        /// </summary>
        public int ParticlesToGenerate
        {
            get { return _particlesToGenerate; }
            set { _particlesToGenerate = value; }
        }

        private Random _random;
        private SpriteBatch _batch;

        /// <summary>
        /// Creates a new random particle generator.
        /// </summary>
        /// <param name="textures">The possible textures to assign the particle.</param>
        /// <param name="batch">The SpriteBatch that the particles will appear on.</param>
        public RandomParticleGenerator(SpriteBatch batch, params Texture2D[] textures)
        {
            if (textures == null)
            {
                throw new ArgumentNullException("textures");
            }
            if (batch == null)
            {
                throw new ArgumentNullException("batch");
            }
            _batch = batch;
            if (textures.Length < 1)
            {
                throw new ArgumentException("The textures array must have at least one element.");
            }
            _textures = textures.ToList();
            _random = new Random();
        }

        private List<Texture2D> _textures;

        /// <summary>
        /// Gets the list of textures to generate particles with.
        /// </summary>
        public List<Texture2D> Textures
        {
            get { return _textures; }
        }

        private TimeSpan _minTTL = TimeSpan.FromMilliseconds(300);
        private TimeSpan _maxTTL = TimeSpan.FromMilliseconds(700);

        /// <summary>
        /// Gets or sets the maximum time to live for a particle.
        /// </summary>
        public TimeSpan MaximumTimeToLive
        {
            get { return _maxTTL; }
            set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentException("The maximum time to live must not be less than or equal to TimeSpan.Zero.");
                }
                if (value.Ticks > (long)Int32.MaxValue)
                {
                    //The TimeSpan is larger than supported, partly a library limitation.
                    throw new ArgumentException("The maximum time to live is larger than supported. The value of Ticks must be less than or equal to Int32.MaxValue. This is euqivalent to approximately 3.5 minutes. It is suggested that the time to live is not longer than 3 minutes.");
                }
                if (value < MinimumTimeToLive)
                {
                    throw new ArgumentException("The maximum time to live must be greater than or equal to the minimum time to live.");
                }
                _maxTTL = value;
            }
        }

        /// <summary>
        /// Gets or sets the minimum time to live for a particle.
        /// </summary>
        public TimeSpan MinimumTimeToLive
        {
            get { return _minTTL; }
            set
            {
                if (value <= TimeSpan.Zero)
                {
                    throw new ArgumentException("The minimum time to live must not be less than or equal to TimeSpan.Zero.");
                }
                if (value.Ticks > (long)Int32.MaxValue)
                {
                    //The TimeSpan is larger than supported, partly a library limitation.
                    throw new ArgumentException("The minimum time to live is larger than supported. The value of Ticks must be less than or equal to Int32.MaxValue. This is euqivalent to approximately 3.5 minutes. It is suggested that the time to live is not longer than 3 minutes.");
                }
                if (value > MaximumTimeToLive)
                {
                    throw new ArgumentException("The minimum time to live must be less than or equal to the maximum time to live.");
                }
                _minTTL = value;
            }
        }

        /// <summary>
        /// Gets or sets the random generator to use in the generation of particles.
        /// </summary>
        public Random Random
        {
            get { return _random; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Random");
                }
                _random = value;
            }
        }

        private float _minumumParticleColorChangeRate = 0.63f;

        /// <summary>
        /// Gets or sets the minimum rate of color change for a particle.
        /// </summary>
        /// <remarks>
        /// Set to 1 to diable color degeneration.
        /// Set to a value less than one to have particles fade out.
        /// Set to a value greater than one to have particles fade in to white.
        /// </remarks>
        public float MinimumParticleColorChangeRate
        {
            get { return _minumumParticleColorChangeRate; }
            set { _minumumParticleColorChangeRate = value; }
        }
        

        /// <summary>
        /// Generates a new particle at the specified position.
        /// </summary>
        /// <returns>A new particle.</returns>
        /// <param name="pos">The position of the object to create particles around.</param>
        public virtual Particle GenerateParticle(Vector2 pos)
        {
            Particle particle = new Particle(_textures[_random.Next(_textures.Count)], pos, _batch);

            particle.Speed = new Vector2((_random.NextDouble() * 2 - 1).ToFloat(), (_random.NextDouble() * 2 - 1).ToFloat());
            particle.RotationVelocity = MathHelper.ToDegrees(Convert.ToSingle(_random.NextDouble() * 2 - 1) / 10f);
            particle.Color = new Color(_random.Next(255), _random.Next(255), _random.Next(255), _random.Next(255));
            particle.Scale = new Vector2(_random.NextDouble().ToFloat());
            particle.TimeToLive = TimeSpan.FromTicks(_random.Next((int)_minTTL.Ticks, (int)_maxTTL.Ticks));

            if (_minumumParticleColorChangeRate != 1)
            {
                float particleColorDegenerationRate = _random.NextDouble().ToFloat();
                while (particleColorDegenerationRate < _minumumParticleColorChangeRate)
                {
                    particleColorDegenerationRate += Convert.ToSingle(_random.NextDouble() % 0.15);
                }
                particle.ColorChange = particleColorDegenerationRate;
            }

            particle.UseCenterAsOrigin = true;

            return particle;
        }
    }
}
