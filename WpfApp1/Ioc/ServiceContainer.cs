using BL;
using DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Tourplanner.ViewModel;
using UI.ViewModel;

namespace TourPlanner.Ioc
{
    public class ServiceContainer
    {
        private ServiceProvider _serviceProvider;

        public ServiceContainer()
        {
            var services = new ServiceCollection();
            services.AddSingleton<CurrentTourViewModel>();
            services.AddSingleton<TourLogViewModel>();
            services.AddSingleton<TourViewModel>();
            services.AddSingleton<IConfiguration>((serviceProvider) =>
            {
                return new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            });
            services.AddSingleton<Connection>();
            services.AddSingleton<ReportGeneration>();
            services.AddSingleton<TourLogic>();
            services.AddSingleton<MapQuest>();
            _serviceProvider = services.BuildServiceProvider();
        }

        public TourLogViewModel TourLogViewModel
            => _serviceProvider.GetService<TourLogViewModel>();

        public TourViewModel TourViewModel
            => _serviceProvider.GetService<TourViewModel>();
        public CurrentTourViewModel CurrentTourViewModel
           => _serviceProvider.GetService<CurrentTourViewModel>();

    }
}
