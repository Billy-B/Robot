using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    [Flags]
    internal enum PositioningFlags
    {
        SWP_NOSIZE = 1,
        SWP_NOMOVE = 2,
        SWP_NOZORDER = 4,
        SWP_NOREDRAW = 8,
        SWP_NOACTIVATE = 0x10,
        SWP_DRAWFRAME = 0x20,
        SWP_FRAMECHANGED = 0x20,
        SWP_SHOWWINDOW = 0x40,
        SWP_HIDEWINDOW = 0x80,
        SWP_NOCOPYBITS = 0x100,
        SWP_NOOWNERZORDER = 0x200,
        SWP_NOREPOSITION = 0x200,
        SWP_NOSENDCHANGING = 0x400,
        SWP_DEFERERASE = 0x2000,
        SWP_ASYNCWINDOWPOS = 0x4000,
    }
}
