using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// Represents an interface for a class that can generate particles.
    /// </summary>
    public interface IParticleGenerator
    {
        /// <summary>
        /// Gets the number of particles to create within an update.
        /// </summary>
        int ParticlesToGenerate { get; }

        /// <summary>
        /// Generates a new particle at the specified position.
        /// </summary>
        /// <returns>A new particle.</returns>
        /// <param name="pos">The position of the object to create particles around.</param>
        Particle GenerateParticle(Vector2 pos);
    }
}
