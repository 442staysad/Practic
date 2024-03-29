﻿using Onion.Core.DTO.Employee;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Department
{
    public class DepartmentDTO : BaseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public EmployeeShortDTO Manager { get; set; }
    }
}