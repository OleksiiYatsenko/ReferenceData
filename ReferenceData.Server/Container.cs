using Microsoft.Practices.Unity;
using ReferenceData.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReferenceData.Server
{
    public class Container
    {
        private static volatile Container instance;
        private static object syncRoot = new Object();

        private IUnityContainer unityContainer;
        public IUnityContainer UnityContainer { get { return unityContainer; } }

        private Container() 
        {
            unityContainer = new UnityContainer();
            unityContainer.RegisterInstance<UsersService>(new UsersService());
            unityContainer.RegisterInstance<LocationsService>(new LocationsService());
            unityContainer.RegisterInstance<ReferenceData.DAL.Services.SubdivisionService>(new ReferenceData.DAL.Services.SubdivisionService());
            unityContainer.RegisterInstance<CountriesService>(new CountriesService());
        }

        public static Container Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new Container();
                        }
                            
                    }
                }
                return instance;
            }
        }
    }
}