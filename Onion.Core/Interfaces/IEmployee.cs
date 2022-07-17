using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;

namespace Onion.Core.Interfaces
{
    public interface IEmployee
    {
        Task<int> CurrentUserId(string login);
        Task<bool> DeleteEmployee(int id);
        Task EmployeeToDepartment(int employeeId, int? departmentId);
        Task EditEmployee(EmployeeUpdateDTO employeeDto);
        Task<IEnumerable<EmployeeDTO>> GetEmployeesList(string sortField, string filterString, string filterDirection);
        Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData();
        Task<EmployeeDTO> GetEmployeeById(int id);
    }
}
