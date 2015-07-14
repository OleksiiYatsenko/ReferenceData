using EmitMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using ReferenceData.Model;
using ReferenceData.UserServiceReference;
using ReferenceData.Utils.Abstract;
using ReferenceData.Utils.Concrete;

namespace ReferenceData.Service
{
    public class CachingUserService : IUsersService
    {
        private IUsersService userService;
        private ICache<string, User> usersCache = new MemoryCache<User>();

        public CachingUserService(IUsersService userService)
        {
            this.userService = userService;
        }

        //public UserFullInfo AddOrUpdate(UserFullInfo user)
        //{
        //    User usr = userService.AddOrUpdate(ObjectMapperManager.DefaultInstance.GetMapper<UserFullInfo, User>(App.ConfigUser).Map(user));
        //    return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);
        //}

        //public IEnumerable<UserFullInfo> GetItems()
        //{
        //    IEnumerable<User> users = userService.GetUsers();

        //    foreach (User usr in users)
        //    {
        //        yield return ObjectMapperManager.DefaultInstance.GetMapper<User, UserFullInfo>(App.ConfigUserFullInfo).Map(usr);

        //    }
        //}
    
        public User[] GetUsers()
        {
            var result = userService.GetUsers();
            usersCache.PutAll(result.Select(x => new KeyValuePair<string, User>(x.Id.ToString(), x)));
            return result;
        }
        
        public Task<User[]> GetUsersAsync()
        {
            return Task.Factory.StartNew(() => GetUsers());
        }
        
        public User AddOrUpdate(User user)
        {
            var newUser = userService.AddOrUpdate(user);
            usersCache.Put(newUser.Id.ToString(), newUser);
            return newUser;
        }
        
        public Task<User> AddOrUpdateAsync(User user)
        {
            return Task.Factory.StartNew(() => AddOrUpdate(user));
        }
        
        public User GetItem(int id)
        {
            var idStr = id.ToString();
            User user;
            if (usersCache.TryGet(idStr, out user))
                return user;

            user = userService.GetItem(id);
            usersCache.Put(idStr, user);
            return user;
        }
        
        public Task<User> GetItemAsync(int id)
        {
            return Task.Factory.StartNew(() => GetItem(id));
        }
    }
}
