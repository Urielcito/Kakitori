using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using KakitoriApp.Util;

namespace KakitoriApp.View
{
    public partial class RegionSelectorWindow : Window
    {
        private System.Windows.Point startPoint;
        private System.Windows.Shapes.Rectangle rect;

        public Rect SelectedRegion { get; private set; }
        public bool SelectionMade { get; private set; } = false;

        public RegionSelectorWindow(Bitmap screenshot)
        {
            InitializeComponent();
            this.Cursor = System.Windows.Input.Cursors.Cross;

            BackgroundImage.Source = ImageConversionUtils.ConvertBitmapToImageSource(screenshot);

            rect = new System.Windows.Shapes.Rectangle
            {
                Stroke = System.Windows.Media.Brushes.White,              
                StrokeThickness = 1,                                       
                StrokeDashArray = new DoubleCollection() { 4, 2 }
            };
            DoubleAnimation dashOffsetAnimation = new DoubleAnimation
            {
                From = 0,
                To = 6,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            rect.BeginAnimation(System.Windows.Shapes.Shape.StrokeDashOffsetProperty, dashOffsetAnimation);
            CanvasOverlay.Children.Add(rect);

            this.MouseLeftButtonDown += Window_MouseLeftButtonDown;
            this.MouseMove += Window_MouseMove;
            this.MouseLeftButtonUp += Window_MouseLeftButtonUp;
        }


        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(this);
            Canvas.SetLeft(rect, startPoint.X);
            Canvas.SetTop(rect, startPoint.Y);
            rect.Width = 0;
            rect.Height = 0;
            rect.Visibility = Visibility.Visible;
            CaptureMouse();
        }

        private void Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                System.Windows.Point pos = e.GetPosition(this);

                double x = Math.Min(pos.X, startPoint.X);
                double y = Math.Min(pos.Y, startPoint.Y);
                double width = Math.Abs(pos.X - startPoint.X);
                double height = Math.Abs(pos.Y - startPoint.Y);

                Canvas.SetLeft(rect, x);
                Canvas.SetTop(rect, y);
                rect.Width = width;
                rect.Height = height;
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseCaptured)
            {
                ReleaseMouseCapture();
                SelectionMade = true;

                double x = Canvas.GetLeft(rect);
                double y = Canvas.GetTop(rect);
                double width = rect.Width;
                double height = rect.Height;
                var screenPos = this.PointToScreen(new System.Windows.Point(x, y));

                SelectedRegion = new Rect(screenPos.X, screenPos.Y, width, height);
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
