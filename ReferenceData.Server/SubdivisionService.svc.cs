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
        private ICache<int, Model.Subdivision> subdivisionCache = new MemoryCache<int, Model.Subdivision>();

        public SubdivisionService()
        {
            subdivisionCache.PutAll(subdivSrv.GetItems().Select(x => new KeyValuePair<int, Model.Subdivision>(x.Id, MapEntityToSubdiv(x))));
        }

        public List<Model.Subdivision> GetSubdivisions()
        {
            return subdivisionCache.GetAll().ToList();
        }

        public Model.Subdivision GetSubdivisionById(int id)
        {
            if (!subdivisionCache.Contains(id))
                subdivisionCache.Put(id, MapEntityToSubdiv(subdivSrv.GetItem(id)));

            return subdivisionCache.Get(id);
        }

        public List<Model.Subdivision> GetItemsByCountryId(int id)
        {
            return subdivisionCache.GetAll().Where(x => x.CountryId == id).ToList();
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
