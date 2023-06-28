using CarAgency.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarAgency.Stores
{
    public class CarAgencyStore
    {
        private readonly Models.CarAgencyDealership _hotel;
        private readonly List<Reservation> _reservations;
        private Lazy<Task> _initializeLazy;

        public IEnumerable<Reservation> Reservations => _reservations;

        public event Action<Reservation> ReservationMade;

        public CarAgencyStore(Models.CarAgencyDealership hotel)
        {
            _hotel = hotel;
            _initializeLazy = new Lazy<Task>(Initialize);

            _reservations = new List<Reservation>();
        }

        public async Task Load()
        {
            try
            {
                await _initializeLazy.Value;
            }
            catch (Exception)
            {
                _initializeLazy = new Lazy<Task>(Initialize);
                throw;
            }
        }

        public async Task MakeReservation(Reservation reservation)
        {
            await _hotel.MakeReservation(reservation);

            _reservations.Add(reservation);

            OnReservationMade(reservation);
        }

        private void OnReservationMade(Reservation reservation)
        {
            ReservationMade?.Invoke(reservation);
        }

        private async Task Initialize()
        {
            IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();

            _reservations.Clear();
            _reservations.AddRange(reservations);
        }
    }
}
