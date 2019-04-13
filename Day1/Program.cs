using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1
{
    class Program
    {
        private static bool debugMode;
        static void Main(string[] args)
        {
            if (args.Count() > 1 && args[1] == "-v")
            {
                debugMode = true;
            }
            switch (args.FirstOrDefault())
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
        }

        private static void p1()
        {
            int frequency = 0;
            string currentLine = "";

            var file = new System.IO.StreamReader("input.txt");
            currentLine = file.ReadLine();
            while (currentLine != null)
            {
                var delta = Convert.ToInt32(currentLine);
                frequency += Convert.ToInt32(currentLine);
                if (debugMode)
                {
                    Console.WriteLine(String.Format("deltaFreq = {0} , new Frequency = {1}", delta, frequency));
                }

                currentLine = file.ReadLine();
            }

            Console.WriteLine("--All Frequencies Read--");
            Console.WriteLine("Final Frequency: " + frequency);
            Console.ReadLine();
        }

        private static void p2()
        {
            HashSet<int> frequencies = new HashSet<int>();
            int frequency = 0;
            frequencies.Add(frequency);
            bool duplicateFound = false;

            var currentLine = "";
            var file = new System.IO.StreamReader("input.txt");
            while(!duplicateFound)
            {
                currentLine = file.ReadLine();
                if(currentLine == null)
                {
                    file.DiscardBufferedData();
                    file.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                    currentLine = file.ReadLine();
                }
                frequency += Convert.ToInt32(currentLine);
                duplicateFound = !frequencies.Add(frequency);
            }

            Console.WriteLine("--Duplicate Frequency Encountered");
            Console.WriteLine("First Duplicate Frequency: " + frequency);
            Console.ReadLine();
            

        }
    }
}
