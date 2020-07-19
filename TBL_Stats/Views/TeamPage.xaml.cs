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
    public partial class TeamPage : ContentPage
    {
        public TeamPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Team team = await App.DataManager.GetTeamAsync();
            Team team = await App.DataManager.GetTeamAsync();
            TeamName.Text = team.TeamName;
            GamesPlayed.Text = $"Games Played: {team.GamesPlayed}";
            Wins.Text = $"Wins: {team.Wins}";
            Losses.Text = $"Losses: {team.Losses}";

            //teamName.Text = team.TeamName;
        }
    }
}