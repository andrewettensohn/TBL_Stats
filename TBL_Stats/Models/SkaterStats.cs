using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TBL_Stats.Models
{
    public class SkaterStats : INotifyPropertyChanged
    {
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

        private int powerPlayGoals;
        public int PowerPlayGoals
        {
            get { return powerPlayGoals; }
            set
            {
                powerPlayGoals = value;
                OnPropertyChanged();
            }
        }

        private int powerPlayPoints;
        public int PowerPlayPoints
        {
            get { return powerPlayPoints; }
            set
            {
                powerPlayPoints = value;
                OnPropertyChanged();
            }
        }

        private int hits;
        public int Hits
        {
            get { return hits; }
            set
            {
                hits = value;
                OnPropertyChanged();
            }
        }

        private decimal faceOffPct;
        public decimal FaceOffPct
        {
            get { return faceOffPct; }
            set
            {
                faceOffPct = value;
                OnPropertyChanged();
            }
        }

        private decimal shotPct;
        public decimal ShotPct
        {
            get { return shotPct; }
            set
            {
                shotPct = value;
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
