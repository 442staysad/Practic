using Onion.Core.Entities;
using Onion.Core.Interfaces;
using System.Text;
using System.Threading.Tasks;
using Onion.Core.DTO.Employee;
using System.Security.Cryptography;
using Onion.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace Onion.Core.Services
{
    public class AccountService:IAccountService
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly IMapper mapper;

        public AccountService(IRepository<Employee> employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        public async Task<bool> IsAuthenticatedLogin(string login, string password) =>
               await employeeRepository.FindAsync(x => x.WorkEmailAddress == login && x.Password == GetMD5HashData(password.ToString())) != null;

        public async Task Register(PasswordEditDTO employeeDto)
        {
            employeeDto.Password = GetMD5HashData(employeeDto.Password);
            await employeeRepository.AddAsync(mapper.ToEmployee(employeeDto.EmployeeDTO, employeeDto.Password));
        }

        public async Task<EmployeeDTO> GetUserByUsername(string username)
            => mapper.ToEmployeeDTO( await employeeRepository.FindAsync(e=>e.WorkEmailAddress.Equals(username),
                                    d=>d.Include(r=>r.Role).Include(d=>d.Department).Include(p=>p.Projects).ThenInclude(p=>p.Project)));

        /// <summary>
        /// take any string and encrypt it using MD5 then
        /// return the encrypted data 
        /// </summary>
        /// <param name="input">input text you will enterd to encrypt it</param>
        /// <returns>return the encrypted text as hexadecimal string</returns>
        public string GetMD5HashData(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
