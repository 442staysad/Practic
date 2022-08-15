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
using Onion.Core.Interfaces.Services;

namespace Onion.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<Department> departmentRepository;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;

        public EmployeeService(IRepository<Employee> employeeRepository, IRepository<Department> departmentRepository, IMapper mapper, IAccountService accountService)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
            this.accountService = accountService;
        }

        public async Task<int> CurrentUserId(string login)
        {
            var currentUser = await employeeRepository.FindAsync(x => x.WorkEmailAddress == login);
            return currentUser == null ? 1 : currentUser.Id;
        }

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

        public async Task EditEmployee(PasswordEditDTO employeeUpdateDto)
        {
            try
            {
                var employee = await employeeRepository.FindAsync(e => e.Id.Equals(employeeUpdateDto.EmployeeDTO.Id));
                await employeeRepository.UpdateAsync(mapper.ToEmployee(employeeUpdateDto.EmployeeDTO, null, employee));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task EmployeeToDepartment(int employeeId, int? departmentId)
        {
            var employee = await employeeRepository.FindAsync(e => e.Id.Equals(employeeId));
            employee.DepartmentId = departmentId;

            try
            {
                await employeeRepository.UpdateAsync(employee);
            }
            catch(Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesByDepartmentId(int id)
        {
            var employees = await employeeRepository.FindAllAsync(e => e.DepartmentId.Equals(id));
            return employees.Select(e=>mapper.ToEmployeeDTO(e));
        }

        public async Task RemoveFromDepartment(int id)
        {
            try
            {
                var employee = await employeeRepository.FindAsync(e => e.Id.Equals(id));
                employee.DepartmentId = null;
                await employeeRepository.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesList(string sortField = null, string sortDirection = null, string filterString = null)
        {
            var employees = employeeRepository.GetAll(e=>e.Include(r=>r.Role).Include(d=>d.Department ))
                                              .ToList().Select(mapper.ToEmployeeDTO);

            if (!String.IsNullOrEmpty(filterString))
                employees = employees.Where(d => d.PersonalData.FullName
                                        .Contains(filterString, StringComparison.OrdinalIgnoreCase)
                                        ||d.DepartmentDTO.Name
                                        .Contains(filterString, StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(sortField))
                if (sortField == "Name")
                    employees = sortDirection == "Ascending" ? employees.OrderBy(d => d.PersonalData.FullName)
                                                              : employees.OrderByDescending(d => d.PersonalData.FullName);
                else
                    employees = sortDirection == "Ascending" ? employees.OrderBy(d => d.DepartmentDTO.Name)
                                                             : employees.OrderByDescending(d => d.DepartmentDTO.Name);

            return await Task.Run(() => employees);
        }

        public async Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData()
            => await Task.Run(() => employeeRepository.GetAll(e => e.Include(r => r.Role)).ToList().Select(mapper.ToEmployeeShortDTO));
        
        public async Task<EmployeeDTO> GetEmployeeById(int id) 
            => mapper.ToEmployeeDTO(await employeeRepository.FindAsync(e => e.Id.Equals(id),e=>e.Include(d=>d.Department).Include(r=>r.Role)));

    }
}
