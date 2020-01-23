using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapInnovativeIdia.Client.ApplicationManager
{
    public class AuthorizationRequirementManager: IAuthorizationRequirement
    {
        public bool IsAuthorized { get; }

        public AuthorizationRequirementManager(bool isAuthorized)
        {
            this.IsAuthorized = isAuthorized;
        }
    }
}
