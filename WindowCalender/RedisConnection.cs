using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace WindowCalender
{
    public class RedisConnection
    {
        private static Lazy<ConnectionMultiplexer> lazyconnectionMultiplexer;

        static RedisConnection() {
            RedisConnection.lazyconnectionMultiplexer = new Lazy<ConnectionMultiplexer>(
                () =>
                {
                    return ConnectionMultiplexer.Connect("localhost:6379");
                });

        }

        public static ConnectionMultiplexer connection
        {
            get
            {
                return lazyconnectionMultiplexer.Value;
            }
        }

    }
}
