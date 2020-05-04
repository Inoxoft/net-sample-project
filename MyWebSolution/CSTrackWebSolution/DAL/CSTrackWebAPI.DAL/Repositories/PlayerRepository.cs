using CSTrackWebAPI.Core.DAL.Implementation;
using CSTrackWebAPI.DAL.Abstraction.Interfaces;
using CSTrackWebAPI.Model.Context.Interfaces;
using CSTrackWebAPI.Model.Entities;
using System;

namespace CSTrackWebAPI.DAL.Repositories
{
    public class PlayerRepository : BaseRepository<Player, Guid>, IPlayerRepository
    {
        public PlayerRepository(ISQLContext context) : base(context)
        {

        }
        
    }
}