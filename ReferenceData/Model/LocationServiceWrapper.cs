using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;

namespace ReferenceData
{
    public class LocationServiceWrapper
    {
        ReferenceData.LocationServiceReference.LocalServiceClient locationService;
        //Dictionary<int, Location> locationDic;

        public LocationServiceWrapper()
        {
            locationService = new LocationServiceReference.LocalServiceClient();
            //locationDic = new Dictionary<int, Location>();
            //locationDic = locationService.GetItemsDictionary();
        }

        public Location GetItem(int id)
        {
            //if(locationDic.ContainsKey(id))
            //{
            //    return locationDic[id];
            //}

            Location loc = locationService.GetLocationById(id);
            
            //if(loc != null)
            //    locationDic.Add(loc.Id, loc);
            
            return loc;
        }

        public IEnumerable<Location> GetItemsBySubdivisionId(int id)
        {
            return locationService.GetLocationsBySubdivisionId(id);
        }
    }
}
