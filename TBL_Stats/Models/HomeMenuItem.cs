using System;
using System.Collections.Generic;
using System.Text;

namespace TBL_Stats.Models
{
    public enum MenuItemType
    {
        Team,
        About,
        Browse
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public int SkaterId { get; set; }

        public string SkaterName { get; set; }
    }
}
