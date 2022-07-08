using Microsoft.EntityFrameworkCore;
using Onion.Core.Entities;
using System;
using System.Collections.Generic;

namespace Onion.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<SystemRole> SystemRoles { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; } 

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var systemRoles = new[]{new SystemRole() { Id = 1, RoleName = "RootUser" },
                                    new SystemRole() { Id = 2, RoleName = "LineManager" },
                                    new SystemRole() { Id = 3, RoleName = "TeamLeader" },
                                    new SystemRole() { Id = 4, RoleName = "User" }};

            modelBuilder.Entity<SystemRole>().HasData(systemRoles);
        }
    }
}
