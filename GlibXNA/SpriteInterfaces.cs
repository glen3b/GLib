using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// A basic drawable component.
    /// </summary>
    public interface IDrawableComponent
    {
        /// <summary>
        /// Draw the IDrawableComponent.
        /// </summary>
        void Draw();
    }


    /// <summary>
    /// Represents a sizable object.
    /// </summary>
    public interface ISizable
    {
        /// <summary>
        /// The width of the object.
        /// </summary>
        float Width { get; }

        /// <summary>
        /// The height of the object.
        /// </summary>
        float Height { get; }
    }

    /// <summary>
    /// Represents a sizable, positionable, screen object.
    /// </summary>
    public interface ISizedScreenObject : ISizable, IPositionable
    {
        
    }

    /// <summary>
    /// An interface representing a gravity source.
    /// </summary>
    public interface IGravitySource : IPositionable
    {
        /// <summary>
        /// Gets or sets the position of the gravity source.
        /// </summary>
        new Vector2 Position { get; set; }

        /// <summary>
        /// Gets the force of attraction for an object affected by this gravity.
        /// </summary>
        /// <param name="attracted">The object that is being attracted to this gravity source.</param>
        /// <returns>The amount to pull the object, in the X and Y axis, towards the gravity source.</returns>
        Vector2 GetAttraction(IPositionable attracted);
        
    }

    /// <summary>
    /// A basic component which has a texture.
    /// </summary>
    public interface ITexturable
    {
        /// <summary>
        /// Gets or sets the texture of the ITexturable.
        /// </summary>
        Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }
    }

    /// <summary>
    /// A basic component which has a position.
    /// </summary>
    public interface IPositionable
    {
        /// <summary>
        /// Gets or sets the position of the IPositionable.
        /// </summary>
        Vector2 Position { get; set; }
    }

    /// <summary>
    /// A sprite, supporting updates and draws.
    /// </summary>
    public interface ISprite : IDrawableComponent
    {
        /// <summary>
        /// Draw the ISprite.
        /// </summary>
        new void Draw();

        /// <summary>
        /// Update the ISprite as applicable (do logic here).
        /// </summary>
        void Update();
    }

    /// <summary>
    /// A basic sprite, supporting updates and draws with the option of toggling SpriteBatch management.
    /// </summary>
    public interface ISpriteBatchManagerSprite : ISprite
    {
        /// <summary>
        /// Draw the ISpriteBatchManagerSprite, beginning and ending the SpriteBatch automatically.
        /// </summary>
        new void Draw();

        /// <summary>
        /// Draw the ISpriteBatchManagerSprite, not beginning and ending the SpriteBatch automatically.
        /// </summary>
        void DrawNonAuto();

        /// <summary>
        /// Update the ISpriteBatchManagerSprite as applicable (do logic here).
        /// </summary>
        new void Update();
    }

    /// <summary>
    /// A basic sprite, supporting updates with a GameTime object passed in and draws.
    /// </summary>
    public interface ITimerSprite : IDrawableComponent
    {
        /// <summary>
        /// Draw the ITimerSprite.
        /// </summary>
        new void Draw();

        /// <summary>
        /// Update the ITimerSprite as applicable (do logic here), using a GameTime object.
        /// </summary>
        /// <param name="gameTime">A snapshot of game timing values.</param>
        void Update(GameTime gameTime);
    }
}
