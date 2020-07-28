using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBL_Stats.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TBL_Stats.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkaterPage : ContentPage
    {
        public SkaterPage(Skater skater)
        {
            InitializeComponent();
            BindingContext = skater;
            //OnAppearing();
            //Skater skater = Task.Run(async () => await App.DataManager.GetSkaterAsync(SkaterId)).Result;
            //PlayerName.Text = name;
            //GamesPlayed.Text = $"Games Played: {skater.Games}";
            //Goals.Text = $"Goals: {skater.Goals}";
            //Assists.Text = $"Assists: {skater.Assists}";

        }

            //protected override async void OnAppearing()
            //{
            //    Skater skater = await App.DataManager.GetSkaterAsync(skaterId);

            //}
    }
}