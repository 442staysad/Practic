using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.Entities;
using Onion.Core.DTO.Project;

namespace Onion.Core.Interfaces.Services
{
    public interface IProjectService
    {
        Task Create(ProjectDTO projectDTO);
        Task Update(ProjectDTO projectDTO);
        Task Delete(int id);
        Task<ProjectDTO> GetProjectById(int id);
        Task<IEnumerable<ProjectDTO>> GetProjectsList(string sortField=null,
                                                       string sortDirection = null,
                                                       string filterString = null,
                                                       DateTime? startDate = null,
                                                       DateTime? endDate = null);
    }
}
