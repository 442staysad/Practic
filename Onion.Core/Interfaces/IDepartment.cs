using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.Entities;
using Onion.Core.DTO;

namespace Onion.Core.Interfaces
{
    public interface IDepartment
    {

        Task CreateOrUpdateDepartment(DepartmentDTO departmentDto);
        Task DeleteDepartment(int id);
        Task<DepartmentDTO> GetDepartmentById(int id);
        Task<IEnumerable<DepartmentDTO>> GetDepartmentsList(
            string sortField,
            string sortDirection,
            string filterSting);
    }
}
