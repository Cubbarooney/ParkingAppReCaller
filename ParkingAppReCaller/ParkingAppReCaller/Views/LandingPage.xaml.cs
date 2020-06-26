using ParkingAppReCaller.Models;
using ParkingAppReCaller.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAppReCaller.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        async void OnAddButtonClicked(object sender, EventArgs args)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewParkingSpotPage()));
        }

        async void OnDirectionsButtonClicked(object sender, EventArgs args)
        {
            var recentLocation = await App.Database.GetMostRecentItemAsync();
            if (recentLocation != null && recentLocation.HasLocation)
            {
                var location = new Location((double)recentLocation.Latitude, (double)recentLocation.Longitude);
                var options = new MapLaunchOptions { NavigationMode = NavigationMode.Walking };

                await Map.OpenAsync(location, options);
            }
            else if (recentLocation == null)
            {
                await DisplayAlert(Constants.Alerts.TITLE, Constants.Alerts.NO_LOCATION, Constants.Alerts.OK);
            }
            else
            {
                await DisplayAlert(Constants.Alerts.TITLE, Constants.Alerts.NO_GPS, Constants.Alerts.OK);
            }
        }

    }
}