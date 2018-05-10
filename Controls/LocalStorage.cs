using Akavache;
using Manofthematch.Data;
using Manofthematch.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Manofthematch.Controls
{
    public class LocalStorage
    {
        public List<Club> clubCollection = new List<Club>();
        readonly ApiMethods apiMethods = new ApiMethods();
        public Favourites Favourites = new Favourites();
        
        // returns a new device id if none exists, returns device id if it exists
        public async Task<Guid> GetCreateDeviceId()
        {

            Guid deviceIdFromLocalStorage;

            try
            {
                deviceIdFromLocalStorage = await BlobCache.UserAccount.GetObject<Guid>("deviceId");
            }
            catch (KeyNotFoundException ex)
            {
                Guid deviceId = Guid.NewGuid();
                await BlobCache.UserAccount.InsertObject("deviceId", deviceId);
                deviceIdFromLocalStorage = await BlobCache.UserAccount.GetObject<Guid>("deviceId");
            }
            
            return deviceIdFromLocalStorage;
        }

        public async void DeleteDeviceId()
        {
            try
            {
                await BlobCache.UserAccount.InvalidateObject<Guid>("deviceId");
            }
            catch (KeyNotFoundException ex)
            {

            }
        }

        public async void WriteInMemory(Favourites favourites)
        {
            clubCollection = await apiMethods.GetAllClubs("GetAllCLubs", 1083);
            favourites.FavClubs = clubCollection;
            await BlobCache.InMemory.InsertObject("favourites", Favourites);
        }
    }
}
