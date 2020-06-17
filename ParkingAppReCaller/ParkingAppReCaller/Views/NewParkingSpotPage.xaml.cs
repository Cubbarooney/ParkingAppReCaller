using ParkingAppReCaller.Models;
using ParkingAppReCaller.Services;
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
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddSpot", viewModel.ParkedCarLocation.Result);
            await Navigation.PopModalAsync();
        }
	}
}