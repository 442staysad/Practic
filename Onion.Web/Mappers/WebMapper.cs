using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using Onion.Web.ViewModels;
using Onion.Web.Models;
using System.Threading.Tasks;

namespace Onion.Web.Mappers
{
    public class WebMapper
    {
        public DepartmentDTO ToDepartmentDTO(CreateDepartmentModel departmentModel) => new DepartmentDTO
        {
            Name = departmentModel.Name,
            Description = departmentModel.Description,
            Manager = new EmployeeDTO
            {
                Id = departmentModel.HeadOfDepartmentId

            }
        };

        public EditDepartmentModel ToEditDepartmentModel(DepartmentDTO departmentDTO) => new EditDepartmentModel
        {
            Id = (int)departmentDTO.Id,
            Name = departmentDTO.Name,
            Description = departmentDTO.Description,
            HeadOfDepartmentId = departmentDTO.Manager.Id
        };

    }
}
