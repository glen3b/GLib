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
        /// <param name="measurementString">The string to use for determining the average size of one character.</param>
        /// <returns>The average character size for this SpriteFont.</returns>
        public static Vector2 GetCharSize(this SpriteFont font, string measurementString = "~1234567890~AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz~1234567890~")
        {
            return new Vector2(font.MeasureString(measurementString).X / measurementString.Length, font.MeasureString(measurementString).Y / measurementString.Length);
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
        /// Get the position which would be required to center the specified sizable object to the specified Viewport.
        /// </summary>
        /// <param name="obj">The sizable object to center.</param>
        /// <param name="centerToViewport">The viewport to center the ISizable to.</param>
        /// <param name="centerIsOrigin">Whether or not the center of the specified ISizable is the origin.</param>
        /// <returns>The position which would be required to center the specified sizable object.</returns>
        public static Vector2 GetCenterPosition(this ISizable obj, Viewport centerToViewport, bool centerIsOrigin = false)
        {
            Vector2 centerOfViewport = new Vector2(centerToViewport.Width / 2, centerToViewport.Height / 2);
            if (!centerIsOrigin)
            {
                centerOfViewport.X -= obj.Width / 2;
                centerOfViewport.Y -= obj.Height / 2;
            }
            
            return centerOfViewport;
        }

        /// <summary>
        /// Create a 2-color Texture2D on the specified graphics device of the specified size.
        /// </summary>
        /// <param name="graphics">The GraphicsDevice to create the Texture2D on.</param>
        /// <param name="colorDetermine">The predicate to use for determining the color of every individual pixel in the texture.</param>
        /// <param name="trueColor">The color to set a pixel to when the predicate returns true.</param>
        /// <param name="falseColor">The color to set a pixel to when the predicate returns false.</param>
        /// <param name="width">The width of the texture.</param>
        /// <param name="height">The height of the texture.</param>
        /// <returns>A 2-color Texture2D of the specified size.</returns>
        [Obsolete("Please use a TextureFactory instead.")]
        public static Texture2D CreateTexture(this GraphicsDevice graphics, Predicate<Point> colorDetermine, Color trueColor, Color falseColor, int width, int height)
        {
            Texture2D returnValue = new Texture2D(graphics, width, height);
            Color[] data = new Color[width * height];
            for (int w_en = 0; w_en < width; w_en++)
            {
                for (int h_en = 0; h_en < height; h_en++)
                {
                    data[h_en * width + w_en] = colorDetermine.Invoke(new Point(w_en, h_en)) ? trueColor : falseColor;
                }
            }

            returnValue.SetData<Color>(data);

            return returnValue;
        }

        /// <summary>
        /// Convert a vector to a rotation angle in radians.
        /// </summary>
        /// <param name="vector">Vector2 to translate to rotation angle.</param>
        /// <param name="initialOffsetAngle">Rotation angle, in radians, by which the texture is offset. A rotation of zero means up.</param>
        /// <returns>Rotation angle, in radians.</returns>
        public static float ToAngle(this Vector2 vector, float initialOffsetAngle = 0.0f)
        {
            return Math.Atan2(vector.X, -vector.Y).ToFloat() + initialOffsetAngle;
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
                sb.Draw(sprToDraw.Texture, sprToDraw.Position, sprToDraw.DrawRegion, sprToDraw.Color, sprToDraw.Rotation.Radians, sprToDraw.Origin, sprToDraw.Scale, sprToDraw.Effect, 0f);
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
        /// <param name="endPoint">The ending point.</param>
        /// <returns>The distance between these two Vector2 positions.</returns>
        public static double GetDistance(this Vector2 startPoint, Vector2 endPoint)
        {
            double sideA = Math.Abs(startPoint.X - endPoint.X);
            double sideB = Math.Abs(startPoint.Y - endPoint.Y);

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
    }
}
