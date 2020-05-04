using System;

namespace CSTrackWebAPI.Core.Model.Abstraction.Interfaces
{
    public interface ICoreEntity<TKey> where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
