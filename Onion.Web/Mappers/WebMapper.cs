using Onion.Core.DTO.Employee;
using Onion.Web.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Onion.Web.Models.Employee;
using Onion.Web.Models.Department;
using Onion.Core.DTO.Department;
using Onion.Core.DTO.Role;

namespace Onion.Web.Mappers
{
    public class WebMapper
    {
        public DepartmentDTO ToDepartmentDTO(CreateDepartmentModel departmentModel) => new DepartmentDTO
        {
            Name = departmentModel.Name,
            Description = departmentModel.Description,
            Manager = new EmployeeDTO
            {
                Id = departmentModel.HeadOfDepartmentId

            }
        };

        public EditDepartmentModel ToEditDepartmentModel(DepartmentDTO departmentDTO) => new EditDepartmentModel
        {
            Id = (int)departmentDTO.Id,
            Name = departmentDTO.Name,
            Description = departmentDTO.Description,
            HeadOfDepartmentId = departmentDTO.Manager.Id
        };

        public EmployeeUpdateDTO ToEmployeeUpdateDTO(RegisterModel model) => new EmployeeUpdateDTO
        {
            Password = model.Password,
            EmployeeDTO = new EmployeeDTO
            {
                WorkEmailAddress = model.WorkEmailAddress,
                WorkPhoneNumber = model.WorkPhoneNumber,
                EmployeeShort = new EmployeeShortDTO
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

        public RegisterModel ToRegisterModel(IEnumerable<RoleDTO> roles, IEnumerable<DepartmentDTO> departments) => new RegisterModel
        {
            Roles = roles.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.RoleName
            }),
            Departments = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            })
        };

        public EditEmployeeModel ToEditEmployeeModel(EmployeeDTO employee,IEnumerable<RoleDTO> roles, IEnumerable<DepartmentDTO> departments) => new EditEmployeeModel
        {
            Id = (int)employee.Id,
            FirstName = employee.EmployeeShort.FirstName,
            LastName = employee.EmployeeShort.LastName,
            Patronymic = employee.EmployeeShort.Patronymic,
            WorkEmailAddress = employee.WorkEmailAddress,
            WorkPhoneNumber = employee.WorkPhoneNumber,
            Roles = roles.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.RoleName
            }),
            Departments = departments.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Name
            })

        };

    }
}
