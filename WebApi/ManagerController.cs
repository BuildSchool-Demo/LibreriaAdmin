using LibreriaAdmin.Interfaces;
using LibreriaAdmin.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public async Task<BaseModel.BaseResult<ManagerViewModel.ManagerListResult>> GetAllManagers()
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpPost("{id}")]
        //public async Task<ActionResult<ManagerViewModel.ManagerSingleResult>> CreateManager([FromBody] ManagerViewModel.ManagerSingleResult manager)
        public ActionResult<ManagerViewModel.ManagerSingleResult> DeleteManager(int id)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + $" Manager控制器Post方法被呼叫 - 傳入的資料為{nameof(id)}資料為:" + id);
            var result = _manageService.DeleteManager(id);

            if (result.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpPost("{id}")]
        //public async Task<ActionResult<ManagerViewModel.ManagerSingleResult>> CreateManager([FromBody] ManagerViewModel.ManagerSingleResult manager)
        public ActionResult<ManagerViewModel.ManagerSingleResult> EditManager(int id, [FromBody] ManagerViewModel.ManagerSingleResult manager)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + $" Manager控制器Post方法被呼叫 - 傳入的資料為{nameof(id)}資料為:" + id);
            var result = _manageService.EditManager(id,manager);

            if (result.IsSuccess == false)
            {
                return NotFound();
            }

            return Ok();
        }

        //Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            _logger.LogWarning(2001, DateTime.Now.ToLongTimeString() + " Token控制器POST方法被呼叫");

            ActionResult response = Unauthorized();

            var user = GetManagerAuthentication(loginVM);

            if (user.IsSuccess == true && user.Msg == "0")
            //if (user.IsSuccess == true)
            {
                var tokenString = GenerateJsonWebToken(loginVM);
                response = Ok(new { token = tokenString });
                //Response.Cookies.Append("R", user.Msg);
            }

            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginVM.Username),
                    new Claim("FullName","test"),
                    new Claim(ClaimTypes.Role, "Administrator"),

                };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = false, //瀏覽器關閉即刻登出

            };
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return response;
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
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
