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
using Newtonsoft.Json;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        IList<Course> courses;
        Dictionary<string, int> inDegree,Credits;
        Dictionary<string, List<string>> graph;
        Dictionary<string, bool> takenCourses;
        int totalCredits = 0;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string department = "CSE";


        public Form1()
        {
            InitializeComponent();

            String jsonresult = File.ReadAllText(@"CSE Prerequisites.json");
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonresult);

            inDegree = new Dictionary<string, int>();
            graph = new Dictionary<string, List<string>>();
            Credits = new Dictionary<string, int>();
            takenCourses = new Dictionary<string, bool>();
           
            for(int i=0; i<courses.Count; i++)
            {
                string courseName = courses[i].CourseName;
                List<string> newlist = new List<string>();
                graph.Add(courseName, newlist);
                Credits[courseName] = courses[i].Credits-48;
                inDegree[courseName] = 0;
                takenCourses[courseName] = false;
            }

            for(int i=0; i<courses.Count; i++)
            {
                if (courses[i].prerequisites[0] == "NIL") continue;
                else
                {
                    inDegree[courses[i].CourseName] = courses[i].prerequisites.Count;
                    for (int j = 0; j < courses[i].prerequisites.Count; j++)                  
                        graph[courses[i].prerequisites[j]].Add(courses[i].CourseName);                    
                }
            }
            populateDataGrids();
        }

        private void populateDataGrids()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            totalCredits = 0;
            foreach (Course crs in courses)
            {
                if (inDegree[crs.CourseName] == 0)
                    dataGridView1.Rows.Add("+", crs.CourseName);
                else if (inDegree[crs.CourseName] < 0)
                {
                    dataGridView2.Rows.Add("-", crs.CourseName);
                    totalCredits += Credits[crs.CourseName];
                }
                
            }

            label2.Text = totalCredits.ToString();

            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
                    
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int j = e.ColumnIndex;
            if (j == 0 && i>=0){
                string courseName = dataGridView1.Rows[i].Cells[1].Value.ToString();

                inDegree[courseName]--;
                takenCourses[courseName] = true;
                foreach (string nxt in graph[courseName])
                {
                    inDegree[nxt]--;
                }

                populateDataGrids();            
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int j = e.ColumnIndex;
            if (j == 0 && i >= 0)
            {
                string courseName = dataGridView2.Rows[i].Cells[1].Value.ToString();

                Queue<string> q = new Queue<string>();
                q.Enqueue(courseName);
                inDegree[courseName]++;
                takenCourses[courseName] = false;
                while (q.Any())
                {
                    string front = q.Dequeue();
                    foreach (string s in graph[front])
                    {
                        if (takenCourses[s])
                        {
                            q.Enqueue(s);
                            inDegree[s] += 2;
                            takenCourses[s] = false;
                        }
                        else
                        {
                            inDegree[s]++;
                        }
                    }
                }


               populateDataGrids();
            }
        }
    }
}
