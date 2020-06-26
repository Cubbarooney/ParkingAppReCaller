using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAppReCaller.Utils
{
    public static class Constants
    {
        public static class Alerts
        {
            // Title
            public readonly static string TITLE = "Alert";
            // Message
            public readonly static string NO_GPS = "The most recently saved entry does not contain any location data. Please view the entry for details or create a new location.";
            public readonly static string NO_LOCATION = "There are no locations currently saved. Start by saving a location!";
            // Cancel
            public readonly static string OK = "OK";
        }

    }
}
