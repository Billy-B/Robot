using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct ANIMATIONINFO
    {
        public uint cbSize;
        public int iMinAnimate;
    }
}
