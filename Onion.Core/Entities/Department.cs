using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Core.Entities
{
    [Table("Departments")]
    public class Department : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<int> HeadOfDepartmentId { get; set; }
        [ForeignKey(nameof(HeadOfDepartmentId))]
        public Employee? Employee { get; set; }
    }
}
