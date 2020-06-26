using ParkingAppReCaller.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Android.Locations;
using SQLite;
using System.IO;
using ParkingAppReCaller.Utils;

namespace ParkingAppReCaller.Services
{
    public class ParkingDataStore
    {
        public const string DatabaseFilename = "ParcSQLite.db3";
        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache |
            SQLiteOpenFlags.ProtectionComplete;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DatabasePath, Flags);
        });
        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public ParkingDataStore()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ParkingSpot).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ParkingSpot)).ConfigureAwait(false);
                }
            }
        }

        public Task<int> AddItemAsync(ParkingSpot spot)
        {
            return Database.InsertAsync(spot);
        }

        public Task<int> DeleteItemAsync(ParkingSpot spot)
        {
            return Database.DeleteAsync(spot);
        }

        public Task<ParkingSpot> GetItemAsync(Guid id)
        {
            return Database.Table<ParkingSpot>().Where(s => s.ID == id).FirstOrDefaultAsync();
        }

        public Task<ParkingSpot> GetItemAsync(string id)
        {
            return GetItemAsync(new Guid(id));
        }

        public async Task<ParkingSpot> GetMostRecentItemAsync()
        {
            var spots = await GetItemsSortedAsync();
            return spots.FirstOrDefault();
        }

        public Task<List<ParkingSpot>> GetItemsAsync()
        {
            return Database.Table<ParkingSpot>().ToListAsync();
        }

        public async Task<List<ParkingSpot>> GetItemsSortedAsync()
        {
            var spots = await GetItemsAsync();
            spots.Sort((a, b) => DateTime.Compare(a.DateParked, b.DateParked));
            spots.Reverse();

            return spots;
        }

        public Task<int> UpdateItemAsync(ParkingSpot spot)
        {

            return Database.UpdateAsync(spot);
        }
    }
}
