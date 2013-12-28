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

        private TimeToLiveSettings _ttlSettings = TimeToLiveSettings.StrictTTL;

        /// <summary>
        /// Gets or sets the time to live settings to apply to particles generated by this particle generator.
        /// </summary>
        public TimeToLiveSettings TTLSettings
        {
            get { return _ttlSettings; }
            set { _ttlSettings = value; }
        }
        

        //private float _minumumParticleColorChangeRate = 0.63f;
        private float _minumumParticleColorChangeRate = 1;

        /// <summary>
        /// Gets or sets the minimum rate of color change for a particle.
        /// </summary>
        /// <remarks>
        /// Set to 1 to diable color degeneration.
        /// Set to a value less than one to have particles fade out.
        /// Disabled by default.
        /// </remarks>
        public float MinimumParticleColorChangeRate
        {
            get { return _minumumParticleColorChangeRate; }
            set {
                if (value > 1)
                {
                    throw new ArgumentException("The MinimumParticleColorChangeRate cannot be greater than one.");
                }
                _minumumParticleColorChangeRate = value; }
        }

        private float _scaleFactor = 1;

        /// <summary>
        /// Gets or sets the value by which to divide the randomly generated scale.
        /// </summary>
        /// <remarks>
        /// Defaults to one. This can be used to enlarge or shrink generated particles by a constant amount.
        /// </remarks>
        public float ScaleFactor
        {
            get { return _scaleFactor; }
            set { 
                if(value <= 0){
#if WINDOWS
                    throw new ArgumentOutOfRangeException("ScaleFactor", value, "The ScaleFactor must be greater than 0."); 
#else
                    throw new ArgumentOutOfRangeException("ScaleFactor"); 
#endif
                }
                _scaleFactor = value; }
        }

        private RandomParticleProperties _randomProperties;

        /// <summary>
        /// Gets or sets a value specifying what properties to randomly generate for new particles.
        /// </summary>
        public RandomParticleProperties RandomProperties
        {
            get { return _randomProperties; }
            set { _randomProperties = value; }
        }

        /// <summary>
        /// Gets the <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> used for the construction of particles.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get
            {
                return _batch;
            }
        }

        /// <summary>
        /// Generates a new particle at the specified position.
        /// </summary>
        /// <returns>A new particle.</returns>
        /// <param name="pos">The position of the object to create particles around.</param>
        public virtual Particle GenerateParticle(Vector2 pos)
        {
            Particle particle = new Particle(_textures[_random.Next(_textures.Count)], pos, _batch);

            particle.Speed = _randomProperties.Speed.HasValue ? _randomProperties.Speed.Value : new Vector2((_random.NextDouble() * 2 - 1).ToFloat(), (_random.NextDouble() * 2 - 1).ToFloat());
            particle.RotationVelocity = _randomProperties.RotationChange.HasValue ? _randomProperties.RotationChange.Value : MathHelper.ToDegrees(Convert.ToSingle(_random.NextDouble() * 2 - 1) / 10f);
            particle.Color = _randomProperties.Tint.HasValue ? _randomProperties.Tint.Value : new Color(_random.Next(255), _random.Next(255), _random.Next(255), _random.Next(255));
            particle.Scale = _randomProperties.Scale.HasValue ? _randomProperties.Scale.Value : new Vector2(_random.NextDouble().ToFloat() / ScaleFactor);
            particle.TimeToLive = _randomProperties.TimeToLive.HasValue ? _randomProperties.TimeToLive.Value : TimeSpan.FromTicks(_random.Next((int)_minTTL.Ticks, (int)_maxTTL.Ticks));
            particle.TimeToLiveSettings = _ttlSettings;

            if (_randomProperties.ColorFactor.HasValue)
            {
                particle.ColorChange = _randomProperties.ColorFactor.Value;
            }
            else if (_minumumParticleColorChangeRate != 1)
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
