using CapInnovativeIdia.Domain.Commons;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface
{
    public interface IIdiaBusinessRepository
    {
        List<ViewIdiaViewModel> GetAllIdia();
        List<ViewIdiaViewModel> GetIdiaByUserId(int userId);
        CreateIdiaViewModel CreateIdiaViewModelData();
        CreateIdiaViewModel EditIdiaViewModelData(int id,int currentUserId);
        Idia GetIdiaById(int id);
        Response CreateIdia(Idia idia);
        Response UpdateIdia(Idia idia);
        Response DeleteIdia(int id, int currentUserId);
        Response ApproveIdia(int id, int userId);
        Response RejectIdia(int id, int userId);
    }
}
