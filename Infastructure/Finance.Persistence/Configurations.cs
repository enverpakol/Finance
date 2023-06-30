using Microsoft.Extensions.Configuration;

namespace Finance.Persistence
{
    static class Configurations
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
#if (DEBUG)
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Finance.API"));
                configurationManager.AddJsonFile("appsettings.Development.json");
#endif
#if (!DEBUG)
				configurationManager.AddJsonFile("appsettings.json");
#endif
                return configurationManager.GetConnectionString("MySql");
            }
        }

        public static string RedisConnection
        {
            get
            {
                ConfigurationManager configurationManager = new();
#if (DEBUG)
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/Finance.API"));
                configurationManager.AddJsonFile("appsettings.Development.json");
#endif
#if (!DEBUG)
				configurationManager.AddJsonFile("appsettings.json");
#endif
                return configurationManager.GetConnectionString("Redis");
            }
        }
    }
}
