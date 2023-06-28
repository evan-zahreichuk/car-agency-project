using CarAgency.Stores;
using CarAgency.ViewModels;
using System;
using System.Threading.Tasks;

namespace CarAgency.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewModel;
        private readonly CarAgencyStore _hotelStore;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, CarAgencyStore hotelStore)
        {
            _viewModel = viewModel;
            _hotelStore = hotelStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;

            try
            {
                await _hotelStore.Load();

                _viewModel.UpdateReservations(_hotelStore.Reservations);
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations.";
            }

            _viewModel.IsLoading = false;
        }
    }
}
