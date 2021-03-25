using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaAdmin.filter
{
    public class CustomActionFilter
    {
        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                //if (!context.ModelState.IsValid)
                //{
                //    context.Result = new BadRequestObjectResult(context.ModelState);
                //}
                var ManagerRole = context.HttpContext.Request.Cookies.TryGetValue("R", out string Role);
                if (Role != "0")
                {
                    //無最高許可權
                    context.HttpContext.Response.Redirect("/Home/Index");
                }
            }
        }
        public void OnActionExecuting(ActionExecutingContext Context)
        {
            var ManagerRole = Context.HttpContext.Request.Cookies.TryGetValue("R", out string Role);
            if (Role != "0")
            {
                //無最高許可權
                Context.HttpContext.Response.Redirect("/Home/Index");
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // our code after action executes
        }

       
    }
}
