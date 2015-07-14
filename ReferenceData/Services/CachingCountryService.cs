using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;
using ReferenceData.CountryServiceReference;

namespace ReferenceData.Services
{
    public class CachingCountryService : ICountryService
    {
        private ICountryService countryService;
        private ICache<string, Country> countryCache = new MemoryCache<Country>();

        public CachingCountryService(ICountryService countryService)
        {
            this.countryService = countryService;
            // Pre-load data to cache
            GetCountries();
        }
    
        public Country[] GetCountries()
        {
            var result = countryService.GetCountries();
            countryCache.PutAll(result.Select(x => new KeyValuePair<string, Country>(x.Id.ToString(), x)));
            return result;
        }
        
        public Task<Country[]> GetCountriesAsync()
        {
            return Task.Factory.StartNew(() => GetCountries());
        }
        
        public Country GetCountryById(int id)
        {
            var idStr = id.ToString();
            Country country;
            if (countryCache.TryGet(idStr, out country))
                return country;

            country = countryService.GetCountryById(id);
            countryCache.Put(idStr, country);
            return country;
        }
        
        public Task<Country> GetCountryByIdAsync(int id)
        {
            return Task.Factory.StartNew(() => GetCountryById(id));
        }
    }
}
