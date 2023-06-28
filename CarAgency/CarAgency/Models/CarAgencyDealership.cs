using CarAgency.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarAgency.Models
{
    public class CarAgencyDealership
    {
        private readonly CarList _carList;

        public string Name { get; }

        public CarAgencyDealership(string name, CarList carList)
        {
            Name = name;
            _carList = carList;
        }

        /// <summary>
        /// Get all reservations.
        /// </summary>
        /// <returns>All reservations in the hotel reservation book.</returns>
        public async Task<IEnumerable<Reservation>> GetAllReservations()
        {
            return await _carList.GetAllReservations();
        }

        /// <summary>
        /// Make a reservation.
        /// </summary>
        /// <param name="reservation">The incoming reservation.</param>
        /// <exception cref="InvalidReservationTimeRangeException">Thrown if reservation start time is after end time.</exception>
        /// <exception cref="ReservationConflictException">Thrown if incoming reservation conflicts with existing reservation.</exception>
        public async Task MakeReservation(Reservation reservation)
        {
            await _carList.AddReservation(reservation);
        }
    }
}
