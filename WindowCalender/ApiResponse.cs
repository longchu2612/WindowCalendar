using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class ApiResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string accessToken { get; set; }

        public string refreshToken { get; set; }
    }
}
