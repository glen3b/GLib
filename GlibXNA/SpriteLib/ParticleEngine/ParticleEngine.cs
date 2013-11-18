using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A class that manages multiple particles.
    /// </summary>
    /// <remarks>
    /// This class handles updating and drawing of particles.
    /// Does not manage the <see cref="SpriteBatch"/>.
    /// </remarks>
    public class ParticleEngine : ITimerSprite
    {
        private bool _isVisible = true;

        /// <summary>
        /// Gets or sets a boolean indicating whether the particles for the tracked object should be drawn.
        /// </summary>
        public bool ParticlesVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                foreach (Particle p in _particles)
                {
                    p.Visible = value;
                }
            }
        }

        /// <summary>
        /// Creates a particle engine using the specified generator.
        /// </summary>
        /// <param name="particleGenerator">The particle generator to use.</param>
        public ParticleEngine(IParticleGenerator particleGenerator)
        {
            if (particleGenerator == null)
            {
                throw new ArgumentNullException("particleGenerator");
            }
            _generator = particleGenerator;
        }

        private IPositionable _tracked;

        /// <summary>
        /// Gets or sets the object that this particle engine tracks and creates particles near.
        /// </summary>
        /// <remarks>
        /// If this is null, the particle engine will not generate new particles.
        /// This is intended to be a reference to an object.
        /// </remarks>
        public IPositionable Tracked
        {
            get { return _tracked; }
            set { _tracked = value; }
        }

        private List<Particle> _particles = new List<Particle>();

        /// <summary>
        /// Gets the collection of particles that this particle engine currently manages.
        /// </summary>
        public ICollection<Particle> Particles
        {
            get { return _particles; }
        }

        private IParticleGenerator _generator;

        /// <summary>
        /// Gets or sets the object to use for generating particles.
        /// </summary>
        public IParticleGenerator Generator
        {
            get { return _generator; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Generator");
                }
                _generator = value;
            }
        }


        /// <summary>
        /// Draws all of the particles being managed by this particle manager.
        /// </summary>
        public void Draw()
        {
            if (!_isVisible)
            {
                return;
            }

            foreach (Particle p in _particles)
            {
                p.DrawNonAuto();
            }
        }

        /// <summary>
        /// Updates all of the particles being managed by this particle manager.
        /// </summary>
        public void Update(Microsoft.Xna.Framework.GameTime gt)
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                //While: 2 in a row?
                while (_particles[i] == null)
                {
                    _particles.RemoveAt(i);
                    //No i--, just update the next one in the list
                }

                _particles[i].Update(gt);
                if (_particles[i].TimeToLive <= TimeSpan.Zero)
                {
                    _particles.RemoveAt(i);
                    i--;
                }
            }

            if (Tracked != null)
            {
                for (int genPart = 0; genPart < Generator.ParticlesToGenerate; genPart++)
                {
                    _particles.Add(Generator.GenerateParticle(Tracked.Position));
                }
            }
        }
    }
}
