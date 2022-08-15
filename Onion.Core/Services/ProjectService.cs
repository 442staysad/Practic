using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Onion.Core.Interfaces;
using Onion.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Onion.Core.DTO.Project;
using Onion.Core.DTO.Employee;
using Onion.Core.Interfaces.Services;

namespace Onion.Core.Services
{
    public class ProjectService:IProjectService
    {
        private readonly IRepository<Project> projectRepository;
        private readonly IRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public ProjectService(IRepository<Project> projectRepository, IRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.projectRepository = projectRepository;
            this. mapper = mapper;
        }

        public async Task Create(ProjectDTO projectDTO)
        {
            try 
            {
                await projectRepository.AddAsync(mapper.ToProject(projectDTO));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task Update(ProjectDTO projectDTO)
        {
            Project project = await projectRepository.FindAsync(d => d.Id.Equals(projectDTO.Id), d => d.Include(e => e.Employees)
                                                                                                       .ThenInclude(r => r.Employee));

            try
            {
                await projectRepository.UpdateAsync(mapper.ToProject(projectDTO, project));
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task Delete(int id)
        {
            try 
            {
                await projectRepository.DeleteAsync(await projectRepository.FindAsync(p => p.Id == id));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task<ProjectDTO> GetProjectById(int id) 
        {
            var project = await projectRepository.FindAsync(p => p.Id.Equals(id), p => p.Include(l=>l.LineManager)
                                                                                        .ThenInclude(r=>r.Role)
                                                                                        .Include(e=>e.Employees)
                                                                                        .ThenInclude(e=>e.Employee)
                                                                                        .ThenInclude(r => r.Role));
            return mapper.ToProjectDTO(project);
        }
        public async Task<IEnumerable<ProjectDTO>> GetProjectsList(string sortField=null, 
                                                                    string sortDirection=null, 
                                                                    string filterString = null, 
                                                                    DateTime? startDate=null, 
                                                                    DateTime? endDate=null)
        {
            var projects = projectRepository.GetAll(p=>p.Include(e=>e.LineManager).ThenInclude(r=>r.Role)
                                                        .Include(e=>e.Employees).ThenInclude(r=>r.Employee)
                                                        .ThenInclude(r=>r.Role)).AsQueryable().ToList()
                                                        .Select(p=>mapper.ToProjectDTO(p));

            if (!String.IsNullOrEmpty(filterString))
                projects = projects.Where(p => p.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase)
                                                || p.LineManager.FullName.Contains(filterString, StringComparison.OrdinalIgnoreCase));
            if (startDate.HasValue)
                projects = projects.Where(p => p.StartDate.Value >= startDate);
            if (endDate.HasValue)
                projects = projects.Where(p => p.StartDate.Value <= endDate);


            if (!String.IsNullOrEmpty(sortField))
            { 
                if (sortField == "Name")
                    projects = sortDirection == "Ascending" ? projects.OrderBy(p => p.Name)
                                                              : projects.OrderByDescending(p => p.Name);
                if (sortField == "Manager")
                    projects = sortDirection == "Ascending" ? projects.OrderBy(p => p.LineManager.FullName)
                                                              : projects.OrderByDescending(p => p.LineManager.FullName);
                if (sortField == "Period")
                    projects = sortDirection == "Ascending" ? projects.OrderBy(p => p.StartDate)
                                                              : projects.OrderByDescending(p => p.StartDate);
                if (sortField == "Status")
                    projects = sortDirection == "Ascending" ? projects.OrderBy(p => p.Status)
                                                              : projects.OrderByDescending(p => p.Status);
                if (sortField == "Cost")
                    projects = sortDirection == "Ascending" ? projects.OrderBy(p => p.Cost)
                                                              : projects.OrderByDescending(p => p.Cost);
            }
            return await Task.Run(() => projects);
        }
    }
}
