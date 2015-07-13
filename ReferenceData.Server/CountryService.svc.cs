using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;
using ReferenceData.DAL.Model;
using ReferenceData.DAL.Services;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class CountryService : ICountryService
    {
        private CountriesService countrySrw = Container.Instance.UnityContainer.Resolve<CountriesService>();
        private ICache<int, Model.Country> countryCache = new MemoryCache<int, Model.Country>();

        public CountryService()
        {
            countryCache.PutAll(countrySrw.GetItems().Select(x => new KeyValuePair<int, Model.Country>(x.Id, MapEntityToCountry(x))));
        }

        public List<Model.Country> GetCountries()
        {
            return countryCache.GetAll().ToList();
        }

        public Model.Country GetCountryById(int id)
        {
            if (!countryCache.Contains(id))
                countryCache.Put(id, MapEntityToCountry(countrySrw.GetItem(id)));

            return countryCache.Get(id);
        }

        private Model.Country MapEntityToCountry(Country country)
        {
            return new Model.Country
            {
                Id = country.Id,
                Description = country.Description
            };
        }
    }
}
