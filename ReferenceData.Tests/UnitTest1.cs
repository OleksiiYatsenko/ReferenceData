using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReferenceData.Model;
using System.Collections.Generic;

namespace ReferenceData.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestServices()
        {
            UserServiceRef.UsersServiceClient usr = new UserServiceRef.UsersServiceClient();
            CountryServiceRef.CountryServiceClient csr = new CountryServiceRef.CountryServiceClient();
            SubdivisionServiceRef.SubdivisionServiceClient ssr = new SubdivisionServiceRef.SubdivisionServiceClient();
            LocationServiceRef.LocalServiceClient lsr = new LocationServiceRef.LocalServiceClient();

            List<User> list = new List<User>(usr.GetUsers());
            
            Assert.IsNotNull(list, "Users collection is null");
            Assert.IsTrue((list.Count > 0), "Users count is less or equals 0");

            Assert.IsNotNull(ssr.GetSubdivisions(), "Subdivisions collection is null");
            Assert.IsNotNull(csr.GetCountries(), "Countries collection is null");
            Assert.IsNotNull(lsr.GetLocations(), "Locations collection is null");

            Assert.IsNotNull(csr.GetCountryById(list[0].CountryId), "Service haven't country by country id");

            Assert.IsNotNull(ssr.GetItemsByCountryId(list[0].CountryId), "Service haven't subdivisions by country id");
            Assert.IsNotNull(ssr.GetSubdivisionById((int)list[0].SubdivisionId), "Service haven't subdivisions by subdivision id");

            Assert.IsNotNull(lsr.GetLocationById((int)list[0].LocationId), "Service haven't locations by subdivisions id");
            Assert.IsNotNull(lsr.GetLocationsBySubdivisionId((int)list[0].SubdivisionId), "Service haven't locations by location id");
        }
    }
}
