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

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct MouseKeybdHardwareInputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;

            [FieldOffset(0)]
            public KEYBDINPUT ki;

            [FieldOffset(0)]
            public HARDWAREINPUT hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public MouseKeybdHardwareInputUnion mkhi;
        }
        enum SendInputFlags
        {
            KEYEVENTF_EXTENDEDKEY = 0x0001,
            KEYEVENTF_KEYUP = 0x0002,
            KEYEVENTF_UNICODE = 0x0004,
            KEYEVENTF_SCANCODE = 0x0008,
        }

        /****************************************************************************
        *
        * DirectInput keyboard scan codes
        *
        ****************************************************************************/
        public const byte DIK_ESCAPE = 0x01;
        public const byte DIK_1 = 0x02;
        public const byte DIK_2 = 0x03;
        public const byte DIK_3 = 0x04;
        public const byte DIK_4 = 0x05;
        public const byte DIK_5 = 0x06;
        public const byte DIK_6 = 0x07;
        public const byte DIK_7 = 0x08;
        public const byte DIK_8 = 0x09;
        public const byte DIK_9 = 0x0A;
        public const byte DIK_0 = 0x0B;
        public const byte DIK_MINUS = 0x0C; /* - on main keyboard */
        public const byte DIK_EQUALS = 0x0D;
        public const byte DIK_BACK = 0x0E; /* backspace */
        public const byte DIK_TAB = 0x0F;
        public const byte DIK_Q = 0x10;
        public const byte DIK_W = 0x11;
        public const byte DIK_E = 0x12;
        public const byte DIK_R = 0x13;
        public const byte DIK_T = 0x14;
        public const byte DIK_Y = 0x15;
        public const byte DIK_U = 0x16;
        public const byte DIK_I = 0x17;
        public const byte DIK_O = 0x18;
        public const byte DIK_P = 0x19;
        public const byte DIK_LBRACKET = 0x1A;
        public const byte DIK_RBRACKET = 0x1B;
        public const byte DIK_RETURN = 0x1C; /* Enter on main keyboard */
        public const byte DIK_LCONTROL = 0x1D;
        public const byte DIK_A = 0x1E;
        public const byte DIK_S = 0x1F;
        public const byte DIK_D = 0x20;
        public const byte DIK_F = 0x21;
        public const byte DIK_G = 0x22;
        public const byte DIK_H = 0x23;
        public const byte DIK_J = 0x24;
        public const byte DIK_K = 0x25;
        public const byte DIK_L = 0x26;
        public const byte DIK_SEMICOLON = 0x27;
        public const byte DIK_APOSTROPHE = 0x28;
        public const byte DIK_GRAVE = 0x29; /* accent grave */
        public const byte DIK_LSHIFT = 0x2A;
        public const byte DIK_BACKSLASH = 0x2B;
        public const byte DIK_Z = 0x2C;
        public const byte DIK_X = 0x2D;
        public const byte DIK_C = 0x2E;
        public const byte DIK_V = 0x2F;
        public const byte DIK_B = 0x30;
        public const byte DIK_N = 0x31;
        public const byte DIK_M = 0x32;
        public const byte DIK_COMMA = 0x33;
        public const byte DIK_PERIOD = 0x34; /* . on main keyboard */
        public const byte DIK_SLASH = 0x35; /* / on main keyboard */
        public const byte DIK_RSHIFT = 0x36;
        public const byte DIK_MULTIPLY = 0x37; /* * on numeric keypad */
        public const byte DIK_LMENU = 0x38; /* left Alt */
        public const byte DIK_SPACE = 0x39;
        public const byte DIK_CAPITAL = 0x3A;
        public const byte DIK_F1 = 0x3B;
        public const byte DIK_F2 = 0x3C;
        public const byte DIK_F3 = 0x3D;
        public const byte DIK_F4 = 0x3E;
        public const byte DIK_F5 = 0x3F;
        public const byte DIK_F6 = 0x40;
        public const byte DIK_F7 = 0x41;
        public const byte DIK_F8 = 0x42;
        public const byte DIK_F9 = 0x43;
        public const byte DIK_F10 = 0x44;
        public const byte DIK_NUMLOCK = 0x45;
        public const byte DIK_SCROLL = 0x46; /* Scroll Lock */
        public const byte DIK_NUMPAD7 = 0x47;
        public const byte DIK_NUMPAD8 = 0x48;
        public const byte DIK_NUMPAD9 = 0x49;
        public const byte DIK_SUBTRACT = 0x4A; /* - on numeric keypad */
        public const byte DIK_NUMPAD4 = 0x4B;
        public const byte DIK_NUMPAD5 = 0x4C;
        public const byte DIK_NUMPAD6 = 0x4D;
        public const byte DIK_ADD = 0x4E; /* + on numeric keypad */
        public const byte DIK_NUMPAD1 = 0x4F;
        public const byte DIK_NUMPAD2 = 0x50;
        public const byte DIK_NUMPAD3 = 0x51;
        public const byte DIK_NUMPAD0 = 0x52;
        public const byte DIK_DECIMAL = 0x53; /* . on numeric keypad */
        public const byte DIK_OEM_102 = 0x56; /* < > | on UK/Germany keyboards */
        public const byte DIK_F11 = 0x57;
        public const byte DIK_F12 = 0x58;
        public const byte DIK_F13 = 0x64; /* (NEC PC98) */
        public const byte DIK_F14 = 0x65; /* (NEC PC98) */
        public const byte DIK_F15 = 0x66; /* (NEC PC98) */
        public const byte DIK_KANA = 0x70; /* (Japanese keyboard) */
        public const byte DIK_ABNT_C1 = 0x73; /* / ? on Portugese (Brazilian) keyboards */
        public const byte DIK_CONVERT = 0x79; /* (Japanese keyboard) */
        public const byte DIK_NOCONVERT = 0x7B; /* (Japanese keyboard) */
        public const byte DIK_YEN = 0x7D; /* (Japanese keyboard) */
        public const byte DIK_ABNT_C2 = 0x7E; /* Numpad . on Portugese (Brazilian) keyboards */
        public const byte DIK_NUMPADEQUALS = 0x8D; /* = on numeric keypad (NEC PC98) */
        public const byte DIK_PREVTRACK = 0x90; /* Previous Track (DIK_CIRCUMFLEX on Japanese keyboard) */
        public const byte DIK_AT = 0x91; /* (NEC PC98) */
        public const byte DIK_COLON = 0x92; /* (NEC PC98) */
        public const byte DIK_UNDERLINE = 0x93; /* (NEC PC98) */
        public const byte DIK_KANJI = 0x94; /* (Japanese keyboard) */
        public const byte DIK_STOP = 0x95; /* (NEC PC98) */
        public const byte DIK_AX = 0x96; /* (Japan AX) */
        public const byte DIK_UNLABELED = 0x97; /* (J3100) */
        public const byte DIK_NEXTTRACK = 0x99; /* Next Track */
        public const byte DIK_NUMPADENTER = 0x9C; /* Enter on numeric keypad */
        public const byte DIK_RCONTROL = 0x9D;
        public const byte DIK_MUTE = 0xA0; /* Mute */
        public const byte DIK_CALCULATOR = 0xA1; /* Calculator */
        public const byte DIK_PLAYPAUSE = 0xA2; /* Play / Pause */
        public const byte DIK_MEDIASTOP = 0xA4; /* Media Stop */
        public const byte DIK_VOLUMEDOWN = 0xAE; /* Volume - */
        public const byte DIK_VOLUMEUP = 0xB0; /* Volume + */
        public const byte DIK_WEBHOME = 0xB2; /* Web home */
        public const byte DIK_NUMPADCOMMA = 0xB3; /* , on numeric keypad (NEC PC98) */
        public const byte DIK_DIVIDE = 0xB5; /* / on numeric keypad */
        public const byte DIK_SYSRQ = 0xB7;
        public const byte DIK_RMENU = 0xB8; /* right Alt */
        public const byte DIK_PAUSE = 0xC5; /* Pause */
        public const byte DIK_HOME = 0xC7; /* Home on arrow keypad */
        public const byte DIK_UP = 0xC8; /* UpArrow on arrow keypad */
        public const byte DIK_PRIOR = 0xC9; /* PgUp on arrow keypad */
        public const byte DIK_LEFT = 0xCB; /* LeftArrow on arrow keypad */
        public const byte DIK_RIGHT = 0xCD; /* RightArrow on arrow keypad */
        public const byte DIK_END = 0xCF; /* End on arrow keypad */
        public const byte DIK_DOWN = 0xD0; /* DownArrow on arrow keypad */
        public const byte DIK_NEXT = 0xD1; /* PgDn on arrow keypad */
        public const byte DIK_INSERT = 0xD2; /* Insert on arrow keypad */
        public const byte DIK_DELETE = 0xD3; /* Delete on arrow keypad */
        public const byte DIK_LWIN = 0xDB; /* Left Windows key */
        public const byte DIK_RWIN = 0xDC; /* Right Windows key */
        public const byte DIK_APPS = 0xDD; /* AppMenu key */
        public const byte DIK_POWER = 0xDE; /* System Power */
        public const byte DIK_SLEEP = 0xDF; /* System Sleep */
        public const byte DIK_WAKE = 0xE3; /* System Wake */
        public const byte DIK_WEBSEARCH = 0xE5; /* Web Search */
        public const byte DIK_WEBFAVORITES = 0xE6; /* Web Favorites */
        public const byte DIK_WEBREFRESH = 0xE7; /* Web Refresh */
        public const byte DIK_WEBSTOP = 0xE8; /* Web Stop */
        public const byte DIK_WEBFORWARD = 0xE9; /* Web Forward */
        public const byte DIK_WEBBACK = 0xEA; /* Web Back */
        public const byte DIK_MYCOMPUTER = 0xEB; /* My Computer */
        public const byte DIK_MAIL = 0xEC; /* Mail */
        public const byte DIK_MEDIASELECT = 0xED; /* Media Select */
        /*
        * Alternate names for keys, to facilitate transition from DOS.
        */
        public const byte DIK_BACKSPACE = DIK_BACK; /* backspace */
        public const byte DIK_NUMPADSTAR = DIK_MULTIPLY; /* * on numeric keypad */
        public const byte DIK_LALT = DIK_LMENU; /* left Alt */
        public const byte DIK_CAPSLOCK = DIK_CAPITAL; /* CapsLock */
        public const byte DIK_NUMPADMINUS = DIK_SUBTRACT; /* - on numeric keypad */
        public const byte DIK_NUMPADPLUS = DIK_ADD; /* + on numeric keypad */
        public const byte DIK_NUMPADPERIOD = DIK_DECIMAL; /* . on numeric keypad */
        public const byte DIK_NUMPADSLASH = DIK_DIVIDE; /* / on numeric keypad */
        public const byte DIK_RALT = DIK_RMENU; /* right Alt */
        public const byte DIK_UPARROW = DIK_UP; /* UpArrow on arrow keypad */
        public const byte DIK_PGUP = DIK_PRIOR; /* PgUp on arrow keypad */
        public const byte DIK_LEFTARROW = DIK_LEFT; /* LeftArrow on arrow keypad */
        public const byte DIK_RIGHTARROW = DIK_RIGHT; /* RightArrow on arrow keypad */
        public const byte DIK_DOWNARROW = DIK_DOWN; /* DownArrow on arrow keypad */
        public const byte DIK_PGDN = DIK_NEXT; /* PgDn on arrow keypad */
        /*
        * Alternate names for keys originally not used on US keyboards.
        */
        public const byte DIK_CIRCUMFLEX = DIK_PREVTRACK; /* Japanese keyboard */


        public const int KEYEVENTF_EXTENDEDKEY = 0x1;
        public const int KEYEVENTF_KEYUP = 0x2;
        public const int INPUT_MOUSE = 0;
        public const int INPUT_KEYBOARD = 1;
        public const int INPUT_HARDWARE = 2;
        public const int KEYEVENTF_SCANCODE = 0x0008;

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

        public static void KeyPress(key key, int delay = 0)
        {
            if (key != key.Null)
            {
                KeyPressDI(Keys.GetDxKey(key), delay);
                //if(delay < 1)
                //{
                //    KeyDown((byte)key);
                //    KeyUp((byte)key);
                //}
                //else
                //{
                //    KeyDown((byte)key);
                //    System.Threading.Thread.Sleep(delay);
                //    KeyUp((byte)key);
                //}
            }
        }

        public static void KeyPressDI(dxKey key, int delay = 0)
        {
            if (key != dxKey.Null)
            {
                //Keydown
                INPUT[] inputs = new INPUT[1];
                inputs[0].type = INPUT_KEYBOARD;
                inputs[0].mkhi.ki.dwFlags = (uint)SendInputFlags.KEYEVENTF_SCANCODE;
                inputs[0].mkhi.ki.wScan = (ushort)key;
                SendInput(1, inputs, Marshal.SizeOf(inputs[0]));

                //Wait
                System.Threading.Thread.Sleep(delay);

                //Keyup
                inputs = new INPUT[1];
                inputs[0].type = INPUT_KEYBOARD;
                inputs[0].mkhi.ki.dwFlags = (uint)SendInputFlags.KEYEVENTF_KEYUP | (uint)SendInputFlags.KEYEVENTF_SCANCODE;
                inputs[0].mkhi.ki.wScan = (ushort)key;
                SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
            }
        }

        #endregion
    }
}
