using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class RedisHelper
    {
        private static ConnectionMultiplexer redisConnection;

        public RedisHelper(string connectionString)
        {
            redisConnection = ConnectionMultiplexer.Connect(connectionString);
        }

        public void SetToken(string key, string value, TimeSpan? expiry = null)
        {
            var db = redisConnection.GetDatabase();
            db.StringSet(key, value, expiry);
        }

        public string GetToken(string key)
        {
            var db = redisConnection.GetDatabase();
            return db.StringGet(key);
        }
    }
}
