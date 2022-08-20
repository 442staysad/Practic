using Onion.Core.DTO.Department;
using Onion.Core.DTO.Employee;
using Onion.Core.DTO.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Onion.Web.Models.Dashboard
{
    public class GanttModel
    {
        public DateTime? DateStart { get; set; } = null;
        public DateTime? DateEnd { get; set; } = null;

        public bool? IsDaysShow { get; set; } = null;

        public int? ProjectId { get; set; }
        public IEnumerable<ProjectDTO>? Projects { get; set; } = null;
        
        public int? Departmentid { get; set; }
        public IEnumerable<DepartmentDTO>? Departments { get; set; } = null;

        public IEnumerable<EmployeeDTO>? Employees { get; set; } = null;

    }
}
