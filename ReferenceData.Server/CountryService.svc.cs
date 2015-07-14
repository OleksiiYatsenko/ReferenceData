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

        public List<Model.Country> GetCountries()
        {
            return countrySrw.GetItems().Select(x => MapEntityToCountry(x)).ToList();
        }

        public Model.Country GetCountryById(int id)
        {
            return MapEntityToCountry(countrySrw.GetItem(id));
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
