using Company.WebApplication1.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace CapInnovativeIdia.Client.ApplicationManager
{
    public class UserManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string CurrentUserEmail
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            }
        }

        public string CurrentUserName
        {
            get
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
        }

        public int CurrentUserId
        {
            get
            {
                return Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid));
            }
        }
    }
}
