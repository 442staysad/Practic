using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Onion.Core.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IRepository<Employee> userRepository;

        public EmployeeService(IRepository<Employee> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> CurrentUserId(string login)
        {
            var currentUser = await userRepository.FindAsync(x => x.WorkEmailAddress == login);
            return await Task.Run(()=> currentUser == null ? 1 : currentUser.Id);
        }

        public async Task<bool> IsAuthenticatedLogin(string login, string password)
        {
            return await Task.Run(() => userRepository.Find(x => x.WorkEmailAddress == login && x.Password == password) != null);
        }
    }
}
