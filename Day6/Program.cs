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
            public int xCoord;
            public int yCoord;
            public int distance;
            public string id;

            public Point(int x, int y, string id)
            {
                xCoord = x;
                yCoord = y;
                this.id = id;
            }

        }

        static void Main(string[] args)
        {
            int maxX = 0, maxY = 0, minX = 0, minY = 0;

            var points = new List<Point>();
            var pointCount = new Dictionary<string, int>();
            var c = 0;
            using (var reader = new StreamReader("input.txt"))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    c++;

                    line = line.Replace(" ", string.Empty);
                    var coords = line.Split(',').ToList();
                    var x = Convert.ToInt32(coords[0]);
                    var y = Convert.ToInt32(coords[1]);

                    maxX = Math.Max(maxX, x);
                    minX = Math.Min(minX, x);
                    maxY = Math.Max(maxY, y);
                    minY = Math.Min(minY, y);

                    points.Add(new Point(y, x, c.ToString()));
                }
            }

            var grid = new Point[maxY + 1, maxX + 1];

            foreach (var p in points)
            {
                grid[p.xCoord, p.yCoord] = p;
                pointCount.Add(p.id, 1);
            }

            for (int i = 0; i < maxY + 1; i++)
            {
                for (int j = 0; j < maxX + 1; j++)
                {
                    if (grid[i, j] == null)
                    {
                        Console.Write(".");
                    }
                    else
                    {
                        Console.Write(grid[i, j].id);
                    }
                }
                Console.WriteLine();
            }

            //Remove all keys w/ infinite bounds
            //for (int i = 0; i < maxX + 1; i++)
            //{
            //    pointCount.Remove(grid[i, 0].id);
            //    pointCount.Remove(grid[i, maxY].id);
            //}

            //for (int i = 0; i < maxY + 1; i++)
            //{
            //    pointCount.Remove(grid[0, i].id);
            //    pointCount.Remove(grid[maxX, i].id);
            //}

            Console.WriteLine(pointCount.Values.ToList().Max());
            Console.ReadLine();
        }

        
    }


}
