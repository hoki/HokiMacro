using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ChapterRelics
{
    public class KeyArgs
    {
        private byte _keyCode;
        public byte KeyCode
        {
            get
            {
                return _keyCode;
            }
            set
            {
                _keyCode = value;
                try
                {
                    _key = (key)value;
                    _dxkey = Keys.GetDxKey(_key);
                }
                catch (Exception) { /* swallow */ }
            }
        }
        public bool Control { get; set; }
        public bool Shift { get; set; }
        public bool Alt { get; set; }
        public bool CapsLock { get; set; }
        public bool KeyUp { get; set; }

        private key _key;
        public key key { get { return _key; } }

        private dxKey _dxkey;
        public dxKey dxKey { get { return _dxkey; } }
    }


    /// <summary>
    /// Captures global keyboard events
    /// </summary>
    public class GlobalKeyboardHook : GlobalHook
    {
        public GlobalKeyboardHook()
        {
            _hookType = WH_KEYBOARD_LL_13;
            _threadId = 0;
        }

        public delegate void DelegateToGlobalKeyboardEvent(KeyArgs keyArgs);
        public DelegateToGlobalKeyboardEvent delToGlobalKeyboardEvent;

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode > -1)
            {
                KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                // Is Control being held down?
                bool control = ((GetKeyState(VK_LCONTROL_162) & 0x80) != 0) ||
                               ((GetKeyState(VK_RCONTROL_163) & 0x80) != 0);
                // Is Shift being held down?
                bool shift = ((GetKeyState(VK_LSHIFT_160) & 0x80) != 0) ||
                             ((GetKeyState(VK_RSHIFT_161) & 0x80) != 0);
                // Is Alt being held down?
                bool alt = ((GetKeyState(VK_LALT_164) & 0x80) != 0) ||
                           ((GetKeyState(VK_RALT_165) & 0x80) != 0);
                // Is CapsLock on?
                bool capslock = (GetKeyState(VK_CAPITAL_20) != 0);

                if ((wParam == WM_KEYDOWN_256 || wParam == WM_KEYUP_257) && delToGlobalKeyboardEvent != null)
                    delToGlobalKeyboardEvent(new KeyArgs
                    {
                        KeyCode = (byte)keyboardHookStruct.vkCode,
                        Control = control,
                        Shift = shift,
                        Alt = alt,
                        CapsLock = capslock,
                        KeyUp = (wParam == WM_KEYUP_257) ? true : false
                    });
            }
            return CallNextHookEx(_handleToHook, nCode, wParam, lParam);
        }
    }
}
