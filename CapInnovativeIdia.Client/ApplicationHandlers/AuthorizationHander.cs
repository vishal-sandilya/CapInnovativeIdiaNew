using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Client.ApplicationManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CapInnovativeIdia.Client.ApplicationHandlers
{
    public class AuthorizationHander : AuthorizationHandler<AuthorizationRequirementManager>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountBusinessRepository _accountBusinessRepository;
        private readonly UserManager _userManager;

        public AuthorizationHander(IHttpContextAccessor httpContextAccessor, IAccountBusinessRepository accountBusinessRepository,UserManager userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _accountBusinessRepository = accountBusinessRepository;
            _userManager = userManager;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirementManager requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                string controllerName = _httpContextAccessor.HttpContext.Request.RouteValues["controller"].ToString();
                string actionName = _httpContextAccessor.HttpContext.Request.RouteValues["action"].ToString();

                bool isAuthorizedUser=_accountBusinessRepository.IsAuthorizedUser(_userManager.CurrentUserId, controllerName, actionName);

                if (isAuthorizedUser)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
