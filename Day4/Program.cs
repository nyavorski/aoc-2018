using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Day4
{
    class Program
    {

        private class Guard
        {
            public int id;
            public int totalMinutesAsleep;
            public int[] minuteSleepTracker = new int[60];
        }

        private class LineData
        {
            public DateTime date;
            public string[] log;

            public LineData(DateTime d, string[] info)
            {
                date = d;
                log = info;
            }
        }

        static void Main(string[] args)
        {
            List<LineData> list = new List<LineData>();

            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();

            using (var reader = new StreamReader("input.txt"))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {

                    var dateEndIndex = line.IndexOf(']');
                    var chunk = line.Split(']');
                    var date = DateTime.ParseExact(chunk[0].Substring(1), "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
                    list.Add(new LineData(date, chunk[1].Trim().Split(' ')));
                }
            }

            list = list.OrderBy(l => l.date).ToList();

            Guard currentGuard = null;
            int startSleepMinute = 99;

            foreach (var v in list)
            {
                //var date = v.Key.Date;
                var minute = v.date.Minute;
                var log = v.log;

                if (log.Count() > 2)
                {
                    var id = Convert.ToInt32(log[1].Substring(1));

                    if(!guards.ContainsKey(id))
                    {
                        currentGuard = new Guard();
                        currentGuard.id = id;
                        guards.Add(id, currentGuard);
                    }
                    else
                    {
                        currentGuard = guards[id];
                    }
                }
                else if (log[0] == "falls")
                {
                    startSleepMinute = minute;

                }
                else if (log[0] == "wakes")
                {
                    for(int i = startSleepMinute; i < minute; i++)
                    {
                        currentGuard.minuteSleepTracker[i]++;
                    }
                    currentGuard.totalMinutesAsleep += minute - startSleepMinute;
                    //Console.WriteLine(string.Format("GUARD #{0} SLEPT FROM {1} to {2}, A SPAN OF {3} MINUTES", currentGuard.id, startSleepMinute, minute, minute - startSleepMinute));
                }
            }

            var sleepyGuard = guards.OrderBy(x => x.Value.totalMinutesAsleep).Last().Value;
            var minutesSlept = sleepyGuard.totalMinutesAsleep;
            
            var sleepyMinute = sleepyGuard.minuteSleepTracker.ToList().IndexOf(sleepyGuard.minuteSleepTracker.ToList().Max());
            Console.WriteLine(string.Format("Guard #{0} slept for {1} minutes, sleeping most during minute {2}", sleepyGuard.id, minutesSlept, sleepyMinute));
            Console.WriteLine("Key1 = " + sleepyGuard.id * sleepyMinute);

            int maxCount = 0;
            int maxMinute = -1;
            int maxId = -1;

            foreach(var v in guards)
            {
                var guard = v.Value;
                for(int i = 0; i < 60; i++)
                {
                    if(guard.minuteSleepTracker[i] > maxCount)
                    {
                        maxCount = guard.minuteSleepTracker[i];
                        maxMinute = i;
                        maxId = guard.id;
                    }
                }         
            }

            Console.WriteLine(string.Format("Key2 = {0}*{1} = {2}", maxId, maxMinute, maxId*maxMinute));

            Console.ReadLine();

        }
    }
}
