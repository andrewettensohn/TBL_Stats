using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using TBL_Stats.Models;
using TBL_Stats.ViewModels;

namespace TBL_Stats.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        string CurrentSeason;
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Team, (NavigationPage)Detail);

            CurrentSeason = $"{DateTime.Now.AddYears(-1).Year}{DateTime.Now.Year}";
        }

        public async Task NavigateFromMenu(HomeMenuItem selectedItem)
        {
            int id = (int)selectedItem.Id;

            if (MenuPages.ContainsKey(2))
            {
                MenuPages.Remove(id);
            }

            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Browse:
                        Skater skater = await App.DataManager.GetSkaterStatsBySeasonAsync(CurrentSeason, selectedItem.Skater);
                        MenuPages.Add(id, new NavigationPage(new SkaterPage(skater)));
                        //if(skater.IsGoalie)
                        //{
                        //    MenuPages.Add(id, new NavigationPage(new SkaterPage(skater)));
                        //}
                        //else
                        //{
                        //    MenuPages.Add(id, new NavigationPage(new SkaterPage(skater)));
                        //}
                        break;
                    case (int)MenuItemType.Team:
                        MenuPages.Add(id, new NavigationPage(new TeamPage()));
                        break;
                }
            }

            NavigationPage newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}