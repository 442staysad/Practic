using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeCreateDTO
    {
        public string Password { get; set; }
        public string PrivateEmailAddress { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public EmployeeDTO EmployeeDTO { get; set; }
    }
}
