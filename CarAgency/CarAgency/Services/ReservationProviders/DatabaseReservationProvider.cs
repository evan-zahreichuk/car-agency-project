using Microsoft.EntityFrameworkCore;
using CarAgency.DbContexts;
using CarAgency.DTOs;
using CarAgency.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarAgency.Services.ReservationProviders
{
    public class DatabaseReservationProvider : IReservationProvider
    {
        private readonly ICarAgencyDbContextFactory _dbContextFactory;

        public DatabaseReservationProvider(ICarAgencyDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            using (CarAgencyDbContext context = _dbContextFactory.CreateDbContext())
            {
                IEnumerable<ReservationDTO> reservationDTOs = await context.Reservations.ToListAsync();

                return reservationDTOs.Select(r => ToReservation(r));
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new CarID(dto.CarNumber), dto.Username, dto.StartTime, dto.EndTime);
        }
    }
}
