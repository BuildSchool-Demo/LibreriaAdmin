using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
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

        //Login
        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + " Token控制器POST方法被呼叫");

            IActionResult response = Unauthorized();

            var user = GetManagerAuthentication(loginVM);

            if (user.IsSuccess == true)
            {
                var tokenString = GenerateJsonWebToken(loginVM);
                response = Ok(new { token = tokenString });
                Response.Cookies.Append("R", user.Msg);
            }
            return response;

        }

        private BaseModel.BaseResult<ManagerViewModel.ManagerSingleResult> GetManagerAuthentication(LoginViewModel loginVM)
        {
            var Manager = _manageService.GetManagerAuthentication(loginVM);
            return Manager;
        }

        private string GenerateJsonWebToken([FromBody] LoginViewModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
