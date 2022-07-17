using Onion.Core.DTO.Role;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeShortDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public RoleDTO Role { get; set; }

        public string FullName => $"{LastName} {FirstName} {Patronymic}";
    }
}
