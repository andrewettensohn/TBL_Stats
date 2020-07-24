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

            Team team = await App.DataManager.GetTeamAsync();
            BindingContext = team;
        }
    }
}