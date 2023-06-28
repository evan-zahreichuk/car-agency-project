using CarAgency.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarAgency.Services.ReservationProviders
{
    public interface IReservationProvider
    {
        Task<IEnumerable<Reservation>> GetAllReservations();
    }
}
