using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediakeysToBrowser
{
    class MessageHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumWindows(EnumWindowsCallback callback, IntPtr extraData);
        private delegate bool EnumWindowsCallback(IntPtr hwnd, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;


        private static string currentSearch = null;
        public static void SendSpaceBar(string windowNameContained)
        {
            currentSearch = windowNameContained;
            Process[] processes = Process.GetProcessesByName("chrome");
            if (processes.Length > 0)
            {
                for (int i = 0; i < processes.Length; i++)
                {
                    if (processes[i] != null)
                    {
                        if (processes[i].MainWindowHandle != null)
                        {
                            EnumWindows(new EnumWindowsCallback(EnumProc), new IntPtr(processes[i].Id));
                        }
                    }
                }
            }
        }

        private static bool EnumProc(IntPtr hWnd, IntPtr lParam)
        {
            int procId = lParam.ToInt32();
            StringBuilder wndName = new StringBuilder();
            wndName.Length = 1024;
            uint wndProcId = 0;

            GetWindowText(hWnd, wndName, wndName.Length);
            GetWindowThreadProcessId(hWnd, out wndProcId);
            
            if (procId == wndProcId && wndName.ToString().Contains(currentSearch))
            {
                IntPtr oldWindow = GetForegroundWindow();
                SetForegroundWindow(hWnd);
                SendMessage(hWnd, WM_KEYDOWN, new IntPtr((int)Keys.Space), IntPtr.Zero);
                SendMessage(hWnd, WM_KEYUP, new IntPtr((int)Keys.Space), IntPtr.Zero);
                SetForegroundWindow(oldWindow);
                return false;
            }
            return true;
        }
    }
}
