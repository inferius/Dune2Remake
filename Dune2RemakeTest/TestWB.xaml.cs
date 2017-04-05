using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dune2RemakeTest
{
    /// <summary>
    /// Interakční logika pro TestWB.xaml
    /// </summary>
    public partial class TestWB : Window
    {
        public TestWB()
        {
            InitializeComponent();
            InitGraphics();
        }

        private void InitGraphics()
        {
            var writeableBmp = BitmapFactory.New(622, 900);
            TestWBImage.Source = writeableBmp;

            using (writeableBmp.GetBitmapContext())
            {

                // Load an image from the calling Assembly's resources via the relative path
                writeableBmp = BitmapFactory.New(32, 32).FromByteArray(System.IO.File.ReadAllBytes(@"E:\Documents\Dokumenty\GitHubRepo\Dune2Remake\Dune2RemakeTest\Resources\Units\tank_body_e.png"));
                // Clear the WriteableBitmap with white color
                //writeableBmp.Clear(Colors.White);

                // Set the pixel at P(10, 13) to black
                writeableBmp.SetPixel(10, 13, Colors.Black);

                // Get the color of the pixel at P(30, 43)
                Color color = writeableBmp.GetPixel(30, 43);

                // Green line from P1(1, 2) to P2(30, 40)
                writeableBmp.DrawLine(500, 2, 500, 1200, Colors.Green);

                // Line from P1(1, 2) to P2(30, 40) using the fastest draw line method 
                int[] pixels = new[] { 1, 2, 30, 40 };
                int w = writeableBmp.PixelWidth;
                int h = writeableBmp.PixelHeight;
                //WriteableBitmapExtensions.DrawLine(writeableBmp.GetBitmapContext, w, h, 1, 2, 30, 40, Colors.Beige);

                // Blue anti-aliased line from P1(10, 20) to P2(50, 70) with a stroke of 5
                writeableBmp.DrawLineAa(10, 20, 50, 70, Colors.Blue, 5);

                // Black triangle with the points P1(10, 5), P2(20, 40) and P3(30, 10)
                writeableBmp.DrawTriangle(10, 5, 20, 40, 30, 10, Colors.Black);

                // Red rectangle from the point P1(2, 4) that is 10px wide and 6px high
                writeableBmp.DrawRectangle(2, 4, 12, 10, Colors.Red);

                // Filled blue ellipse with the center point P1(2, 2) that is 8px wide and 5px high
                writeableBmp.FillEllipseCentered(2, 2, 8, 5, Colors.Blue);

                // Closed green polyline with P1(10, 5), P2(20, 40), P3(30, 30) and P4(7, 8)
                int[] p = new int[] { 10, 5, 20, 40, 30, 30, 7, 8, 10, 5 };
                writeableBmp.DrawPolyline(p, Colors.Green);

                // Cubic Beziér curve from P1(5, 5) to P4(20, 7) 
                // with the control points P2(10, 15) and P3(15, 0)
                writeableBmp.DrawBezier(5, 5, 10, 15, 15, 0, 20, 7, Colors.Purple);

                // Cardinal spline with a tension of 0.5 
                // through the points P1(10, 5), P2(20, 40) and P3(30, 30)
                int[] pts = new int[] { 10, 5, 20, 40, 30, 30 };
                writeableBmp.DrawCurve(pts, 0.5f, Colors.Yellow);

                // A filled Cardinal spline with a tension of 0.5 
                // through the points P1(10, 5), P2(20, 40) and P3(30, 30) 
                writeableBmp.FillCurveClosed(pts, 0.5f, Colors.Green);

                // Blit a bitmap using the additive blend mode at P1(10, 10)
                //writeableBmp.Blit(new Point(10, 10), bitmap, sourceRect, Colors.White, WriteableBitmapExtensions.BlendMode.Additive);

                // Override all pixels with a function that changes the color based on the coordinate
                //writeableBmp.ForEach((x, y, color) => Color.FromArgb(color.A, (byte)(color.R / 2), (byte)(x * y), 100));

            } // Invalidate and present in the Dispose call


        }
    }
}
