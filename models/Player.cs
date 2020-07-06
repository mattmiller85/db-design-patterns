using System;
using System.Collections.Generic;

namespace models
{
    public class Player
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Position Position { get; set; }
        public List<PlayerRoster> PlayerRosters { get; set; }
    }
}