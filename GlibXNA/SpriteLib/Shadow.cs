﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A shadow behind an object.
    /// </summary>
    /// <typeparam name="TShadowed">The type of the shadowed object.</typeparam>
    public abstract class Shadow<TShadowed> : IDrawableComponent, IPositionable where TShadowed : IPositionable
    {
        /// <summary>
        /// Creates a shadow, shadowing the specified object at the specified relative position.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        /// <param name="relativePos">The position of the shadow relative to the shadowed object.</param>
        public Shadow(TShadowed shadowed, Vector2 relativePos)
        {
            if (shadowed == null)
            {
                throw new ArgumentNullException("shadowed");
            }
            ShadowedObject = shadowed;
            RelativePosition = relativePos;
        }

        /// <summary>
        /// Creates a shadow, shadowing the specified object.
        /// </summary>
        /// <param name="shadowed">The shadowed object.</param>
        public Shadow(TShadowed shadowed) : this(shadowed, Vector2.One) { }

        private Vector2 _relativePosition = Vector2.One;

        /// <summary>
        /// Gets or sets the position of the shadow relative to <see cref="ShadowedObject"/>.
        /// </summary>
        public Vector2 RelativePosition
        {
            get { return _relativePosition; }
            set { _relativePosition = value; }
        }

        private TShadowed _shadowedObject;

        /// <summary>
        /// Gets or sets the object that is shadowed.
        /// </summary>
        public TShadowed ShadowedObject
        {
            get { return _shadowedObject; }
            set
            {
                if (value == null)
                {
                    throw new NullReferenceException("The shadowed object must not be null.");
                }
                _shadowedObject = value;
            }
        }


        /// <summary>
        /// Gets or sets the absolute position of the shadow.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                if (ShadowedObject == null)
                {
                    throw new InvalidOperationException("ShadowedObject is null.");
                }
                return ShadowedObject.Position + RelativePosition;
            }
            set
            {
                if (ShadowedObject == null)
                {
                    throw new InvalidOperationException("ShadowedObject is null.");
                }
                RelativePosition = value - ShadowedObject.Position;
            }
        }

        /// <summary>
        /// Renders the shadow.
        /// </summary>
        public abstract void Draw();
    }
}
