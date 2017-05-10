﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    internal enum WM : uint
    {
        ACTIVATE = 6,
        ACTIVATEAPP = 0x1c,
        AFXFIRST = 0x360,
        AFXLAST = 0x37f,
        APP = 0x8000,
        APPCOMMAND = 0x319,
        ASKCBFORMATNAME = 780,
        BN_CLICKED = 0xf5,
        CANCELJOURNAL = 0x4b,
        CANCELMODE = 0x1f,
        CAPTURECHANGED = 0x215,
        CHANGECBCHAIN = 0x30d,
        CHANGEUISTATE = 0x127,
        CHAR = 0x102,
        CHARTOITEM = 0x2f,
        CHILDACTIVATE = 0x22,
        CLEAR = 0x303,
        CLIPBOARDUPDATE = 0x31d,
        CLOSE = 0x10,
        COMMAND = 0x111,
        [Obsolete]
        COMMNOTIFY = 0x44,
        COMPACTING = 0x41,
        COMPAREITEM = 0x39,
        CONTEXTMENU = 0x7b,
        COPY = 0x301,
        COPYDATA = 0x4a,
        CPL_LAUNCH = 0x1400,
        CPL_LAUNCHED = 0x1401,
        CREATE = 1,
        CTLCOLORBTN = 0x135,
        CTLCOLORDLG = 310,
        CTLCOLOREDIT = 0x133,
        CTLCOLORLISTBOX = 0x134,
        CTLCOLORMSGBOX = 0x132,
        CTLCOLORSCROLLBAR = 0x137,
        CTLCOLORSTATIC = 0x138,
        CUT = 0x300,
        DEADCHAR = 0x103,
        DELETEITEM = 0x2d,
        DESTROY = 2,
        DESTROYCLIPBOARD = 0x307,
        DEVICECHANGE = 0x219,
        DEVMODECHANGE = 0x1b,
        DISPLAYCHANGE = 0x7e,
        DRAWCLIPBOARD = 0x308,
        DRAWITEM = 0x2b,
        DROPFILES = 0x233,
        DWMCOLORIZATIONCOLORCHANGED = 800,
        DWMCOMPOSITIONCHANGED = 0x31e,
        DWMNCRENDERINGCHANGED = 0x31f,
        DWMWINDOWMAXIMIZEDCHANGE = 0x321,
        ENABLE = 10,
        ENDSESSION = 0x16,
        ENTERIDLE = 0x121,
        ENTERMENULOOP = 0x211,
        ENTERSIZEMOVE = 0x231,
        ERASEBKGND = 20,
        EXITMENULOOP = 530,
        EXITSIZEMOVE = 0x232,
        FONTCHANGE = 0x1d,
        GETDLGCODE = 0x87,
        GETFONT = 0x31,
        GETHOTKEY = 0x33,
        GETICON = 0x7f,
        GETMINMAXINFO = 0x24,
        GETOBJECT = 0x3d,
        GETTEXT = 13,
        GETTEXTLENGTH = 14,
        GETTITLEBARINFOEX = 0x33f,
        HANDHELDFIRST = 0x358,
        HANDHELDLAST = 0x35f,
        HELP = 0x53,
        HOTKEY = 0x312,
        HSCROLL = 0x114,
        HSCROLLCLIPBOARD = 0x30e,
        ICONERASEBKGND = 0x27,
        IME_CHAR = 0x286,
        IME_COMPOSITION = 0x10f,
        IME_COMPOSITIONFULL = 0x284,
        IME_CONTROL = 0x283,
        IME_ENDCOMPOSITION = 270,
        IME_KEYDOWN = 0x290,
        IME_KEYLAST = 0x10f,
        IME_KEYUP = 0x291,
        IME_NOTIFY = 0x282,
        IME_REQUEST = 0x288,
        IME_SELECT = 0x285,
        IME_SETCONTEXT = 0x281,
        IME_STARTCOMPOSITION = 0x10d,
        INITDIALOG = 0x110,
        INITMENU = 0x116,
        INITMENUPOPUP = 0x117,
        INPUT = 0xff,
        INPUT_DEVICE_CHANGE = 0xfe,
        INPUTLANGCHANGE = 0x51,
        INPUTLANGCHANGEREQUEST = 80,
        KEYDOWN = 0x100,
        KEYFIRST = 0x100,
        KEYLAST = 0x109,
        KEYUP = 0x101,
        KILLFOCUS = 8,
        LBUTTONDBLCLK = 0x203,
        LBUTTONDOWN = 0x201,
        LBUTTONUP = 0x202,
        MBUTTONDBLCLK = 0x209,
        MBUTTONDOWN = 0x207,
        MBUTTONUP = 520,
        MDIACTIVATE = 0x222,
        MDICASCADE = 0x227,
        MDICREATE = 0x220,
        MDIDESTROY = 0x221,
        MDIGETACTIVE = 0x229,
        MDIICONARRANGE = 0x228,
        MDIMAXIMIZE = 0x225,
        MDINEXT = 0x224,
        MDIREFRESHMENU = 0x234,
        MDIRESTORE = 0x223,
        MDISETMENU = 560,
        MDITILE = 550,
        MEASUREITEM = 0x2c,
        MENUCHAR = 0x120,
        MENUCOMMAND = 0x126,
        MENUDRAG = 0x123,
        MENUGETOBJECT = 0x124,
        MENURBUTTONUP = 290,
        MENUSELECT = 0x11f,
        MOUSEACTIVATE = 0x21,
        MOUSEFIRST = 0x200,
        MOUSEHOVER = 0x2a1,
        MOUSEHWHEEL = 0x20e,
        MOUSELAST = 0x20e,
        MOUSELEAVE = 0x2a3,
        MOUSEMOVE = 0x200,
        MOUSEWHEEL = 0x20a,
        MOVE = 3,
        MOVING = 0x216,
        NCACTIVATE = 0x86,
        NCCALCSIZE = 0x83,
        NCCREATE = 0x81,
        NCDESTROY = 130,
        NCHITTEST = 0x84,
        NCLBUTTONDBLCLK = 0xa3,
        NCLBUTTONDOWN = 0xa1,
        NCLBUTTONUP = 0xa2,
        NCMBUTTONDBLCLK = 0xa9,
        NCMBUTTONDOWN = 0xa7,
        NCMBUTTONUP = 0xa8,
        NCMOUSEHOVER = 0x2a0,
        NCMOUSELEAVE = 0x2a2,
        NCMOUSEMOVE = 160,
        NCPAINT = 0x85,
        NCRBUTTONDBLCLK = 0xa6,
        NCRBUTTONDOWN = 0xa4,
        NCRBUTTONUP = 0xa5,
        NCXBUTTONDBLCLK = 0xad,
        NCXBUTTONDOWN = 0xab,
        NCXBUTTONUP = 0xac,
        NEXTDLGCTL = 40,
        NEXTMENU = 0x213,
        NOTIFY = 0x4e,
        NOTIFYFORMAT = 0x55,
        NULL = 0,
        PAINT = 15,
        PAINTCLIPBOARD = 0x309,
        PAINTICON = 0x26,
        PALETTECHANGED = 0x311,
        PALETTEISCHANGING = 0x310,
        PARENTNOTIFY = 0x210,
        PASTE = 770,
        PENWINFIRST = 0x380,
        PENWINLAST = 0x38f,
        [Obsolete]
        POWER = 0x48,
        POWERBROADCAST = 0x218,
        PRINT = 0x317,
        PRINTCLIENT = 0x318,
        QUERYDRAGICON = 0x37,
        QUERYENDSESSION = 0x11,
        QUERYNEWPALETTE = 0x30f,
        QUERYOPEN = 0x13,
        QUERYUISTATE = 0x129,
        QUEUESYNC = 0x23,
        QUIT = 0x12,
        RBUTTONDBLCLK = 0x206,
        RBUTTONDOWN = 0x204,
        RBUTTONUP = 0x205,
        RENDERALLFORMATS = 0x306,
        RENDERFORMAT = 0x305,
        SETCURSOR = 0x20,
        SETFOCUS = 7,
        SETFONT = 0x30,
        SETHOTKEY = 50,
        SETICON = 0x80,
        SETREDRAW = 11,
        SETTEXT = 12,
        SETTINGCHANGE = 0x1a,
        SHOWWINDOW = 0x18,
        SIZE = 5,
        SIZECLIPBOARD = 0x30b,
        SIZING = 0x214,
        SPOOLERSTATUS = 0x2a,
        STYLECHANGED = 0x7d,
        STYLECHANGING = 0x7c,
        SYNCPAINT = 0x88,
        SYSCHAR = 0x106,
        SYSCOLORCHANGE = 0x15,
        SYSCOMMAND = 0x112,
        SYSDEADCHAR = 0x107,
        SYSKEYDOWN = 260,
        SYSKEYUP = 0x105,
        SYSTIMER = 280,
        TABLET_FIRST = 0x2c0,
        TABLET_LAST = 0x2df,
        TCARD = 0x52,
        THEMECHANGED = 0x31a,
        TIMECHANGE = 30,
        TIMER = 0x113,
        UNDO = 0x304,
        UNICHAR = 0x109,
        UNINITMENUPOPUP = 0x125,
        UPDATEUISTATE = 0x128,
        USER = 0x400,
        USERCHANGED = 0x54,
        VKEYTOITEM = 0x2e,
        VSCROLL = 0x115,
        VSCROLLCLIPBOARD = 0x30a,
        WINDOWPOSCHANGED = 0x47,
        WINDOWPOSCHANGING = 70,
        WININICHANGE = 0x1a,
        WTSSESSION_CHANGE = 0x2b1,
        XBUTTONDBLCLK = 0x20d,
        XBUTTONDOWN = 0x20b,
        XBUTTONUP = 0x20c
    }
}
