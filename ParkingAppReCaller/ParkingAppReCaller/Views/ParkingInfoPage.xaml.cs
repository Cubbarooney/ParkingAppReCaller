using ParkingAppReCaller.Models;
using ParkingAppReCaller.Utils;
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