using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Win32APIs
{
    public class APIs
    {
        public const int WM_PRINT = 0x0317;
        public const int WM_PRINTCLIENT = 0x0318;

        public const int PRF_CHECKVISIBLE = 0x00000001;
        public const int PRF_NONCLIENT = 0x00000002;
        public const int PRF_CLIENT = 0x00000004;
        public const int PRF_ERASEBKGND = 0x00000008;
        public const int PRF_CHILDREN = 0x00000010;
        public const int PRF_OWNED = 0x00000020;

        public const int SW_SHOWNOACTIVATE = 4;
        public const int HWND_TOPMOST = -1;
        public const uint SWP_NOACTIVATE = 0x0010;

        public enum TernaryRasterOperations
        {
            BLACKNESS = 0x42,
            DSTINVERT = 0x550009,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            PATCOPY = 0xf00021,
            PATINVERT = 0x5a0049,
            PATPAINT = 0xfb0a09,
            SRCAND = 0x8800c6,
            SRCCOPY = 0xcc0020,
            SRCERASE = 0x440328,
            SRCINVERT = 0x660046,
            SRCPAINT = 0xee0086,
            WHITENESS = 0xff0062
        }

        [DllImport("USER32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT rc);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        static extern bool SetWindowPos(
             int hWnd,           // window handle
             int hWndInsertAfter,    // placement-order handle
             int X,          // horizontal position
             int Y,          // vertical position
             int cx,         // width
             int cy,         // height
             uint uFlags);       // window positioning flags

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("gdi32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static void ShowInactiveTopmost(IntPtr FormHandle)
        {
            ShowWindow(FormHandle, SW_SHOWNOACTIVATE);
        }

        public class ControlToImage
        {
            private static Bitmap GetImage(Graphics formg, int w, int h, int yOffset)
            {
                Bitmap image = new Bitmap(w, h - yOffset);
                Graphics graphics = Graphics.FromImage(image);
                IntPtr hdc = formg.GetHdc();
                IntPtr ptr2 = graphics.GetHdc();
                try
                {
                    APIs.BitBlt(ptr2, 0, 0, w, h - yOffset, hdc, 0, yOffset, APIs.TernaryRasterOperations.SRCCOPY);
                }
                catch (Exception exception1)
                {
                }
                finally
                {
                    try
                    {
                        formg.ReleaseHdc(hdc);
                    }
                    catch (Exception exception2)
                    {
                    }
                    try
                    {
                        graphics.ReleaseHdc(ptr2);
                    }
                    catch (Exception exception3)
                    {
                    }
                }
                return image;
            }

            public static Image GetControlScreenshot(System.Windows.Forms.Control control)
            {
                object x = new object();
                Bitmap bitmap = null;

                lock (control)
                {
                    if (control == null || control.Handle == IntPtr.Zero)
                        return (Image)bitmap;

                    if (control.Width < 1 || control.Height < 1) return (Image)bitmap;

                    if (bitmap != null)
                        bitmap.Dispose();

                    // preparing the bitmap.
                    bitmap = new Bitmap(control.Width, control.Height);

                    bitmap = GetImage(control.CreateGraphics(), control.Width, control.Height, 0);
                }

                return (Image)bitmap;
            }

            //public static Image CaptureControlImage(AxMSTSCLib.AxMsRdpClient2 ctrl)
            //{
            //    //Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height);

            //    //Graphics gBmp = Graphics.FromImage(bmp);

            //    //IntPtr dcBmp = gBmp.GetHdc();

            //    //SendMessage(ctrl.Handle, WM_PRINT, dcBmp, PRF_CLIENT | PRF_CHILDREN | PRF_NONCLIENT | PRF_OWNED);

            //    //gBmp.ReleaseHdc(dcBmp);

            //    Image ret = null;

            //    Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height); ;

            //    IntPtr p_hWnd = ctrl.Handle;
            //    IntPtr hWnd = APIs.FindWindowEx(p_hWnd, IntPtr.Zero, "UIMainClass", IntPtr.Zero);
            //    if (hWnd != IntPtr.Zero)
            //    {
            //        hWnd = APIs.FindWindowEx(hWnd, IntPtr.Zero, "UIContainerClass", IntPtr.Zero);

            //        if (hWnd != IntPtr.Zero)
            //        {
            //            hWnd = APIs.FindWindowEx(hWnd, IntPtr.Zero, "OPWindowClass", "Output Painter Window");

            //            if (hWnd != IntPtr.Zero)
            //            {
            //                //Graphics gBmp = Graphics.FromHwnd(hWnd);
            //                //APIs.RECT r = new APIs.RECT();
            //                //APIs.GetWindowRect(hWnd, out r);
            //                //ret = GetImage(gBmp, r.Right, r.Bottom, 0);

            //                ////IntPtr dcBmp = gBmp.GetHdc();

            //                ////IntPtr param = new IntPtr(APIs.PRF_CLIENT | APIs.PRF_CHILDREN | APIs.PRF_NONCLIENT);

            //                ////APIs.SendMessage(ctrl.Handle, APIs.WM_PRINT, dcBmp, param);

            //                ////gBmp.ReleaseHdc(dcBmp);

            //                ////ret = (Image)bmp;

            //                ////ScreenCapture sc = new ScreenCapture();
            //                ////ret = sc.CaptureWindow(hWnd);
            //            }
            //        }
            //    }

            //    return ret;
            //}
        }
    }
}
