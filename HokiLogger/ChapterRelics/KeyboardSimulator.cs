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

        struct INPUT
        {
            public UInt32 type;
            public ushort wVk;
            public ushort wScan;
            public UInt32 dwFlags;
            public UInt32 time;
            public UIntPtr dwExtraInfo;
            public UInt32 uMsg;
            public ushort wParamL;
            public ushort wParamH;

        }

        enum SendInputFlags
        {
            KEYEVENTF_EXTENDEDKEY = 0x0001,
            KEYEVENTF_KEYUP = 0x0002,
            KEYEVENTF_UNICODE = 0x0004,
            KEYEVENTF_SCANCODE = 0x0008,
        }

        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte key, byte scan, int flags, int extraInfo);

        [DllImport("user32.dll")]
        static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);

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
