using ReferenceData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReferenceData.Server
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
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
