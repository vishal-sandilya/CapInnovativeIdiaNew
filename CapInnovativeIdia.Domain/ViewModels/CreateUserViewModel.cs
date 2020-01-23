using CapInnovativeIdia.Domain.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.ViewModels
{
    public class CreateUserViewModel
    {
        public User User { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Team> Teams { get; set; }

        [Required(ErrorMessage ="Please select access level.")]
        public IEnumerable<int> RoleIds { get; set; }
    }
}
