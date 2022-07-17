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

namespace Onion.Core.Services
{
    public class DepartmentService : IDepartment
    {
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateDepartment(DepartmentDTO departmentDto)
        {
            if (departmentDto.Id == null)
            {
                await _departmentRepository.AddAsync( _mapper.ToDepartment(departmentDto));
            }
            else
            {
                Department department = await _departmentRepository.FindAsync(d => d.Id == departmentDto.Id,d=>d.Include(e=>e.Employee));

                try{
                    await _departmentRepository.UpdateAsync(_mapper.ToDepartment(departmentDto, department));
                }
                catch{
                    throw new ArgumentException(nameof(department));
                }
            }
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _departmentRepository.FindAsync(d => d.Id == id);
            await _departmentRepository.DeleteAsync(department);
        }
        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.FindAsync(d => d.Id == id);
            return _mapper.ToDepartmentDTO(department);
        }
        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsList(string sortField = null,string sortDirection = null, string filterString = null)
        {
            var departments =_departmentRepository.GetAll(d=>d.Include(e => e.Employee)
                                                              .ThenInclude(r => r.Role)).AsQueryable().ToList().Select(_mapper.ToDepartmentDTO);

            if (!String.IsNullOrEmpty(filterString))
                departments =departments.Where(d => d.Name.Contains(filterString, StringComparison.OrdinalIgnoreCase)
                                                || d.Manager.EmployeeShort.FullName.Contains(filterString,StringComparison.OrdinalIgnoreCase));

            if (!String.IsNullOrEmpty(sortField))
                if (sortField == "Name")
                    departments = sortDirection == "Ascending"? departments.OrderBy(d => d.Name)
                                                              : departments.OrderByDescending(d => d.Name);
                else
                    departments =sortDirection == "Ascending"? departments.OrderBy(d => d.Manager.EmployeeShort.FullName)
                                                             : departments.OrderByDescending(d => d.Manager.EmployeeShort.FullName);

            return await Task.Run(() => departments);
        }
    }
}
