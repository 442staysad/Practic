using Microsoft.EntityFrameworkCore;
using Onion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace Onion.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext appDbContext)
        {
            try
            {
                AddManagingEmployee(appDbContext);
                // AddEmployee(appDbContext);
                AddDepartments(appDbContext);
                AddRootUser(appDbContext);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString() + "/n" + ex.StackTrace);
            }
        }
        private static void AddRootUser(ApplicationDbContext appDbContext)
        {
            if (!appDbContext.Employees.Any())
            {
                var employee = new Employee
                {
                    FirstName = "Glebov",
                    LastName = "Gleb",
                    Patronymic = "Arkadievich",
                    RoleId = 1,
                    PrivatePhoneNumber = (80338545533).ToString(),
                    WorkPhoneNumber = (80331112242).ToString(),
                    PrivateEmailAddress = "GlebGleb@gmail.com",
                    WorkEmailAddress = "Gleb.PlanDep@gmail.com"
                };

                appDbContext.Add(employee);
                appDbContext.SaveChanges();
            }
        }

        private static void AddManagingEmployee(ApplicationDbContext appDbContext)
        {                        
            if (!appDbContext.Employees.Any())
            {                                                    
                var employee = new Employee
                {
                    FirstName = "Pavel",
                    LastName = "Korolev",
                    Patronymic = "Dmitrievich",
                    DepartmentId = 1,
                    RoleId = 2,
                    PrivatePhoneNumber = (80334445533).ToString(),
                    WorkPhoneNumber = (80331112233).ToString(),
                    PrivateEmailAddress = "PavKor@gmail.com",
                    WorkEmailAddress = "PavKor.PlanDep@gmail.com"
                };                
                appDbContext.Add(employee);                
                appDbContext.SaveChanges();
            }
        }
        private static void AddEmployee(ApplicationDbContext appDbContext)
        {
            if (!appDbContext.Employees.Any())
            {
                var employee = new Employee
                {
                    FirstName = "Sergey",
                    LastName = "Astakhov",
                    Patronymic = "Albertovich",
                    DepartmentId = 1,
                    RoleId = 3,
                    PrivatePhoneNumber = (80334445541).ToString(),
                    WorkPhoneNumber = (80331112231).ToString(),
                    PrivateEmailAddress = "Ast@gmail.com",
                    WorkEmailAddress = "Ast.PlanDep@gmail.com"
                };
                appDbContext.Add(employee);
                appDbContext.SaveChanges();
            }
        }

        private static void AddDepartments(ApplicationDbContext appDbContext)
        {
            if (!appDbContext.Departments.Any())
            {
                var departmentDevOps = new Department
                {
                    Name = "DevOps отдел",
                    Description = "Налаживание кооперации между разработчиками и системными администраторами",
                };
                var departmentAccounting = new Department
                {
                    Name = "Бухгалтерия",
                    Description = "Обычная такая классическая бухгалтерия",
                };
                appDbContext.Add(departmentDevOps);
                appDbContext.SaveChanges();
                appDbContext.Add(departmentAccounting);
                appDbContext.SaveChanges();
            }
        }
    }
}
