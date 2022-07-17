using System.Threading.Tasks;
using Onion.Core.DTO.Department;
using Onion.Core.DTO.Employee;
using Onion.Core.DTO.Role;
using Onion.Core.Entities;
using Onion.Core.Interfaces;

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
                departmentDTO.Manager = ToEmployeeDTO(department.Employee);
            else
                departmentDTO.Manager =new EmployeeDTO { EmployeeShort = new EmployeeShortDTO { FirstName="Неназначено" } };
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

        public EmployeeDTO ToEmployeeDTO(Employee employee) => new EmployeeDTO
        {
                Id = employee.Id,
                PrivateEmailAddress = employee.PrivateEmailAddress,
                PrivatePhoneNumber = employee.PrivatePhoneNumber,
                WorkEmailAddress = employee.WorkEmailAddress,
                WorkPhoneNumber = employee.WorkPhoneNumber,
                EmployeeShort = ToEmployeeShortDTO(employee),
                DepartmentDTO = ToDepartmentDTO(employee.Department)
            };

        public EmployeeShortDTO ToEmployeeShortDTO(Employee employee) => new EmployeeShortDTO
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Patronymic = employee.Patronymic,
            Role = ToRoleDTO(employee.Role)
        };

        public RoleDTO ToRoleDTO(SystemRole role) => new RoleDTO
        {
            Id=role.Id,
            RoleName=role.RoleName
        };

        public Employee ToEmployee(EmployeeUpdateDTO employeeDTO, Employee employee = null)
        {
            if (employee == null)
                employee = new Employee();

            employee.FirstName = employeeDTO.EmployeeDTO.EmployeeShort.FirstName;
            employee.LastName = employeeDTO.EmployeeDTO.EmployeeShort.LastName;
            employee.Patronymic = employeeDTO.EmployeeDTO.EmployeeShort.Patronymic;
            employee.DepartmentId = employeeDTO.EmployeeDTO.DepartmentDTO.Id;
            employee.PrivateEmailAddress = employeeDTO.EmployeeDTO.PrivateEmailAddress;
            employee.PrivatePhoneNumber = employeeDTO.EmployeeDTO.PrivatePhoneNumber;
            employee.WorkPhoneNumber = employeeDTO.EmployeeDTO.WorkPhoneNumber;
            employee.WorkEmailAddress = employeeDTO.EmployeeDTO.WorkEmailAddress;
            employee.RoleId = (int)employeeDTO.EmployeeDTO.EmployeeShort.Role.Id;
            employee.Password = employeeDTO.Password;

            return employee;
        }
    }
}