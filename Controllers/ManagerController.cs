using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public ManagerController(IConfiguration config, ILogger<ManagerController> logger)
        {
            _config = config;
            _logger = logger;
        }
        public IActionResult Login([FromForm] LoginViewModel loginVM)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + " Token POST方法被呼叫");
            IActionResult response = Unauthorized();

            return null;
        }
    }
}
    