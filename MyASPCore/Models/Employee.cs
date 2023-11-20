using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyASPCore.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Department? Department { get; set; }

    }
}