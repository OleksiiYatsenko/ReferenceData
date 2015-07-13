using ReferenceData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReferenceData.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ILocalService" in both code and config file together.
    [ServiceContract]
    public interface ILocalService
    {
        [OperationContract]
        List<Location> GetLocations();

        [OperationContract]
        Location GetLocationById(int id);

        [OperationContract]
        List<Model.Location> GetLocationsBySubdivisionId(int id);
    }
}
