using Microsoft.AspNetCore.Mvc.Rendering;
using Onion.Core.DTO.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Web.Models.Department
{
    public class DepartmentModel
    {
        public int? Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название не указано")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Глава Отдела")]
        [ForeignKey("Employee")]
        public int? HeadOfDepartmentId { get; set; }

        public IEnumerable<EmployeeShortDTO> HeadOfDepartment { get; set; }

        public IEnumerable<EmployeeDTO> DepartmentEmployees { get; set; }
    }
}
