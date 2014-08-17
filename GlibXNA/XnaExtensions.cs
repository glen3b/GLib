using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.GamerServices;

namespace Glib.XNA
{
    /// <summary>
    /// Extensions onto XNA objects.
    /// </summary>
    public static class XnaExtensions
    {
        private static bool? _gamerServicesAvailable = null;

        /// <summary>
        /// Gets a boolean indicating whether or not the GamerServices guide is visible.
        /// </summary>
        /// <remarks>
        /// This property will return false in the case of lack of availability of gamer services.
        /// </remarks>
        public static bool IsGuideVisible
        {
            get
            {
                if (_gamerServicesAvailable.HasValue)
                {
                    return _gamerServicesAvailable.Value && Guide.IsVisible;
                }
                else
                {
                    try
                    {
                        bool guideVisible = Guide.IsVisible;
                        _gamerServicesAvailable = true;
                        return guideVisible;
                    }
                    catch (InvalidOperationException)
                    {
                        _gamerServicesAvailable = false;
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Get the average size per character for this SpriteFont.
        /// </summary>
        /// <param name="font">The font to measure the character size of.</param>
        /// <param name="measurer">The string to use for determining the average size of one character.</param>
        /// <returns>The average character size for this SpriteFont.</returns>
        public static Vector2 GetCharSize(this SpriteFont font, string measurer)
        {
            if (font == null) throw new ArgumentNullException("font");
            return new Vector2(font.MeasureString(measurer).X / measurer.Length, font.MeasureString(measurer).Y / measurer.Length);
        }

        /// <summary>
        /// Get the average size per character for this SpriteFont.
        /// </summary>
        /// <param name="font">The font to measure the character size of.</param>
        /// <returns>The average character size for this SpriteFont.</returns>
        public static Vector2 GetCharSize(this SpriteFont font)
        {
            return font.GetCharSize("AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789!@#$%^&*()-_=+[]{}\|/?,.<>");
        }

        /// <summary>
        /// Converts the specified vector to a positionable object.
        /// </summary>
        /// <param name="pos">The vector to convert to a positionable.</param>
        /// <returns>A positionable object with the position of the Vector2.</returns>
        public static IPositionable AsPositionable(this Vector2 pos)
        {
            return new Glib.XNA.SpriteLib.PositionRepresentation(pos);
        }

        /// <summary>
        /// Get the position which would be required to center the specified sizable object to the specified Viewport, assuming is has an origin of (0, 0).
        /// </summary>
        /// <param name="obj">The sizable object to center.</param>
        /// <param name="centerTo">The viewport to center the ISizable to.</param>
        /// <returns>The position which would be required to center the specified sizable object.</returns>
        public static Vector2 GetCenterPosition(this ISizable obj, Viewport centerTo)
        {
            return obj.GetCenterPosition(centerTo, (obj is IOriginPositionable ? (obj as IOriginPositionable).Origin : Vector2.Zero));
        }

        /// <summary>
        /// Calculates the inverse of the axes of the specified vector (where new.X = orig.Y and new.Y = orig.X).
        /// </summary>
        /// <param name="vector">The vector to invert.</param>
        /// <returns>The inverse of the specified vector.</returns>
        public static Vector2 Inverse(this Vector2 vector)
        {
            return new Vector2(vector.Y, vector.X);
        }

        /// <summary>
        /// Get the position which would be required to center the specified sizable object to the specified Viewport.
        /// </summary>
        /// <param name="obj">The sizable object to center.</param>
        /// <param name="centerToViewport">The viewport to center the ISizable to.</param>
        /// <param name="origin">The origin of the ISizable, unscaled.</param>
        /// <returns>The position which would be required to center the specified sizable object.</returns>
        public static Vector2 GetCenterPosition(this ISizable obj, Viewport centerToViewport, Vector2 origin)
        {
            Vector2 centerOfViewport = new Vector2(centerToViewport.Width / 2, centerToViewport.Height / 2);

            centerOfViewport.X -= (obj.Width / 2f) - origin.X * (obj is IScaled ? (obj as IScaled).Scale.X : 1);
            centerOfViewport.Y -= (obj.Height / 2f) - origin.Y * (obj is IScaled ? (obj as IScaled).Scale.Y : 1);


            return centerOfViewport;
        }

        /// <summary>
        /// Convert a vector to a rotation angle in radians.
        /// </summary>
        /// <param name="vector">Vector2 to translate to rotation angle.</param>
        /// <param name="initialOffsetAngle">Rotation angle, in radians, by which the texture is offset. A rotation of zero means up.</param>
        /// <returns>Rotation angle, in radians.</returns>
        public static float ToAngle(this Vector2 vector, float initialOffsetAngle)
        {
            return Math.Atan2(vector.X, -vector.Y).ToFloat() + initialOffsetAngle;
        }

        /// <summary>
        /// Convert a vector to a rotation angle in radians, assuming the texture has an offset of 0.
        /// </summary>
        /// <param name="vector">Vector2 to translate to rotation angle.</param>
        /// <returns>Rotation angle, in radians.</returns>
        public static float ToAngle(this Vector2 vector)
        {
            return vector.ToAngle(0);
        }

        /// <summary>
        /// Convert an angle in radians to a Vector2.
        /// </summary>
        /// <param name="angle">The angle to translate to a vector.</param>
        /// <returns>A vector representing a rotation angle in radians.</returns>
        public static Vector2 AngleToVector(this float angle)
        {
            return AngleToVector(Convert.ToDouble(angle));
        }

        /// <summary>
        /// Draw the specified Sprite to this SpriteBatch.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw to.</param>
        /// <param name="sprToDraw">The sprite to draw.</param>
        public static void Draw(this SpriteBatch sb, Glib.XNA.SpriteLib.Sprite sprToDraw)
        {
            if (sprToDraw.Visible)
            {
                sb.Draw(sprToDraw.Texture, sprToDraw.Position, sprToDraw.DrawRegion, sprToDraw.TintColor, sprToDraw.Rotation.Radians, sprToDraw.Origin, sprToDraw.Scale, sprToDraw.Effect, sprToDraw.LayerDepth);
            }
        }

        /// <summary>
        /// Convert an angle in radians to a Vector2.
        /// </summary>
        /// <param name="angle">The angle to translate to a vector.</param>
        /// <returns>A vector representing a rotation angle in radians.</returns>
        public static Vector2 AngleToVector(this double angle)
        {
            return new Vector2(Math.Sin(angle).ToFloat(), -(Math.Cos(angle).ToFloat()));
        }

        /// <summary>
        /// Get the distance between two points.
        /// </summary>
        /// <param name="startPoint">The starting point.</param>
        /// <param name="endpoint">The ending point.</param>
        /// <returns>The distance between these two Vector2 positions.</returns>
        public static double GetDistance(this Vector2 startPoint, Vector2 endpoint)
        {
            double sideA = Math.Abs(startPoint.X - endpoint.X);
            double sideB = Math.Abs(startPoint.Y - endpoint.Y);

            double hypotenuse = Math.Sqrt(sideA.RaiseToPower(2) + sideB.RaiseToPower(2));

            return hypotenuse;
        }

        /// <summary>
        /// Get the hypotenuse from the right triangle represented by this vector (side A = X, side B = Y).
        /// </summary>
        /// <param name="triangle">The known triangle sides represented as a Vector2 (must be a right triangle).</param>
        /// <returns>The length of the triangle's hypotenuse. Inaccurate if it is not a right triangle.</returns>
        public static float GetHypotenuse(this Vector2 triangle)
        {
            double hypotenuse = Math.Sqrt(triangle.X.RaiseToPower(2) + triangle.X.RaiseToPower(2));

            return hypotenuse.ToFloat();
        }

        /// <summary>
        /// Determines whether or not the specified triangle is a right triangle (where X is side A, Y is side B, and Z is the hypoentuse).
        /// </summary>
        /// <param name="triangle">The triangle to use when determining if this is a right triangle.</param>
        /// <returns>Whether or not the specified triangle is a right triangle.</returns>
        public static bool IsRightTriangle(this Vector3 triangle)
        {
            return triangle.X.RaiseToPower(2) + triangle.Y.RaiseToPower(2) == triangle.Z.RaiseToPower(2);
        }

        /// <summary>
        /// Generates a new <see cref="Vector2"/> within the specified range.
        /// </summary>
        /// <param name="rand">The random generator to use for the creation of the variables.</param>
        /// <param name="minimumPos">The inclusive minimum position of the random vector.</param>
        /// <param name="maximumPos">The exclusive maximum position of the random vector.</param>
        /// <returns>A random <see cref="Vector2"/> within the specified bounds.</returns>
        public static Vector2 NextVector2(this Random rand, Vector2 minimumPos, Vector2 maximumPos)
        {
            if (rand == null)
            {
                throw new ArgumentNullException("rand");
            }

            if (minimumPos.Equals(maximumPos))
            {
                return minimumPos;
            }

            if (minimumPos.X > maximumPos.X || minimumPos.Y > maximumPos.Y)
            {
                throw new ArgumentException("The minimum position must be less than the maximum position.");
            }

            Double minX = Math.Ceiling(minimumPos.X);
            Double minY = Math.Ceiling(minimumPos.Y);

            Double maxX = Math.Floor(maximumPos.X);
            Double maxY = Math.Floor(maximumPos.Y);

            Vector2 result = Vector2.Zero;

            if (!minX.Equals(maxX))
            {
                double tempX = rand.Next(Convert.ToInt32(minX), Convert.ToInt32(maxX));
                double addition = rand.NextDouble();
                if (tempX + addition < maxX)
                {
                    tempX += addition;
                }
                result.X = Convert.ToSingle(tempX);
            }
            else
            {
                result.X = minimumPos.X;
            }

            if (!minY.Equals(maxY))
            {
                double tempY = rand.Next(Convert.ToInt32(minY), Convert.ToInt32(maxY));
                double addition = rand.NextDouble();
                if (tempY + addition < maxY)
                {
                    tempY += addition;
                }
                result.Y = Convert.ToSingle(tempY);
            }
            else
            {
                result.Y = minimumPos.Y;
            }

            return result;
        }
    }
}
