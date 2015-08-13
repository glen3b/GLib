using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Glib.WinForms
{
    /// <summary>
    /// Indicates the state of a ProgressBar.
    /// </summary>
    public enum ProgressBarState : int
    {
        /// <summary>
        /// The ProgressBar is in its normal, green state. This enumeration has a value of 1.
        /// </summary>
        Normal = 1,
        /// <summary>
        /// The ProgressBar is in its errored, red state. This enumeration has a value of 2.
        /// </summary>
        Error = 2,
        /// <summary>
        /// The ProgressBar is in its paused, yellow state. This enumeration has a value of 3.
        /// </summary>
        Paused = 3,
    }

    /// <summary>
    /// Provides extension methods for Windows Forms objects.
    /// </summary>
    public static class WindowsFormsExtensions
    {
        const int WM_USER = 0x400;
        const int PBM_SETSTATE = WM_USER + 16;
        const int PBM_GETSTATE = WM_USER + 17;
        const uint BCM_SETSHIELD = 0x0000160C;

        // TODO: HandleRef or IntPtr?
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Gets the current ProgressBarState of a given ProgressBar.
        /// </summary>
        /// <param name="pBar">The ProgressBar whose state is being queried.</param>
        /// <returns>The ProgressBarState representing the state: normal, errored, or paused, of the ProgressBar.</returns>
        public static ProgressBarState GetState(this ProgressBar pBar)
        {
            return (ProgressBarState)(int)SendMessage(new HandleRef(pBar, pBar.Handle), PBM_GETSTATE, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// Toggles the Windows Vista and above UAC shield icon appearing on a button.
        /// The Button must have its <see cref="System.Windows.Forms.ButtonBase.FlatStyle"/> property set to <see cref="FlatStyle.System"/> for it to appear.
        /// </summary>
        /// <param name="button">The button whose UAC state is being toggled.</param>
        /// <param name="shieldStatus">The new status of the UAC shield.</param>
        /// <exception cref="ArgumentException">If the Button does not have the appropriate FlatStyle.</exception>
        public static void SetUACShield(this ButtonBase button, Boolean shieldStatus)
        {
            if (button.FlatStyle != FlatStyle.System)
            {
                throw new ArgumentException("This Button does not have the appropriate FlatStyle of System.");
            }

            SendMessage(new HandleRef(button, button.Handle), BCM_SETSHIELD, IntPtr.Zero, new IntPtr(shieldStatus ? 1 : 0));
        }

        /// <summary>
        /// Sets the ProgressBarState of a given ProgressBar.
        /// </summary>
        /// <param name="pBar">The progress bar control whose state is being set.</param>
        /// <param name="state">The new state of the progress bar.</param>
        public static void SetState(this ProgressBar pBar, ProgressBarState state)
        {
            SendMessage(new HandleRef(pBar, pBar.Handle), PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
        } 
    }
}
