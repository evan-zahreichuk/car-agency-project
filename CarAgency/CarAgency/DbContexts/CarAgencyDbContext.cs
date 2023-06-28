using Microsoft.EntityFrameworkCore;
using CarAgency.DTOs;

namespace CarAgency.DbContexts
{
    public class CarAgencyDbContext : DbContext
    {
        public CarAgencyDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
