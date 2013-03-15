using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChapterRelics
{
    /// <summary>
    /// Simulate keyboard key presses
    /// </summary>
    public static class KeySim
    {

        #region Windows API Code

        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte key, byte scan, int flags, int extraInfo); 

        #endregion

        #region Methods

        public static void KeyDown(byte key)
        {
            keybd_event(key, 0, 0, 0);
        }

        public static void KeyUp(byte key)
        {
            keybd_event(key, 0, 2, 0);
        }

        public static void KeyPress(byte key, int delay)
        {
            if(delay < 1)
            {
                KeyDown(key);
                KeyUp(key);
            }
            else
            {
                KeyDown(key);
                System.Threading.Thread.Sleep(delay);
                KeyUp(key);
            }
        }
        #endregion
    }
}
