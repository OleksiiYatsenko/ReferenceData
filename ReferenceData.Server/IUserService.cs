using ReferenceData.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace ReferenceData.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IUsersService
    {
        [OperationContract]
        List<User> GetUsers();

        [OperationContract]
        User AddOrUpdate(User user);

        [OperationContract]
        User GetItem(int id);
    }
}
