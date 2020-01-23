using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent
{
    public interface IUnitOfWork:IDisposable
    {
        IIdiaRepository Idia { get; }
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        ITeamRepository Team { get; }
        IAccountRepository Account { get; }
        IControllerRepository Controller { get; }
        IControllerActionRepository ControllerAction { get;  }
        IUserAccessRepository UserAccess { get; }
        IIdiaCategoryRepository IdiaCategory { get; }
        IIdiaProposalRepository IdiaProposal { get; }
        IIdiaStatusRepository IdiaStatus { get; }
        int Complete();
    }
}
