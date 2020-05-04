using CSTrackWebAPI.Service.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Service
{
    public class SeederHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public SeederHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var seedService = scope.ServiceProvider.GetService<ISeedService>();

                await seedService.SeedAsync();

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // noop
            return Task.CompletedTask;
        }
    }
}
