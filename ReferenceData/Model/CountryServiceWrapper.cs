using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;

namespace ReferenceData
{
    public class CountryServiceWrapper
    {
        ReferenceData.CountryServiceReference.CountryServiceClient countryService;
        //Dictionary<int, Country> countryDictionary;

        public CountryServiceWrapper()
        {
            countryService = new ReferenceData.CountryServiceReference.CountryServiceClient();
            //countryDictionary = countryService.GetItemsDictionary();
        }

        public Country GetItem(int id)
        {
            //if (countryDictionary.ContainsKey(id))
            //    return countryDictionary[id];

            Country country = countryService.GetCountryById(id);
            //countryDictionary[id] = country;
            return country;
        }

        public IEnumerable<Country> GetItems()
        {
            return countryService.GetCountries();
        }
    }
}
