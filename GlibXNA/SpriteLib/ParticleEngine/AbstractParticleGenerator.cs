using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A class that provides a basis for generating particles with a fixed set of settings.
    /// </summary>
    public abstract class AbstractParticleGenerator : IParticleGenerator
    {
        /// <summary>
        /// Gets or sets the number of particles to create within an update.
        /// </summary>
        public int ParticlesToGenerate
        {
            get;
            set;
        }

        private Texture2D _texture;
        
        /// <summary>
        /// Gets or sets the texture to generate particles with.
        /// </summary>
        public virtual Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        private SpriteBatch _batch;

        /// <summary>
        /// Gets or sets the <see cref="SpriteBatch"/> to generate particles with.
        /// </summary>
        public SpriteBatch Batch
        {
            get { return _batch; }
            set { _batch = value; }
        }

        /// <summary>
        /// Set the desired properties on the specified generated particle.
        /// </summary>
        /// <param name="particle">The particle to set properties on.</param>
        protected abstract void SetProperties(Particle particle);

        /// <summary>
        /// Generates a new particle at the specified position.
        /// </summary>
        /// <returns>A new particle.</returns>
        /// <param name="pos">The position of the object to create particles around.</param>
        public virtual Particle GenerateParticle(Vector2 pos)
        {
            Particle retVal = new Particle(Texture, pos, Batch);
            retVal.UseCenterAsOrigin = true;
            SetProperties(retVal);

            return retVal;
        }
    }
}
