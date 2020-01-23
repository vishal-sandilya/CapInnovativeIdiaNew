using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Client.ApplicationManager;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.Domains;
using CapInnovativeIdia.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.Client.Controllers
{
    [Authorize(Policy = "Authorized")]
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IUserBusinessRepository _userBusinessRepository;

        public readonly UserManager _userManager;

        public UserController(IUserBusinessRepository userBusinessRepository, UserManager userManager)
        {
            this._userBusinessRepository = userBusinessRepository;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var users = _userBusinessRepository.GetAllUser();

            return View(users);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            var userModelData = _userBusinessRepository.EditUserViewModelData(id);

            if (userModelData.User != null)
            {
                return View(userModelData);
            }
            else
            {
                TempData["info"] = ResponseMessageConstant.RecordNotFound;
            }

            return RedirectToAction(RouteConstant.Index_Action, RouteConstant.User_Controller);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var modelData = _userBusinessRepository.CreateUserViewModelData();

            return View(modelData);
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
              var response= _userBusinessRepository.CreateUser(createUserViewModel, _userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;
                }

                return RedirectToAction(RouteConstant.Index_Action, RouteConstant.User_Controller);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           var userModelData= _userBusinessRepository.EditUserViewModelData(id);

            if (userModelData.User != null)
            {
                return View(userModelData);
            }
            else
            {
                TempData["info"] = ResponseMessageConstant.RecordNotFound;
            }

            return RedirectToAction(RouteConstant.Index_Action, RouteConstant.User_Controller);
        }

        [HttpPost]
        public IActionResult Edit(CreateUserViewModel createUserViewModel)
        {
            if(ModelState.IsValid)
            {
                var response = _userBusinessRepository.UpdateUser(createUserViewModel, _userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;
                }

                return RedirectToAction(RouteConstant.Index_Action, RouteConstant.User_Controller);
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            var response=_userBusinessRepository.DeleteUser(id);

            if (response.ResponseType == ResponseType.Success)
            {
                TempData["Success"] = response.ResponseMessage;
            }
            else
            {
                TempData["Info"] = response.ResponseMessage;
            }

            return RedirectToAction(RouteConstant.Index_Action,RouteConstant.User_Controller);
        }
    }
}