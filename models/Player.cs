using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace models
{
    public class Player
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Position Position { get; set; }
        [NotMapped]
        public List<Roster> Rosters { get; set; }
        [JsonIgnore]
        public List<PlayerRoster> PlayerRosters { get; set; }
    }
}