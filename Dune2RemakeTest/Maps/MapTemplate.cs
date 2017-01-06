using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Dune2RemakeTest.Maps
{
    public class MapTemplate
    {
        public Point MapSize { get; private set; }
        public Dictionary<Point, MapElement> MapElements { get; private set; } = new Dictionary<Point, MapElement>();

        public MapElement GetMapElement(Point position)
        {
            return MapElements.Where(item => item.Key == position).Select(item => item.Value).FirstOrDefault();
        }

        public int[,] GetMapPath()
        {
            int[,] r = new int[(int) MapSize.X,(int)MapSize.Y];

            for (var x = 0; x < MapSize.X; x++)
            {
                for (var y = 0; y < MapSize.Y; y++)
                {
                    //r[x]
                }
            }

            return r;
        }
    }
}
