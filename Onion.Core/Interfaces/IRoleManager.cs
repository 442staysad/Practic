using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.Core.Interfaces
{
    public interface IRoleManager
    {
        public string GetCurrentRoleOfSelectedUser(string login);
        public IQueryable<SystemRole> GetAllRoles();

        public bool IsRootUser(string login);

        public bool IsHeadOfDepartment(string login);
    }
}
