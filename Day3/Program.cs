using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day3
{
    class Program
    {
        
        static void Main(string[] args)
        {
            #region Part1
            int xDimension = 1000;
            int yDimension = 1000;
            using (var reader = new StreamReader("input.txt"))
            {
                var tarp = new int[xDimension, yDimension];
                int conflicts = 0;
                string line;
                var claimNum = 0;
                HashSet<int> completeClaim = new HashSet<int>();
                while( (line = reader.ReadLine()) != null)
                {
                    claimNum++;
                    line = line.Replace(" ", string.Empty);
                    var claimStart = line.Substring(line.IndexOf('@')+1, line.IndexOf(':') - line.IndexOf('@')-1).Split(',');
                    var xStart = Convert.ToInt32(claimStart[0]);
                    var yStart = Convert.ToInt32(claimStart[1]);

                    var size = line.Substring(line.IndexOf(":")+1).Split('x');
                    var xLength = Convert.ToInt32(size[0]);
                    var yLength = Convert.ToInt32(size[1]);
                    bool hasConflict = false;
                    for(int i = xStart; i < xLength+xStart; i++)
                    {
                        for(int j = yStart; j < yLength+yStart; j++)
                        {
                            if (tarp[i, j] == 0)
                            {
                                tarp[i, j] = claimNum;
                            }
                            else if (tarp[i,j] != -1)
                            {
                                completeClaim.Remove(tarp[i, j]);
                                hasConflict = true;
                                tarp[i, j] = -1;
                                conflicts++;
                            }
                            else
                            {
                                hasConflict = true;
                            }
                        }
                    }
                    if(!hasConflict)
                    {
                        completeClaim.Add(claimNum);
                    }
                    
                }
                Console.WriteLine("Number of Overlaps: " + conflicts);
                Console.WriteLine("Complete Claim: " + completeClaim.First());
            }

            Console.ReadLine();

            #endregion


        }
    }
}
