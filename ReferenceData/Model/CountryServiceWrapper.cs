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
    public class CountryServiceWrapper
    {
        ReferenceData.CountryServiceReference.CountryServiceClient countryService;
        private ICache<int, Country> countryCache = new MemoryCache<int, Country>();

        public CountryServiceWrapper()
        {
            countryService = new ReferenceData.CountryServiceReference.CountryServiceClient();
            countryCache.PutAll(countryService.GetCountries().Select(x => new KeyValuePair<int, Country>(x.Id, x)));
        }

        public Country GetItem(int id)
        {
            return countryCache.Get(id) ?? null;
        }

        public IEnumerable<Country> GetItems()
        {
            return countryCache.GetAll();
        }
    }
}
