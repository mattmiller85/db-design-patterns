using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace models
{
    public class PlayerRoster
    {
        public string PlayerId { get; set; }
        public string RosterId { get; set; }
        
        public Player Player { get; set; }
        public Roster Roster { get; set; }
    }
}