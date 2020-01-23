using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent.Repositories
{
    public class ControllerActionRepository : Repository<ControllerAction>, IControllerActionRepository
    {
        public ControllerActionRepository(CapInnovativeIdiaDbContext capInnovativeIdiaDbContext) : base(capInnovativeIdiaDbContext)
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
