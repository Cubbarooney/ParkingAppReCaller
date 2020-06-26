using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace ParkingAppReCaller.Utils
{
    public static class GeolocationUtils
    {

        public async static Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            // TODO: Enable Location if disabled...

            return status;
        }

        public async static Task<Location> GetLocationAsync(GeolocationAccuracy desiredAccuracy)
        {
            Location loc;
            var permission = await CheckAndRequestLocationPermission();

            if (permission == PermissionStatus.Granted)
            {
                try
                {
                    var request = new GeolocationRequest(desiredAccuracy);
                    loc = await Geolocation.GetLocationAsync(request);
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                    loc = null;
                }
                catch (FeatureNotEnabledException fneEx)
                {
                    // Handle not enabled on device exception
                    loc = null;
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                    loc = null;
                }
                catch (Exception ex)
                {
                    loc = null;
                }
            }
            else
            {
                loc = null;
            }

            return loc;
        }
    }
}
