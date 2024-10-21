using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Policy;
using System.ComponentModel.DataAnnotations;

namespace WindowCalender
{
    public partial class PlanOfDate : Form
    {
        string connectionString = @"Data Source=DESKTOP-2RHLRSM;Initial Catalog=CalenderAPI;Integrated Security=True;Encrypt=False";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        private DateTime dateTime;

        private List<Appointment> appointments = new List<Appointment>();

        public DateTime DateTime { get; set; }

        private FlowLayoutPanel fPanel = new FlowLayoutPanel(); 

        public List<Appointment> Appointments { get; set; }

        public HttpClient _httpClient;
        public AppointmentService _appointmentService;

        public String test;
        public PlanOfDate(DateTime dateTime)
        {
            
            InitializeComponent();
            this.dateTime = dateTime;
            dtpkDate.Value= dateTime;
            //displayJobsOfDay(dateTime);
        }

        public async Task<List<Appointment>> GetAppointments(DateTime dt)
        {
            HttpClient httpClient = new HttpClient();
            string dateStr = dt.ToString("yyyy-MM-dd");
            string link = $"http://localhost:5112/api/Schedules/dateTime/{dateStr}";
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(link);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseData);

                    List<Appointment> schedules = JsonConvert.DeserializeObject<List<Appointment>>(responseData);

                    return schedules;

                }
                else
                {
                    MessageBox.Show($"Lỗi: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                return null;
            }
        }

        public async Task displayJobsOfDay(DateTime dateTime)
        {

            fPanel.Size = new Size(790, 320);
            pnlJob.Controls.Add(fPanel);

            List<Appointment> appointments = await GetAppointments(dateTime);
            fPanel.Controls.Clear();
           
            if (appointments.Count == 0)
            {
                lblMessageBox.Text = "Hôm nay bạn không có lịch nào !";
                lblMessageBox.Visible = true;
               
            }
            else
            {
                lblMessageBox.Visible = false;
                AJobTitle aJobTitle = new AJobTitle();
                fPanel.Controls.Add(aJobTitle);
            }


            if (appointments != null)
            {

                for (int i = 0; i < appointments.Count; i++)
                {
                    Console.WriteLine(appointments[i]);
                    ADayJob dayJob = new ADayJob(appointments[i]);
                    Console.WriteLine(appointments[i]);
                    dayJob.Edited += DayJob_Edited;
                    dayJob.Deleted += DayJob_Deleted;
                    fPanel.Controls.Add(dayJob);
                }
            }

        }

        //public List<Appointment> getAppointmentsByDate(DateTime dateTime)
        //{
            

            
        //}

        private async void DayJob_Deleted(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            ADayJob uc = sender as ADayJob;
            Appointment appointment = uc.Appointment;
            if (result == DialogResult.Yes)
            {
                Console.WriteLine(appointment);
                Boolean check = await deleteAppointment(appointment.id);
                if(check == true)
                {
                    fPanel.Controls.Remove(uc);
                    appointments.Remove(appointment);
                    MessageBox.Show("Xóa thành công");

                }
                else
                {
                    MessageBox.Show("Xóa thất bại");
                }
                displayJobsOfDay(appointment.Date);
            }
           
            
        }

        public async Task<bool> deleteAppointment(int id)
        {
            HttpClient httpClient = new HttpClient();
            string link = $"http://localhost:5112/api/Schedules/{id}";
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(link);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                return false;
            }
        }

        private async void DayJob_Edited(object sender, EventArgs e)
        {
            
            ADayJob uc = sender as ADayJob;
            
            if (uc == null) return;

            
            Appointment appointment = uc.Appointment;
            Console.WriteLine(appointment);

            //for (int i = 0; i < appointments.Count; i++)
            //{
            //    if (appointments[i].Id == appointment.Id)  
            //    {
            //        appointments[i] = appointment;  
            //        break;
            //    }
            //}
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(appointment, serviceProvider: null, items: null);
            bool isValid = Validator.TryValidateObject(appointment, context, validationResults, true);
            if (!isValid)
            {
                
                foreach (var validationResult in validationResults)
                {
                    MessageBox.Show(validationResult.ErrorMessage);
                }
                return; 
            }
            if (appointment.fromX > appointment.toX)
            {
                MessageBox.Show("StartTime < EndTime");
                return;
            }
            else if (appointment.fromX == appointment.toX && appointment.fromY >= appointment.toY)
            {
                MessageBox.Show("StartTime < EndTime");
                return;
            }
            






            //updateAppointmentbyId(appointment);
            bool result = await updateAppointmentWithId(appointment.id, appointment);
            List<Appointment> list = await GetAppointments(appointment.Date);
            UpdateFlowLayoutPanel(list);
            uc.setFieldReadOnly(true);
            uc.changeTextValueOfButtonEdit();
            uc.isEditing = false;
        }

        public async Task<bool> updateAppointmentWithId(int id,Appointment appointment)
        {
            HttpClient httpclient = new HttpClient();
            string link = $"http://localhost:5112/api/Schedules/{id}";
            Console.WriteLine(link);
            string jsonData = JsonConvert.SerializeObject(appointment);
            Console.WriteLine(jsonData);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpclient.PutAsync(link,content);
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Chỉnh sửa thành công!");
                    return true;
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa thất bại!");
                    return false;
                }

            }
            catch (Exception ex) {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return false;
            }

        }

        private void UpdateFlowLayoutPanel(List<Appointment> appointments)
        {
            
            fPanel.Controls.Clear();
            AJobTitle aJobTitle = new AJobTitle();
            fPanel.Controls.Add(aJobTitle);

            foreach (var app in appointments)
            {
                ADayJob dayJobControl = new ADayJob(app);
                dayJobControl.Edited += DayJob_Edited;
                dayJobControl.Deleted += DayJob_Deleted;
                fPanel.Controls.Add(dayJobControl);    
            }


        }


       

        //public List<Appointment> getAllApointments(DateTime dateTime)
        //{

        //    //List<Appointment> appointments = new List<Appointment>();
        //    appointments.Clear();
        //    try
        //    {
        //        con = new SqlConnection(connectionString);
        //        con.Open();
        //        string query = "select * from dbo.Schedules where date = @dateParam";
        //        cmd = new SqlCommand(query, con);

        //        cmd.Parameters.AddWithValue("@dateParam", dateTime.Date);

        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if(reader.HasRows == false)
        //            {
        //                return appointments;
        //            }
        //            while (reader.Read())
        //            {   
        //                Appointment appointment = new Appointment();
        //                if (!reader.IsDBNull(reader.GetOrdinal("id")))
        //                {
        //                    appointment.Id = reader.GetInt32(reader.GetOrdinal("id"));
        //                }

        //                if (!reader.IsDBNull(reader.GetOrdinal("reason")))
        //                {
        //                    appointment.Reason = reader.GetString(reader.GetOrdinal("reason"));
        //                }


        //                Point fromPoint = new Point();


        //                if (!reader.IsDBNull(reader.GetOrdinal("fromX")))
        //                {
        //                    fromPoint.X = reader.GetInt32(reader.GetOrdinal("fromX"));
        //                }
        //                if (!reader.IsDBNull(reader.GetOrdinal("fromY")))
        //                {
        //                    fromPoint.Y = reader.GetInt32(reader.GetOrdinal("fromY"));
        //                }

        //                appointment.From = fromPoint;   

        //                Point toPoint = new Point();

        //                if (!reader.IsDBNull(reader.GetOrdinal("toX")))
        //                {
        //                    toPoint.X = reader.GetInt32(reader.GetOrdinal("toX"));
        //                }
        //                if (!reader.IsDBNull(reader.GetOrdinal("toY")))
        //                {
        //                    toPoint.Y = reader.GetInt32(reader.GetOrdinal("toY"));
        //                }

        //                appointment.To = toPoint;
        //                appointments.Add(appointment);

        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        MessageBox.Show("Error :" + ex.Message);
        //    }
        //    finally
        //    {
                
        //        if (con != null && con.State == ConnectionState.Open)
        //        {
        //            con.Close();
        //        }
        //    }
        //    return appointments;

        //}

        public void updateAppointmentbyId(Appointment appointment)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "UPDATE dbo.Schedules SET fromX = @fromXParam, fromY = @fromYParam, toX = @toXParam, toY = @toYParam, reason = @reasonParam WHERE ID = @idParam";
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@fromXParam", appointment.fromX);
                        cmd.Parameters.AddWithValue("@fromYParam", appointment.fromY);
                        cmd.Parameters.AddWithValue("@toXParam", appointment.toX);
                        cmd.Parameters.AddWithValue("@toYParam", appointment.toY);
                        cmd.Parameters.AddWithValue("@reasonParam", appointment.reason);
                        cmd.Parameters.AddWithValue("@idParam", appointment.id);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy cuộc hẹn với ID đã cho.");
                        }
                    }

                    
                }
            
            

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public bool insertAppointment(DateTime dateTime)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string query = "Insert INTO dbo.Schedules (date) Values (@dateParam)";
                    using (cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@dateParam", dateTime.Date);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected < 0) {
                            MessageBox.Show("Thêm mới thất bại!");
                            return false;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {

                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return true;

        }

        //public async Task deleteAppointment(int id)
        //{

        //}

        public void deleteAppointmentById(int id)
        {
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                string query = "DELETE FROM dbo.Schedules WHERE id = @idParam";
                cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@idParam", id);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    MessageBox.Show("No appointment found with the provided ID.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

        }

        

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void pnlJob_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void dtpkDate_ValueChanged(object sender, EventArgs e)
        {
            displayJobsOfDay((sender as DateTimePicker).Value);
        }

        public async Task addNewAppointment(DateTime dateTime)
        {
            HttpClient httpClient = new HttpClient();
            Appointment appointment = new Appointment() { Date = dtpkDate.Value };
            string dateStr = dateTime.ToString("yyyy-MM-dd");
            string link = $"http://localhost:5112/api/Schedules/{dateStr}";
            //appointments.Add(appointment);
            try
            {
                HttpContent content = new StringContent(String.Empty);
                HttpResponseMessage response = await httpClient.PostAsync(link, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Appointment appointment_2 = JsonConvert.DeserializeObject<Appointment>(jsonResponse);
                    appointments.Add(appointment_2);
                    ADayJob aDayJob = new ADayJob(appointment_2);
                    aDayJob.Edited += DayJob_Edited;
                    aDayJob.Deleted += DayJob_Deleted;
                    fPanel.Controls.Add(aDayJob);
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");

            }

        }

        public void showJobWithDateTime(DateTime dateTime)
        {
            
        }

        private void PlanOfDate_Load(object sender, EventArgs e)
        {

        }

        private void btnTomorrow_Click(object sender, EventArgs e)
        {
            this.dtpkDate.Value = dtpkDate.Value.AddDays(1);
            
        }

        private void btnYesterday_Click(object sender, EventArgs e)
        {
            this.dtpkDate.Value = dtpkDate.Value.AddDays(-1);
            
        }

        private void mnsHomNay_Click(object sender, EventArgs e)
        {
            this.dtpkDate.Value = DateTime.Now;
        }

        public async void mnsThemViec_Click(object sender, EventArgs e)
        {
            List<Appointment> list = await GetAppointments(dtpkDate.Value);
            if (list.Count == 0)
            {
               AJobTitle aJobTitle = new AJobTitle();
               fPanel.Controls.Add(aJobTitle);
               lblMessageBox.Visible = false;
            }
            addNewAppointment(dtpkDate.Value);
        }



        private void lblMessageBox_Click(object sender, EventArgs e)
        {

        }

      

        //public void insertAppointment(Appointment appointment)
        //{
        //    try
        //    {
        //        con = new SqlConnection(connectionString);
        //        con.Open();


        //    }
        //    catch(Exception ex)
        //    {

        //    }

        //}


    }
}
