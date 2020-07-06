using Microsoft.EntityFrameworkCore;
using models;

namespace persistence_relational
{
    public class WvuFootballDataContext : DbContext
    {
        public WvuFootballDataContext(DbContextOptions<WvuFootballDataContext> options) : base(options)   
        {
            
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PlayerRoster> PlayerRosters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
                "Data Source=localhost;Initial Catalog=db_design_patterns_sql_dev;User Id=sa;Password=somethingStr0ng#;");
        }
    
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlayerRoster>().HasKey(e => new { e.PlayerId, e.RosterId });
        }
    }
}