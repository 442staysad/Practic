using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Onion.Web.ViewModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using Onion.Core.DTO;
using Onion.Core.DTO.Project;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System;
using Onion.Web.Mappers;
using Onion.Web.Models;
using Onion.Core.Interfaces.Services;
using Onion.Web.Models.Project;
using Microsoft.AspNetCore.Authorization;

namespace Onion.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IProjectService projectService;
        private readonly WebMapper mapper;

        public ProjectController(IProjectService projectService, IEmployeeService employeeService, WebMapper mapper)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.mapper = mapper;
        }

        // GET: ProjectController
        public async Task<IActionResult> Index(
               string sortField,
               string sortDirection,
               string filterString,
               DateTime? startDate,
               DateTime? endDate,
               int? pageIndex = 1,
               int pageSize = 8)
        {
            var projectsDtos = await projectService.GetProjectsList(sortField,sortDirection,filterString, startDate, endDate);
            var model = new PaginatedListViewModel(projectsDtos.Count(), (int)pageIndex, projectsDtos, pageSize)
            {
                StartDate = startDate,
                EndDate = endDate,
                SortField = sortField,
                SortDirection = sortDirection =="Ascending"?"Descending" : "Ascending",
                FilterString = filterString,
            };

            return await Task.Run(() => View(model));
        }

        // GET: ProjectController/Create
        [Authorize(Roles = "RootUser,LineManager")]
        public async Task<IActionResult> Create()
        {
            var employees =await employeeService.GetEmployeeShortData();
            return await Task.Run(() => View(mapper.ToProjectModel(employees)));
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectModel model)
        {
           if(ModelState.IsValid)
            await projectService.Create(mapper.ToProjectDTO(model));
           return RedirectToAction(nameof(Index));
        }

        // GET: ProjectController/Edit/5
        [Authorize(Roles = "RootUser,LineManager")]
        public async Task<IActionResult> Edit(int id)
        {
            var employees =await employeeService.GetEmployeeShortData();
            var projectDTO = await projectService.GetProjectById(id);
            return View(mapper.ToProjectModel(employees,projectDTO));
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectModel model)
        {
            if (ModelState.IsValid)
                await projectService.Update(mapper.ToProjectDTO(model));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveEmployee(int empId, int projId)
        {
            return RedirectToAction(nameof(Edit), new { id = projId });
        }

        // POST: ProjectController/Delete/5
        [Authorize(Roles = "RootUser,LineManager")]
        public async Task<IActionResult> Delete(int id)
        {
            await projectService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
