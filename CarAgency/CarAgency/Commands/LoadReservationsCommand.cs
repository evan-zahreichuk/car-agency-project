using CarAgency.Stores;
using CarAgency.ViewModels;
using System;
using System.Threading.Tasks;

namespace CarAgency.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewModel;
        private readonly CarAgencyStore _carAgencyStore;

        public LoadReservationsCommand(ReservationListingViewModel viewModel, CarAgencyStore CarAgencyStore)
        {
            _viewModel = viewModel;
            _carAgencyStore = CarAgencyStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.ErrorMessage = string.Empty;
            _viewModel.IsLoading = true;

            try
            {
                await _carAgencyStore.Load();

                _viewModel.UpdateReservations(_carAgencyStore.Reservations);
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Failed to load reservations.";
            }

            _viewModel.IsLoading = false;
        }
    }
}
