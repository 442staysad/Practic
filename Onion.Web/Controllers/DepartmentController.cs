using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Onion.Web.ViewModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System;
using Onion.Web.Mappers;
using Onion.Web.Models.Department;
using Onion.Core.Interfaces.Services;

namespace Onion.Web.Controllers
{
	[Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;
        private readonly WebMapper mapper;

        public DepartmentController(IDepartmentService departmentService, IEmployeeService employeeService, WebMapper mapper)
        {
            this.departmentService = departmentService;
            this.employeeService = employeeService;
            this.mapper = mapper;
        }

        // GET: Department
        public async Task<IActionResult> Index(
               string sortField,
               string sortDirection,
               string filterString,
               int? pageIndex=1,
               int pageSize = 10)
        {
            var departmentsDtos = await departmentService.GetDepartmentsList(sortField,sortDirection, filterString);

            var model = new PaginatedListViewModel(departmentsDtos.Count(),(int)pageIndex,departmentsDtos,pageSize)
            {
                SortField = sortField,
                SortDirection=sortDirection=="Ascending"? "Descending" : "Ascending",
                FilterString= filterString
            };

            return await Task.Run(() => View(model));
        }

        [HttpGet]
        [Authorize(Roles = "RootUser")]
        public async Task<ActionResult> Create()
        {
            var employees = await employeeService.GetEmployeeShortData();
            DepartmentModel model = mapper.ToDepartmentModel(null, employees);

            return await Task.Run(() => View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentModel model)
        {  
            await departmentService.Create(mapper.ToDepartmentDTO(model));
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        [Authorize(Roles = "RootUser")]
        public async Task<ActionResult> Edit(int id)
        {
            var employees = await employeeService.GetEmployeeShortData();
            var department = await departmentService.GetDepartmentById(id);
            var departmentEmployees = await employeeService.GetEmployeesByDepartmentId(id);
            return await Task.Run(() => View(mapper.ToDepartmentModel(department,employees,departmentEmployees)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentModel model)
        {
            await departmentService.Update(mapper.ToDepartmentDTO(model));
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "RootUser")]
        public async Task<IActionResult> Delete(int id)
        {
            await departmentService.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "RootUser")]
        public async Task<IActionResult> RemoveEmployee(int empId, int depId)
        {
            await departmentService.RemoveFromDepartment(empId);
            return RedirectToAction(nameof(Edit),new { id = depId });
        }
    } 
}
