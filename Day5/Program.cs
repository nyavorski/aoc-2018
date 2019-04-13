using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt").Trim();

            List<string> badHombres = new List<string>();

            for (char c = 'a'; c <= 'z'; c++)
            {
                badHombres.Add(c.ToString() + c.ToString().ToUpper());
                badHombres.Add(c.ToString().ToUpper() + c.ToString());
            }

            Console.WriteLine(decompose(input, badHombres));

            Console.WriteLine("------------");

            var min = -1;

            for (char c = 'a'; c <= 'z'; c++)
            {
                var inputMk2 = input.Replace(c.ToString(), "");
                inputMk2 = inputMk2.Replace(c.ToString().ToUpper(), "");
                var minMk2 = decompose(inputMk2, badHombres);
                min = min == -1 ? minMk2 : Math.Min(min, minMk2);
            }

            Console.WriteLine(min);
            Console.ReadLine();


        }

        private static int decompose(string input, List<string> strippers)
        {
            var prevLength = -1;
                       
            while (prevLength != input.Length)
            {
                prevLength = input.Length;
                foreach (var v in strippers)
                {
                    input = input.Replace(v, "");
                }
            }
            return input.Length;

        }
    }
}
