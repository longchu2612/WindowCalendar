﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft;
using System.Net.Http;
using System.Net;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using NLog.LayoutRenderers;
using System.Security.Cryptography;



namespace WindowCalender
{
    public partial class Form1 : Form
    {
        #region Peoperties
        NotifyIcon notify;

        public NotifyIcon NotifyIcon { get; set; }

        string connectstring = @"Data Source=DESKTOP-2RHLRSM;Initial Catalog=CalenderAPI;Integrated Security=True;Encrypt=False";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;
        DataTable dt;

        private int clickCount = 1;

        private List<List<Button>> matrix;



        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }


        private List<String> dateOfWeek = new List<string>() { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

        private AppointmentService _appointmentService;


        #endregion

        // 
        //public Form1(AppointmentService appointmentService)
        //{
        //    InitializeComponent();

        //    notify = new NotifyIcon();
        //    loadMatrix();

        //    lstAppointment.View = View.Details;

        //    lstAppointment.Columns.Add("Nội dung", 180);
        //    lstAppointment.Columns.Add("From Time", 100);
        //    lstAppointment.Columns.Add("To Time", 100);


        //    _appointmentService = appointmentService;
        //}
        public Form1()
        {
            InitializeComponent();

            notify = new NotifyIcon();
            loadMatrix();

            lstAppointment.View = View.Details;

            lstAppointment.Columns.Add("Nội dung", 180);
            lstAppointment.Columns.Add("From Time", 100);
            lstAppointment.Columns.Add("To Time", 100);


        }


        public void loadMatrix()
        {

            Matrix = new List<List<Button>>();
            Button oldbutton = new Button() { Width = -Constant.margin, Height = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Constant.DayOfColumn; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Constant.DayOfWeek; j++) {
                    Button btn = new Button() { Width = Constant.WidthOfButton + Constant.margin, Height = Constant.HeigthOfButton };
                    btn.Location = new Point(Constant.WidthOfButton + oldbutton.Location.X, oldbutton.Location.Y);

                    //btn.Click += btn_Click;
                    btn.MouseClick += btn_MouseClick;
                    btn.MouseDown += btn_MouseDown;
                    //btn.DoubleClick += btn_DoubleClick;

                    pnlMatrix.Controls.Add(btn);
                    Matrix[i].Add(btn);

                    oldbutton = btn;
                }
                oldbutton = new Button() { Width = -Constant.margin, Height = 0, Location = new Point(0, oldbutton.Location.Y + Constant.HeigthOfButton) };

            }

            addNumberToMatrix(dtpDateTime.Value);



        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (string.IsNullOrEmpty((sender as Button).Text))
                {
                    return;
                }
                txtNotification.Visible = false;
                lstAppointment.Visible = false;
                PlanOfDate planOfDate = new PlanOfDate(new DateTime(dtpDateTime.Value.Year, dtpDateTime.Value.Month, Convert.ToInt32((sender as Button).Text)));
                planOfDate.ShowDialog();
            }
        }

        private async void btn_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Button buttonClicked = sender as Button;
                int year = dtpDateTime.Value.Year;
                int month = dtpDateTime.Value.Month;

                if (buttonClicked.Text == "")
                {
                    txtNotification.Text = "";
                    lstAppointment.Visible = false;
                    return;
                }

                DateTime dateTime = new DateTime(year, month, Convert.ToInt32(buttonClicked.Text));
                Console.WriteLine(dateTime);



                AppointmentResult result = await GetAppointmentsWithHavingReason(dateTime);


                if (result.Appointments == null && result.IsTokenValid == false)
                {
                    MessageBox.Show("Unthorization");
                    this.Close();
                    LoginForm login = new LoginForm();
                    login.Show();
                    return;
                }

                if (result.Appointments == null || result.Appointments.Count == 0)
                {
                    txtNotification.Text = "Hôm này bạn không có lịch hẹn nào!";
                    txtNotification.Visible = true;
                    lstAppointment.Visible = false;
                }
                else
                {
                    int count = result.Appointments.Count;
                    txtNotification.Text = $"Hôm nay bạn có {count} lịch hẹn!";
                    txtNotification.Visible = true;

                    lstAppointment.Visible = true;

                    lstAppointment.Items.Clear();

                    Console.WriteLine(result.Appointments);

                    foreach (var appointment in result.Appointments)
                    {
                        //ListViewItem item = new ListViewItem(appointment.Reason);
                        Console.WriteLine(appointment.reason);
                        ListViewItem item = new ListViewItem(appointment.reason);
                        string fromCoordinates = (appointment.fromX.HasValue ? appointment.fromX.Value.ToString("D2") : "00") +
                             ":" +
                             (appointment.fromY.HasValue ? appointment.fromY.Value.ToString("D2") : "00");
                        item.SubItems.Add(fromCoordinates);
                        string toCoordinates = (appointment.toX.HasValue ? appointment.toX.Value.ToString("D2") : "00") +
                           ":" +
                           (appointment.toY.HasValue ? appointment.toY.Value.ToString("D2") : "00");
                        item.SubItems.Add(toCoordinates);
                        //Console.WriteLine(item);
                        lstAppointment.Items.Add(item);
                    }

                }
            }
        }

        //private void btn_DoubleClick(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty((sender as Button).Text))
        //    {
        //        return;
        //    }
        //    txtNotification.Visible = false;
        //    lstAppointment.Visible = false;
        //    PlanOfDate planOfDate = new PlanOfDate(new DateTime(dtpDateTime.Value.Year, dtpDateTime.Value.Month, Convert.ToInt32((sender as Button).Text)));
        //    planOfDate.ShowDialog();
        //}



        private void btn_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty((sender as Button).Text))
            {
                return;
            }
            txtNotification.Visible = false;
            lstAppointment.Visible = false;
            PlanOfDate planOfDate = new PlanOfDate(new DateTime(dtpDateTime.Value.Year, dtpDateTime.Value.Month, Convert.ToInt32((sender as Button).Text)));
            planOfDate.ShowDialog();

            //if (e.Button == MouseButtons.Left)
            //{
            //    clickCount++;
            //    if (clickCount == 2)
            //    {
            //        if (string.IsNullOrEmpty((sender as Button).Text))
            //        {
            //            return;
            //        }
            //        txtNotification.Visible = false;
            //        lstAppointment.Visible = false;
            //        //HttpClient httpClient = new HttpClient();
            //        //AppointmentService appointmentService = new AppointmentService(httpClient);
            //        //Console.WriteLine(appointmentService);
            //        PlanOfDate planOfDate = new PlanOfDate(new DateTime(dtpDateTime.Value.Year, dtpDateTime.Value.Month, Convert.ToInt32((sender as Button).Text)));
            //        planOfDate.ShowDialog();
            //        clickCount = 0;

            //    }
            //    else
            //    {
            //        Task.Delay(500).ContinueWith(t => clickCount = 0);
            //    }

            //}

        }

        private async void btn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty((sender as Button).Text))
            {
                return;
            }
            txtNotification.Visible = false;
            lstAppointment.Visible = false;
            PlanOfDate planOfDate = new PlanOfDate(new DateTime(dtpDateTime.Value.Year, dtpDateTime.Value.Month, Convert.ToInt32((sender as Button).Text)));
            planOfDate.ShowDialog();

            //-------------------------------------------------------
            //Button buttonClicked = sender as Button;
            //int year = dtpDateTime.Value.Year;
            //int month = dtpDateTime.Value.Month;

            //if(buttonClicked.Text == "")
            //{
            //    txtNotification.Text = "";
            //    lstAppointment.Visible= false;
            //    return;
            //}

            //DateTime dateTime = new DateTime(year,month,Convert.ToInt32(buttonClicked.Text));
            //Console.WriteLine(dateTime);



            //AppointmentResult result = await GetAppointmentsWithHavingReason(dateTime);


            //if (result.Appointments == null && result.IsTokenValid == false)
            //{
            //    MessageBox.Show("Unthorization");
            //    this.Close();
            //    LoginForm login = new LoginForm();
            //    login.Show();
            //    return;
            //}

            //if (result.Appointments == null || result.Appointments.Count == 0)
            //{
            //        txtNotification.Text = "Hôm này bạn không có lịch hẹn nào!";
            //        txtNotification.Visible = true;
            //        lstAppointment.Visible = false;
            //}
            //else
            //{
            //        int count = result.Appointments.Count;
            //        txtNotification.Text = $"Hôm nay bạn có {count} lịch hẹn!";
            //        txtNotification.Visible = true;

            //        lstAppointment.Visible = true;

            //        lstAppointment.Items.Clear();

            //        Console.WriteLine(result.Appointments);

            //        foreach (var appointment in result.Appointments)
            //        {
            //            //ListViewItem item = new ListViewItem(appointment.Reason);
            //            Console.WriteLine(appointment.reason);
            //            ListViewItem item = new ListViewItem(appointment.reason);
            //            string fromCoordinates = (appointment.fromX.HasValue ? appointment.fromX.Value.ToString("D2") : "00") +
            //                 ":" +
            //                 (appointment.fromY.HasValue ? appointment.fromY.Value.ToString("D2") : "00");
            //            item.SubItems.Add(fromCoordinates);
            //            string toCoordinates = (appointment.toX.HasValue ? appointment.toX.Value.ToString("D2") : "00") +
            //               ":" +
            //               (appointment.toY.HasValue ? appointment.toY.Value.ToString("D2") : "00");
            //            item.SubItems.Add(toCoordinates);
            //            //Console.WriteLine(item);
            //            lstAppointment.Items.Add(item);
            //        }

            //}


        }

        public async Task<AppointmentResult> GetAppointmentsWithHavingReason(DateTime dt)
        {
            HttpClient httpClient = new HttpClient();
            TokenStorage tokenStorage = TokenStorage.Instance;
            var accessToken = tokenStorage.accesToken;
            string dateStr = dt.ToString("yyyy-MM-dd");
            string userId = tokenStorage.userId; 
            string link = $"http://localhost:5112/api/Schedules/getAllDateByUserId?dateTime={dateStr}&userId={userId}";
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

              try
              {
                    HttpResponseMessage response = await httpClient.GetAsync(link);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseData = await response.Content.ReadAsStringAsync();

                        SchedulesResponseDTO responseDTO = JsonConvert.DeserializeObject<SchedulesResponseDTO>(responseData);

                        if(responseDTO.accessToken != null)
                        {
                          tokenStorage.accesToken = responseDTO.accessToken;
                        }
 
                        return new AppointmentResult
                        {
                            Appointments = responseDTO.scheduleList,
                            IsTokenValid = true
                        };

                    }
                    else
                    {
                        return new AppointmentResult
                        {
                            Appointments = null,
                            IsTokenValid = false
                        };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                    return new AppointmentResult
                    {
                        Appointments = null,
                        IsTokenValid = false
                    };
                }
            
            
        }

        public string getUserIdFromAccessToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (!tokenHandler.CanReadToken(accessToken))
            {
                Console.WriteLine("Token không hợp lệ.");
                return null;
            }

            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "Id");


            return userIdClaim?.Value;
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
            catch (Exception ex) {
                return null;
            }
        }

        public TimeSpan convertPointToTime(Point point)
        {
            int hours = point.X;
            int minutes = point.Y;

            TimeSpan time = new TimeSpan(hours, minutes, 0);
            return time;
        }

        private EventHandler btn_Click()
        {
            throw new NotImplementedException();
        }

        void addNumberToMatrix(DateTime dateTime)
        {
            clearMatrix();
            DateTime useDate  = new DateTime(dateTime.Year,dateTime.Month,1);
            int line = 0;
            for(int i = 1; i <= DaysOfMonth(dateTime); i++)
            {
                int column = dateOfWeek.IndexOf(useDate.DayOfWeek.ToString());
                Button btn = Matrix[line][column];
                btn.Text = i.ToString();
                //btn.Click += btn_Click;

                if (isEqualDate(useDate, dateTime))
                {
                    btn.BackColor = Color.Red;
                }

                if (isEqualDate(useDate,DateTime.Now))
                {
                    btn.BackColor = Color.Yellow;
                }
                


                if (column >= 6)
                    line++;
                useDate = useDate.AddDays(1);
            }
        }

        bool isEqualDate(DateTime useDate, DateTime dateTimeNow) {
            if (useDate.Day == dateTimeNow.Day && useDate.Month == dateTimeNow.Month && useDate.Year == dateTimeNow.Year)
            {
                return true;
            }
            return false;
        }

        public int DaysOfMonth(DateTime date)
        {
            switch (date.Month) {
                case 4:
                    return 30;
                case 6:
                    return 30;
                case 9:
                    return 30;
                case 11:
                    return 30;
                case 2:
                    if ((date.Year % 4 == 0 && date.Year % 100 != 0) || date.Year % 400 == 0)
                        return 29;
                    else
                        return 28;
                default:
                    return 31;
            }
        }

        void clearMatrix()
        {
            if(Matrix == null)
            {
                return;
            }
            for(int i = 0; i < Matrix.Count; i++)
            {
                for(int j = 0; j < Matrix[i].Count; j++)
                {
                        Button btn = Matrix[i][j];
                        btn.Text = "";
                        btn.BackColor = Color.White;
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            tmNotify.Interval = 60000;
            tmNotify.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtpDateTime.Value = DateTime.Now;
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            dtpDateTime.Value = dtpDateTime.Value.AddMonths(-1);
        }

        private void pnlMatrix_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpDateTime_ValueChanged(object sender, EventArgs e)
        {
            addNumberToMatrix((sender as DateTimePicker).Value);
        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            dtpDateTime.Value = dtpDateTime.Value.AddMonths(1);
        }

        private void AddSchedule()
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            AddSchedule();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        public List<Appointment> GetAppointments(DateTime dt,int userId)
        {

            var redisHelper = new RedisHelper("localhost:6379");
            string accessToken = redisHelper.GetToken("AccessToken");
            string refreshToken = redisHelper.GetToken("RefreshToken");



            string dateStr = dt.ToString("yyyy-MM-dd");
            Console.WriteLine(dateStr);
            Console.WriteLine(dt.Date);
            Console.WriteLine(dt);
            string link = $"http://localhost:5112/api/Schedule/dateTime/{dateStr}";
            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            
            try
            {
                WebResponse res = req.GetResponse();
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(List<Appointment>));
                var data = js.ReadObject(res.GetResponseStream());
                List<Appointment> appointments = (List<Appointment>)data;
                //var appointments = JsonConvert.DeserializeObject<List<Appointment>>(data, settings);
                return appointments;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Appointment> getAllAppointments()
        {
            string link = "http://localhost:5112/api/Schedule";
            HttpWebRequest req = HttpWebRequest.CreateHttp(link);
            WebResponse res = req.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Appointment[]));
            object data = js.ReadObject(res.GetResponseStream());
            Appointment[] appointments = (Appointment[])data;
            return appointments.ToList();
        }
        
       
        

        private async void tmNotify_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(sender);
            //Console.WriteLine(e);
            //HttpClient httpClient = new HttpClient();
            //TimeSpan currentTime = DateTime.Now.TimeOfDay;

            //AppointmentResult result = await GetAppointmentsWithHavingReason(DateTime.Now);

            //if (result.Appointments == null && result.IsTokenValid == false)
            //{
            //    MessageBox.Show("Unthorization");
            //    this.Close();
            //    var cacheconnection = RedisConnection.connection.GetDatabase();
            //    await cacheconnection.KeyDeleteAsync("accessToken");
            //    await cacheconnection.KeyDeleteAsync("refreshToken");
            //    LoginForm login = new LoginForm();
            //    login.Show();
            //    return;
            //}


            //if (result.Appointments == null)
            //{
            //    return;
            //}

            //foreach (var appointment in result.Appointments)
            //{
            //    DateTime appointmentStartTime = new DateTime(
            //      DateTime.Now.Year,
            //      DateTime.Now.Month,
            //      DateTime.Now.Day,
            //      appointment.fromX ?? 0,
            //      appointment.fromY ?? 0,
            //      0);
            //    if (appointmentStartTime - DateTime.Now <= TimeSpan.FromMinutes(2) && appointmentStartTime > DateTime.Now)
            //    {
            //        String schedule = getNotifycation(appointment);
            //        notifyIcon1.ShowBalloonTip(10000, "Thông báo lịch hẹn", schedule, ToolTipIcon.Info);
            //    }
            //}
        }

        public String getNotifycation(Appointment appointment)
        {
            
            return $"Sắp đến lịch hẹn: {appointment.reason}\nGiờ bắt đầu: {appointment.fromX:D2}: {appointment.fromY:D2}\n đến {appointment.toX:D2} : {appointment.toY:D2}";
        }

        private void pnlMatrix_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("dupck");
        }

        private void pnlMatrix_MouseUp(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("muoup");
        }

        private void pnlMatrix_Click(object sender, EventArgs e)
        {
            MessageBox.Show("click");
        }

        private void pnlMatrix_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show("dupck");
        }

        private void pnlMatrix_ClientSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
