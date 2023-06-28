using Microsoft.EntityFrameworkCore;

namespace CarAgency.DbContexts
{
    public class CarAgencyDbContextFactory : ICarAgencyDbContextFactory
    {
        private readonly string _connectionString;

        public CarAgencyDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public CarAgencyDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;

            return new CarAgencyDbContext(options);
        }
    }
}
