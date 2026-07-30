using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace InkCanvasForClass_Remastered.Helpers
{
    internal class ForegroundWindowInfo
    {
        //[DllImport("user32.dll")]
        //private static extern IntPtr GetForegroundWindow();

        //[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        //private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        //[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        //[DllImport("user32.dll")]
        //private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        //[StructLayout(LayoutKind.Sequential)]
        //public struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;

        //    public int Width => Right - Left;
        //    public int Height => Bottom - Top;
        //}

        public static string WindowTitle()
        {
            IntPtr foregroundWindowHandle = PInvoke.GetForegroundWindow();

            const int nChars = 256;
            StringBuilder windowTitle = new(nChars);
            _ = PInvoke.GetWindowText(new HWND(foregroundWindowHandle), new Span<char>(windowTitle.ToString().ToCharArray()));

            return windowTitle.ToString();
        }

        public static string WindowClassName()
        {
            IntPtr foregroundWindowHandle = PInvoke.GetForegroundWindow();

            const int nChars = 256;
            StringBuilder className = new(nChars);
            _ = PInvoke.GetClassName(new HWND(foregroundWindowHandle), new Span<char>(className.ToString().ToCharArray()));

            return className.ToString();
        }

        public static RECT WindowRect()
        {
            IntPtr foregroundWindowHandle = PInvoke.GetForegroundWindow();

            _ = PInvoke.GetWindowRect(new HWND(foregroundWindowHandle), out RECT windowRect);

            return windowRect;
        }

        public static string ProcessName()
        {
            IntPtr foregroundWindowHandle = PInvoke.GetForegroundWindow();
            _ = PInvoke.GetWindowThreadProcessId(new HWND(foregroundWindowHandle), out uint processId);

            try
            {
                Process process = Process.GetProcessById((int)processId);
                return process.ProcessName;
            }
            catch (ArgumentException)
            {
                // Process with the given ID not found
                return "Unknown";
            }
        }

        public static string ProcessPath()
        {
            IntPtr foregroundWindowHandle = PInvoke.GetForegroundWindow();
            _ = PInvoke.GetWindowThreadProcessId(new HWND(foregroundWindowHandle), out uint processId);

            try
            {
                Process process = Process.GetProcessById((int)processId);
                return process.MainModule.FileName;
            }
            catch
            {
                // Process with the given ID not found
                return "Unknown";
            }
        }
    }
}