using Onion.Core.DTO.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Onion.Core.Interfaces.Services
{
    public interface IRoleManager
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
    }
}
