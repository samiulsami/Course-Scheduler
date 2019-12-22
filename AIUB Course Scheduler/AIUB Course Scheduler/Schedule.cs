using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AIUB_Course_Scheduler
{
    public class Schedule
    {
        public List<Section> sections { get; set; }
        public int Credits { get; set; }

        public string hash;
       
        public Schedule()
        {
            sections = new List<Section>();
            //this.sz = sz;
            //takenIndex = new bool[sz + 1];
            Credits = 0;
        }
        public bool AddSection(Section sc)
        {

            foreach(Section ss in sections)
            {
                if (ss.checkClash(sc.times)) return false;
            }
            Credits += sc.Credits;
            sections.Add(sc);
            return true;
        }
        public object Clone()
        {
            var serialized = JsonConvert.SerializeObject(this);
            return (Schedule)JsonConvert.DeserializeObject<Schedule>(serialized);
        }
    }
}
