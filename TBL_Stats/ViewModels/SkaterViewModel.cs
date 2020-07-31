using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TBL_Stats.Models;
using Xamarin.Forms;

namespace TBL_Stats.ViewModels
{
    public class SkaterViewModel : INotifyPropertyChanged
    {
        //public ICommand MyCommand { private set; get; }
        public ICommand ChangeSelectedSeasonCommand { private set; get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name { private set; get; }
        public int Games { private set; get; }
        //public int Goals { private set; get; }
        //public int Assists { private set; get; }
        //public string SelectedYearRange { private set; get; }
        //public string SelectedYearRange { 
        //    private set; 
        //    get {OnPropertyChanged("")}; }
        private Skater Skater { get; set; }
        public string SelectedYearRange
        {
            get { return Skater.SelectedYearRange; }
            set
            {
                Skater.SelectedYearRange = value;
                UpdateSeason(value);
                OnPropertyChanged("SelectedYearRange");
            }
        }

        public int Goals
        {
            get { return Skater.Goals; }
            set
            {
                Skater.Goals = value;
                OnPropertyChanged("Goals");
            }
        }

        public int Assists
        {
            get { return Skater.Assists; }
            set
            {
                Skater.Assists = value;
                OnPropertyChanged("");
                //OnPropertyChanged("Assists");
            }
        }

        public List<string> YearRange { private set; get; }

        public SkaterViewModel(Skater skater)
        {
            Name = skater.Name;
            Games = skater.Games;
            //Goals = skater.Goals;
            //Assists = skater.Assists;
            //SelectedYearRange = skater.SelectedYearRange;
            YearRange = skater.YearRange;

            Skater = skater;

            //MyCommand = new Command(AddGoal);
            //ChangeSelectedSeasonCommand = new Command<string>(ChangeSelectedSeason);
        }

        async void UpdateSeason(string season)
        {
            Skater skater = await App.DataManager.GetSkaterStatsBySeasonAsync(season, Skater);
            Skater = skater;
            //Goals = skater.Goals;
        }

        //void AddGoal()
        //{
        //    Goals += 1;
        //    OnPropertyChanged("Goals");
        //}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
