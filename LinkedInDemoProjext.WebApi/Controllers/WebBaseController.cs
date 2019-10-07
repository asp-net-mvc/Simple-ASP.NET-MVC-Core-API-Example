using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LinkedInDemoProjext.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebBaseController : ControllerBase, IActionFilter
    {
        public string UserIdentityToken { get; set; }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            UserIdentityToken = context.HttpContext.Request?.Headers["Authorization"];

            if (UserIdentityToken != null)
                UserIdentityToken = UserIdentityToken.Replace("Bearer ", "");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
    }
}