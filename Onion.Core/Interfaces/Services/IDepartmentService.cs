using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.Entities;
using Onion.Core.DTO.Department;

namespace Onion.Core.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task Create(DepartmentDTO projectDTO);
        Task Update(DepartmentDTO projectDTO);
        Task DeleteDepartment(int id);
        Task<DepartmentDTO> GetDepartmentById(int id);
        Task<IEnumerable<DepartmentDTO>> GetDepartmentsList(string sortField = null,
                                                            string sortDirection = null,
                                                            string filterSting = null);
    }
}
