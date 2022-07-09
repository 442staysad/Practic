using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Onion.Core.Interfaces
{
    public interface IRoleManager
    {
        public IQueryable<SystemRole> GetAllRoles();
    }
}
