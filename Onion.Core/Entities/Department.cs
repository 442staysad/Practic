using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey(nameof(Employee))]
        public int? HeadOfDepartmentId { get; set; }

        /// <summary>
        /// Глава отдела (линейный менеджер).
        /// </summary>
        public Employee Employee { get; set; }
    }
}
