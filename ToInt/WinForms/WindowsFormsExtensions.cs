using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Glib.WinForms
{
    public enum ProgressBarState : int
    {
        Normal = 1,
        Error = 2,
        Paused = 3,
    }

    public static class WindowsFormsExtensions
    {
        const int WM_USER = 0x400;
        const int PBM_SETSTATE = WM_USER + 16;
        const int PBM_GETSTATE = WM_USER + 17;
        const uint BCM_SETSHIELD = 0x0000160C;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public static ProgressBarState GetState(this ProgressBar pBar)
        {
            return (ProgressBarState)(int)SendMessage(pBar.Handle, PBM_GETSTATE, IntPtr.Zero, IntPtr.Zero);
        }

        public static void SetUACShield(this Button button, Boolean shieldStatus)
        {
            SendMessage(button.Handle, BCM_SETSHIELD, IntPtr.Zero, new IntPtr(shieldStatus ? 1 : 0));
        }

        public static void SetState(this ProgressBar pBar, ProgressBarState state)
        {
            SendMessage(pBar.Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
        } 
    }
}
