using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TBL_Stats.Models
{
    public class Skater : INotifyPropertyChanged
    {
        [PrimaryKey]
        public int SkaterId { get; set; }
        public string Name { get; set; }
        public string PositionShort { get; set; }
        public int JerseyNumber { get; set; }

        private string selectedYearRange;
        public string SelectedYearRange
        {
            get { return selectedYearRange; }
            set
            {
                selectedYearRange = value;
                OnPropertyChanged();
            }
        }

        private List<string> yearRange;
        public List<string> YearRange
        {
            get { return yearRange; }
            set
            {
                yearRange = value;
                OnPropertyChanged();
            }
        }

        public bool IsGoalie { get; set; }

        //Non-Goalie specific stats

        private SkaterStats regularSeasonSkaterStats;
        public SkaterStats RegularSeasonSkaterStats
        {
            get { return regularSeasonSkaterStats; }
            set
            {
                regularSeasonSkaterStats = value;
                OnPropertyChanged();
            }
        }

        private SkaterStats playoffSkaterStats;
        public SkaterStats PlayoffSkaterStats
        {
            get { return playoffSkaterStats; }
            set
            {
                playoffSkaterStats = value;
                OnPropertyChanged();
            }
        }


        //Goalie Specific Stats

        private GoalieStats regularSeasonGoalieStats;
        public GoalieStats RegularSeasonGoalieStats
        {
            get { return regularSeasonGoalieStats; }
            set
            {
                regularSeasonGoalieStats = value;
                OnPropertyChanged();
            }
        }

        private GoalieStats playoffGoalieStats;
        public GoalieStats PlayoffGoalieStats
        {
            get { return playoffGoalieStats; }
            set
            {
                playoffGoalieStats = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
