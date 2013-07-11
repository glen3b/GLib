using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Glib.XNA.SpriteLib;
using System.IO;
using System.Windows.Forms;
using Glib.XNA.InputLib;

namespace SpritePreviewer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Not a good name
        SpriteManager arrows;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent(ref System.Windows.Forms.Form winform)
        {
            this.tintColorDialog = new System.Windows.Forms.ColorDialog();
            this.tintChangeButton = new System.Windows.Forms.Button();
            winform.SuspendLayout();
            // 
            // tintChangeButton
            // 
            this.tintChangeButton.Location = new System.Drawing.Point(13, 63);
            this.tintChangeButton.Name = "tintChangeButton";
            this.tintChangeButton.Size = new System.Drawing.Size(75, 23);
            this.tintChangeButton.TabIndex = 0;
            this.tintChangeButton.Text = "Change Tint";
            this.tintChangeButton.Click += new EventHandler(tintChangeButton_Click);
            this.tintChangeButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            winform.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            winform.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            winform.ClientSize = new System.Drawing.Size(736, 496);
            winform.Controls.Add(this.tintChangeButton);
            winform.Name = "Sprite Previewer";
            winform.Text = "Sprite Previewer";
            winform.ResumeLayout(false);

        }

        void tintChangeButton_Click(object sender, EventArgs e)
        {
            if(tintColorDialog.ShowDialog(((Button)sender).FindForm()) != DialogResult.Cancel){
                System.Drawing.Color selected = tintColorDialog.Color;
                Microsoft.Xna.Framework.Color tint = new Color(selected.R, selected.G, selected.B, 255);
                arrows[0].Color = tint;
            }

        }

        private System.Windows.Forms.ColorDialog tintColorDialog;
        private System.Windows.Forms.Button tintChangeButton;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Components.Add(new InputManagerComponent(this));

            base.Initialize();
        }

        TimeSpan keyPressLimiter = new TimeSpan();

        bool isSelecting = false;

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            arrows = new SpriteManager(spriteBatch);
            ts = new TextSprite(spriteBatch, new Vector2(12), Content.Load<SpriteFont>("ScaleFont"), "Current Scale: 1\r\n(Use arrow keys to adjust)", Color.Black);
            arrows.AddNewSprite(new Vector2(75), Content.Load<Texture2D>("arrowincircle"));
            arrows[0].Updated += new EventHandler(arrow_Updated);
            arrows[0].Rotation = new SpriteRotation(90);
            arrows[0].UseCenterAsOrigin = true;
            //arrows[0].Center = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            //Enable drag-n-drop support

            System.Windows.Forms.Form underlyingForm = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(Window.Handle);
            underlyingForm.AllowDrop = true;
            underlyingForm.DragEnter += new System.Windows.Forms.DragEventHandler(underlyingForm_DragEnter);
            underlyingForm.DragDrop += new System.Windows.Forms.DragEventHandler(underlyingForm_DragDrop);
            //base.Initialize();
            InitializeComponent(ref underlyingForm);
            IsMouseVisible = true;
        }

        TextSprite ts;

        private decimal _scale = 1;

        decimal scale{
            get
            {
                return _scale;
            }
            set
            {
                if (value <= 25 && value >= 0.1m)
                {
                    _scale = value;
                    ts.Text = "Current Scale: " + scale;
                    arrows[0].Scale = new Vector2(Convert.ToSingle(scale));
                }
                else
                {
                    if (value > 25)
                    {
                        _scale = 25;
                    }
                    else if (value < 0.1m)
                    {
                        _scale = 0.1m;
                    }
                }
            }
        }

        void underlyingForm_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                string fileName = ((string[])e.Data.GetData(System.Windows.Forms.DataFormats.FileDrop))[0];

                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    try
                    {
                        Texture2D droppedImage = Texture2D.FromStream(GraphicsDevice, stream);
                        Color tint = arrows[0].Color;
                        Vector2 scale = new Vector2(Convert.ToSingle(this.scale));
                        //arrows[0].Texture = droppedImage;
                        isSelecting = false;
                        arrows.Sprites.RemoveRange(0, arrows.Sprites.Count);
                        arrows.AddNewSprite(new Vector2(25), droppedImage);
                        arrows[0].Updated += new EventHandler(arrow_Updated);
                        arrows[0].UseCenterAsOrigin = true;
                        arrows[0].Scale = scale;
                        arrows[0].Color = tint;
                        if (MessageBox.Show("Is this a sprite sheet?", "Sprite Sheet?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {

                            relativeSelectStart = null;
                            isSelecting = true;
                        }
                    }
                    catch
                    {
                        //Not a valid image - ignore...
                    }
                }
            }
        }

        void underlyingForm_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.FileDrop))
            {
                e.Effect = System.Windows.Forms.DragDropEffects.Copy;
            }
        }

        Vector2? relativeSelectStart = null;
        MouseState? old = null;
        //SpriteSheet drawnSpriteSheetAnimate = null;

        void arrow_Updated(object sender, EventArgs e)
        {
            MouseState current = Mouse.GetState();
            if (!old.HasValue)
            {
                old = Mouse.GetState();
            }
            if (!isSelecting)
            {
                //((Sprite)sender).Rotation.Degrees += 0.5f;
                ((Sprite)sender).FollowMouse();
            }
            else
            {
                Sprite active = ((Sprite)sender);
                MouseState ms = Mouse.GetState();
                active.Rotation = new SpriteRotation();
                active.Position = new Vector2(25);
                active.UseCenterAsOrigin = false;
                Vector2 mousePos = new Vector2(ms.X, ms.Y);
                if (active.ClickCheck(ms) && !relativeSelectStart.HasValue)
                {
                    relativeSelectStart = mousePos;
                    arrows.AddNewSprite(relativeSelectStart.Value, Content.Load<Texture2D>("dot"));
                }
                if (active.Intersects(mousePos) && relativeSelectStart.HasValue && old.Value.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && current.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                {
                    Texture2D use = arrows[0].Texture;
                    arrows.Sprites.RemoveRange(0, arrows.Sprites.Count);
                    arrows.Add(new SpriteSheet(use, new Rectangle(0, 0, Convert.ToInt32(mousePos.X - relativeSelectStart.Value.X), Convert.ToInt32(mousePos.Y - relativeSelectStart.Value.Y)), new Vector2(25), spriteBatch));
                    arrows[0].Updated += new EventHandler(arrow_Updated);
                    ((SpriteSheet)arrows[0]).IsAnimated = true;
                    arrows[0].UseCenterAsOrigin = true;
                    isSelecting = false;
                }
            }
            old = Mouse.GetState();
        }

        private bool allNonSpriteSheet(Sprite spr)
        {
            return spr.GetType() != typeof(SpriteSheet);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            keyPressLimiter += gameTime.ElapsedGameTime;
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                this.Exit();

            arrows.Update(gameTime);

            KeyboardState current = KeyboardManager.State;

            if (current.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up) && keyPressLimiter.TotalMilliseconds >= 75)
            {
                scale += current.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) || current.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift) ? 0.5m : 0.05m;
                keyPressLimiter = new TimeSpan();
            }
            if (current.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down) && keyPressLimiter.TotalMilliseconds >= 75)
            {
                scale -= current.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) ? 0.5m : 0.05m;
                keyPressLimiter = new TimeSpan();
            }

            // TODO: Add your update logic here
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            arrows.DrawNonAuto();
            ts.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
