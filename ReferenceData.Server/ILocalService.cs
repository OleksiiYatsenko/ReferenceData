﻿using ReferenceData.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReferenceData.Server
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
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
