using System.Windows.Controls;
using System.Windows.Media;

namespace Dune2RemakeTest.Graphics
{
    class GameButton : Canvas
    {
        ImageBrush _base = new ImageBrush();
        ImageBrush _mouseDown = new ImageBrush();
        ImageBrush _mouseUp = new ImageBrush();
        public GameButton() : base()
        {
            
        }
    }
}
