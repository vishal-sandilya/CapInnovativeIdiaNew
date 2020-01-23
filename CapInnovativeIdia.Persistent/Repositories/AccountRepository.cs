using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapInnovativeIdia.Persistent.Repositories
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        public AccountRepository(CapInnovativeIdiaDbContext capInnovativeIdiaDbContext) : base(capInnovativeIdiaDbContext)
        {

        }

        public User ValidateUser(string userName, string password)
        {
           return CapInnovativeIdiaDbContext.User.Where(x => x.Email == userName && x.Password == password).SingleOrDefault();
        }
        public int CheckUserAuthorization(int userid, string controller, string action)
        {
           return CapInnovativeIdiaDbContext.Controller.
                                       Join(CapInnovativeIdiaDbContext.ControllerAction, c => c.Id, a => a.ControllerId, 
                                       (c,a)=> new
                                            { 
                                                c,a                                                                                          
                                            }).
                                        Join(CapInnovativeIdiaDbContext.UserRoleMapping, x=> x.a.RoleId, u => u.RoleId,
                                        (x,u)=> new
                                             {
                                                x,u,
                                             }).
                                        Where(qc=>qc.x.c.Name==controller && qc.x.a.Name==action && qc.x.a.RoleId==qc.u.RoleId && qc.u.UserId==userid && qc.u.IsActive==1).
                                        Select(s=>s.x.a.Id).SingleOrDefault();
        }
        public CapInnovativeIdiaDbContext CapInnovativeIdiaDbContext
        {
            get { return Context as CapInnovativeIdiaDbContext; }
        }

    }
}
