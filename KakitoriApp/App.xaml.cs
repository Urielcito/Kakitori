using KakitoriApp.Services;
using KakitoriApp.View;
using System;
using System.Windows;
using System.Windows.Interop;

namespace KakitoriApp
{
    public partial class App : System.Windows.Application
    {
        private MainWindow _mainWindow;
        private HotkeyService _hotkeyService;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _mainWindow = new MainWindow()
            {
                WindowStyle = WindowStyle.None,
                AllowsTransparency = true,
                Opacity = 0,
                ShowInTaskbar = false,
                ShowActivated = false
            };
            _mainWindow.SourceInitialized += (_, __) =>
            {
                _hotkeyService = new HotkeyService(_mainWindow);
                _hotkeyService.OnFullScreenHotkeyPressed += () =>
                {
                    ScreenshotService.CaptureFullScreen();
                };

                _hotkeyService.OnRegionHotkeyPressed += () =>
                {
                    ScreenshotService.CaptureRegion();
                };
                _mainWindow.Hide(); // ocultamos apenas carga, sin que el usuario vea nada
            };

            _mainWindow.Show(); // esto fuerza el HWND internamente
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _hotkeyService?.Dispose();
            base.OnExit(e);
        }
    }
}