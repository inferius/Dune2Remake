using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Dune2RemakeTest.Graphics;

namespace Dune2RemakeTest
{
    public class TerrainGenerator
    {
        public static readonly int OneFieldSize = 80;

        //public List<Terrain> Terrains { get; private set; }
        public SortedList<string, Terrain> Terrains { get; private set; }

        public TerrainGenerator(MainWindow w)
        {
            //Terrains = new List<Terrain>(400);
            Terrains = new SortedList<string, Terrain>(200);

            var cnt_w = w.Width / OneFieldSize;
            var cnt_h = w.Height / OneFieldSize;

            for (int i = 0; i < cnt_w; i++)
            {
                for (int j = 0; j < cnt_h; j++)
                {
                    var c = new Graphics.Terrain(new Point((double)(i * OneFieldSize), (double)(j * OneFieldSize)))
                    {
                        Width = OneFieldSize,
                        Height = OneFieldSize,
                        Background = new ImageBrush()
                        {
                            ImageSource = new BitmapImage(new Uri(@"Resources\Terrain\sand_e.png", UriKind.Relative)),
                            Stretch = Stretch.UniformToFill
                        },
                        X = i,
                        Y = j,
                        TerrainManager = this

                    };

                    c.MouseLeftButtonUp += (s, e) =>
                    {
                        w.SelectedUnit?.MoveTo(c);
                    };

                    if (i > 0) c.SetValue(Canvas.LeftProperty, (double)(i * OneFieldSize));
                    if (j > 0) c.SetValue(Canvas.TopProperty, (double)(j * OneFieldSize));
                    w.MainCanvas.Children.Add(c);
                    Terrains.Add($"{i};{j}", c);
                }
            }
        }

        public Terrain GetCanvasTerrainByBasicCoordinate(int x, int y)
        {
            return Terrains[$"{x};{y}"];
        }

        public Terrain GetCanvasTerrainByPoint(Point point)
        {
            var ter = Terrains.FirstOrDefault(item =>
                (item.Value.Position.X <= point.X && (item.Value.Position.X + (double) OneFieldSize) > point.X) &&
                (item.Value.Position.Y <= point.Y && (item.Value.Position.Y + (double) OneFieldSize) > point.Y)
                );
            
            return ter.Value;
        }
    }
}
