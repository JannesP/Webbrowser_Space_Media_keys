using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediakeysToBrowser
{
    class KeyHelper
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifier, int vk);
        [DllImport("user32.dll")]
        private static extern void UnregisterHotKey(IntPtr hWnd, int id);

        public const int WM_HOTKEY = 0x312;

        public static void InstallHook(IntPtr hWnd)
        {
            RegisterHotKey(hWnd, (int)Keys.MediaPlayPause, 0, (int)Keys.MediaPlayPause);
        }

        public static void RemoveHook(IntPtr hWnd)
        {
            UnregisterHotKey(hWnd, (int)Keys.MediaPlayPause);
        }

    }
}
