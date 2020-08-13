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
        public ICommand ChangeSelectedSeasonCommand { private set; get; }

        //private Skater _Skater { get; set; }
        public Skater Skater { get; set; }
        public string SelectedYearRange
        {
            get { return Skater.SelectedYearRange; }
            set
            {
                Skater.SelectedYearRange = value;
                UpdateStats(value);
                OnPropertyChanged("SelectedYearRange");
            }
        }

        //public int Games
        //{
        //    get { return Skater.Games; }
        //    set
        //    {
        //        Skater.Games = value;
        //        OnPropertyChanged("Games");
        //    }
        //}

        //public int Goals
        //{
        //    get { return Skater.Goals; }
        //    set
        //    {
        //        Skater.Goals = value;
        //        OnPropertyChanged("Goals");
        //    }
        //}

        //public int Assists
        //{
        //    get { return Skater.Assists; }
        //    set
        //    {
        //        Skater.Assists = value;
        //        OnPropertyChanged("Assists");
        //    }
        //}

        public string Name { private set; get; }
        public List<string> YearRange { private set; get; }

        public SkaterViewModel(Skater skater)
        {
            Name = skater.Name;
            YearRange = skater.YearRange;
            Skater = skater;
        }

        async Task UpdateStats(string season)
        {
            Skater = await App.DataManager.GetSkaterStatsBySeasonAsync(season, Skater);
            //Goals = skater.Goals;
            //Assists = skater.Assists;
            //Games = skater.Games;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
