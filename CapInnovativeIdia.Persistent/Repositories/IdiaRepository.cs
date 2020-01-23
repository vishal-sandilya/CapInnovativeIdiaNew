using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using CapInnovativeIdia.Persistent.RepositoriesInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapInnovativeIdia.Persistent.Repositories
{
    class IdiaRepository:Repository<Idia>, IIdiaRepository
    {
        public IdiaRepository(CapInnovativeIdiaDbContext capInnovativeIdiaDbContext):base(capInnovativeIdiaDbContext)
        {

        }
        public List<ViewIdiaViewModel> GetAllIdiaDetails()
        {

          return CapInnovativeIdiaDbContext.Idia.Join(CapInnovativeIdiaDbContext.IdiaCategory, i => i.CategoryId, ic => ic.Id,
                                                (i, ic) => new
                                                {
                                                    i,
                                                    ic
                                                }).
                                                Join(CapInnovativeIdiaDbContext.IdiaProposal, i => i.i.IdiaProposalId, ip => ip.Id,
                                                (i, ip) => new
                                                {
                                                    i,
                                                    ip
                                                }).Join(CapInnovativeIdiaDbContext.Team, i => i.i.i.TeamId, t => t.Id,
                                                (i, t) => new
                                                {
                                                    i,
                                                    t
                                                }
                                                ).Join(CapInnovativeIdiaDbContext.IdiaStatus,i=>i.i.i.i.IdiaStatusId,icc=> icc.Id,
                                                (i, icc) =>new
                                                {
                                                    i,
                                                    icc
                                                })
                                                .Join(CapInnovativeIdiaDbContext.User, i => i.i.i.i.i.CreatedBy, u => u.Id,
                                                (i, u) => new ViewIdiaViewModel
                                                {
                                                    Id = i.i.i.i.i.Id,
                                                    Title = i.i.i.i.i.Title,
                                                    IdiaDescription= i.i.i.i.i.IdiaDescription,
                                                    IdiaBenifit= i.i.i.i.i.IdiaBenifit,
                                                    Customer=i.i.t.Name,
                                                    IdiaCategory=i.i.i.i.ic.Name,
                                                    IdiaProposal=i.i.i.ip.Name,
                                                    IdiaStatus=i.icc.Name,
                                                    IdiaStatuId=i.icc.Id,
                                                    CreatedBy= u.FirstName,
                                                    CreatedUserid= i.i.i.i.i.CreatedBy,
                                                    CreatedDate = Convert.ToDateTime(i.i.i.i.i.CreatedDate)
                                                }).ToList();
        }

        public CapInnovativeIdiaDbContext CapInnovativeIdiaDbContext {
            get { return Context as CapInnovativeIdiaDbContext; }
        }
                
    }
}
