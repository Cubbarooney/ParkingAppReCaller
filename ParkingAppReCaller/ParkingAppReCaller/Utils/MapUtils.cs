using ParkingAppReCaller.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace ParkingAppReCaller.Utils
{
    public static class MapUtils
    {
        public static void MoveMapToLocation(Map map, ParkingSpot spot)
        {
            map.IsShowingUser = true;

            if (spot.HasLocation)
            {
                var pos = new Position((double)spot.Latitude, (double)spot.Longitude);
                var pin = new Pin
                {
                    Label = "Parking Spot: " + spot.DateString,
                    Address = spot.Notes,
                    Position = pos,
                    Type = PinType.Place,
                };

                map.MoveToRegion(new MapSpan(pos, 0.01, 0.01));
                map.Pins.Add(pin);
            }
        }
    }
}
