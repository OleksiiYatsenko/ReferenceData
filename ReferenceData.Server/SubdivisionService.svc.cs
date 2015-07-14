using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;
using ReferenceData.DAL.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class SubdivisionService : ISubdivisionService
    {
        private ReferenceData.DAL.Services.SubdivisionService subdivSrv = Container.Instance.UnityContainer.Resolve<ReferenceData.DAL.Services.SubdivisionService>();

        public List<Model.Subdivision> GetSubdivisions()
        {
            return subdivSrv.GetItems().Select(x => MapEntityToSubdiv(x)).ToList();
        }

        public Model.Subdivision GetSubdivisionById(int id)
        {
            return MapEntityToSubdiv(subdivSrv.GetItem(id));
        }

        public List<Model.Subdivision> GetItemsByCountryId(int id)
        {
            return subdivSrv.GetItemsByCountryId(id).Select(x => MapEntityToSubdiv(x)).ToList();
        }

        private Model.Subdivision MapEntityToSubdiv(Subdivision subdv)
        {
            return new Model.Subdivision
            {
                Id = subdv.Id,
                Description = subdv.Description,
                CountryId = subdv.CountryId
            };
        }
    }
}
