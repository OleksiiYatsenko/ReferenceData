using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ReferenceData.DAL.Model;
using ReferenceData.DAL.Services;
using Microsoft.Practices.Unity;

namespace ReferenceData.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LocalService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select LocalService.svc or LocalService.svc.cs at the Solution Explorer and start debugging.
    public class LocalService : ILocalService
    {
        LocationsService locSrv = Container.Instance.UnityContainer.Resolve<LocationsService>();

        public List<Model.Location> GetLocations()
        {
            return locSrv.GetItems().Select(x => MapEntityToLocation(x)).ToList();
        }

        public Model.Location GetLocationById(int id)
        {
            return MapEntityToLocation(locSrv.GetItem(id)); ;
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
