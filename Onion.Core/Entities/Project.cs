using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Onion.Core.Entities
{
    [Table("Projects")]
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        
        public Nullable<int> LineManagerId { get; set; }
        [ForeignKey(nameof(LineManagerId))]
        public Employee LineManager { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Cost { get; set; }
        
        public virtual ICollection<ProjectEmployees>? Employees { get; set; }

        public Project()
        {
            Employees = new List<ProjectEmployees>();
        }

    }
}
