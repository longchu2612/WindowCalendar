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
    public partial class PlanDay : UserControl
    {

        string connectstring = @"Data Source=DESKTOP-2RHLRSM;Initial Catalog=Project_4;Integrated Security=True;Trust Server Certificate=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        private DateTime date;

        public DateTime Date { get => date; set => date = value; }

        public PlanDay(DateTime dateTime)
        {
            InitializeComponent();
            this.date = dateTime;
            dtpDate.Value = dateTime;
        }

        private void PlanDay_Load(object sender, EventArgs e)
        {
            dtpDate.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpAppointmentTime_ValueChanged(object sender, EventArgs e)
        {
            dtpAppointmentTime.Format = DateTimePickerFormat.Time;
            dtpAppointmentTime.ShowUpDown = true; 
        }

        private void btnCreateCalendar_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dtpEndTime.Format = DateTimePickerFormat.Time;
            dtpEndTime.ShowUpDown = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            showScheduleDetails((sender as DateTimePicker).Value);
        }

        void showScheduleDetails(DateTime dateTime)
        {

        }
    }
}
