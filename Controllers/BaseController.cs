using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 執行Action之前運作
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var ManagerRole = filterContext.HttpContext.Request.Cookies.TryGetValue("R", out string Role);
            if (Role == "0")
            {
                //無最高許可權
                filterContext.HttpContext.Response.Redirect("./Manager/ManagerIndex");
            }
        }
    }
}
