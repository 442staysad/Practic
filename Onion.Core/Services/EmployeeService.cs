using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;
using System.Security.Cryptography;

namespace Onion.Core.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<int> CurrentUserId(string login)
        {
            var currentUser = await employeeRepository.FindAsync(x => x.WorkEmailAddress == login);
            return currentUser == null ? 1 : currentUser.Id;
        }

        public async Task EmployeeToDepartment(int employeeId, int? departmentId)
        {
            var employee =await employeeRepository.FindAsync(e => e.Id.Equals(employeeId));
                employee.DepartmentId = departmentId;

            await employeeRepository.UpdateAsync(employee);
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesList(string sortField, string filterString, string filterDirection)
        {
            var employees = employeeRepository.GetAll().Select(mapper.ToEmployeeDTO);
            return await Task.Run(() => employees);
        }

        public async Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData() => 
               await Task.Run(() => employeeRepository.GetAll(e => e.Include(r => r.Role)).Select(mapper.ToEmployeeShortDTO));

        public async Task<EmployeeDTO> GetEmployeeById(int id) => mapper.ToEmployeeDTO(await employeeRepository.FindAsync(e => e.Id.Equals(id)));

        public async Task<bool> DeleteEmployee(int id)
        {
            var item = await employeeRepository.FindAsync(e => e.Id == id);

            if (item != null)
            {
                await employeeRepository.DeleteAsync(item);
                return true;
            }
            return false;
        }

        public async Task EditEmployee(EmployeeUpdateDTO employeeDto)
        {
            var employee =await employeeRepository.FindAsync(e => e.Id.Equals(employeeDto.EmployeeDTO.Id));
            await  employeeRepository.UpdateAsync(mapper.ToEmployee(employeeDto,employee));
        }

    }
}
