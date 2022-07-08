using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Onion.Core.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// Название отдела.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание отдела.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Внешний ключ навигационного свойства "HeadOfDepartment", или глава отдела (представлен как линейный менеджер).
        /// </summary>
        [ForeignKey(nameof(HeadOfDepartment))]
        public int? HeadOfDepartmentId { get; set; }

        /// <summary>
        /// Глава отдела (линейный менеджер).
        /// </summary>
        public Employee HeadOfDepartment { get; set; }
    }
}
