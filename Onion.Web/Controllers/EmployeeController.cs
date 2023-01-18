using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Onion.Web.ViewModels;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Onion.Core.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Onion.Core.DTO.Employee;
using Onion.Web.Mappers;
using Onion.Web.Models.Employee;
using Onion.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace Onion.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly IRoleManager roleService;
        private readonly WebMapper mapper;

        public EmployeeController(IDepartmentService departmentService, IEmployeeService employeeService, IRoleManager roleService, IAccountService accountService, WebMapper mapper)
        {
            this.accountService = accountService;
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.roleService = roleService;
            this.mapper = mapper;
        }

        // GET: Department
        public async Task<IActionResult> Index(
            string sortField = "",
            string sortDirection = "",
            string filterString = "",
            int pageIndex = 1)
        {
            const int pageSize = 10;

            var employeesDtos = await employeeService.GetEmployeesList(sortField, sortDirection, filterString);

            var model = new PaginatedListViewModel(employeesDtos.Count(), pageIndex, employeesDtos ,pageSize)
            {
                SortField = sortField,
                SortDirection = sortDirection == "Ascending" ? "Descending" : "Ascending",
                FilterString = filterString
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {

            return await Task.Run(() => View());
        }



        [HttpGet]

        [Authorize(Roles = "RootUser,LineManager")]
        public async Task<ActionResult> Create()
        {
            var departments = await departmentService.GetDepartmentsList();
            var roles = await roleService.GetAllRoles();
            return await Task.Run(() => View(mapper.ToRegisterModel(roles, departments)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                await accountService.Register(mapper.ToEmployeeUpdateDTO(model));
                return await Task.Run(() => RedirectToAction(nameof(Create)));
            }
            else
                ModelState.AddModelError("", "Проверьте введенные данные");

            return await Task.Run(() => View(model));
        }

        [HttpGet]

        [Authorize(Roles = "RootUser,LineManager")]
        public async Task<ActionResult> Edit(int id)
        {
            var employee =await employeeService.GetEmployeeById(id);
            var departments =await departmentService.GetDepartmentsList();
            var roles = await roleService.GetAllRoles();

            return await Task.Run(() => View(mapper.ToEmployeeModel(employee,roles,departments)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeModel model)
        {
            if (ModelState.IsValid)
            {
                var employeeDTO = mapper.ToEmployeeUpdateDTO(model);
                employeeDTO.EmployeeDTO.Id = model.Id;
                await employeeService.EditEmployee(employeeDTO);

                return await Task.Run(() => RedirectToAction(nameof(Index)));
            }
            return await Task.Run(() => RedirectToAction(nameof(Edit)));
        }


        [Authorize(Roles = "RootUser")]
        public async Task<IActionResult> Delete(int id)
        {
            await employeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
