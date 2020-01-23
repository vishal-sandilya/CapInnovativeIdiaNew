using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapInnovativeIdia.BusinessRepository.BusinessRepositoriesInterface;
using CapInnovativeIdia.Client.ApplicationManager;
using CapInnovativeIdia.Domain.Constants;
using CapInnovativeIdia.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static CapInnovativeIdia.Domain.Enums.ResponseTypeEnum;

namespace CapInnovativeIdia.Client.Controllers
{
    [Authorize(Policy = "Authorized")]
    public class IdiaController : Controller
    {
        private readonly IIdiaBusinessRepository _idiaBusinessRepository;
        private readonly UserManager _userManager;

        public IdiaController(IIdiaBusinessRepository idiaBusinessRepository,UserManager userManager)
        {
            this._idiaBusinessRepository = idiaBusinessRepository;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var idias=_idiaBusinessRepository.GetAllIdia();

            return View(idias);
        }

        public IActionResult View(int id)
        {
            var idia = _idiaBusinessRepository.GetAllIdia().Where(x=>x.Id==id).SingleOrDefault();

            if (idia != null)
            {
                return View(idia);
            }
            else
            {
                TempData["info"] = ResponseMessageConstant.RecordNotFound; 
            }

            return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Idia_Controller);
        }

        public IActionResult MyIdia()
        {
            var myIdia = _idiaBusinessRepository.GetIdiaByUserId(_userManager.CurrentUserId);
            
            return View(myIdia);  
        }

        [HttpGet]
        public IActionResult Create()
        {
            var idiaDataModel=_idiaBusinessRepository.CreateIdiaViewModelData();

            return View(idiaDataModel);
        }

        [HttpPost]
        public IActionResult Create(CreateIdiaViewModel createIdiaViewModel)
        {
            if (ModelState.IsValid)
            {
                createIdiaViewModel.Idia.CreatedBy = _userManager.CurrentUserId;

                var response=_idiaBusinessRepository.CreateIdia(createIdiaViewModel.Idia);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;
                }
     
                return RedirectToAction(RouteConstant.MyIdia_Action, RouteConstant.Idia_Controller);
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           var editIdiaViewModelData= _idiaBusinessRepository.EditIdiaViewModelData(id,_userManager.CurrentUserId);
            
            if (editIdiaViewModelData.Idia != null)
            {
                return View(editIdiaViewModelData);
            }
            TempData["Warning"] = ResponseMessageConstant.UnAuthorizedAction;

            return RedirectToAction(RouteConstant.MyIdia_Action, RouteConstant.Idia_Controller);
        }

        [HttpPost]
        public IActionResult Edit(CreateIdiaViewModel createIdiaViewModel)
        {
            if (ModelState.IsValid)
            {
               var response= _idiaBusinessRepository.UpdateIdia(createIdiaViewModel.Idia);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;

                    return RedirectToAction(RouteConstant.MyIdia_Action, RouteConstant.Idia_Controller);
                }    
            }
            return View();
        }


        public IActionResult Delete(int id)
        {
           var response= _idiaBusinessRepository.DeleteIdia(id,_userManager.CurrentUserId);

            if (response.ResponseType == ResponseType.Success)
            {
                TempData["Success"] = response.ResponseMessage;
            }

            if (response.ResponseType == ResponseType.Warning)
            {
                TempData["Warning"] = response.ResponseMessage;
            }

            return RedirectToAction(RouteConstant.MyIdia_Action, RouteConstant.Idia_Controller);
        }
    }
}