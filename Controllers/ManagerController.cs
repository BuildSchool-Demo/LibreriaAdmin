using LibreriaAdmin.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    [Authorize]

    public class ManagerController : Controller
    {
        private readonly ILogger _logger;

        public ManagerController( ILogger<ManagerController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult ManagerIndex()
        {
            return View();
        }
    }
}
