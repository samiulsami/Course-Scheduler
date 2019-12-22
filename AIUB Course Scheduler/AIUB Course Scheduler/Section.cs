using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUB_Course_Scheduler
{
    public struct Time
    {
        public string day;
        public int from;
        public int to;
        public string room;
        public string courseName;
    }


    public class Section
    {
        
        public int capacity { get; set; }
        public int count { get; set; }
        public string courseName { get; set; }
        public string section { get; set; }
        public int Credits { get; set; }
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
            t.courseName = this.courseName;
            times.Add(t);
            //Console.WriteLine("From: " + t.from + " to: " + t.to + "(" + from + " " + to + ")");
        }

        private string Trim(string s)
        {
            int i = 0;
            for (; i < s.Length && s[i] != '(' && s[i] != '['; i++) ;
            if (i > s.Length) i = s.Length;
            return s.Substring(0, i).ToLower().Trim();
        }
        public bool checkClash(List<Time> t)
        {
            
            //return false;
            foreach(Time x in t)
            {
                if (Trim(this.courseName).Equals(Trim(x.courseName))) return true;
                foreach (Time y in times)
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
