using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;

namespace ReferenceData
{
    public class SubDivisionServiceWrapper
    {
        ReferenceData.SubdivisionServiceReference.SubdivisionServiceClient subdivisionService;
        //Dictionary<int, Subdivision> subdivisionDictionary;

        public SubDivisionServiceWrapper()
        {
            subdivisionService = new ReferenceData.SubdivisionServiceReference.SubdivisionServiceClient();
            //subdivisionDictionary = subdivisionService.GetItemsDictionary();
        }

        public Subdivision GetItem(int id)
        {
            //if (subdivisionDictionary.ContainsKey(id))
            //    return subdivisionDictionary[id];

            Subdivision subdivision = subdivisionService.GetSubdivisionById(id);
            //subdivisionDictionary[id] = subdivision;
            return subdivision;
        }

        public IEnumerable<Subdivision> GetItemsByCountryId(int id)
        {
            return subdivisionService.GetItemsByCountryId(id);
        }

    }
}
