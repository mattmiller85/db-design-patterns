using Microsoft.EntityFrameworkCore;
using models;

namespace persistence_relational
{
    public class WvuFootballDataContext : DbContext
    {
        public DbSet<Player> Employees { get; set; }
        public DbSet<Roster> Rosters { get; set; }
        public DbSet<Position> Positions { get; set; }
    }
}