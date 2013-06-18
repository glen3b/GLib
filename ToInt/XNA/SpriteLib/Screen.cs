using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// Represents a RenderTarget2D which is a screen.
    /// </summary>
    public class Screen : IPositionable
    {
        private static int screenNum = 1;

        /// <summary>
        /// Gets or sets a boolean representing whether or not this Screen is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// All the Sprites to draw to this Screen.
        /// </summary>
        public SpriteManager Sprites;

        private bool _centerOrigin = false;

        /// <summary>
        /// Gets or sets a value indicating whether or not to use the center of the Screen as the origin when drawing.
        /// </summary>
        public bool CenterOrigin
        {
            get { return _centerOrigin; }
            set { _centerOrigin = value; }
        }
        

        /// <summary>
        /// Center the position of this Screen relative to the position of the specified 
        /// </summary>
        /// <param name="v"></param>
        public void CenterToViewport(Viewport v)
        {
            _centerOrigin = true;
            Position = new Vector2(v.Width / 2, v.Height / 2);
        }

        /// <summary>
        /// Any non-Sprite deriving Sprites that need to be drawn to this Screen.
        /// </summary>
        public List<ISprite> AdditionalSprites = new List<ISprite>();

        /// <summary>
        /// The color to clear this screen as.
        /// </summary>
        public Color ClearColor;

        /// <summary>
        /// Gets the width of this Screen, in pixels.
        /// </summary>
        public int Width
        {
            get
            {
                return Target.Width;
            }
        }

        /// <summary>
        /// Gets the height of this Screen, in pixels.
        /// </summary>
        public int Height
        {
            get
            {
                return Target.Height;
            }
        }

        private string _name = "Screen";

        /// <summary>
        /// Gets or sets the name of the Screen.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        

        /// <summary>
        /// The color to tint this screen as.
        /// </summary>
        public Color TintColor = Color.White;

        /// <summary>
        /// The target to render things to.
        /// </summary>
        public RenderTarget2D Target;

        /// <summary>
        /// The graphics device to draw to.
        /// </summary>
        public GraphicsDevice Graphics;

        /// <summary>
        /// Create a new screen.
        /// </summary>
        /// <param name="target">The render target to draw to.</param>
        /// <param name="color">The color to clear this Screen as before drawing.</param>
        /// <param name="allSprites">The SpriteManager containing the Sprites to draw.</param>
        public Screen(RenderTarget2D target, Color color, SpriteManager allSprites)
        {
            this.ClearColor = color;
            this.Sprites = allSprites;
            this.Target = target;
            this.Graphics = target.GraphicsDevice;
            _name += Screen.screenNum.ToString();
            Screen.screenNum++;
        }

        /// <summary>
        /// Create a new screen.
        /// </summary>
        /// <param name="sizeOfTarget">The size and position of the RenderTarget.</param>
        /// <param name="color">The color to clear this Screen as before drawing.</param>
        /// <param name="allSprites">The SpriteManager containing the Sprites to draw.</param>
        public Screen(Rectangle sizeOfTarget, Color color, SpriteManager allSprites) : this(new RenderTarget2D(allSprites.SpriteBatch.GraphicsDevice, sizeOfTarget.Width, sizeOfTarget.Height), color, allSprites)
        {
            Position = new Vector2(sizeOfTarget.X, sizeOfTarget.Y);
        }

        /// <summary>
        /// Create a new Screen with no Sprites by default.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw.</param>
        /// <param name="c">The color of the Screen.</param>
        public Screen(SpriteBatch sb, Color c) : this(new SpriteManager(sb), c)
        {

        }

        /// <summary>
        /// Create a new Screen with a background image.
        /// </summary>
        /// <param name="sb">The SpriteBatch to draw.</param>
        /// <param name="c">The color of the Screen.</param>
        /// <param name="back">The background image of the Screen.</param>
        public Screen(SpriteBatch sb, Color c, Texture2D back)
            : this(new SpriteManager(sb), c)
        {
            BackgroundSprite = new Sprite(back, Vector2.Zero, sb);
        }

        /// <summary>
        /// If set, the Sprite to use as the background.
        /// </summary>
        public Sprite BackgroundSprite = null;

        /// <summary>
        /// Create a new screen.
        /// </summary>
        /// <param name="color">The color to clear the Screen as before Sprite drawing.</param>
        /// <param name="mgr">The SpriteManager managing a SpriteBatch with a viewport to use as the Screen size.</param>
        public Screen(SpriteManager mgr, Color color)
            : this(new RenderTarget2D(mgr.SpriteBatch.GraphicsDevice, mgr.SpriteBatch.GraphicsDevice.Viewport.Width, mgr.SpriteBatch.GraphicsDevice.Viewport.Height), color, mgr)
        {
        }

        /// <summary>
        /// Gets or sets the position of the Screen relative to the display.
        /// </summary>
        public Vector2 Position
        {
            get;
            set;
        }

        /// <summary>
        /// Update all Sprites on this Screen.
        /// </summary>
        public void Update()
        {
            if (BackgroundSprite != null)
            {
                BackgroundSprite.Update();
            }
            Sprites.Update();
            foreach (ISprite spr in AdditionalSprites)
            {
                spr.Update();
            }
        }

        /// <summary>
        /// Update all Sprites on this Screen.
        /// </summary>
        /// <param name="game">The active game time.</param>
        public void Update(GameTime game)
        {
            if (BackgroundSprite != null)
            {
                if (BackgroundSprite.GetType().Implements(typeof(ITimerSprite)))
                {
                    BackgroundSprite.Cast<ITimerSprite>().Update(game);
                }
                else
                {
                    BackgroundSprite.Update();
                }
            }
            Sprites.Update(game);
            foreach (ISprite spr in AdditionalSprites)
            {
                if (spr.GetType().Implements(typeof(ITimerSprite)))
                {
                    spr.Cast<ITimerSprite>().Update(game);
                }
                else
                {
                    spr.Update();
                }
            }
        }
    }

    /// <summary>
    /// Represents a set of Screens to manage.
    /// </summary>
    public class ScreenManager : ISprite, IEnumerable<Screen>, ICollection<Screen>
    {
        /// <summary>
        /// All the Screen objects managed by this ScreenManager.
        /// </summary>
        /// <remarks>
        /// The component that is called by all implemented IEnumerable and ICollection methods.
        /// </remarks>
        private List<Screen> _allScreens = new List<Screen>();

        /// <summary>
        /// The graphics device to draw to.
        /// </summary>
        public GraphicsDevice Graphics;

        /// <summary>
        /// The SpriteBatch to draw to.
        /// </summary>
        public SpriteBatch SpriteBatch;

        /// <summary>
        /// The main background color.
        /// </summary>
        public Color Background;

        /// <summary>
        /// Create a ScreenManager.
        /// </summary>
        public ScreenManager(SpriteBatch spriteBatch, Color clearColor, params Screen[] screens)
        {
            Graphics = spriteBatch.GraphicsDevice;
            this.SpriteBatch = spriteBatch;
            this.Background = clearColor;
            _allScreens = screens.ToList();
        }

        /// <summary>
        /// Update all the Screen's sprites.
        /// </summary>
        /// <param name="updateInvisible">Whether or not to update invisible screens.</param>
        public void Update(bool updateInvisible)
        {
            Update(updateInvisible, null);
        }

        /// <summary>
        /// Update all the Screen's sprites.
        /// </summary>
        /// <param name="gt">The active GameTime.</param>
        public void Update(GameTime gt)
        {
            Update(false, gt);
        }

        /// <summary>
        /// Update all the Screen's sprites.
        /// </summary>
        /// <param name="updateInvisible">Whether or not to update invisible screens.</param>
        /// <param name="gt">The active GameTime.</param>
        public void Update(bool updateInvisible, GameTime gt)
        {
            foreach (Screen s in _allScreens)
            {
                if (s.Visible || updateInvisible)
                {
                    if (gt != null)
                    {
                        s.Update(gt);
                    }
                    else
                    {
                        s.Update();
                    }
                }
            }
        }

        /// <summary>
        /// Update all the Screen's sprites.
        /// </summary>
        public void Update()
        {
            Update(false);
        }

        /// <summary>
        /// Draws all the visible screens.
        /// </summary>
        public void Draw()
        {
            foreach (Screen s in this)
            {
                Graphics.SetRenderTarget(s.Target);
                Graphics.Clear(s.ClearColor);
                SpriteBatch.Begin();
                if (s.BackgroundSprite != null)
                {
                    s.BackgroundSprite.DrawNonAuto();
                }
                s.Sprites.DrawNonAuto();
                foreach(ISprite spr in s.AdditionalSprites){
                    if (spr.GetType().Implements(typeof(ISpriteBatchManagerSprite)))
                    {
                        spr.Cast<ISpriteBatchManagerSprite>().DrawNonAuto();
                    }
                    else
                    {
                        spr.Draw();
                    }
                }
                SpriteBatch.End();
            }
            Graphics.SetRenderTarget(null);
            Graphics.Clear(Background);
            SpriteBatch.Begin();
            foreach (Screen s in _allScreens)
            {
                if (s.Visible)
                {
                    SpriteBatch.Draw(s.Target, s.Position, null, s.TintColor, 0f, (s.CenterOrigin ? new Vector2(s.Width / 2, s.Height / 2): Vector2.Zero), Vector2.One, SpriteEffects.None, 0f);
                }
            }
            SpriteBatch.End();
        }

        /// <summary>
        /// Gets the Screen at the specified index.
        /// </summary>
        /// <param name="index">The index in the AllScreens list.</param>
        /// <returns>The screen with the specified index.</returns>
        public Screen this[int index]
        {
            get
            {
                return _allScreens[index];
            }
        }

        /// <summary>
        /// Gets the Screen with the specified name.
        /// </summary>
        /// <param name="name">The name of the screen.</param>
        /// <exception cref="InvalidOperationException">If multiple Screens with the specified name exist.</exception>
        /// <exception cref="IndexOutOfRangeException">If no Screens with the specified name exist.</exception>
        /// <returns>The screen with the specified name.</returns>
        public Screen this[string name]
        {
            get
            {
                int? index = null;
                int numFound = 0;
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name == name)
                    {
                        index = i;
                        numFound++;
                    }
                }
                if (numFound > 1)
                {
                    throw new InvalidOperationException("Requested Screen is ambiguous between the "+numFound+" Screens with the specified name.");
                }
                else if (numFound == 1)
                {
                    return this[index.Value];
                }
                throw new IndexOutOfRangeException("A Screen with the specified name was not found in this ScreenManager.");
            }
        }

        /// <summary>
        /// Add the specified Screen to this ScreenManager.
        /// </summary>
        /// <param name="item">The screen to add.</param>
        public void Add(Screen item)
        {
            _allScreens.Add(item);
        }

        /// <summary>
        /// Clear this ScreenManager of all Screens.
        /// </summary>
        public void Clear()
        {
            _allScreens.Clear();
        }

        /// <summary>
        /// Check if this ScreenManager contains the specified Screen.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether or not this ScreenManager contains item.</returns>
        public bool Contains(Screen item)
        {
            return _allScreens.Contains(item);
        }

        /// <summary>
        /// Copies the entire collection of Screens to the specified array starting at the specified index.
        /// </summary>
        /// <param name="array">The array to copy items to.</param>
        /// <param name="arrayIndex">The index to begin putting items.</param>
        public void CopyTo(Screen[] array, int arrayIndex)
        {
            _allScreens.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets the number of Screens this ScreenManager is managing.
        /// </summary>
        public int Count
        {
            get { return _allScreens.Count; }
        }

        /// <summary>
        /// Gets a value representing whether or not this collection is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Remove the specified Screen from this ScreenManager.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>Whether or not the item was removed.</returns>
        public bool Remove(Screen item)
        {
            return _allScreens.Remove(item);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the ScreenManager.
        /// </summary>
        /// <returns>An enumerator that iterates through the ScreenManager.</returns>
        public IEnumerator<Screen> GetEnumerator()
        {
            return _allScreens.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _allScreens.GetEnumerator();
        }
    }
}
