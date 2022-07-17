using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.DTO.Role;

namespace Onion.Core.Services
{
    public class RoleManagingService : IRoleManager
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IRepository<SystemRole> roleRepository;
        private readonly IMapper mapper;

        public RoleManagingService(IRepository<Employee> employeeRepository, IRepository<SystemRole> roleRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.roleRepository = roleRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles() 
        { 
            var roles = await roleRepository.GetAllAsync();
            return roles.Select(r=>mapper.ToRoleDTO(r));
        }
    }
}
