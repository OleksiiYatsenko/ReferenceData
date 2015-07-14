using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using ReferenceData.Model;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;
using ReferenceData.SubdivisionServiceReference;

namespace ReferenceData.Service
{
    public class CachingSubdivisionService : ISubdivisionService
    {
        private ISubdivisionService subdivisionService;

        private ICache<string, Subdivision> subdivisionCache = new MemoryCache<Subdivision>();
        private ICache<string, Subdivision[]> subdivisionsByCountryCache = new MemoryCache<Subdivision[]>();

        public CachingSubdivisionService(ISubdivisionService subdivisionService)
        {
            this.subdivisionService = subdivisionService;
            // Pre-load data to cache
            GetSubdivisions();
        }

        public Subdivision[] GetSubdivisions()
        {
            var result = subdivisionService.GetSubdivisions();
            subdivisionCache.PutAll(result.Select(x => new KeyValuePair<string, Subdivision>(x.Id.ToString(), x)));
            subdivisionsByCountryCache.PutAll( from s in result group s by s.CountryId into g select new KeyValuePair<string, Subdivision[]>(g.Key.ToString(), g.ToArray()) );
            return result;
        }
        
        public Task<Subdivision[]> GetSubdivisionsAsync()
        {
            return Task.Factory.StartNew(() => GetSubdivisions());
        }
        
        public Subdivision GetSubdivisionById(int id)
        {
            var idStr = id.ToString();
            Subdivision subdivision;
            if (subdivisionCache.TryGet(idStr, out subdivision))
                return subdivision;

            subdivision = subdivisionService.GetSubdivisionById(id);
            subdivisionCache.Put(idStr, subdivision);
            return subdivision;
        }
        
        public Task<Subdivision> GetSubdivisionByIdAsync(int id)
        {
            return Task.Factory.StartNew(() => GetSubdivisionById(id));
        }
        
        public Subdivision[] GetItemsByCountryId(int id)
        {
            var idStr = id.ToString();
            Subdivision[] subdivisions;
            if (subdivisionsByCountryCache.TryGet(idStr, out subdivisions))
                return subdivisions;

            subdivisions = subdivisionService.GetSubdivisions();
            subdivisionsByCountryCache.Put(idStr, subdivisions);
            return subdivisions;
        }
        
        public Task<Subdivision[]> GetItemsByCountryIdAsync(int id)
        {
            return Task.Factory.StartNew(() => GetItemsByCountryId(id));
        }
    }
}
