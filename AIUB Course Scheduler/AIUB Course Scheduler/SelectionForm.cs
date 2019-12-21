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
    public partial class SelectionForm : Form
    {
        private IList<Course> AvailableCourses;
        private IList<Course> courses;
        private string department;
        public SelectionForm(object AvailableCourses,string department, object courses)
        {
            this.AvailableCourses = (List<Course>)AvailableCourses;
            this.courses = (List<Course>)courses;
            this.department = department;
            InitializeComponent();
            dataGridView2.Rows.Clear();
            foreach (Course crs in this.AvailableCourses)
            {
                dataGridView2.Rows.Add(false, crs.CourseName);
            }
            

        }

        private void SelectionForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Course> selectedCourses = new List<Course>();
            Dictionary<string, bool> taken = new Dictionary<string, bool>();
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                string CourseName = row.Cells[1].Value.ToString();
                if (Convert.ToBoolean(row.Cells[0].Value) == true)
                {
                    taken[CourseName] = true;
                }
                else taken[CourseName] = false;
            }

            foreach (Course crs in courses)
            {
                if (!taken.ContainsKey(crs.CourseName)) continue;

                if (taken[crs.CourseName])
                {
                    selectedCourses.Add(crs);
                }
            }

            SchedulePreferencesSelection sps = new SchedulePreferencesSelection(selectedCourses,taken, AvailableCourses, department, courses);
            this.Hide();
            sps.ShowDialog();
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1(department);
            this.Hide();
            f1.ShowDialog();
            this.Close();
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            int j = e.ColumnIndex;

            if (j == 0 && i >= 0)
            {        
                    DataGridViewRow row = dataGridView2.Rows[i];
                    DataGridViewCheckBoxCell cell = row.Cells[0] as DataGridViewCheckBoxCell;                
                    cell.Value = !Convert.ToBoolean(cell.Value);
                    if (Convert.ToBoolean(cell.Value) == true)
                        row.DefaultCellStyle.BackColor = Color.LightGray;                    
                    else 
                        row.DefaultCellStyle.BackColor = Color.White;                
            }
        }
    }
}
