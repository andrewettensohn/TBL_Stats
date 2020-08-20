using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TBL_Stats.Models
{
    public class GoalieStats : INotifyPropertyChanged
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

        private int powerPlaySaves;
        public int PowerPlaySaves
        {
            get { return powerPlaySaves; }
            set
            {
                powerPlaySaves = value;
                OnPropertyChanged();
            }
        }

        private int shotsAgainst;
        public int ShotsAgainst
        {
            get { return shotsAgainst; }
            set
            {
                shotsAgainst = value;
                OnPropertyChanged();
            }
        }

        private int shortHandedSaves;
        public int ShortHandedSaves
        {
            get { return shortHandedSaves; }
            set
            {
                shortHandedSaves = value;
                OnPropertyChanged();
            }
        }

        private decimal powerPlaySavePercentage;
        public decimal SavePercentage
        {
            get { return powerPlaySavePercentage; }
            set
            {
                powerPlaySavePercentage = value;
                OnPropertyChanged();
            }
        }

        private decimal shortHandedSavePercentage;
        public decimal SavePercentage
        {
            get { return shortHandedSavePercentage; }
            set
            {
                savePercentage = value;
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
