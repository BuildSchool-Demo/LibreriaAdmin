using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult LoginIndex()
        {
            return View();
        }
    }
}
