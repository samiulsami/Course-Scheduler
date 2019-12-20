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
    public partial class SecondForm : Form
    {
        FirstForm ff;
        public SecondForm(FirstForm ff)
        {
            this.ff = ff;
            InitializeComponent();
            //ff.Close();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            confirmBtn.Enabled = true;
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your department");
                return;
            }
            string department = comboBox1.SelectedItem.ToString();      
            Form1 csf = new Form1(department,this);
            csf.Show();
            this.Hide();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            ff.Show();
            this.Close();
        }

        private void SecondForm_Load(object sender, EventArgs e)
        {

        }
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
