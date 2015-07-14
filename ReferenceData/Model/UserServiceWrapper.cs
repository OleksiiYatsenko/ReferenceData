using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using ReferenceData.Model;

namespace ReferenceData
{
    public class UserServiceWrapper
    {
        ReferenceData.UserServiceReference.UsersServiceClient userService;
        
        public UserServiceWrapper()
        {
            userService = new ReferenceData.UserServiceReference.UsersServiceClient();
        }

        public UserFullInfo AddOrUpdate(UserFullInfo user)
        {
            User usr = userService.AddOrUpdate(ObjectMapperManager.DefaultInstance.GetMapper<UserFullInfo, User>(App.ConfigUser).Map(user));
            return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);
        }

        public IEnumerable<UserFullInfo> GetItems()
        {
            IEnumerable<User> users = userService.GetUsers();

            foreach (User usr in users)
            {
                yield return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);

            }
        }

        public IEnumerable<UserFullInfo>GetItemsForAsync()
        {
            IEnumerable<User> users = userService.GetUsers();
            List<UserFullInfo> usf = new List<UserFullInfo>();
            foreach (User usr in users)
            {
                //yield return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);
                usf.Add(ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr));
            }
            return usf;
        }
    }
}
