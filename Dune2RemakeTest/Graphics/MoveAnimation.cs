using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Dune2RemakeTest.Graphics
{
    public class MoveAnimation
    {
        public static async void MoveTo(Unit unit, Terrain target, Maps.MapTemplate map)
        {
        }

        internal List<MovePath> MovesCalc()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Dijskrtuv algoritmus
        /// </summary>
        /// <param name="d">matice delek (int.MaxValue pokud hrana mezi uzly neexistuje)</param>
        /// <param name="from">uzel ze ktereho se hledaji nejkratsi cesty</param>
        /// <returns>strom predchudcu (z ciloveho uzlu znaci cestu do uzlu from)</returns>
        public static int[] doDijkstra(int[,] d, int from)
        {
            List<int> set = new List<int> {@from};

            bool[] closed = new bool[d.Length];
            int[] distances = new int[d.Length];
            for (int i = 0; i < d.Length; i++)
            {
                if (i != from)
                {
                    distances[i] = int.MaxValue;
                }
                else {
                    distances[i] = 0;
                }
            }


            int[] predecessors = new int[d.Length];
            predecessors[from] = -1;

            while (set.Count > 0)
            {
                //najdi nejblizsi dosazitelny uzel
                int minDistance = int.MaxValue;
                int node = -1;
                foreach (var i in set)
                {
                    if (distances[i] < minDistance)
                    {
                        minDistance = distances[i];
                        node = i;
                    }
                }

                set.Remove(node);
                closed[node] = true;

                //zkrat vzdalenosti
                for (int i = 0; i < d.Length; i++)
                {
                    //existuje tam hrana
                    if (d[node,i] != int.MaxValue)
                    {
                        if (!closed[i])
                        {
                            //cesta se zkrati
                            if (distances[node] + d[node,i] < distances[i])
                            {
                                distances[i] = distances[node] + d[node,i];
                                predecessors[i] = node;
                                set.Add(i); // prida uzel mezi kandidaty, pokud je jiz obsazen, nic se nestane
                            }
                        }
                    }
                }
            }
            return predecessors;
        }


    }

    internal struct MovePath
    {
        public Point Start;
        public Point End;

        public MovePath(double x, double y, double x2, double y2) : this(new Point(x, y), new Point(x2, y2))
        {
            
        }
        public MovePath(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
