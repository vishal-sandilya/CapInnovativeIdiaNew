using CapInnovativeIdia.Domain.Commons;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface
{
    public interface IAccountBusinessRepository
    {
        User Login(string userName, string password);
        void CreatePassword(string password,string confirmPasssword);
        Response ForgotPassword(string email);
        Response ChangePassword(ChangePasswordUserViewModel changePasswordUserViewModel, int currentUserId);
        Response UpdateProfile(User user, int currentUserId);
        bool IsAuthorizedUser(int userid, string controller, string action);
    }
}
