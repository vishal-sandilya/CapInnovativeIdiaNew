using CapInnovativeIdia.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent.RepositoriesInterface
{
    public interface IAccountRepository:IRepository<User>
    {
        User ValidateUser(string userName, string password);
        int CheckUserAuthorization(int userid, string controller, string action);
    }
}
