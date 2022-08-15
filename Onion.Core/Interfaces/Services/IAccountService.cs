using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;

namespace Onion.Core.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> IsAuthenticatedLogin(string login, string password);
        Task Register(PasswordEditDTO employeeDto);
        string GetMD5HashData(string input);
    }
}
