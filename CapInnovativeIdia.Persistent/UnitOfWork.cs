using CapInnovativeIdia.Persistent.Repositories;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent
{
    public class UnitOfWork : IUnitOfWork
    {
        private static IUnitOfWork _unitOfWork;
        private readonly CapInnovativeIdiaDbContext _context;
        private UnitOfWork()
        {
            this._context = new CapInnovativeIdiaDbContext();
            Idia = new IdiaRepository(_context);
            User = new UserRepository(_context);
            Role = new RoleRepository(_context);
            Account = new AccountRepository(_context);
            Controller = new ControllerRepository(_context);
            ControllerAction = new ControllerActionRepository(_context);
            UserAccess = new UserAccessRepository(_context);
            Team = new TeamRepository(_context);
            IdiaCategory = new IdiaCategoryRepository(_context);
            IdiaProposal = new IdiaProposalRepository(_context);
            IdiaStatus = new IdiaStatusRepository(_context);
        }

        public static IUnitOfWork PersistentUnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                    _unitOfWork= new UnitOfWork();

                return _unitOfWork;
            }
        }
        public IIdiaRepository Idia { get; private set; }
        public IUserRepository User { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IAccountRepository Account { get; private set; }
        public IControllerRepository Controller { get; private set; }
        public IUserAccessRepository UserAccess { get; private set; }
        public IControllerActionRepository ControllerAction { get; private set; }
        public ITeamRepository Team { get; private set; }
        public IIdiaCategoryRepository  IdiaCategory { get; private set; }
        public IIdiaProposalRepository  IdiaProposal { get; private set; }
        public IIdiaStatusRepository IdiaStatus { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
