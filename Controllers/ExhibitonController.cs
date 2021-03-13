using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class ExhibitonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RentalIndex()
        {
            return View();
        }

        public IActionResult ExhibitonIndex()
        {
            return View();
        }
    }
}
