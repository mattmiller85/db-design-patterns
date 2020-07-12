using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace models
{
    public class Roster
    {
        public string Id { get; set; }
        public int Year { get; set; }
        [JsonIgnore]
        public List<PlayerRoster> PlayerRosters { get; set; }
        public string Conference { get; set; }
        [NotMapped]
        public List<Player> Players { get; set; }
    }
}