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
        private readonly IRepository<Department> _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IRepository<Department> departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        public async Task Create(DepartmentDTO departmentDto)
        {
            try{
                await _departmentRepository.AddAsync(_mapper.ToDepartment(departmentDto));
            }
            catch(Exception ex){
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task Update(DepartmentDTO departmentDto)
        {
            try
            {

                Department department = await _departmentRepository.FindAsync(d => d.Id.Equals(departmentDto.Id), d => d.Include(e => e.Employee));
                await _departmentRepository.UpdateAsync(_mapper.ToDepartment(departmentDto, department));
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.InnerException.Message);
            }
        }

        public async Task DeleteDepartment(int id)
        {
            var department = await _departmentRepository.FindAsync(d => d.Id == id);
            await _departmentRepository.DeleteAsync(department);
        }

        public async Task<DepartmentDTO> GetDepartmentById(int id)
        {
            var department = await _departmentRepository.FindAsync(d => d.Id.Equals(id),e=>e.Include(d=>d.Employee).ThenInclude(r=>r.Role));
            return _mapper.ToDepartmentDTO(department);
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartmentsList(string sortField = null,string sortDirection = null, string filterString = null)
        {
            var departments =_departmentRepository.GetAll(d=>d.Include(e => e.Employee)
                                                              .ThenInclude(r => r.Role)).AsQueryable().ToList().Select(_mapper.ToDepartmentDTO);

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
