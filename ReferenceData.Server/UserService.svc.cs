using Microsoft.Practices.Unity;
using ReferenceData.DAL.Model;
using ReferenceData.DAL.Services;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ReferenceData.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class UserService : IUsersService
    {
        private UsersService usrSrw = Container.Instance.UnityContainer.Resolve<UsersService>();

        public List<Model.User> GetUsers()
        {
            return usrSrw.GetItems().Select(x => MapEntityToUser(x)).ToList();
        }

        public Model.User AddOrUpdate(Model.User usr)
        {
            User user = usrSrw.AddOrUpdate(MapUserToEntity(usr));
            return MapEntityToUser(user);
        }

        public Model.User GetItem(int id)
        {
            return MapEntityToUser((usrSrw.GetItem(id)));
        }

        private User MapUserToEntity(Model.User usr)
        {
            return new User
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                SecondName = usr.SecondName,
                CountryId = usr.CountryId,
                SubDivisionId = usr.SubdivisionId,
                LocationId = usr.LocationId
            };
        }

        private Model.User MapEntityToUser(User usr)
        {
            return new ReferenceData.Model.User
            {
                Id = usr.Id,
                FirstName = usr.FirstName,
                SecondName = usr.SecondName,
                CountryId = usr.CountryId,
                SubdivisionId = usr.SubDivisionId,
                LocationId = usr.LocationId
            };
        }
    }
}
