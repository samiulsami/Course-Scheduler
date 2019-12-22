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
        List<Schedule> scheduleList;
        Schedule currentSchedule;
        public ScheduleDisplayForm(List<Schedule> scheduleList)
        {
            this.scheduleList = scheduleList;
            currentSchedule = scheduleList[0];
            InitializeComponent();

            display();
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
                x = a;
                y = b;
            }
           public static bool operator >(pair xx, pair yy)
            {
                return xx.x > yy.x;
            }
            public static bool operator <(pair xx, pair yy)
            {
                return xx.x < yy.x;
            }
        };
        private void display()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int i = 0; i < 20; i++) dataGridView1.Rows.Add();
            Dictionary<string, List<pair>> dayMem = new Dictionary<string, List<pair>>();
            currentSchedule = scheduleList[0];
            foreach(Section sc in currentSchedule.sections)
            {
               
                foreach(Time tm in sc.times)
                {
                    string dayString = tm.day.ToLower().Trim();
                    if (dayMem.ContainsKey(dayString) == false)
                    {
                        List<pair> p = new List<pair>();
                        dayMem.Add(dayString, p);
                    }
                    dayMem[dayString].Add(new pair(tm.from, tm.to, sc.courseName, tm.room));
                    //dayMem[dayString].Sort();
                }
            }
            List<string> dayArr = new List<string>();
            foreach (DataGridViewColumn clm in dataGridView1.Columns)
            {
                string dayString = clm.HeaderText.ToString().ToLower().Trim();
                dayArr.Add(dayString);
            }
            for(int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                
                for(int j=0; j<6; j++)
                {
                    if (dayMem.ContainsKey(dayArr[j]) == false)
                    {
                        List<pair> p = new List<pair>();
                        dayMem.Add(dayArr[j], p);
                    }
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
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
