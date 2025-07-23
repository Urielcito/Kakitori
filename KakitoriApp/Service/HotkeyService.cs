using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Forms;

namespace KakitoriApp.Services
{
    public class HotkeyService : IDisposable
    {
        private const int REGION_HOTKEY_ID = 9000;
        private const int FULLSCREEN_HOTKEY_ID = 9001;
        private WindowInteropHelper _helper;
        private IntPtr _handle;

        public event Action OnFullScreenHotkeyPressed;
        public event Action OnRegionHotkeyPressed;

        public HotkeyService(Window window)
        {
            _helper = new WindowInteropHelper(window);
            _handle = _helper.Handle;

            bool fullScreenCaptureHotkey = RegisterHotKey(_handle, FULLSCREEN_HOTKEY_ID, 0,(int)Keys.PrintScreen);
            bool regionCaptureHotkey = RegisterHotKey(_handle, REGION_HOTKEY_ID, MOD_CONTROL, (int)Keys.PrintScreen);

            HwndSource source = HwndSource.FromHwnd(_handle);
            source.AddHook(HwndHook);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;

            if (msg == WM_HOTKEY)
            {
                int id = wParam.ToInt32();

                if (id == FULLSCREEN_HOTKEY_ID)
                {
                    OnFullScreenHotkeyPressed?.Invoke();
                    handled = true;
                }
                else if (id == REGION_HOTKEY_ID)
                {
                    OnRegionHotkeyPressed?.Invoke();
                    handled = true;
                }
            }

            return IntPtr.Zero;
        }

        public void Dispose()
        {
            UnregisterHotKey(_handle, FULLSCREEN_HOTKEY_ID);
            UnregisterHotKey(_handle, REGION_HOTKEY_ID);
        }

        #region Win32 API

        private const int MOD_CONTROL = 0x0002;
        private const int MOD_SHIFT = 0x0004;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        #endregion
    }
}
