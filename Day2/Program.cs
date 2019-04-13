using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day2
{
    class Program
    {
        static void Main(string[] args)
        {
            switch(args.FirstOrDefault())
            {
                case "-p1":
                    {
                        p1();
                        break;
                    }
                case "-p2":
                    {
                        p2();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
            Console.ReadLine();

        }

        private static void p1()
        {
            //var foo = "abbcccdddd";
            //var chars = foo.Distinct();
            //foreach(var v in chars)
            //{
            //    Console.WriteLine(v + " : " + foo.Where(c => c.Equals(v)).Count());
            //}
            //Console.ReadLine();
            //return;

            var doubleCount = 0;
            var tripleCount = 0;
            
            var file = new System.IO.StreamReader("input.txt");
            var currentLine = file.ReadLine();
            while (currentLine != null)
            {
                var uniqueChars = currentLine.Distinct();
                bool countDouble = false, countTriple = false;
                foreach (var v in uniqueChars)
                {
                    if(!countDouble)
                    {
                        countDouble = currentLine.Where(c => c.Equals(v)).Count() == 2;
                        if(countDouble)
                        {
                            doubleCount++;
                        }
                    }
                    if(!countTriple)
                    {
                        countTriple = currentLine.Where(c => c.Equals(v)).Count() == 3;
                        if (countTriple)
                        {
                            tripleCount++;
                        }
                    }
                    if(countDouble && countTriple)
                    {
                        break;
                    }
                }
                currentLine = file.ReadLine();
            }

            Console.WriteLine("--All Line Read--");
            Console.WriteLine("Doubles: " + doubleCount);
            Console.WriteLine("Triples: " + tripleCount);
            Console.WriteLine("Checksum: " + doubleCount*tripleCount);
        }

        private static void p2()
        {

            //var foo = "abcdefghijklmnop";
            //var bar = "abcdefghijklmnoz";
            //var zipped = foo.Zip(bar, (first, second) => first.ToString() + second);
            //var diffs = zipped.Where(s => s.Distinct().Count() == 2).Count();
            //return;


            bool done = false;
            var lines = System.IO.File.ReadAllLines("input.txt");
            for (int i = 0; i < lines.Count() - 1; i++)
            {
                for (int j = i + 1; j < lines.Count(); j++)
                {
                    var zipped = lines[i].Zip(lines[j], (first, second) => first.ToString() + second);
                    if (zipped.Where(s => s.Distinct().Count() == 2).Count() == 1)
                    {
                        var commonOnly = lines[i].Zip(lines[j], (first, second) => { if (first == second) return first.ToString(); else return ""; });
                        Console.WriteLine("Key is : " + string.Join("", commonOnly));
                        done = true;
                        break;
                    }
                }
                if (done)
                    break;
            }

        }
    }
}
