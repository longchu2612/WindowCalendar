using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCalender
{
    public partial class DailyPlan : Form
    {

        string connectstring = @"Data Source=DESKTOP-2RHLRSM;Initial Catalog=Project_4;Integrated Security=True;Trust Server Certificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        private DateTime dateTime;
        public DailyPlan()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dtpStartTime.Format = DateTimePickerFormat.Time;
            dtpStartTime.ShowUpDown = true;
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
        }

        private void DailyPlan_Load(object sender, EventArgs e)
        {

        }
        
    }
}
