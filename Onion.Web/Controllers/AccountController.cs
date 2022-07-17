using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Onion.Core.Interfaces;
using Onion.Web.Mappers;
using Onion.Web.Models.Employee;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Onion.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDepartment departmentService;
        private readonly IRoleManager roleService;
        private readonly IAccount accountService;
        private readonly WebMapper mapper;

        public AccountController(IAccount accountService, WebMapper mapper, IRoleManager roleService, IDepartment departmentService)
        {
            this.mapper = mapper;
            this.accountService = accountService;
            this.roleService = roleService;
            this.departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult> Register()
        {
            var departments =await departmentService.GetDepartmentsList();
            var roles =await roleService.GetAllRoles();
            return await Task.Run(() => View(mapper.ToRegisterModel(roles,departments)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                await accountService.Register(mapper.ToEmployeeUpdateDTO(model));
                return await Task.Run(() => RedirectToAction(nameof(Register)));
            }
            else
                ModelState.AddModelError("", "Проверьте введенные данные");

            return await Task.Run(() => View(model));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel userModel)
        {
            if (!ModelState.IsValid)
            {
                if (accountService.IsAuthenticatedLogin(userModel.Email, userModel.Password).Result)
                {
                    await Authenticate(userModel.Email);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Нет такого пользователя");
            }
            else
            {
                ModelState.AddModelError("", "Что-то не так с введенными данными");
            }
            return View();
        }

        private async Task Authenticate(string userLogin)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userLogin)
            };
            ClaimsIdentity identity = new ClaimsIdentity(claims, "AppCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }

        public async Task<ActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
