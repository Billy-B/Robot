using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    internal static class HWndUtil
    {
        [DllImport("user32.dll")]
        public static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, IntPtr wParam, IntPtr lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, uint wParam, string lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, uint wParam, StringBuilder lParam);
        
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, WM Msg, uint wParam, uint lParam);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref int attrValue, int size);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, ref ANIMATIONINFO pvParam, SPIF fWinIni);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, ShowWindowCommands nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, PositioningFlags uFlags);
        
        [DllImport("USER32.DLL")]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);

        public static IEnumerable<IntPtr> EnumChildren(IntPtr hwnd)
        {
            IntPtr ptr = IntPtr.Zero;
            List<IntPtr> list = new List<IntPtr>();
            do
            {
                ptr = FindWindowEx(hwnd, ptr, null, null);
                if (ptr != IntPtr.Zero)
                {
                    yield return ptr;
                }
            }
            while (ptr != IntPtr.Zero);
        }

        public static IEnumerable<IntPtr> EnumHwnds()
        {
            return EnumChildren(IntPtr.Zero);
        }

        public static string GetHwndClassName(IntPtr hwnd)
        {
            StringBuilder lpClassName = new StringBuilder(0x100);
            GetClassName(hwnd, lpClassName, lpClassName.MaxCapacity);
            return lpClassName.ToString();
        }

        public static IntPtr GetHwndFromClass(string className)
        {
            return (IntPtr)FindWindow(className, null);
        }

        public static IntPtr GetHwndFromTitle(string windowText)
        {
            return (IntPtr)FindWindow(null, windowText);
        }

        public static Point GetHwndPos(IntPtr hwnd)
        {
            RECT lpRect = new RECT();
            GetWindowRect(hwnd, out lpRect);
            return new Point(lpRect.Left, lpRect.Top);
        }

        public static Size GetHwndSize(IntPtr hwnd)
        {
            RECT lpRect = new RECT();
            GetWindowRect(hwnd, out lpRect);
            return new Size(lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top);
        }

        public static string GetHwndText(IntPtr hwnd)
        {
            int capacity = ((int)SendMessage(hwnd, WM.GETTEXTLENGTH, 0, 0)) + 1;
            StringBuilder lParam = new StringBuilder(capacity);
            SendMessage(hwnd, WM.GETTEXT, (uint)capacity, lParam);
            return lParam.ToString();
        }

        public static string GetHwndTitle(IntPtr hwnd)
        {
            StringBuilder lpString = new StringBuilder(GetHwndTitleLength(hwnd) + 1);
            GetWindowText(hwnd, lpString, lpString.Capacity);
            return lpString.ToString();
        }

        public static int GetHwndTitleLength(IntPtr hwnd)
        {
            return GetWindowTextLength(hwnd);
        }

        public static int GetMessageInt(IntPtr hwnd, WM msg)
        {
            return (int)SendMessage(hwnd, msg, 0, 0);
        }

        public static string GetMessageString(IntPtr hwnd, WM msg, uint param)
        {
            StringBuilder lParam = new StringBuilder(0x10000);
            SendMessage(hwnd, msg, param, lParam);
            return lParam.ToString();
        }

        public static bool SetHwndPos(IntPtr hwnd, int x, int y)
        {
            return SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOZORDER);
        }

        public static bool SetHwndSize(IntPtr hwnd, int w, int h)
        {
            return SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOZORDER);
        }

        public static void SetHwndText(IntPtr hwnd, string text)
        {
            SendMessage(hwnd, WM.SETTEXT, 0, text);
        }
    }
}
