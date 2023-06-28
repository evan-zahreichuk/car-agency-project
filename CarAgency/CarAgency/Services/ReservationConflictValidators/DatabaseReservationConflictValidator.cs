using Microsoft.EntityFrameworkCore;
using CarAgency.DbContexts;
using CarAgency.DTOs;
using CarAgency.Models;
using System.Linq;
using System.Threading.Tasks;

namespace CarAgency.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ICarAgencyDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ICarAgencyDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (CarAgencyDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await context.Reservations
                    .Where(r => r.CarNumber == reservation.CarID.CarNumber)
                    .Where(r => r.EndTime > reservation.StartTime)
                    .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                if (reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(new CarID(dto.CarNumber), dto.Username, dto.StartTime, dto.EndTime);
        }
    }
}
