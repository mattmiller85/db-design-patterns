using System;
using System.Collections.Generic;

namespace models
{
    public class Roster
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public List<PlayerRoster> PlayerRosters { get; set; }
        public string Conference { get; set; }
    }
}