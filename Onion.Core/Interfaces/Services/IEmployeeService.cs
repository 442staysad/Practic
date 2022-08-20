using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;

namespace Onion.Core.Interfaces.Services
{
    public interface IEmployeeService
    {
        Task<int> CurrentUserId(string login);
        Task<bool> DeleteEmployee(int id);
        Task EmployeeToDepartment(int employeeId, int? departmentId);
        Task EditEmployee(PasswordEditDTO employeeDto);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesList(string sortField = null,
                                                        string sortDirection = null,
                                                        string filterString = null);
        Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData();
        Task<IEnumerable<EmployeeDTO>> GetEmployeesByDepartmentId(int id);
        Task<EmployeeDTO> GetEmployeeById(int id);
        Task<IEnumerable<EmployeeDTO>> PopolateChart(int? depId = null, int? projId = null);
    }
}
