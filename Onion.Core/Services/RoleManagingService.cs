using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.Core.Services
{
    public class RoleManagingService : IRoleManager
    {
        IRepository<Employee> _employeeRepository;
        IRepository<SystemRole> _roleRepository;

        public RoleManagingService(IRepository<Employee> employeeRepository, IRepository<SystemRole> roleRepository)
        {
            _employeeRepository = employeeRepository;
            _roleRepository = roleRepository;
        }
        
        public IQueryable<SystemRole> GetAllRoles()
        {
            IQueryable<SystemRole> allRoles = _roleRepository.GetAll();            
            return allRoles;
        }
    }
}
