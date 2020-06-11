using ParkingAppReCaller.Models;
using ParkingAppReCaller.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingAppReCaller.ViewModels
{
    public class ParkingSpotViewModel
    {
        public NotifyTaskCompletion<ParkingSpot> ParkedCarLocation { get; private set; }

        public ParkingSpotViewModel(ParkingSpot parkingSpot)
        {
            this.ParkedCarLocation = new NotifyTaskCompletion<ParkingSpot>(parkingSpot);
        }

        public ParkingSpotViewModel()
        {
            this.ParkedCarLocation = new NotifyTaskCompletion<ParkingSpot>(ParkingSpot.CreateNewParkingSpotAsync());
        }
    }
}
