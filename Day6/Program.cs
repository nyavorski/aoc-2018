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
            public readonly string id;

            public Point(int x, int y, string id)
            {
                this.x = x;
                this.y = y;
                this.id = id;
            }

            public override String ToString()
            {
                return id + " = " + x + "," + y;
            }
        }

        static void Main(string[] args)
        {
            int maxX = 0, maxY = 0, minX = 0, minY = 0;

            var points = new List<Point>();
            var infPoints = new HashSet<Point>();
            var areaMap = new Dictionary<string, int>();
            var c = 'A';
            using (var reader = new StreamReader("input.txt"))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Replace(" ", string.Empty);
                    var coords = line.Split(',').ToList();
                    var x = Convert.ToInt32(coords[0]);
                    var y = Convert.ToInt32(coords[1]);

                    points.Add(new Point(x, y, c.ToString()));
                    areaMap.Add(c.ToString(), 1);
                    c++;
                }
            }

            // points.ForEach(p => Console.WriteLine(p.ToString()));

            minX = points.Min(k => k.x);
            maxX = points.Max(k => k.x);
            minY = points.Min(k => k.y);
            maxY = points.Max(k => k.y);

            // Console.WriteLine(minX);
            // Console.WriteLine(maxX);
            // Console.WriteLine(minY);
            // Console.WriteLine(maxY);

            string[,] grid = new string[maxY+1,maxX+1];
            foreach(var v in points)
            {
                grid[v.y, v.x] = v.id;
            }


            // for(int i = 0; i <= maxY; i++)
            // {
            //     for(int j = 0; j <= maxX; j++)
            //     {
            //             if(grid[i,j] ==  null)
            //                 Console.Write('.');
            //             else
            //                 Console.Write(grid[i,j]);
            //     }
            //     Console.WriteLine();
            // }

            Console.WriteLine();
            
            for(int i = 0; i <= maxY; i++)
            {
                for(int j = 0; j <= maxX; j++)
                {
                    if(grid[i,j] == null)
                    {
                        var refPoint = new Point(j, i, ".");
                        var p = ClosestPoint(points, refPoint);
                        if(p != null)
                        {
                            areaMap[p.id]++;
                            grid[i,j] = p.id;
                        }
                        else
                        {
                            grid[i,j] = ".";
                        }
                    }

                }
            }

            // for(int i = 0; i <= maxY; i++)
            // {
            //     for(int j = 0; j <= maxX; j++)
            //     {
            //             if(grid[i,j] == null)
            //                 Console.Write('.');
            //             else
            //                 Console.Write(grid[i,j]);
            //     }
            //     Console.WriteLine();
            // }


            Console.WriteLine("------------------------");

            // foreach(var p in areaMap)
            // {
            //     Console.WriteLine(p.Key + ": " + p.Value);
            // }

            Console.WriteLine("-------------");

            //walk along the borders of the min / max coordinates to find all infinite regions
            for(int i = minY; i <= maxY; i ++)
            {
                areaMap.Remove(grid[i,minX]); //L
                areaMap.Remove(grid[i,maxX]); //R 
            }

            for(int i = minX; i <= maxX; i ++)
            {
                areaMap.Remove(grid[minY,i]); //T
                areaMap.Remove(grid[maxY,i]); //B
            }
            
            // foreach(var v in areaMap.Keys)
            // {
            //     Console.WriteLine(v + ": " + areaMap[v]);
            // }

            Console.WriteLine("Max: " + areaMap.Values.Max());
        }

        private static Point ClosestPoint(List<Point> points, Point p)
        {
            Point closest = null;
            var minDistance = -1;

            foreach(var pt in points)
            {
                var d = CalculateDistance(pt, p);

                //same point
                if(d == 0)
                {
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
            return closest;

        }

        private static int CalculateDistance(Point p1, Point p2)
        {
            return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
        }
    }
}
