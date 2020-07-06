using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using models;

namespace persistence_relational
{
    public class RosterConfiguration : IEntityTypeConfiguration<Roster>
    {
        public void Configure(EntityTypeBuilder<Roster> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.PlayerRosters);
            builder.HasIndex(c => c.Year);
        }
    }
    
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasMany(e => e.PlayerRosters);
        }
    }
    
    public class PlayerRosterConfiguration : IEntityTypeConfiguration<PlayerRoster>
    {
        public void Configure(EntityTypeBuilder<PlayerRoster> builder)
        {
            builder.HasKey(e => new { e.PlayerId, e.RosterId });
        }
    }
}