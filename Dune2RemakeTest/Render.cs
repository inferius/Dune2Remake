using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Dune2RemakeTest
{
    public class Render
    {
        private WriteableBitmap _buffer;
        private int _width;
        private int _height;
        public int Width { get { return _width; } set { _width = value; Redraw(); } }
        public int Height { get { return _height; } set { _height = value; Redraw(); } }

        public Render(Image ic, int width = 1024, int height = 768)
        {
            _buffer = BitmapFactory.New(width, height);
            Width = width;
            Height = height;
            ic.Source = _buffer;
        }

        public void AddLand(WriteableBitmap _land, Point _positionPoint)
        {
            //_buffer
        }

        public void Redraw()
        {
            
        }
    }
}
