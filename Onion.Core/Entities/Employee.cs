using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Onion.Core.Entities
{
    [Table("Employees")]
    public class Employee : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public SystemRole Role { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }
        public string PrivateEmailAddress { get; set; }
        public string WorkEmailAddress { get; set; }
        public string Password { get; set; }
    }
}
