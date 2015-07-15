using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceData.Services.Abstract
{
    public interface IUserServiceClient
    {
        IEnumerable<UserFullInfo> GetUsers();
        UserFullInfo AddOrUpdate(UserFullInfo user);
        UserFullInfo GetUser(int id);
    }
}
