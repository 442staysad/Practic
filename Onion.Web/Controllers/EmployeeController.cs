using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Onion.Core.Interfaces;
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

namespace Onion.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IDepartment departmentService;
        private readonly IEmployee employeeService;
        private readonly IRoleManager roleService;
        private readonly WebMapper mapper;

        public EmployeeController(IDepartment departmentService, IEmployee employeeService, IRoleManager roleService, WebMapper mapper)
        {
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

            var model = new PagedListViewModel(employeesDtos.Count(), pageIndex, employeesDtos ,pageSize)
            {
                SortField = sortField,
                SortDirection = sortDirection == "Ascending" ? "Descending" : "Ascending",
                FilterString = filterString,
                ListItems = employeesDtos
            };

            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {

            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var employee =await employeeService.GetEmployeeById(id);
            var departments =await departmentService.GetDepartmentsList();
            var roles = await roleService.GetAllRoles();

            return await Task.Run(() => View(mapper.ToEditEmployeeModel(employee,roles,departments)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEmployeeModel model)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            await employeeService.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
