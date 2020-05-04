using CSTrackWebAPI.Common;
using CSTrackWebAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSTrackWebAPI.Model.Context.Configurations
{
    class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.Property(c => c.Name).IsRequired(true);
            builder.Property(c => c.Age).IsRequired(true);
        }
    }
}