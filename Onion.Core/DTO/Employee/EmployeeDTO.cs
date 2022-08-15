﻿using Onion.Core.DTO.Department;
using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Core.DTO.Employee
{
    public class EmployeeDTO : BaseDTO
    {
        public string PrivateEmailAddress { get; set; }
        public string PrivatePhoneNumber { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string WorkEmailAddress { get; set; }

        public EmployeeShortDTO? PersonalData { get; set; }
        public DepartmentDTO? DepartmentDTO { get; set; }

    }
}
