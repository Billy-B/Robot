using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Robot
{
    public static class Robot
    {
        private static ANIMATIONINFO _animationInfo;

        private static readonly uint _size;

        static Robot()
        {
            _animationInfo = new ANIMATIONINFO();
            _size = (uint)Marshal.SizeOf(_animationInfo);
            _animationInfo.cbSize = _size;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, ulong dwExtraInfo);

        private const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hObject,int nXDest,int nYDest, int nWidth,int nHeight,IntPtr hObjectSource, int nXSrc,int nYSrc,int dwRop);
        
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hDC,int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd,IntPtr hDC);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int nFlags);

        public static int PrimaryScreenWidth
        {
            get { return _screenWidth; }
        }

        public static int PrimaryScreenHeight
        {
            get { return _screenHeight; }
        }

        public static bool GlobalWindowAnimationsEnabled
        {
            get
            {
                HWndUtil.SystemParametersInfo(SPI.SPI_GETANIMATION, _size, ref _animationInfo, SPIF.None);
                return _animationInfo.iMinAnimate != 0;
            }
            set
            {
                if (value != GlobalWindowAnimationsEnabled)
                {
                    _animationInfo.iMinAnimate = value ? 1 : 0;
                    HWndUtil.SystemParametersInfo(SPI.SPI_SETANIMATION, _size, ref _animationInfo, SPIF.SPIF_SENDCHANGE);
                }
            }
        }

        internal static void disableAnimations()
        {
            _animationInfo.iMinAnimate = 0;
            HWndUtil.SystemParametersInfo(SPI.SPI_SETANIMATION, _size, ref _animationInfo, SPIF.SPIF_SENDCHANGE);
        }

        internal static void enableAnimations()
        {
            _animationInfo.iMinAnimate = 1;
            HWndUtil.SystemParametersInfo(SPI.SPI_SETANIMATION, _size, ref _animationInfo, SPIF.SPIF_SENDCHANGE);
        }

        /*public static void SetMinimizeMaximizeAnimation(bool status)
        {
            WindowHelpers.SystemParametersInfo(SPI.SPI_GETANIMATION, _size, ref _animationInfo, SPIF.None);

            if (animationInfo.IMinAnimate != status)
            {
                animationInfo.IMinAnimate = status;
                WindowHelpers.SystemParametersInfo(SPI.SPI_SETANIMATION, ANIMATIONINFO.GetSize(),
                 ref animationInfo, SPIF.SPIF_SENDCHANGE);
            }
        }*/

        private static readonly int _screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        private static readonly int _screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
        private static readonly int _scale_x = ushort.MaxValue / _screenWidth;
        private static readonly int _scale_y = ushort.MaxValue / _screenHeight;

        const uint KEYEVENTF_KEYUP = 0x0002;

        #region LUTs

        private static VirtualKey[] _charMapper = 
        {
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.VK_TAB,
            VirtualKey.VK_RETURN,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.Undefined,
            VirtualKey.VK_SPACE,
            VirtualKey.K_1,
            VirtualKey.VK_OEM_7,
            VirtualKey.K_3,
            VirtualKey.K_4,
            VirtualKey.K_5,
            VirtualKey.K_7,
            VirtualKey.VK_OEM_7,
            VirtualKey.K_9,
            VirtualKey.K_0,
            VirtualKey.K_8,
            VirtualKey.VK_OEM_PLUS,
            VirtualKey.VK_OEM_COMMA,
            VirtualKey.VK_OEM_MINUS,
            VirtualKey.VK_OEM_PERIOD,
            VirtualKey.VK_OEM_2,
            VirtualKey.K_0,
            VirtualKey.K_1,
            VirtualKey.K_2,
            VirtualKey.K_3,
            VirtualKey.K_4,
            VirtualKey.K_5,
            VirtualKey.K_6,
            VirtualKey.K_7,
            VirtualKey.K_8,
            VirtualKey.K_9,
            VirtualKey.VK_OEM_1,
            VirtualKey.VK_OEM_1,
            VirtualKey.VK_OEM_COMMA,
            VirtualKey.VK_OEM_PLUS,
            VirtualKey.VK_OEM_PERIOD,
            VirtualKey.VK_OEM_2,
            VirtualKey.K_2,
            VirtualKey.K_A,
            VirtualKey.K_B,
            VirtualKey.K_C,
            VirtualKey.K_D,
            VirtualKey.K_E,
            VirtualKey.K_F,
            VirtualKey.K_G,
            VirtualKey.K_H,
            VirtualKey.K_I,
            VirtualKey.K_J,
            VirtualKey.K_K,
            VirtualKey.K_L,
            VirtualKey.K_M,
            VirtualKey.K_N,
            VirtualKey.K_O,
            VirtualKey.K_P,
            VirtualKey.K_Q,
            VirtualKey.K_R,
            VirtualKey.K_S,
            VirtualKey.K_T,
            VirtualKey.K_U,
            VirtualKey.K_V,
            VirtualKey.K_W,
            VirtualKey.K_X,
            VirtualKey.K_Y,
            VirtualKey.K_Z,
            VirtualKey.VK_OEM_4,
            VirtualKey.VK_OEM_5,
            VirtualKey.VK_OEM_6,
            VirtualKey.K_6,
            VirtualKey.VK_OEM_MINUS,
            VirtualKey.VK_OEM_3,
            VirtualKey.K_A,
            VirtualKey.K_B,
            VirtualKey.K_C,
            VirtualKey.K_D,
            VirtualKey.K_E,
            VirtualKey.K_F,
            VirtualKey.K_G,
            VirtualKey.K_H,
            VirtualKey.K_I,
            VirtualKey.K_J,
            VirtualKey.K_K,
            VirtualKey.K_L,
            VirtualKey.K_M,
            VirtualKey.K_N,
            VirtualKey.K_O,
            VirtualKey.K_P,
            VirtualKey.K_Q,
            VirtualKey.K_R,
            VirtualKey.K_S,
            VirtualKey.K_T,
            VirtualKey.K_U,
            VirtualKey.K_V,
            VirtualKey.K_W,
            VirtualKey.K_X,
            VirtualKey.K_Y,
            VirtualKey.K_Z,
            VirtualKey.VK_OEM_4,
            VirtualKey.VK_OEM_5,
            VirtualKey.VK_OEM_6,
            VirtualKey.VK_OEM_3,
            VirtualKey.Undefined
        };

        private static bool[] _charNeedsShiftMapper =
        {
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            true,
            true,
            true,
            false,
            true,
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            true,
            false,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            true,
            false,
            false,
            false,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            true,
            false
        };

        #endregion

        public static void SetCursorPosition(int xPos, int yPos)
        {
            mouse_event(0x8001, (uint)(xPos * _scale_x), (uint)(yPos * _scale_y), 0, 0);
        }

        public static void LeftClick(int xPos, int yPos)
        {
            mouse_event(0x8001, (uint)(xPos * _scale_x), (uint)(yPos * _scale_y), 0, 0);
            mouse_event(0x0002, 0, 0, 0, 0);
            mouse_event(0x0004, 0, 0, 0, 0);
        }

        public static void RightClick(int xPos, int yPos)
        {
            mouse_event(0x8001, (uint)(xPos * _scale_x), (uint)(yPos * _scale_y), 0, 0);
            mouse_event(0x0008, 0, 0, 0, 0);
            mouse_event(0x0010, 0, 0, 0, 0);
        }

        public static void Delay(int millisecondsDelay)
        {
            Task.Delay(millisecondsDelay);
        }

        public static void TypeString(string s)
        {
            foreach (char c in s)
            {
                KeyboardInputChar(c);
            }
        }

        public static void TypeLine(string s)
        {
            TypeString(s);
            KeyboardInputChar('\n');
        }

        public static void PressAndReleaseKey(VirtualKey key)
        {
            PressKey(key);
            ReleaseKey(key);
        }

        public static void PressKey(VirtualKey key)
        {
            keybd_event((byte)key, 0, 0, 0);
        }

        public static void ReleaseKey(VirtualKey key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_KEYUP, 0);
        }

        public static void KeyboardInputChar(char c)
        {
            VirtualKey mappedKey = _charMapper[c];
            if (mappedKey == VirtualKey.Undefined)
            {
                throw new ArgumentException("Character " + c + " cannot be mapped to keyboard input.");
            }
            if (_charNeedsShiftMapper[c])
            {
                PressKey(VirtualKey.VK_SHIFT);
                PressAndReleaseKey(mappedKey);
                ReleaseKey(VirtualKey.VK_SHIFT);
            }
            else
            {
                PressAndReleaseKey(mappedKey);
            }
        }

        public static Bitmap GetScreenCapture()
        {
            IntPtr windowHandle = GetDesktopWindow();
            IntPtr hdcSrc = GetWindowDC(windowHandle);
            RECT windowRect = new RECT();
            GetWindowRect(windowHandle, out windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            return GetScreenCapture(0, 0, width, height);
        }

        public static Bitmap GetScreenCapture(Rectangle rect)
        {
            return GetScreenCapture(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Bitmap GetScreenCapture(Point location, Size size)
        {
            return GetScreenCapture(location.X, location.Y, size.Width, size.Height);
        }

        public static Bitmap GetScreenCapture(int x, int y, int width, int height)
        {
            IntPtr windowHandle = GetDesktopWindow();
            IntPtr hdcSrc = GetWindowDC(windowHandle);
            IntPtr hdcDest = CreateCompatibleDC(hdcSrc);
            IntPtr hBitmap = CreateCompatibleBitmap(hdcSrc, width, height);
            IntPtr hOld = SelectObject(hdcDest, hBitmap);
            BitBlt(hdcDest, 0, 0, width, height, hdcSrc, x, y, SRCCOPY);
            SelectObject(hdcDest, hOld);
            DeleteDC(hdcDest);
            ReleaseDC(windowHandle, hdcSrc);
            Bitmap img = Image.FromHbitmap(hBitmap);
            DeleteObject(hBitmap);
            return img;
        }
    }
}
