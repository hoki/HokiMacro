using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

namespace ChapterRelics
{

    [StructLayout(LayoutKind.Sequential)]
    public class POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MouseHookStruct
    {
        public POINT pt;
        public int hwnd;
        public int wHitTestCode;
        public int dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class MouseLLHookStruct
    {
        public POINT pt;
        public int mouseData;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public class KeyboardHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    public enum key
    {
        zero_numpad = 96,// Numpad 0 
        one_numpad = 97,// Numpad 1 
        two_numpad = 98,// Numpad 2 
        three_numpad = 99,// Numpad 3 
        four_numpad = 100,// Numpad 4 
        five_numpad = 101,// Numpad 5 
        six_numpad = 102,// Numpad 6 
        seven_numpad = 103,// Numpad 7 
        eight_numpad = 104,// Numpad 8 
        nine_numpad = 105,// Numpad 9 

        numlock = 144,// Numlock 
        forwardSlash_numpad = 111,// Numpad / 
        asterisk_numpad = 106,// Numpad * 
        dash_numpad = 109,// Numpad - 
        plus_numpad = 107,// Numpad + 
        period_numpad = 110,// Numpad . 

        five_numlock_numpad = 12,// Numpad-numlock 5, doesn't do anything 

        printScreen = 44,// Print Screen 
        scrollLock = 145,// Scroll Lock 
        pauseBreak = 19,// Pause Break 

        insert = 45,// Insert 
        home = 36,// Home 
        pageUp = 33,// Page Up 
        delete = 46,// Delete 
        end = 35,// End 
        pageDown = 34,// Page Down 

        leftArrow = 37,// Left Arrow 
        upArrow = 38,// Up Arrow 
        rightArrow = 39,// Right Arrow 
        downArrow = 40,// Down Arrow 

        f1 = 112,// F1 
        f2 = 113,// F2 
        f3 = 114,// F3 
        f4 = 115,// F4 
        f5 = 116,// F5 
        f6 = 117,// F6 
        f7 = 118,// F7 
        f8 = 119,// F8 
        f9 = 120,// F9 
        f10 = 121,// F10 
        f11 = 122,// F11 
        f12 = 123,// F12 

        escape = 27,// Escape 

        tilde = 192,// Tilde ~, ` 
        one = 49,// Main 1 ! 
        two = 50,// Main 2 @ 
        three = 51,// Main 3 # 
        four = 52,// Main 4 $ 
        five = 53,// Main 5 % 
        six = 54,// Main 6 ^ 
        seven = 55,// Main 7 & 
        eight = 56,// Main 8 * 
        nine = 57,// Main 9 ( 
        zero = 48,// Main 0 ) 
        dash = 189,// Main - _ 
        equals = 187,// Main + = 
        backspace = 8,// Backspace 

        tab = 9,// Tab 
        Q = 81,// Q 
        W = 87,// W 
        E = 69,// E 
        R = 82,// R 
        T = 84,// T 
        Y = 89,// Y 
        U = 85,// U 
        I = 73,// I 
        O = 79,// O 
        P = 80,// P 
        bracket_open = 219,// [ { 
        bracket_close = 221,// ] } 
        backSlash = 220,// \ | 

        capsLock = 20,// Capslock 
        A = 65,// A 
        S = 83,// S 
        D = 68,// D 
        F = 70,// F 
        G = 71,// G 
        H = 72,// H 
        J = 74,// J 
        K = 75,// K 
        L = 76,// L 
        semiColon = 186,// ; : 
        quote = 222,// " '
        enter = 13,// Enter 

        shift_left = 160,// Shift (Left) 
        Z = 90,// Z 
        X = 88,// X 
        C = 67,// C 
        V = 86,// V 
        B = 66,// B 
        N = 78,// N 
        M = 77,// M 
        comma = 188,// , < 
        period = 190,// . > 
        forwardSlash = 191,// / ? 
        shift_right = 161,// Shift (Right) 

        control_left = 162,// Control (Left) 
        windows_left = 91,// Windows (Left) 
        alt_left = 164,// Alt (Left) 
        spacebar = 32,// Spacebar 
        alt_right = 165,// Alt (Right) 
        windows_right = 92,// Windows (Right) - only one of my keyboards had a second windows key 
        menu = 93,// Menu Button, at least it looks like a menu on my keyboards 
        control_right = 163,// Control (Right) 

        Null = -1
    }

    public enum dxKey
    {
        zero_numpad = 0x52,// Numpad 0 
        one_numpad = 0x4F,// Numpad 1 
        two_numpad = 0x50,// Numpad 2 
        three_numpad = 0x51,// Numpad 3 
        four_numpad = 0x4B,// Numpad 4 
        five_numpad = 0x4C,// Numpad 5 
        six_numpad = 0x4D,// Numpad 6 
        seven_numpad = 0x47,// Numpad 7 
        eight_numpad = 0x48,// Numpad 8 
        nine_numpad = 0x49,// Numpad 9 

        numlock = 0x45,// Numlock 
        forwardSlash_numpad = 0xB5,// Numpad / 
        asterisk_numpad = 0x37,// Numpad * 
        dash_numpad = 0x4A,// Numpad - 
        plus_numpad = 0x4E,// Numpad + 
        period_numpad = 0x53,// Numpad . 

        five_numlock_numpad = 0x4C,// Numpad-numlock 5, doesn't do anything 

        printScreen = 0xB7,// Print Screen 
        scrollLock = 0x46,// Scroll Lock 
        pauseBreak = 0xC5,// Pause Break 

        insert = 0xD2,// Insert 
        home = 0xC7,// Home 
        pageUp = 0xC9,// Page Up 
        delete = 0xD3,// Delete 
        end = 0xCF,// End 
        pageDown = 0xD1,// Page Down 

        leftArrow = 0xCB,// Left Arrow 
        upArrow = 0xC8,// Up Arrow 
        rightArrow = 0xCD,// Right Arrow 
        downArrow = 0xD0,// Down Arrow 

        f1 = 0x3B,// F1 
        f2 = 0x3C,// F2 
        f3 = 0x3D,// F3 
        f4 = 0x3E,// F4 
        f5 = 0x3F,// F5 
        f6 = 0x40,// F6 
        f7 = 0x41,// F7 
        f8 = 0x42,// F8 
        f9 = 0x43,// F9 
        f10 = 0x44,// F10 
        f11 = 0x57,// F11 
        f12 = 0x58,// F12 

        escape = 0x01,// Escape 

        tilde = 0x29,// Tilde ~, ` usually called grave/accent
        one = 0x02,// Main 1 ! 
        two = 0x03,// Main 2 @ 
        three = 0x04,// Main 3 # 
        four = 0x05,// Main 4 $ 
        five = 0x06,// Main 5 % 
        six = 0x07,// Main 6 ^ 
        seven = 0x08,// Main 7 & 
        eight = 0x09,// Main 8 * 
        nine = 0x0A,// Main 9 ( 
        zero = 0x0B,// Main 0 ) 
        dash = 0x0C,// Main - _ 
        equals = 0x0D,// Main + = 
        backspace = 0x0E,// Backspace 

        tab = 0x0F,// Tab 
        Q = 0x10,// Q 
        W = 0x11,// W 
        E = 0x12,// E 
        R = 0x13,// R 
        T = 0x14,// T 
        Y = 0x15,// Y 
        U = 0x16,// U 
        I = 0x17,// I 
        O = 0x18,// O 
        P = 0x19,// P 
        bracket_open = 0x1A,// [ { 
        bracket_close = 0x1B,// ] } 
        backSlash = 0x2B,// \ | 

        capsLock = 0x3A,// Capslock 
        A = 0x1E,// A 
        S = 0x1F,// S 
        D = 0x20,// D 
        F = 0x21,// F 
        G = 0x22,// G 
        H = 0x23,// H 
        J = 0x24,// J 
        K = 0x25,// K 
        L = 0x26,// L 
        semiColon = 0x27,// ; : 
        quote = 0x28,// " '
        enter = 0x1C,// Enter 

        shift_left = 0x2A,// Shift (Left) 
        Z = 0x2C,// Z 
        X = 0x2D,// X 
        C = 0x2E,// C 
        V = 0x2F,// V 
        B = 0x30,// B 
        N = 0x31,// N 
        M = 0x32,// M 
        comma = 0x33,// , < 
        period = 0x34,// . > 
        forwardSlash = 0x35,// / ? 
        shift_right = 0x36,// Shift (Right) 

        control_left = 0x1D,// Control (Left) 
        windows_left = 0xDB,// Windows (Left) 
        alt_left = 0x38,// Alt (Left) 
        spacebar = 0x39,// Spacebar 
        alt_right = 0xB8,// Alt (Right) 
        windows_right = 0xDC,// Windows (Right) - only one of my keyboards had a second windows key 
        menu = 0xDD,// Menu Button, at least it looks like a menu on my keyboards 
        control_right = 0x9D,// Control (Right) 

        Null = -1
    }

    public static class Keys
    {
        public static dxKey GetDxKey(key key)
        {
            switch (key)
            {
                case key.zero_numpad: return dxKey.zero_numpad;
                case key.one_numpad: return dxKey.one_numpad;
                case key.two_numpad: return dxKey.two_numpad;
                case key.three_numpad: return dxKey.three_numpad;
                case key.four_numpad: return dxKey.four_numpad;
                case key.five_numpad: return dxKey.five_numpad;
                case key.six_numpad: return dxKey.six_numpad;
                case key.seven_numpad: return dxKey.seven_numpad;
                case key.eight_numpad: return dxKey.eight_numpad;
                case key.nine_numpad: return dxKey.nine_numpad;

                case key.numlock: return dxKey.numlock;
                case key.forwardSlash_numpad: return dxKey.forwardSlash_numpad;
                case key.asterisk_numpad: return dxKey.asterisk_numpad;
                case key.dash_numpad: return dxKey.dash_numpad;
                case key.plus_numpad: return dxKey.plus_numpad;
                case key.period_numpad: return dxKey.period_numpad;

                case key.five_numlock_numpad: return dxKey.five_numlock_numpad;

                case key.printScreen: return dxKey.printScreen;
                case key.scrollLock: return dxKey.scrollLock;
                case key.pauseBreak: return dxKey.pauseBreak;

                case key.insert: return dxKey.insert;
                case key.home: return dxKey.home;
                case key.pageUp: return dxKey.pageUp;
                case key.delete: return dxKey.delete;
                case key.end: return dxKey.end;
                case key.pageDown: return dxKey.pageDown;

                case key.leftArrow: return dxKey.leftArrow;
                case key.upArrow: return dxKey.upArrow;
                case key.rightArrow: return dxKey.rightArrow;
                case key.downArrow: return dxKey.downArrow;

                case key.f1: return dxKey.f1;
                case key.f2: return dxKey.f2;
                case key.f3: return dxKey.f3;
                case key.f4: return dxKey.f4;
                case key.f5: return dxKey.f5;
                case key.f6: return dxKey.f6;
                case key.f7: return dxKey.f7;
                case key.f8: return dxKey.f8;
                case key.f9: return dxKey.f9;
                case key.f10: return dxKey.f10;
                case key.f11: return dxKey.f11;
                case key.f12: return dxKey.f12;

                case key.escape: return dxKey.escape;

                case key.tilde: return dxKey.tilde;
                case key.one: return dxKey.one;
                case key.two: return dxKey.two;
                case key.three: return dxKey.three;
                case key.four: return dxKey.four;
                case key.five: return dxKey.five;
                case key.six: return dxKey.six;
                case key.seven: return dxKey.seven;
                case key.eight: return dxKey.eight;
                case key.nine: return dxKey.nine;
                case key.zero: return dxKey.zero;
                case key.dash: return dxKey.dash;
                case key.equals: return dxKey.equals;
                case key.backspace: return dxKey.backspace;

                case key.tab: return dxKey.tab;
                case key.Q: return dxKey.Q;
                case key.W: return dxKey.W;
                case key.E: return dxKey.E;
                case key.R: return dxKey.R;
                case key.T: return dxKey.T;
                case key.Y: return dxKey.Y;
                case key.U: return dxKey.U;
                case key.I: return dxKey.I;
                case key.O: return dxKey.O;
                case key.P: return dxKey.P;
                case key.bracket_open: return dxKey.bracket_open;
                case key.bracket_close: return dxKey.bracket_close;
                case key.backSlash: return dxKey.backSlash;

                case key.capsLock: return dxKey.capsLock;
                case key.A: return dxKey.A;
                case key.S: return dxKey.S;
                case key.D: return dxKey.D;
                case key.F: return dxKey.F;
                case key.G: return dxKey.G;
                case key.H: return dxKey.H;
                case key.J: return dxKey.J;
                case key.K: return dxKey.K;
                case key.L: return dxKey.L;
                case key.semiColon: return dxKey.semiColon;
                case key.quote: return dxKey.quote;
                case key.enter: return dxKey.enter;

                case key.shift_left: return dxKey.shift_left;
                case key.Z: return dxKey.Z;
                case key.X: return dxKey.X;
                case key.C: return dxKey.C;
                case key.V: return dxKey.V;
                case key.B: return dxKey.B;
                case key.N: return dxKey.N;
                case key.M: return dxKey.M;
                case key.comma: return dxKey.comma;
                case key.period: return dxKey.period;
                case key.forwardSlash: return dxKey.forwardSlash;
                case key.shift_right: return dxKey.shift_right;

                case key.control_left: return dxKey.control_left;
                case key.windows_left: return dxKey.windows_left;
                case key.alt_left: return dxKey.alt_left;
                case key.spacebar: return dxKey.spacebar;
                case key.alt_right: return dxKey.alt_right;
                case key.windows_right: return dxKey.windows_right;
                case key.menu: return dxKey.menu;
                case key.control_right: return dxKey.control_right;
                default: return dxKey.Null;
            }
        }
    }


    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook
    {
        #region Windows API Code
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowThreadProcessId(HandleRef handle, out int processId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int UnhookWindowsHookEx(int idHook);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);

        [DllImport("user32")]
        protected static extern int ToAscii(int uVirtKey, int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

        [DllImport("user32")]
        protected static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern short GetKeyState(int vKey);

        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        //TODO: turn this shit into an enum - they are not keycodes
        public const int WH_MOUSE_LL_14 = 14;
        public const int WH_KEYBOARD_LL_13 = 13;

        public const int WH_MOUSE_7 = 7;
        public const int WH_KEYBOARD_2 = 2;
        public const int WM_MOUSEMOVE_512 = 512;
        public const int WM_LBUTTONDOWN_513 = 513;
        public const int WM_RBUTTONDOWN_516 = 516;
        public const int WM_MBUTTONDOWN_519 = 519;
        public const int WM_LBUTTONUP_514 = 514;
        public const int WM_RBUTTONUP_517 = 517;
        public const int WM_MBUTTONUP_520 = 520;
        public const int WM_LBUTTONDBLCLK_515 = 515;
        public const int WM_RBUTTONDBLCLK_518 = 518;
        public const int WM_MBUTTONDBLCLK_521 = 521;
        public const int WM_MOUSEWHEEL_522 = 522;
        public const int WM_KEYDOWN_256 = 256;
        public const int WM_KEYUP_257 = 257;
        public const int WM_SYSKEYDOWN_260 = 260;
        public const int WM_SYSKEYUP_261 = 261;


        ///Virtual Keys
        ///http://msdn.microsoft.com/en-us/library/ms927178.aspx
        ///These should stay public const byte bytes so as to not have to convert from an enum.
        ///There are lots of virtual keys but we only need these for grabbing their states.
        ///Like, is capslock on, is left shift pressed, etc~
        public const byte VK_SHIFT_16 = 16;
        public const byte VK_CONTROL_17 = 17;
        public const byte VK_ALT_18 = 18;
        public const byte VK_CAPITAL_20 = 20;
        public const byte VK_NUMLOCK_144 = 144;
        public const byte VK_LSHIFT_160 = 160;
        public const byte VK_RSHIFT_161 = 161;
        public const byte VK_LCONTROL_162 = 162;
        public const byte VK_RCONTROL_163 = 163;
        public const byte VK_LALT_164 = 164;
        public const byte VK_RALT_165 = 165;

        public const byte LLKHF_ALTDOWN_32 = 32;

        

        #endregion


        #region Private Variables

        protected int _hookType;
        protected int _handleToHook;
        protected bool _isStarted;
        protected int _threadId;
        protected HookProc _hookCallback;

        #endregion

        #region Properties

        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }

        #endregion

        #region public const byteructor

        public GlobalHook()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        #endregion

        #region Methods

        public void Start()
        {
            if (!_isStarted && _hookType != 0)
            {
                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                _hookCallback = new HookProc(HookCallbackProcedure);
                _handleToHook = SetWindowsHookEx(
                    _hookType,
                    _hookCallback,
                    Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    _threadId);

                // Were we able to sucessfully start hook?
                if (_handleToHook != 0)
                {
                    _isStarted = true;
                }
            }
        }

        public void Stop()
        {
            if (_isStarted)
            {
                UnhookWindowsHookEx(_handleToHook);
                _isStarted = false;
            }
        }

        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return 0;
        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                Stop();
            }
        }
        #endregion
    }
}
