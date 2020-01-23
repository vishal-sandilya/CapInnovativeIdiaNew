using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent.RepositoriesInterface
{
    public interface IUserRepository:IRepository<User>
    {
        void CreateUser(CreateUserViewModel createUserViewModel, int userId);
        void UpdateUser(CreateUserViewModel createUserViewModel, int userId);
        void DeleteUser(int id);
    }
}
