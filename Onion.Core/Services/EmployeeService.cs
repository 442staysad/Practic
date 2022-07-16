using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;

namespace Onion.Core.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public EmployeeService(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<int> CurrentUserId(string login)
        {
            var currentUser = await employeeRepository.FindAsync(x => x.WorkEmailAddress == login);
            return currentUser == null ? 1 : currentUser.Id;
        }

        public async Task<IEnumerable<EmployeeShortDTO>> GetEmployeeShortData() => 
               await Task.Run(() => employeeRepository.GetAll().Include(r=>r.Role).Select(e => mapper.ToEmployeeShortDTO(e)).AsNoTracking().AsEnumerable());

        public async Task<bool> IsAuthenticatedLogin(string login, string password) =>
               await employeeRepository.FindAsync(x => x.WorkEmailAddress == login && x.Password == password) != null;
    }
}
