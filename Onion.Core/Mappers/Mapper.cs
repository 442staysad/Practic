using System.Threading.Tasks;
using Onion.Core.DTO.Department;
using Onion.Core.DTO.Employee;
using Onion.Core.DTO.Role;
using Onion.Core.DTO.Project;
using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Onion.Core.Mappers
{
    public class Mapper : IMapper
    {
        public DepartmentDTO ToDepartmentDTO(Department department)
        {
            DepartmentDTO departmentDTO = new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
            };
            if (department.Employee != null)
                departmentDTO.Manager = ToEmployeeShortDTO(department.Employee);
            else
                departmentDTO.Manager =new EmployeeShortDTO {FirstName="Неназначено"};
            return departmentDTO;
        }

        public Department ToDepartment(DepartmentDTO departmentDTO, Department department = null)
        {
            if (department == null)
                department = new Department();

            department.Name = departmentDTO.Name;
            department.Description = departmentDTO.Description;
            department.HeadOfDepartmentId = departmentDTO.Manager.Id;
            
            return department;
        }

        public EmployeeDTO ToEmployeeDTO(Employee employee)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO
            {
                Id = employee.Id,
                PrivateEmailAddress = employee.PrivateEmailAddress,
                PrivatePhoneNumber = employee.PrivatePhoneNumber,
                WorkEmailAddress = employee.WorkEmailAddress,
                WorkPhoneNumber = employee.WorkPhoneNumber,
                PersonalData = ToEmployeeShortDTO(employee),
                DepartmentDTO = employee.Department == null ? new DepartmentDTO { Name = "Без отдела" } : ToDepartmentDTO(employee.Department)
            };
            if (employee.Projects!= null)
                employeeDTO.EmployeeProjects = employee.Projects.Where(e=>e.EmployeeId==employee.Id).Select(e => ToProjectDTO(e.Project));
            return employeeDTO;
        }

        public ProjectDTO ToProjectDTO(Project project)
        {
            ProjectDTO projectDTO = new ProjectDTO {
            Id=project.Id, 
            Name=project.Name,
            Description=project.Description,
            Cost=project.Cost,
            StartDate=project.StartDate,
            EndDate=project.EndDate,
            Status=project.Status,
            };
            if (project.Employees.Count != 0)
                projectDTO.ProjectEmployees = project.Employees.Where(p=>p.ProjectId==project.Id).Select(e => ToEmployeeDTO(e.Employee));
            if (project.LineManagerId != null)
                projectDTO.LineManager = ToEmployeeShortDTO(project.LineManager);
            else
                projectDTO.LineManager = new EmployeeShortDTO { FirstName = "Неначначено" };
            return projectDTO;
        }

        public Project ToProject(ProjectDTO projectDTO, Project project = null)
        {
            if (project == null)
                project = new Project();
            else
                project.Id = (int)projectDTO.Id;
            
            project.Name = projectDTO.Name;
            project.Description = projectDTO.Description;
            project.Cost = projectDTO.Cost;
            project.StartDate = projectDTO.StartDate;
            project.EndDate = projectDTO.EndDate;
            project.Status = projectDTO.Status;
            project.LineManagerId = projectDTO.LineManager.Id;
            if (projectDTO.ProjectEmployees.Count() > 0)
            {
                    project.Employees.Clear();
                foreach (var item in projectDTO.ProjectEmployees)
                    project.Employees.Add(new ProjectEmployees { EmployeeId= (int)item.Id});
            }
            return project;
        }

        public EmployeeShortDTO ToEmployeeShortDTO(Employee employee)
        {
            EmployeeShortDTO dto = new EmployeeShortDTO
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
            };
            if (employee.Role != null)
                dto.Role = ToRoleDTO(employee.Role);
            return dto;
        }

        public RoleDTO ToRoleDTO(SystemRole role) => new RoleDTO
        {
            Id=role.Id,
            RoleName=role.RoleName
        };

        public Employee ToEmployee(EmployeeDTO employeeDTO,string password=null, Employee employee = null)
        {
            if (employee == null)
                employee = new Employee();

            employee.FirstName = employeeDTO.PersonalData.FirstName;
            employee.LastName = employeeDTO.PersonalData.LastName;
            employee.Patronymic = employeeDTO.PersonalData.Patronymic;
            employee.DepartmentId = employeeDTO.DepartmentDTO.Id;
            employee.PrivateEmailAddress = employeeDTO.PrivateEmailAddress;
            employee.PrivatePhoneNumber = employeeDTO.PrivatePhoneNumber;
            employee.WorkPhoneNumber = employeeDTO.WorkPhoneNumber;
            employee.WorkEmailAddress = employeeDTO.WorkEmailAddress;
            employee.RoleId = (int)employeeDTO.PersonalData.Role.Id;
            if(!string.IsNullOrEmpty(password))
                employee.Password = password;

            return employee;
        }
    }
}