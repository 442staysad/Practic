using Onion.Core.Entities;
using Onion.Core.DTO.Employee;
using System.Threading.Tasks;
using Onion.Core.DTO.Department;
using Onion.Core.DTO.Role;

namespace Onion.Core.Interfaces
{
    public interface IMapper
    {
        DepartmentDTO ToDepartmentDTO(Department department);
        Department ToDepartment(DepartmentDTO departmentDTO, Department department = null);
        EmployeeDTO ToEmployeeDTO(Employee employee);
        EmployeeShortDTO ToEmployeeShortDTO(Employee employee);
        Employee ToEmployee(EmployeeUpdateDTO employeeDTO,Employee employee=null);
        RoleDTO ToRoleDTO(SystemRole systemRole);
    }
}
