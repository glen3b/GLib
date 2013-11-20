using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA.SpriteLib.ParticleEngine
{
    /// <summary>
    /// An enum representing how to apply the time to live property of a <see cref="Particle"/>.
    /// </summary>
    /// <remarks>
    /// Using flags to combine StrictTTL with any of the AlphaLess enumerator constants will render the particle dead when the alpha drops below the specified level or when the time to live has dropped below zero, whichever comes first.
    /// AlphaLess flags render the particle dead when the alpha value of the tint color of the particle has reached that value (or below it).
    /// </remarks>
    [Flags]
    public enum TimeToLiveSettings
    {
        /// <summary>
        /// Kill the particle when its remaining time to live is less than or equal to zero.
        /// </summary>
        StrictTTL = 1,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 175 or less.
        /// </summary>
        AlphaLess175 = 2,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 150 or less.
        /// </summary>
        AlphaLess150 = 4,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 125 or less.
        /// </summary>
        AlphaLess125 = 8,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 100 or less.
        /// </summary>
        AlphaLess100 = 16,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 75 or less.
        /// </summary>
        AlphaLess75 = 32,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 50 or less.
        /// </summary>
        AlphaLess50 = 64,
        /// <summary>
        /// Kill the particle when its tint color has an alpha value of 25 or less.
        /// </summary>
        AlphaLess25 = 128
    }
}
