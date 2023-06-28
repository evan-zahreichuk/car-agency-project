using CarAgency.Models;
using System.Threading.Tasks;

namespace CarAgency.Services.ReservationCreators
{
    public interface IReservationCreator
    {
        Task CreateReservation(Reservation reservation);
    }
}
