using System;
using CSTrackWebAPI.Core.DAL.Abstraction.Interfaces;
using CSTrackWebAPI.Model.Entities;

namespace CSTrackWebAPI.DAL.Abstraction.Interfaces
{
    public interface IPlayerRepository : IRepository<Player, Guid>
    {
        
    }
}