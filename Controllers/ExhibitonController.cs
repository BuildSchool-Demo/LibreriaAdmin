using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]

    public class ExhibitonController : Controller
    {
        public IExhibitonService _service;

        public ExhibitonController(IExhibitonService service)
        {
            _service = service;
        }



        public IActionResult RentalIndex()
        {

            return View();
        }

        public IActionResult ExhibitonIndex()
        {

            return View();
        }

        [AllowAnonymous]
        public IActionResult Email(int exhibitionId)
        {
            ViewData["exhibitionId"] = exhibitionId;
            //ViewData["GetRentalDate"] = _service.GetRentalDate(exhibitionId);
            return View();
        }

        public IActionResult SendMail(int exhibitionId)
        {
            ViewBag.exhibitionId = exhibitionId;
            ViewData["customerData"] = _service.GetCustomerData(exhibitionId);

            return View();
        }

    }
}
