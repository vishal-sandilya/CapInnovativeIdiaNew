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
    public class ReviewerController : Controller
    {
        private readonly IIdiaBusinessRepository _idiaBusinessRepository;
        private readonly UserManager _userManager;

        public ReviewerController(IIdiaBusinessRepository idiaBusinessRepository, UserManager userManager)
        {
            this._idiaBusinessRepository = idiaBusinessRepository;
            this._userManager = userManager;
        }
        public IActionResult Index()
        {
            var idias = _idiaBusinessRepository.GetAllIdia();

            return View(idias);
        }

        public IActionResult View(int id)
        {
            var idia = _idiaBusinessRepository.GetAllIdia().Where(x => x.Id == id).SingleOrDefault();

            if (idia != null)
            {
                return View(idia);
            }
            else
            {
                TempData["info"] = ResponseMessageConstant.RecordNotFound;
            }

            return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Reviewer_Controller);
        }

        public IActionResult Action(ViewIdiaViewModel viewIdiaViewModel, string actionType)
        {
            if (actionType == ActionTypeConstant.Approve)
            {
               var response= _idiaBusinessRepository.ApproveIdia(viewIdiaViewModel.Id, _userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;
                 
                }
                else if(response.ResponseType == ResponseType.Info) 
                {
                    TempData["Info"] = response.ResponseMessage;
                }

                return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Reviewer_Controller);
            }

            if(actionType==ActionTypeConstant.Reject)
            {
                var response=_idiaBusinessRepository.RejectIdia(viewIdiaViewModel.Id, _userManager.CurrentUserId);

                if (response.ResponseType == ResponseType.Success)
                {
                    TempData["Success"] = response.ResponseMessage;

                }
                else if (response.ResponseType == ResponseType.Info)
                {
                    TempData["Info"] = response.ResponseMessage;
                }

                return RedirectToAction(RouteConstant.Index_Action, RouteConstant.Reviewer_Controller);
            }

            return RedirectToAction( RouteConstant.Index_Action, RouteConstant.Reviewer_Controller);
        }
    }
}