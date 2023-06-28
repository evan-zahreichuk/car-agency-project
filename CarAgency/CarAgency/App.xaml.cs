using CarAgency;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarAgency.DbContexts;
using CarAgency.HostBuilders;
using CarAgency.Models;
using CarAgency.Services;
using CarAgency.Services.ReservationConflictValidators;
using CarAgency.Services.ReservationCreators;
using CarAgency.Services.ReservationProviders;
using CarAgency.Stores;
using CarAgency.ViewModels;
using System;
using System.Linq;
using System.Windows;
using CarAgency;

namespace CarAgency
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .AddViewModels()
                .ConfigureServices((hostContext, services) =>
                {
                    bool isEndToEndTest = Environment.GetCommandLineArgs().Any(a => a == "E2E");

                    if (!isEndToEndTest)
                    {
                        string connectionString = hostContext.Configuration.GetConnectionString("Default");
                        services.AddSingleton<ICarAgencyDbContextFactory>(new CarAgencyDbContextFactory(connectionString));
                    }
                    else
                    {
                        services.AddSingleton<ICarAgencyDbContextFactory>(new InMemoryCarAgencyDbContextFactory());
                    }

                    services.AddSingleton<IReservationProvider, DatabaseReservationProvider>();
                    services.AddSingleton<IReservationCreator, DatabaseReservationCreator>();
                    services.AddSingleton<IReservationConflictValidator, DatabaseReservationConflictValidator>();

                    services.AddTransient<CarList>();

                    string carAgencyDealershipName = hostContext.Configuration.GetValue<string>("CarAgencyDealershipName");
                    services.AddSingleton((s) => new CarAgencyDealership(carAgencyDealershipName, s.GetRequiredService<CarList>()));

                    services.AddSingleton<CarAgencyStore>();
                    services.AddSingleton<NavigationStore>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            ICarAgencyDbContextFactory CarAgencyDbContextFactory = _host.Services.GetRequiredService<ICarAgencyDbContextFactory>();
            using (CarAgencyDbContext dbContext = CarAgencyDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<ReservationListingViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<ReservationListingViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
