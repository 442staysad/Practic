using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Onion.Core.Interfaces.Services;
using Onion.Web.Mappers;
using Onion.Web.Models.Dashboard;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace Onion.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
        private readonly IProjectService projectService;
        private readonly WebMapper mapper;

        public DashboardController(IProjectService projectService, IEmployeeService employeeService, 
                                   IDepartmentService departmentService, WebMapper mapper)
        {
            this.employeeService = employeeService;
            this.projectService = projectService;
            this.departmentService = departmentService;
            this.mapper = mapper;
        }

        // GET: DashboardController
        public async Task<ActionResult> Index(GanttModel model)
        {

            model = new GanttModel()
            {
                IsDaysShow = model.IsDaysShow??true,
                ProjectId = model.ProjectId,
                Departmentid=model.Departmentid,
                DateStart = model?.DateStart ?? DateTime.Now.AddDays(-31),
                DateEnd = model?.DateEnd ?? DateTime.Now,
                Departments = model?.Departments?? await departmentService.GetDepartmentsList(),
                Employees = await employeeService.PopolateChart(model.Departmentid,model.ProjectId),
                Projects = model?.Projects?? await projectService.GetProjectsList(),
            }; 

            return View(model);
        }
    }
}
