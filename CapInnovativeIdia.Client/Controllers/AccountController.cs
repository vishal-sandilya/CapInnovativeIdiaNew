using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.BusinessRepository.CommanFunctions;
using CapInnovativeIdia.Client.ApplicationManager;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.Client.Controllers
{
    [Authorize]
    public class AccountController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IAccountBusinessRepository _accountBusinessRepository;
        private readonly IUserBusinessRepository _userBusinessRepository;
        public readonly UserManager _userManager;
        public AccountController(IAccountBusinessRepository accountBusinessRepository,IUserBusinessRepository userBusinessRepository, UserManager userManager)
        {
            this._accountBusinessRepository = accountBusinessRepository;
            this._userBusinessRepository = userBusinessRepository;
            this._userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {

            var userDetails = _accountBusinessRepository.Login(loginUserViewModel.Email, loginUserViewModel.Password);

            if (userDetails!=null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

                List<Claim> claims= new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Sid, userDetails.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, userDetails.Email));
                claims.Add(new Claim(ClaimTypes.Name, userDetails.FirstName + " " + userDetails.LastName));

                identity.AddClaims(claims);

                var principal = new ClaimsPrincipal(identity);               
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Home_Controller);
            }

            TempData["Error"] = ResponseMessageConstant.InvalidEmailOrPassword;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction(RouteConstant.Login_Action, RouteConstant.Account_Controller);
        }

        public IActionResult MyProfile()
        {
           var userProfile= _userBusinessRepository.EditUserViewModelData(_userManager.CurrentUserId);

            return View(userProfile);
        }

        [HttpPost]
        public IActionResult UpdateProfile(CreateUserViewModel createUserViewModel)
        {
            ModelState.Remove("RoleIds");

            if (ModelState.IsValid)
            {
                var response = _accountBusinessRepository.UpdateProfile(createUserViewModel.User, _userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;

                    return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Home_Controller);
                }                       
            }
            return RedirectToAction(RouteConstant.MyProfile_Action, RouteConstant.Account_Controller);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordUserViewModel changePasswordUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var response= _accountBusinessRepository.ChangePassword(changePasswordUserViewModel,_userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;

                    return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Home_Controller);
                }

                TempData["Error"] = response.ResponseMessage;
            }
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(LoginUserViewModel loginUserViewModel)
        {
            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                var response = _accountBusinessRepository.ForgotPassword(loginUserViewModel.Email);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;

                    return RedirectToAction(RouteConstant.PasswordResetSuccessfully_Action, RouteConstant.Account_Controller);
                }
                else
                {
                    TempData["Error"] = response.ResponseMessage;
                }               
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult PasswordResetSuccessfully()
        {
            return View();
        }
    }
}