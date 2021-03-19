using LibreriaAdmin.Interfaces;
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
        private readonly IManagerService _manageService;

        public ManagerController(IConfiguration config, ILogger<ManagerController> logger, IManagerService managerService)
        {
            _config = config;
            _logger = logger;
            _manageService = managerService;
        }
        public IActionResult Login([FromForm] ManagerViewModel loginVM)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + " Token POST方法被呼叫");
            IActionResult response = Unauthorized();

            return response;
        }
       
        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManager()
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult>();

            try
            {
                //result.Body = _manageService.GetManager();
                return result;
            }
            catch(Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }
    }
}
    