using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TerminalWorkbench.Presentation
{
    public class PictureBox : Control
    {
        public event PaintEventHandler Paint;
        internal void Draw(Graphics graphics) => Paint?.Invoke(this, new PaintEventArgs(graphics));
    }
    public delegate void PaintEventHandler(object sender, PaintEventArgs e);
    public class PaintEventArgs : EventArgs
    {
        public Graphics Graphics
        {
            get;
        }
        public PaintEventArgs(Graphics graphics)
        {
            Graphics = graphics;
        }
    }
    internal sealed class DrawingSurface : FrameworkElement
    {
        private readonly PictureBox model;
        public DrawingSurface(PictureBox model)
        {
            this.model = model;
            MouseDown += (_, e) => Mouse(0, e);
            MouseMove += (_, e) => Mouse(1, e);
            MouseUp += (_, e) => Mouse(2, e);
            SizeChanged += (_, e) =>
            {
                model.Size = new System.Drawing.Size(Math.Max(1, (int)e.NewSize.Width), Math.Max(1, (int)e.NewSize.Height));
                InvalidateVisual();
            };
        }
        private void Mouse(int type, System.Windows.Input.MouseEventArgs e)
        {
            var point = e.GetPosition(this);
            model.RaiseMouse(type, new MouseEventArgs
            {
                X = (int)point.X,
                Y = (int)point.Y,
                Button = e.LeftButton == MouseButtonState.Pressed ? MouseButtons.Left : e.RightButton == MouseButtonState.Pressed ? MouseButtons.Right : MouseButtons.None
            });
            if (model.Cursor is System.Windows.Input.Cursor cursor)
                Cursor = cursor;
        }
        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);
            int width = Math.Max(1, (int)ActualWidth), height = Math.Max(1, (int)ActualHeight);
            using var bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(model.BackColor.IsEmpty ? System.Drawing.Color.Black : model.BackColor);
                model.Draw(graphics);
            }
            var data = bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            try
            {
                var source = BitmapSource.Create(width, height, 96, 96, PixelFormats.Pbgra32, null, data.Scan0, data.Stride * height, data.Stride);
                context.DrawImage(source, new Rect(0, 0, ActualWidth, ActualHeight));
            }
            finally { bitmap.UnlockBits(data); }
        }
    }
    public sealed class AxTChart : Control
    {
        private readonly TrendSeries series;
        public AxTChart()
        {
            series = new TrendSeries(this);
        }
        public int SeriesCount => 1;
        public TrendSeries Series(int index)
        {
            if (index != 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            return series;
        }
    }
    public sealed class TrendSeries
    {
        private readonly AxTChart chart;
        public string Title { get; set; } = string.Empty;
        internal List<System.Windows.Point> Points { get; } = new List<System.Windows.Point>();
        internal TrendSeries(AxTChart chart)
        {
            this.chart = chart;
        }
        public void Clear()
        {
            Points.Clear();
            chart.Invalidate();
        }
        public void AddXY(double x, double y, string label, int color)
        {
            Points.Add(new System.Windows.Point(x, y));
            chart.Invalidate();
        }
    }
    internal sealed class TrendSurface : FrameworkElement
    {
        private readonly AxTChart model;
        private double left;
        private double right = 1;
        private double dragX;
        private bool panning;
        public TrendSurface(AxTChart model)
        {
            this.model = model;
            ToolTip = "滚轮缩放；右键拖动平移；双击复位。";
            MouseWheel += (_, e) =>
            {
                if (model.Series(0).Points.Count < 2)
                    return;
                double anchor = Math.Clamp((e.GetPosition(this).X - 50) / Math.Max(1, ActualWidth - 70), 0, 1);
                double oldSpan = right - left;
                double span = Math.Clamp(oldSpan * (e.Delta > 0 ? 0.8 : 1.25), 1.0 / Math.Max(1, model.Series(0).Points.Count - 1), 1);
                left = Math.Clamp(left + anchor * (oldSpan - span), 0, 1 - span);
                right = left + span;
                InvalidateVisual();
                e.Handled = true;
            };
            MouseDown += (_, e) =>
            {
                if (e.ClickCount == 2)
                {
                    left = 0;
                    right = 1;
                    InvalidateVisual();
                }
                if (e.ChangedButton != MouseButton.Right)
                    return;
                dragX = e.GetPosition(this).X;
                panning = true;
                CaptureMouse();
            };
            MouseMove += (_, e) =>
            {
                if (!panning)
                    return;
                double x = e.GetPosition(this).X;
                double span = right - left;
                left = Math.Clamp(left - (x - dragX) / Math.Max(1, ActualWidth - 70) * span, 0, 1 - span);
                right = left + span;
                dragX = x;
                InvalidateVisual();
            };
            MouseUp += (_, _) => { panning = false; ReleaseMouseCapture(); };
        }
        protected override void OnRender(DrawingContext context)
        {
            base.OnRender(context);
            context.DrawRectangle(System.Windows.Media.Brushes.White, null, new Rect(0, 0, ActualWidth, ActualHeight));
            var points = model.Series(0).Points;
            if (points.Count == 0)
                return;
            double fullStart = points[0].X;
            double fullSpan = Math.Max(1, points[points.Count - 1].X - fullStart);
            double minX = fullStart + fullSpan * left, maxX = fullStart + fullSpan * right;
            double minY = double.PositiveInfinity, maxY = double.NegativeInfinity;
            foreach (var point in points)
            {
                if (point.X < minX || point.X > maxX)
                    continue;
                minY = Math.Min(minY, point.Y);
                maxY = Math.Max(maxY, point.Y);
            }
            double rangeX = Math.Max(1, maxX - minX), rangeY = Math.Max(0.001, maxY - minY);
            double width = Math.Max(1, ActualWidth - 70), height = Math.Max(1, ActualHeight - 60);
            var geometry = new StreamGeometry();
            using (var path = geometry.Open())
            {
                bool started = false;
                for (int i = 0; i < points.Count; i++)
                {
                    if (points[i].X < minX || points[i].X > maxX)
                        continue;
                    var point = new System.Windows.Point(50 + (points[i].X - minX) / rangeX * width, 20 + (maxY - points[i].Y) / rangeY * height);
                    if (!started)
                    {
                        path.BeginFigure(point, false, false);
                        started = true;
                    }
                    else
                        path.LineTo(point, true, false);
                }
            }
            context.DrawGeometry(null, new System.Windows.Media.Pen(System.Windows.Media.Brushes.Teal, 1.2), geometry);
            var axis = new System.Windows.Media.Pen(System.Windows.Media.Brushes.SlateGray, 1);
            context.DrawLine(axis, new System.Windows.Point(50, 20), new System.Windows.Point(50, height + 20));
            context.DrawLine(axis, new System.Windows.Point(50, height + 20), new System.Windows.Point(width + 50, height + 20));
            for (int tick = 0; tick <= 4; tick++)
            {
                double fraction = tick / 4.0;
                var y = new System.Windows.Point(4, 15 + height * fraction);
                var x = new System.Windows.Point(42 + width * fraction, height + 27);
                Label(context, (maxY - rangeY * fraction).ToString("G5"), y);
                Label(context, (minX + rangeX * fraction).ToString("G5"), x);
            }
        }
        private void Label(DrawingContext context, string text, System.Windows.Point point)
        {
            var formatted = new FormattedText(text, System.Globalization.CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                new Typeface("Consolas"), 11, System.Windows.Media.Brushes.SlateGray, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            context.DrawText(formatted, point);
        }
    }
}
