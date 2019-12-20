using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;
using Newtonsoft.Json;
using System.IO;

namespace Course_Scheduler
{
    public partial class Form1 : Form
    {
        private string jsonstring;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = "DefaultJSON.json";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Navigate to Excel file";
            ofd.Filter = "Excel File|*.xlsx";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = ofd.FileName;
                textBox2.Text = TrimFileExtension(ofd.SafeFileName) + ".json";
                

                //System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                FileStream stream = File.Open(@ofd.FileName, FileMode.Open, FileAccess.Read);
                IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                DataSet result = excelReader.AsDataSet();
                DataTable dt = result.Tables[0];

                Dictionary<string, string> CourseIDtoCourseName = new Dictionary<string, string>();
                string courseID;
                string courseName;                
                CourseIDtoCourseName["NIL"] = "NIL";
                IList<Course> courses = new List<Course>();

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    courseID = dt.Rows[i][0].ToString();
                    courseName = dt.Rows[i][1].ToString();
                    CourseIDtoCourseName[courseID] = courseName;

                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    courseID = dt.Rows[i][0].ToString();
                    courseName = dt.Rows[i][1].ToString();
                    
                    CourseIDtoCourseName[courseID] = courseName;
                    string[] prerequisites = (dt.Rows[i][2].ToString()).Split(',');
                    for (int j = 0; j < prerequisites.Length; j++)
                        prerequisites[j] = prerequisites[j].Trim();
                    int tmp_credits = 0;
                    int crd = 0;
                    if (prerequisites[0] != "NIL" && Int32.TryParse(prerequisites[0], out tmp_credits))
                    {
                        crd = tmp_credits;
                        prerequisites[0] = "NIL";
                    }                           

                    Course crs = new Course();
                    crs.Credits_required = crd;
                    crs.CourseID = courseID;
                    crs.CourseName = courseName;
                    crs.Credits = Convert.ToInt32(dt.Rows[i][3].ToString()[0])-48;
                    crs.prerequisites = new List<string>();
                    foreach (string s in prerequisites) crs.prerequisites.Add(CourseIDtoCourseName[s]);

                    courses.Add(crs);
                }

                jsonstring = JsonConvert.SerializeObject(courses);
                button2.Enabled = true;
                

            }
        }

        private string TrimFileExtension(string s)
        {
            int i;
            bool found = false;
            for(i=0 ; i<s.Length; i++)
            {
                if (s[i] == '.')
                {
                    found = true;
                    break;
                }
            }
            if (!found) return s;
            return s.Substring(0, i);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string savePath = fbd.SelectedPath;
                savePath = savePath + "\\" + TrimFileExtension(textBox2.Text) + ".json";
                File.WriteAllText(savePath, jsonstring);
                button2.Enabled = false;

            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
