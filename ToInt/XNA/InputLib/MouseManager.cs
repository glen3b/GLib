using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Glib.XNA.InputLib
{
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
        /// Gets the last known mouse state before the current mouse state.
        /// </summary>
        public static MouseState LastMouseState { get; private set; }

        private static List<ScreenRegion> _allRegions = new List<ScreenRegion>();

        /// <summary>
        /// Gets a list of all known updated SreenRegion objects.
        /// </summary>
        public static List<ScreenRegion> AllRegions
        {
            get { return _allRegions; }
        }

        /// <summary>
        /// Update the MouseManager.
        /// </summary>
        internal static void Update()
        {
            _currentMs = Microsoft.Xna.Framework.Input.Mouse.GetState();

            foreach (ScreenRegion s in _allRegions)
            {
                s.Update();
            }

            LastMouseState = _currentMs;
        }
        
        
    }
}
