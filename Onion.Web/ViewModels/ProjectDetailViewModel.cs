using System;

namespace Onion.Web.ViewModels
{
    public class ProjectDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int LineManagerId { get; set; }
        public string LineManager { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Cost { get; set; }
        public PaginatedListViewModel ProjectEmployee { get; set; }
    }
}
