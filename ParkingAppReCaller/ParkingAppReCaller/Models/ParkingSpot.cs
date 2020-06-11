//using Android.Locations;
using Android.Content;
using Android.Locations;
using ParkingAppReCaller.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAppReCaller.Models
{
    public class ParkingSpot
    {
        private DateTime _dateParked;
        private Guid _ID;
        private Location _parkingLocation;

        public readonly static string FORMAT_STRING = "dd-MMM-yyyy hh:mmtt";
        public string ID { get { return _ID.ToString(); } }
        public string Notes;
        public string DateParked
        {
            get
            {
                return _dateParked.ToLocalTime().ToString(FORMAT_STRING).ToUpper();
            }
        }
        public string Latitude { get { return _parkingLocation.Latitude.ToString(); } }
        public string Longitude { get { return _parkingLocation.Longitude.ToString(); } }
        public string LatLon { get { return Latitude + ", " + Longitude; } }

        private ParkingSpot() { }

        public static async Task<ParkingSpot> CreateNewParkingSpotAsync(string notes = null)
        {
            return new ParkingSpot()
            {
                _dateParked = DateTime.UtcNow,
                Notes = notes == null ? notes : string.Empty,
                _ID = Guid.NewGuid(),
                _parkingLocation = await DependencyService.Get<IDeviceLocationService>().GetLocationAsync()
            };
        }
    }
}
