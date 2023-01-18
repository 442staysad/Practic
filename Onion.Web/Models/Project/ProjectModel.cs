using Microsoft.AspNetCore.Mvc.Rendering;
using Onion.Core.DTO.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Web.Models.Project
{
    public class ProjectModel
    {
        public int? Id { get; set; }
        [Required]
        [DisplayName("Название проекта")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

        [DisplayName("Статус")]
        public string Status { get; set; }

        [DisplayName("Ответственный")]
        public int? LineManagerId { get; set; }
        public IEnumerable<EmployeeShortDTO>? LineManager { get; set; }


        [DisplayName("Дата начала")]
        [Required]
        public DateTime? StartDate { get; set; }

        [DisplayName("Дата окончания")]
        [Required]
        public DateTime? EndDate { get; set; }


        [RegularExpression(@"^-?[0-9]*\.?[0-9]+$")]
        [DisplayName("Стоимость")]
        public decimal? Cost { get; set; }


        public IEnumerable<EmployeeShortDTO>? Employees { get; set; }
        public List<int>? ProjectEmployeesIds { get; set; }
    }
}
