using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowCalender
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string user_name = txtUserName.Text;
            string password = txtPassword.Text;

            if(string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Bạn chưa nhập tài khoản hoặc mật khẩu!");
                return;
            }
            var apiResponse = await checkAccountLogin(user_name, password); 
            if(apiResponse != null)
            {
                //Console.WriteLine(tokenModel.AccessToken);
                //Console.WriteLine(tokenModel.RefreshToken);

                //var cacheconnection = RedisConnection.connection.GetDatabase();
                //cacheconnection.StringSet("accessToken", apiResponse.accessToken);
                //cacheconnection.StringSet("refreshToken", apiResponse.refreshToken);

                // lấy ra accessToken 
                TokenStorage tokenStorage = TokenStorage.Instance;
                tokenStorage.accesToken = apiResponse.accessToken;
                string userId = getUserIdFromAccessToken(apiResponse.accessToken);
                tokenStorage.userId = userId;

                this.Hide();

                Form1 form1 = new Form1();
                form1.Show();

                
            }
            else
            {
                MessageBox.Show("Mật khẩu và tài khoản chưa hợp lệ!");
                return;
            }
             
        }



        



        public async Task<ApiResponse> checkAccountLogin(string username, string password)
        {
            string link = "http://localhost:5112/api/User/Login";
            using (HttpClient client = new HttpClient())
            {
                var loginData = new
                {
                    Username = username,
                    Password = password
                };
                var json = JsonConvert.SerializeObject(loginData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(link, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                        

                        return apiResponse;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex) {

                    Console.WriteLine(ex.Message);
                    return null;

                }
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
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
