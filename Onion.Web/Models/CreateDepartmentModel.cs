using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Web.Models
{
    public class CreateDepartmentModel
    {
        [Display(Name = "Название")]
        [Required(ErrorMessage = "Название не указано")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Глава Отдела")]
        [ForeignKey("Employee")]
        public int? HeadOfDepartmentId { get; set; }

        public IEnumerable<SelectListItem> HeadOfDepartment { get; set; }
    }
}
