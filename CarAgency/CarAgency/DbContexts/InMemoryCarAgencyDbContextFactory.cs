using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace CarAgency.DbContexts
{
    public class InMemoryCarAgencyDbContextFactory : ICarAgencyDbContextFactory
    {
        private readonly SqliteConnection _connection;

        public InMemoryCarAgencyDbContextFactory()
        {
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();
        }

        public CarAgencyDbContext CreateDbContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connection).Options;

            return new CarAgencyDbContext(options);
        }
    }
}
