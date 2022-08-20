using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data.SqlClient;
using Onion.Core.Interfaces;
using Onion.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Onion.Core.DTO.Department;
using Onion.Core.Interfaces.Services;

namespace Onion.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<Department> departmentRepository;
        private readonly IMapper mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper,IRepository<Employee> employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this.departmentRepository = departmentRepository;
            this.mapper = mapper;
        }

        public async Task Create(DepartmentDTO departmentDto)
        {
            try{
                await departmentRepository.AddAsync(mapper.ToDepartment(departmentDto));
            }
            catch(Exception ex){
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task Update(DepartmentDTO departmentDto)
        {
            try
            {
                Department department =await departmentRepository.FindAsync(d=>d.Id.Equals(departmentDto.Id));

                if (department.HeadOfDepartmentId.HasValue&&department.HeadOfDepartmentId!=departmentDto.Manager.Id)
                {
                    Employee currentManager = await employeeRepository.FindAsync(e => e.Id.Equals(department.HeadOfDepartmentId));
                    await RemoveFromDepartment(currentManager.Id);
                }
                await departmentRepository.UpdateAsync(mapper.ToDepartment(departmentDto, department));

                if (departmentDto.Manager.Id.HasValue)
                {
                    Employee newManager = await employeeRepository.FindAsync(e => e.Id.Equals(departmentDto.Manager.Id));
                    newManager.DepartmentId = department.Id;
                    await employeeRepository.UpdateAsync(newManager);
                }

            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await departmentRepository.FindAsync(d => d.Id == id);
            await departmentRepository.DeleteAsync(department);
        }

        public async Task RemoveFromDepartment(int empId)
        {
            try
            {
                var employee = await employeeRepository.FindAsync(e => e.Id.Equals(empId));
                employee.DepartmentId = null;
                await employeeRepository.UpdateAsync(employee);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await departmentRepository.FindAsync(d => d.Id.Equals(id),e=>e.Include(d=>d.Employee).ThenInclude(r=>r.Role));
            return mapper.ToDepartmentDTO(department);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsList(string sortField = null,string sortDirection = null, string filterString = null)
        {
            var departments =departmentRepository.GetAll(d=>d.Include(e => e.Employee)
                                                              .ThenInclude(r => r.Role)).AsQueryable().ToList().Select(mapper.ToDepartmentDTO);

            if (!String.IsNullOrEmpty(filterString))
                departments =departments.Where(d => d.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase)
                                                || d.Manager.FullName.Contains(filterString,StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(sortField))
                if (sortField == "Name")
                    departments = sortDirection == "Ascending"? departments.OrderBy(d => d.Name)
                                                              : departments.OrderByDescending(d => d.Name);
                else
                    departments =sortDirection == "Ascending"? departments.OrderBy(d => d.Manager.FullName)
                                                             : departments.OrderByDescending(d => d.Manager.FullName);

            return await Task.Run(() => departments);
        }
    }
}
