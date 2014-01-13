﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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

        private uint _frameNumber = 0;

        private int _frameRatio = 1;

        /// <summary>
        /// Gets or sets the number of frames to allow to pass before each particle generation burst.
        /// </summary>
        /// <remarks>
        /// This property allows for behavior such as that generated by the following:
        /// <code>
        /// //If FramesPerGeneration frames have passed since the last time FramesPerGeneration frames passed, generate particles
        /// if(_framesPassed % FramesPerGeneration == 0){
        ///     //Generates particles, this only adds one but real implementation would account for Generator.ParticlesToGenerate
        ///     Particles.Add(Generator.GenerateParticle(Tracked.Position + PositionOffset));
        /// }
        /// </code>
        /// This means that every FramesPerGeneration frames, Generator.ParticlesToGenerate particles will be generated.
        /// For example, if FramesPerGeneration is 2 and Generator.ParticlesToGenerate is 40, every 2 frames 40 particles will be generated.
        /// By default this value is 1.
        /// </remarks>
        public int FramesPerGeneration
        {
            get { return _frameRatio; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("FramesPerGeneration must be greater than or equal to zero.");
                }
                _frameRatio = value;
            }
        }


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

        private Vector2 _positionOffset;

        /// <summary>
        /// Gets or sets the offset from the position of <see cref="Tracked"/> at which to generate the particles.
        /// </summary>
        public Vector2 PositionOffset
        {
            get { return _positionOffset; }
            set { _positionOffset = value; }
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
                if (_particles[i].IsDead)
                {
                    _generator.Pool.ReturnParticle(_particles[i]);
                    _particles.RemoveAt(i);
                    i--;
                }
            }

            if (Tracked != null && _frameNumber % _frameRatio == 0)
            {
                for (int genPart = 0; genPart < Generator.ParticlesToGenerate; genPart++)
                {
                    _particles.Add(Generator.GenerateParticle(Tracked.Position + PositionOffset));
                }
            }

            if (_frameNumber++ >= uint.MaxValue - 250)
            {
                _frameNumber %= Convert.ToUInt32(_frameRatio);
            }
        }
    }
}
