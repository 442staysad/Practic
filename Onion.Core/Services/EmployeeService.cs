using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Onion.Core.Services
{
    public class EmployeeService : IEmployee
    {
        IRepository<Employee> userRepository;

        public EmployeeService(IRepository<Employee> userRepository)
        {
            this.userRepository = userRepository;
        }

        public int CurrentUserId(string login)
        {
            var currentUser = userRepository.Find(x => x.WorkEmailAddress == login);
            if (currentUser != null)
            {
                return currentUser.Id;
            }
            else
            {
                return 1;
            }
        }

        public bool IsAuthenticatedLogin(string login, string password)
        {
            string passwordEntered = password;
            if (userRepository.Find(x => x.WorkEmailAddress == login && x.Password == passwordEntered) != null)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
