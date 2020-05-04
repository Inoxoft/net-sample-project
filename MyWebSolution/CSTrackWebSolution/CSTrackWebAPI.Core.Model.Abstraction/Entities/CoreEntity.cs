using CSTrackWebAPI.Core.Model.Abstraction.Interfaces;
using System;

namespace CSTrackWebAPI.Core.Model.Abstraction.Entities
{
    public abstract class CoreEntity<TKey> : ICoreEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public CoreEntity()
        {
        }

        public CoreEntity(CoreEntity<TKey> coreEntity)
        {
            this.Id = coreEntity.Id;
        }

    }
}
