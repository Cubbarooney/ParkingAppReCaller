using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ParkingAppReCaller.Views;
using ParkingAppReCaller.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ParkingAppReCaller
{
    public partial class App : Application
    {
        static ParkingDataStore database;
        public static ParkingDataStore Database
        {
            get
            {
                if (database == null)
                {
                    database = new ParkingDataStore();
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
