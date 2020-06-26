using ParkingAppReCaller.Models;
using ParkingAppReCaller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ParkingAppReCaller.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ParkingInfoPage : ContentPage
	{
        ParkingSpotViewModel viewModel;

        public ParkingInfoPage(ParkingSpotViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

            SetUpMoveMap();
        }

		public ParkingInfoPage()
		{
			InitializeComponent ();

            viewModel = new ParkingSpotViewModel();

            BindingContext = viewModel;

            SetUpMoveMap();
		}

        private void SetUpMoveMap()
        {
            if (viewModel.ParkedCarLocation.IsCompleted)
            {
                MoveMapToLocation();
            }
            else
            {
                viewModel.ParkedCarLocation.PropertyChanged += ParkedCarLocation_PropertyChanged;
            }
        }

        private void MoveMapToLocation()
        {
            var loc = viewModel.ParkedCarLocation.Result;
            var pos = new Position(loc.Latitude, loc.Longitude);
            var pin = new Pin
            {
                Label = "Parking Spot: " + loc.DateString,
                Address = loc.Notes,
                Position = pos,
                Type = PinType.Place,
            };

            map.IsShowingUser = true;
            map.MoveToRegion(new MapSpan(pos, 0.01, 0.01));
            map.Pins.Add(pin);
        }

        private void ParkedCarLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsCompleted":
                    MoveMapToLocation();
                    break;
            }
        }
    }
}