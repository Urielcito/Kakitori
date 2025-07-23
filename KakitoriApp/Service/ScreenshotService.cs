using System.Drawing.Imaging;
using KakitoriApp.Util;

namespace KakitoriApp.Services
{
    public static class ScreenshotService
    {
        public static void CaptureRegion()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
                using Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(screenshot))
                {
                    g.CopyFromScreen(System.Drawing.Point.Empty, System.Drawing.Point.Empty, bounds.Size);
                }

                var selector = new KakitoriApp.View.RegionSelectorWindow(screenshot);
                bool? result = selector.ShowDialog();

                if (result == true && selector.SelectionMade)
                {
                    var rect = selector.SelectedRegion;
                    var captureBounds = new System.Drawing.Rectangle(
                        (int)rect.X,
                        (int)rect.Y,
                        (int)rect.Width,
                        (int)rect.Height
                    );

                    CaptureAndSave(captureBounds, "region.png");
                }
            });
        }
        public static void CaptureFullScreen()
        {
            Rectangle bounds = Screen.GetBounds(System.Drawing.Point.Empty);
            CaptureAndSave(bounds, "fullscreen.png");
        }
        public static void CaptureAndSave(Rectangle bounds, string path)
        {
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point(bounds.X, bounds.Y), System.Drawing.Point.Empty, bounds.Size);
                }

                bitmap.Save(path, ImageFormat.Png);

                // Copiar al portapapeles
                var bitmapSource = ImageConversionUtils.ConvertBitmapToBitmapSource(bitmap);
                System.Windows.Clipboard.SetImage(bitmapSource);
            }
        }
    }
}
