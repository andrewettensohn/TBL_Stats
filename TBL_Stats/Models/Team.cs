using System;
using System.Collections.Generic;
using System.Text;

namespace TBL_Stats.Models
{
    public class Team
    {
        public string TeamName { get; set; }

        public int GamesPlayed { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int OverTime { get; set; }
    }
}
