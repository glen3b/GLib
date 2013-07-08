using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel;
using Glib.XNA.InputLib;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// An implementation of ISprite with many features, such as updated, drawn, and moved events, an easily accessible position, configurable position changes per update, center-point support, and scale support.
    /// </summary>
    public class Sprite : ISprite, ISpriteBatchManagerSprite, ITexturable, IPositionable, ISizedScreenObject, ISizable
    {
        /// <summary>
        /// Convert the specified Sprite to a rectangle.
        /// </summary>
        /// <param name="spr">The Sprite to convert to a Rectangle.</param>
        /// <returns>The Rectangle representing the area of the Sprite.</returns>
        static public implicit operator Rectangle(Sprite spr)
        {
            return spr.Rectangle;
        }

        /// <summary>
        /// Convert the specified Sprite to a screen region.
        /// </summary>
        /// <param name="spr">The Sprite to convert to a screen region.</param>
        /// <returns>The screen region representing the area of the Sprite (scale sensitive).</returns>
        static public implicit operator ScreenRegion(Sprite spr)
        {
            return new ScreenRegion(spr.Position, new Vector2(spr.Width, spr.Height));
        }

        /// <summary>
        /// Convert the specified Sprite to a texture.
        /// </summary>
        /// <param name="spr">The Sprite to convert to a Texture2D.</param>
        /// <returns>The texture of the Sprite.</returns>
        static public implicit operator Texture2D(Sprite spr)
        {
            return spr.Texture;
        }

        /// <summary>
        /// The speed of the sprite in X and Y.
        /// </summary>
        public Vector2 Speed = new Vector2(0);

        private SpriteEffects _effect = SpriteEffects.None;

        /// <summary>
        /// The effect to apply to the Sprite when drawn.
        /// </summary>
        public virtual SpriteEffects Effect
        {
            get
            {
                return _effect;
            }
            set
            {
                _effect = value;
            }
        }

        /// <summary>
        /// The speed of the sprite along the X axis.
        /// </summary>
        public virtual float XSpeed
        {
            get
            {
                return Speed.X;
            }
            set
            {
                Speed.X = value;
                UpdateParams.UpdateX = true;
            }
        }

        /// <summary>
        /// A boolean representing whether or not to only draw a selected region of the sprite.
        /// </summary>
        public virtual bool OnlyDrawRegion
        {
            get
            {
                return DrawRegion != null;
            }
            set
            {
                if (!value)
                {
                    DrawRegion = null;
                }
            }
        }

        /// <summary>
        /// The selected region of the Sprite to draw. Set to null to draw the entire Sprite.
        /// </summary>
        public Rectangle? DrawRegion = null;

        /// <summary>
        /// An EventHandler called after the successful movement of this Sprite.
        /// </summary>
        public event EventHandler Moved = null;

        /// <summary>
        /// The speed of the sprite along the Y axis.
        /// </summary>
        public virtual float YSpeed
        {
            get
            {
                return Speed.Y;
            }
            set
            {
                Speed.Y = value;
                UpdateParams.UpdateY = true;
            }
        }

        /// <summary>
        /// The SpriteManager associated with this sprite, if any.
        /// </summary>
        public SpriteManager SpriteManager = null;

        /// <summary>
        /// An event called after every update of this sprite.
        /// </summary>
        public event EventHandler Updated = null;

        /// <summary>
        /// An event called after every draw of this sprite.
        /// </summary>
        public event EventHandler Drawn = null;

        /// <summary>
        /// A cancellable event called before every change of this sprite's position.
        /// </summary>
        public event SpriteMoveEventHandler Move = null;

        /// <summary>
        /// The SpriteBatch used for drawing the sprite.
        /// </summary>
        public SpriteBatch SpriteBatch;

        /// <summary>
        /// The scale at which to render the sprite.
        /// </summary>
        public Vector2 Scale = new Vector2(1, 1);

        /// <summary>
        /// The scale-sensitive center of the sprite.
        /// Setting this property is experimental.
        /// This property will break with UseCenterAsOrigin.
        /// </summary>
        public virtual Vector2 Center
        {
            get
            {
                return new Vector2(X + (Width / 2), Y + (Height / 2));
                //return new Vector2(Rectangle.Center.X, Rectangle.Center.Y);
            }
            set
            {
                Vector2 proposition = new Vector2(value.X - Width / 2, value.Y - Height / 2);
                if (!isMoveEventCanceled(proposition))
                {
                    _pos = proposition;
                    if (Moved != null)
                    {
                        Moved(this, new EventArgs());
                    }
                }
                
            }
        }
        
        /// <summary>
        /// The current X coordinate of the sprite.
        /// </summary>
        public virtual float X
        {
            get
            {
                return Position.X;
            }
            set
            {
                if (!isMoveEventCanceled(new Vector2(value, _pos.Y)))
                {
                    _pos.X = value;
                    if (Moved != null)
                    {
                        Moved(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// The current Y coordinate of the sprite.
        /// </summary>
        public virtual float Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                if (!isMoveEventCanceled(new Vector2(_pos.X, value)))
                {
                    _pos.Y = value;
                    if (Moved != null)
                    {
                        Moved(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// A scale-sensitive width. Use Texture.Width to not account for scale.
        /// </summary>
        public virtual float Width
        {
            get
            {
                return Texture.Width * Scale.X;
            }
            set
            {
                Scale.X = value / Texture.Width;
            }
        }

        /// <summary>
        /// A scale-sensitive height. Use Texture.Height to not account for scale.
        /// </summary>
        public virtual float Height
        {
            get
            {
                return Texture.Height * Scale.Y;
            }
            set
            {
                Scale.Y = value / Texture.Height;
            }
        }

        /// <summary>
        /// The current rotation of the sprite.
        /// </summary>
        public SpriteRotation Rotation = new SpriteRotation();

        private Texture2D _texture;

        /// <summary>
        /// The texture of the sprite.
        /// </summary>
        public virtual Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        
        private Vector2 _pos;

        /// <summary>
        /// The current position of the sprite.
        /// </summary>
        public virtual Vector2 Position
        {
            get
            {
                return _pos;
            }
            set
            {
                if (!isMoveEventCanceled(value))
                {
                    _pos = value;
                    if (Moved != null)
                    {
                        Moved(this, new EventArgs());
                    }
                }
            }
        }

        /// <summary>
        /// Calls the Move event and returns whether or not it was canceled.
        /// </summary>
        /// <remarks>
        /// Calls an event; call only when neccesary.
        /// </remarks>
        /// <param name="newPos">The position to call the move event with.</param>
        /// <returns>Whethjer or not the called move event was cancelled.</returns>
        protected virtual bool isMoveEventCanceled(Vector2 newPos)
        {
            bool cancel = false;
            if (Move != null)
            {
                SpriteMoveEventArgs args = new SpriteMoveEventArgs(_pos, newPos);
                foreach (SpriteMoveEventHandler tmp in Move.GetInvocationList())
                {
                    tmp(this, args);
                    cancel = args.Cancel;
                }
                return cancel;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The color of which to tint the sprite. Defaults to white.
        /// </summary>
        public Color Color = Color.White;

        /// <summary>
        /// An event called when the mouse enters the area of the Sprite.
        /// </summary>
        public event EventHandler MouseEnter;
        /// <summary>
        /// An event called when the mouse leaves the area of the Sprite.
        /// </summary>
        public event EventHandler MouseLeave;

        /// <summary>
        /// The <see cref="UpdateParamaters">UpdateParamaters</see> used to update the sprite.
        /// </summary>
        public UpdateParamaters UpdateParams = new UpdateParamaters(true,true);

        /// <summary>
        /// Create a new Sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 pos, SpriteBatch sb)
        {
            _pos = pos;
            this.SpriteBatch = sb;
            this.Texture = texture;
        }

        /// <summary>
        /// Create a new Sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 pos, Color color, SpriteBatch sb) : this(texture, pos, sb)
        {
            this.Color = color;
        }

        /// <summary>
        /// Create a new Sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 pos, Color color, SpriteBatch sb, UpdateParamaters up) : this(texture, pos, color, sb)
        {
            this.UpdateParams = up;
        }

        /// <summary>
        /// Create a new Sprite.
        /// </summary>
        public Sprite(Texture2D texture, Vector2 pos, SpriteBatch sb, UpdateParamaters up) : this(texture, pos, sb)
        {
            this.UpdateParams = up;
        }

        /// <summary>
        /// Draws the sprite.
        /// Automatically begins the SpriteBatch before you draw the sprite and ends the SpriteBatch after you draw the sprite.
        /// </summary>
        public virtual void Draw()
        {
            try
            {
                SpriteBatch.Begin();
            }
            catch { }
            DrawNonAuto();
            SpriteBatch.End();
        }

        /// <summary>
        /// Gets an approximate Rectangle representing the area covered by this Sprite.
        /// </summary>
        public Rectangle Rectangle
        {
            get
            {
                Vector2 usedPos = Position;
                usedPos -= Origin;
                return new Rectangle(Convert.ToInt32(usedPos.X), Convert.ToInt32(usedPos.Y), Convert.ToInt32(Width), Convert.ToInt32(Height));
            }
        }

        private Vector2 _origin = Vector2.Zero;

        /// <summary>
        /// Gets or sets the origin of the Sprite.
        /// </summary>
        public virtual Vector2 Origin
        {
            get { return _origin; }
            set {
                _origin = value;
                if (value == new Vector2(Texture.Width / 2, Texture.Height / 2))
                {
                    _useCenterAsOrigin = true;
                }
            }
        }

        private bool _useCenterAsOrigin = false;

        /// <summary>
        /// Gets or sets whether or not to use the center of the Sprite as the origin.
        /// </summary>
        /// <remarks>
        /// When Origin is set, it will only affect this value if set precisely to the center of the Sprite (no scale accounting).
        /// If this is set to false, the Origin will not be changed.
        /// </remarks>
        public bool UseCenterAsOrigin
        {
            get
            {
                return _useCenterAsOrigin;
            }
            set
            {
                _useCenterAsOrigin = value;
                if (value)
                {
                    Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);
                }
            }
        }

        private MouseState _lastMouseState = new MouseState();

        /// <summary>
        /// Draws the sprite.
        /// Requires you to begin the SpriteBatch before you draw the sprite, and to end the SpriteBatch after you draw the sprite.
        /// </summary>
        public virtual void DrawNonAuto()
        {
            SpriteBatch.Draw(this);
            if (Drawn != null)
            {
                Drawn(this, new EventArgs());
            }
        }

        /// <summary>
        /// Call the drawn event after drawing of the Sprite.
        /// </summary>
        protected void CallDrawn()
        {
            Drawn(this, new EventArgs());
        }

        /// <summary>
        /// Checks whether the user is clicking on the sprite.
        /// </summary>
        /// <param name="ms">The current MouseState</param>
        /// <returns></returns>
        public bool ClickCheck(MouseState ms)
        {
            return ms.LeftButton == ButtonState.Pressed && Intersects(new Vector2(ms.X, ms.Y));
        }

        /// <summary>
        /// Checks whether the user is clicking on the sprite, using the MouseState of InputLib.Mouse.MouseManager.
        /// </summary>
        public bool ClickCheck()
        {
            return ClickCheck(MouseManager.CurrentMouseState);
        }
            

        /// <summary>
        /// Checks whether the given point intersects with the sprite.
        /// </summary>
        /// <param name="pos">The position to check</param>
        /// <returns></returns>
        public bool Intersects(Vector2 pos)
        {
            float realX = X;
            float realY = Y;
            realX -= Origin.X;
            realY -= Origin.Y;

            return pos.X <= realX + Width && pos.X >= realX && pos.Y >= realY && pos.Y <= realY + Height;
        }

        /// <summary>
        /// Checks whether the specified MouseState's pointer intersects with this sprite.
        /// </summary>
        /// <param name="ms">The MouseState to check intersection against.</param>
        /// <returns></returns>
        public bool Intersects(MouseState ms)
        {
            return Intersects(new Vector2(ms.X, ms.Y));
        }

        /// <summary>
        /// Checks whether the given rectangle intersects with this sprite.
        /// </summary>
        /// <param name="r">The rectangle to check intersection against</param>
        /// <returns></returns>
        public bool Intersects(Rectangle r)
        {
            return Rectangle.Intersects(r);
        }

        /// <summary>
        /// Checks whether the given sprte intersects with this sprite.
        /// </summary>
        /// <param name="s">The sprite to check intersection against</param>
        /// <returns></returns>
        public bool Intersects(Sprite s)
        {
            return Intersects(s.Rectangle);
        }

        /// <summary>
        /// Determines the edges which this sprite has points past, if any.
        /// </summary>
        /// <returns>An array of directions representing the edges this sprite is past. Empty if none.</returns>
        public Direction[] EdgesPast()
        {
            Viewport vp = SpriteBatch.GraphicsDevice.Viewport;
            if (UsedViewport.HasValue)
            {
                vp = UsedViewport.Value;
            }
            List<Direction> allEdges = new List<Direction>();
            float realX = X;
            float realY = Y;
            realX -= Origin.X;
            realY -= Origin.Y;
            if (realX < 0)
            {
                allEdges.Add(Direction.Left);
            }
            if (realY < 0)
            {
                allEdges.Add(Direction.Top);
            }
            if (realX + Width > vp.Width)
            {
                allEdges.Add(Direction.Right);
            }
            if (realY + Height > vp.Height)
            {
                allEdges.Add(Direction.Bottom);
            }
            return allEdges.ToArray();
        }

        /// <summary>
        /// If not null, the viewport to use in viewport-requiring operation.
        /// </summary>
        public Viewport? UsedViewport = null;

        /// <summary>
        /// Remove this Sprite from it's associated SpriteManager.
        /// </summary>
        /// <exception cref="System.NullReferenceException">If there is no associated SpriteManager (the SpriteManager property is null).</exception>
        public void RemoveFromManager()
        {
            SpriteManager.RemoveSelf(this);
        }


        /// <summary>
        /// Follow the mouse pointer.
        /// </summary>
        /// <remarks>
        /// Uses the InputLib.Mouse.MouseManager.CurrentMouseState.
        /// </remarks>
        /// <param name="initialRotation">The offset rotation to use in adjusting the Sprite's rotation towards the mouse.</param>
        /// <param name="speed">The speed of following.</param>
        public void FollowMouse(SpriteRotation initialRotation, float speed = .1f)
        {
            MouseState mouse = MouseManager.CurrentMouseState;
            Vector2 target = new Vector2(mouse.X, mouse.Y);

            Vector2 direction = target - Position;
            float acceleration = direction.Length() / 10;

            if (direction.LengthSquared() > 1)
            {
                direction.Normalize();

                direction += new Vector2(speed, speed);
                direction *= acceleration;
                Position += direction;
                Rotation.Radians = direction.ToAngle(initialRotation.Radians);
            }
        }


        /// <summary>
        /// Follow the mouse pointer.
        /// </summary>
        /// <param name="speed">The speed of following.</param>
        public void FollowMouse(float speed = .05f)
        {
            FollowMouse(new SpriteRotation(), speed);
        }

        /// <summary>
        /// Logically update this sprite. This can also be done in the Updated event.
        /// </summary>
        public virtual void Update()
        {
            if (UpdateParams.UpdateX)
            {
                X += XSpeed;
            }
            if (UpdateParams.UpdateY)
            {
                Y += YSpeed;
            }
            if (UpdateParams.FixEdgeOff)
            {
                Direction[] past = EdgesPast();
                if(past.Contains(Direction.Left) || past.Contains(Direction.Right)){
                        XSpeed *= -1;
                }if(past.Contains(Direction.Top) || past.Contains(Direction.Bottom)){
                        YSpeed *= -1;
                }
            }
            /*
            if (UpdateParams.FollowMouse)
            {
                MouseState mouse = Mouse.GetState();
                Vector2 target = new Vector2(mouse.X, mouse.Y);

                Vector2 direction = target - Position;
                float acceleration = direction.Length() / 10;

                if (direction.LengthSquared() > 1)
                {
                    direction.Normalize();

                    direction += new Vector2(UpdateParams.MouseFollowSpeed);
                    direction *= acceleration;
                    Position += direction;
                    Rotation.Radians = VectorToAngle(direction, Rotation.Radians);
                }
            }
            */
            MouseState current = MouseManager.CurrentMouseState;

            if (MouseEnter != null && Intersects(current) && !Intersects(_lastMouseState))
            {
                MouseEnter(this, new EventArgs());
            }

            if (MouseLeave != null && !Intersects(current) && Intersects(_lastMouseState))
            {
                MouseLeave(this, new EventArgs());
            }

            if (Updated != null)
            {
                Updated(this, new EventArgs());
            }


            _lastMouseState = current;
        }
    }
}
