using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Onion.Core.Interfaces;
using Onion.Web.ViewModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Onion.Web.Mappers;
using Onion.Web.Models.Department;

namespace Onion.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartment departmentService;
        private readonly IEmployee employeeService;
        private readonly WebMapper mapper;

        public DepartmentController(IDepartment departmentService, IEmployee employeeService, WebMapper mapper)
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

            var model = new PagedListViewModel(departmentsDtos.Count(),(int)pageIndex,departmentsDtos,pageSize)
            {
                SortField = sortField,
                SortDirection=sortDirection=="Ascending"? "Descending" : "Ascending",
                FilterString= filterString,
                ListItems=departmentsDtos
            };

            return await Task.Run(() => View(model));
        }

        
        public async Task<IActionResult> Details(int id)
        {
            
            return await Task.Run(() => View());
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var employees = await employeeService.GetEmployeeShortData();
            CreateDepartmentModel model = new CreateDepartmentModel
            {
                HeadOfDepartment = employees.Where(e => e.Role.Id == 2).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.LastName} {e.FirstName} {e.Patronymic}",
                })
            };

            return await Task.Run(() => View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDepartmentModel model)
        {  
            await departmentService.CreateOrUpdateDepartment(mapper.ToDepartmentDTO(model));
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var employees = await employeeService.GetEmployeeShortData();
            var department = await departmentService.GetDepartmentById((int)id);
            EditDepartmentModel model = new EditDepartmentModel
            {
                Id = (int)department.Id,
                Name = department.Name,
                Description = department.Description,
                HeadOfDepartment = employees.Where(e => e.Role.Id == 2).Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = $"{e.LastName} {e.FirstName} {e.Patronymic}",
                })
            };
             return await Task.Run(() => View(model));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditDepartmentModel model)
        {
            await departmentService.CreateOrUpdateDepartment(mapper.ToDepartmentDTO(model));
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await departmentService.DeleteDepartment(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
