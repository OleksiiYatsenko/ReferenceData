using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData
{
    public class LocationServiceWrapper
    {
        ReferenceData.LocationServiceReference.LocalServiceClient locationService;
        private ICache<int, Location> locationCache = new MemoryCache<int, Location>();

        public LocationServiceWrapper()
        {
            locationService = new LocationServiceReference.LocalServiceClient();
            locationCache.PutAll(locationService.GetLocations().Select(x => new KeyValuePair<int, Location>(x.Id, x)));
        }

        public Location GetItem(int id)
        {
            return locationCache.Get(id) ?? null;
        }

        public IEnumerable<Location> GetItemsBySubdivisionId(int id)
        {
            return locationCache.GetAll().Where(x => x.SubdivisionId == id);
        }
    }
}
