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
using System.Net.Http.Headers;
using System.CodeDom;

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

        public async Task<AppointmentResult> GetAppointments(DateTime dt,string userId)
        {
            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            var refreshToken = cacheconnection.StringGet("refreshToken");

            TokenModel tokenModel = new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            var validateToken = await TokenHelper.checkToken(tokenModel);
            if(validateToken == null)
            {
                return new AppointmentResult
                {
                    Appointments = null,
                    IsTokenValid = false
                };

            }

            if(validateToken.Success == true)
            {
                cacheconnection.StringSet("accessToken", validateToken.accessToken);
                
            }

            HttpClient httpClient = new HttpClient();
            string dateStr = dt.ToString("yyyy-MM-dd");
            string link = $"http://localhost:5112/api/Schedules/getAllDateByUserIdWithoutReason?dateTime={dateStr}&userId={userId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cacheconnection.StringGet("accessToken"));
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(link);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    Console.WriteLine(responseData);

                    List<Appointment> schedules = JsonConvert.DeserializeObject<List<Appointment>>(responseData);

                    return new AppointmentResult
                    {
                        Appointments = schedules,
                        IsTokenValid = true
                    };

                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Lỗi: {response.StatusCode}");
                    MessageBox.Show($"Lỗi: {response.StatusCode}\nNội dung lỗi: {errorContent}");

                    return new AppointmentResult
                    {
                        Appointments = null,
                        IsTokenValid = true
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                return new AppointmentResult
                {
                    Appointments = null,
                    IsTokenValid = true
                };
            }
        }

        public async Task displayJobsOfDay(DateTime dateTime)
        {



            fPanel.Size = new Size(790, 320);
            pnlJob.Controls.Add(fPanel);


            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            Console.WriteLine(accessToken);
            string userId = TokenHelper.getUserIdFromAccessToken(accessToken);

            AppointmentResult result = await GetAppointments(dateTime,userId);
            fPanel.Controls.Clear();

            if (result.Appointments == null && result.IsTokenValid == false)
            {
                MessageBox.Show("Unthorization");
                this.Close();
                await cacheconnection.KeyDeleteAsync("accessToken");
                await cacheconnection.KeyDeleteAsync("refreshToken");
                LoginForm login = new LoginForm();
                login.Show();
                return;
            }

            if (result.Appointments.Count == 0)
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


            if (result.Appointments != null)
            {

                for (int i = 0; i < result.Appointments.Count; i++)
                {
                    //Console.WriteLine(appointments[i]);
                    ADayJob dayJob = new ADayJob(result.Appointments[i]);
                    //Console.WriteLine(result.Appointments[i]);
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
            ADayJob uc = sender as ADayJob;
            Appointment appointment = uc.Appointment;
            var delete_result = await deleteAppointment(appointment.id);
            if (delete_result.Success == false && delete_result.TokenInvalid == true)
            {
                LogoutAndRedirectToLogin();
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                Console.WriteLine(appointment);
                
                if(delete_result.Success == true)
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

        public async Task<DeleteAppointmentResult> deleteAppointment(int id)
        {

            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            var refreshToken = cacheconnection.StringGet("refreshToken");

            TokenModel tokenModel = new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            var validateToken = await TokenHelper.checkToken(tokenModel);
            if (validateToken == null)
            {
                return new DeleteAppointmentResult
                {
                    Success = false,
                    TokenInvalid = true,
                    ErrorMessage = "Token has expire and invalid"
                };
            }
            if (validateToken.Success == true)
            {
                cacheconnection.StringSet("accessToken", validateToken.accessToken);
            }

            HttpClient httpClient = new HttpClient();
            string link = $"http://localhost:5112/api/Schedules/{id}";
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(link);
                if (response.IsSuccessStatusCode)
                {
                    return new DeleteAppointmentResult
                    {
                        Success = true
                    };
                }
                else
                {
                    return new DeleteAppointmentResult
                    {
                        Success = false,
                        TokenInvalid = false
                    };
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                return new DeleteAppointmentResult
                {
                    Success = false,
                    TokenInvalid = false    
                };
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
            var result = await updateAppointmentWithId(appointment.id, appointment);
            if(result.Success == false && result.TokenInvalid == true)
            {
                LogoutAndRedirectToLogin();
                return;
            }

            var cacheconnection = RedisConnection.connection.GetDatabase();
            string userid = TokenHelper.getUserIdFromAccessToken(cacheconnection.StringGet("accessToken"));

            AppointmentResult result_2 = await GetAppointments(appointment.Date,userid);

            if (result_2.Appointments == null && result_2.IsTokenValid == false)
            {
                LogoutAndRedirectToLogin();
                return;
            }
            UpdateFlowLayoutPanel(result_2.Appointments);
            uc.setFieldReadOnly(true);
            uc.changeTextValueOfButtonEdit();
            uc.isEditing = false;
        }

        public async Task<UpdateAppointmentResult> updateAppointmentWithId(int id,Appointment appointment)
        {
            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            var refreshToken = cacheconnection.StringGet("refreshToken");

            TokenModel tokenModel = new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };

            var validateToken = await TokenHelper.checkToken(tokenModel);
            if(validateToken == null)
            {
                return new UpdateAppointmentResult
                {
                    Success = false,
                    TokenInvalid = true,
                    ErrorMessage = "Token has expire and invalid"
                };
            }
            if(validateToken.Success == true)
            {
                cacheconnection.StringSet("accessToken", validateToken.accessToken);
            }

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
                    
                    return new UpdateAppointmentResult
                    {
                        Success = true
                    };
                }
                else
                {
                    MessageBox.Show("Chỉnh sửa thất bại!");
                    return new UpdateAppointmentResult
                    {
                        Success = false,
                        TokenInvalid = false
                    };
                }

            }
            catch (Exception ex) {
                Console.WriteLine($"Error occurred: {ex.Message}");
                return new UpdateAppointmentResult
                {
                    Success = false,
                    TokenInvalid = false
                };
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

        public async Task<ApiResponse> checkToken(TokenModel tokenModel)
        {
            HttpClient httpClient = new HttpClient();
            string link = "http://localhost:5112/api/User/RenewToken";
            string jsonContent = JsonConvert.SerializeObject(tokenModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(link, content);
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorContent);

                if (response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.Conflict)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseData);
                    return apiResponse;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AddApointmentResult> addNewAppointment(DateTime dateTime)
        {
            //checkToken
            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            var refreshToken = cacheconnection.StringGet("refreshToken");

            TokenModel tokenModel = new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

            var validateToken = await TokenHelper.checkToken(tokenModel);
            if(validateToken == null)
            {
                //return new AddAppointmentResult
                //{
                //    Success = false,
                //    TokenInvalid = true,
                //    ErrorMessage = "Token không hợp lệ hoặc đã hết hạn."
                //};
                return new AddApointmentResult
                {
                    Success = false,
                    TokenInvalid = true,
                    ErrorMessage = "Token has expire or invalid"
                };
            }
            if(validateToken.Success == true)
            {
                cacheconnection.StringSet("accessToken", validateToken.accessToken);
            }

            HttpClient httpClient = new HttpClient();
            
            Appointment appointment = new Appointment() { Date = dtpkDate.Value };
            string dateStr = dateTime.ToString("yyyy-MM-dd");
            string userId = TokenHelper.getUserIdFromAccessToken(cacheconnection.StringGet("accessToken"));

            var jsonData = new {
                dateTime = dateStr,
                user_id = userId
            };

            string jsonString = JsonConvert.SerializeObject(jsonData);
            
            string link = $"http://localhost:5112/api/Schedules/insertByDate";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cacheconnection.StringGet("accessToken"));
            //appointments.Add(appointment);
            try
            {
                HttpContent content = new StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");
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
                    return new AddApointmentResult
                    {
                        Success = true
                    };
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error: {response.StatusCode}\nContent: {errorContent}");
                    return new AddApointmentResult
                    {
                        Success = false,
                        TokenInvalid = false,
                        ErrorMessage = errorContent
                    };
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new AddApointmentResult
                {
                    Success = false,
                    TokenInvalid = false,
                    ErrorMessage = ex.Message
                };
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

        // 
        public async void mnsThemViec_Click(object sender, EventArgs e)
        {
            var cacheconnection = RedisConnection.connection.GetDatabase();
            var accessToken = cacheconnection.StringGet("accessToken");
            var refreshToken = cacheconnection.StringGet("refreshToken");

            //TokenModel tokenModel = new TokenModel
            //{
            //    AccessToken = accessToken,
            //    RefreshToken = refreshToken
            //};

            //var validateToken = await TokenHelper.checkToken(tokenModel);
            //if (validateToken == null)
            //{
            //    MessageBox.Show("Unthorization");
            //    this.Close();
            //    await cacheconnection.KeyDeleteAsync("accessToken");
            //    await cacheconnection.KeyDeleteAsync("refreshToken");

            //    LoginForm login = new LoginForm();
            //    login.Show();
            //    return;

            //}
            //if (validateToken.Success == true)
            //{

            //    cacheconnection.StringSet("accessToken", validateToken.accessToken);

            //}
            string userId = TokenHelper.getUserIdFromAccessToken(accessToken);
            AppointmentResult result = await GetAppointments(dtpkDate.Value,userId);
            var access = cacheconnection.StringGet("accessToken");
            Console.WriteLine(access);
            Console.WriteLine(result);
            
            if (result.Appointments == null && result.IsTokenValid == false)
            {
                LogoutAndRedirectToLogin();
                return;
            }
            if (result.Appointments.Count == 0)
            {
                AJobTitle aJobTitle = new AJobTitle();
                fPanel.Controls.Add(aJobTitle);
                lblMessageBox.Visible = false;
            }
            var addResult  = await addNewAppointment(dtpkDate.Value);
            if(addResult.Success == false && addResult.TokenInvalid == true)
            {
                LogoutAndRedirectToLogin();
                return;
            }
        }

        public async void LogoutAndRedirectToLogin()
        {
            MessageBox.Show("Unauthorized");
            foreach (Form form in Application.OpenForms)
            {
                form.Hide();
            }
            // Xóa accessToken và refreshToken khỏi cache
            var cacheconnection = RedisConnection.connection.GetDatabase();
            await cacheconnection.KeyDeleteAsync("accessToken");
            await cacheconnection.KeyDeleteAsync("refreshToken");
            LoginForm login = new LoginForm();
            login.Show();
            
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
