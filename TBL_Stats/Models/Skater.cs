using System;
using System.Collections.Generic;
using System.Text;

namespace TBL_Stats.Models
{
    public class Skater
    {
        public int SkaterId { get; set; }

        public string Name { get; set; }

        public string PositionShort { get; set; }

        public string SelectedYearRange { get; set; }

        public List<string> YearRange { get; set; }

        public int Games { get; set; }

        public bool IsGoalie { get; set; }

        //Non-Goalie specific stats

        public int Goals { get; set; }

        public int Assists { get; set; }

        //Goalie specific stats

        public int Shutouts { get; set; }

        public int Saves { get; set; }

        public decimal SavePercentage { get; set; }
    }
}
