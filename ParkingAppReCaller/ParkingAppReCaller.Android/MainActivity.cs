using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Location;
using Android.Util;
using Android;

namespace ParkingAppReCaller.Droid
{
    [Activity(Label = "PARC", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private readonly static int REQUEST_LOCATION = 1;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            var spam = IsGooglePlayServicesInstalled();
            var eggs = IsGPSPermissionGiven();
            RequestGPSPermission();
            var spamandeggs = IsGPSPermissionGiven();

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        bool IsGooglePlayServicesInstalled()
        {
            var queryResult = Android.Gms.Common.GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (queryResult == Android.Gms.Common.ConnectionResult.Success)
            {
                Log.Info("MainActivity", "Google Play Services is installed on this device.");
                return true;
            }

            if (Android.Gms.Common.GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
            {
                // Check if there is a way the user can resolve the issue
                var errorString = Android.Gms.Common.GoogleApiAvailability.Instance.GetErrorString(queryResult);
                Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                          queryResult, errorString);

                // Alternately, display the error to the user.
            }

            return false;
        }

        bool IsGPSPermissionGiven()
        {
            if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                return true;
            }
            else
            {
                // The app does not have permission ACCESS_FINE_LOCATION 
                return false;
            }
        }

        void RequestGPSPermission()
        {
            /*if(Android.Support.V4.App.ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.AccessFineLocation))
            {
                var requiredPermissions = new String[] { Manifest.Permission.AccessFineLocation };
                Android.Support.Design.Widget.Snackbar.Make(layout, Resource.)
            }
            else {
            */
            Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION);
            //}
        }
    }
}