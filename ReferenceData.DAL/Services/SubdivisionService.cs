using ReferenceData.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.DAL.Services
{
    public class SubdivisionService
    {
        public static void AddOrUpdate(Subdivision subdivision)
        {
            using (var connection = new ReferenceDataEntities())
            {
                Subdivision oldValue = connection.Subdivisions.FirstOrDefault(x => x.Id == subdivision.Id);
                if (oldValue != null)
                {
                    connection.Entry(oldValue).CurrentValues.SetValues(subdivision);
                    connection.SaveChanges();
                }
                else
                {
                    connection.Subdivisions.Add(subdivision);
                    connection.SaveChanges();
                }
            }
        }

        public IEnumerable<Subdivision> GetItems()
        {
            List<Subdivision> subdivisions;
            using (var connection = new ReferenceDataEntities())
            {
                subdivisions = connection.Subdivisions.ToList();
            }

            return subdivisions;
        }

        public Subdivision GetItem(int id)
        {
            Subdivision subdivision;
            using (var connection = new ReferenceDataEntities())
            {
                subdivision = connection.Subdivisions.FirstOrDefault(x => x.Id == id);
            }

            return subdivision;
        }

        public IEnumerable<Subdivision> GetItemsByCountryId(int countryId)
        {
            List<Subdivision> subdivisions;
            using (var connection = new ReferenceDataEntities())
            {
                subdivisions = connection.Subdivisions.Where(x => x.CountryId == countryId).ToList();
            }

            return subdivisions;
        }

        public Dictionary<int, Subdivision> GetItemsDictionary()
        {
            Dictionary<int, Subdivision> dic;
            using (var connection = new ReferenceDataEntities())
            {
                dic = connection.Subdivisions.ToDictionary(k => k.Id);
            }
            return dic;
        }
    }
}
