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
        plus = 187,// Main + = 
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
        ///These should stay const bytes so as to not have to convert from an enum.
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

        #region Constructor

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
