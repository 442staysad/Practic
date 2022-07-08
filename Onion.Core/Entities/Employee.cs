using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Onion.Core.Entities
{
    public class Employee : BaseEntity
    {

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Личный номер мобильного телефона.
        /// </summary>
        public string PrivatePhoneNumber { get; set; }

        /// <summary>
        /// Рабочий номер мобильного телефона.
        /// </summary>
        public string WorkPhoneNumber { get; set; }

        /// <summary>
        /// Внешний ключ навигационного свойства "Role".
        /// </summary>
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        /// <summary>
        /// Роль. 
        /// </summary>
        public SystemRole Role { get; set; }

        /// <summary>
        /// Внешний ключ навигационного свойства "Department".
        /// </summary>
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }

        /// <summary>
        /// Отдел.
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Личный адрес электронной почты.
        /// </summary>
        public string PrivateEmailAddress { get; set; }
        /// <summary>
        /// Рабочий адрес электронной почты.
        /// </summary>
        public string WorkEmailAddress { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; }
    }
}
