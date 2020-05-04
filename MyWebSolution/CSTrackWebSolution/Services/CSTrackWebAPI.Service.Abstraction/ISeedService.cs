using System;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Service.Abstraction
{
    public interface ISeedService : IDisposable
    {
        Task SeedAsync();
    }
}
