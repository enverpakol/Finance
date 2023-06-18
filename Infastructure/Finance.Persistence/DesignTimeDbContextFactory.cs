using Finance.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Finance.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppData>
    {
        public AppData CreateDbContext(string[] args)
        {

            DbContextOptionsBuilder<AppData> dbContextOptionsBuilder = new();

            dbContextOptionsBuilder.UseMySql(Configurations.ConnectionString,
                ServerVersion.AutoDetect(Configurations.ConnectionString));

            return new AppData(dbContextOptionsBuilder.Options);
        }
    }
}
