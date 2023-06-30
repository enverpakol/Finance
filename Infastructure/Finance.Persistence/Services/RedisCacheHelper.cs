using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Persistence.Services
{
    public class RedisCacheHelper
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public RedisCacheHelper()
        {
            _redisConnection = ConnectionMultiplexer.Connect(Configurations.RedisConnection);
        }

        public IDatabase GetRedisDatabase()
        {
            return _redisConnection.GetDatabase();
        }
    }
}
