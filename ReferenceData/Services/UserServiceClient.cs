using EmitMapper;
using ReferenceData.Model;
using ReferenceData.Services.Abstract;
using ReferenceData.UserServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Services
{
    class UserServiceClient : IUserServiceClient
    {
        private IUsersService userService;

        public UserServiceClient(IUsersService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<UserFullInfo> GetUsers()
        {
            foreach (var user in userService.GetUsers())
                yield return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(user);
        }

        public UserFullInfo AddOrUpdate(UserFullInfo user)
        {
            User usr = ObjectMapperManager.DefaultInstance.GetMapper<UserFullInfo, User>(App.ConfigUserFullInfo).Map(user);
            usr = userService.AddOrUpdate(usr);
            return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);
        }

        public UserFullInfo GetUser(int id)
        {
            return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(userService.GetItem(id));
        }
    }
}
