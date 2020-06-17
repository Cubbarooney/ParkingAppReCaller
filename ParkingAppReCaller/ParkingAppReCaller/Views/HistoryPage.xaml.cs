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
	public partial class HistoryPage : ContentPage
	{
        HistoryViewModel viewModel;

		public HistoryPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new HistoryViewModel();
		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ParkingSpot;
            if (item == null)
            {
                return;
            }

            await Navigation.PushAsync(new ParkingInfoPage(new ParkingSpotViewModel(item)));

            SpotsListView.SelectedItem = null;    
        }

        async void AddSpot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewParkingSpotPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if(viewModel.Spots.Count == 0)
            {
                viewModel.LoadSpotsCommand.Execute(null);
            }
        }
    }
}