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
    public partial class SchedulePreferencesSelection : Form
    {
        private IList<Course> x;
        private IList<Course> z;
        private string y;
        private List<Course> selectedCourses;

        private void button1_Click(object sender, EventArgs e)
        {
            SelectionForm sf = new SelectionForm(x, y, z);
            this.Hide();
            sf.ShowDialog();
            this.Close();
        }

        public SchedulePreferencesSelection(object selectedCourses, object x, object y, object z)
        {
            this.x = x as List<Course>;
            this.z = z as List<Course>;
            this.y = y as string;
            this.selectedCourses = (List<Course>)selectedCourses;
            InitializeComponent();
        }

        private void SchedulePreferencesSelection_Load(object sender, EventArgs e)
        {

        }
    }
}
