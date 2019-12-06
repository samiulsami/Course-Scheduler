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
        int totalCredits = 0;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        string department = "CSE";


        public Form1()
        {
            InitializeComponent();

            //Read file containing the pre-requisites for each course in the CSE program
            //COURSES WITH CREDI REQUIREMENTS ARE NOT INCLUDED YET (e.g; RESEEARCH METHODOLOGY, THESIS, INTERNSHIP, etc.)
            String jsonresult = File.ReadAllText(@"CSE Prerequisites.json");
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonresult);

            inDegree = new Dictionary<string, int>();
            graph = new Dictionary<string, List<string>>();
            Credits = new Dictionary<string, int>();
           
            //Initialize graph and other variables
            for(int i=0; i<courses.Count; i++)
            {
                string courseName = courses[i].CourseName;
                List<string> newlist = new List<string>();
                graph.Add(courseName, newlist);
                Credits[courseName] = courses[i].Credits-48;//Credits stored as ASCII characters in file
                inDegree[courseName] = 0;
            }

            //Create a Directed Acyclic Graph with courses as vertices, and calculate In-Degree of each vertex
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
            // Populates the data-grids with available courses and taken courses
            // Taken courses have in-degree -1
            // Available courses have in-degree 0

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            totalCredits = 0;
            foreach (Course crs in courses)
            {
                if (inDegree[crs.CourseName] == 0)
                    dataGridView1.Rows.Add("+", crs.CourseName);
                else if (inDegree[crs.CourseName] ==-1)
                {
                    dataGridView2.Rows.Add("-", crs.CourseName);
                    totalCredits += Credits[crs.CourseName];
                }                
            }

            //Display total completed credits
            label2.Text = totalCredits.ToString();


            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            dataGridView2.Sort(dataGridView2.Columns[1], ListSortDirection.Ascending);
                    
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            int i = e.RowIndex;
            int j = e.ColumnIndex;

            //When user Adds a course by clicking the "+" button
            //Note: all courses shown in the left data grid have in-degree 0
            if (j == 0 && i>=0){   
                
                //Decrement In-degree of selected course and set it to -1
                //Decrement In-degree of all child vertices of selected course
                string courseName = dataGridView1.Rows[i].Cells[1].Value.ToString();
                inDegree[courseName]--;                
                foreach (string nxt in graph[courseName])                
                    inDegree[nxt]--;                

                populateDataGrids();            
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int j = e.ColumnIndex;

            //When user removes a taken course by clicking the "-" button
            //Note: all taken courses have in-degree -1
            if (j == 0 && i >= 0)
            {
                //Run a BFS from selected vertex and only explore a vertex(course) if it has been taken
                //Increment in-degree of all child vertices by 1
                //Increment in-degree of all Taken courses(vertices) by 2 instead, since their in-degree is -1
                string courseName = dataGridView2.Rows[i].Cells[1].Value.ToString();

                Queue<string> q = new Queue<string>();
                q.Enqueue(courseName);
                inDegree[courseName]++;
                //takenCourses[courseName] = false;
                while (q.Any())
                {
                    string front = q.Dequeue();
                    foreach (string s in graph[front])
                    {
                        if (inDegree[s]==-1)
                        {
                            q.Enqueue(s);
                            inDegree[s] += 2;
                        }
                        else inDegree[s]++;                        
                    }
                }

                populateDataGrids();
            }
        }
    }
}
