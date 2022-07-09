using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeDTO : BaseDTO
    {
        public EmployeeContactsDTO EmployeeContactsDTO { get; set; }
        public EmployeeShortDTO EmployeeShort { get; set; }
        public DepartmentDTO DepartmentDTO { get; set; }
    }
}
