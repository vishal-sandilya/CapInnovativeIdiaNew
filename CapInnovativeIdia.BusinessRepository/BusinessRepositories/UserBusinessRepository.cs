using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Domain.Commons;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using CapInnovativeIdia.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositories
{
    public class UserBusinessRepository : IUserBusinessRepository
    {
        private readonly IUnitOfWork unitOfWork;
        private Response _response = new Response();
        public UserBusinessRepository()
        {
            this.unitOfWork = UnitOfWork.PersistentUnitOfWork;
        }

        public IEnumerable<User> GetAllUser()
        {
            return unitOfWork.User.GetAll();
        }

        public User GetUserById(int id)
        {
            return unitOfWork.User.GET(id);
        }

        public CreateUserViewModel CreateUserViewModelData()
        {
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();

            createUserViewModel.Roles = unitOfWork.Role.GetAll();
            createUserViewModel.Teams = unitOfWork.Team.GetAll();

            return createUserViewModel;
        }

        public Response CreateUser(CreateUserViewModel createUserViewModel, int userid)
        {
            unitOfWork.User.CreateUser(createUserViewModel, userid);

            _response.ResponseMessage = ResponseMessageConstant.UserCreatedSuccessfully;
            _response.ResponseType = ResponseType.Success;

            return _response;
        }

        public CreateUserViewModel EditUserViewModelData(int id)
        {
            CreateUserViewModel createUserViewModel = new CreateUserViewModel();

            createUserViewModel.User = GetUserById(id);
            createUserViewModel.Roles = unitOfWork.Role.GetAll();
            createUserViewModel.Teams = unitOfWork.Team.GetAll();
            createUserViewModel.RoleIds = unitOfWork.UserAccess.Find(x => x.UserId == createUserViewModel.User.Id && x.IsActive == 1).Select(x => x.RoleId);

            return createUserViewModel;
        }

        public Response UpdateUser(CreateUserViewModel createUserViewModel, int userId)
        {

            unitOfWork.User.UpdateUser(createUserViewModel, userId);

            _response.ResponseMessage = ResponseMessageConstant.UserUpdatedSuccessfully;
            _response.ResponseType = ResponseType.Success;

            return _response;
        }

        public Response DeleteUser(int id)
        {
            var user = unitOfWork.User.Find(u => u.Id == id && u.IsActive == 1).SingleOrDefault();

            if (user != null)
            {
                unitOfWork.User.DeleteUser(id);

                _response.ResponseMessage = ResponseMessageConstant.UserDeletedSuccessfully;
                _response.ResponseType = ResponseType.Success;
            }
            else
            {
                _response.ResponseMessage = ResponseMessageConstant.RecordNotFound;
                _response.ResponseType = ResponseType.Info;
            }

            return _response;
        }


    }
}
