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
        private int curPos=0;

        
        public ScheduleDisplayForm(List<Schedule> scheduleList)
        {
            this.scheduleList = scheduleList;
            curPos = 0;
            //loadButtons();
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
            if (curPos < scheduleList.Count) button1.Enabled = true;
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
        private void display()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            for (int i = 0; i < 20; i++) dataGridView1.Rows.Add();
            Dictionary<string, List<pair>> dayMem = new Dictionary<string, List<pair>>();

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

            foreach (Section sc in scheduleList[curPos].sections)
            {
               
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
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            curPos--;
            if (curPos < 0) curPos = 0;
            display();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            curPos++;
            if (curPos >= scheduleList.Count) curPos = scheduleList.Count - 1;
            display();
        }
    }
}
