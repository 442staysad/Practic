using Onion.Core.Entities;
using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces
{
    public interface IMapper
    {
        Task<DepartmentDTO> ToDepartmentDTO(Department department);
        Task<Department> ToDepartment(DepartmentDTO departmentDTO, Department department = null);
        Task<EmployeeDTO> ToEmployeeDTO(Employee employee);
        Task<Employee> ToEmployee(EmployeeDTO employeeDTO);
    }
}
