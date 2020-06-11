using ParkingAppReCaller.Models;
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
	public partial class ParkingInfoPage : ContentPage
	{
        ParkingSpotViewModel viewModel;

        public ParkingInfoPage(ParkingSpotViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;

        }

		public ParkingInfoPage()
		{
			InitializeComponent ();

            viewModel = new ParkingSpotViewModel();
            BindingContext = viewModel;
		}
	}
}