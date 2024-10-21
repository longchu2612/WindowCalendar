using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Xml.Schema;
using System.Security.Policy;
using System.Net.Http;
using Newtonsoft.Json;

namespace WindowCalender 
{
    public class AppointmentService
    {
        private readonly HttpClient _httpClient;

        public AppointmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Appointment>> GetAppointments(DateTime dt)
        {

            string dateStr = dt.ToString("yyyy-MM-dd");
            string link = $"http://localhost:5112/api/Schedules/dateTime/{dateStr}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(link);
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

        public async Task<Appointment> insertAppointment(DateTime dateTime)
        {
            string dateStr = dateTime.ToString("yyyy-MM-dd");
            string link = $"http://localhost:5112/api/Schedules/{dateStr}";
            try
            {
                HttpContent content = new StringContent(String.Empty);
                HttpResponseMessage response = await _httpClient.PostAsync(link, content);
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    Appointment appointment = JsonConvert.DeserializeObject<Appointment>(jsonResponse);  
                    MessageBox.Show("Add New Schedule Successfully");
                    return appointment;
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                    return null;
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> deleteAppointment(int id)
        {
            string deleteId = id.ToString();
            String url = $"http://localhost:5112/api/Schedules/{deleteId}";
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Delete Schedule Successfully");
                    return true;
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return false;
            }
        }

        //public Task<bool> deleteAppointment()
        //{
        //    String url = "";
        //}
    }
}
