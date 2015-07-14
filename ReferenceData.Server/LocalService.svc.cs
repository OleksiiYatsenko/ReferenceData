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

        public List<Model.Location> GetLocations()
        {
            return locSrv.GetItems().Select(x => MapEntityToLocation(x)).ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            return MapEntityToLocation(locSrv.GetItem(id));
        }

        public List<Model.Location> GetLocationsBySubdivisionId(int id)
        {
            return locSrv.GetLocationsBySubdivisionId(id).Select(x => MapEntityToLocation(x)).ToList();
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
