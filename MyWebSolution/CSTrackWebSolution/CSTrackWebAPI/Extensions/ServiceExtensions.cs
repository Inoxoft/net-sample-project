using CSTrackWebAPI.Core.DAL.Abstraction.Interfaces;
using CSTrackWebAPI.DAL.Repositories;
using CSTrackWebAPI.Managers;
using CSTrackWebAPI.Managers.Abstraction;
using CSTrackWebAPI.Model.Context;
using CSTrackWebAPI.Model.Context.Interfaces;
using CSTrackWebAPI.Service;
using CSTrackWebAPI.Service.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace CytometrixWebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(
            this IServiceCollection services)
        {
            services.AddScoped<ISQLContext, SQLContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IServiceManager, ServiceManager>();

            //Repositories
            services.Scan(
            x =>
            {
                x.FromAssemblyOf<PlayerRepository>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IRepository<,>)))
                    .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                        .AsMatchingInterface()
                        .WithScopedLifetime();

            });

            services.AddSingleton<ISeedService, SeedService>();

            services.AddSingleton<IHostedService, SeederHostedService>();


            return services;
        }

    }
}
