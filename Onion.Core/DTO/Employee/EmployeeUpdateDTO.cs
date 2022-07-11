using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeUpdateDTO
    {
        public string Password { get; set; }
        public EmployeeDTO EmployeeDTO { get; set; }
    }
}
