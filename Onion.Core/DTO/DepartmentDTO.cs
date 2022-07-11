using Onion.Core.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO
{
    public class DepartmentDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeDTO Manager { get; set; }  
    }
}