using System.Windows;
using System.Windows.Controls;

namespace Dune2RemakeTest.Graphics
{
    public class Terrain : Canvas
    {
        public Point Position { get; private set; }
        public TerrainType Type { get; internal set; }
        public int X { get; internal set; }
        public int Y { get; internal set; }

        internal TerrainGenerator TerrainManager;

        public Terrain(Point position, TerrainType type = TerrainType.Sand)
        {
            Position = position;
            Type = type;

        }
    }

    public enum TerrainType
    {
        Underground,
        Air,
        Sand,
        Rock,
        HighRock,
        Spice,
        Pavement,
        Building
    }
}
