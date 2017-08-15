using System;
using System.Runtime.InteropServices;
using System.Threading;


namespace HSTMarketSpeed.Routine
{
    public static class MSLoginHelper
    {
        #region Win32API

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpszClass, string lpszWindow);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, Int32 uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, String lParam);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, Int32 wParam, Int32 lParam);

        [DllImport("User32.Dll")]
        public static extern int GetWindowRect(IntPtr hWnd, out RECT rect);

        public const uint WM_SETTEXT = 0x000C;
        public const uint WM_LBUTTONDOWN = 0x201;
        public const uint WM_LBUTTONUP = 0x202;
        public const int GW_HWNDNEXT = 2;

        // GetWindowRect用型宣言
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #endregion


        public static void SendText(IntPtr hWnd, string text)
        {
            SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, text);
        }

        public static void PushButton(IntPtr hWnd, int x, int y)
        {
            int param = y << 16 | x;
            PostMessage(hWnd, WM_LBUTTONDOWN, 0, param);
            PostMessage(hWnd, WM_LBUTTONUP, 0, param);
        }

        public static IntPtr FindNextWindow(IntPtr hWndParent, string name)
        {
            return GetWindow(FindWindowEx(hWndParent, IntPtr.Zero, "Static", name), GW_HWNDNEXT);
        }

        public delegate void InitHandler();

        public static IntPtr WaitFindingTopWindow(string name)
        {
            return WaitFindingTopWindow(name, null);
        }

        public static IntPtr WaitFindingTopWindow(string name, InitHandler init)
        {
            IntPtr hWnd = IntPtr.Zero;
            do
            {
                if (init != null)
                {
                    init.Invoke();
                }
                hWnd = FindWindow(null, name);
                Thread.Sleep(100);
            } while (hWnd == IntPtr.Zero);
            return hWnd;
        }

        public static IntPtr WaitFindingChildWindow(IntPtr hWndParent, string clazz, string name)
        {
            IntPtr hWnd = IntPtr.Zero;
            do
            {
                hWnd = FindWindowEx(hWndParent, IntPtr.Zero, clazz, name);
                Thread.Sleep(100);
            } while (hWnd == IntPtr.Zero);
            return hWnd;
        }

    }
}
