using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;
using ReferenceData.DAL.Model;
using ReferenceData.DAL.Services;

namespace ReferenceData.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CountryService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CountryService.svc or CountryService.svc.cs at the Solution Explorer and start debugging.
    public class CountryService : ICountryService
    {
        private CountriesService countrySrw = Container.Instance.UnityContainer.Resolve<CountriesService>();
        private static Dictionary<int, Model.Country> countryDic;

        public List<Model.Country> GetCountries()
        {
            List<Model.Country> tmpLst = countrySrw.GetItems().Select(x => MapEntityToCountry(x)).ToList();
            if(countryDic != null)
            {
                countryDic = tmpLst.Select(x => x).ToDictionary(x => x.Id);
            }
            return tmpLst;
        }

        public Model.Country GetCountryById(int id)
        {
            if (countryDic != null)
                return countryDic[id];

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
