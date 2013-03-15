using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ChapterRelics
{

    /// <summary>
    /// Captures global mouse events
    /// </summary>
    public enum GlobalMouseEventType
    {
        None,
        MouseDown,
        MouseUp,
        DoubleClick,
        MouseWheel,
        MouseMove
    }

    public class GlobalMouseHook : GlobalHook
    {
        public delegate void DelegateToGlobalMouseEvent(int nCode, int wParam, IntPtr lParam, MouseButtons button, GlobalMouseEventType eventType, int delta, int numberOfClicks, MouseLLHookStruct mouseHookStruct);
        public DelegateToGlobalMouseEvent delToGlobalMouseEvent;

        public GlobalMouseHook()
        {
            _hookType = WH_MOUSE_LL_14;
            _threadId = 0;
        }

        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            if (nCode > -1)
            {
                MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
                MouseButtons button = GetButton(wParam);
                GlobalMouseEventType eventType = GetEventType(wParam);
                int delta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
                int numberOfClicks = (eventType == GlobalMouseEventType.DoubleClick ? 2 : 1);
                if (delToGlobalMouseEvent != null)
                    delToGlobalMouseEvent(nCode, wParam, lParam, button, eventType, delta, numberOfClicks, mouseHookStruct);

                //MouseEventArgs e = new MouseEventArgs(
                //    button,
                //    (eventType == GlobalMouseEventType.DoubleClick ? 2 : 1),
                //    mouseHookStruct.pt.x,
                //    mouseHookStruct.pt.y,
                //    (eventType == GlobalMouseEventType.MouseWheel ? (short)((mouseHookStruct.mouseData >> 16) & 0xffff) : 0));
            }
            return CallNextHookEx(_handleToHook, nCode, wParam, lParam);
        }

        private const int XButtonDown_523 = 523;
        private const int XButtonUp_524 = 524;
        private const int XButton1 = 1;
        private const int XButton2 = 2;
        private MouseButtons GetButton(Int32 wParam)
        {
            switch (wParam)
            {
                case WM_LBUTTONDOWN_513:
                case WM_LBUTTONUP_514:
                case WM_LBUTTONDBLCLK_515:
                    return MouseButtons.Left;
                case WM_RBUTTONDOWN_516:
                case WM_RBUTTONUP_517:
                case WM_RBUTTONDBLCLK_518:
                    return MouseButtons.Right;
                case WM_MBUTTONDOWN_519:
                case WM_MBUTTONUP_520:
                case WM_MBUTTONDBLCLK_521:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;
            }
        }

        private GlobalMouseEventType GetEventType(Int32 wParam)
        {
            switch (wParam)
            {
                case WM_LBUTTONDOWN_513:
                case WM_RBUTTONDOWN_516:
                case WM_MBUTTONDOWN_519:
                    return GlobalMouseEventType.MouseDown;
                case WM_LBUTTONUP_514:
                case WM_RBUTTONUP_517:
                case WM_MBUTTONUP_520:
                    return GlobalMouseEventType.MouseUp;
                case WM_LBUTTONDBLCLK_515:
                case WM_RBUTTONDBLCLK_518:
                case WM_MBUTTONDBLCLK_521:
                    return GlobalMouseEventType.DoubleClick;
                case WM_MOUSEWHEEL_522:
                    return GlobalMouseEventType.MouseWheel;
                case WM_MOUSEMOVE_512:
                    return GlobalMouseEventType.MouseMove;
                default:
                    return GlobalMouseEventType.None;
            }
        }
    }
}
