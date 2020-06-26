using ParkingAppReCaller.Models;
using ParkingAppReCaller.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ParkingAppReCaller.ViewModels
{
    class HistoryViewModel : INotifyPropertyChanged
    {
        bool isBusy = false;

        public string Title;
        public ObservableCollection<ParkingSpot> Spots { get; set; }
        public Command LoadSpotsCommand { get; set; }
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public HistoryViewModel()
        {
            Title = "Parking History";
            Spots = new ObservableCollection<ParkingSpot>();
            LoadSpotsCommand = new Command(async () => await ExecuteLoadSpotsCommand());

            MessagingCenter.Subscribe<NewParkingSpotPage, ParkingSpot>(this, "AddSpot", async (obj, spot) =>
            {
                var newSpot = spot as ParkingSpot;
                Spots.Insert(0, newSpot);
                await App.Database.AddItemAsync(newSpot);
            });
        }

        async Task ExecuteLoadSpotsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Spots.Clear();
                var spots = await App.Database.GetItemsSortedAsync();
                
                foreach(var spot in spots)
                {
                    Spots.Add(spot);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
