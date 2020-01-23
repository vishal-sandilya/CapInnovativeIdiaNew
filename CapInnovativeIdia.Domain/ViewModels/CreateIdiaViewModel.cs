using CapInnovativeIdia.Domain.Domains;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Domain.ViewModels
{
    public class CreateIdiaViewModel
    {
        public Idia Idia { get; set; }
        public List<Team> Teams { get; set; }
        public List<IdiaCategory> IdiaCategories { get; set; }
        public List<IdiaProposal> IdiaProposals { get; set; }

    }
}
