using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TBL_Stats.Models
{
    public class Skater : INotifyPropertyChanged
    {
        public int SkaterId { get; set; }

        public string Name { get; set; }

        public string PositionShort { get; set; }

        public string SelectedYearRange { get; set; }

        public List<string> YearRange { get; set; }

        public bool IsGoalie { get; set; }

        private int games;
        public int Games
        {
            get { return games; }
            set
            {
                games = value;
                OnPropertyChanged();
            }
        }

        //Non-Goalie specific stats

        private int goals;
        public int Goals
        {
            get { return goals; }
            set
            {
                goals = value;
                OnPropertyChanged();
            }
        }

        private int assists;
        public int Assists
        {
            get { return assists; }
            set
            {
                assists = value;
                OnPropertyChanged();
            }
        }

        //Goalie Specific Stats

        private int shutouts;
        public int Shutouts
        {
            get { return shutouts; }
            set
            {
                shutouts = value;
                OnPropertyChanged();
            }
        }

        private int saves;
        public int Saves
        {
            get { return saves; }
            set
            {
                saves = value;
                OnPropertyChanged();
            }
        }

        private decimal savePercentage;
        public decimal SavePercentage
        {
            get { return savePercentage; }
            set
            {
                savePercentage = value;
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
