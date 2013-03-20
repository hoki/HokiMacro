using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChapterRelics;

namespace HokiMacro.Debug
{
    public class Debug
    {
        public Debug() { }
        public static void RightClick(int nCode, int wParam, IntPtr lParam, MouseButtons button, GlobalMouseEventType eventType, int delta, int numberOfClicks, MouseLLHookStruct mouseHookStruct)
        {
            bool stop = true;
        }
        public static void LeftClick(int nCode, int wParam, IntPtr lParam, MouseButtons button, GlobalMouseEventType eventType, int delta, int numberOfClicks, MouseLLHookStruct mouseHookStruct)
        {
            bool stop = true;
        }

        public static string KeyboardEventTrigger(int nCode, int wParam, IntPtr lParam, bool control, bool shift, bool alt, bool capslock, bool keyUp, KeyboardHookStruct keyboardHookStruct)
        {
            return GetKeyDescriptionByVkCode(keyboardHookStruct.vkCode, keyUp);
        }

        public static string GetKeyDescriptionByVkCode(int vkCode, bool keyUp)
        {
            string returnString = string.Empty;
            if (!keyUp)
            {
                switch ((key)vkCode)
                {
                    case key.zero_numpad: returnString = "Numpad 0"; break;
                    case key.one_numpad: returnString = "Numpad 1"; break;
                    case key.two_numpad: returnString = "Numpad 2"; break;
                    case key.three_numpad: returnString = "Numpad 3"; break;
                    case key.four_numpad: returnString = "Numpad 4"; break;
                    case key.five_numpad: returnString = "Numpad 5"; break;
                    case key.six_numpad: returnString = "Numpad 6"; break;
                    case key.seven_numpad: returnString = "Numpad 7"; break;
                    case key.eight_numpad: returnString = "Numpad 8"; break;
                    case key.nine_numpad: returnString = "Numpad 9"; break;

                    case key.numlock: returnString = "Numlock"; break;
                    case key.forwardSlash_numpad: returnString = "Numpad /"; break;
                    case key.asterisk_numpad: returnString = "Numpad *"; break;
                    case key.dash_numpad: returnString = "Numpad -"; break;
                    case key.plus_numpad: returnString = "Numpad +"; break;
                    case key.period_numpad: returnString = "Numpad ."; break;

                    case key.five_numlock_numpad: returnString = "Numpad-numlock 5, doesn't do anything"; break;

                    case key.printScreen: returnString = "Print Screen"; break;
                    case key.scrollLock: returnString = "Scroll Lock"; break;
                    case key.pauseBreak: returnString = "Pause Break"; break;

                    case key.insert: returnString = "Insert"; break;
                    case key.home: returnString = "Home"; break;
                    case key.pageUp: returnString = "Page Up"; break;
                    case key.delete: returnString = "Delete"; break;
                    case key.end: returnString = "End"; break;
                    case key.pageDown: returnString = "Page Down"; break;

                    case key.leftArrow: returnString = "Left Arrow"; break;
                    case key.upArrow: returnString = "Up Arrow"; break;
                    case key.rightArrow: returnString = "Right Arrow"; break;
                    case key.downArrow: returnString = "Down Arrow"; break;

                    case key.f1: returnString = "F1"; break;
                    case key.f2: returnString = "F2"; break;
                    case key.f3: returnString = "F3"; break;
                    case key.f4: returnString = "F4"; break;
                    case key.f5: returnString = "F5"; break;
                    case key.f6: returnString = "F6"; break;
                    case key.f7: returnString = "F7"; break;
                    case key.f8: returnString = "F8"; break;
                    case key.f9: returnString = "F9"; break;
                    case key.f10: returnString = "F10"; break;
                    case key.f11: returnString = "F11"; break;
                    case key.f12: returnString = "F12"; break;

                    case key.escape: returnString = "Escape"; break;

                    case key.tilde: returnString = "Tilde ~, `"; break;
                    case key.one: returnString = "Main 1 !"; break;
                    case key.two: returnString = "Main 2 @"; break;
                    case key.three: returnString = "Main 3 #"; break;
                    case key.four: returnString = "Main 4 $"; break;
                    case key.five: returnString = "Main 5 %"; break;
                    case key.six: returnString = "Main 6 ^"; break;
                    case key.seven: returnString = "Main 7 &"; break;
                    case key.eight: returnString = "Main 8 *"; break;
                    case key.nine: returnString = "Main 9 ("; break;
                    case key.zero: returnString = "Main 0 )"; break;
                    case key.dash: returnString = "Main - _"; break;
                    case key.equals: returnString = "Main + ="; break;
                    case key.backspace: returnString = "Backspace"; break;

                    case key.tab: returnString = "Tab"; break;
                    case key.Q: returnString = "Q"; break;
                    case key.W: returnString = "W"; break;
                    case key.E: returnString = "E"; break;
                    case key.R: returnString = "R"; break;
                    case key.T: returnString = "T"; break;
                    case key.Y: returnString = "Y"; break;
                    case key.U: returnString = "U"; break;
                    case key.I: returnString = "I"; break;
                    case key.O: returnString = "O"; break;
                    case key.P: returnString = "P"; break;
                    case key.bracket_open: returnString = "[ {"; break;
                    case key.bracket_close: returnString = "] }"; break;
                    case key.backSlash: returnString = "\\ |"; break;

                    case key.capsLock: returnString = "Capslock"; break;
                    case key.A: returnString = "A"; break;
                    case key.S: returnString = "S"; break;
                    case key.D: returnString = "D"; break;
                    case key.F: returnString = "F"; break;
                    case key.G: returnString = "G"; break;
                    case key.H: returnString = "H"; break;
                    case key.J: returnString = "J"; break;
                    case key.K: returnString = "K"; break;
                    case key.L: returnString = "L"; break;
                    case key.semiColon: returnString = "; break; :"; break;
                    case key.quote: returnString = "' \""; break;
                    case key.enter: returnString = "Enter"; break;

                    case key.shift_left: returnString = "Shift (Left)"; break;
                    case key.Z: returnString = "Z"; break;
                    case key.X: returnString = "X"; break;
                    case key.C: returnString = "C"; break;
                    case key.V: returnString = "V"; break;
                    case key.B: returnString = "B"; break;
                    case key.N: returnString = "N"; break;
                    case key.M: returnString = "M"; break;
                    case key.comma: returnString = ", <"; break;
                    case key.period: returnString = ". >"; break;
                    case key.forwardSlash: returnString = "/ ?"; break;
                    case key.shift_right: returnString = "Shift (Right)"; break;

                    case key.control_left: returnString = "Control (Left)"; break;
                    case key.windows_left: returnString = "Windows (Left)"; break;
                    //case key.alt_left: returnString = "Alt (Left)"; break;
                    case key.spacebar: returnString = "Spacebar"; break;
                    //case key.alt_right: returnString = "Alt (Right)"; break;
                    case key.windows_right: returnString = "Windows (Right) - only one of my keyboards had a second windows key"; break;
                    case key.menu: returnString = "Menu Button, at least it looks like a menu on my keyboards"; break;
                    case key.control_right: returnString = "Control (Right)"; break;
                }
            }
            else
            {
                switch ((key)vkCode)
                {
                    case key.alt_left: returnString = "Alt (Left)"; break;
                    case key.alt_right: returnString = "Alt (Right)"; break;
                }
            }

            //if (vkCode == 76)//L
            //{
            //    KeyboardSimulator.keybd_event((byte)72, 0, 0, 0);//H
            //    KeyboardSimulator.keybd_event((byte)72, 0, 2, 0);
            //    KeyboardSimulator.keybd_event((byte)79, 0, 0, 0);//O
            //    KeyboardSimulator.keybd_event((byte)79, 0, (byte)2, 0);
            //    KeyboardSimulator.keybd_event((byte)75, 0, 0, 0);//K
            //    KeyboardSimulator.keybd_event((byte)75, 0, KeyboardSimulator.KEYEVENTF_KEYUP, 0);
            //    KeyboardSimulator.keybd_event((byte)73, 0, 0, 0);//I
            //    KeyboardSimulator.keybd_event((byte)73, 0, KeyboardSimulator.KEYEVENTF_KEYUP, 0);
            //}

            return returnString;
        }
    }
}
