using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIUB_Course_Scheduler
{
    public class Schedule : ICloneable
    {
        public List<Section> sections { get; set; }
        public Dictionary<string, bool> takenCourses { get; set; }
        public int Credits { get; set; }

       // public bool[] takenIndex { get; set; }
        public int sz { get; set; }
       
        public Schedule()
        {
            takenCourses = new Dictionary<string, bool>();
            sections = new List<Section>();
            //this.sz = sz;
            //takenIndex = new bool[sz + 1];
            Credits = 0;
        }
        public bool AddSection(Section sc)
        {
            if (takenCourses.ContainsKey(sc.courseName)) return false;
            else takenCourses.Add(sc.courseName, true);

            foreach(Section ss in sections)
            {
                if (ss.checkClash(sc.times)) return false;
            }
            Credits += sc.Credits;
            sections.Add(sc);
            return true;
        }
        public void RemoveSection(Section sc)
        {

            int tmpCredits = sc.Credits;
            if (sections.Remove(sc))
            {
                Credits -= sc.Credits;
                takenCourses.Remove(sc.courseName);
            }
        }

        public object Clone()
        {
            return new Schedule
            {
                sections = this.sections,
                takenCourses = this.takenCourses,
                //takenIndex=this.takenIndex,
                Credits = this.Credits,
                sz = this.sz

            };
        }
    }
}
