using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.Entities
{
    public class ProjectEmployees
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
