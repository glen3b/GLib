using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Glib.XNA
{
    /// <summary>
    /// Represents a class that generates positions randomly upon each call to <see cref="Position"/>.
    /// </summary>
    public sealed class RandomPositionGenerator : IPositionable
    {
        /// <summary>
        /// Gets the random generator used for generating random positions.
        /// </summary>
        public Random RandomGenerator { get; private set; }

        private Vector2 _minPos;

        /// <summary>
        /// Gets or sets the minimum position to generate.
        /// </summary>
        public Vector2 MinimumPosition
        {
            get { return _minPos; }
            set
            {
                if (value.X > _maxPos.X || value.Y > _maxPos.Y)
                {
                    throw new ArgumentException("The minimum position must be less than the maximum position.");
                }
                _minPos = value;
            }
        }

        private Vector2 _maxPos;

        /// <summary>
        /// Gets or sets the maximum position to generate.
        /// </summary>
        public Vector2 MaximumPosition
        {
            get { return _maxPos; }
            set
            {
                if (value.X < _minPos.X || value.Y < _minPos.Y)
                {
                    throw new ArgumentException("The maximum position must be greater than the minimum position.");
                }
                _maxPos = value;
            }
        }


        /// <summary>
        /// Creates a new <see cref="RandomPositionGenerator"/>.
        /// </summary>
        /// <param name="randGenerator">The object to use for generating random numbers.</param>
        /// <param name="minPos">The minimum position to generate.</param>
        /// <param name="maxPos">The maximum position to generate.</param>
        public RandomPositionGenerator(Random randGenerator, Vector2 minPos, Vector2 maxPos)
        {
            if (randGenerator == null)
            {
                throw new ArgumentNullException("randGenerator");
            }
            if (minPos.X > maxPos.X || minPos.Y > maxPos.Y)
            {
                throw new ArgumentException("The minimum position must be less than the maximum position.");
            }

            _minPos = minPos;
            _maxPos = maxPos;
            RandomGenerator = randGenerator;
        }

        /// <summary>
        /// Creates a new <see cref="RandomPositionGenerator"/>, initializing a new <see cref="Random"/> object.
        /// </summary>
        /// <param name="minPos">The minimum position to generate.</param>
        /// <param name="maxPos">The maximum position to generate.</param>
        public RandomPositionGenerator(Vector2 minPos, Vector2 maxPos)
            : this(new Random(), minPos, maxPos)
        {
            //Passthru
        }

        /// <summary>
        /// Creates a new <see cref="RandomPositionGenerator"/>, using the size and position of the specified <see cref="Viewport"/> to bound the generated values.
        /// </summary>
        /// <param name="boundingViewport">The <see cref="Viewport"/> to use to bound the generated positions.</param>
        public RandomPositionGenerator(Viewport boundingViewport)
            : this(new Vector2(boundingViewport.X, boundingViewport.Y), new Vector2(boundingViewport.Width + boundingViewport.X, boundingViewport.Height + boundingViewport.Y))
        {
            //Passthru
        }

        /// <summary>
        /// Gets the next random position.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return RandomGenerator.NextVector2(_minPos, _maxPos);
            }
            set
            {
                throw new InvalidOperationException("You cannot set the position of a random position generator.");
            }
        }
    }
}
