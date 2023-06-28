namespace CarAgency.DbContexts
{
    public interface ICarAgencyDbContextFactory
    {
        CarAgencyDbContext CreateDbContext();
    }
}