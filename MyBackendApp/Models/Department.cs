using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackendApp.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string? DepartmentName { get; set; }
        public IEnumerable<Employee>? Employees { get; set; }
    }
}