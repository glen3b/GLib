using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Glib.XNA
{
    /// <summary>
    /// A timer firing events at set intervals during the game.
    /// </summary>
    public class GameTimer
    {
        private bool _isRepeating;

        private TimeSpan _time;
        private TimeSpan _elapsedTime = new TimeSpan();


        /// <summary>
        /// Gets or sets the interval of time to fire events from.
        /// </summary>
        public TimeSpan Time
        {
            get { return _time; }
            set { _time = value; }
        }

        /// <summary>
        /// Create a new GameTimer with the specified interval.
        /// </summary>
        /// <param name="timePassing">The interval between calls to Elapsed.</param>
        public GameTimer(TimeSpan timePassing)
        {
            _time = timePassing;
        }

        /// <summary>
        /// An event called after every elapse of the time specified by this GameTimer.
        /// </summary>
        public event EventHandler Elapsed;

        private bool _callEvent = true;

        /// <summary>
        /// Update the GameTimer, calling the Elapsed event if neccesary.
        /// </summary>
        /// <param name="gt">The current GameTime.</param>
        public void Update(GameTime gt)
        {
            _elapsedTime += gt.ElapsedGameTime;
            if (_elapsedTime >= _time)
            {
                if (Elapsed != null && _callEvent)
                {
                    Elapsed(this, EventArgs.Empty);
                }
                if (IsRepeating)
                {
                    _callEvent = true;
                    _elapsedTime = new TimeSpan();
                }
                else
                {
                    _callEvent = false;
                }
            }
        }

        /// <summary>
        /// Create a new GameTimer with the specified interval.
        /// </summary>
        /// <param name="msTimePassing">The interval (in milliseconds) between calls to Elapsed.</param>
        public GameTimer(int msTimePassing)
            : this(TimeSpan.FromMilliseconds(msTimePassing))
        {

        }

        /// <summary>
        /// Gets or sets a boolean indeicating whether or not this timer is repeating.
        /// </summary>
        public bool IsRepeating
        {
            get { return _isRepeating; }
            set { _isRepeating = value; }
        }
        
    }
}
