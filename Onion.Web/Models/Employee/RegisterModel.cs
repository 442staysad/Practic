using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Web.Models.Employee
{
    public class RegisterModel
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Назначьте роль")]
        public int RoleId { get; set; }
        public IEnumerable<SelectListItem> Roles { get; set; }

        public int? DepartmentId { get; set; }
        public IEnumerable<SelectListItem> Departments { get; set; }

        [Required(ErrorMessage = "Добавьте E-mail")]
        public string WorkEmailAddress { get; set; }
        public string WorkPhoneNumber { get; set; }

        [Required(ErrorMessage = "Создайте пароль или сгенерируйте")]
        public string Password { get; set; }
    }
}
