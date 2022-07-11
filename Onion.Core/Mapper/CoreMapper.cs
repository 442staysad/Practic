using System.Threading.Tasks;
using Onion.Core.DTO;
using Onion.Core.DTO.Employee;
using Onion.Core.Entities;
 
namespace Onion.Core.Mapper
{
    public class CoreMapper
    {
        public async Task<DepartmentDTO> ToDepartmentDTO(Department department)
        {
            return await Task.Run(() => new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                Manager = new EmployeeDTO
                {
                    Id = department.Employee.Id,
                    EmployeeShort = new EmployeeShortDTO
                    {
                        FirstName = department.Employee.FirstName,
                        LastName = department.Employee.LastName,
                        Patronymic = department.Employee.Patronymic,
                    }
                }
            });
        }

        public async Task<Department> ToDepartment(DepartmentDTO departmentDTO, Department department = null)
        {
            if (department == null)
                department = new Department();
            department.Name = departmentDTO.Name;
            department.Description = departmentDTO.Description;
            department.HeadOfDepartmentId = departmentDTO.Manager.Id;
            department.Employee.DepartmentId = department.Id;

            return await Task.Run(() => department);
        }

        public async Task<EmployeeDTO> ToEmployeeDTO(Employee employee)
        {
            return await Task.Run(() =>new EmployeeDTO
            {
                Id=employee.Id,
                PrivateEmailAddress=employee.PrivateEmailAddress,
                PrivatePhoneNumber=employee.PrivatePhoneNumber,
                WorkEmailAddress=employee.WorkEmailAddress,
                WorkPhoneNumber=employee.WorkPhoneNumber,
                EmployeeShort=new EmployeeShortDTO
                {
                   FirstName=employee.FirstName,
                   LastName=employee.LastName,
                   Patronymic=employee.Patronymic,
                   Role=new RoleDTO
                   {
                       Id=employee.RoleId
                   }
                },
                DepartmentDTO=new DepartmentDTO
                {
                    Id=employee.DepartmentId,
                    Name=employee.Department.Name,
                }
            });
        }

        public async Task<Employee> ToEmployee(EmployeeUpdateDTO employeeDTO,Employee employee=null)
        {
            if (employee == null)
                employee = new Employee();

            employee.FirstName = employeeDTO.EmployeeDTO.EmployeeShort.FirstName;
            employee.LastName = employeeDTO.EmployeeDTO.EmployeeShort.LastName;
            employee.Patronymic = employeeDTO.EmployeeDTO.EmployeeShort.Patronymic;
            employee.DepartmentId = employeeDTO.EmployeeDTO.DepartmentDTO.Id;
            employee.PrivateEmailAddress = employeeDTO.EmployeeDTO.PrivateEmailAddress;
            employee.PrivatePhoneNumber = employeeDTO.EmployeeDTO.PrivatePhoneNumber;
            employee.WorkPhoneNumber = employeeDTO.EmployeeDTO.WorkPhoneNumber;
            employee.WorkEmailAddress = employeeDTO.EmployeeDTO.WorkEmailAddress;
            employee.RoleId = (int)employeeDTO.EmployeeDTO.EmployeeShort.Role.Id;
            employee.Password = employeeDTO.Password;

            return await Task.Run(() => employee);
        }
    }
}