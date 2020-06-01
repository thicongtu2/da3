using System;
using System.Linq;
using System.Threading.Tasks;
using Da3.Core.Entities;
using Da3.Core.Role;
using Da3.Extentions;
using Da3.Infrastructure.Authentication;
using Da3.Infrastructure.Database;
using Da3.Models;
using Da3.Share.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Da3.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly ILogger _logger;

        public AccountController(ApplicationDbContext dbContext, ApplicationUserManager userManager,
            ApplicationSignInManager signInManager,
            ILogger<AccountController> logger) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe,
                    lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {model.Email} logged in.");
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var info = _dbContext.Employers.FirstOrDefault(i => i.UserId == user.Id);
                    if (info == null)
                    {
                        return RedirectToAction("Info", "Employer");
                    }
                    else
                    {
                        HttpContext.Session.Set("Employer", info);
                    }

                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning($"User {model.Email} account locked out.");
                    ModelState.AddModelError(string.Empty, "Account locked out.");
                    return View(model);
                }
                else
                {
                    _logger.LogWarning($"User {model.Email} fail to log in");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            _logger.LogWarning($"User {model.Email} tries to login with Invalid ModelState.");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation($"User {User.Identity.Name} logged out.");
            await _signInManager.SignOutAsync();
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Da3User()
                    {Da3UserId = "Guest", UserName = model.Email, Email = model.Email, EmailConfirmed = true};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RoleConstants.Employer);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User created a new account with password.");

                    return RedirectToAction("Login", "Account");
                }

                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}