using Onion.Core.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Project
{
    public class ProjectDTO:BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public EmployeeShortDTO? LineManager { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        public IEnumerable<EmployeeDTO>? ProjectEmployees { get; set; }
    }
}
