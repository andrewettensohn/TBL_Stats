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

        public int Games { get; set; }

        public int Goals { get; set; }

        public int Assists { get; set; }

        public string SelectedYearRange { get; set; }

        public List<string> YearRange { get; set; }
    }
}
