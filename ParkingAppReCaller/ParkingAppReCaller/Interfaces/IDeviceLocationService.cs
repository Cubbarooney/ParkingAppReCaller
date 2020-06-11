using Android.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingAppReCaller.Interfaces
{
    public interface IDeviceLocationService
    {
        Task<Location> GetLocationAsync();
    }
}
