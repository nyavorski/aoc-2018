using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day6
{
    class Program
    {
        private class Point
        {
            public readonly int x;
            public readonly int y;

            public readonly char id;

            public Point(int x, int y, char id)
            {
                this.x = x;
                this.y = y;
                this.id = id;
            }
        }

        static void Main(string[] args)
        {
            int maxX = 0, maxY = 0, minX = 0, minY = 0;

            var points = new Dictionary<Point, int>();
            var c = 'A';
            using (var reader = new StreamReader("input.txt"))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Replace(" ", string.Empty);
                    var coords = line.Split(',').ToList();
                    var y = Convert.ToInt32(coords[0]);
                    var x = Convert.ToInt32(coords[1]);

                    points.Add(new Point(x, y, c), 0);
                    c++;
                }
            }

            minX = points.Keys.Min(k => k.x);
            maxX = points.Keys.Max(k => k.x);
            minY = points.Keys.Min(k => k.y);
            maxY = points.Keys.Max(k => k.y);

            
            for(int i = 0; i <= maxX; i++)
            {
                for(int j = 0; j <= maxY; j++)
                {
                    var refPoint = new Point(i, j, '.');
                    var d = 0;
                    var p = ClosestPoint(points.Keys.ToList(), refPoint, out d);
                    if(p != null)
                    {
                        points[p]++;
                        if(d == 0)
                        {
                            Console.Write(p.id);
                        }
                        else
                        {
                            // Console.Write('.');
                            Console.Write(p.id.ToString().ToLower());
                        }
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine("------------------------");

            foreach(var p in points)
            {
                Console.WriteLine(string.Format("{0}: {1}", p.Key.id, p.Value));
            }

            Console.WriteLine("-------------");
            foreach(var v in points.Keys.Where(k => k.x == minX || k.y == minY || k.x == maxX || k.y == maxY).ToList())
            {
                Console.WriteLine("Removing " + v.id);
                points.Remove(v);
            }
            
            foreach(var v in points.Keys)
            {
                Console.WriteLine(v.id + ": " + points[v]);
            }

            Console.WriteLine("Max: " + points.Values.Max());
        }

        private static Point ClosestPoint(List<Point> points, Point p, out int dist)
        {
            Point closest = null;
            var minDistance = -1;

            foreach(var pt in points)
            {
                var d = CalculateDistance(pt, p);

                //same point
                if(d == 0)
                {
                    dist = 0;
                    return pt;
                }

                //set baseline
                if(minDistance == -1)
                {
                    minDistance = d;
                    closest = pt;
                }
                //baseline already set
                else
                {
                    if(d == minDistance)
                    {
                        closest = null;
                    }
                    else if(d < minDistance)
                    {
                        minDistance = d;
                        closest = pt;
                    }
                }
            }

            dist = minDistance;
            return closest;

        }

        private static int CalculateDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
        }
    }
}
