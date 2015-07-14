using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;
using ReferenceData.LocationServiceReference;

namespace ReferenceData.Services
{
    public class CachingLocationService : ILocalService
    {
        private ILocalService locationService;

        private ICache<string, Location> locationCache = new MemoryCache<Location>();
        private ICache<string, Location[]> locationsBySubdivisionsCache = new MemoryCache<Location[]>();

        public CachingLocationService(ILocalService locationService)
        {
            this.locationService = locationService;
            // Pre-load data to cache
            GetLocations();
        }

        public Task<Location[]> GetLocationsAsync()
        {
            return Task.Factory.StartNew(() => GetLocations());
        }

        public Location GetLocationById(int id)
        {
            var idStr = id.ToString();
            Location location;
            if (locationCache.TryGet(idStr, out location))
                return location;

            location = locationService.GetLocationById(id);
            locationCache.Put(idStr, location);
            return location;
        }

        public Location[] GetLocations()
        {
            var result = locationService.GetLocations();
            locationCache.PutAll(result.Select(x => new KeyValuePair<string, Location>(x.Id.ToString(), x)));
            locationsBySubdivisionsCache.PutAll( from l in result group l by l.SubdivisionId into g select new KeyValuePair<string, Location[]>(g.Key.ToString(), g.ToArray()) );
            return result;
        }

        public Task<Location> GetLocationByIdAsync(int id)
        {
            return Task.Factory.StartNew(() => GetLocationById(id));
        }

        public Location[] GetLocationsBySubdivisionId(int id)
        {
            var idStr = id.ToString();
            Location[] locations;
            if (locationsBySubdivisionsCache.TryGet(idStr, out locations))
                return locations;

            locations = locationService.GetLocationsBySubdivisionId(id);
            locationsBySubdivisionsCache.Put(idStr, locations);
            return locations;
        }

        public Task<Location[]> GetLocationsBySubdivisionIdAsync(int id)
        {
            return Task.Factory.StartNew(() => GetLocationsBySubdivisionId(id));
        }
    }
}
