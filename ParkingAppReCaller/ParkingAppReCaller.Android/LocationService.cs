using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;

using ParkingAppReCaller.Interfaces;
using Xamarin.Forms;

[assembly: Dependency(typeof(ParkingAppReCaller.Droid.LocationService))]
namespace ParkingAppReCaller.Droid
{
    class LocationService : IDeviceLocationService
    {
        FusedLocationProviderClient fusedLocationProviderClient;

        public LocationService()
        {
            fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(Android.App.Application.Context);
        }

        public async Task<Location> GetLocationAsync()
        {
            //return await fusedLocationProviderClient.GetLastLocationAsync();
            var l = await fusedLocationProviderClient.GetLastLocationAsync();
            return l;
        }
    }
}