using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(CapInnovativeIdiaDbContext capInnovativeIdiaDbContext) : base(capInnovativeIdiaDbContext)
        {

        }
        public CapInnovativeIdiaDbContext CapInnovativeIdiaDbContext
        {
            get
            {
                return Context as CapInnovativeIdiaDbContext;
            }
        }
    }
}
