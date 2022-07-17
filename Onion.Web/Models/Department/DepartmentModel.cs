using System;
using System.Collections.Generic;
using Onion.Core.DTO.Employee;

namespace Onion.Web.Models.Department
{
    public class DepartmentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manager { get; set; }
        public IEnumerable<EmployeeShortDTO> Employees { get; set; }

    }
}
