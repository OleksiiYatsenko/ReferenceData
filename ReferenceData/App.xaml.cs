using EmitMapper.MappingConfiguration;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReferenceData.Model;

namespace ReferenceData
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IUnityContainer Container { get; private set; }
        public static DefaultMapConfig ConfigUser;
        public static DefaultMapConfig ConfigUserFullInfo;

        private static CountryServiceWrapper csw;
        private static LocationServiceWrapper lsw;
        private static SubDivisionServiceWrapper ssw;

        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new UnityContainer();
            Container.RegisterInstance<UserServiceWrapper>(new UserServiceWrapper());
            Container.RegisterInstance<LocationServiceWrapper>(new LocationServiceWrapper());
            Container.RegisterInstance<SubDivisionServiceWrapper>(new SubDivisionServiceWrapper());
            Container.RegisterInstance<CountryServiceWrapper>(new CountryServiceWrapper());
            InitializeConfigs();
            base.OnStartup(e);
        }

        private void InitializeConfigs()
        {
            csw = Container.Resolve<CountryServiceWrapper>();
            lsw = Container.Resolve<LocationServiceWrapper>();
            ssw = Container.Resolve<SubDivisionServiceWrapper>();

            ConfigUser = new DefaultMapConfig();
                ConfigUserFullInfo = new DefaultMapConfig();
                ConfigUser.ConvertUsing((UserFullInfo usr) =>
                    new User
                    {
                        Id = usr.Id,
                        FirstName = usr.FirstName,
                        SecondName = usr.SecondName,
                        CountryId = usr.Country.Id,
                        LocationId = usr.Location.Id,
                        SubdivisionId = usr.Subdivision.Id
                    });

                ConfigUserFullInfo.ConvertUsing((User usr) =>
                    new UserFullInfo
                    {
                        Id = usr.Id,
                        FirstName = usr.FirstName,
                        SecondName = usr.SecondName,
                        Country = csw.GetItem(usr.CountryId),
                        Subdivision = usr.SubdivisionId != null ? ssw.GetItem((int)usr.SubdivisionId) : null,
                        Location = usr.LocationId != null ? lsw.GetItem((int)usr.LocationId) : null
                        
                    });
        }
    }
}
