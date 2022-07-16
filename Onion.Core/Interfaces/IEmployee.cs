using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;

namespace Onion.Core.Interfaces
{
    public interface IEmployee
    {
        Task<bool> IsAuthenticatedLogin(string login, string password);

        Task<int> CurrentUserId(string login);
        Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData();
    }
}
