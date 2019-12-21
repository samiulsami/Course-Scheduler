using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUB_Course_Scheduler
{
    public class Section
    {
        public struct Time{
            public string day;
            public int from;
            public int to;
            public string room;
        }
        public int capacity { get; set; }
        public int count { get; set; }
        public string courseName { get; set; }
        public string section { get; set; }
        public List<Time> times { get; set; }
        public Section(int capacity, int count, string courseName, string section)
        {
            this.capacity = capacity;
            this.count = count;
            this.courseName = courseName;
            this.section = section;
            times = new List<Time>();
        }

        private int getSeconds(string t)
        {
            int totalSeconds = 0;
            int i = 0;
            for (; t[i] != ':'; i++) ;

            int hours = Convert.ToInt32(t.Substring(0, i));
            int minutes = Convert.ToInt32(t.Substring(i+1, 2));
            if (t[t.Length-2] == 'P' && hours!=12) hours += 12;
            totalSeconds = (hours * 60 * 60) + (minutes*60);
            return totalSeconds;
        }
        public void addTime(string day, string from, string to, string room)
        {
            Time t = new Time();
            t.day = day;
            t.from = getSeconds(from);
            t.to = getSeconds(to);
            t.room = room;
            times.Add(t);
            Console.WriteLine("From: " + t.from + " to: " + t.to + "(" + from + " " + to + ")");
        }

        public bool checkClash(List<Time> t)
        {

            foreach(Time x in t)
            {           
                foreach(Time y in times)
                {
                    if (x.day != y.day) continue;

                    if (x.from >= y.from && x.from < y.to) return true;
                    else if (x.to > y.from && x.to <= y.to) return true;
                    else if (y.from >= x.from && y.from < x.to) return true;
                    else if (y.to > x.from && y.to <= x.to) return true;
                }
            }
            return false;
        }
    }
}
