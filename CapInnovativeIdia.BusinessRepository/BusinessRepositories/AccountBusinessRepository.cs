using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Persistent;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using CapInnovativeIdia.Domain.ViewModels;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.Commons;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositories
{
    public class AccountBusinessRepository : IAccountBusinessRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Response _response=new Response();
        public AccountBusinessRepository()
        {
            this._unitOfWork = UnitOfWork.PersistentUnitOfWork;
        }
        public User Login(string userName, string password)
        {
            return _unitOfWork.Account.ValidateUser(userName, password);
        }
        public Response ChangePassword(ChangePasswordUserViewModel changePasswordUserViewModel,int currentUserId)
        {
            var user = _unitOfWork.User.Find(u => u.Id == currentUserId && u.Password == changePasswordUserViewModel.CurrentPassword && u.IsActive == 1).SingleOrDefault();

            if (user != null)
            {
                user.Password = changePasswordUserViewModel.NewPassword;
                _unitOfWork.User.Update(user);
                _unitOfWork.Complete();

                _response.ResponseMessage= ResponseMessageConstant.PasswordChangeSuccessfully;
                _response.ResponseType = ResponseType.Success;
            }
            else
            {
                _response.ResponseMessage = ResponseMessageConstant.InvalidCurrentPassword;
                _response.ResponseType = ResponseType.Error;
            }

            return _response;
        }

        public void CreatePassword(string password, string confirmPasssword)
        {
            throw new NotImplementedException();
        }

        public Response ForgotPassword(string email)
        {
            var user = _unitOfWork.User.Find(u => u.Email == email && u.IsActive == 1).SingleOrDefault();

            if (user != null)
            {
                _response.ResponseMessage = ResponseMessageConstant.ForgotPasswordLinkSent;
                _response.ResponseType = ResponseType.Success;
            }
            else
            {
                _response.ResponseMessage = ResponseMessageConstant.EmailNotRegister;
                _response.ResponseType = ResponseType.Error;
            }

            return _response;
        }

        public Response UpdateProfile(User user,int currentUserId)
        {
            var profileDetails = _unitOfWork.User.GET(user.Id);

            if (profileDetails != null)
            {
                profileDetails.FirstName = user.FirstName;
                profileDetails.LastName = user.LastName;
                profileDetails.Email = user.Email;
                profileDetails.ModifiedBy = currentUserId;
                profileDetails.ModifiedDate = DateTime.Now;

                _unitOfWork.User.Update(profileDetails);
                _unitOfWork.Complete();

                _response.ResponseMessage = ResponseMessageConstant.UpdateProfileSuccessfully;
                _response.ResponseType = ResponseType.Success;
            }

            return _response;
        }

        public bool IsAuthorizedUser(int userid, string controller, string action)
        {
           int acceessCode= _unitOfWork.Account.CheckUserAuthorization(userid, controller, action);

            if (acceessCode > 0)
            {
                return true;
            }

            else
            {
                return false;
            }
        }
    }
}
