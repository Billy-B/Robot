using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class Window
    {
        private bool _animationsEnabled = true;

        private Window _parent;

        private Window(IntPtr hwnd)
        {
            this._HWnd = hwnd;
        }

        // <summary>
        // Bring this window to the foreground
        // </summary>
        public bool Activate()
        {
            return HWndUtil.SetForegroundWindow(_HWnd);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            Window other = obj as Window;
            return other != null && Equals(other);
        }

        public bool Equals(Window obj)
        {
            return this._HWnd == obj._HWnd;
        }

        public void Click(int xOffset, int yOffset)
        {
            uint coordsArg = (uint)((yOffset << 0x10) | xOffset);
            HWndUtil.SendMessage(this._HWnd, WM.LBUTTONDOWN, 0, coordsArg);
            HWndUtil.SendMessage(this._HWnd, WM.LBUTTONUP, 0, coordsArg);
        }

        public void Click(Point point)
        {
            Click(point.X, point.Y);
        }

        public Window GetChild(string cls, string title)
        {
            return new Window(HWndUtil.FindWindowEx(this._HWnd, IntPtr.Zero, cls, title));
        }

        public Window[] GetChildren()
        {
            IntPtr[] childPtrs = HWndUtil.EnumChildren(this._HWnd).ToArray();
            Window[] ret = new Window[childPtrs.Length];
            for (int i = 0; i < childPtrs.Length; i++)
            {
                ret[i] = new Window(childPtrs[i]);
            }
            return ret;
        }

        public override int GetHashCode()
        {
            return this._HWnd.GetHashCode();
        }

        public Window Parent
        {
            get
            {
                Window ret = _parent;
                if (ret == null)
                {
                    ret = new Window(HWndUtil.GetParent(this._HWnd));
                    _parent = ret;
                }
                return ret;
            }
        }

        public static Window GetWindowByTitle(string title)
        {
            return new Window(HWndUtil.GetHwndFromTitle(title));
        }

        public static Window GetWindowByClassName(string className)
        {
            return new Window(HWndUtil.GetHwndFromClass(className));
        }

        public static Window[] GetAllWindows()
        {
            IntPtr[] ptrs = HWndUtil.EnumHwnds().ToArray();
            Window[] ret = new Window[ptrs.Length];
            for (int i = 0; i < ptrs.Length; i++)
            {
                ret[i] = new Window(ptrs[i]);
            }
            return ret;
        }

        public static Window[] GetWindowsByProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            return processes.SelectMany(p => GetWindowsByProcess(p)).ToArray();
        }

        public static Window[] GetWindowsByProcess(Process proc)
        {
            return GetAllWindows().Where(w => w.Process.Id == proc.Id && w.Visible).ToArray();
        }

        public string ClassName
        {
            get
            {
                return HWndUtil.GetHwndClassName(this._HWnd);
            }
        }

        internal IntPtr _HWnd;

        public Point Location
        {
            get
            {
                return HWndUtil.GetHwndPos(this._HWnd);
            }
            set
            {
                if (State != WindowState.Normal)
                {
                    State = WindowState.Normal;
                }
                HWndUtil.SetHwndPos(this._HWnd, value.X, value.Y);
            }
        }

        private Bitmap captureScreen()
        {
            RECT rc;
            Robot.GetWindowRect(this._HWnd, out rc);

            Bitmap bmp = new Bitmap(rc.Width, rc.Height, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            Robot.PrintWindow(this._HWnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();
            return bmp;
        }

        public Bitmap GetImage()
        {
            if (State == WindowState.Minimized)
            {
                Point prev = Location;
                bool prevAnimationsEnabled = Robot.GlobalWindowAnimationsEnabled;
                if (prevAnimationsEnabled)
                {
                    Robot.disableAnimations();
                    Location = new Point(-9000, -9000);
                    Bitmap ret = captureScreen();
                    Location = prev;
                    State = WindowState.Minimized;
                    Robot.enableAnimations();
                    return ret;
                }
                else
                {
                    Location = new Point(-9000, -9000);
                    Bitmap ret = captureScreen();
                    Location = prev;
                    State = WindowState.Minimized;
                    return ret;
                }
            }
            else
            {
                return captureScreen();
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return HWndUtil.GetHwndSize(this._HWnd);
            }
            set
            {
                HWndUtil.SetHwndSize(this._HWnd, value.Width, value.Height);
            }
        }

        public bool Visible
        {
            get { return HWndUtil.IsWindowVisible(this._HWnd); }
        }

        public string Text
        {
            get
            {
                return HWndUtil.GetHwndText(this._HWnd);
            }
            set
            {
                HWndUtil.SetHwndText(this._HWnd, value);
            }
        }

        public string Title
        {
            get
            {
                return HWndUtil.GetHwndTitle(this._HWnd);
            }
            set
            {
                HWndUtil.SetWindowText(this._HWnd, value);
            }
        }

        public bool AnimationsEnabled
        {
            get
            {
                return _animationsEnabled;
            }
            set
            {
                if (value != _animationsEnabled)
                {
                    int attrVal = value ? 0 : 1;
                    int success = HWndUtil.DwmSetWindowAttribute(this._HWnd, 3, ref attrVal, 4);
                    _animationsEnabled = value;
                }
            }
        }

        public WindowState State
        {
            get
            {
                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                HWndUtil.GetWindowPlacement(this._HWnd, ref placement);
                return (WindowState)placement.ShowCmd;
            }
            set
            {
                ShowWindowCommands cmd;
                switch (value)
                {
                    case WindowState.Maximized:
                        cmd = ShowWindowCommands.MAXIMIZE;
                        break;
                    case WindowState.Minimized:
                        cmd = ShowWindowCommands.MINIMIZE;
                        break;
                    case WindowState.Normal:
                        cmd = ShowWindowCommands.SHOWNORMAL;
                        break;
                    default:
                        throw new ArgumentException();
                }
                HWndUtil.ShowWindow(this._HWnd, cmd);
            }
        }

        public Process Process
        {
            get
            {
                uint outId;
                uint id = HWndUtil.GetWindowThreadProcessId(this._HWnd, out outId);
                return Process.GetProcessById((int)outId);
            }
        }
    }
}
