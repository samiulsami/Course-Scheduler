using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AIUB_Course_Scheduler
{
    public partial class ScheduleDisplayForm : Form
    {
        private List<Schedule> scheduleList;
        private IList<Course> x;
        private IList<Course> z;
        private string y;
        private List<Course> selectedCourses;
        private int curPos=0;

        
        public ScheduleDisplayForm(List<Schedule> scheduleList, object selectedCourses, object x, object y, object z)
        {
            this.x = x as List<Course>;
            this.z = z as List<Course>;
            this.y = y as string;
            this.selectedCourses = (List<Course>)selectedCourses;
            this.scheduleList = scheduleList;
            curPos = 0;
            //loadButtons();
            InitializeComponent();

            display(scheduleList[curPos]);
        }

        public struct pair
        {
            public int x, y;
            public string room;
            public string courseName;
            public pair(int a, int b,string courseName, string room)
            {
                this.courseName = courseName;
                this.room = room;
                this.x = a;
                this.y = b;
            }
        };
        void loadButtons()
        {
            if (curPos == 0) button2.Enabled = false;
            else
            {
                button2.Enabled = true;
            }
            if (curPos < scheduleList.Count-1) button1.Enabled = true;
            else
            {
                button1.Enabled = false;
            }
        }
        public class ccomp : IComparer<pair>
        {
            public ccomp()
            {                
            }
            public int Compare(pair tmp1, pair tmp2)
            {

                if (tmp1.x > tmp2.x) return 1;
                else return -1;

            }
        }
        private void display(Schedule cur)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int i = 0; i < 20; i++) dataGridView1.Rows.Add();
            Dictionary<string, List<pair>> dayMem = new Dictionary<string, List<pair>>();

            label1.Text = cur.Credits + " Credits";
            List<string> dayArr = new List<string>();
            foreach (DataGridViewColumn clm in dataGridView1.Columns)
            {
                string dayString = clm.HeaderText.ToString().ToLower().Trim();
                dayArr.Add(dayString);
                if (dayMem.ContainsKey(dayString) == false)
                {
                    List<pair> p = new List<pair>();
                    dayMem.Add(dayString, p);
                }
            }

            foreach (Section sc in cur.sections)
            {
                //Console.WriteLine(sc.courseName + " " + sc.times[0].day);
                foreach(Time tm in sc.times)
                {
                    string dayString = tm.day.ToLower().Trim();
                    
                    dayMem[dayString].Add(new pair(tm.from, tm.to, sc.courseName, tm.room));
                    
                }
            }
            
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                
                for (int j=0; j<6; j++)
                {
                    ccomp icomp = new ccomp();
                    dayMem[dayArr[j]].Sort(icomp);
                    if (dayMem[dayArr[j]].Count <= i) continue;
                    else
                    {
                        dataGridView1.Rows[i].Cells[j].Value = dayMem[dayArr[j]][i].courseName.ToString();
                        string time1 = "";
                        string time2 = "";
                        string time = "";
                        int from = dayMem[dayArr[j]][i].x;
                        int to = dayMem[dayArr[j]][i].y;
                        time1 = String.Format("{0:00}:{1:00}", from / 3600, (from / 60) % 60);
                        time2 = String.Format("{0:00}:{1:00}", to / 3600, (to / 60) % 60);
                        time = time1 + " - " + time2;
                        dataGridView1.Rows[i].Cells[j].Value += " \n " + time + " [" +dayMem[dayArr[j]][i].room + "]";
                    }
                }
            }
            loadButtons();
            //Console.WriteLine(curPos)
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            curPos--;
            if (curPos < 0) curPos = 0;
            display(scheduleList[curPos]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curPos++;
            if (curPos >= scheduleList.Count) curPos = scheduleList.Count - 1;
            display(scheduleList[curPos]);            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SchedulePreferencesSelection sps = new SchedulePreferencesSelection(selectedCourses, x, y, z);
            this.Hide();
            sps.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
