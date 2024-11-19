using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class TokenStorage
    {
        public string accesToken { get; set; }  
        public string refreshToken { get; set; }

        public String userId { get; set; }

        private static TokenStorage _instance;

        private static readonly object _lock = new object();

        private TokenStorage() { }

        public static TokenStorage Instance
        {
            get
            {
                // Kiểm tra nếu đối tượng đã được tạo
                if (_instance == null)
                {
                    lock (_lock) // Đảm bảo thread safety
                    {
                        if (_instance == null)
                        {
                            _instance = new TokenStorage();
                        }
                    }
                }
                return _instance;
            }
        }




    }
}
