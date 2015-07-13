using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData
{
    public class SubDivisionServiceWrapper
    {
        ReferenceData.SubdivisionServiceReference.SubdivisionServiceClient subdivisionService;
        private ICache<int, Subdivision> subdivisionCache = new MemoryCache<int, Subdivision>();

        public SubDivisionServiceWrapper()
        {
            subdivisionService = new ReferenceData.SubdivisionServiceReference.SubdivisionServiceClient();
            subdivisionCache.PutAll(subdivisionService.GetSubdivisions().Select(x => new KeyValuePair<int, Subdivision>(x.Id, x)));
        }

        public Subdivision GetItem(int id)
        {
            return subdivisionCache.Get(id) ?? null;
        }

        public IEnumerable<Subdivision> GetItemsByCountryId(int id)
        {
            return subdivisionCache.GetAll().Where(x => x.CountryId == id);
        }

    }
}
