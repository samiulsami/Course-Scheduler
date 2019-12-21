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
        private Dictionary<string, bool> taken;

        private void button1_Click(object sender, EventArgs e)
        {
            SelectionForm sf = new SelectionForm(x, y, z);
            this.Hide();
            sf.ShowDialog();
            this.Close();
        }

        public SchedulePreferencesSelection(object selectedCourses, object taken, object x, object y, object z)
        {
            this.x = x as List<Course>;
            this.z = z as List<Course>;
            this.y = y as string;
            this.taken = (Dictionary<string,bool>)taken;
            this.selectedCourses = (List<Course>)selectedCourses;
            InitializeComponent();

            FileStream stream = File.Open(@"Offered Course Report.xlsx", FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            DataTable dt = result.Tables[0];
            
            //Offered courses row starts from index-2
            for(int i=2; i<dt.Rows.Count; i++)
            {
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
                string CourseTitle = dt.Rows[i][5].ToString();
                string Section = dt.Rows[i][6].ToString();
                string Type = dt.Rows[i][8].ToString();
                string Day = dt.Rows[i][9].ToString();
                string StartTime = dt.Rows[i][10].ToString();
                string EndTime = dt.Rows[i][11].ToString();
                string Room = dt.Rows[i][12].ToString();

            }
            

        }

        private void SchedulePreferencesSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
