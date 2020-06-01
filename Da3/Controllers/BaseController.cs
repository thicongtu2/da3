using System.Collections.Generic;
using Da3.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Da3.Controllers
{
    public class BaseController : Controller
    {
        
        protected readonly ApplicationDbContext _dbContext;
        protected JsonResult Json(bool success, string message = "")
        {
            return new JsonResult(new
            {
                success,
                message
            });
        }
        
        protected bool IsMobileDevice()
        {
            return Request.Headers["User-Agent"].ToString().IndexOf("Mobile") > 0;
        }
        
        public BaseController(ApplicationDbContext dbContext) : base()
        {
            _dbContext = dbContext;
        }
        
        protected virtual string GetRemoteIpAddress()
        {
            return ControllerContext.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
        
        protected IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        
        protected void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        protected void AddErrors(IEnumerable<string> errors)
        {
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
        }
    }
}