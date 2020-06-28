using System;
using System.Collections.Generic;

namespace models
{
    public class Roster
    {
        public string Id { get; set; }
        public int Year { get; set; }
        public List<Player> Players { get; set; }
        public string Conference { get; set; }
    }
}