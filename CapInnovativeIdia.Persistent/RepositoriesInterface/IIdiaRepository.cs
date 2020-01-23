using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Persistent.RepositoriesInterface
{
    public interface IIdiaRepository:IRepository<Idia>
    {
        List<ViewIdiaViewModel> GetAllIdiaDetails();
    }
}
