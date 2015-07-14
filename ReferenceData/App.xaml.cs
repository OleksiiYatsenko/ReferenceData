using EmitMapper.MappingConfiguration;
using Microsoft.Practices.Unity;
using System.Windows;
using ReferenceData.Model;
using ReferenceData.UserServiceReference;
using ReferenceData.Service;
using ReferenceData.LocationServiceReference;
using ReferenceData.Services;
using ReferenceData.SubdivisionServiceReference;
using ReferenceData.CountryServiceReference;

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

        private static ICountryService csw;
        private static ILocalService lsw;
        private static ISubdivisionService ssw;

        protected override void OnStartup(StartupEventArgs e)
        {
            Container = new UnityContainer();
            Container.RegisterInstance<IUsersService>(new CachingUserService(new UsersServiceClient()));
            Container.RegisterInstance<ILocalService>(new CachingLocationService(new LocalServiceClient()));
            Container.RegisterInstance<ISubdivisionService>(new CachingSubdivisionService(new SubdivisionServiceClient()));
            Container.RegisterInstance<ICountryService>(new CachingCountryService(new CountryServiceClient()));
            InitializeConfigs();
            base.OnStartup(e);
        }

        private void InitializeConfigs()
        {
            csw = Container.Resolve<ICountryService>();
            lsw = Container.Resolve<ILocalService>();
            ssw = Container.Resolve<ISubdivisionService>();

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
                        Country = csw.GetCountryById(usr.CountryId),
                        Subdivision = usr.SubdivisionId != null ? ssw.GetSubdivisionById((int)usr.SubdivisionId) : null,
                        Location = usr.LocationId != null ? lsw.GetLocationById((int)usr.LocationId) : null
                        
                    });
        }
    }
}
