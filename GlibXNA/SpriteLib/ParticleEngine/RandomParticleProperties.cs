﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// A structure specifying what properties to randomly generate for a particle generated by a <see cref="RandomParticleGenerator"/>.
    /// </summary>
    public struct RandomParticleProperties
    {
        /// <summary>
        /// The speed to use for new particles, or null for a random value.
        /// </summary>
        public Vector2? Speed;

        /// <summary>
        /// The tint to use for new particles, or null for a random value.
        /// </summary>
        public Color? Tint;

        /// <summary>
        /// The scale to use for new particles, or null for a random value.
        /// </summary>
        public Vector2? Scale;

        /// <summary>
        /// The TTL to use for new particles, or null for a random value.
        /// </summary>
        public TimeSpan? TimeToLive;

        /// <summary>
        /// The color multiplier to use for new particles, or null for a random value.
        /// </summary>
        public float? ColorFactor;

        /// <summary>
        /// The rotation change (in degrees) to use for new particles, or null for a random value.
        /// </summary>
        public float? RotationChange;
    }
}