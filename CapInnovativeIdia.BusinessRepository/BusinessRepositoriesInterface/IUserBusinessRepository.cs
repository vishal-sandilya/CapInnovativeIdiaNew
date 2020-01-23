using CapInnovativeIdia.Domain.Commons;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface
{
    public interface IUserBusinessRepository
    {
        User GetUserById(int id);
        IEnumerable<User> GetAllUser();
        CreateUserViewModel CreateUserViewModelData();
        CreateUserViewModel EditUserViewModelData(int id);
        Response CreateUser(CreateUserViewModel  createUserViewModel,int userid);
        Response UpdateUser(CreateUserViewModel createUserViewModel, int userId);
        Response DeleteUser(int id);

    }
}
