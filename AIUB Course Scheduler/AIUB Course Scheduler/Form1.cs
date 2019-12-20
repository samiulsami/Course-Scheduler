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

namespace AIUB_Course_Scheduler
{
    public partial class Form1 : Form
    {
        private IList<Course> courses;
        private Dictionary<string, int> inDegree,Credits,Age;
        private Dictionary<string, List<string>> graph;
        private int totalCredits = 0;
        private string department = "CSE";
        //private SecondForm sf;

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public Form1(string department)
        {
            InitializeComponent();
            this.department = department;
            string fileName = department + ".json";           
            //Read file containing the pre-requisites for each course in the CSE program
            //COURSES WITH CREDI REQUIREMENTS ARE NOT INCLUDED YET (e.g; RESEEARCH METHODOLOGY, THESIS, INTERNSHIP, etc.)
            String jsonresult = File.ReadAllText(@fileName);
            courses = JsonConvert.DeserializeObject<List<Course>>(jsonresult);

            inDegree = new Dictionary<string, int>();
            graph = new Dictionary<string, List<string>>();
            Credits = new Dictionary<string, int>();
            Age = new Dictionary<string, int>();
           
            //Initialize graph and other variables
            for(int i=0; i<courses.Count; i++)
            {
                string courseName = courses[i].CourseName;
                List<string> newlist = new List<string>();
                graph.Add(courseName, newlist);
                Credits[courseName] = courses[i].Credits;
                inDegree[courseName] = 0;
                Age[courseName] = 0;
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

        private void topological_sort(string source)
        {
            //do a topological sort from source and decrease increase in-degree of all reachable nodes
            Queue<string> q = new Queue<string>();
            q.Enqueue(source);
            inDegree[source]=0;
            while (q.Any())
            {
                string front = q.Dequeue();
                foreach (string s in graph[front])
                {
                    if (inDegree[s] == -1)
                    {
                        q.Enqueue(s);
                        inDegree[s] += 2;
                    }
                    else inDegree[s]++;
                }
            }
        }
       
        private void populateDataGrids()
        {
            // Populates the data-grids with available courses and taken courses
            // Taken courses have in-degree -1
            // Available courses have in-degree 0
            label4.Text = department;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            totalCredits = 0;
            foreach (Course crs in courses)
            {
                if (crs.Credits_required > 0) continue;

                if (inDegree[crs.CourseName] == 0 && crs.Credits_required==0)
                {
                    dataGridView1.Rows.Add("+", crs.CourseName);
                    Age[crs.CourseName]++;
                }
                else if (inDegree[crs.CourseName] == -1)
                {
                    dataGridView2.Rows.Add("-", crs.CourseName);
                    Age[crs.CourseName] = 0;
                    totalCredits += crs.Credits;
                }                
            }

            foreach (Course crs in courses)
            {
                if (crs.Credits_required == 0) continue;

                if(totalCredits>=crs.Credits_required && inDegree[crs.CourseName] == 0)
                {
                    dataGridView1.Rows.Add("+", crs.CourseName);
                    Age[crs.CourseName]++;
                }
                else if (inDegree[crs.CourseName] == -1 && totalCredits>=crs.Credits_required)
                {
                    dataGridView2.Rows.Add("-", crs.CourseName);
                    Age[crs.CourseName] = 0;
                    totalCredits += crs.Credits;
                }
                else if(inDegree[crs.CourseName]==-1 && totalCredits < crs.Credits_required)
                {
                    //inDegree[crs.CourseName] = 0;
                    topological_sort(crs.CourseName);
                    populateDataGrids();//Never called more than once per course since "topological sort()" sets inDegree[courseName] to 0"
                }
            }

            //Display total completed credits
            label2.Text = totalCredits.ToString();

            //Sort courses to display by "Age", meaning freshly unlocked courses are at the top
            Cmp cmp = new Cmp(ref Age);
            dataGridView1.Sort(cmp);
            dataGridView2.Sort(cmp);
                    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to reset all taken courses?", "Confirm action", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                foreach(Course crs in courses)
                {
                    if (crs.prerequisites[0] == "NIL") inDegree[crs.CourseName] = 0;
                    else inDegree[crs.CourseName] = crs.prerequisites.Count;
                    Age[crs.CourseName] = 0;                    
                }
                populateDataGrids();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Continue to next step?", "Confirm action", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                IList<Course> AvailableCourses = new List<Course>();
                foreach (Course crs in courses)
                {
                    if (inDegree[crs.CourseName] == 0 && totalCredits>=crs.Credits_required)
                        AvailableCourses.Add(crs);
                }
                SelectionForm sf = new SelectionForm(AvailableCourses,department);
                this.Hide();
                sf.ShowDialog();
                this.Close();

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Go Back?", "Confirm action", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                SecondForm sf = new SecondForm();
                this.Hide();
                sf.ShowDialog();
                this.Close();
            }
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

                topological_sort(courseName);

                populateDataGrids();
            }
        }
    }
    public class Cmp : System.Collections.IComparer
    {
        //Custom comparer class for datagridview columns
        private Dictionary<string, int> Age;
        public Cmp(ref Dictionary<string, int> Age)
        {
            this.Age = Age;
        }
        public int Compare(object x, object y)
        {
            DataGridViewRow dgv1 = (DataGridViewRow)x;
            DataGridViewRow dgv2 = (DataGridViewRow)y;
            string first, second;
            first = dgv1.Cells[1].Value.ToString();
            second = dgv2.Cells[1].Value.ToString();
            if (Age[first] > Age[second]) return 1;
            else if (Age[first] == Age[second]) return System.String.Compare(first, second);
            return -1;
        }
    }
}
