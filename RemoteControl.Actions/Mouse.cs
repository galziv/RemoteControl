using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RemoteControl.Actions
{
    public class Mouse
    {
        [DllImport("User32.Dll")]
        private static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        private static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        public static int X { get; set; }
        public static int Y { get; set; }

        public string FilePath { get; set; }

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
        }

        public void MoveBy(int x, int y)
        {
            mouse_event((int)(MouseEventFlags.MOVE), x, y, 0, 0);
        }

        public void LeftClick()
        {
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
        }

        public void RightClick()
        {
            mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
        }
    }
}
