using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TBL_Stats.Services;
using TBL_Stats.Views;

namespace TBL_Stats
{
    public partial class App : Application
    {
        public static DataManager DataManager { get; private set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DataManager = new DataManager(new RestService());
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
