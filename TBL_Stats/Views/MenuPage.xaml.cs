using TBL_Stats.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TBL_Stats.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        public MenuPage()
        {
            InitializeComponent();
            OnAppearing();

            ListViewMenu.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                HomeMenuItem selectedItem = (HomeMenuItem)e.SelectedItem;
                await RootPage.NavigateFromMenu(selectedItem);
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            List<Skater> skaters = await App.DataManager.GetSkatersAsync();

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Team, Title="Tampa Bay Lightning"}
            };

            foreach (Skater skater in skaters)
            {
                menuItems.Add(new HomeMenuItem { Id = MenuItemType.Browse, Title = skater.Name, SkaterId = skater.SkaterId });
            }

            ListViewMenu.ItemsSource = menuItems;
        }
    }
}