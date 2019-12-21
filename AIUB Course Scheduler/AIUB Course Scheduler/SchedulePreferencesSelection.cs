using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;

namespace AIUB_Course_Scheduler
{
    public partial class SchedulePreferencesSelection : Form
    {
        private IList<Course> x;
        private IList<Course> z;
        private string y;
        private List<Course> selectedCourses;
        private List<Section> sectionList;
        private bool checkReserved = true;

        private void button1_Click(object sender, EventArgs e)
        {
            SelectionForm sf = new SelectionForm(x, y, z);
            this.Hide();
            sf.ShowDialog();
            this.Close();
        }

        private string Trim(string s)
        {
            int i = 0;
            for (; s[i] != '(' && s[i] != '[' && i < s.Length; i++) ;
            return s.Substring(0, i).ToLower();
        }

        public SchedulePreferencesSelection(object selectedCourses, object x, object y, object z)
        {
            this.x = x as List<Course>;
            this.z = z as List<Course>;
            this.y = y as string;
            this.selectedCourses = (List<Course>)selectedCourses;
            sectionList = new List<Section>();

            InitializeComponent();

            FileStream stream = File.Open(@"Offered Course Report.xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            DataTable dt = result.Tables[0];

            //MessageBox.Show(dt.Rows.Count.ToString());
            bool[] vis = new bool[dt.Rows.Count + 1];

            foreach (Course crs in this.selectedCourses)
            {
                //Offered courses row starts from index-2
                for (int i = 2; i < dt.Rows.Count; i++)
                {
                    if (vis[i]) continue;
                    /*
                        col-2 = Status
                        col-3 = Capacity
                        col-4 = Count
                        col-5 = Course Title
                        col-6 = Section
                        col-8 = Type
                        col-9 = Day
                        col-10 = start time
                        col-11 = end time
                        col-12 = room
                    */
                    string Status = dt.Rows[i][2].ToString();
                    int Capacity = Convert.ToInt32(dt.Rows[i][3].ToString());
                    int Count = Convert.ToInt32(dt.Rows[i][4].ToString());
                    string CourseTitle = Trim(dt.Rows[i][5].ToString());
                    string _Section = dt.Rows[i][6].ToString();
                    string _Type = dt.Rows[i][8].ToString();
                    string Day = dt.Rows[i][9].ToString();
                    string StartTime = dt.Rows[i][10].ToString();
                    string EndTime = dt.Rows[i][11].ToString();
                    string Room = dt.Rows[i][12].ToString();

                    if (_Type.Length == 0 || Day.Length == 0) continue;
                    if (Status == "Reserved" && !checkReserved)
                    {
                        continue;
                    }
                    else if (Status == "Cancel") continue;

                    //Console.WriteLine(CourseTitle + " " + crs.CourseName.ToLower());
                    if (crs.CourseName.ToLower().Trim().Equals(CourseTitle.Trim()))
                    {
                        vis[i] = true;
                        //Console.WriteLine(" yes");
                        //Console.Read();
                        Section sc = new Section(Capacity, Count, crs.CourseName, _Section);
                        sc.addTime(Day, StartTime, EndTime, Room);

                        for (int j = i + 1; j < dt.Rows.Count; j++)
                        {                            
                            if (CourseTitle == Trim(dt.Rows[j][5].ToString()))
                            {
                                if (vis[j]) continue;
                                if (dt.Rows[j][6].ToString() == _Section)
                                {
                                    vis[j] = true;
                                    sc.addTime(dt.Rows[j][9].ToString(), dt.Rows[j][10].ToString(), dt.Rows[j][11].ToString(), dt.Rows[j][12].ToString());
                                }

                            }
                            else break;
                        }

                        sectionList.Add(sc);
                    }
                    

                }
            }
            MessageBox.Show("Done " + sectionList.Count);
            foreach(Section sc in sectionList)
            {
                Console.WriteLine(sc.courseName + " " + sc.section);
            }
            

        }

        private void SchedulePreferencesSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
