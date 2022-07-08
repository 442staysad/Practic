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

        public string GetCurrentRoleOfSelectedUser(string login)
        {
            var user = _employeeRepository.Find(x => x.WorkEmailAddress == login);
            if (user != null)
            {
                var currentRole = user.Role.RoleName;
                return currentRole; 
            }
            else
            {
                return "Пользователь не найден.";
            }
        }

        public bool IsRootUser(string login)
        {
            var user = _employeeRepository.Find(x => x.WorkEmailAddress == login);
            if (user != null && user.RoleId == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsHeadOfDepartment(string login)
        {
            var user = _employeeRepository.Find(x => x.WorkEmailAddress == login);
            if (user != null && user.RoleId == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IQueryable<SystemRole> GetAllRoles()
        {
            IQueryable<SystemRole> allRoles = _roleRepository.GetAll();            
            return allRoles;
        }
    }
}
