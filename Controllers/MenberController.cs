using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class MenberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MemberIndex()
        {
            return View();
        }
    }
}
