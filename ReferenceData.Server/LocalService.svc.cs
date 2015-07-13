using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReferenceData.DAL.Model;
using ReferenceData.DAL.Services;
using Microsoft.Practices.Unity;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class LocalService : ILocalService
    {
        private LocationsService locSrv = Container.Instance.UnityContainer.Resolve<LocationsService>();
        private ICache<int, Model.Location> locationCache = new MemoryCache<int, Model.Location>();

        public LocalService()
        {
            locationCache.PutAll(locSrv.GetItems().Select(x => new KeyValuePair<int, Model.Location>(x.Id, MapEntityToLocation(x))));
        }

        public List<Model.Location> GetLocations()
        {
            return locationCache.GetAll().ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            if (!locationCache.Contains(id))
                locationCache.Put(id, MapEntityToLocation(locSrv.GetItem(id)));

            return locationCache.Get(id);
        }

        public List<Model.Location> GetLocationsBySubdivisionId(int id)
        {
            return locationCache.GetAll().Where(x => x.SubdivisionId == id).ToList();
        }

        private Model.Location MapEntityToLocation(Location loc)
        {
            return new Model.Location
            {
                Id = loc.Id,
                Description = loc.Description,
                SubdivisionId = loc.SubdivisionId
            };
        }
    }
}
