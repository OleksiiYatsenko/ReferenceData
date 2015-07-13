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
    public interface ICountryService
    {
        [OperationContract]
        List<Country> GetCountries();

        [OperationContract]
        Country GetCountryById(int id);
    }
}
