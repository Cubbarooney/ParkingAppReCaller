using ParkingAppReCaller.Models;
using ParkingAppReCaller.Services;
using ParkingAppReCaller.Utils;
using ParkingAppReCaller.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkingAppReCaller.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewParkingSpotPage : ContentPage
	{
        ParkingSpotViewModel viewModel;

		public NewParkingSpotPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new ParkingSpotViewModel();

            SetUpMoveMap();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddSpot", viewModel.ParkedCarLocation.Result);
            await Navigation.PopModalAsync();
        }

        private void SetUpMoveMap()
        {
            if (viewModel.ParkedCarLocation.IsCompleted)
            {
                MapUtils.MoveMapToLocation(map, viewModel.ParkedCarLocation.Result);
            }
            else
            {
                viewModel.ParkedCarLocation.PropertyChanged += ParkedCarLocation_PropertyChanged;
            }
        }

        private void ParkedCarLocation_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsCompleted":
                    MapUtils.MoveMapToLocation(map, viewModel.ParkedCarLocation.Result);
                    break;
            }
        }
    }
}