using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibreriaAdmin.WebApi
{
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public BaseModel.BaseResult<ManagerViewModel.ManagerListResult> GetAllManagers()
        {
            var result = new BaseModel.BaseResult<ManagerViewModel.ManagerListResult>();
            try
            {
                result.Body = _manageService.GetAllManagers();

                return result;
            }
            catch (Exception ex)
            {
                result.Msg = ex.Message;
                result.IsSuccess = false;

                return result;
            }
        }

        [HttpGet]
        public BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManager(int managerID)
        {
            var Manager = _manageService.GetManager(managerID);
            return Manager;
        }
        [HttpPost]
        //public async Task<ActionResult<ManagerViewModel.ManagerSingleResult>> CreateManager([FromBody] ManagerViewModel.ManagerSingleResult manager)
        public ActionResult<ManagerViewModel.ManagerSingleResult> CreateManager([FromBody] ManagerViewModel.ManagerSingleResult manager)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + $" Manager控制器Post方法被呼叫 - 傳入的資料為:" + JsonSerializer.Serialize(manager));
            var result = _manageService.CreateManager(manager);

            if (result.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
