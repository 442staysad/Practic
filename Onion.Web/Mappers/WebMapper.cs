using Onion.Core.DTO.Employee;
using Onion.Web.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onion.Web.Models.Employee;
using Onion.Web.Models.Project;
using Onion.Web.Models.Department;
using Onion.Core.DTO.Department;
using Onion.Core.DTO.Role;
using Onion.Core.DTO.Project;

namespace Onion.Web.Mappers
{
    public class WebMapper
    {
        public DepartmentDTO ToDepartmentDTO(DepartmentModel departmentModel) => new DepartmentDTO
        {
            Id=departmentModel.Id,
            Name = departmentModel.Name,
            Description = departmentModel.Description,
            Manager = new EmployeeShortDTO
            {
                Id = departmentModel.HeadOfDepartmentId

            }
        };

        public ProjectDTO CreateToProjectDTO(ProjectModel model) => new ProjectDTO
        {
            Name = model.Name,
            Description = model.Description,
            Status = model.Status,
            Cost = model.Cost,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            ProjectEmployees = model.ProjectEmployeesIds.Select(e=>new EmployeeDTO { Id=e}),
            LineManager = new EmployeeShortDTO {Id=model.LineManagerId }
        };

        public DepartmentModel ToDepartmentModel(DepartmentDTO departmentDTO = null,
                                                IEnumerable<EmployeeShortDTO> employees = null,
                                                IEnumerable<EmployeeDTO> departmentEmployees=null)
        {
            DepartmentModel model = new DepartmentModel
            {
                HeadOfDepartment = employees.Where(e=>e.Role.Id.Equals(2)),
            };
            if (departmentDTO != null)
            {
                model.Id = departmentDTO.Id;
                model.Name = departmentDTO.Name;
                model.Description = departmentDTO.Description;
                model.HeadOfDepartmentId = departmentDTO.Manager.Id;

                if (departmentEmployees.Count() > 0)
                    model.DepartmentEmployees = departmentEmployees;
            }
            
            return model;
        }


        public PasswordEditDTO ToEmployeeUpdateDTO(EmployeeModel model) => new PasswordEditDTO
        {
            Password = model.Password,
            EmployeeDTO = new EmployeeDTO
            {
                PrivateEmailAddress = model.PrivateEmailAddress,
                PrivatePhoneNumber = model.PrivatePhoneNumber,
                WorkEmailAddress = model.WorkEmailAddress,
                WorkPhoneNumber = model.WorkPhoneNumber,
                PersonalData = new EmployeeShortDTO
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Role = new RoleDTO
                    {
                        Id = model.RoleId
                    }
                },
                DepartmentDTO=new DepartmentDTO
                {
                    Id=model.DepartmentId
                }
            }
        };

        public EmployeeModel ToRegisterModel(IEnumerable<RoleDTO> roles, IEnumerable<DepartmentDTO> departments) => new EmployeeModel
        {
            Roles = roles,
            Departments = departments
        };

        public ProjectDTO ToProjectDTO(ProjectModel model) 
        {
            ProjectDTO project = new ProjectDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Status = model.Status,
                Cost = model.Cost,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
            };
            project.LineManager = new EmployeeShortDTO { Id = model.LineManagerId };
            if (model.ProjectEmployeesIds != null)
            {
                project.ProjectEmployees = model.ProjectEmployeesIds.Select(i=>new EmployeeDTO { Id=i});
            }
            return project;
        }

        public ProjectModel ToProjectModel(IEnumerable<EmployeeShortDTO> employees = null, ProjectDTO projectDTO = null)
        {
            ProjectModel model = new ProjectModel {
                LineManager = employees.Where(e=>e.Role.Id.Equals(2)),
                Employees = employees.Where(e => !e.Role.Id.Equals(2))
            };

            if (projectDTO != null)
            {
                model.Id = projectDTO.Id;
                model.Name = projectDTO.Name;
                model.StartDate = projectDTO.StartDate;
                model.EndDate = projectDTO.EndDate;
                model.Status = projectDTO.Status;
                model.Cost = projectDTO.Cost;
                model.Description = projectDTO.Description;
                if (projectDTO.LineManager != null)
                {
                    model.LineManagerId = projectDTO.LineManager.Id;
                }
                model.ProjectEmployeesIds = new List<int>();
                if (projectDTO.ProjectEmployees.Count() != 0)
                    model.ProjectEmployeesIds = projectDTO.ProjectEmployees.Select(p => (int)p.Id).ToList();
            }
            return model;
        }

        public EmployeeModel ToEmployeeModel(EmployeeDTO employee,IEnumerable<RoleDTO> roles, IEnumerable<DepartmentDTO> departments)
        {
            EmployeeModel model = new EmployeeModel
            {
                Id = employee.Id,
                FirstName = employee.PersonalData.FirstName,
                LastName = employee.PersonalData.LastName,
                Patronymic = employee.PersonalData.Patronymic,
                PrivatePhoneNumber = employee.PrivatePhoneNumber,
                PrivateEmailAddress = employee.PrivateEmailAddress,
                WorkEmailAddress = employee.WorkEmailAddress,
                WorkPhoneNumber = employee.WorkPhoneNumber,
                Roles = roles,
                RoleId = (int)employee.PersonalData.Role.Id,
                Departments = departments
            };
            if (employee.DepartmentDTO != null)
                model.DepartmentId = employee.DepartmentDTO.Id;
            return model;
        }

    }
}
