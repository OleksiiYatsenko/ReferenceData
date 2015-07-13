using ReferenceData.Model;
using System.Collections.Generic;
using System.ServiceModel;

namespace ReferenceData.Server
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
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
