//using Android.Locations;
using Android.Content;
using Android.Locations;
using ParkingAppReCaller.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAppReCaller.Models
{
    public class ParkingSpot
    {
        public readonly static string FORMAT_STRING = "dd-MMM-yyyy hh:mmtt";
        public string LatLon { get => Latitude + ", " + Longitude; }
        public string DateString
        {
            get
            {
                return DateParked
                        .ToLocalTime()
                        .ToString(FORMAT_STRING)
                        .ToUpper();
            }
        }

        [PrimaryKey]
        public Guid ID { get; set; }
        public DateTime DateParked { get; set; }
        public string Notes { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public float Accuracy { get; set; }

        public static async Task<ParkingSpot> CreateAsync(string notes = null)
        {
            var loc = await DependencyService.Get<IDeviceLocationService>().GetLocationAsync();
            return new ParkingSpot
            {
                DateParked = DateTime.UtcNow,
                Notes = notes == null ? notes : string.Empty,
                ID = Guid.NewGuid(),
                Latitude = loc.Latitude,
                Longitude = loc.Longitude,
                Accuracy = loc.Accuracy,
            };
        }

    }
}
