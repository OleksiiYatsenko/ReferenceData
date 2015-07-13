using ReferenceData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReferenceData.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISubdivisionService" in both code and config file together.
    [ServiceContract]
    public interface ISubdivisionService
    {
        [OperationContract]
        List<Subdivision> GetSubdivisions();

        [OperationContract]
        Subdivision GetSubdivisionById(int id);

        [OperationContract]
        List<Subdivision> GetItemsByCountryId(int id);
    }
}
