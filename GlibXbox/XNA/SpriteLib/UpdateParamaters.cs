using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glib.XNA.SpriteLib
{
    /// <summary>
    /// A structure representing things to automatically do when Update() is called on a Sprite.
    /// </summary>
    public struct UpdateParamaters
    {
        /// <summary>
        /// Whether or not to acknowledge the XIncrease value.
        /// </summary>
        public bool UpdateX;

        
        /*
        /// <summary>
        /// Whether or not to follow the mouse pointer.
        /// </summary>
        public bool FollowMouse = false;

        /// <summary>
        /// The velocity at which to follow the mouse.
        /// </summary>
        public float MouseFollowSpeed = .1f;
        */

        /// <summary>
        /// Whether or not to acknowledge the YIncrease value.
        /// </summary>
        public bool UpdateY;

        /// <summary>
        /// Whether or not to automatically fix the sprite from going off the edge based on XIncrease and YIncrease values.
        /// </summary>
        public bool FixEdgeOff;

        /// <summary>
        /// Create a new UpdateParamaters with the values specified.
        /// </summary>
        /// <param name="UpdateXParam">Whether or not to acknowledge the XIncrease value.</param>
        /// <param name="UpdateYParam">Whether or not to acknowledge the YIncrease value.</param>
        /// <param name="FixEdgeOffParam">Whether or not to automatically fix the sprite from going off the edge.</param>
        public UpdateParamaters(bool UpdateXParam, bool UpdateYParam, bool FixEdgeOffParam) : this(UpdateXParam, UpdateYParam)
        {
            this.FixEdgeOff = FixEdgeOffParam;
        }

        
       /*
        /// <summary>
        /// Create a new UpdateParamaters with the values specified.
        /// </summary>
        /// <param name="UpdateXParam">Whether or not to acknowledge the XIncrease value.</param>
        /// <param name="UpdateYParam">Whether or not to acknowledge the YIncrease value.</param>
        /// <param name="FixEdgeOffParam">Whether or not to automatically fix the sprite from going off the edge.</param>
        /// <param name="mouseFollow">Whether or not to follow the mouse.</param>
        public UpdateParamaters(bool UpdateXParam, bool UpdateYParam, bool FixEdgeOffParam, bool mouseFollow)
            : this(UpdateXParam, UpdateYParam, FixEdgeOffParam)
        {
            this.FixEdgeOff = FixEdgeOffParam;
            this.FollowMouse = mouseFollow;
        }
       */

        /// <summary>
        /// Create a new UpdateParamaters with the values specified, and the default (false) for FixEdgeOff.
        /// </summary>
        /// <param name="UpdateXParam">Whether or not to acknowledge the XIncrease value.</param>
        /// <param name="UpdateYParam">Whether or not to acknowledge the YIncrease value.</param>
        public UpdateParamaters(bool UpdateXParam, bool UpdateYParam)
        {
            this.UpdateX = UpdateXParam;
            this.UpdateY = UpdateYParam;
            this.FixEdgeOff = false;
        }

    }
}
