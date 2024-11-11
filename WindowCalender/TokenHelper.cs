using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace WindowCalender
{
    public class TokenHelper
    {
        public static async Task<ApiResponse> checkToken(TokenModel tokenModel)
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

        public static string getUserIdFromAccessToken(string accessToken)
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
    }
}
