using LibreriaAdmin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ILogger _logger;
        //private readonly IManagerService _manageService;

        public ManagerController( ILogger<ManagerController> logger, IManagerService managerService)
        {
            _logger = logger;
            //_manageService = managerService;
        }
        public IActionResult ManagerIndex()
        {
            return View();
        }
    }
}
