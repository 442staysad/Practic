using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onion.Core.Interfaces.Services;
using Onion.Web.Mappers;
using Onion.Web.Models.Employee;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onion.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly WebMapper mapper;

        public AccountController(IAccountService accountService, WebMapper mapper)
        {
            this.mapper = mapper;
            this.accountService = accountService;
        }

		
        [AllowAnonymous]
        public async Task<IActionResult> Login() => await Task.Run(() => View());

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel userModel)
        {
            if (ModelState.IsValid)
            {
                if (await accountService.IsAuthenticatedLogin(userModel.Email, userModel.Password))
                {
                    await Authenticate(userModel.Email);
                    if(User.IsInRole("User"))
                        return RedirectToAction("Index", "Dashboard");
                    if (User.IsInRole("LineManager"))
                        return RedirectToAction("Index", "Department");
                    if (User.IsInRole("RootUser"))
                        return RedirectToAction("Index", "Employee");
                }
                ModelState.AddModelError("", "Пользователя не существует");
            }
            else
            {
                ModelState.AddModelError("", "Проверьте введенные данные");
            }
            return View();
        }

        private async Task Authenticate(string userLogin)
        {
            var user= await accountService.GetUserByUsername(userLogin);
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin),
                new Claim(ClaimTypes.Role, user.PersonalData.Role.RoleName)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
