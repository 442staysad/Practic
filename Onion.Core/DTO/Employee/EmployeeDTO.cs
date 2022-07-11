using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeDTO : BaseDTO
    {
        public string PrivateEmailAddress { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string WorkEmailAddress { get; set; }

        public EmployeeShortDTO EmployeeShort { get; set; }
        public DepartmentDTO DepartmentDTO { get; set; }

    }
}
