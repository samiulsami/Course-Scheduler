using System.Collections.Generic;

namespace Course_Scheduler
{
    class Course
    {
        public List<string> prerequisites;
        public string CourseName;
        public string CourseID;
        public int Credits;
        public int Credits_required = 0;
    }
}
