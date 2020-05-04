using CSTrackWebAPI.Core.Model.Abstraction.Entities;
using System;
using System.Collections.Generic;

namespace CSTrackWebAPI.Model.Entities
{
    public class Player : CoreEntity<Guid>
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
