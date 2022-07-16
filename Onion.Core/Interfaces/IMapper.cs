using Onion.Core.Entities;
using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces
{
    public interface IMapper
    {
        DepartmentDTO ToDepartmentDTO(Department department);
        Department ToDepartment(DepartmentDTO departmentDTO, Department department = null);
        EmployeeDTO ToEmployeeDTO(Employee employee);
        EmployeeShortDTO ToEmployeeShortDTO(Employee employee);
        Employee ToEmployee(EmployeeUpdateDTO employeeDTO,Employee employee=null);
    }
}
