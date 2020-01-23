using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Domain.Commons;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.Enums;
using CapInnovativeIdia.Domain.ViewModels;
using CapInnovativeIdia.Persistent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositories
{
    public class IdiaBusinessRepository : IIdiaBusinessRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private Response _response = new Response();
        public IdiaBusinessRepository()
        {
            _unitOfWork = UnitOfWork.PersistentUnitOfWork;
        }
        public Response CreateIdia(Idia idia)
        {         
            _unitOfWork.Idia.Add(idia);
            _unitOfWork.Complete();

            _response.ResponseMessage = ResponseMessageConstant.IdiaCreatedSuccessfully;
            _response.ResponseType = ResponseTypeEnum.ResponseType.Success;

            return _response;
        }

        public Response DeleteIdia(int id, int currentUserId)
        {
            var idia = _unitOfWork.Idia.Find(i=>i.Id==id && i.CreatedBy== currentUserId).SingleOrDefault();

            if (idia != null)
            {
                _unitOfWork.Idia.Remove(idia);
                _unitOfWork.Complete();

                _response.ResponseMessage = ResponseMessageConstant.IdiaDeletedSuccessfully;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Success;
            }
            else
            {
                _response.ResponseMessage = ResponseMessageConstant.UnAuthorizedAction;
                _response.ResponseType= ResponseTypeEnum.ResponseType.Warning;
            }

            return _response;
        }

        public CreateIdiaViewModel CreateIdiaViewModelData()
        {
            CreateIdiaViewModel createIdiaViewModel = new CreateIdiaViewModel();

            createIdiaViewModel.Teams= _unitOfWork.Team.GetAll().ToList();
            createIdiaViewModel.IdiaCategories = _unitOfWork.IdiaCategory.GetAll().ToList();
            createIdiaViewModel.IdiaProposals = _unitOfWork.IdiaProposal.GetAll().ToList();

            return createIdiaViewModel;
        }

        public List<ViewIdiaViewModel> GetAllIdia()
        {
            return _unitOfWork.Idia.GetAllIdiaDetails();
        }

        public List<ViewIdiaViewModel> GetIdiaByUserId(int userid)
        {
            var idia = _unitOfWork.Idia.GetAllIdiaDetails();
            var myIdia = idia.Where(i => i.CreatedUserid == userid).ToList();

            return myIdia;
        }

        public CreateIdiaViewModel EditIdiaViewModelData(int id, int currentUserId)
        {
            CreateIdiaViewModel createIdiaViewModel = new CreateIdiaViewModel();

            createIdiaViewModel.Teams = _unitOfWork.Team.GetAll().ToList();
            createIdiaViewModel .IdiaCategories= _unitOfWork.IdiaCategory.GetAll().ToList();
            createIdiaViewModel.IdiaProposals= _unitOfWork.IdiaProposal.GetAll().ToList();
            createIdiaViewModel.Idia = _unitOfWork.Idia.Find(i=>i.Id==id && i.CreatedBy==currentUserId && i.IsActive==1).SingleOrDefault();

            return createIdiaViewModel;
        }
        public Idia GetIdiaById(int id)
        {
            return _unitOfWork.Idia.GET(id);
        }

        public Response UpdateIdia(Idia idia)
        {
            var updateIdia = _unitOfWork.Idia.GET(idia.Id);

            if (updateIdia != null)
            {
                updateIdia.Title = idia.Title;
                updateIdia.IdiaDescription = idia.IdiaDescription;
                updateIdia.IdiaBenifit = idia.IdiaBenifit;
                updateIdia.TeamId = idia.TeamId;
                updateIdia.CategoryId = idia.CategoryId;
                updateIdia.IdiaProposalId = idia.IdiaProposalId;

                _unitOfWork.Idia.Update(updateIdia);
                _unitOfWork.Complete();

                _response.ResponseMessage = ResponseMessageConstant.IdiaEditedSuccessfully;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Success;
            }

            return _response;
        }

        public Response ApproveIdia(int id, int userId) 
        {
            var updateIdia = _unitOfWork.Idia.GET(id);

            if (updateIdia != null)
            {
                updateIdia.IdiaStatusId = Convert.ToInt32(IdiaStatusEnum.IdiaStatus.Approved);
                updateIdia.ActionBy = userId;
                updateIdia.ActionDate = DateTime.Now;
                _unitOfWork.Idia.Update(updateIdia);
                _unitOfWork.Complete();

                _response.ResponseMessage = ResponseMessageConstant.IdiaApprovedSuccessfully;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Success;
            }

            else
            {
                _response.ResponseMessage = ResponseMessageConstant.RecordNotFound;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Info;
            }

            return _response;
        }

        public Response RejectIdia(int id, int userId)
        {
            var updateIdia = _unitOfWork.Idia.GET(id);

            if (updateIdia != null)
            {
                updateIdia.IdiaStatusId = Convert.ToInt32(IdiaStatusEnum.IdiaStatus.Reject);
                updateIdia.ActionBy = userId;
                updateIdia.ActionDate = DateTime.Now;
                _unitOfWork.Idia.Update(updateIdia);
                _unitOfWork.Complete();

                _response.ResponseMessage = ResponseMessageConstant.IdiaRejectedSuccessfully;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Success;
            }
            else 
            {
                _response.ResponseMessage = ResponseMessageConstant.RecordNotFound;
                _response.ResponseType = ResponseTypeEnum.ResponseType.Info;
            }

            return _response;
        }
    }
}
