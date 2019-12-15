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
    public partial class FirstForm : Form
    {

        public FirstForm()
        {
            InitializeComponent();
        }

        private void appName_Label_Click(object sender, EventArgs e)
        {
                
        }

        private void enterBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            SecondForm sf = new SecondForm(this);
            sf.Show();
        }

        private void FirstForm_Load(object sender, EventArgs e)
        {

        }

    }
}
