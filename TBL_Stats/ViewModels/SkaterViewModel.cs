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
        public SkaterViewModel(Skater skater)
        {
            YearRange = skater.YearRange;
            Skater = skater;
        }

        public ICommand ChangeSelectedSeasonCommand { private set; get; }
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

        public string SelectedYear { get; set; } 
        public List<string> YearRange { private set; get; }

        async Task UpdateStats(string season)
        {
            Skater = await App.DataManager.GetSkaterStatsBySeasonAsync(season, Skater);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
