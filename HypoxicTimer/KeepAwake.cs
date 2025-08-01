using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace HypoxicTimer
{
    unsafe public static class KeepAwake
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        internal const int INPUT_MOUSE = 0;
        internal const int MOUSEEVENTF_MOVE = 0x0001;
        
        public static bool Wakeup()
        {
            int dx = 0;
            int dy = 0;

            INPUT inp = new INPUT();
            inp.type = KeepAwake.INPUT_MOUSE;
            inp.mkhi.mi.dx = dx;
            inp.mkhi.mi.dy = dy;
            inp.mkhi.mi.mouseData = 0;
            inp.mkhi.mi.dwFlags = KeepAwake.MOUSEEVENTF_MOVE;
            inp.mkhi.mi.time = 0;
            inp.mkhi.mi.dwExtraInfo = (IntPtr)0;

            uint retval = SendInput(1, ref inp, Marshal.SizeOf(inp));
            if (retval != 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

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
}
