using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace models
{
    public class Roster
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public List<PlayerRoster> PlayerRosters { get; set; }
        public string Conference { get; set; }
        [NotMapped]
        public List<Player> Players { get; set; }
    }
}