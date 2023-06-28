using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CarAgency.DbContexts
{
    public class CarAgencyDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarAgencyDbContext>
    {
        public CarAgencyDbContext CreateDbContext(string[] args)
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite("Data Source=CarAgency.db").Options;

            return new CarAgencyDbContext(options);
        }
    }
}
