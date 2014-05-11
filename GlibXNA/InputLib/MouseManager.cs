using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.ObjectModel;

namespace Glib.XNA.InputLib
{
#if WINDOWS
    /// <summary>
    /// Represents a manager of mouse events and data.
    /// </summary>
    public static class MouseManager
    {
        private static MouseState _currentMs;

        /// <summary>
        /// Gets the current mouse state.
        /// </summary>
        public static MouseState CurrentMouseState
        {
            get { return _currentMs; }
        }

        /// <summary>
        /// Gets an <see cref="IPositionable"/> representing the current mouse position.
        /// </summary>
        public static IPositionable MousePositionable
        {
            get
            {
                return _mousePositionable;
            }
        }

        private static Glib.XNA.SpriteLib.PositionRepresentation _mousePositionable = (Glib.XNA.SpriteLib.PositionRepresentation)Vector2.Zero.AsPositionable();

        /// <summary>
        /// Gets the last known mouse state before the current mouse state.
        /// </summary>
        public static MouseState LastMouseState { get; private set; }

        /// <summary>
        /// An event fired after the update of the MouseManager, but before the assignment of LastMouseState.
        /// </summary>
        [Obsolete("This event is no longer fired between the sets of the current and last mouse states. New update logic renders this event unneeded.")]
        public static event EventHandler Updated;

        private static Collection<ScreenRegion> _allRegions = new Collection<ScreenRegion>();

        /// <summary>
        /// Gets a collection of all known updated SreenRegion objects.
        /// </summary>
        public static Collection<ScreenRegion> AllRegions
        {
            get { return _allRegions; }
        }

        /// <summary>
        /// Update the MouseManager.
        /// </summary>
        internal static void Update()
        {
            LastMouseState = _currentMs;

            if (Updated != null)
            {
                Updated(null, EventArgs.Empty);
            }

            foreach (ScreenRegion s in _allRegions)
            {
                s.Update();
            }

            _currentMs = Microsoft.Xna.Framework.Input.Mouse.GetState();

            _mousePositionable.X = _currentMs.X;
            _mousePositionable.Y = _currentMs.Y;
        }
        
        
    }
#endif
}
